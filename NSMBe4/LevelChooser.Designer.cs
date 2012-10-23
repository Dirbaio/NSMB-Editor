
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
            this.importClipboard = new System.Windows.Forms.Button();
            this.openLevel = new System.Windows.Forms.Button();
            this.hexEditLevelButton = new System.Windows.Forms.Button();
            this.exportLevelButton = new System.Windows.Forms.Button();
            this.importLevelButton = new System.Windows.Forms.Button();
            this.editLevelButton = new System.Windows.Forms.Button();
            this.levelTreeView = new System.Windows.Forms.TreeView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
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
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.useMDI = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.languageListBox = new System.Windows.Forms.ListBox();
            this.updateSpriteDataButton = new System.Windows.Forms.Button();
            this.changeLanguageButton = new System.Windows.Forms.Button();
            this.dumpMapButton = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.openROMDialog = new System.Windows.Forms.OpenFileDialog();
            this.importLevelDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportLevelDialog = new System.Windows.Forms.SaveFileDialog();
            this.savePatchDialog = new System.Windows.Forms.SaveFileDialog();
            this.openPatchDialog = new System.Windows.Forms.OpenFileDialog();
            this.openTextFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveTextFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.exportClipboard = new System.Windows.Forms.Button();
            this.openClipboard = new System.Windows.Forms.Button();
            this.tilesetList1 = new NSMBe4.TilesetList();
            this.backgroundList1 = new NSMBe4.BackgroundList();
            this.filesystemBrowser1 = new NSMBe4.DSFileSystem.FilesystemBrowser();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(550, 493);
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
            this.tabPage2.Size = new System.Drawing.Size(542, 467);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "<tabPage2>";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // importClipboard
            // 
            this.importClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importClipboard.Enabled = false;
            this.importClipboard.Location = new System.Drawing.Point(7, 436);
            this.importClipboard.Name = "importClipboard";
            this.importClipboard.Size = new System.Drawing.Size(94, 23);
            this.importClipboard.TabIndex = 6;
            this.importClipboard.Text = "Import Clipboard";
            this.importClipboard.UseVisualStyleBackColor = true;
            this.importClipboard.Click += new System.EventHandler(this.importClipboard_Click);
            // 
            // openLevel
            // 
            this.openLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openLevel.Location = new System.Drawing.Point(220, 406);
            this.openLevel.Name = "openLevel";
            this.openLevel.Size = new System.Drawing.Size(119, 23);
            this.openLevel.TabIndex = 5;
            this.openLevel.Text = "Open Exported Level";
            this.openLevel.UseVisualStyleBackColor = true;
            this.openLevel.Click += new System.EventHandler(this.openLevel_Click);
            // 
            // hexEditLevelButton
            // 
            this.hexEditLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEditLevelButton.Enabled = false;
            this.hexEditLevelButton.Location = new System.Drawing.Point(453, 435);
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
            this.exportLevelButton.Location = new System.Drawing.Point(107, 406);
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
            this.importLevelButton.Location = new System.Drawing.Point(6, 406);
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
            this.editLevelButton.Location = new System.Drawing.Point(453, 406);
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
            this.levelTreeView.Size = new System.Drawing.Size(522, 394);
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
            this.tabPage5.Size = new System.Drawing.Size(542, 467);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Tilesets";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.backgroundList1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(542, 467);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Backgrounds";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.filesystemBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(542, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<tabPage1>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.musicSlotsGrp);
            this.tabPage3.Controls.Add(this.patchesGroupbox);
            this.tabPage3.Controls.Add(this.nsmbToolsGroupbox);
            this.tabPage3.Controls.Add(this.asmToolsGroupbox);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.dumpMapButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(542, 467);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "<tabPage3>";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            this.musicSlotsGrp.Size = new System.Drawing.Size(278, 210);
            this.musicSlotsGrp.TabIndex = 8;
            this.musicSlotsGrp.TabStop = false;
            this.musicSlotsGrp.Text = "Music Slots";
            // 
            // renameBtn
            // 
            this.renameBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.renameBtn.Location = new System.Drawing.Point(197, 181);
            this.renameBtn.Name = "renameBtn";
            this.renameBtn.Size = new System.Drawing.Size(75, 23);
            this.renameBtn.TabIndex = 10;
            this.renameBtn.Text = "Rename";
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
            this.musicList.Size = new System.Drawing.Size(266, 147);
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
            this.patchesGroupbox.Text = "Patches";
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
            this.nsmbToolsGroupbox.Size = new System.Drawing.Size(246, 137);
            this.nsmbToolsGroupbox.TabIndex = 6;
            this.nsmbToolsGroupbox.TabStop = false;
            this.nsmbToolsGroupbox.Text = "NSMB Tools";
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
            this.dataFinderButton.Location = new System.Drawing.Point(6, 106);
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
            this.asmToolsGroupbox.Text = "ASM Tools";
            // 
            // makeclean
            // 
            this.makeclean.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.makeclean.Location = new System.Drawing.Point(6, 77);
            this.makeclean.Name = "makeclean";
            this.makeclean.Size = new System.Drawing.Size(266, 23);
            this.makeclean.TabIndex = 3;
            this.makeclean.Text = "Run \'make clean\'";
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
            this.makeinsert.Text = "Run \'make\' and insert";
            this.makeinsert.UseVisualStyleBackColor = true;
            this.makeinsert.Click += new System.EventHandler(this.padarm7bin_Click);
            // 
            // decompArm9Bin
            // 
            this.decompArm9Bin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.decompArm9Bin.Location = new System.Drawing.Point(6, 19);
            this.decompArm9Bin.Name = "decompArm9Bin";
            this.decompArm9Bin.Size = new System.Drawing.Size(266, 23);
            this.decompArm9Bin.TabIndex = 3;
            this.decompArm9Bin.Text = "Decompress ARM9 binary";
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
            this.groupBox1.Controls.Add(this.autoUpdate);
            this.groupBox1.Controls.Add(this.useMDI);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.languageListBox);
            this.groupBox1.Controls.Add(this.updateSpriteDataButton);
            this.groupBox1.Controls.Add(this.changeLanguageButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 244);
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
            this.deleteBackups.Text = "Delete all backups";
            this.deleteBackups.UseVisualStyleBackColor = true;
            this.deleteBackups.Click += new System.EventHandler(this.deleteBackups_Click);
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(118, 180);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(50, 13);
            this.lblMinutes.TabIndex = 12;
            this.lblMinutes.Text = "Minute(s)";
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
            this.lblEvery.Size = new System.Drawing.Size(34, 13);
            this.lblEvery.TabIndex = 10;
            this.lblEvery.Text = "Every";
            // 
            // chkAutoBackup
            // 
            this.chkAutoBackup.AutoSize = true;
            this.chkAutoBackup.Location = new System.Drawing.Point(6, 156);
            this.chkAutoBackup.Name = "chkAutoBackup";
            this.chkAutoBackup.Size = new System.Drawing.Size(117, 17);
            this.chkAutoBackup.TabIndex = 9;
            this.chkAutoBackup.Text = "Auto-backup levels";
            this.chkAutoBackup.UseVisualStyleBackColor = true;
            this.chkAutoBackup.CheckedChanged += new System.EventHandler(this.autoBackupTime_ValueChanged);
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.Location = new System.Drawing.Point(6, 130);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Size = new System.Drawing.Size(140, 17);
            this.autoUpdate.TabIndex = 7;
            this.autoUpdate.Text = "Auto-update Sprite Data";
            this.autoUpdate.UseVisualStyleBackColor = true;
            this.autoUpdate.CheckedChanged += new System.EventHandler(this.autoUpdate_CheckedChanged);
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
            this.updateSpriteDataButton.Location = new System.Drawing.Point(165, 126);
            this.updateSpriteDataButton.Name = "updateSpriteDataButton";
            this.updateSpriteDataButton.Size = new System.Drawing.Size(75, 23);
            this.updateSpriteDataButton.TabIndex = 3;
            this.updateSpriteDataButton.Text = "Update now";
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
            this.dumpMapButton.Text = "Dump rom map";
            this.dumpMapButton.UseVisualStyleBackColor = true;
            this.dumpMapButton.Click += new System.EventHandler(this.dumpMapButton_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.linkLabel2);
            this.tabPage4.Controls.Add(this.linkLabel1);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Controls.Add(this.label5);
            this.tabPage4.Controls.Add(this.label4);
            this.tabPage4.Controls.Add(this.label3);
            this.tabPage4.Controls.Add(this.label1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(542, 467);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "<tabPage4>";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(211, 258);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(120, 13);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "NSMB Hacking Domain";
            this.toolTip1.SetToolTip(this.linkLabel2, "http://nsmbhd.net/");
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(224, 245);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(95, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "NSMBe on GitHub";
            this.toolTip1.SetToolTip(this.linkLabel1, "http://github.com/Dirbaio/NSMB-Editor");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(246, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Links";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(84, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(375, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "And everyone else who\'s helped us make the editor and community awesome!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(209, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Piranhaplant - Developer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(204, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Dirbaio - Current Developer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(205, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Treeki - Original Developer";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(238, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Credits";
            // 
            // label3
            // 
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
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(469, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "New Super Mario Bros. Editor";
            // 
            // openROMDialog
            // 
            this.openROMDialog.Filter = "Nintendo DS ROMs (*.nds)|*.nds|All files (*.*)|*.*";
            // 
            // importLevelDialog
            // 
            this.importLevelDialog.Filter = "NSMB Editor Exported Levels (*.nml)|*.nml|All files (*.*)|*.*";
            // 
            // exportLevelDialog
            // 
            this.exportLevelDialog.Filter = "NSMB Editor Exported Levels (*.nml)|*.nml|All files (*.*)|*.*";
            // 
            // savePatchDialog
            // 
            this.savePatchDialog.Filter = "NSMB Patches (*.nmp)|*.nmp|All files (*.*)|*.*";
            // 
            // openPatchDialog
            // 
            this.openPatchDialog.Filter = "NSMB Patches (*.nmp)|*.nmp|All files (*.*)|*.*";
            // 
            // openTextFileDialog
            // 
            this.openTextFileDialog.Filter = "Text Files|*.txt|All files|*.*";
            // 
            // saveTextFileDialog
            // 
            this.saveTextFileDialog.Filter = "Text files|*.txt";
            // 
            // exportClipboard
            // 
            this.exportClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportClipboard.Enabled = false;
            this.exportClipboard.Location = new System.Drawing.Point(108, 436);
            this.exportClipboard.Name = "exportClipboard";
            this.exportClipboard.Size = new System.Drawing.Size(106, 23);
            this.exportClipboard.TabIndex = 7;
            this.exportClipboard.Text = "Export to Clipboard";
            this.exportClipboard.UseVisualStyleBackColor = true;
            this.exportClipboard.Click += new System.EventHandler(this.exportClipboard_Click);
            // 
            // openClipboard
            // 
            this.openClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openClipboard.Location = new System.Drawing.Point(221, 436);
            this.openClipboard.Name = "openClipboard";
            this.openClipboard.Size = new System.Drawing.Size(118, 23);
            this.openClipboard.TabIndex = 8;
            this.openClipboard.Text = "Open Clipboard";
            this.openClipboard.UseVisualStyleBackColor = true;
            this.openClipboard.Click += new System.EventHandler(this.openClipboard_Click);
            // 
            // tilesetList1
            // 
            this.tilesetList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tilesetList1.Location = new System.Drawing.Point(3, 3);
            this.tilesetList1.Name = "tilesetList1";
            this.tilesetList1.Size = new System.Drawing.Size(536, 461);
            this.tilesetList1.TabIndex = 0;
            // 
            // backgroundList1
            // 
            this.backgroundList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.backgroundList1.Location = new System.Drawing.Point(0, 0);
            this.backgroundList1.Name = "backgroundList1";
            this.backgroundList1.Size = new System.Drawing.Size(542, 467);
            this.backgroundList1.TabIndex = 0;
            // 
            // filesystemBrowser1
            // 
            this.filesystemBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesystemBrowser1.Location = new System.Drawing.Point(3, 3);
            this.filesystemBrowser1.Name = "filesystemBrowser1";
            this.filesystemBrowser1.Size = new System.Drawing.Size(536, 461);
            this.filesystemBrowser1.TabIndex = 0;
            // 
            // LevelChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 517);
            this.Controls.Add(this.tabControl1);
            this.Name = "LevelChooser";
            this.Text = "<_TITLE>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelChooser_FormClosing);
            this.Load += new System.EventHandler(this.LevelChooser_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage6.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
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
        private System.Windows.Forms.OpenFileDialog openROMDialog;
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
        private System.Windows.Forms.CheckBox autoUpdate;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label9;
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
    }
}