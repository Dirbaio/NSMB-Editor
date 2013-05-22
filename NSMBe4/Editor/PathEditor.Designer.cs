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
            this.lblPathID = new System.Windows.Forms.Label();
            this.pathID = new System.Windows.Forms.NumericUpDown();
            this.pathsList = new System.Windows.Forms.ListBox();
            this.grpPathSettings = new System.Windows.Forms.GroupBox();
            this.grpNodeSettings = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblValue1 = new System.Windows.Forms.Label();
            this.lblValue2 = new System.Windows.Forms.Label();
            this.lblValue4 = new System.Windows.Forms.Label();
            this.lblValue5 = new System.Windows.Forms.Label();
            this.lblValue6 = new System.Windows.Forms.Label();
            this.lblValue3 = new System.Windows.Forms.Label();
            this.unk1 = new System.Windows.Forms.NumericUpDown();
            this.unk3 = new System.Windows.Forms.NumericUpDown();
            this.unk2 = new System.Windows.Forms.NumericUpDown();
            this.unk4 = new System.Windows.Forms.NumericUpDown();
            this.unk5 = new System.Windows.Forms.NumericUpDown();
            this.unk6 = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathID)).BeginInit();
            this.grpPathSettings.SuspendLayout();
            this.grpNodeSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.unk1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unk6)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // addPath
            // 
            this.addPath.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.addPath.Location = new System.Drawing.Point(47, 169);
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
            this.deletePath.Location = new System.Drawing.Point(128, 169);
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
            this.tableLayoutPanel1.Controls.Add(this.lblPathID, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pathID, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(235, 28);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // lblPathID
            // 
            this.lblPathID.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPathID.AutoSize = true;
            this.lblPathID.Location = new System.Drawing.Point(3, 7);
            this.lblPathID.Name = "lblPathID";
            this.lblPathID.Size = new System.Drawing.Size(52, 13);
            this.lblPathID.TabIndex = 0;
            this.lblPathID.Text = "<PathID>";
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
            this.pathsList.Location = new System.Drawing.Point(0, 0);
            this.pathsList.Name = "pathsList";
            this.pathsList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.pathsList.Size = new System.Drawing.Size(250, 160);
            this.pathsList.TabIndex = 2;
            this.pathsList.SelectedIndexChanged += new System.EventHandler(this.pathsList_SelectedIndexChanged);
            // 
            // grpPathSettings
            // 
            this.grpPathSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPathSettings.Controls.Add(this.tableLayoutPanel1);
            this.grpPathSettings.Location = new System.Drawing.Point(3, 8);
            this.grpPathSettings.Name = "grpPathSettings";
            this.grpPathSettings.Size = new System.Drawing.Size(244, 53);
            this.grpPathSettings.TabIndex = 3;
            this.grpPathSettings.TabStop = false;
            this.grpPathSettings.Text = "<PathSettings>";
            // 
            // grpNodeSettings
            // 
            this.grpNodeSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpNodeSettings.Controls.Add(this.tableLayoutPanel2);
            this.grpNodeSettings.Location = new System.Drawing.Point(3, 67);
            this.grpNodeSettings.Name = "grpNodeSettings";
            this.grpNodeSettings.Size = new System.Drawing.Size(244, 183);
            this.grpNodeSettings.TabIndex = 4;
            this.grpNodeSettings.TabStop = false;
            this.grpNodeSettings.Text = "<NodeSettings>";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.lblValue1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblValue2, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.lblValue4, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.lblValue5, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.lblValue6, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.lblValue3, 0, 2);
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
            // lblValue1
            // 
            this.lblValue1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValue1.AutoSize = true;
            this.lblValue1.Location = new System.Drawing.Point(3, 6);
            this.lblValue1.Name = "lblValue1";
            this.lblValue1.Size = new System.Drawing.Size(52, 13);
            this.lblValue1.TabIndex = 0;
            this.lblValue1.Text = "<Value1>";
            // 
            // lblValue2
            // 
            this.lblValue2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValue2.AutoSize = true;
            this.lblValue2.Location = new System.Drawing.Point(3, 32);
            this.lblValue2.Name = "lblValue2";
            this.lblValue2.Size = new System.Drawing.Size(52, 13);
            this.lblValue2.TabIndex = 0;
            this.lblValue2.Text = "<Value2>";
            // 
            // lblValue4
            // 
            this.lblValue4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValue4.AutoSize = true;
            this.lblValue4.Location = new System.Drawing.Point(3, 84);
            this.lblValue4.Name = "lblValue4";
            this.lblValue4.Size = new System.Drawing.Size(52, 13);
            this.lblValue4.TabIndex = 0;
            this.lblValue4.Text = "<Value4>";
            // 
            // lblValue5
            // 
            this.lblValue5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValue5.AutoSize = true;
            this.lblValue5.Location = new System.Drawing.Point(3, 110);
            this.lblValue5.Name = "lblValue5";
            this.lblValue5.Size = new System.Drawing.Size(52, 13);
            this.lblValue5.TabIndex = 0;
            this.lblValue5.Text = "<Value5>";
            // 
            // lblValue6
            // 
            this.lblValue6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValue6.AutoSize = true;
            this.lblValue6.Location = new System.Drawing.Point(3, 137);
            this.lblValue6.Name = "lblValue6";
            this.lblValue6.Size = new System.Drawing.Size(52, 13);
            this.lblValue6.TabIndex = 0;
            this.lblValue6.Text = "<Value6>";
            // 
            // lblValue3
            // 
            this.lblValue3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblValue3.AutoSize = true;
            this.lblValue3.Location = new System.Drawing.Point(3, 58);
            this.lblValue3.Name = "lblValue3";
            this.lblValue3.Size = new System.Drawing.Size(52, 13);
            this.lblValue3.TabIndex = 0;
            this.lblValue3.Text = "<Value3>";
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
            // panel1
            // 
            this.panel1.Controls.Add(this.deletePath);
            this.panel1.Controls.Add(this.addPath);
            this.panel1.Controls.Add(this.pathsList);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 196);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grpNodeSettings);
            this.panel2.Controls.Add(this.grpPathSettings);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 196);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(250, 253);
            this.panel2.TabIndex = 3;
            // 
            // PathEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "PathEditor";
            this.Size = new System.Drawing.Size(250, 449);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pathID)).EndInit();
            this.grpPathSettings.ResumeLayout(false);
            this.grpNodeSettings.ResumeLayout(false);
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
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button addPath;
        private System.Windows.Forms.Button deletePath;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblPathID;
        private System.Windows.Forms.NumericUpDown pathID;
        private System.Windows.Forms.ListBox pathsList;
        private System.Windows.Forms.GroupBox grpPathSettings;
        private System.Windows.Forms.GroupBox grpNodeSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label lblValue1;
        private System.Windows.Forms.Label lblValue2;
        private System.Windows.Forms.Label lblValue4;
        private System.Windows.Forms.Label lblValue5;
        private System.Windows.Forms.Label lblValue6;
        private System.Windows.Forms.Label lblValue3;
        private System.Windows.Forms.NumericUpDown unk1;
        private System.Windows.Forms.NumericUpDown unk3;
        private System.Windows.Forms.NumericUpDown unk2;
        private System.Windows.Forms.NumericUpDown unk4;
        private System.Windows.Forms.NumericUpDown unk5;
        private System.Windows.Forms.NumericUpDown unk6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
