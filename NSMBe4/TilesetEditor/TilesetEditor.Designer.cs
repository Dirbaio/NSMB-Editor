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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TilesetEditor));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tilesetObjectEditor1 = new NSMBe4.TilesetObjectEditor();
            this.objectPickerControl1 = new NSMBe4.ObjectPickerControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.map16Editor1 = new NSMBe4.Map16Editor();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.graphicsEditor1 = new NSMBe4.GraphicsEditor();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportButton = new System.Windows.Forms.ToolStripButton();
            this.importButton = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTilesetButton = new System.Windows.Forms.ToolStripButton();
            this.importTilesetButton = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.deleteAllButton,
            this.toolStripSeparator2,
            this.exportButton,
            this.importButton,
            this.toolStripSeparator3,
            this.exportTilesetButton,
            this.importTilesetButton});
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
            this.toolStripButton1.Size = new System.Drawing.Size(130, 22);
            this.toolStripButton1.Text = "<toolStripButton1>";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // deleteAllButton
            // 
            this.deleteAllButton.Image = global::NSMBe4.Properties.Resources.cross_script;
            this.deleteAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteAllButton.Name = "deleteAllButton";
            this.deleteAllButton.Size = new System.Drawing.Size(61, 22);
            this.deleteAllButton.Text = "Del All";
            this.deleteAllButton.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // exportButton
            // 
            this.exportButton.Image = global::NSMBe4.Properties.Resources.image__arrow;
            this.exportButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(60, 22);
            this.exportButton.Text = "Export";
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click_1);
            // 
            // importButton
            // 
            this.importButton.Image = global::NSMBe4.Properties.Resources.image__plus;
            this.importButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(63, 22);
            this.importButton.Text = "Import";
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "PNG|*.png";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PNG|*.png";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // exportTilesetButton
            // 
            this.exportTilesetButton.Image = ((System.Drawing.Image)(resources.GetObject("exportTilesetButton.Image")));
            this.exportTilesetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportTilesetButton.Name = "exportTilesetButton";
            this.exportTilesetButton.Size = new System.Drawing.Size(94, 22);
            this.exportTilesetButton.Text = "export tileset";
            this.exportTilesetButton.Click += new System.EventHandler(this.exportTilesetButton_Click);
            // 
            // importTilesetButton
            // 
            this.importTilesetButton.Image = ((System.Drawing.Image)(resources.GetObject("importTilesetButton.Image")));
            this.importTilesetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importTilesetButton.Name = "importTilesetButton";
            this.importTilesetButton.Size = new System.Drawing.Size(97, 22);
            this.importTilesetButton.Text = "import tileset";
            this.importTilesetButton.Click += new System.EventHandler(this.importTilesetButton_Click);
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "NSMB Tilesets|*.nmt";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "NSMB Tilesets|*.nmt";
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
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton deleteAllButton;
        private System.Windows.Forms.ToolStripButton importButton;
        private System.Windows.Forms.ToolStripButton exportButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton exportTilesetButton;
        private System.Windows.Forms.ToolStripButton importTilesetButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
    }
}