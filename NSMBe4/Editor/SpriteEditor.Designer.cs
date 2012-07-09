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
            this.spriteDataPanel = new System.Windows.Forms.Panel();
            this.search = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.clearSearch = new NSMBe4.XButton();
            this.clearSpriteData = new NSMBe4.XButton();
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).BeginInit();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // spriteListBox
            // 
            this.spriteListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.spriteListBox.FormattingEnabled = true;
            this.spriteListBox.IntegralHeight = false;
            this.spriteListBox.Location = new System.Drawing.Point(0, 88);
            this.spriteListBox.Name = "spriteListBox";
            this.spriteListBox.Size = new System.Drawing.Size(282, 308);
            this.spriteListBox.TabIndex = 26;
            this.spriteListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.spriteListBox_DrawItem);
            this.spriteListBox.SelectedIndexChanged += new System.EventHandler(this.spriteListBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(98, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "<label10>";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spriteDataTextBox
            // 
            this.spriteDataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteDataTextBox.Location = new System.Drawing.Point(107, 3);
            this.spriteDataTextBox.Name = "spriteDataTextBox";
            this.spriteDataTextBox.Size = new System.Drawing.Size(150, 20);
            this.spriteDataTextBox.TabIndex = 23;
            this.spriteDataTextBox.Text = "00 00 00 00 00 00";
            this.spriteDataTextBox.TextChanged += new System.EventHandler(this.spriteDataTextBox_TextChanged);
            // 
            // spriteTypeUpDown
            // 
            this.spriteTypeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteTypeUpDown.Location = new System.Drawing.Point(107, 29);
            this.spriteTypeUpDown.Maximum = new decimal(new int[] {
            323,
            0,
            0,
            0});
            this.spriteTypeUpDown.Name = "spriteTypeUpDown";
            this.spriteTypeUpDown.Size = new System.Drawing.Size(150, 20);
            this.spriteTypeUpDown.TabIndex = 22;
            this.spriteTypeUpDown.ValueChanged += new System.EventHandler(this.spriteTypeUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Location = new System.Drawing.Point(3, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 15);
            this.label8.TabIndex = 21;
            this.label8.Text = "<label8>";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // deleteSpriteButton
            // 
            this.deleteSpriteButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deleteSpriteButton.Location = new System.Drawing.Point(147, 3);
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
            this.addSpriteButton.Location = new System.Drawing.Point(53, 3);
            this.addSpriteButton.Name = "addSpriteButton";
            this.addSpriteButton.Size = new System.Drawing.Size(88, 23);
            this.addSpriteButton.TabIndex = 19;
            this.addSpriteButton.Text = "<addSpriteButton>";
            this.addSpriteButton.UseVisualStyleBackColor = true;
            this.addSpriteButton.Click += new System.EventHandler(this.addSpriteButton_Click);
            // 
            // spriteDataPanel
            // 
            this.spriteDataPanel.AutoSize = true;
            this.spriteDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.spriteDataPanel.Location = new System.Drawing.Point(0, 26);
            this.spriteDataPanel.MinimumSize = new System.Drawing.Size(10, 10);
            this.spriteDataPanel.Name = "spriteDataPanel";
            this.spriteDataPanel.Size = new System.Drawing.Size(282, 10);
            this.spriteDataPanel.TabIndex = 27;
            // 
            // search
            // 
            this.search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.search.Location = new System.Drawing.Point(3, 4);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(72, 18);
            this.search.TabIndex = 25;
            this.search.Text = "<search>";
            this.search.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // searchBox
            // 
            this.searchBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.searchBox.Location = new System.Drawing.Point(81, 3);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(176, 20);
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
            this.panel3.Size = new System.Drawing.Size(282, 26);
            this.panel3.TabIndex = 23;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel1.Controls.Add(this.clearSpriteData, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.spriteTypeUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.spriteDataTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(282, 52);
            this.tableLayoutPanel1.TabIndex = 31;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanel2.Controls.Add(this.clearSearch, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.search, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.searchBox, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 396);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(282, 26);
            this.tableLayoutPanel2.TabIndex = 32;
            // 
            // clearSearch
            // 
            this.clearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSearch.Location = new System.Drawing.Point(263, 5);
            this.clearSearch.Name = "clearSearch";
            this.clearSearch.Size = new System.Drawing.Size(16, 16);
            this.clearSearch.TabIndex = 26;
            this.clearSearch.Click += new System.EventHandler(this.clearSearch_Click);
            // 
            // clearSpriteData
            // 
            this.clearSpriteData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSpriteData.Location = new System.Drawing.Point(263, 5);
            this.clearSpriteData.Name = "clearSpriteData";
            this.clearSpriteData.Size = new System.Drawing.Size(16, 16);
            this.clearSpriteData.TabIndex = 23;
            this.clearSpriteData.Click += new System.EventHandler(this.clearSpriteData_Click);
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spriteListBox);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.spriteDataPanel);
            this.Controls.Add(this.panel3);
            this.Name = "SpriteEditor";
            this.Size = new System.Drawing.Size(282, 422);
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
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
        private System.Windows.Forms.Panel spriteDataPanel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label search;
        private System.Windows.Forms.Panel panel3;
        private XButton clearSearch;
        private XButton clearSpriteData;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

    }
}
