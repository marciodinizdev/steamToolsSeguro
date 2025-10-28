using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Reflection;

namespace steamToolsSeguro
{
    public partial class Form1 : Form
    {
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
        // 2. Lógica para Mover a Janela Flutuante (Sem Borda)
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
            
            // 🚨 CORREÇÃO CRÍTICA: Carrega o ícone como Recurso Embutido (Embedded Resource)
            try
            {
                // O nome do recurso é: [Namespace].[NomeDoArquivo]
                var assembly = Assembly.GetExecutingAssembly();
                var iconStream = assembly.GetManifestResourceStream("steamToolsSeguro.AppIcon.ico");
                
                if (iconStream != null)
                {
                    this.Icon = new Icon(iconStream);
                    iconStream.Seek(0, SeekOrigin.Begin); // Reposiciona o stream para o Bitmap
                    this.BackgroundImage = new Bitmap(iconStream);
                }
                else
                {
                    // Mensagem de erro apenas se o nome interno estiver errado
                    // Removemos a mensagem de erro que procurava o arquivo no Desktop.
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de processamento do ícone: {ex.Message}", "Aviso de Ícone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            // Eventos para mover a janela e carregar
            this.MouseDown += MoveWindow_MouseDown;
            this.Load += Form1_Load;
        }

        private void Form1_Load(object? sender, EventArgs e)
        {
            this.ClientSize = new Size(64, 64);
            this.Size = new Size(64, 64);
        }

        private void FecharPrograma_Click(object? sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MoveWindow_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        // ====================================================================
        // 3. Evento: Mouse entra na área (DragEnter)
        // ====================================================================
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

        // ====================================================================
        // 4. Evento: Arquivo é solto na área (DragDrop)
        // ====================================================================
        private void Form1_DragDrop(object? sender, DragEventArgs e)
        {
            if (e.Data is null) return;
            
            string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];

            if (files != null)
            {
                foreach (string sourcePath in files)
                {
                    if (File.Exists(sourcePath))
                    {
                        ProcessFile(sourcePath);
                    }
                }
            }
        }

        // ====================================================================
        // 5. Lógica Principal: Processar e Mover o Arquivo (Feedback via MessageBox)
        // ====================================================================
        private void ProcessFile(string sourcePath)
        {
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

                    MessageBox.Show($"Arquivo '{fileName}' copiado com sucesso para:\n{destinationDirectory}", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"ERRO ao copiar '{fileName}':\n{ex.Message}\n\nSe o destino for pasta do sistema (Steam), TENTE EXECUTAR O PROGRAMA COMO ADMINISTRADOR.", "Erro de Permissão/Caminho", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show($"Extensão '{extension}' não está mapeada para nenhum destino.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}