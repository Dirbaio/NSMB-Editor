
using NSMBe4.DSFileSystem;
namespace NSMBe4 {
    partial class LevelChooser {
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.openClipboard = new System.Windows.Forms.Button();
            this.exportClipboard = new System.Windows.Forms.Button();
            this.importClipboard = new System.Windows.Forms.Button();
            this.openLevel = new System.Windows.Forms.Button();
            this.hexEditLevelButton = new System.Windows.Forms.Button();
            this.exportLevelButton = new System.Windows.Forms.Button();
            this.importLevelButton = new System.Windows.Forms.Button();
            this.editLevelButton = new System.Windows.Forms.Button();
            this.levelTreeView = new System.Windows.Forms.TreeView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tilesetList1 = new NSMBe4.TilesetList();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.backgroundList1 = new NSMBe4.BackgroundList();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.filesystemBrowser1 = new NSMBe4.DSFileSystem.FilesystemBrowser();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.grpDLPMode = new System.Windows.Forms.GroupBox();
            this.lblDLPMode2 = new System.Windows.Forms.Label();
            this.lblDLPMode1 = new System.Windows.Forms.Label();
            this.dlpCheckBox = new System.Windows.Forms.CheckBox();
            this.musicSlotsGrp = new System.Windows.Forms.GroupBox();
            this.renameBtn = new System.Windows.Forms.Button();
            this.musicList = new System.Windows.Forms.ListBox();
            this.patchesGroupbox = new System.Windows.Forms.GroupBox();
            this.patchExport = new System.Windows.Forms.Button();
            this.patchImport = new System.Windows.Forms.Button();
            this.nsmbToolsGroupbox = new System.Windows.Forms.GroupBox();
            this.mpPatch = new System.Windows.Forms.Button();
            this.mpPatch2 = new System.Windows.Forms.Button();
            this.dataFinderButton = new System.Windows.Forms.Button();
            this.asmToolsGroupbox = new System.Windows.Forms.GroupBox();
            this.makeclean = new System.Windows.Forms.Button();
            this.makeinsert = new System.Windows.Forms.Button();
            this.decompArm9Bin = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.deleteBackups = new System.Windows.Forms.Button();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.autoBackupTime = new System.Windows.Forms.NumericUpDown();
            this.lblEvery = new System.Windows.Forms.Label();
            this.chkAutoBackup = new System.Windows.Forms.CheckBox();
            this.useMDI = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.languageListBox = new System.Windows.Forms.ListBox();
            this.updateSpriteDataButton = new System.Windows.Forms.Button();
            this.changeLanguageButton = new System.Windows.Forms.Button();
            this.dumpMapButton = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.lnkNSMBHD = new System.Windows.Forms.LinkLabel();
            this.lnkGitHub = new System.Windows.Forms.LinkLabel();
            this.lblLinksHeader = new System.Windows.Forms.Label();
            this.lblCredits = new System.Windows.Forms.Label();
            this.lblCreditsHeader = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.importLevelDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportLevelDialog = new System.Windows.Forms.SaveFileDialog();
            this.savePatchDialog = new System.Windows.Forms.SaveFileDialog();
            this.openPatchDialog = new System.Windows.Forms.OpenFileDialog();
            this.openTextFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveTextFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.openROMDialog = new System.Windows.Forms.OpenFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.grpDLPMode.SuspendLayout();
            this.musicSlotsGrp.SuspendLayout();
            this.patchesGroupbox.SuspendLayout();
            this.nsmbToolsGroupbox.SuspendLayout();
            this.asmToolsGroupbox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoBackupTime)).BeginInit();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(550, 514);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.openClipboard);
            this.tabPage2.Controls.Add(this.exportClipboard);
            this.tabPage2.Controls.Add(this.importClipboard);
            this.tabPage2.Controls.Add(this.openLevel);
            this.tabPage2.Controls.Add(this.hexEditLevelButton);
            this.tabPage2.Controls.Add(this.exportLevelButton);
            this.tabPage2.Controls.Add(this.importLevelButton);
            this.tabPage2.Controls.Add(this.editLevelButton);
            this.tabPage2.Controls.Add(this.levelTreeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(542, 488);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "<Level Listing>";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // openClipboard
            // 
            this.openClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openClipboard.Location = new System.Drawing.Point(220, 459);
            this.openClipboard.Name = "openClipboard";
            this.openClipboard.Size = new System.Drawing.Size(118, 23);
            this.openClipboard.TabIndex = 8;
            this.openClipboard.Text = "<OpenClipboard>";
            this.openClipboard.UseVisualStyleBackColor = true;
            this.openClipboard.Click += new System.EventHandler(this.openClipboard_Click);
            // 
            // exportClipboard
            // 
            this.exportClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportClipboard.Enabled = false;
            this.exportClipboard.Location = new System.Drawing.Point(107, 459);
            this.exportClipboard.Name = "exportClipboard";
            this.exportClipboard.Size = new System.Drawing.Size(106, 23);
            this.exportClipboard.TabIndex = 7;
            this.exportClipboard.Text = "<ExportToClipboard>";
            this.exportClipboard.UseVisualStyleBackColor = true;
            this.exportClipboard.Click += new System.EventHandler(this.exportClipboard_Click);
            // 
            // importClipboard
            // 
            this.importClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importClipboard.Enabled = false;
            this.importClipboard.Location = new System.Drawing.Point(6, 459);
            this.importClipboard.Name = "importClipboard";
            this.importClipboard.Size = new System.Drawing.Size(94, 23);
            this.importClipboard.TabIndex = 6;
            this.importClipboard.Text = "<ImportClipboard>";
            this.importClipboard.UseVisualStyleBackColor = true;
            this.importClipboard.Click += new System.EventHandler(this.importClipboard_Click);
            // 
            // openLevel
            // 
            this.openLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openLevel.Location = new System.Drawing.Point(219, 429);
            this.openLevel.Name = "openLevel";
            this.openLevel.Size = new System.Drawing.Size(119, 23);
            this.openLevel.TabIndex = 5;
            this.openLevel.Text = "<OpenExportedLevel>";
            this.openLevel.UseVisualStyleBackColor = true;
            this.openLevel.Click += new System.EventHandler(this.openLevel_Click);
            // 
            // hexEditLevelButton
            // 
            this.hexEditLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEditLevelButton.Enabled = false;
            this.hexEditLevelButton.Location = new System.Drawing.Point(452, 458);
            this.hexEditLevelButton.Name = "hexEditLevelButton";
            this.hexEditLevelButton.Size = new System.Drawing.Size(75, 23);
            this.hexEditLevelButton.TabIndex = 4;
            this.hexEditLevelButton.Text = "<hexEditLevelButton>";
            this.hexEditLevelButton.UseVisualStyleBackColor = true;
            this.hexEditLevelButton.Click += new System.EventHandler(this.hexEditLevelButton_Click);
            // 
            // exportLevelButton
            // 
            this.exportLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportLevelButton.Enabled = false;
            this.exportLevelButton.Location = new System.Drawing.Point(106, 429);
            this.exportLevelButton.Name = "exportLevelButton";
            this.exportLevelButton.Size = new System.Drawing.Size(107, 23);
            this.exportLevelButton.TabIndex = 3;
            this.exportLevelButton.Text = "<exportLevelButton>";
            this.exportLevelButton.UseVisualStyleBackColor = true;
            this.exportLevelButton.Click += new System.EventHandler(this.exportLevelButton_Click);
            // 
            // importLevelButton
            // 
            this.importLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importLevelButton.Enabled = false;
            this.importLevelButton.Location = new System.Drawing.Point(5, 429);
            this.importLevelButton.Name = "importLevelButton";
            this.importLevelButton.Size = new System.Drawing.Size(95, 23);
            this.importLevelButton.TabIndex = 2;
            this.importLevelButton.Text = "<importLevelButton>";
            this.importLevelButton.UseVisualStyleBackColor = true;
            this.importLevelButton.Click += new System.EventHandler(this.importLevelButton_Click);
            // 
            // editLevelButton
            // 
            this.editLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editLevelButton.Enabled = false;
            this.editLevelButton.Location = new System.Drawing.Point(452, 429);
            this.editLevelButton.Name = "editLevelButton";
            this.editLevelButton.Size = new System.Drawing.Size(75, 23);
            this.editLevelButton.TabIndex = 1;
            this.editLevelButton.Text = "<editLevelButton>";
            this.editLevelButton.UseVisualStyleBackColor = true;
            this.editLevelButton.Click += new System.EventHandler(this.editLevelButton_Click);
            // 
            // levelTreeView
            // 
            this.levelTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.levelTreeView.Location = new System.Drawing.Point(6, 6);
            this.levelTreeView.Name = "levelTreeView";
            this.levelTreeView.Size = new System.Drawing.Size(522, 417);
            this.levelTreeView.TabIndex = 0;
            this.levelTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.levelTreeView_AfterSelect);
            this.levelTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.levelTreeView_NodeMouseDoubleClick);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.tilesetList1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(542, 488);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "<Tilesets>";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tilesetList1
            // 
            this.tilesetList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetList1.Location = new System.Drawing.Point(3, 3);
            this.tilesetList1.Name = "tilesetList1";
            this.tilesetList1.Size = new System.Drawing.Size(536, 482);
            this.tilesetList1.TabIndex = 0;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.backgroundList1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(542, 488);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "<Backgrounds>";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // backgroundList1
            // 
            this.backgroundList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundList1.Location = new System.Drawing.Point(0, 0);
            this.backgroundList1.Name = "backgroundList1";
            this.backgroundList1.Size = new System.Drawing.Size(542, 488);
            this.backgroundList1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.filesystemBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(542, 488);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<File Browser>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // filesystemBrowser1
            // 
            this.filesystemBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesystemBrowser1.Location = new System.Drawing.Point(3, 3);
            this.filesystemBrowser1.Name = "filesystemBrowser1";
            this.filesystemBrowser1.Size = new System.Drawing.Size(536, 482);
            this.filesystemBrowser1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.grpDLPMode);
            this.tabPage3.Controls.Add(this.musicSlotsGrp);
            this.tabPage3.Controls.Add(this.patchesGroupbox);
            this.tabPage3.Controls.Add(this.nsmbToolsGroupbox);
            this.tabPage3.Controls.Add(this.asmToolsGroupbox);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.dumpMapButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(542, 488);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "<Tools/Options>";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // grpDLPMode
            // 
            this.grpDLPMode.Controls.Add(this.lblDLPMode2);
            this.grpDLPMode.Controls.Add(this.lblDLPMode1);
            this.grpDLPMode.Controls.Add(this.dlpCheckBox);
            this.grpDLPMode.Location = new System.Drawing.Point(6, 365);
            this.grpDLPMode.Name = "grpDLPMode";
            this.grpDLPMode.Size = new System.Drawing.Size(246, 117);
            this.grpDLPMode.TabIndex = 11;
            this.grpDLPMode.TabStop = false;
            this.grpDLPMode.Text = "<DLP mode>";
            // 
            // lblDLPMode2
            // 
            this.lblDLPMode2.Location = new System.Drawing.Point(6, 82);
            this.lblDLPMode2.Name = "lblDLPMode2";
            this.lblDLPMode2.Size = new System.Drawing.Size(231, 31);
            this.lblDLPMode2.TabIndex = 11;
            this.lblDLPMode2.Text = "<You do NOT need to enable this if you\'re using firmware.nds or FlashMe.>";
            // 
            // lblDLPMode1
            // 
            this.lblDLPMode1.Location = new System.Drawing.Point(6, 39);
            this.lblDLPMode1.Name = "lblDLPMode1";
            this.lblDLPMode1.Size = new System.Drawing.Size(234, 43);
            this.lblDLPMode1.TabIndex = 11;
            this.lblDLPMode1.Text = "<This will prevent some values in the header from being updated. The ROM will wor" +
    "k over DLP but may not work on some flashcards.>";
            // 
            // dlpCheckBox
            // 
            this.dlpCheckBox.AutoSize = true;
            this.dlpCheckBox.Location = new System.Drawing.Point(6, 19);
            this.dlpCheckBox.Name = "dlpCheckBox";
            this.dlpCheckBox.Size = new System.Drawing.Size(210, 17);
            this.dlpCheckBox.TabIndex = 10;
            this.dlpCheckBox.Text = "<Enable Download Play-friendly mode>";
            this.dlpCheckBox.UseVisualStyleBackColor = true;
            this.dlpCheckBox.CheckedChanged += new System.EventHandler(this.dlpCheckBox_CheckedChanged);
            // 
            // musicSlotsGrp
            // 
            this.musicSlotsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.musicSlotsGrp.Controls.Add(this.renameBtn);
            this.musicSlotsGrp.Controls.Add(this.musicList);
            this.musicSlotsGrp.Location = new System.Drawing.Point(258, 236);
            this.musicSlotsGrp.Name = "musicSlotsGrp";
            this.musicSlotsGrp.Size = new System.Drawing.Size(278, 246);
            this.musicSlotsGrp.TabIndex = 8;
            this.musicSlotsGrp.TabStop = false;
            this.musicSlotsGrp.Text = "<Music Slots>";
            // 
            // renameBtn
            // 
            this.renameBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.renameBtn.Location = new System.Drawing.Point(197, 217);
            this.renameBtn.Name = "renameBtn";
            this.renameBtn.Size = new System.Drawing.Size(75, 23);
            this.renameBtn.TabIndex = 10;
            this.renameBtn.Text = "<Rename>";
            this.renameBtn.UseVisualStyleBackColor = true;
            this.renameBtn.Click += new System.EventHandler(this.renameBtn_Click);
            // 
            // musicList
            // 
            this.musicList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.musicList.FormattingEnabled = true;
            this.musicList.Location = new System.Drawing.Point(6, 20);
            this.musicList.Name = "musicList";
            this.musicList.Size = new System.Drawing.Size(266, 186);
            this.musicList.TabIndex = 9;
            // 
            // patchesGroupbox
            // 
            this.patchesGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patchesGroupbox.Controls.Add(this.patchExport);
            this.patchesGroupbox.Controls.Add(this.patchImport);
            this.patchesGroupbox.Location = new System.Drawing.Point(258, 6);
            this.patchesGroupbox.Name = "patchesGroupbox";
            this.patchesGroupbox.Size = new System.Drawing.Size(278, 80);
            this.patchesGroupbox.TabIndex = 7;
            this.patchesGroupbox.TabStop = false;
            this.patchesGroupbox.Text = "<Patches>";
            // 
            // patchExport
            // 
            this.patchExport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patchExport.Location = new System.Drawing.Point(6, 19);
            this.patchExport.Name = "patchExport";
            this.patchExport.Size = new System.Drawing.Size(266, 23);
            this.patchExport.TabIndex = 3;
            this.patchExport.Text = "<patchExport>";
            this.patchExport.UseVisualStyleBackColor = true;
            this.patchExport.Click += new System.EventHandler(this.patchExport_Click);
            // 
            // patchImport
            // 
            this.patchImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.patchImport.Location = new System.Drawing.Point(6, 48);
            this.patchImport.Name = "patchImport";
            this.patchImport.Size = new System.Drawing.Size(266, 23);
            this.patchImport.TabIndex = 3;
            this.patchImport.Text = "<patchImport>";
            this.patchImport.UseVisualStyleBackColor = true;
            this.patchImport.Click += new System.EventHandler(this.patchImport_Click);
            // 
            // nsmbToolsGroupbox
            // 
            this.nsmbToolsGroupbox.Controls.Add(this.mpPatch);
            this.nsmbToolsGroupbox.Controls.Add(this.mpPatch2);
            this.nsmbToolsGroupbox.Controls.Add(this.dataFinderButton);
            this.nsmbToolsGroupbox.Location = new System.Drawing.Point(6, 6);
            this.nsmbToolsGroupbox.Name = "nsmbToolsGroupbox";
            this.nsmbToolsGroupbox.Size = new System.Drawing.Size(246, 109);
            this.nsmbToolsGroupbox.TabIndex = 6;
            this.nsmbToolsGroupbox.TabStop = false;
            this.nsmbToolsGroupbox.Text = "<NSMB Tools>";
            // 
            // mpPatch
            // 
            this.mpPatch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mpPatch.Location = new System.Drawing.Point(6, 19);
            this.mpPatch.Name = "mpPatch";
            this.mpPatch.Size = new System.Drawing.Size(234, 23);
            this.mpPatch.TabIndex = 3;
            this.mpPatch.Text = "<mpPatch>";
            this.mpPatch.UseVisualStyleBackColor = true;
            this.mpPatch.Click += new System.EventHandler(this.mpPatch_Click);
            // 
            // mpPatch2
            // 
            this.mpPatch2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mpPatch2.Location = new System.Drawing.Point(6, 48);
            this.mpPatch2.Name = "mpPatch2";
            this.mpPatch2.Size = new System.Drawing.Size(234, 23);
            this.mpPatch2.TabIndex = 5;
            this.mpPatch2.Text = "<mpPatch2>";
            this.mpPatch2.UseVisualStyleBackColor = true;
            this.mpPatch2.Click += new System.EventHandler(this.mpPatch2_Click);
            // 
            // dataFinderButton
            // 
            this.dataFinderButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataFinderButton.Location = new System.Drawing.Point(6, 77);
            this.dataFinderButton.Name = "dataFinderButton";
            this.dataFinderButton.Size = new System.Drawing.Size(234, 23);
            this.dataFinderButton.TabIndex = 3;
            this.dataFinderButton.Text = "<dataFinderButton>";
            this.dataFinderButton.UseVisualStyleBackColor = true;
            this.dataFinderButton.Click += new System.EventHandler(this.dataFinderButton_Click);
            // 
            // asmToolsGroupbox
            // 
            this.asmToolsGroupbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.asmToolsGroupbox.Controls.Add(this.makeclean);
            this.asmToolsGroupbox.Controls.Add(this.makeinsert);
            this.asmToolsGroupbox.Controls.Add(this.decompArm9Bin);
            this.asmToolsGroupbox.Location = new System.Drawing.Point(258, 92);
            this.asmToolsGroupbox.Name = "asmToolsGroupbox";
            this.asmToolsGroupbox.Size = new System.Drawing.Size(278, 109);
            this.asmToolsGroupbox.TabIndex = 4;
            this.asmToolsGroupbox.TabStop = false;
            this.asmToolsGroupbox.Text = "<ASM Tools>";
            // 
            // makeclean
            // 
            this.makeclean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.makeclean.Location = new System.Drawing.Point(6, 77);
            this.makeclean.Name = "makeclean";
            this.makeclean.Size = new System.Drawing.Size(266, 23);
            this.makeclean.TabIndex = 3;
            this.makeclean.Text = "<Run \'make clean\'>";
            this.makeclean.UseVisualStyleBackColor = true;
            this.makeclean.Click += new System.EventHandler(this.makeclean_Click);
            // 
            // makeinsert
            // 
            this.makeinsert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.makeinsert.Location = new System.Drawing.Point(6, 48);
            this.makeinsert.Name = "makeinsert";
            this.makeinsert.Size = new System.Drawing.Size(266, 23);
            this.makeinsert.TabIndex = 3;
            this.makeinsert.Text = "<Run \'make\' and insert>";
            this.makeinsert.UseVisualStyleBackColor = true;
            this.makeinsert.Click += new System.EventHandler(this.makeinsert_Click);
            // 
            // decompArm9Bin
            // 
            this.decompArm9Bin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.decompArm9Bin.Location = new System.Drawing.Point(6, 19);
            this.decompArm9Bin.Name = "decompArm9Bin";
            this.decompArm9Bin.Size = new System.Drawing.Size(266, 23);
            this.decompArm9Bin.TabIndex = 3;
            this.decompArm9Bin.Text = "<Decompress ARM9 binary>";
            this.decompArm9Bin.UseVisualStyleBackColor = true;
            this.decompArm9Bin.Click += new System.EventHandler(this.decompArm9Bin_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.deleteBackups);
            this.groupBox1.Controls.Add(this.lblMinutes);
            this.groupBox1.Controls.Add(this.autoBackupTime);
            this.groupBox1.Controls.Add(this.lblEvery);
            this.groupBox1.Controls.Add(this.chkAutoBackup);
            this.groupBox1.Controls.Add(this.useMDI);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.languageListBox);
            this.groupBox1.Controls.Add(this.updateSpriteDataButton);
            this.groupBox1.Controls.Add(this.changeLanguageButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 121);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 238);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // deleteBackups
            // 
            this.deleteBackups.Location = new System.Drawing.Point(26, 204);
            this.deleteBackups.Name = "deleteBackups";
            this.deleteBackups.Size = new System.Drawing.Size(120, 23);
            this.deleteBackups.TabIndex = 13;
            this.deleteBackups.Text = "<Delete all backups>";
            this.deleteBackups.UseVisualStyleBackColor = true;
            this.deleteBackups.Click += new System.EventHandler(this.deleteBackups_Click);
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(118, 180);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(62, 13);
            this.lblMinutes.TabIndex = 12;
            this.lblMinutes.Text = "<Minute(s)>";
            // 
            // autoBackupTime
            // 
            this.autoBackupTime.Location = new System.Drawing.Point(63, 178);
            this.autoBackupTime.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.autoBackupTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.autoBackupTime.Name = "autoBackupTime";
            this.autoBackupTime.Size = new System.Drawing.Size(46, 20);
            this.autoBackupTime.TabIndex = 11;
            this.autoBackupTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.autoBackupTime.ValueChanged += new System.EventHandler(this.autoBackupTime_ValueChanged);
            // 
            // lblEvery
            // 
            this.lblEvery.AutoSize = true;
            this.lblEvery.Location = new System.Drawing.Point(23, 180);
            this.lblEvery.Name = "lblEvery";
            this.lblEvery.Size = new System.Drawing.Size(46, 13);
            this.lblEvery.TabIndex = 10;
            this.lblEvery.Text = "<Every>";
            // 
            // chkAutoBackup
            // 
            this.chkAutoBackup.AutoSize = true;
            this.chkAutoBackup.Location = new System.Drawing.Point(6, 156);
            this.chkAutoBackup.Name = "chkAutoBackup";
            this.chkAutoBackup.Size = new System.Drawing.Size(129, 17);
            this.chkAutoBackup.TabIndex = 9;
            this.chkAutoBackup.Text = "<Auto-backup levels>";
            this.chkAutoBackup.UseVisualStyleBackColor = true;
            this.chkAutoBackup.CheckedChanged += new System.EventHandler(this.autoBackupTime_ValueChanged);
            // 
            // useMDI
            // 
            this.useMDI.AutoSize = true;
            this.useMDI.Location = new System.Drawing.Point(6, 107);
            this.useMDI.Name = "useMDI";
            this.useMDI.Size = new System.Drawing.Size(58, 17);
            this.useMDI.TabIndex = 6;
            this.useMDI.Text = "<MDI>";
            this.useMDI.UseVisualStyleBackColor = true;
            this.useMDI.CheckedChanged += new System.EventHandler(this.useMDI_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "<label2>";
            // 
            // languageListBox
            // 
            this.languageListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.languageListBox.FormattingEnabled = true;
            this.languageListBox.Location = new System.Drawing.Point(6, 32);
            this.languageListBox.Name = "languageListBox";
            this.languageListBox.Size = new System.Drawing.Size(153, 69);
            this.languageListBox.TabIndex = 1;
            // 
            // updateSpriteDataButton
            // 
            this.updateSpriteDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.updateSpriteDataButton.Location = new System.Drawing.Point(6, 127);
            this.updateSpriteDataButton.Name = "updateSpriteDataButton";
            this.updateSpriteDataButton.Size = new System.Drawing.Size(153, 23);
            this.updateSpriteDataButton.TabIndex = 3;
            this.updateSpriteDataButton.Text = "<UpdateSpriteData>";
            this.updateSpriteDataButton.UseVisualStyleBackColor = true;
            this.updateSpriteDataButton.Click += new System.EventHandler(this.updateSpriteDataButton_Click);
            // 
            // changeLanguageButton
            // 
            this.changeLanguageButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.changeLanguageButton.Location = new System.Drawing.Point(165, 78);
            this.changeLanguageButton.Name = "changeLanguageButton";
            this.changeLanguageButton.Size = new System.Drawing.Size(75, 23);
            this.changeLanguageButton.TabIndex = 2;
            this.changeLanguageButton.Text = "<changeLanguageButton>";
            this.changeLanguageButton.UseVisualStyleBackColor = true;
            this.changeLanguageButton.Click += new System.EventHandler(this.changeLanguageButton_Click);
            // 
            // dumpMapButton
            // 
            this.dumpMapButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpMapButton.Location = new System.Drawing.Point(258, 207);
            this.dumpMapButton.Name = "dumpMapButton";
            this.dumpMapButton.Size = new System.Drawing.Size(278, 23);
            this.dumpMapButton.TabIndex = 3;
            this.dumpMapButton.Text = "<Dump ROM map>";
            this.dumpMapButton.UseVisualStyleBackColor = true;
            this.dumpMapButton.Click += new System.EventHandler(this.dumpMapButton_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.lnkNSMBHD);
            this.tabPage4.Controls.Add(this.lnkGitHub);
            this.tabPage4.Controls.Add(this.lblLinksHeader);
            this.tabPage4.Controls.Add(this.lblCredits);
            this.tabPage4.Controls.Add(this.lblCreditsHeader);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(542, 488);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "<About>";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // lnkNSMBHD
            // 
            this.lnkNSMBHD.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lnkNSMBHD.AutoSize = true;
            this.lnkNSMBHD.Location = new System.Drawing.Point(211, 258);
            this.lnkNSMBHD.Name = "lnkNSMBHD";
            this.lnkNSMBHD.Size = new System.Drawing.Size(120, 13);
            this.lnkNSMBHD.TabIndex = 9;
            this.lnkNSMBHD.TabStop = true;
            this.lnkNSMBHD.Text = "NSMB Hacking Domain";
            this.toolTip1.SetToolTip(this.lnkNSMBHD, "http://nsmbhd.net/");
            this.lnkNSMBHD.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // lnkGitHub
            // 
            this.lnkGitHub.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lnkGitHub.AutoSize = true;
            this.lnkGitHub.Location = new System.Drawing.Point(224, 245);
            this.lnkGitHub.Name = "lnkGitHub";
            this.lnkGitHub.Size = new System.Drawing.Size(95, 13);
            this.lnkGitHub.TabIndex = 8;
            this.lnkGitHub.TabStop = true;
            this.lnkGitHub.Text = "NSMBe on GitHub";
            this.toolTip1.SetToolTip(this.lnkGitHub, "http://github.com/Dirbaio/NSMB-Editor");
            this.lnkGitHub.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // lblLinksHeader
            // 
            this.lblLinksHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblLinksHeader.AutoSize = true;
            this.lblLinksHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLinksHeader.Location = new System.Drawing.Point(246, 225);
            this.lblLinksHeader.Name = "lblLinksHeader";
            this.lblLinksHeader.Size = new System.Drawing.Size(51, 20);
            this.lblLinksHeader.TabIndex = 7;
            this.lblLinksHeader.Text = "Links";
            // 
            // lblCredits
            // 
            this.lblCredits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCredits.Location = new System.Drawing.Point(6, 164);
            this.lblCredits.Name = "lblCredits";
            this.lblCredits.Size = new System.Drawing.Size(530, 61);
            this.lblCredits.TabIndex = 3;
            this.lblCredits.Text = "Treeki - Original Developer\r\nDirbaio - Current Developer\r\nPiranhaplant - Develope" +
    "r\r\nAnd everyone else who\'s helped us make the editor and community awesome!";
            this.lblCredits.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCreditsHeader
            // 
            this.lblCreditsHeader.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCreditsHeader.AutoSize = true;
            this.lblCreditsHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCreditsHeader.Location = new System.Drawing.Point(238, 144);
            this.lblCreditsHeader.Name = "lblCreditsHeader";
            this.lblCreditsHeader.Size = new System.Drawing.Size(66, 20);
            this.lblCreditsHeader.TabIndex = 2;
            this.lblCreditsHeader.Text = "Credits";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(530, 75);
            this.label3.TabIndex = 1;
            this.label3.Text = "Version 5.2 SVN";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(469, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Super Mario Bros. Editor";
            // 
            // LevelChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 538);
            this.Controls.Add(this.tabControl1);
            this.Name = "LevelChooser";
            this.Text = "<_TITLE>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelChooser_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LevelChooser_FormClosed);
            this.Load += new System.EventHandler(this.LevelChooser_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.grpDLPMode.ResumeLayout(false);
            this.grpDLPMode.PerformLayout();
            this.musicSlotsGrp.ResumeLayout(false);
            this.patchesGroupbox.ResumeLayout(false);
            this.nsmbToolsGroupbox.ResumeLayout(false);
            this.asmToolsGroupbox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.autoBackupTime)).EndInit();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button exportLevelButton;
        private System.Windows.Forms.Button importLevelButton;
        private System.Windows.Forms.Button editLevelButton;
        private System.Windows.Forms.TreeView levelTreeView;
        private System.Windows.Forms.OpenFileDialog importLevelDialog;
        private System.Windows.Forms.SaveFileDialog exportLevelDialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button changeLanguageButton;
        private System.Windows.Forms.ListBox languageListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dataFinderButton;
        private System.Windows.Forms.Button hexEditLevelButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button patchExport;
        private System.Windows.Forms.Button patchImport;
        private System.Windows.Forms.SaveFileDialog savePatchDialog;
        private System.Windows.Forms.OpenFileDialog openPatchDialog;
        private FilesystemBrowser filesystemBrowser1;
        private System.Windows.Forms.Button mpPatch;
        private System.Windows.Forms.Button decompArm9Bin;
        private System.Windows.Forms.Button mpPatch2;
        private System.Windows.Forms.GroupBox asmToolsGroupbox;
        private System.Windows.Forms.Button makeinsert;
        private System.Windows.Forms.OpenFileDialog openTextFileDialog;
        private System.Windows.Forms.Button dumpMapButton;
        private System.Windows.Forms.Button makeclean;
        private System.Windows.Forms.SaveFileDialog saveTextFileDialog;
        private System.Windows.Forms.CheckBox useMDI;
        private System.Windows.Forms.Button updateSpriteDataButton;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCredits;
        private System.Windows.Forms.Label lblCreditsHeader;
        private System.Windows.Forms.LinkLabel lnkNSMBHD;
        private System.Windows.Forms.LinkLabel lnkGitHub;
        private System.Windows.Forms.Label lblLinksHeader;
        private System.Windows.Forms.GroupBox patchesGroupbox;
        private System.Windows.Forms.GroupBox nsmbToolsGroupbox;
        private System.Windows.Forms.TabPage tabPage5;
        private TilesetList tilesetList1;
        private System.Windows.Forms.TabPage tabPage6;
        private BackgroundList backgroundList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox musicSlotsGrp;
        private System.Windows.Forms.Button renameBtn;
        private System.Windows.Forms.ListBox musicList;
        private System.Windows.Forms.Label lblMinutes;
        private System.Windows.Forms.NumericUpDown autoBackupTime;
        private System.Windows.Forms.Label lblEvery;
        private System.Windows.Forms.CheckBox chkAutoBackup;
        private System.Windows.Forms.Button deleteBackups;
        private System.Windows.Forms.Button openLevel;
        private System.Windows.Forms.Button importClipboard;
        private System.Windows.Forms.Button openClipboard;
        private System.Windows.Forms.Button exportClipboard;
        private System.Windows.Forms.GroupBox grpDLPMode;
        private System.Windows.Forms.Label lblDLPMode1;
        private System.Windows.Forms.CheckBox dlpCheckBox;
        private System.Windows.Forms.Label lblDLPMode2;
        private System.Windows.Forms.OpenFileDialog openROMDialog;
    }
}
