namespace NSMBe4
{
    partial class ViewEditor
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
            this.viewsList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cameraID = new System.Windows.Forms.NumericUpDown();
            this.music = new System.Windows.Forms.ComboBox();
            this.unk1 = new System.Windows.Forms.NumericUpDown();
            this.unk2 = new System.Windows.Forms.NumericUpDown();
            this.unk3 = new System.Windows.Forms.NumericUpDown();
            this.light = new System.Windows.Forms.NumericUpDown();
            this.progressID = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.xPos = new System.Windows.Forms.NumericUpDown();
            this.width = new System.Windows.Forms.NumericUpDown();
            this.height = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.yPos = new System.Windows.Forms.NumericUpDown();
            this.viewID = new System.Windows.Forms.NumericUpDown();
            this.addViewButton = new System.Windows.Forms.Button();
            this.deleteViewButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cameraID)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.light)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressID)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewID)).BeginInit();
            this.SuspendLayout();
            // 
            // viewsList
            // 
            this.viewsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.viewsList.FormattingEnabled = true;
            this.viewsList.Location = new System.Drawing.Point(3, 3);
            this.viewsList.Name = "viewsList";
            this.viewsList.Size = new System.Drawing.Size(238, 82);
            this.viewsList.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 221);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(238, 210);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "View settings";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.cameraID, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.music, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.unk1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.unk2, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.unk3, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.light, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.progressID, 1, 6);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(7, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(225, 185);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Camera ID";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Unknown 1";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Unknown 2 (A)";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Unknow 3 (B)";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 137);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "3D Lighting";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Music";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 164);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Progress Path ID";
            // 
            // cameraID
            // 
            this.cameraID.Location = new System.Drawing.Point(115, 3);
            this.cameraID.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.cameraID.Name = "cameraID";
            this.cameraID.Size = new System.Drawing.Size(107, 20);
            this.cameraID.TabIndex = 1;
            this.cameraID.ValueChanged += new System.EventHandler(this.viewSettings_ValueChanged);
            // 
            // music
            // 
            this.music.FormattingEnabled = true;
            this.music.Items.AddRange(new object[] {
            "00 - None",
            "01 - Tower",
            "02 - Starman",
            "03 - Mega Mario",
            "04 - End of Level",
            "05 - Death",
            "06 - Desert",
            "07 - Bowser Jr. Battle",
            "08 - Minigames?",
            "09 - Underground",
            "0A - Bonus room?",
            "0B - Underwater",
            "0C - Lava",
            "0D - End of Game",
            "0E - Beach",
            "0F - Boss",
            "10 - Ghost House",
            "11 - Castle",
            "12 - Timer",
            "13 - End of Game (Other theme)",
            "14 - Minigames?",
            "15 - Final Boss",
            "16 - Boss Beaten",
            "17 - Nothing?",
            "18 - Mushrooms",
            "19 - Toad House",
            "1A - Grassland",
            "1B - Title Screen",
            "1C - SMB End of Level",
            "1D - Unknown fanfare",
            "1E - Multiplayer",
            "1F - Title Screen"});
            this.music.Location = new System.Drawing.Point(115, 29);
            this.music.Name = "music";
            this.music.Size = new System.Drawing.Size(107, 21);
            this.music.TabIndex = 2;
            this.music.SelectedIndexChanged += new System.EventHandler(this.viewSettings_ValueChanged);
            // 
            // unk1
            // 
            this.unk1.Location = new System.Drawing.Point(115, 56);
            this.unk1.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.unk1.Name = "unk1";
            this.unk1.Size = new System.Drawing.Size(107, 20);
            this.unk1.TabIndex = 1;
            this.unk1.ValueChanged += new System.EventHandler(this.viewSettings_ValueChanged);
            // 
            // unk2
            // 
            this.unk2.Location = new System.Drawing.Point(115, 82);
            this.unk2.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.unk2.Name = "unk2";
            this.unk2.Size = new System.Drawing.Size(107, 20);
            this.unk2.TabIndex = 1;
            this.unk2.ValueChanged += new System.EventHandler(this.viewSettings_ValueChanged);
            // 
            // unk3
            // 
            this.unk3.Location = new System.Drawing.Point(115, 108);
            this.unk3.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.unk3.Name = "unk3";
            this.unk3.Size = new System.Drawing.Size(107, 20);
            this.unk3.TabIndex = 1;
            this.unk3.ValueChanged += new System.EventHandler(this.viewSettings_ValueChanged);
            // 
            // light
            // 
            this.light.Location = new System.Drawing.Point(115, 134);
            this.light.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.light.Name = "light";
            this.light.Size = new System.Drawing.Size(107, 20);
            this.light.TabIndex = 1;
            this.light.ValueChanged += new System.EventHandler(this.viewSettings_ValueChanged);
            // 
            // progressID
            // 
            this.progressID.Location = new System.Drawing.Point(115, 160);
            this.progressID.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.progressID.Name = "progressID";
            this.progressID.Size = new System.Drawing.Size(107, 20);
            this.progressID.TabIndex = 1;
            this.progressID.ValueChanged += new System.EventHandler(this.viewSettings_ValueChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label9, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.xPos, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.width, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.height, 3, 1);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.yPos, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.viewID, 2, 2);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 136);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(238, 79);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Y:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(121, 32);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Height";
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(121, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Width";
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(3, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "X:";
            // 
            // xPos
            // 
            this.xPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.xPos.Location = new System.Drawing.Point(62, 3);
            this.xPos.Maximum = new decimal(new int[] {
            8912,
            0,
            0,
            0});
            this.xPos.Name = "xPos";
            this.xPos.Size = new System.Drawing.Size(53, 20);
            this.xPos.TabIndex = 1;
            this.xPos.ValueChanged += new System.EventHandler(this.position_ValueChanged);
            // 
            // width
            // 
            this.width.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.width.Location = new System.Drawing.Point(180, 3);
            this.width.Maximum = new decimal(new int[] {
            8912,
            0,
            0,
            0});
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(55, 20);
            this.width.TabIndex = 1;
            this.width.ValueChanged += new System.EventHandler(this.position_ValueChanged);
            // 
            // height
            // 
            this.height.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.height.Location = new System.Drawing.Point(180, 29);
            this.height.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(55, 20);
            this.height.TabIndex = 1;
            this.height.ValueChanged += new System.EventHandler(this.position_ValueChanged);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(3, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "ID:";
            // 
            // yPos
            // 
            this.yPos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.yPos.Location = new System.Drawing.Point(62, 29);
            this.yPos.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.yPos.Name = "yPos";
            this.yPos.Size = new System.Drawing.Size(53, 20);
            this.yPos.TabIndex = 1;
            this.yPos.ValueChanged += new System.EventHandler(this.position_ValueChanged);
            // 
            // viewID
            // 
            this.viewID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.SetColumnSpan(this.viewID, 2);
            this.viewID.Location = new System.Drawing.Point(121, 55);
            this.viewID.Maximum = new decimal(new int[] {
            4096,
            0,
            0,
            0});
            this.viewID.Name = "viewID";
            this.viewID.Size = new System.Drawing.Size(114, 20);
            this.viewID.TabIndex = 1;
            this.viewID.ValueChanged += new System.EventHandler(this.position_ValueChanged);
            // 
            // addViewButton
            // 
            this.addViewButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addViewButton.Location = new System.Drawing.Point(37, 108);
            this.addViewButton.Name = "addViewButton";
            this.addViewButton.Size = new System.Drawing.Size(82, 22);
            this.addViewButton.TabIndex = 3;
            this.addViewButton.Text = "Add";
            this.addViewButton.UseVisualStyleBackColor = true;
            this.addViewButton.Click += new System.EventHandler(this.addViewButton_Click);
            // 
            // deleteViewButton
            // 
            this.deleteViewButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deleteViewButton.Location = new System.Drawing.Point(125, 108);
            this.deleteViewButton.Name = "deleteViewButton";
            this.deleteViewButton.Size = new System.Drawing.Size(82, 22);
            this.deleteViewButton.TabIndex = 3;
            this.deleteViewButton.Text = "Delete";
            this.deleteViewButton.UseVisualStyleBackColor = true;
            this.deleteViewButton.Click += new System.EventHandler(this.deleteViewButton_Click);
            // 
            // ViewEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deleteViewButton);
            this.Controls.Add(this.addViewButton);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.viewsList);
            this.Name = "ViewEditor";
            this.Size = new System.Drawing.Size(244, 434);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cameraID)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.light)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.progressID)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.width)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewID)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox viewsList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown cameraID;
        private System.Windows.Forms.ComboBox music;
        private System.Windows.Forms.NumericUpDown unk1;
        private System.Windows.Forms.NumericUpDown unk2;
        private System.Windows.Forms.NumericUpDown unk3;
        private System.Windows.Forms.NumericUpDown light;
        private System.Windows.Forms.NumericUpDown progressID;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown xPos;
        private System.Windows.Forms.NumericUpDown width;
        private System.Windows.Forms.NumericUpDown height;
        private System.Windows.Forms.NumericUpDown yPos;
        private System.Windows.Forms.Button addViewButton;
        private System.Windows.Forms.Button deleteViewButton;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown viewID;
    }
}
