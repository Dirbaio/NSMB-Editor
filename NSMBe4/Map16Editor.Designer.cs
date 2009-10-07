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
            this.panel1 = new System.Windows.Forms.Panel();
            this.editQ2 = new System.Windows.Forms.Button();
            this.editQ4 = new System.Windows.Forms.Button();
            this.editQ3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.editQ1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tileNumber = new System.Windows.Forms.NumericUpDown();
            this.controlByte = new System.Windows.Forms.NumericUpDown();
            this.tileByte = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tilePicker1 = new NSMBe4.TilePicker();
            this.map16Picker1 = new NSMBe4.Map16Picker();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileByte)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.editQ2);
            this.panel1.Controls.Add(this.editQ4);
            this.panel1.Controls.Add(this.editQ3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.editQ1);
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(282, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(295, 106);
            this.panel1.TabIndex = 2;
            // 
            // editQ2
            // 
            this.editQ2.Location = new System.Drawing.Point(6, 61);
            this.editQ2.Name = "editQ2";
            this.editQ2.Size = new System.Drawing.Size(34, 34);
            this.editQ2.TabIndex = 1;
            this.editQ2.UseVisualStyleBackColor = true;
            this.editQ2.Click += new System.EventHandler(this.editQ2_Click);
            // 
            // editQ4
            // 
            this.editQ4.Location = new System.Drawing.Point(46, 61);
            this.editQ4.Name = "editQ4";
            this.editQ4.Size = new System.Drawing.Size(34, 34);
            this.editQ4.TabIndex = 1;
            this.editQ4.UseVisualStyleBackColor = true;
            this.editQ4.Click += new System.EventHandler(this.editQ4_Click);
            // 
            // editQ3
            // 
            this.editQ3.Location = new System.Drawing.Point(46, 21);
            this.editQ3.Name = "editQ3";
            this.editQ3.Size = new System.Drawing.Size(34, 34);
            this.editQ3.TabIndex = 1;
            this.editQ3.UseVisualStyleBackColor = true;
            this.editQ3.Click += new System.EventHandler(this.editQ3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Edit tile:";
            // 
            // editQ1
            // 
            this.editQ1.Location = new System.Drawing.Point(6, 21);
            this.editQ1.Name = "editQ1";
            this.editQ1.Size = new System.Drawing.Size(34, 34);
            this.editQ1.TabIndex = 1;
            this.editQ1.UseVisualStyleBackColor = true;
            this.editQ1.Click += new System.EventHandler(this.editQ1_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tileNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.controlByte, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tileByte, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(95, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(200, 106);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tileNumber
            // 
            this.tileNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tileNumber.Location = new System.Drawing.Point(103, 3);
            this.tileNumber.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.tileNumber.Name = "tileNumber";
            this.tileNumber.Size = new System.Drawing.Size(94, 20);
            this.tileNumber.TabIndex = 0;
            this.tileNumber.ValueChanged += new System.EventHandler(this.tileNumber_ValueChanged);
            // 
            // controlByte
            // 
            this.controlByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.controlByte.Hexadecimal = true;
            this.controlByte.Location = new System.Drawing.Point(103, 29);
            this.controlByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.controlByte.Name = "controlByte";
            this.controlByte.Size = new System.Drawing.Size(94, 20);
            this.controlByte.TabIndex = 0;
            this.controlByte.ValueChanged += new System.EventHandler(this.controlByte_ValueChanged);
            // 
            // tileByte
            // 
            this.tileByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tileByte.Location = new System.Drawing.Point(103, 69);
            this.tileByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.tileByte.Name = "tileByte";
            this.tileByte.Size = new System.Drawing.Size(94, 20);
            this.tileByte.TabIndex = 0;
            this.tileByte.ValueChanged += new System.EventHandler(this.tileByte_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tile number";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Control byte";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tile byte";
            // 
            // tilePicker1
            // 
            this.tilePicker1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tilePicker1.Location = new System.Drawing.Point(282, 0);
            this.tilePicker1.MinimumSize = new System.Drawing.Size(256, 224);
            this.tilePicker1.Name = "tilePicker1";
            this.tilePicker1.Size = new System.Drawing.Size(295, 224);
            this.tilePicker1.TabIndex = 1;
            this.tilePicker1.TileSelected += new NSMBe4.TilePicker.TileSelectedd(this.tilePicker1_TileSelected);
            // 
            // map16Picker1
            // 
            this.map16Picker1.AutoScroll = true;
            this.map16Picker1.Dock = System.Windows.Forms.DockStyle.Left;
            this.map16Picker1.Location = new System.Drawing.Point(0, 0);
            this.map16Picker1.MinimumSize = new System.Drawing.Size(282, 187);
            this.map16Picker1.Name = "map16Picker1";
            this.map16Picker1.Size = new System.Drawing.Size(282, 330);
            this.map16Picker1.TabIndex = 0;
            this.map16Picker1.TileSelected += new NSMBe4.Map16Picker.TileSelectedd(this.map16Picker1_TileSelected);
            // 
            // Map16Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tilePicker1);
            this.Controls.Add(this.map16Picker1);
            this.Name = "Map16Editor";
            this.Size = new System.Drawing.Size(577, 330);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileByte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Map16Picker map16Picker1;
        private TilePicker tilePicker1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown tileNumber;
        private System.Windows.Forms.NumericUpDown controlByte;
        private System.Windows.Forms.NumericUpDown tileByte;
        private System.Windows.Forms.Button editQ2;
        private System.Windows.Forms.Button editQ4;
        private System.Windows.Forms.Button editQ3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button editQ1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
