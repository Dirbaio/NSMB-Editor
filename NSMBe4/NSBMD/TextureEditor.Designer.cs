namespace NSMBe4.NSBMD
{
    partial class TextureEditor
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
            this.textureListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.paletteListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.paletteExpCount = new System.Windows.Forms.NumericUpDown();
            this.importAll = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.exportAll = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.paletteNum = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.paletteSize = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteExpCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteSize)).BeginInit();
            this.SuspendLayout();
            // 
            // textureListBox
            // 
            this.textureListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.textureListBox.FormattingEnabled = true;
            this.textureListBox.Location = new System.Drawing.Point(12, 25);
            this.textureListBox.Name = "textureListBox";
            this.textureListBox.Size = new System.Drawing.Size(120, 147);
            this.textureListBox.TabIndex = 0;
            this.textureListBox.SelectedIndexChanged += new System.EventHandler(this.textureListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Texture";
            // 
            // paletteListBox
            // 
            this.paletteListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.paletteListBox.FormattingEnabled = true;
            this.paletteListBox.Location = new System.Drawing.Point(12, 192);
            this.paletteListBox.Name = "paletteListBox";
            this.paletteListBox.Size = new System.Drawing.Size(120, 108);
            this.paletteListBox.TabIndex = 0;
            this.paletteListBox.SelectedIndexChanged += new System.EventHandler(this.paletteListBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Palette";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.paletteExpCount);
            this.panel1.Controls.Add(this.importAll);
            this.panel1.Controls.Add(this.importButton);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.exportAll);
            this.panel1.Controls.Add(this.exportButton);
            this.panel1.Location = new System.Drawing.Point(138, 329);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 56);
            this.panel1.TabIndex = 2;
            // 
            // paletteExpCount
            // 
            this.paletteExpCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.paletteExpCount.Location = new System.Drawing.Point(165, 20);
            this.paletteExpCount.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.paletteExpCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.paletteExpCount.Name = "paletteExpCount";
            this.paletteExpCount.Size = new System.Drawing.Size(120, 20);
            this.paletteExpCount.TabIndex = 1;
            this.paletteExpCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.paletteExpCount.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // importAll
            // 
            this.importAll.Location = new System.Drawing.Point(84, 30);
            this.importAll.Name = "importAll";
            this.importAll.Size = new System.Drawing.Size(75, 23);
            this.importAll.TabIndex = 0;
            this.importAll.Text = "button1";
            this.importAll.UseVisualStyleBackColor = true;
            this.importAll.Click += new System.EventHandler(this.importAll_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(3, 30);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 0;
            this.importButton.Text = "button1";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(162, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Palettes to export:";
            // 
            // exportAll
            // 
            this.exportAll.Location = new System.Drawing.Point(84, 3);
            this.exportAll.Name = "exportAll";
            this.exportAll.Size = new System.Drawing.Size(75, 23);
            this.exportAll.TabIndex = 0;
            this.exportAll.Text = "button1";
            this.exportAll.UseVisualStyleBackColor = true;
            this.exportAll.Click += new System.EventHandler(this.exportAll_Click);
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(3, 3);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 0;
            this.exportButton.Text = "button1";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.LightSlateGray;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(138, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(422, 314);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "PNG|*.png";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "PNG|*.png";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 349);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Palette number";
            // 
            // paletteNum
            // 
            this.paletteNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.paletteNum.Location = new System.Drawing.Point(12, 365);
            this.paletteNum.Name = "paletteNum";
            this.paletteNum.Size = new System.Drawing.Size(120, 20);
            this.paletteNum.TabIndex = 1;
            this.paletteNum.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 310);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Palette size";
            // 
            // paletteSize
            // 
            this.paletteSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.paletteSize.Location = new System.Drawing.Point(12, 326);
            this.paletteSize.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.paletteSize.Name = "paletteSize";
            this.paletteSize.Size = new System.Drawing.Size(120, 20);
            this.paletteSize.TabIndex = 1;
            this.paletteSize.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // TextureEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 395);
            this.Controls.Add(this.paletteSize);
            this.Controls.Add(this.paletteNum);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.paletteListBox);
            this.Controls.Add(this.textureListBox);
            this.Name = "TextureEditor";
            this.Text = "TextureEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TextureEditor_FormClosed);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.paletteExpCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.paletteSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox textureListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox paletteListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importAll;
        private System.Windows.Forms.Button exportAll;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown paletteNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown paletteSize;
        private System.Windows.Forms.NumericUpDown paletteExpCount;
        private System.Windows.Forms.Label label5;
    }
}