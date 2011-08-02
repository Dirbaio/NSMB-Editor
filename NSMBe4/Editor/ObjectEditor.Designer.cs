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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.objXPosUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.objWidthUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.objHeightUpDown = new System.Windows.Forms.NumericUpDown();
            this.objYPosUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tileset0tab = new System.Windows.Forms.TabPage();
            this.tileset0picker = new NSMBe4.ObjectPickerControlNew();
            this.tileset1tab = new System.Windows.Forms.TabPage();
            this.tileset1picker = new NSMBe4.ObjectPickerControlNew();
            this.tileset2tab = new System.Windows.Forms.TabPage();
            this.tileset2picker = new NSMBe4.ObjectPickerControlNew();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objXPosUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objWidthUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeightUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.objYPosUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tileset0tab.SuspendLayout();
            this.tileset1tab.SuspendLayout();
            this.tileset2tab.SuspendLayout();
            this.SuspendLayout();
            // 
            // deleteObjectButton
            // 
            this.deleteObjectButton.Location = new System.Drawing.Point(141, 6);
            this.deleteObjectButton.Name = "deleteObjectButton";
            this.deleteObjectButton.Size = new System.Drawing.Size(88, 23);
            this.deleteObjectButton.TabIndex = 12;
            this.deleteObjectButton.Text = "<deleteObjectButton>";
            this.deleteObjectButton.UseVisualStyleBackColor = true;
            this.deleteObjectButton.Click += new System.EventHandler(this.deleteObjectButton_Click);
            // 
            // addObjectButton
            // 
            this.addObjectButton.Location = new System.Drawing.Point(47, 6);
            this.addObjectButton.Name = "addObjectButton";
            this.addObjectButton.Size = new System.Drawing.Size(88, 23);
            this.addObjectButton.TabIndex = 11;
            this.addObjectButton.Text = "<addObjectButton>";
            this.addObjectButton.UseVisualStyleBackColor = true;
            this.addObjectButton.Click += new System.EventHandler(this.addObjectButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.objXPosUpDown, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.objWidthUpDown, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.objHeightUpDown, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.objYPosUpDown, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 36);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(278, 54);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // objXPosUpDown
            // 
            this.objXPosUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objXPosUpDown.Location = new System.Drawing.Point(56, 3);
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
            this.label2.Location = new System.Drawing.Point(138, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "<label2>";
            // 
            // objWidthUpDown
            // 
            this.objWidthUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objWidthUpDown.Location = new System.Drawing.Point(191, 3);
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
            this.objWidthUpDown.Size = new System.Drawing.Size(84, 20);
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
            this.label3.Location = new System.Drawing.Point(3, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "<label3>";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(138, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "<label4>";
            // 
            // objHeightUpDown
            // 
            this.objHeightUpDown.Dock = System.Windows.Forms.DockStyle.Fill;
            this.objHeightUpDown.Location = new System.Drawing.Point(191, 30);
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
            this.objHeightUpDown.Size = new System.Drawing.Size(84, 20);
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
            this.objYPosUpDown.Location = new System.Drawing.Point(56, 30);
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
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "<label1>";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.deleteObjectButton);
            this.panel1.Controls.Add(this.addObjectButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 36);
            this.panel1.TabIndex = 13;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tileset0tab);
            this.tabControl1.Controls.Add(this.tileset1tab);
            this.tabControl1.Controls.Add(this.tileset2tab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 90);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(278, 333);
            this.tabControl1.TabIndex = 15;
            // 
            // tileset0tab
            // 
            this.tileset0tab.Controls.Add(this.tileset0picker);
            this.tileset0tab.Location = new System.Drawing.Point(4, 22);
            this.tileset0tab.Name = "tileset0tab";
            this.tileset0tab.Padding = new System.Windows.Forms.Padding(3);
            this.tileset0tab.Size = new System.Drawing.Size(270, 307);
            this.tileset0tab.TabIndex = 0;
            this.tileset0tab.Text = "Tileset 0";
            this.tileset0tab.UseVisualStyleBackColor = true;
            // 
            // tileset0picker
            // 
            this.tileset0picker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileset0picker.Location = new System.Drawing.Point(3, 3);
            this.tileset0picker.Name = "tileset0picker";
            this.tileset0picker.Size = new System.Drawing.Size(264, 301);
            this.tileset0picker.TabIndex = 0;
            this.tileset0picker.ObjectSelected += new NSMBe4.ObjectPickerControlNew.ObjectSelectedDelegate(this.tileset0picker_ObjectSelected);
            // 
            // tileset1tab
            // 
            this.tileset1tab.Controls.Add(this.tileset1picker);
            this.tileset1tab.Location = new System.Drawing.Point(4, 22);
            this.tileset1tab.Name = "tileset1tab";
            this.tileset1tab.Padding = new System.Windows.Forms.Padding(3);
            this.tileset1tab.Size = new System.Drawing.Size(270, 307);
            this.tileset1tab.TabIndex = 1;
            this.tileset1tab.Text = "Tileset 1";
            this.tileset1tab.UseVisualStyleBackColor = true;
            // 
            // tileset1picker
            // 
            this.tileset1picker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileset1picker.Location = new System.Drawing.Point(3, 3);
            this.tileset1picker.Name = "tileset1picker";
            this.tileset1picker.Size = new System.Drawing.Size(264, 301);
            this.tileset1picker.TabIndex = 1;
            this.tileset1picker.ObjectSelected += new NSMBe4.ObjectPickerControlNew.ObjectSelectedDelegate(this.tileset1picker_ObjectSelected);
            // 
            // tileset2tab
            // 
            this.tileset2tab.Controls.Add(this.tileset2picker);
            this.tileset2tab.Location = new System.Drawing.Point(4, 22);
            this.tileset2tab.Name = "tileset2tab";
            this.tileset2tab.Size = new System.Drawing.Size(270, 307);
            this.tileset2tab.TabIndex = 2;
            this.tileset2tab.Text = "Tileset 2";
            this.tileset2tab.UseVisualStyleBackColor = true;
            // 
            // tileset2picker
            // 
            this.tileset2picker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileset2picker.Location = new System.Drawing.Point(0, 0);
            this.tileset2picker.Name = "tileset2picker";
            this.tileset2picker.Size = new System.Drawing.Size(270, 307);
            this.tileset2picker.TabIndex = 1;
            this.tileset2picker.ObjectSelected += new NSMBe4.ObjectPickerControlNew.ObjectSelectedDelegate(this.tileset2picker_ObjectSelected);
            // 
            // ObjectEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "ObjectEditor";
            this.Size = new System.Drawing.Size(278, 423);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.objXPosUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objWidthUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objHeightUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.objYPosUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tileset0tab.ResumeLayout(false);
            this.tileset1tab.ResumeLayout(false);
            this.tileset2tab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button deleteObjectButton;
        private System.Windows.Forms.Button addObjectButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.NumericUpDown objXPosUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown objWidthUpDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown objHeightUpDown;
        private System.Windows.Forms.NumericUpDown objYPosUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tileset0tab;
        private ObjectPickerControlNew tileset0picker;
        private System.Windows.Forms.TabPage tileset1tab;
        private ObjectPickerControlNew tileset1picker;
        private System.Windows.Forms.TabPage tileset2tab;
        private ObjectPickerControlNew tileset2picker;

    }
}
