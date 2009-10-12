namespace NSMBe4 {
    partial class LevelEditor {
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.saveLevelButton = new System.Windows.Forms.ToolStripButton();
            this.viewMinimapButton = new System.Windows.Forms.ToolStripButton();
            this.levelConfigButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.editObjectsButton = new System.Windows.Forms.ToolStripButton();
            this.editEntrancesButton = new System.Windows.Forms.ToolStripButton();
            this.editPathsButton = new System.Windows.Forms.ToolStripButton();
            this.editViewsButton = new System.Windows.Forms.ToolStripButton();
            this.editZonesButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.spriteFinder = new System.Windows.Forms.ToolStripButton();
            this.zoomMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.smallBlockOverlaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMap16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllSpritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editTileset = new System.Windows.Forms.ToolStripButton();
            this.PanelContainer = new System.Windows.Forms.Panel();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.levelEditorControl1 = new NSMBe4.LevelEditorControl();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLevelButton,
            this.viewMinimapButton,
            this.levelConfigButton,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.editObjectsButton,
            this.editEntrancesButton,
            this.editPathsButton,
            this.editViewsButton,
            this.editZonesButton,
            this.toolStripSeparator2,
            this.spriteFinder,
            this.zoomMenu,
            this.toolStripSeparator3,
            this.editTileset,
            this.optionsMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(891, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveLevelButton
            // 
            this.saveLevelButton.Image = ((System.Drawing.Image)(resources.GetObject("saveLevelButton.Image")));
            this.saveLevelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveLevelButton.Name = "saveLevelButton";
            this.saveLevelButton.Size = new System.Drawing.Size(81, 22);
            this.saveLevelButton.Text = "Save Level";
            this.saveLevelButton.Click += new System.EventHandler(this.saveLevelButton_Click);
            // 
            // viewMinimapButton
            // 
            this.viewMinimapButton.Image = ((System.Drawing.Image)(resources.GetObject("viewMinimapButton.Image")));
            this.viewMinimapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewMinimapButton.Name = "viewMinimapButton";
            this.viewMinimapButton.Size = new System.Drawing.Size(103, 22);
            this.viewMinimapButton.Text = "View Minimap";
            this.viewMinimapButton.Click += new System.EventHandler(this.viewMinimapButton_Click);
            // 
            // levelConfigButton
            // 
            this.levelConfigButton.Image = ((System.Drawing.Image)(resources.GetObject("levelConfigButton.Image")));
            this.levelConfigButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.levelConfigButton.Name = "levelConfigButton";
            this.levelConfigButton.Size = new System.Drawing.Size(131, 22);
            this.levelConfigButton.Text = "Level Configuration";
            this.levelConfigButton.Click += new System.EventHandler(this.levelConfigButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel1.Text = "Editing:";
            // 
            // editObjectsButton
            // 
            this.editObjectsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editObjectsButton.Image = ((System.Drawing.Image)(resources.GetObject("editObjectsButton.Image")));
            this.editObjectsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editObjectsButton.Name = "editObjectsButton";
            this.editObjectsButton.Size = new System.Drawing.Size(23, 22);
            this.editObjectsButton.Text = "Objects/Sprites";
            this.editObjectsButton.Click += new System.EventHandler(this.editObjectsButton_Click);
            // 
            // editEntrancesButton
            // 
            this.editEntrancesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editEntrancesButton.Image = ((System.Drawing.Image)(resources.GetObject("editEntrancesButton.Image")));
            this.editEntrancesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editEntrancesButton.Name = "editEntrancesButton";
            this.editEntrancesButton.Size = new System.Drawing.Size(23, 22);
            this.editEntrancesButton.Text = "Entrances";
            this.editEntrancesButton.Click += new System.EventHandler(this.editEntrancesButton_Click);
            // 
            // editPathsButton
            // 
            this.editPathsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editPathsButton.Image = ((System.Drawing.Image)(resources.GetObject("editPathsButton.Image")));
            this.editPathsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editPathsButton.Name = "editPathsButton";
            this.editPathsButton.Size = new System.Drawing.Size(23, 22);
            this.editPathsButton.Text = "Paths";
            this.editPathsButton.Click += new System.EventHandler(this.editPathsButton_Click);
            // 
            // editViewsButton
            // 
            this.editViewsButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editViewsButton.Image = ((System.Drawing.Image)(resources.GetObject("editViewsButton.Image")));
            this.editViewsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editViewsButton.Name = "editViewsButton";
            this.editViewsButton.Size = new System.Drawing.Size(23, 22);
            this.editViewsButton.Text = "Views";
            this.editViewsButton.Click += new System.EventHandler(this.editViewsButton_Click);
            // 
            // editZonesButton
            // 
            this.editZonesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editZonesButton.Image = ((System.Drawing.Image)(resources.GetObject("editZonesButton.Image")));
            this.editZonesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editZonesButton.Name = "editZonesButton";
            this.editZonesButton.Size = new System.Drawing.Size(23, 22);
            this.editZonesButton.Text = "Zones";
            this.editZonesButton.Click += new System.EventHandler(this.editZonesButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // spriteFinder
            // 
            this.spriteFinder.Image = global::NSMBe4.Properties.Resources.find;
            this.spriteFinder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.spriteFinder.Name = "spriteFinder";
            this.spriteFinder.Size = new System.Drawing.Size(93, 22);
            this.spriteFinder.Text = "Sprite Finder";
            this.spriteFinder.Click += new System.EventHandler(this.spriteFinder_Click);
            // 
            // zoomMenu
            // 
            this.zoomMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.toolStripMenuItem7,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.zoomMenu.Image = global::NSMBe4.Properties.Resources.zoom;
            this.zoomMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomMenu.Name = "zoomMenu";
            this.zoomMenu.Size = new System.Drawing.Size(68, 22);
            this.zoomMenu.Text = "Zoom";
            this.zoomMenu.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripDropDownButton1_DropDownItemClicked);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Checked = true;
            this.toolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem2.Text = "100 %";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem1.Text = "85 %";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem7.Text = "75 %";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem3.Text = "66 %";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem4.Text = "50 %";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem5.Text = "33 %";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItem6.Text = "25 %";
            // 
            // optionsMenu
            // 
            this.optionsMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallBlockOverlaysToolStripMenuItem,
            this.viewMap16ToolStripMenuItem,
            this.deleteAllObjectsToolStripMenuItem,
            this.deleteAllSpritesToolStripMenuItem});
            this.optionsMenu.Image = ((System.Drawing.Image)(resources.GetObject("optionsMenu.Image")));
            this.optionsMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(29, 22);
            this.optionsMenu.Text = "Options";
            this.optionsMenu.ToolTipText = "Options";
            // 
            // smallBlockOverlaysToolStripMenuItem
            // 
            this.smallBlockOverlaysToolStripMenuItem.Name = "smallBlockOverlaysToolStripMenuItem";
            this.smallBlockOverlaysToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.smallBlockOverlaysToolStripMenuItem.Text = "Use Small Block Overlays";
            this.smallBlockOverlaysToolStripMenuItem.Click += new System.EventHandler(this.smallBlockOverlaysToolStripMenuItem_Click);
            // 
            // viewMap16ToolStripMenuItem
            // 
            this.viewMap16ToolStripMenuItem.Name = "viewMap16ToolStripMenuItem";
            this.viewMap16ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.viewMap16ToolStripMenuItem.Text = "View Map16";
            this.viewMap16ToolStripMenuItem.Click += new System.EventHandler(this.viewMap16ToolStripMenuItem_Click);
            // 
            // deleteAllObjectsToolStripMenuItem
            // 
            this.deleteAllObjectsToolStripMenuItem.Name = "deleteAllObjectsToolStripMenuItem";
            this.deleteAllObjectsToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.deleteAllObjectsToolStripMenuItem.Text = "Delete All Objects";
            this.deleteAllObjectsToolStripMenuItem.Click += new System.EventHandler(this.deleteAllObjectsToolStripMenuItem_Click);
            // 
            // deleteAllSpritesToolStripMenuItem
            // 
            this.deleteAllSpritesToolStripMenuItem.Name = "deleteAllSpritesToolStripMenuItem";
            this.deleteAllSpritesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.deleteAllSpritesToolStripMenuItem.Text = "Delete All Sprites";
            this.deleteAllSpritesToolStripMenuItem.Click += new System.EventHandler(this.deleteAllSpritesToolStripMenuItem_Click);
            // 
            // editTileset
            // 
            this.editTileset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.editTileset.Image = global::NSMBe4.Properties.Resources.gfxeditor;
            this.editTileset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editTileset.Name = "editTileset";
            this.editTileset.Size = new System.Drawing.Size(23, 22);
            this.editTileset.Text = "Edit Tileset";
            this.editTileset.Click += new System.EventHandler(this.editTileset_Click);
            // 
            // PanelContainer
            // 
            this.PanelContainer.ContextMenuStrip = this.contextMenuStrip2;
            this.PanelContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelContainer.Location = new System.Drawing.Point(0, 25);
            this.PanelContainer.Name = "PanelContainer";
            this.PanelContainer.Size = new System.Drawing.Size(250, 551);
            this.PanelContainer.TabIndex = 10;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(61, 4);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(145, 92);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // levelEditorControl1
            // 
            this.levelEditorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.levelEditorControl1.BackColor = System.Drawing.SystemColors.Control;
            this.levelEditorControl1.ContextMenuStrip = this.contextMenuStrip2;
            this.levelEditorControl1.Location = new System.Drawing.Point(256, 25);
            this.levelEditorControl1.Name = "levelEditorControl1";
            this.levelEditorControl1.Size = new System.Drawing.Size(635, 551);
            this.levelEditorControl1.TabIndex = 3;
            this.levelEditorControl1.SetDirtyFlag += new NSMBe4.LevelEditorControl.SetDirtyFlagDelegate(this.levelEditorControl1_SetDirtyFlag);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 576);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.Controls.Add(this.PanelContainer);
            this.Controls.Add(this.levelEditorControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "LevelEditor";
            this.Text = "NSMB Editor 4";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LevelEditor_FormClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelEditor_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LevelEditorControl levelEditorControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton saveLevelButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton optionsMenu;
        private System.Windows.Forms.ToolStripMenuItem viewMap16ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton viewMinimapButton;
        private System.Windows.Forms.ToolStripMenuItem smallBlockOverlaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton editObjectsButton;
        private System.Windows.Forms.ToolStripButton editEntrancesButton;
        private System.Windows.Forms.ToolStripButton levelConfigButton;
        private System.Windows.Forms.ToolStripMenuItem deleteAllObjectsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteAllSpritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton editPathsButton;
        private System.Windows.Forms.Panel PanelContainer;
        private System.Windows.Forms.ToolStripButton editViewsButton;
        private System.Windows.Forms.ToolStripButton spriteFinder;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripDropDownButton zoomMenu;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripButton editZonesButton;
        private System.Windows.Forms.ToolStripButton editTileset;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

