namespace NSMBe4
{
    partial class GraphicsViewer
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
            this.viewport = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imageSizes = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.paletteNum = new System.Windows.Forms.NumericUpDown();
            this.use4bpp = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.viewport)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteNum)).BeginInit();
            this.SuspendLayout();
            // 
            // viewport
            // 
            this.viewport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.viewport.Location = new System.Drawing.Point(130, 0);
            this.viewport.Name = "viewport";
            this.viewport.Size = new System.Drawing.Size(520, 299);
            this.viewport.TabIndex = 0;
            this.viewport.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imageSizes);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.paletteNum);
            this.panel1.Controls.Add(this.use4bpp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(130, 299);
            this.panel1.TabIndex = 1;
            // 
            // imageSizes
            // 
            this.imageSizes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.imageSizes.FormattingEnabled = true;
            this.imageSizes.Location = new System.Drawing.Point(3, 25);
            this.imageSizes.Name = "imageSizes";
            this.imageSizes.Size = new System.Drawing.Size(121, 199);
            this.imageSizes.TabIndex = 3;
            this.imageSizes.SelectedIndexChanged += new System.EventHandler(this.imageSizes_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "<label2>";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 251);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "<label1>";
            // 
            // paletteNum
            // 
            this.paletteNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.paletteNum.Location = new System.Drawing.Point(3, 267);
            this.paletteNum.Name = "paletteNum";
            this.paletteNum.Size = new System.Drawing.Size(124, 20);
            this.paletteNum.TabIndex = 1;
            this.paletteNum.ValueChanged += new System.EventHandler(this.paletteNum_ValueChanged);
            // 
            // use4bpp
            // 
            this.use4bpp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.use4bpp.AutoSize = true;
            this.use4bpp.Location = new System.Drawing.Point(6, 231);
            this.use4bpp.Name = "use4bpp";
            this.use4bpp.Size = new System.Drawing.Size(79, 17);
            this.use4bpp.TabIndex = 0;
            this.use4bpp.Text = "<use4bpp>";
            this.use4bpp.UseVisualStyleBackColor = true;
            this.use4bpp.CheckedChanged += new System.EventHandler(this.use4bpp_CheckedChanged);
            // 
            // GraphicsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 299);
            this.Controls.Add(this.viewport);
            this.Controls.Add(this.panel1);
            this.Name = "GraphicsViewer";
            this.Text = "<_TITLE>";
            ((System.ComponentModel.ISupportInitialize)(this.viewport)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteNum)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox viewport;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox use4bpp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown paletteNum;
        private System.Windows.Forms.ListBox imageSizes;
        private System.Windows.Forms.Label label2;
    }
}