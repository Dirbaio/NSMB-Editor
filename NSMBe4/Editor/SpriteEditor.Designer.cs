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
            this.panel1 = new System.Windows.Forms.Panel();
            this.rawSpriteData = new System.Windows.Forms.Panel();
            this.clearSpriteData = new NSMBe4.XButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.clearSearch = new NSMBe4.XButton();
            this.search = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.rawSpriteData.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
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
            this.spriteListBox.Size = new System.Drawing.Size(289, 307);
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
            this.spriteDataTextBox.Size = new System.Drawing.Size(157, 20);
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
            this.spriteTypeUpDown.Size = new System.Drawing.Size(176, 20);
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
            // spriteDataPanel
            // 
            this.spriteDataPanel.AutoSize = true;
            this.spriteDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.spriteDataPanel.Location = new System.Drawing.Point(0, 26);
            this.spriteDataPanel.MinimumSize = new System.Drawing.Size(10, 10);
            this.spriteDataPanel.Name = "spriteDataPanel";
            this.spriteDataPanel.Size = new System.Drawing.Size(289, 10);
            this.spriteDataPanel.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.spriteTypeUpDown);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 65);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(289, 23);
            this.panel1.TabIndex = 28;
            // 
            // rawSpriteData
            // 
            this.rawSpriteData.AutoSize = true;
            this.rawSpriteData.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rawSpriteData.Controls.Add(this.clearSpriteData);
            this.rawSpriteData.Controls.Add(this.spriteDataTextBox);
            this.rawSpriteData.Controls.Add(this.label10);
            this.rawSpriteData.Dock = System.Windows.Forms.DockStyle.Top;
            this.rawSpriteData.Location = new System.Drawing.Point(0, 36);
            this.rawSpriteData.MinimumSize = new System.Drawing.Size(10, 10);
            this.rawSpriteData.Name = "rawSpriteData";
            this.rawSpriteData.Size = new System.Drawing.Size(289, 29);
            this.rawSpriteData.TabIndex = 29;
            // 
            // clearSpriteData
            // 
            this.clearSpriteData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSpriteData.Location = new System.Drawing.Point(268, 7);
            this.clearSpriteData.Name = "clearSpriteData";
            this.clearSpriteData.Size = new System.Drawing.Size(16, 16);
            this.clearSpriteData.TabIndex = 23;
            this.clearSpriteData.Click += new System.EventHandler(this.clearSpriteData_Click);
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
            this.clearSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearSearch.Location = new System.Drawing.Point(268, 5);
            this.clearSearch.Name = "clearSearch";
            this.clearSearch.Size = new System.Drawing.Size(16, 16);
            this.clearSearch.TabIndex = 26;
            this.clearSearch.Click += new System.EventHandler(this.clearSearch_Click);
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
            this.Controls.Add(this.rawSpriteData);
            this.Controls.Add(this.spriteDataPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Name = "SpriteEditor";
            this.Size = new System.Drawing.Size(289, 422);
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.rawSpriteData.ResumeLayout(false);
            this.rawSpriteData.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
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
        private System.Windows.Forms.Panel spriteDataPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel rawSpriteData;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label search;
        private System.Windows.Forms.Panel panel3;
        private XButton clearSearch;
        private XButton clearSpriteData;

    }
}
