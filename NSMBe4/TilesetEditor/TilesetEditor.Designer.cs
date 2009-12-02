namespace NSMBe4
{
    partial class TilesetEditor
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tilesetObjectEditor1 = new NSMBe4.TilesetObjectEditor();
            this.objectPickerControl1 = new NSMBe4.ObjectPickerControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.map16Editor1 = new NSMBe4.Map16Editor();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.graphicsEditor1 = new NSMBe4.GraphicsEditor();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.newTilesGroupBox = new System.Windows.Forms.GroupBox();
            this.palette2RadioButton = new System.Windows.Forms.RadioButton();
            this.palette1RadioButton = new System.Windows.Forms.RadioButton();
            this.exportButton = new System.Windows.Forms.Button();
            this.importButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.filenameTextBox = new System.Windows.Forms.TextBox();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.filenameLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.newTilesGroupBox.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(836, 536);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.tilesetObjectEditor1);
            this.tabPage1.Controls.Add(this.objectPickerControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(828, 510);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<tabPage1>";
            // 
            // tilesetObjectEditor1
            // 
            this.tilesetObjectEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetObjectEditor1.Location = new System.Drawing.Point(181, 3);
            this.tilesetObjectEditor1.Name = "tilesetObjectEditor1";
            this.tilesetObjectEditor1.Size = new System.Drawing.Size(644, 504);
            this.tilesetObjectEditor1.TabIndex = 2;
            this.tilesetObjectEditor1.mustRepaintObjects += new NSMBe4.TilesetObjectEditor.mustRepaintObjectsD(this.mustRepaintObjects);
            // 
            // objectPickerControl1
            // 
            this.objectPickerControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.objectPickerControl1.Location = new System.Drawing.Point(3, 3);
            this.objectPickerControl1.Name = "objectPickerControl1";
            this.objectPickerControl1.Size = new System.Drawing.Size(178, 504);
            this.objectPickerControl1.TabIndex = 1;
            this.objectPickerControl1.ObjectSelected += new NSMBe4.ObjectPickerControl.ObjectSelectedDelegate(this.objectPickerControl1_ObjectSelected);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.map16Editor1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(828, 510);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "<tabPage2>";
            // 
            // map16Editor1
            // 
            this.map16Editor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.map16Editor1.Location = new System.Drawing.Point(3, 3);
            this.map16Editor1.Name = "map16Editor1";
            this.map16Editor1.Size = new System.Drawing.Size(822, 504);
            this.map16Editor1.TabIndex = 0;
            this.map16Editor1.mustRepaintObjects += new NSMBe4.Map16Editor.mustRepaintObjectsD(this.mustRepaintObjects);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.graphicsEditor1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(828, 510);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "<tabPage3>";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // graphicsEditor1
            // 
            this.graphicsEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graphicsEditor1.Location = new System.Drawing.Point(3, 3);
            this.graphicsEditor1.Name = "graphicsEditor1";
            this.graphicsEditor1.Size = new System.Drawing.Size(822, 504);
            this.graphicsEditor1.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.newTilesGroupBox);
            this.tabPage4.Controls.Add(this.exportButton);
            this.tabPage4.Controls.Add(this.importButton);
            this.tabPage4.Controls.Add(this.tableLayoutPanel1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(828, 510);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "<tabPage4>";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // newTilesGroupBox
            // 
            this.newTilesGroupBox.Controls.Add(this.palette2RadioButton);
            this.newTilesGroupBox.Controls.Add(this.palette1RadioButton);
            this.newTilesGroupBox.Location = new System.Drawing.Point(462, 68);
            this.newTilesGroupBox.Name = "newTilesGroupBox";
            this.newTilesGroupBox.Size = new System.Drawing.Size(200, 100);
            this.newTilesGroupBox.TabIndex = 3;
            this.newTilesGroupBox.TabStop = false;
            this.newTilesGroupBox.Text = "insert new tiles into:";
            // 
            // palette2RadioButton
            // 
            this.palette2RadioButton.AutoSize = true;
            this.palette2RadioButton.Location = new System.Drawing.Point(18, 47);
            this.palette2RadioButton.Name = "palette2RadioButton";
            this.palette2RadioButton.Size = new System.Drawing.Size(48, 17);
            this.palette2RadioButton.TabIndex = 0;
            this.palette2RadioButton.TabStop = true;
            this.palette2RadioButton.Text = "pal 2";
            this.palette2RadioButton.UseVisualStyleBackColor = true;
            // 
            // palette1RadioButton
            // 
            this.palette1RadioButton.AutoSize = true;
            this.palette1RadioButton.Location = new System.Drawing.Point(18, 24);
            this.palette1RadioButton.Name = "palette1RadioButton";
            this.palette1RadioButton.Size = new System.Drawing.Size(48, 17);
            this.palette1RadioButton.TabIndex = 0;
            this.palette1RadioButton.TabStop = true;
            this.palette1RadioButton.Text = "pal 1";
            this.palette1RadioButton.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(369, 68);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 2;
            this.exportButton.Text = "export";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // importButton
            // 
            this.importButton.Location = new System.Drawing.Point(288, 68);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 2;
            this.importButton.Text = "import";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.filenameTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.selectFileButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.filenameLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(822, 50);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // filenameTextBox
            // 
            this.filenameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.filenameTextBox.Location = new System.Drawing.Point(58, 15);
            this.filenameTextBox.Name = "filenameTextBox";
            this.filenameTextBox.Size = new System.Drawing.Size(636, 20);
            this.filenameTextBox.TabIndex = 0;
            // 
            // selectFileButton
            // 
            this.selectFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.selectFileButton.AutoSize = true;
            this.selectFileButton.Location = new System.Drawing.Point(700, 13);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(119, 23);
            this.selectFileButton.TabIndex = 2;
            this.selectFileButton.Text = "select file...";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // filenameLabel
            // 
            this.filenameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.filenameLabel.AutoSize = true;
            this.filenameLabel.Location = new System.Drawing.Point(3, 18);
            this.filenameLabel.Name = "filenameLabel";
            this.filenameLabel.Size = new System.Drawing.Size(49, 13);
            this.filenameLabel.TabIndex = 1;
            this.filenameLabel.Text = "file name";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(836, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::NSMBe4.Properties.Resources.save;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(121, 22);
            this.toolStripButton1.Text = "<toolStripButton1>";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "PNG|*.png";
            // 
            // TilesetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TilesetEditor";
            this.Text = "TilesetEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TilesetEditor_FormClosed);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.newTilesGroupBox.ResumeLayout(false);
            this.newTilesGroupBox.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ObjectPickerControl objectPickerControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private TilesetObjectEditor tilesetObjectEditor1;
        private System.Windows.Forms.TabPage tabPage2;
        private Map16Editor map16Editor1;
        private System.Windows.Forms.TabPage tabPage3;
        private GraphicsEditor graphicsEditor1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.Button importButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox filenameTextBox;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.Label filenameLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox newTilesGroupBox;
        private System.Windows.Forms.RadioButton palette2RadioButton;
        private System.Windows.Forms.RadioButton palette1RadioButton;
    }
}