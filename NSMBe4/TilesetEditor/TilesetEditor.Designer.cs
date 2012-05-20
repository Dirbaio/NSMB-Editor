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
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.imageManager1 = new NSMBe4.ImageManager();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportButton = new System.Windows.Forms.ToolStripButton();
            this.importButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportTilesetButton = new System.Windows.Forms.ToolStripButton();
            this.importTilesetButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.createDescriptions = new System.Windows.Forms.ToolStripButton();
            this.deleteDescriptions = new System.Windows.Forms.ToolStripButton();
            this.setend = new System.Windows.Forms.ToolStripButton();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.tilemapEditor1 = new NSMBe4.TilemapEditor.TilemapEditor();
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
            this.tabControl1.Size = new System.Drawing.Size(957, 536);
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
            this.tabPage1.Size = new System.Drawing.Size(949, 510);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<tabPage1>";
            // 
            // tilesetObjectEditor1
            // 
            this.tilesetObjectEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetObjectEditor1.Location = new System.Drawing.Point(181, 3);
            this.tilesetObjectEditor1.Name = "tilesetObjectEditor1";
            this.tilesetObjectEditor1.Size = new System.Drawing.Size(765, 504);
            this.tilesetObjectEditor1.TabIndex = 2;
            this.tilesetObjectEditor1.mustRepaintObjects += new NSMBe4.TilesetObjectEditor.mustRepaintObjectsD(this.mustRepaintObjects);
            this.tilesetObjectEditor1.DescriptionChanged += new NSMBe4.TilesetObjectEditor.changeDescription(this.tilesetObjectEditor1_DescriptionChanged);
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
            this.tabPage2.Controls.Add(this.tilemapEditor1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(949, 510);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "<tabPage2>";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.imageManager1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(949, 510);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // imageManager1
            // 
            this.imageManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageManager1.Location = new System.Drawing.Point(3, 3);
            this.imageManager1.Name = "imageManager1";
            this.imageManager1.Size = new System.Drawing.Size(943, 504);
            this.imageManager1.TabIndex = 0;
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
            this.importTilesetButton,
            this.toolStripSeparator4,
            this.createDescriptions,
            this.deleteDescriptions,
            this.setend});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(957, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::NSMBe4.Properties.Resources.save;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(66, 22);
            this.toolStripButton1.Text = "<save>";
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
            this.deleteAllButton.Size = new System.Drawing.Size(77, 22);
            this.deleteAllButton.Text = "<Del All>";
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
            this.exportButton.Size = new System.Drawing.Size(76, 22);
            this.exportButton.Text = "<Export>";
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click_1);
            // 
            // importButton
            // 
            this.importButton.Image = global::NSMBe4.Properties.Resources.image__plus;
            this.importButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(79, 22);
            this.importButton.Text = "<Import>";
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
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
            this.exportTilesetButton.Size = new System.Drawing.Size(110, 22);
            this.exportTilesetButton.Text = "<export tileset>";
            this.exportTilesetButton.Click += new System.EventHandler(this.exportTilesetButton_Click);
            // 
            // importTilesetButton
            // 
            this.importTilesetButton.Image = ((System.Drawing.Image)(resources.GetObject("importTilesetButton.Image")));
            this.importTilesetButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importTilesetButton.Name = "importTilesetButton";
            this.importTilesetButton.Size = new System.Drawing.Size(113, 22);
            this.importTilesetButton.Text = "<import tileset>";
            this.importTilesetButton.Click += new System.EventHandler(this.importTilesetButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // createDescriptions
            // 
            this.createDescriptions.Image = global::NSMBe4.Properties.Resources.textfield_add;
            this.createDescriptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.createDescriptions.Name = "createDescriptions";
            this.createDescriptions.Size = new System.Drawing.Size(107, 22);
            this.createDescriptions.Text = "<descriptions>";
            this.createDescriptions.Click += new System.EventHandler(this.createDescriptions_Click);
            // 
            // deleteDescriptions
            // 
            this.deleteDescriptions.Image = global::NSMBe4.Properties.Resources.cross_script;
            this.deleteDescriptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteDescriptions.Name = "deleteDescriptions";
            this.deleteDescriptions.Size = new System.Drawing.Size(142, 22);
            this.deleteDescriptions.Text = "<delete descriptions>";
            this.deleteDescriptions.Click += new System.EventHandler(this.deleteDescriptions_Click);
            // 
            // setend
            // 
            this.setend.Image = global::NSMBe4.Properties.Resources.cross_script;
            this.setend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.setend.Name = "setend";
            this.setend.Size = new System.Drawing.Size(81, 22);
            this.setend.Text = "<set end>";
            this.setend.ToolTipText = "makes the selected block the last in the tileset";
            this.setend.Click += new System.EventHandler(this.setend_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "PNG|*.png";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "PNG|*.png";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.Filter = "NSMB Tilesets|*.nmt";
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.Filter = "NSMB Tilesets|*.nmt";
            // 
            // tilemapEditor1
            // 
            this.tilemapEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilemapEditor1.Location = new System.Drawing.Point(3, 3);
            this.tilemapEditor1.Name = "tilemapEditor1";
            this.tilemapEditor1.Size = new System.Drawing.Size(943, 504);
            this.tilemapEditor1.TabIndex = 0;
            // 
            // TilesetEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(957, 561);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TilesetEditor";
            this.Text = "TilesetEditor";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TilesetEditor_FormClosed);
            this.Load += new System.EventHandler(this.TilesetEditor_Load);
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
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton createDescriptions;
        private System.Windows.Forms.ToolStripButton deleteDescriptions;
        private System.Windows.Forms.ToolStripButton setend;
        private System.Windows.Forms.TabPage tabPage3;
        private ImageManager imageManager1;
        private TilemapEditor.TilemapEditor tilemapEditor1;
    }
}