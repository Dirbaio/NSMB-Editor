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
            this.saveSpriteDataButton = new System.Windows.Forms.Button();
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
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteYPosUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteXPosUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // spriteListBox
            // 
            this.spriteListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.spriteListBox.FormattingEnabled = true;
            this.spriteListBox.IntegralHeight = false;
            this.spriteListBox.Location = new System.Drawing.Point(4, 166);
            this.spriteListBox.Name = "spriteListBox";
            this.spriteListBox.Size = new System.Drawing.Size(266, 211);
            this.spriteListBox.TabIndex = 26;
            this.spriteListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.spriteListBox_DrawItem);
            this.spriteListBox.SelectedIndexChanged += new System.EventHandler(this.spriteListBox_SelectedIndexChanged);
            // 
            // saveSpriteDataButton
            // 
            this.saveSpriteDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.saveSpriteDataButton.Location = new System.Drawing.Point(118, 137);
            this.saveSpriteDataButton.Name = "saveSpriteDataButton";
            this.saveSpriteDataButton.Size = new System.Drawing.Size(146, 23);
            this.saveSpriteDataButton.TabIndex = 25;
            this.saveSpriteDataButton.Text = "Save Sprite Data";
            this.saveSpriteDataButton.UseVisualStyleBackColor = true;
            this.saveSpriteDataButton.Click += new System.EventHandler(this.saveSpriteDataButton_Click);
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(12, 113);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 13);
            this.label10.TabIndex = 24;
            this.label10.Text = "Sprite Data:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // spriteDataTextBox
            // 
            this.spriteDataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteDataTextBox.Location = new System.Drawing.Point(118, 110);
            this.spriteDataTextBox.Name = "spriteDataTextBox";
            this.spriteDataTextBox.Size = new System.Drawing.Size(146, 20);
            this.spriteDataTextBox.TabIndex = 23;
            this.spriteDataTextBox.Text = "00 00 00 00 00 00";
            // 
            // spriteTypeUpDown
            // 
            this.spriteTypeUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.spriteTypeUpDown.Location = new System.Drawing.Point(118, 83);
            this.spriteTypeUpDown.Maximum = new decimal(new int[] {
            323,
            0,
            0,
            0});
            this.spriteTypeUpDown.Name = "spriteTypeUpDown";
            this.spriteTypeUpDown.Size = new System.Drawing.Size(146, 20);
            this.spriteTypeUpDown.TabIndex = 22;
            this.spriteTypeUpDown.ValueChanged += new System.EventHandler(this.spriteTypeUpDown_ValueChanged);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(12, 85);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Sprite Type:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // deleteSpriteButton
            // 
            this.deleteSpriteButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.deleteSpriteButton.Location = new System.Drawing.Point(136, 3);
            this.deleteSpriteButton.Name = "deleteSpriteButton";
            this.deleteSpriteButton.Size = new System.Drawing.Size(88, 23);
            this.deleteSpriteButton.TabIndex = 20;
            this.deleteSpriteButton.Text = "Delete Sprite";
            this.deleteSpriteButton.UseVisualStyleBackColor = true;
            this.deleteSpriteButton.Click += new System.EventHandler(this.deleteSpriteButton_Click);
            // 
            // addSpriteButton
            // 
            this.addSpriteButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.addSpriteButton.Location = new System.Drawing.Point(42, 3);
            this.addSpriteButton.Name = "addSpriteButton";
            this.addSpriteButton.Size = new System.Drawing.Size(88, 23);
            this.addSpriteButton.TabIndex = 19;
            this.addSpriteButton.Text = "Add Sprite";
            this.addSpriteButton.UseVisualStyleBackColor = true;
            this.addSpriteButton.Click += new System.EventHandler(this.addSpriteButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel3);
            this.groupBox1.Location = new System.Drawing.Point(4, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(266, 45);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sprite Positioning";
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
            this.tableLayoutPanel3.Size = new System.Drawing.Size(260, 26);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // spriteYPosUpDown
            // 
            this.spriteYPosUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteYPosUpDown.Location = new System.Drawing.Point(167, 3);
            this.spriteYPosUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.spriteYPosUpDown.Name = "spriteYPosUpDown";
            this.spriteYPosUpDown.Size = new System.Drawing.Size(90, 20);
            this.spriteYPosUpDown.TabIndex = 5;
            this.spriteYPosUpDown.ValueChanged += new System.EventHandler(this.spriteYPosUpDown_ValueChanged);
            // 
            // spriteXPosUpDown
            // 
            this.spriteXPosUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spriteXPosUpDown.Location = new System.Drawing.Point(38, 3);
            this.spriteXPosUpDown.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.spriteXPosUpDown.Name = "spriteXPosUpDown";
            this.spriteXPosUpDown.Size = new System.Drawing.Size(88, 20);
            this.spriteXPosUpDown.TabIndex = 1;
            this.spriteXPosUpDown.ValueChanged += new System.EventHandler(this.spriteXPosUpDown_ValueChanged);
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 6);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "X:";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(144, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Y:";
            // 
            // SpriteEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.spriteListBox);
            this.Controls.Add(this.saveSpriteDataButton);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.spriteDataTextBox);
            this.Controls.Add(this.spriteTypeUpDown);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.deleteSpriteButton);
            this.Controls.Add(this.addSpriteButton);
            this.Controls.Add(this.groupBox1);
            this.Name = "SpriteEditor";
            this.Size = new System.Drawing.Size(273, 380);
            ((System.ComponentModel.ISupportInitialize)(this.spriteTypeUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spriteYPosUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteXPosUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox spriteListBox;
        private System.Windows.Forms.Button saveSpriteDataButton;
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

    }
}
