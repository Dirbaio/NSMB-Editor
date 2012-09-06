namespace NSMBe4
{
    partial class ImageManager
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
            this.imageListBox = new System.Windows.Forms.ListBox();
            this.paletteListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.image2dOptions = new System.Windows.Forms.Panel();
            this.fourBppCheckBox = new System.Windows.Forms.CheckBox();
            this.tileOffsetNumber = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.tileWidthNumber = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.autopaletteCheckBox = new System.Windows.Forms.CheckBox();
            this.importAllBtn = new System.Windows.Forms.Button();
            this.saveAllBtn = new System.Windows.Forms.Button();
            this.exportAllBtn = new System.Windows.Forms.Button();
            this.importThisWithPalBtn = new System.Windows.Forms.Button();
            this.importThisBtn = new System.Windows.Forms.Button();
            this.exportThisBtn = new System.Windows.Forms.Button();
            this.graphicsEditor1 = new NSMBe4.GraphicsEditor();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.image2dOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileOffsetNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileWidthNumber)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageListBox
            // 
            this.imageListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageListBox.FormattingEnabled = true;
            this.imageListBox.Location = new System.Drawing.Point(0, 13);
            this.imageListBox.Name = "imageListBox";
            this.imageListBox.Size = new System.Drawing.Size(243, 98);
            this.imageListBox.TabIndex = 1;
            this.imageListBox.SelectedIndexChanged += new System.EventHandler(this.imageListBox_SelectedIndexChanged);
            this.imageListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.imageListBox_MouseDown);
            // 
            // paletteListBox
            // 
            this.paletteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.paletteListBox.FormattingEnabled = true;
            this.paletteListBox.Location = new System.Drawing.Point(0, 13);
            this.paletteListBox.Name = "paletteListBox";
            this.paletteListBox.Size = new System.Drawing.Size(243, 99);
            this.paletteListBox.TabIndex = 2;
            this.paletteListBox.SelectedIndexChanged += new System.EventHandler(this.paletteListBox_SelectedIndexChanged);
            this.paletteListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.paletteListBox_MouseDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Controls.Add(this.image2dOptions);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 508);
            this.panel1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(249, 235);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.imageListBox);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 3);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(243, 111);
            this.panel4.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Bitmaps";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.paletteListBox);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 120);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(243, 112);
            this.panel3.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Palettes";
            // 
            // image2dOptions
            // 
            this.image2dOptions.Controls.Add(this.fourBppCheckBox);
            this.image2dOptions.Controls.Add(this.tileOffsetNumber);
            this.image2dOptions.Controls.Add(this.label4);
            this.image2dOptions.Controls.Add(this.tileWidthNumber);
            this.image2dOptions.Controls.Add(this.label3);
            this.image2dOptions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.image2dOptions.Location = new System.Drawing.Point(0, 235);
            this.image2dOptions.Name = "image2dOptions";
            this.image2dOptions.Size = new System.Drawing.Size(249, 87);
            this.image2dOptions.TabIndex = 3;
            // 
            // fourBppCheckBox
            // 
            this.fourBppCheckBox.AutoSize = true;
            this.fourBppCheckBox.Location = new System.Drawing.Point(44, 57);
            this.fourBppCheckBox.Name = "fourBppCheckBox";
            this.fourBppCheckBox.Size = new System.Drawing.Size(119, 17);
            this.fourBppCheckBox.TabIndex = 2;
            this.fourBppCheckBox.Text = "View in 4bpp format";
            this.fourBppCheckBox.UseVisualStyleBackColor = true;
            this.fourBppCheckBox.CheckedChanged += new System.EventHandler(this.tileWidthNumber_ValueChanged);
            // 
            // tileOffsetNumber
            // 
            this.tileOffsetNumber.AutoSize = true;
            this.tileOffsetNumber.Enabled = false;
            this.tileOffsetNumber.Location = new System.Drawing.Point(44, 31);
            this.tileOffsetNumber.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.tileOffsetNumber.Name = "tileOffsetNumber";
            this.tileOffsetNumber.Size = new System.Drawing.Size(199, 20);
            this.tileOffsetNumber.TabIndex = 1;
            this.tileOffsetNumber.ValueChanged += new System.EventHandler(this.tileWidthNumber_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tile width";
            // 
            // tileWidthNumber
            // 
            this.tileWidthNumber.Enabled = false;
            this.tileWidthNumber.Location = new System.Drawing.Point(44, 5);
            this.tileWidthNumber.Maximum = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.tileWidthNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileWidthNumber.Name = "tileWidthNumber";
            this.tileWidthNumber.Size = new System.Drawing.Size(199, 20);
            this.tileWidthNumber.TabIndex = 1;
            this.tileWidthNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tileWidthNumber.ValueChanged += new System.EventHandler(this.tileWidthNumber_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Tile offset";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.autopaletteCheckBox);
            this.panel2.Controls.Add(this.importAllBtn);
            this.panel2.Controls.Add(this.saveAllBtn);
            this.panel2.Controls.Add(this.exportAllBtn);
            this.panel2.Controls.Add(this.importThisWithPalBtn);
            this.panel2.Controls.Add(this.importThisBtn);
            this.panel2.Controls.Add(this.exportThisBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 322);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 186);
            this.panel2.TabIndex = 5;
            // 
            // autopaletteCheckBox
            // 
            this.autopaletteCheckBox.AutoSize = true;
            this.autopaletteCheckBox.Checked = true;
            this.autopaletteCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.autopaletteCheckBox.Location = new System.Drawing.Point(6, 159);
            this.autopaletteCheckBox.Name = "autopaletteCheckBox";
            this.autopaletteCheckBox.Size = new System.Drawing.Size(114, 17);
            this.autopaletteCheckBox.TabIndex = 3;
            this.autopaletteCheckBox.Text = "Auto palette select";
            this.autopaletteCheckBox.UseVisualStyleBackColor = true;
            // 
            // importAllBtn
            // 
            this.importAllBtn.Location = new System.Drawing.Point(3, 130);
            this.importAllBtn.Name = "importAllBtn";
            this.importAllBtn.Size = new System.Drawing.Size(237, 23);
            this.importAllBtn.TabIndex = 0;
            this.importAllBtn.Text = "Import all bitmaps with all palettes [BROKEN]";
            this.importAllBtn.UseVisualStyleBackColor = true;
            this.importAllBtn.Click += new System.EventHandler(this.importAllBtn_Click);
            // 
            // saveAllBtn
            // 
            this.saveAllBtn.Location = new System.Drawing.Point(168, 3);
            this.saveAllBtn.Name = "saveAllBtn";
            this.saveAllBtn.Size = new System.Drawing.Size(75, 23);
            this.saveAllBtn.TabIndex = 0;
            this.saveAllBtn.Text = "Save all";
            this.saveAllBtn.UseVisualStyleBackColor = true;
            this.saveAllBtn.Click += new System.EventHandler(this.saveAllBtn_Click);
            // 
            // exportAllBtn
            // 
            this.exportAllBtn.Location = new System.Drawing.Point(3, 101);
            this.exportAllBtn.Name = "exportAllBtn";
            this.exportAllBtn.Size = new System.Drawing.Size(237, 23);
            this.exportAllBtn.TabIndex = 0;
            this.exportAllBtn.Text = "Export all bitmaps with all palettes";
            this.exportAllBtn.UseVisualStyleBackColor = true;
            this.exportAllBtn.Click += new System.EventHandler(this.exportAllBtn_Click);
            // 
            // importThisWithPalBtn
            // 
            this.importThisWithPalBtn.Location = new System.Drawing.Point(3, 61);
            this.importThisWithPalBtn.Name = "importThisWithPalBtn";
            this.importThisWithPalBtn.Size = new System.Drawing.Size(240, 23);
            this.importThisWithPalBtn.TabIndex = 0;
            this.importThisWithPalBtn.Text = "Import this bitmap and recreate palette";
            this.importThisWithPalBtn.UseVisualStyleBackColor = true;
            this.importThisWithPalBtn.Click += new System.EventHandler(this.importThisWithPalBtn_Click);
            // 
            // importThisBtn
            // 
            this.importThisBtn.Location = new System.Drawing.Point(3, 32);
            this.importThisBtn.Name = "importThisBtn";
            this.importThisBtn.Size = new System.Drawing.Size(240, 23);
            this.importThisBtn.TabIndex = 0;
            this.importThisBtn.Text = "Import this bitmap with existing palette";
            this.importThisBtn.UseVisualStyleBackColor = true;
            this.importThisBtn.Click += new System.EventHandler(this.importThisBtn_Click);
            // 
            // exportThisBtn
            // 
            this.exportThisBtn.Location = new System.Drawing.Point(3, 3);
            this.exportThisBtn.Name = "exportThisBtn";
            this.exportThisBtn.Size = new System.Drawing.Size(116, 23);
            this.exportThisBtn.TabIndex = 0;
            this.exportThisBtn.Text = "Export this bitmap";
            this.exportThisBtn.UseVisualStyleBackColor = true;
            this.exportThisBtn.Click += new System.EventHandler(this.exportThisBtn_Click);
            // 
            // graphicsEditor1
            // 
            this.graphicsEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicsEditor1.Location = new System.Drawing.Point(249, 0);
            this.graphicsEditor1.Name = "graphicsEditor1";
            this.graphicsEditor1.Size = new System.Drawing.Size(568, 508);
            this.graphicsEditor1.TabIndex = 4;
            this.graphicsEditor1.SomethingSaved += new NSMBe4.GraphicsEditor.SomethingSavedD(this.graphicsEditor1_SomethingSaved);
            // 
            // ImageManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.graphicsEditor1);
            this.Controls.Add(this.panel1);
            this.Name = "ImageManager";
            this.Size = new System.Drawing.Size(817, 508);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.image2dOptions.ResumeLayout(false);
            this.image2dOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileOffsetNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileWidthNumber)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox imageListBox;
        private System.Windows.Forms.ListBox paletteListBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel image2dOptions;
        private System.Windows.Forms.NumericUpDown tileWidthNumber;
        private System.Windows.Forms.Label label3;
        private GraphicsEditor graphicsEditor1;
        private System.Windows.Forms.CheckBox fourBppCheckBox;
        private System.Windows.Forms.NumericUpDown tileOffsetNumber;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button importAllBtn;
        private System.Windows.Forms.Button exportAllBtn;
        private System.Windows.Forms.Button importThisBtn;
        private System.Windows.Forms.Button exportThisBtn;
        private System.Windows.Forms.Button saveAllBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button importThisWithPalBtn;
        private System.Windows.Forms.CheckBox autopaletteCheckBox;
    }
}
