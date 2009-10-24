namespace NSMBe4.Filesystem
{
    partial class FilesystemBrowserDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.filesystemBrowser1 = new NSMBe4.Filesystem.FilesystemBrowser();
            this.SuspendLayout();
            // 
            // filesystemBrowser1
            // 
            this.filesystemBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesystemBrowser1.Location = new System.Drawing.Point(0, 0);
            this.filesystemBrowser1.Name = "filesystemBrowser1";
            this.filesystemBrowser1.Size = new System.Drawing.Size(471, 292);
            this.filesystemBrowser1.TabIndex = 0;
            // 
            // FilesystemBrowserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 292);
            this.Controls.Add(this.filesystemBrowser1);
            this.Name = "FilesystemBrowserDialog";
            this.Text = "_TITLE";
            this.ResumeLayout(false);

        }

        #endregion

        private FilesystemBrowser filesystemBrowser1;
    }
}