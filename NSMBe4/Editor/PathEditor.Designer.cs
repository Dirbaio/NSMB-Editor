namespace NSMBe4
{
    partial class PathEditor
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
            this.addPath = new System.Windows.Forms.Button();
            this.deletePath = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.pathID = new System.Windows.Forms.NumericUpDown();
            this.pathsList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.unk1 = new System.Windows.Forms.NumericUpDown();
            this.unk3 = new System.Windows.Forms.NumericUpDown();
            this.unk2 = new System.Windows.Forms.NumericUpDown();
            this.unk4 = new System.Windows.Forms.NumericUpDown();
            this.unk5 = new System.Windows.Forms.NumericUpDown();
            this.unk6 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathID)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unk1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk6)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.SuspendLayout();
            // 
            // addPath
            // 
            this.addPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addPath.Location = new System.Drawing.Point(47, 180);
            this.addPath.Name = "addPath";
            this.addPath.Size = new System.Drawing.Size(75, 21);
            this.addPath.TabIndex = 0;
            this.addPath.Text = "<addPath>";
            this.addPath.UseVisualStyleBackColor = true;
            this.addPath.Click += new System.EventHandler(this.addPath_Click);
            // 
            // deletePath
            // 
            this.deletePath.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.deletePath.Location = new System.Drawing.Point(128, 180);
            this.deletePath.Name = "deletePath";
            this.deletePath.Size = new System.Drawing.Size(75, 21);
            this.deletePath.TabIndex = 0;
            this.deletePath.Text = "<deletePath>";
            this.deletePath.UseVisualStyleBackColor = true;
            this.deletePath.Click += new System.EventHandler(this.deletePath_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pathID, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(235, 28);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "<label1>";
            // 
            // pathID
            // 
            this.pathID.Location = new System.Drawing.Point(120, 3);
            this.pathID.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.pathID.Name = "pathID";
            this.pathID.Size = new System.Drawing.Size(112, 20);
            this.pathID.TabIndex = 1;
            this.pathID.ValueChanged += new System.EventHandler(this.pathID_ValueChanged);
            // 
            // pathsList
            // 
            this.pathsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pathsList.FormattingEnabled = true;
            this.pathsList.Location = new System.Drawing.Point(3, 3);
            this.pathsList.Name = "pathsList";
            this.pathsList.Size = new System.Drawing.Size(244, 173);
            this.pathsList.TabIndex = 2;
            this.pathsList.SelectedIndexChanged += new System.EventHandler(this.pathsList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(6, 204);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(241, 53);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(3, 263);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(244, 183);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "<groupBox2>";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.label7, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.label8, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.unk1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.unk3, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.unk2, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.unk4, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.unk5, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.unk6, 1, 5);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 6;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(235, 158);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "<label3>";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "<label4>";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "<label6>";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "<label7>";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 137);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "<label8>";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(3, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "<label9>";
            // 
            // unk1
            // 
            this.unk1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.unk1.Location = new System.Drawing.Point(120, 3);
            this.unk1.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.unk1.Name = "unk1";
            this.unk1.Size = new System.Drawing.Size(112, 20);
            this.unk1.TabIndex = 1;
            this.unk1.ValueChanged += new System.EventHandler(this.unk1_ValueChanged);
            // 
            // unk3
            // 
            this.unk3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.unk3.Location = new System.Drawing.Point(120, 55);
            this.unk3.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.unk3.Name = "unk3";
            this.unk3.Size = new System.Drawing.Size(112, 20);
            this.unk3.TabIndex = 1;
            this.unk3.ValueChanged += new System.EventHandler(this.unk3_ValueChanged);
            // 
            // unk2
            // 
            this.unk2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.unk2.Location = new System.Drawing.Point(120, 29);
            this.unk2.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.unk2.Name = "unk2";
            this.unk2.Size = new System.Drawing.Size(112, 20);
            this.unk2.TabIndex = 1;
            this.unk2.ValueChanged += new System.EventHandler(this.unk2_ValueChanged);
            // 
            // unk4
            // 
            this.unk4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.unk4.Location = new System.Drawing.Point(120, 81);
            this.unk4.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.unk4.Name = "unk4";
            this.unk4.Size = new System.Drawing.Size(112, 20);
            this.unk4.TabIndex = 1;
            this.unk4.ValueChanged += new System.EventHandler(this.unk4_ValueChanged);
            // 
            // unk5
            // 
            this.unk5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.unk5.Location = new System.Drawing.Point(120, 107);
            this.unk5.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.unk5.Name = "unk5";
            this.unk5.Size = new System.Drawing.Size(112, 20);
            this.unk5.TabIndex = 1;
            this.unk5.ValueChanged += new System.EventHandler(this.unk5_ValueChanged);
            // 
            // unk6
            // 
            this.unk6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.unk6.Location = new System.Drawing.Point(120, 134);
            this.unk6.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.unk6.Name = "unk6";
            this.unk6.Size = new System.Drawing.Size(112, 20);
            this.unk6.TabIndex = 1;
            this.unk6.ValueChanged += new System.EventHandler(this.unk6_ValueChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(200, 100);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "<label10>";
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(61, 3);
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(53, 20);
            this.numericUpDown6.TabIndex = 1;
            // 
            // PathEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.addPath);
            this.Controls.Add(this.deletePath);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pathsList);
            this.Name = "PathEditor";
            this.Size = new System.Drawing.Size(250, 449);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathID)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unk1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk6)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addPath;
        private System.Windows.Forms.Button deletePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown pathID;
        private System.Windows.Forms.ListBox pathsList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown unk1;
        private System.Windows.Forms.NumericUpDown unk3;
        private System.Windows.Forms.NumericUpDown unk2;
        private System.Windows.Forms.NumericUpDown unk4;
        private System.Windows.Forms.NumericUpDown unk5;
        private System.Windows.Forms.NumericUpDown unk6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
    }
}
