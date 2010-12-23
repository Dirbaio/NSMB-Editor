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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tileBehavior = new NSMBe4.ByteArrayEditor();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.yFlip = new System.Windows.Forms.CheckBox();
            this.tileNumber = new System.Windows.Forms.NumericUpDown();
            this.controlByte = new System.Windows.Forms.NumericUpDown();
            this.tileByte = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.secPal = new System.Windows.Forms.CheckBox();
            this.xFlip = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.editQ1 = new System.Windows.Forms.Button();
            this.editQ3 = new System.Windows.Forms.Button();
            this.editQ2 = new System.Windows.Forms.Button();
            this.editQ4 = new System.Windows.Forms.Button();
            this.tilePicker1 = new NSMBe4.TilePicker();
            this.map16Picker1 = new NSMBe4.Map16Picker();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileByte)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(282, 224);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(423, 248);
            this.panel1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(6, 172);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(414, 73);
            this.groupBox4.TabIndex = 6;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "<groupBox4>";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "<button1>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.tileBehavior);
            this.groupBox3.Location = new System.Drawing.Point(6, 114);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(414, 52);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "<groupBox3>";
            // 
            // tileBehavior
            // 
            this.tileBehavior.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tileBehavior.Location = new System.Drawing.Point(6, 19);
            this.tileBehavior.Name = "tileBehavior";
            this.tileBehavior.Size = new System.Drawing.Size(402, 27);
            this.tileBehavior.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel1);
            this.groupBox2.Location = new System.Drawing.Point(101, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(319, 102);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "<groupBox2>";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.yFlip, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.tileNumber, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.controlByte, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tileByte, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.secPal, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.xFlip, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 14);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(307, 77);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // yFlip
            // 
            this.yFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.yFlip.AutoSize = true;
            this.yFlip.Location = new System.Drawing.Point(158, 56);
            this.yFlip.Name = "yFlip";
            this.yFlip.Size = new System.Drawing.Size(146, 17);
            this.yFlip.TabIndex = 3;
            this.yFlip.Text = "<yFlip>";
            this.yFlip.UseVisualStyleBackColor = true;
            this.yFlip.CheckedChanged += new System.EventHandler(this.yFlip_CheckedChanged);
            // 
            // tileNumber
            // 
            this.tileNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tileNumber.Location = new System.Drawing.Point(56, 3);
            this.tileNumber.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.tileNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.tileNumber.Name = "tileNumber";
            this.tileNumber.Size = new System.Drawing.Size(96, 20);
            this.tileNumber.TabIndex = 0;
            this.tileNumber.ValueChanged += new System.EventHandler(this.tileNumber_ValueChanged);
            // 
            // controlByte
            // 
            this.controlByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.controlByte.Hexadecimal = true;
            this.controlByte.Location = new System.Drawing.Point(56, 29);
            this.controlByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.controlByte.Name = "controlByte";
            this.controlByte.Size = new System.Drawing.Size(96, 20);
            this.controlByte.TabIndex = 0;
            this.controlByte.ValueChanged += new System.EventHandler(this.controlByte_ValueChanged);
            // 
            // tileByte
            // 
            this.tileByte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tileByte.Location = new System.Drawing.Point(56, 55);
            this.tileByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.tileByte.Name = "tileByte";
            this.tileByte.Size = new System.Drawing.Size(96, 20);
            this.tileByte.TabIndex = 0;
            this.tileByte.ValueChanged += new System.EventHandler(this.tileByte_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "<label1>";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "<label2>";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "<label3>";
            // 
            // secPal
            // 
            this.secPal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.secPal.AutoSize = true;
            this.secPal.Location = new System.Drawing.Point(158, 4);
            this.secPal.Name = "secPal";
            this.secPal.Size = new System.Drawing.Size(146, 17);
            this.secPal.TabIndex = 2;
            this.secPal.Text = "<secPal>";
            this.secPal.UseVisualStyleBackColor = true;
            this.secPal.CheckedChanged += new System.EventHandler(this.secPal_CheckedChanged);
            // 
            // xFlip
            // 
            this.xFlip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.xFlip.AutoSize = true;
            this.xFlip.Location = new System.Drawing.Point(158, 30);
            this.xFlip.Name = "xFlip";
            this.xFlip.Size = new System.Drawing.Size(146, 17);
            this.xFlip.TabIndex = 2;
            this.xFlip.Text = "<xFlip>";
            this.xFlip.UseVisualStyleBackColor = true;
            this.xFlip.CheckedChanged += new System.EventHandler(this.xFlip_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.editQ1);
            this.groupBox1.Controls.Add(this.editQ3);
            this.groupBox1.Controls.Add(this.editQ2);
            this.groupBox1.Controls.Add(this.editQ4);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(89, 102);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // editQ1
            // 
            this.editQ1.Location = new System.Drawing.Point(6, 19);
            this.editQ1.Name = "editQ1";
            this.editQ1.Size = new System.Drawing.Size(34, 34);
            this.editQ1.TabIndex = 1;
            this.editQ1.UseVisualStyleBackColor = true;
            this.editQ1.Click += new System.EventHandler(this.editQ1_Click);
            // 
            // editQ3
            // 
            this.editQ3.Location = new System.Drawing.Point(46, 19);
            this.editQ3.Name = "editQ3";
            this.editQ3.Size = new System.Drawing.Size(34, 34);
            this.editQ3.TabIndex = 1;
            this.editQ3.UseVisualStyleBackColor = true;
            this.editQ3.Click += new System.EventHandler(this.editQ3_Click);
            // 
            // editQ2
            // 
            this.editQ2.Location = new System.Drawing.Point(6, 59);
            this.editQ2.Name = "editQ2";
            this.editQ2.Size = new System.Drawing.Size(34, 34);
            this.editQ2.TabIndex = 1;
            this.editQ2.UseVisualStyleBackColor = true;
            this.editQ2.Click += new System.EventHandler(this.editQ2_Click);
            // 
            // editQ4
            // 
            this.editQ4.Location = new System.Drawing.Point(46, 59);
            this.editQ4.Name = "editQ4";
            this.editQ4.Size = new System.Drawing.Size(34, 34);
            this.editQ4.TabIndex = 1;
            this.editQ4.UseVisualStyleBackColor = true;
            this.editQ4.Click += new System.EventHandler(this.editQ4_Click);
            // 
            // tilePicker1
            // 
            this.tilePicker1.AutoSize = true;
            this.tilePicker1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tilePicker1.Location = new System.Drawing.Point(282, 0);
            this.tilePicker1.MinimumSize = new System.Drawing.Size(256, 224);
            this.tilePicker1.Name = "tilePicker1";
            this.tilePicker1.Size = new System.Drawing.Size(423, 224);
            this.tilePicker1.TabIndex = 1;
            this.tilePicker1.TileSelected += new NSMBe4.TilePicker.TileSelectedd(this.tilePicker1_TileSelected);
            this.tilePicker1.QuarterChanged += new NSMBe4.TilePicker.QuarterChangedd(this.tilePicker1_QuarterChanged);
            // 
            // map16Picker1
            // 
            this.map16Picker1.AutoScroll = true;
            this.map16Picker1.Dock = System.Windows.Forms.DockStyle.Left;
            this.map16Picker1.Location = new System.Drawing.Point(0, 0);
            this.map16Picker1.MinimumSize = new System.Drawing.Size(282, 187);
            this.map16Picker1.Name = "map16Picker1";
            this.map16Picker1.Size = new System.Drawing.Size(282, 472);
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
            this.Size = new System.Drawing.Size(705, 472);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tileNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileByte)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Button editQ1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private ByteArrayEditor tileBehavior;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox secPal;
        private System.Windows.Forms.CheckBox xFlip;
        private System.Windows.Forms.CheckBox yFlip;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button1;
    }
}
