namespace NSMBe4
{
    partial class ObjectEditor
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
            this.deleteObjectButton = new System.Windows.Forms.Button();
            this.addObjectButton = new System.Windows.Forms.Button();
            this.objPositionBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.objXPosUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.objWidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.objHeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.objYPosUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.objPickerBox = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.objTypeUpDown = new System.Windows.Forms.NumericUpDown();
            this.objTileset0Button = new System.Windows.Forms.RadioButton();
            this.objTileset1Button = new System.Windows.Forms.RadioButton();
            this.objTileset2Button = new System.Windows.Forms.RadioButton();
            this.objPositionBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objXPosUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objWidthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objYPosUpDown)).BeginInit();
            this.objPickerBox.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objTypeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // deleteObjectButton
            // 
            this.deleteObjectButton.Location = new System.Drawing.Point(143, 3);
            this.deleteObjectButton.Name = "deleteObjectButton";
            this.deleteObjectButton.Size = new System.Drawing.Size(88, 23);
            this.deleteObjectButton.TabIndex = 12;
            this.deleteObjectButton.Text = "<deleteObjectButton>";
            this.deleteObjectButton.UseVisualStyleBackColor = true;
            this.deleteObjectButton.Click += new System.EventHandler(this.deleteObjectButton_Click);
            // 
            // addObjectButton
            // 
            this.addObjectButton.Location = new System.Drawing.Point(49, 3);
            this.addObjectButton.Name = "addObjectButton";
            this.addObjectButton.Size = new System.Drawing.Size(88, 23);
            this.addObjectButton.TabIndex = 11;
            this.addObjectButton.Text = "<addObjectButton>";
            this.addObjectButton.UseVisualStyleBackColor = true;
            this.addObjectButton.Click += new System.EventHandler(this.addObjectButton_Click);
            // 
            // objPositionBox
            // 
            this.objPositionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objPositionBox.Controls.Add(this.tableLayoutPanel1);
            this.objPositionBox.Location = new System.Drawing.Point(3, 32);
            this.objPositionBox.Name = "objPositionBox";
            this.objPositionBox.Size = new System.Drawing.Size(272, 69);
            this.objPositionBox.TabIndex = 9;
            this.objPositionBox.TabStop = false;
            this.objPositionBox.Text = "<objPositionBox>";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.8595F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.68595F));
            this.tableLayoutPanel1.Controls.Add(this.objXPosUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.objWidthUpDown, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.objHeightUpDown, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.objYPosUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(266, 50);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // objXPosUpDown
            // 
            this.objXPosUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objXPosUpDown.Location = new System.Drawing.Point(42, 3);
            this.objXPosUpDown.Maximum = new decimal(new int[] {
            511,
            0,
            0,
            0});
            this.objXPosUpDown.Name = "objXPosUpDown";
            this.objXPosUpDown.Size = new System.Drawing.Size(76, 20);
            this.objXPosUpDown.TabIndex = 1;
            this.objXPosUpDown.ValueChanged += new System.EventHandler(this.objXPosUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(142, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "<label2>";
            // 
            // objWidthUpDown
            // 
            this.objWidthUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objWidthUpDown.Location = new System.Drawing.Point(195, 3);
            this.objWidthUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.objWidthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.objWidthUpDown.Name = "objWidthUpDown";
            this.objWidthUpDown.Size = new System.Drawing.Size(68, 20);
            this.objWidthUpDown.TabIndex = 3;
            this.objWidthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.objWidthUpDown.ValueChanged += new System.EventHandler(this.objWidthUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "<label3>";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "<label4>";
            // 
            // objHeightUpDown
            // 
            this.objHeightUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objHeightUpDown.Location = new System.Drawing.Point(195, 28);
            this.objHeightUpDown.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.objHeightUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.objHeightUpDown.Name = "objHeightUpDown";
            this.objHeightUpDown.Size = new System.Drawing.Size(68, 20);
            this.objHeightUpDown.TabIndex = 6;
            this.objHeightUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.objHeightUpDown.ValueChanged += new System.EventHandler(this.objHeightUpDown_ValueChanged);
            // 
            // objYPosUpDown
            // 
            this.objYPosUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objYPosUpDown.Location = new System.Drawing.Point(42, 28);
            this.objYPosUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.objYPosUpDown.Name = "objYPosUpDown";
            this.objYPosUpDown.Size = new System.Drawing.Size(76, 20);
            this.objYPosUpDown.TabIndex = 7;
            this.objYPosUpDown.ValueChanged += new System.EventHandler(this.objYPosUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "<label1>";
            // 
            // objPickerBox
            // 
            this.objPickerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.objPickerBox.Controls.Add(this.tableLayoutPanel2);
            this.objPickerBox.Location = new System.Drawing.Point(6, 104);
            this.objPickerBox.Name = "objPickerBox";
            this.objPickerBox.Size = new System.Drawing.Size(272, 316);
            this.objPickerBox.TabIndex = 10;
            this.objPickerBox.TabStop = false;
            this.objPickerBox.Text = "<objPickerBox>";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.objTypeUpDown, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.objTileset0Button, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.objTileset1Button, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.objTileset2Button, 3, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(266, 51);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "<label5>";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "<label6>";
            // 
            // objTypeUpDown
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.objTypeUpDown, 3);
            this.objTypeUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objTypeUpDown.Location = new System.Drawing.Point(109, 28);
            this.objTypeUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.objTypeUpDown.Name = "objTypeUpDown";
            this.objTypeUpDown.Size = new System.Drawing.Size(154, 20);
            this.objTypeUpDown.TabIndex = 2;
            this.objTypeUpDown.ValueChanged += new System.EventHandler(this.objTypeUpDown_ValueChanged);
            // 
            // objTileset0Button
            // 
            this.objTileset0Button.AutoSize = true;
            this.objTileset0Button.Location = new System.Drawing.Point(109, 3);
            this.objTileset0Button.Name = "objTileset0Button";
            this.objTileset0Button.Size = new System.Drawing.Size(31, 17);
            this.objTileset0Button.TabIndex = 3;
            this.objTileset0Button.Text = "0";
            this.objTileset0Button.UseVisualStyleBackColor = true;
            this.objTileset0Button.Click += new System.EventHandler(this.objTileset0Button_Click);
            // 
            // objTileset1Button
            // 
            this.objTileset1Button.AutoSize = true;
            this.objTileset1Button.Location = new System.Drawing.Point(162, 3);
            this.objTileset1Button.Name = "objTileset1Button";
            this.objTileset1Button.Size = new System.Drawing.Size(31, 17);
            this.objTileset1Button.TabIndex = 4;
            this.objTileset1Button.Text = "1";
            this.objTileset1Button.UseVisualStyleBackColor = true;
            this.objTileset1Button.Click += new System.EventHandler(this.objTileset1Button_Click);
            // 
            // objTileset2Button
            // 
            this.objTileset2Button.AutoSize = true;
            this.objTileset2Button.Location = new System.Drawing.Point(215, 3);
            this.objTileset2Button.Name = "objTileset2Button";
            this.objTileset2Button.Size = new System.Drawing.Size(31, 17);
            this.objTileset2Button.TabIndex = 5;
            this.objTileset2Button.Text = "2";
            this.objTileset2Button.UseVisualStyleBackColor = true;
            this.objTileset2Button.Click += new System.EventHandler(this.objTileset2Button_Click);
            // 
            // ObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.deleteObjectButton);
            this.Controls.Add(this.addObjectButton);
            this.Controls.Add(this.objPickerBox);
            this.Controls.Add(this.objPositionBox);
            this.Name = "ObjectEditor";
            this.Size = new System.Drawing.Size(278, 423);
            this.objPositionBox.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objXPosUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objWidthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objYPosUpDown)).EndInit();
            this.objPickerBox.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objTypeUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deleteObjectButton;
        private System.Windows.Forms.Button addObjectButton;
        private System.Windows.Forms.GroupBox objPositionBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown objXPosUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown objWidthUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown objHeightUpDown;
        private System.Windows.Forms.NumericUpDown objYPosUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox objPickerBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown objTypeUpDown;
        private System.Windows.Forms.RadioButton objTileset0Button;
        private System.Windows.Forms.RadioButton objTileset1Button;
        private System.Windows.Forms.RadioButton objTileset2Button;

    }
}
