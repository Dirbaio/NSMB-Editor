namespace NSMBe4
{
    partial class ImageManagerWindow
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
            this.m = new NSMBe4.ImageManager();
            this.SuspendLayout();
            // 
            // m
            // 
            this.m.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m.Location = new System.Drawing.Point(0, 0);
            this.m.Name = "m";
            this.m.Size = new System.Drawing.Size(292, 268);
            this.m.TabIndex = 0;
            // 
            // ImageManagerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 268);
            this.Controls.Add(this.m);
            this.Name = "ImageManagerWindow";
            this.Text = "x";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageManagerWindow_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        public ImageManager m;

    }
}