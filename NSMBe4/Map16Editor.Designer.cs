namespace NSMBe4
{
    partial class Map16Editor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.map16Picker1 = new NSMBe4.Map16Picker();
            this.SuspendLayout();
            // 
            // map16Picker1
            // 
            this.map16Picker1.AutoScroll = true;
            this.map16Picker1.Dock = System.Windows.Forms.DockStyle.Left;
            this.map16Picker1.Location = new System.Drawing.Point(0, 0);
            this.map16Picker1.MinimumSize = new System.Drawing.Size(282, 187);
            this.map16Picker1.Name = "map16Picker1";
            this.map16Picker1.Size = new System.Drawing.Size(282, 307);
            this.map16Picker1.TabIndex = 0;
            // 
            // Map16Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.map16Picker1);
            this.Name = "Map16Editor";
            this.Size = new System.Drawing.Size(659, 307);
            this.ResumeLayout(false);

        }

        #endregion

        private Map16Picker map16Picker1;
    }
}
