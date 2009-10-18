namespace NSMBe4
{
    partial class TilesetObjectEditor
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
            this.editZone = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.objWidth = new System.Windows.Forms.NumericUpDown();
            this.objHeight = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.map16Tile = new System.Windows.Forms.NumericUpDown();
            this.controlByte = new System.Windows.Forms.NumericUpDown();
            this.previewBox = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.map16Picker1 = new NSMBe4.Map16Picker();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.newLineButton = new System.Windows.Forms.Button();
            this.emptyTileButton = new System.Windows.Forms.Button();
            this.slopeControlButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.editZone)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeight)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.map16Tile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // editZone
            // 
            this.editZone.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editZone.Location = new System.Drawing.Point(0, 13);
            this.editZone.Name = "editZone";
            this.editZone.Size = new System.Drawing.Size(235, 249);
            this.editZone.TabIndex = 0;
            this.editZone.TabStop = false;
            this.editZone.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editZone_MouseDown);
            this.editZone.Paint += new System.Windows.Forms.PaintEventHandler(this.editZone_Paint);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.editZone);
            this.splitContainer1.Panel1.Controls.Add(this.label5);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.previewBox);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Size = new System.Drawing.Size(392, 389);
            this.splitContainer1.SplitterDistance = 262;
            this.splitContainer1.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(235, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "<label5>";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(235, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(157, 262);
            this.panel2.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.objWidth);
            this.groupBox2.Controls.Add(this.objHeight);
            this.groupBox2.Location = new System.Drawing.Point(3, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(148, 99);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "<groupBox2>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "<label7>";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "<label6>";
            // 
            // objWidth
            // 
            this.objWidth.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objWidth.Location = new System.Drawing.Point(9, 32);
            this.objWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.objWidth.Name = "objWidth";
            this.objWidth.Size = new System.Drawing.Size(129, 20);
            this.objWidth.TabIndex = 2;
            this.objWidth.ValueChanged += new System.EventHandler(this.objWidth_ValueChanged);
            // 
            // objHeight
            // 
            this.objHeight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objHeight.Location = new System.Drawing.Point(9, 73);
            this.objHeight.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.objHeight.Name = "objHeight";
            this.objHeight.Size = new System.Drawing.Size(129, 20);
            this.objHeight.TabIndex = 2;
            this.objHeight.ValueChanged += new System.EventHandler(this.objHeight_ValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.map16Tile);
            this.groupBox1.Controls.Add(this.controlByte);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(148, 101);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "<label1>";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "<label2>";
            // 
            // map16Tile
            // 
            this.map16Tile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.map16Tile.Location = new System.Drawing.Point(9, 32);
            this.map16Tile.Maximum = new decimal(new int[] {
            768,
            0,
            0,
            0});
            this.map16Tile.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.map16Tile.Name = "map16Tile";
            this.map16Tile.Size = new System.Drawing.Size(129, 20);
            this.map16Tile.TabIndex = 1;
            this.map16Tile.ValueChanged += new System.EventHandler(this.map16Tile_ValueChanged);
            // 
            // controlByte
            // 
            this.controlByte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.controlByte.Hexadecimal = true;
            this.controlByte.Location = new System.Drawing.Point(9, 71);
            this.controlByte.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.controlByte.Name = "controlByte";
            this.controlByte.Size = new System.Drawing.Size(129, 20);
            this.controlByte.TabIndex = 1;
            this.controlByte.ValueChanged += new System.EventHandler(this.controlByte_ValueChanged);
            // 
            // previewBox
            // 
            this.previewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewBox.Location = new System.Drawing.Point(0, 13);
            this.previewBox.Name = "previewBox";
            this.previewBox.Size = new System.Drawing.Size(392, 110);
            this.previewBox.TabIndex = 0;
            this.previewBox.TabStop = false;
            this.previewBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.previewBox_MouseMove);
            this.previewBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.previewBox_MouseDown);
            this.previewBox.Paint += new System.Windows.Forms.PaintEventHandler(this.previewBox_Paint);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(392, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "<label3>";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.map16Picker1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(392, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(282, 389);
            this.panel1.TabIndex = 2;
            // 
            // map16Picker1
            // 
            this.map16Picker1.AutoScroll = true;
            this.map16Picker1.AutoSize = true;
            this.map16Picker1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map16Picker1.Location = new System.Drawing.Point(0, 42);
            this.map16Picker1.MinimumSize = new System.Drawing.Size(282, 187);
            this.map16Picker1.Name = "map16Picker1";
            this.map16Picker1.Size = new System.Drawing.Size(282, 347);
            this.map16Picker1.TabIndex = 3;
            this.map16Picker1.TileSelected += new NSMBe4.Map16Picker.TileSelectedd(this.map16Picker1_TileSelected);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.Location = new System.Drawing.Point(0, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(282, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "<label4>";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.newLineButton);
            this.panel3.Controls.Add(this.emptyTileButton);
            this.panel3.Controls.Add(this.slopeControlButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(282, 29);
            this.panel3.TabIndex = 5;
            // 
            // newLineButton
            // 
            this.newLineButton.Location = new System.Drawing.Point(177, 3);
            this.newLineButton.Name = "newLineButton";
            this.newLineButton.Size = new System.Drawing.Size(72, 23);
            this.newLineButton.TabIndex = 0;
            this.newLineButton.Text = "<newLineButton>";
            this.newLineButton.UseVisualStyleBackColor = true;
            this.newLineButton.Click += new System.EventHandler(this.newLineButton_Click);
            // 
            // emptyTileButton
            // 
            this.emptyTileButton.Location = new System.Drawing.Point(99, 3);
            this.emptyTileButton.Name = "emptyTileButton";
            this.emptyTileButton.Size = new System.Drawing.Size(72, 23);
            this.emptyTileButton.TabIndex = 0;
            this.emptyTileButton.Text = "<emptyTileButton>";
            this.emptyTileButton.UseVisualStyleBackColor = true;
            this.emptyTileButton.Click += new System.EventHandler(this.emptyTileButton_Click);
            // 
            // slopeControlButton
            // 
            this.slopeControlButton.Location = new System.Drawing.Point(6, 3);
            this.slopeControlButton.Name = "slopeControlButton";
            this.slopeControlButton.Size = new System.Drawing.Size(87, 23);
            this.slopeControlButton.TabIndex = 0;
            this.slopeControlButton.Text = "<slopeControlButton>";
            this.slopeControlButton.UseVisualStyleBackColor = true;
            // 
            // TilesetObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "TilesetObjectEditor";
            this.Size = new System.Drawing.Size(674, 389);
            ((System.ComponentModel.ISupportInitialize)(this.editZone)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeight)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.map16Tile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.controlByte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.previewBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox editZone;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox previewBox;
        private Map16Picker map16Picker1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown map16Tile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown controlByte;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown objHeight;
        private System.Windows.Forms.NumericUpDown objWidth;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button emptyTileButton;
        private System.Windows.Forms.Button slopeControlButton;
        private System.Windows.Forms.Button newLineButton;
    }
}
