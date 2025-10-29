using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Reflection;
using System.Diagnostics; 
using System.Threading; 
using System.Collections.Generic; 
using System.Text; 

namespace steamToolsSeguro
{
    public partial class Form1 : Form
    {
        // Caminhos e Nomes Finais
        private const string SteamPath = @"C:\Program Files (x86)\Steam\steam.exe";
        private const string SteamProcessName = "steam"; 
        
        // Caminho da DLL e Pasta de Destino
        private const string DllFileName = "hid.dll";
        private const string SteamRootDirectory = @"C:\Program Files (x86)\Steam";

        // ====================================================================
        // 1. Mapeamento de Extensões -> Diretórios de Destino
        // ====================================================================
        private readonly System.Collections.Generic.Dictionary<string, string> MAPPING =
            new System.Collections.Generic.Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { ".manifest", @"C:\Program Files (x86)\Steam\depotcache" }, 
            { ".lua", @"C:\Program Files (x86)\Steam\config\stplug-in" }, 
        };

        // ====================================================================
        // 2. Lógica para Mover a Janela (User32 P/Invoke)
        // ====================================================================
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2; 

        public Form1()
        {
            InitializeComponent();
            
            // Define o ícone da Barra de Tarefas
            try
            {
                // Carregamento do Ícone como Recurso Embutido
                var assembly = Assembly.GetExecutingAssembly();
                var iconStream = assembly.GetManifestResourceStream("steamToolsSeguro.AppIcon.ico");
                
                if (iconStream != null)
                {
                    this.Icon = new Icon(iconStream);
                    iconStream.Seek(0, SeekOrigin.Begin); 
                    this.BackgroundImage = new Bitmap(iconStream);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar o ícone: {ex.Message}", "Aviso de Ícone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Estilo do Menu de Contexto (Azul e Negrito)
            this.contextMenuStrip1.Font = new Font("Segoe UI", 11F, FontStyle.Bold); 
            this.contextMenuStrip1.ForeColor = Color.DodgerBlue; 

            foreach (ToolStripMenuItem item in this.contextMenuStrip1.Items)
            {
                 item.ForeColor = Color.DodgerBlue;
                 item.Font = this.contextMenuStrip1.Font; 
            }

            // Eventos para mover a janela e carregar
            this.MouseDown += MoveWindow_MouseDown;
            this.Load += Form1_Load;
        }

        // Checa se o processo está ativo
        private bool IsProcessRunning(string name)
        {
            return Process.GetProcessesByName(name).Length > 0;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            // 🚨 NOVO: Instala a DLL antes de posicionar a janela
            InstallRequiredDll();
            
            // Fix de tamanho
            this.ClientSize = new Size(64, 64);
            this.Size = new Size(64, 64);
            
            // CÁLCULO DE POSICIONAMENTO (25% do topo, Centro Horizontal)
            Screen primaryScreen = Screen.PrimaryScreen;
            int x = (primaryScreen.WorkingArea.Width - this.Width) / 2;
            int y = (int)(primaryScreen.WorkingArea.Height * 0.25) - (this.Height / 2);
            this.Location = new Point(x, y);
        }
        
        // ====================================================================
        // LÓGICA DE INSTALAÇÃO VIA RECURSO EMBUTIDO (Autocontida)
        // ====================================================================
        private void InstallRequiredDll()
        {
            string destinationPath = Path.Combine(SteamRootDirectory, DllFileName);

            // Se o arquivo já existir, não faz nada
            if (File.Exists(destinationPath))
            {
                return;
            }

            // Tenta obter o recurso embutido (de dentro do .exe)
            var assembly = Assembly.GetExecutingAssembly();
            // O nome do recurso é [Namespace].[NomeDoArquivo]
            string resourceName = $"steamToolsSeguro.{DllFileName}";
            
            using (Stream? resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                {
                    MessageBox.Show($"Erro interno: Recurso '{DllFileName}' não encontrado no executável. Verifique se o .csproj está configurado com <EmbeddedResource Include=\"hid.dll\" />.", "Erro de Instalação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    // Tenta copiar a DLL para o destino
                    using (FileStream fileStream = File.Create(destinationPath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                    // Sucesso (silencioso)
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show(
                        $"Falha de permissão ao instalar '{DllFileName}'.\n\nPor favor, feche e execute o programa como ADMINISTRADOR.", 
                        "Aviso Crítico de Permissão", 
                        MessageBoxButtons.OK, 
                        MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro desconhecido ao instalar '{DllFileName}': {ex.Message}", "Erro de Instalação", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // ... (Restante dos métodos: FecharPrograma_Click, ReiniciarSteam_Click, etc.)

        private void FecharPrograma_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReiniciarSteam_Click(object? sender, EventArgs e)
        {
            if (!File.Exists(SteamPath))
            {
                MessageBox.Show($"O executável do Steam não foi encontrado em:\n{SteamPath}\nVerifique se o Steam está instalado no diretório padrão.", "Erro de Caminho do Steam", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (IsProcessRunning(SteamProcessName))
                {
                    Process.Start(new ProcessStartInfo(SteamPath, "-shutdown") { UseShellExecute = true });
                    Thread.Sleep(3000); 
                    Process.Start(new ProcessStartInfo(SteamPath) { UseShellExecute = true });
                }
                else
                {
                    Process.Start(new ProcessStartInfo(SteamPath) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao tentar executar o Steam: {ex.Message}", "Erro de Processo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MoveWindow_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void Form1_DragEnter(object? sender, DragEventArgs e)
        {
            if (e.Data is not null && e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy; 
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void Form1_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data is null) return;
            
            string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];
            
            var successfulFiles = new List<string>();
            var failedFiles = new List<string>();

            if (files != null)
            {
                foreach (string sourcePath in files)
                {
                    if (File.Exists(sourcePath))
                    {
                        if (!ProcessFile(sourcePath, out string errorMessage))
                        {
                            failedFiles.Add(Path.GetFileName(sourcePath) + (string.IsNullOrEmpty(errorMessage) ? "" : $" ({errorMessage})"));
                        }
                        else
                        {
                            successfulFiles.Add(Path.GetFileName(sourcePath));
                        }
                    }
                }
            }
            
            // Geração do Feedback Consolidado
            var message = new StringBuilder();
            
            if (successfulFiles.Count > 0)
            {
                message.AppendLine($"SUCESSO: {successfulFiles.Count} arquivo(s) copiado(s) com êxito.");
            }

            if (failedFiles.Count > 0)
            {
                message.AppendLine($"FALHA: {failedFiles.Count} arquivo(s) não puderam ser copiados.");
                message.AppendLine("Verifique permissões (Executar como Admin) e mapeamentos.");
            }
            
            if (message.Length > 0)
            {
                MessageBox.Show(message.ToString(), "Status da Operação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private bool ProcessFile(string sourcePath, out string errorMessage)
        {
            errorMessage = string.Empty;
            string extension = Path.GetExtension(sourcePath);
            string fileName = Path.GetFileName(sourcePath);
            string destinationDirectory = string.Empty;

            if (MAPPING.TryGetValue(extension, out destinationDirectory))
            {
                try
                {
                    Directory.CreateDirectory(destinationDirectory);

                    string destinationPath = Path.Combine(destinationDirectory, fileName);
                    
                    File.Copy(sourcePath, destinationPath, overwrite: true); 
                    
                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
            else
            {
                errorMessage = "Extensão não mapeada.";
                return false;
            }
        }
    }
}