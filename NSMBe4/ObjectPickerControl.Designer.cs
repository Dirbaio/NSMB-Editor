namespace NSMBe4 {
    partial class ObjectPickerControl {
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
            this.DrawingArea = new System.Windows.Forms.PictureBox();
            this.vScrollBar = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).BeginInit();
            this.SuspendLayout();
            // 
            // DrawingArea
            // 
            this.DrawingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingArea.Location = new System.Drawing.Point(0, 0);
            this.DrawingArea.Name = "DrawingArea";
            this.DrawingArea.Size = new System.Drawing.Size(183, 335);
            this.DrawingArea.TabIndex = 0;
            this.DrawingArea.TabStop = false;
            this.DrawingArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseMove);
            this.DrawingArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DrawingArea_MouseDown);
            this.DrawingArea.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingArea_Paint);
            // 
            // vScrollBar
            // 
            this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar.LargeChange = 4;
            this.vScrollBar.Location = new System.Drawing.Point(183, 0);
            this.vScrollBar.Maximum = 256;
            this.vScrollBar.Name = "vScrollBar";
            this.vScrollBar.Size = new System.Drawing.Size(15, 335);
            this.vScrollBar.TabIndex = 1;
            this.vScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar_ValueChanged);
            // 
            // ObjectPickerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DrawingArea);
            this.Controls.Add(this.vScrollBar);
            this.Name = "ObjectPickerControl";
            this.Size = new System.Drawing.Size(198, 335);
            this.Resize += new System.EventHandler(this.ObjectPickerControl_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DrawingArea)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox DrawingArea;
        private System.Windows.Forms.VScrollBar vScrollBar;
    }
}
