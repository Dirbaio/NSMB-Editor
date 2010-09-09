namespace NSMBe4
{
    partial class SpriteEditor
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
            this.spriteListBox = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.spriteDataTextBox = new System.Windows.Forms.TextBox();
            this.spriteTypeUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.deleteSpriteButton = new System.Windows.Forms.Button();
            this.addSpriteButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.spriteYPosUpDown = new System.Windows.Forms.NumericUpDown();
            this.spriteXPosUpDown = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.spriteDataPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rawSpriteData = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clearSearch = new System.Windows.Forms.PictureBox();
            this.search = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteYPosUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteXPosUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.rawSpriteData.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clearSearch)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // spriteListBox
            // 
            this.spriteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.spriteListBox.FormattingEnabled = true;
            this.spriteListBox.IntegralHeight = false;
            this.spriteListBox.Location = new System.Drawing.Point(0, 133);
            this.spriteListBox.Name = "spriteListBox";
            this.spriteListBox.Size = new System.Drawing.Size(289, 262);
            this.spriteListBox.TabIndex = 26;
            this.spriteListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.spriteListBox_DrawItem);
            this.spriteListBox.SelectedIndexChanged += new System.EventHandler(this.spriteListBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 18);
            this.label10.TabIndex = 24;
            this.label10.Text = "<label10>";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spriteDataTextBox
            // 
            this.spriteDataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteDataTextBox.Location = new System.Drawing.Point(105, 6);
            this.spriteDataTextBox.Name = "spriteDataTextBox";
            this.spriteDataTextBox.Size = new System.Drawing.Size(181, 20);
            this.spriteDataTextBox.TabIndex = 23;
            this.spriteDataTextBox.Text = "00 00 00 00 00 00";
            this.spriteDataTextBox.TextChanged += new System.EventHandler(this.spriteDataTextBox_TextChanged);
            // 
            // spriteTypeUpDown
            // 
            this.spriteTypeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteTypeUpDown.Location = new System.Drawing.Point(108, 3);
            this.spriteTypeUpDown.Maximum = new decimal(new int[] {
            323,
            0,
            0,
            0});
            this.spriteTypeUpDown.Name = "spriteTypeUpDown";
            this.spriteTypeUpDown.Size = new System.Drawing.Size(181, 20);
            this.spriteTypeUpDown.TabIndex = 22;
            this.spriteTypeUpDown.ValueChanged += new System.EventHandler(this.spriteTypeUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(17, 4);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 15);
            this.label8.TabIndex = 21;
            this.label8.Text = "<label8>";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // deleteSpriteButton
            // 
            this.deleteSpriteButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deleteSpriteButton.Location = new System.Drawing.Point(150, 3);
            this.deleteSpriteButton.Name = "deleteSpriteButton";
            this.deleteSpriteButton.Size = new System.Drawing.Size(88, 23);
            this.deleteSpriteButton.TabIndex = 20;
            this.deleteSpriteButton.Text = "<deleteSpriteButton>";
            this.deleteSpriteButton.UseVisualStyleBackColor = true;
            this.deleteSpriteButton.Click += new System.EventHandler(this.deleteSpriteButton_Click);
            // 
            // addSpriteButton
            // 
            this.addSpriteButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addSpriteButton.Location = new System.Drawing.Point(56, 3);
            this.addSpriteButton.Name = "addSpriteButton";
            this.addSpriteButton.Size = new System.Drawing.Size(88, 23);
            this.addSpriteButton.TabIndex = 19;
            this.addSpriteButton.Text = "<addSpriteButton>";
            this.addSpriteButton.UseVisualStyleBackColor = true;
            this.addSpriteButton.Click += new System.EventHandler(this.addSpriteButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 45);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.63636F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.36364F));
            this.tableLayoutPanel3.Controls.Add(this.spriteYPosUpDown, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.spriteXPosUpDown, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.label11, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label9, 2, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(283, 26);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // spriteYPosUpDown
            // 
            this.spriteYPosUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteYPosUpDown.Location = new System.Drawing.Point(181, 3);
            this.spriteYPosUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spriteYPosUpDown.Name = "spriteYPosUpDown";
            this.spriteYPosUpDown.Size = new System.Drawing.Size(99, 20);
            this.spriteYPosUpDown.TabIndex = 5;
            this.spriteYPosUpDown.ValueChanged += new System.EventHandler(this.spriteYPosUpDown_ValueChanged);
            // 
            // spriteXPosUpDown
            // 
            this.spriteXPosUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteXPosUpDown.Location = new System.Drawing.Point(41, 3);
            this.spriteXPosUpDown.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.spriteXPosUpDown.Name = "spriteXPosUpDown";
            this.spriteXPosUpDown.Size = new System.Drawing.Size(96, 20);
            this.spriteXPosUpDown.TabIndex = 1;
            this.spriteXPosUpDown.ValueChanged += new System.EventHandler(this.spriteXPosUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 26);
            this.label11.TabIndex = 0;
            this.label11.Text = "<label11>";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(148, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 26);
            this.label9.TabIndex = 4;
            this.label9.Text = "<label9>";
            // 
            // spriteDataPanel
            // 
            this.spriteDataPanel.AutoSize = true;
            this.spriteDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.spriteDataPanel.Location = new System.Drawing.Point(0, 100);
            this.spriteDataPanel.MinimumSize = new System.Drawing.Size(10, 10);
            this.spriteDataPanel.Name = "spriteDataPanel";
            this.spriteDataPanel.Size = new System.Drawing.Size(289, 10);
            this.spriteDataPanel.TabIndex = 27;
            this.spriteDataPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.spriteDataPanel_Paint);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.spriteTypeUpDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 110);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 23);
            this.panel1.TabIndex = 28;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // rawSpriteData
            // 
            this.rawSpriteData.AutoSize = true;
            this.rawSpriteData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rawSpriteData.Controls.Add(this.spriteDataTextBox);
            this.rawSpriteData.Controls.Add(this.label10);
            this.rawSpriteData.Dock = System.Windows.Forms.DockStyle.Top;
            this.rawSpriteData.Location = new System.Drawing.Point(0, 71);
            this.rawSpriteData.MinimumSize = new System.Drawing.Size(10, 10);
            this.rawSpriteData.Name = "rawSpriteData";
            this.rawSpriteData.Size = new System.Drawing.Size(289, 29);
            this.rawSpriteData.TabIndex = 29;
            this.rawSpriteData.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.clearSearch);
            this.panel2.Controls.Add(this.search);
            this.panel2.Controls.Add(this.searchBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 395);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(289, 27);
            this.panel2.TabIndex = 30;
            // 
            // clearSearch
            // 
            this.clearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSearch.Image = global::NSMBe4.Properties.Resources.cross_script;
            this.clearSearch.Location = new System.Drawing.Point(268, 6);
            this.clearSearch.Name = "clearSearch";
            this.clearSearch.Size = new System.Drawing.Size(16, 16);
            this.clearSearch.TabIndex = 26;
            this.clearSearch.TabStop = false;
            this.clearSearch.Click += new System.EventHandler(this.clearSearch_Click);
            this.clearSearch.MouseEnter += new System.EventHandler(this.clearSearch_MouseEnter);
            this.clearSearch.MouseLeave += new System.EventHandler(this.clearSearch_MouseLeave);
            // 
            // search
            // 
            this.search.Location = new System.Drawing.Point(3, 3);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(85, 18);
            this.search.TabIndex = 25;
            this.search.Text = "<search>";
            this.search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.Location = new System.Drawing.Point(94, 3);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(168, 20);
            this.searchBox.TabIndex = 0;
            this.searchBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.addSpriteButton);
            this.panel3.Controls.Add(this.deleteSpriteButton);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(289, 26);
            this.panel3.TabIndex = 23;
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spriteListBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.spriteDataPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.rawSpriteData);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.Name = "SpriteEditor";
            this.Size = new System.Drawing.Size(289, 422);
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteYPosUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteXPosUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.rawSpriteData.ResumeLayout(false);
            this.rawSpriteData.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.clearSearch)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox spriteListBox;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox spriteDataTextBox;
        private System.Windows.Forms.NumericUpDown spriteTypeUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button deleteSpriteButton;
        private System.Windows.Forms.Button addSpriteButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.NumericUpDown spriteYPosUpDown;
        private System.Windows.Forms.NumericUpDown spriteXPosUpDown;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel spriteDataPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel rawSpriteData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label search;
        private System.Windows.Forms.PictureBox clearSearch;
        private System.Windows.Forms.Panel panel3;

    }
}
