
namespace ComfyUI_Portable_Launcher
{
    partial class ComfyForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComfyForm));
            this.ComfyViewer = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.ComfyTerminal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ComfyViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // ComfyViewer
            // 
            this.ComfyViewer.AllowExternalDrop = true;
            this.ComfyViewer.CreationProperties = null;
            this.ComfyViewer.DefaultBackgroundColor = System.Drawing.Color.White;
            this.ComfyViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComfyViewer.Location = new System.Drawing.Point(0, 0);
            this.ComfyViewer.Name = "ComfyViewer";
            this.ComfyViewer.Size = new System.Drawing.Size(771, 420);
            this.ComfyViewer.TabIndex = 0;
            this.ComfyViewer.ZoomFactor = 1D;
            // 
            // ComfyTerminal
            // 
            this.ComfyTerminal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ComfyTerminal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ComfyTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ComfyTerminal.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ComfyTerminal.ForeColor = System.Drawing.Color.DarkKhaki;
            this.ComfyTerminal.Location = new System.Drawing.Point(0, 0);
            this.ComfyTerminal.Multiline = true;
            this.ComfyTerminal.Name = "ComfyTerminal";
            this.ComfyTerminal.Size = new System.Drawing.Size(771, 420);
            this.ComfyTerminal.TabIndex = 1;
            // 
            // ComfyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(771, 420);
            this.Controls.Add(this.ComfyTerminal);
            this.Controls.Add(this.ComfyViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ComfyForm";
            this.Text = "ComfyUI Portable Launcher";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ComfyForm_FormClosing);
            this.Load += new System.EventHandler(this.ComfyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ComfyViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 ComfyViewer;
        private System.Windows.Forms.TextBox ComfyTerminal;
    }
}

