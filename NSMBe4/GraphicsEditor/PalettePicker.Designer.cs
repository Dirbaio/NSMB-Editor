namespace NSMBe4 {
    partial class PalettePicker {
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
            this.drawingBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).BeginInit();
            this.SuspendLayout();
            // 
            // drawingBox
            // 
            this.drawingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.drawingBox.Location = new System.Drawing.Point(0, 0);
            this.drawingBox.Name = "drawingBox";
            this.drawingBox.Size = new System.Drawing.Size(192, 192);
            this.drawingBox.TabIndex = 0;
            this.drawingBox.TabStop = false;
            this.drawingBox.Click += new System.EventHandler(this.drawingBox_Click);
            this.drawingBox.Paint += new System.Windows.Forms.PaintEventHandler(this.drawingBox_Paint);
            this.drawingBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.drawingBox_MouseDown);
            this.drawingBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.drawingBox_MouseMove);
            this.drawingBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.drawingBox_MouseUp);
            // 
            // PalettePicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drawingBox);
            this.Name = "PalettePicker";
            this.Size = new System.Drawing.Size(192, 192);
            ((System.ComponentModel.ISupportInitialize)(this.drawingBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox drawingBox;
    }
}
