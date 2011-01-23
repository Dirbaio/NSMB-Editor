namespace NSMBe4 {
    partial class ColourPickerControl {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.renderer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.renderer)).BeginInit();
            this.SuspendLayout();
            // 
            // renderer
            // 
            this.renderer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.renderer.Location = new System.Drawing.Point(0, 0);
            this.renderer.Name = "renderer";
            this.renderer.Size = new System.Drawing.Size(256, 102);
            this.renderer.TabIndex = 0;
            this.renderer.TabStop = false;
            this.renderer.Click += new System.EventHandler(this.renderer_Click);
            this.renderer.Paint += new System.Windows.Forms.PaintEventHandler(this.renderer_Paint);
            this.renderer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.renderer_MouseDown);
            this.renderer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderer_MouseMove);
            // 
            // ColourPickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.renderer);
            this.Name = "ColourPickerControl";
            this.Size = new System.Drawing.Size(256, 102);
            ((System.ComponentModel.ISupportInitialize)(this.renderer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox renderer;
    }
}
