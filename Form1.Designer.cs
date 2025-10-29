namespace steamToolsSeguro
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        
        // Declaração dos componentes do Menu de Contexto
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1; 
        private System.Windows.Forms.ToolStripMenuItem fecharToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reiniciarSteamToolStripMenuItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components); 
            this.fecharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem(); 
            this.reiniciarSteamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem(); 
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reiniciarSteamToolStripMenuItem, 
            this.fecharToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 48); 
            // 
            // fecharToolStripMenuItem
            // 
            this.fecharToolStripMenuItem.Name = "fecharToolStripMenuItem";
            this.fecharToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.fecharToolStripMenuItem.Text = "Fechar Programa"; 
            this.fecharToolStripMenuItem.Click += new System.EventHandler(this.FecharPrograma_Click); 
            // 
            // reiniciarSteamToolStripMenuItem
            // 
            this.reiniciarSteamToolStripMenuItem.Name = "reiniciarSteamToolStripMenuItem";
            this.reiniciarSteamToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reiniciarSteamToolStripMenuItem.Text = "Reiniciar Steam"; 
            this.reiniciarSteamToolStripMenuItem.Click += new System.EventHandler(this.ReiniciarSteam_Click); 
            // 
            // Form1
            // 
            this.AllowDrop = true; 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black; 
            this.ClientSize = new System.Drawing.Size(64, 64); 
            this.ContextMenuStrip = this.contextMenuStrip1; 
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None; 
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false; 
            this.ShowInTaskbar = false; 
            this.Text = "File Sorter";
            this.TopMost = true; 
            this.TransparencyKey = System.Drawing.Color.Black; 
            
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch; 
            
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop); 
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter); 
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}