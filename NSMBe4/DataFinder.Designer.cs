namespace NSMBe4 {
    partial class DataFinder {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataFinder));
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.findBlockRadioButton = new System.Windows.Forms.RadioButton();
            this.findSpriteRadioButton = new System.Windows.Forms.RadioButton();
            this.blockNumberUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.splitCountUpDown = new System.Windows.Forms.NumericUpDown();
            this.spriteUpDown = new System.Windows.Forms.NumericUpDown();
            this.processButton = new System.Windows.Forms.Button();
            this.labellingTypeCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.blockNumberUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCountUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // outputTextBox
            // 
            this.outputTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.outputTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.outputTextBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.outputTextBox.Location = new System.Drawing.Point(12, 121);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputTextBox.Size = new System.Drawing.Size(407, 140);
            this.outputTextBox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(406, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "<label1>";
            // 
            // findBlockRadioButton
            // 
            this.findBlockRadioButton.AutoSize = true;
            this.findBlockRadioButton.Location = new System.Drawing.Point(16, 71);
            this.findBlockRadioButton.Name = "findBlockRadioButton";
            this.findBlockRadioButton.Size = new System.Drawing.Size(140, 17);
            this.findBlockRadioButton.TabIndex = 2;
            this.findBlockRadioButton.TabStop = true;
            this.findBlockRadioButton.Text = "<findBlockRadioButton>";
            this.findBlockRadioButton.UseVisualStyleBackColor = true;
            // 
            // findSpriteRadioButton
            // 
            this.findSpriteRadioButton.AutoSize = true;
            this.findSpriteRadioButton.Location = new System.Drawing.Point(16, 95);
            this.findSpriteRadioButton.Name = "findSpriteRadioButton";
            this.findSpriteRadioButton.Size = new System.Drawing.Size(140, 17);
            this.findSpriteRadioButton.TabIndex = 3;
            this.findSpriteRadioButton.TabStop = true;
            this.findSpriteRadioButton.Text = "<findSpriteRadioButton>";
            this.findSpriteRadioButton.UseVisualStyleBackColor = true;
            // 
            // blockNumberUpDown
            // 
            this.blockNumberUpDown.Location = new System.Drawing.Point(216, 71);
            this.blockNumberUpDown.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.blockNumberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.blockNumberUpDown.Name = "blockNumberUpDown";
            this.blockNumberUpDown.Size = new System.Drawing.Size(60, 20);
            this.blockNumberUpDown.TabIndex = 4;
            this.blockNumberUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(282, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "<label2>";
            // 
            // splitCountUpDown
            // 
            this.splitCountUpDown.Location = new System.Drawing.Point(340, 71);
            this.splitCountUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.splitCountUpDown.Name = "splitCountUpDown";
            this.splitCountUpDown.Size = new System.Drawing.Size(79, 20);
            this.splitCountUpDown.TabIndex = 6;
            // 
            // spriteUpDown
            // 
            this.spriteUpDown.Location = new System.Drawing.Point(216, 95);
            this.spriteUpDown.Maximum = new decimal(new int[] {
            323,
            0,
            0,
            0});
            this.spriteUpDown.Name = "spriteUpDown";
            this.spriteUpDown.Size = new System.Drawing.Size(60, 20);
            this.spriteUpDown.TabIndex = 7;
            this.spriteUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // processButton
            // 
            this.processButton.Location = new System.Drawing.Point(344, 95);
            this.processButton.Name = "processButton";
            this.processButton.Size = new System.Drawing.Size(75, 20);
            this.processButton.TabIndex = 8;
            this.processButton.Text = "<processButton>";
            this.processButton.UseVisualStyleBackColor = true;
            this.processButton.Click += new System.EventHandler(this.processButton_Click);
            // 
            // labellingTypeCheckBox
            // 
            this.labellingTypeCheckBox.AutoSize = true;
            this.labellingTypeCheckBox.Location = new System.Drawing.Point(323, 99);
            this.labellingTypeCheckBox.Name = "labellingTypeCheckBox";
            this.labellingTypeCheckBox.Size = new System.Drawing.Size(15, 14);
            this.labellingTypeCheckBox.TabIndex = 9;
            this.labellingTypeCheckBox.UseVisualStyleBackColor = true;
            // 
            // DataFinder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 273);
            this.Controls.Add(this.labellingTypeCheckBox);
            this.Controls.Add(this.processButton);
            this.Controls.Add(this.spriteUpDown);
            this.Controls.Add(this.splitCountUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.blockNumberUpDown);
            this.Controls.Add(this.findSpriteRadioButton);
            this.Controls.Add(this.findBlockRadioButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.outputTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataFinder";
            this.Text = "<_TITLE>";
            this.Load += new System.EventHandler(this.DataFinder_Load);
            ((System.ComponentModel.ISupportInitialize)(this.blockNumberUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitCountUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spriteUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton findBlockRadioButton;
        private System.Windows.Forms.RadioButton findSpriteRadioButton;
        private System.Windows.Forms.NumericUpDown blockNumberUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown splitCountUpDown;
        private System.Windows.Forms.NumericUpDown spriteUpDown;
        private System.Windows.Forms.Button processButton;
        private System.Windows.Forms.CheckBox labellingTypeCheckBox;
    }
}