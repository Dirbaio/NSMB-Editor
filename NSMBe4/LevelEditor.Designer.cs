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
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsMenu = new System.Windows.Forms.ToolStripDropDownButton();
            this.smallBlockOverlaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewMap16ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllObjectsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteAllSpritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PanelContainer = new System.Windows.Forms.Panel();
            this.levelEditorControl1 = new NSMBe4.LevelEditorControl();
            this.toolStrip1.SuspendLayout();
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
            this.toolStripSeparator2,
            this.optionsMenu});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(853, 25);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // saveLevelButton
            // 
            this.saveLevelButton.Image = ((System.Drawing.Image)(resources.GetObject("saveLevelButton.Image")));
            this.saveLevelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveLevelButton.Name = "saveLevelButton";
            this.saveLevelButton.Size = new System.Drawing.Size(79, 22);
            this.saveLevelButton.Text = "Save Level";
            this.saveLevelButton.Click += new System.EventHandler(this.saveLevelButton_Click);
            // 
            // viewMinimapButton
            // 
            this.viewMinimapButton.Image = ((System.Drawing.Image)(resources.GetObject("viewMinimapButton.Image")));
            this.viewMinimapButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.viewMinimapButton.Name = "viewMinimapButton";
            this.viewMinimapButton.Size = new System.Drawing.Size(90, 22);
            this.viewMinimapButton.Text = "View Minimap";
            this.viewMinimapButton.Click += new System.EventHandler(this.viewMinimapButton_Click);
            // 
            // levelConfigButton
            // 
            this.levelConfigButton.Image = ((System.Drawing.Image)(resources.GetObject("levelConfigButton.Image")));
            this.levelConfigButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.levelConfigButton.Name = "levelConfigButton";
            this.levelConfigButton.Size = new System.Drawing.Size(120, 22);
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
            this.toolStripLabel1.Size = new System.Drawing.Size(43, 22);
            this.toolStripLabel1.Text = "Editing:";
            // 
            // editObjectsButton
            // 
            this.editObjectsButton.Image = ((System.Drawing.Image)(resources.GetObject("editObjectsButton.Image")));
            this.editObjectsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editObjectsButton.Name = "editObjectsButton";
            this.editObjectsButton.Size = new System.Drawing.Size(101, 22);
            this.editObjectsButton.Text = "Objects/Sprites";
            this.editObjectsButton.Click += new System.EventHandler(this.editObjectsButton_Click);
            // 
            // editEntrancesButton
            // 
            this.editEntrancesButton.Image = ((System.Drawing.Image)(resources.GetObject("editEntrancesButton.Image")));
            this.editEntrancesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editEntrancesButton.Name = "editEntrancesButton";
            this.editEntrancesButton.Size = new System.Drawing.Size(75, 22);
            this.editEntrancesButton.Text = "Entrances";
            this.editEntrancesButton.Click += new System.EventHandler(this.editEntrancesButton_Click);
            // 
            // editPathsButton
            // 
            this.editPathsButton.Image = ((System.Drawing.Image)(resources.GetObject("editPathsButton.Image")));
            this.editPathsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editPathsButton.Name = "editPathsButton";
            this.editPathsButton.Size = new System.Drawing.Size(54, 22);
            this.editPathsButton.Text = "Paths";
            this.editPathsButton.Click += new System.EventHandler(this.editPathsButton_Click);
            // 
            // editViewsButton
            // 
            this.editViewsButton.Image = ((System.Drawing.Image)(resources.GetObject("editViewsButton.Image")));
            this.editViewsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.editViewsButton.Name = "editViewsButton";
            this.editViewsButton.Size = new System.Drawing.Size(54, 22);
            this.editViewsButton.Text = "Views";
            this.editViewsButton.Click += new System.EventHandler(this.editViewsButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // optionsMenu
            // 
            this.optionsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallBlockOverlaysToolStripMenuItem,
            this.viewMap16ToolStripMenuItem,
            this.deleteAllObjectsToolStripMenuItem,
            this.deleteAllSpritesToolStripMenuItem});
            this.optionsMenu.Image = ((System.Drawing.Image)(resources.GetObject("optionsMenu.Image")));
            this.optionsMenu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.optionsMenu.Name = "optionsMenu";
            this.optionsMenu.Size = new System.Drawing.Size(73, 22);
            this.optionsMenu.Text = "Options";
            this.optionsMenu.ToolTipText = "Options";
            // 
            // smallBlockOverlaysToolStripMenuItem
            // 
            this.smallBlockOverlaysToolStripMenuItem.Name = "smallBlockOverlaysToolStripMenuItem";
            this.smallBlockOverlaysToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.smallBlockOverlaysToolStripMenuItem.Text = "Use Small Block Overlays";
            this.smallBlockOverlaysToolStripMenuItem.Click += new System.EventHandler(this.smallBlockOverlaysToolStripMenuItem_Click);
            // 
            // viewMap16ToolStripMenuItem
            // 
            this.viewMap16ToolStripMenuItem.Name = "viewMap16ToolStripMenuItem";
            this.viewMap16ToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.viewMap16ToolStripMenuItem.Text = "View Map16";
            this.viewMap16ToolStripMenuItem.Click += new System.EventHandler(this.viewMap16ToolStripMenuItem_Click);
            // 
            // deleteAllObjectsToolStripMenuItem
            // 
            this.deleteAllObjectsToolStripMenuItem.Name = "deleteAllObjectsToolStripMenuItem";
            this.deleteAllObjectsToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.deleteAllObjectsToolStripMenuItem.Text = "Delete All Objects";
            this.deleteAllObjectsToolStripMenuItem.Click += new System.EventHandler(this.deleteAllObjectsToolStripMenuItem_Click);
            // 
            // deleteAllSpritesToolStripMenuItem
            // 
            this.deleteAllSpritesToolStripMenuItem.Name = "deleteAllSpritesToolStripMenuItem";
            this.deleteAllSpritesToolStripMenuItem.Size = new System.Drawing.Size(203, 22);
            this.deleteAllSpritesToolStripMenuItem.Text = "Delete All Sprites";
            this.deleteAllSpritesToolStripMenuItem.Click += new System.EventHandler(this.deleteAllSpritesToolStripMenuItem_Click);
            // 
            // PanelContainer
            // 
            this.PanelContainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.PanelContainer.Location = new System.Drawing.Point(0, 25);
            this.PanelContainer.Name = "PanelContainer";
            this.PanelContainer.Size = new System.Drawing.Size(250, 518);
            this.PanelContainer.TabIndex = 10;
            // 
            // levelEditorControl1
            // 
            this.levelEditorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.levelEditorControl1.BackColor = System.Drawing.SystemColors.Control;
            this.levelEditorControl1.Location = new System.Drawing.Point(256, 25);
            this.levelEditorControl1.Name = "levelEditorControl1";
            this.levelEditorControl1.Size = new System.Drawing.Size(597, 518);
            this.levelEditorControl1.TabIndex = 3;
            this.levelEditorControl1.SetDirtyFlag += new NSMBe4.LevelEditorControl.SetDirtyFlagDelegate(this.levelEditorControl1_SetDirtyFlag);
            // 
            // LevelEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 543);
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
    }
}

