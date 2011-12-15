
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.hexEditLevelButton = new System.Windows.Forms.Button();
            this.exportLevelButton = new System.Windows.Forms.Button();
            this.importLevelButton = new System.Windows.Forms.Button();
            this.editLevelButton = new System.Windows.Forms.Button();
            this.levelTreeView = new System.Windows.Forms.TreeView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.asmToolsGroupbox = new System.Windows.Forms.GroupBox();
            this.makeclean = new System.Windows.Forms.Button();
            this.makeinsert = new System.Windows.Forms.Button();
            this.decompArm9Bin = new System.Windows.Forms.Button();
            this.mpPatch2 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.autoUpdate = new System.Windows.Forms.CheckBox();
            this.useMDI = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.languageListBox = new System.Windows.Forms.ListBox();
            this.updateSpriteDataButton = new System.Windows.Forms.Button();
            this.changeLanguageButton = new System.Windows.Forms.Button();
            this.dumpMapButton = new System.Windows.Forms.Button();
            this.tilesetEditor = new System.Windows.Forms.Button();
            this.mpPatch = new System.Windows.Forms.Button();
            this.patchImport = new System.Windows.Forms.Button();
            this.patchExport = new System.Windows.Forms.Button();
            this.dataFinderButton = new System.Windows.Forms.Button();
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
            this.nsmbToolsGroupbox = new System.Windows.Forms.GroupBox();
            this.patchesGroupbox = new System.Windows.Forms.GroupBox();
            this.filesystemBrowser1 = new NSMBe4.DSFileSystem.FilesystemBrowser();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.asmToolsGroupbox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.nsmbToolsGroupbox.SuspendLayout();
            this.patchesGroupbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(550, 478);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.hexEditLevelButton);
            this.tabPage2.Controls.Add(this.exportLevelButton);
            this.tabPage2.Controls.Add(this.importLevelButton);
            this.tabPage2.Controls.Add(this.editLevelButton);
            this.tabPage2.Controls.Add(this.levelTreeView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(542, 452);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "<tabPage2>";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // hexEditLevelButton
            // 
            this.hexEditLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEditLevelButton.Location = new System.Drawing.Point(381, 423);
            this.hexEditLevelButton.Name = "hexEditLevelButton";
            this.hexEditLevelButton.Size = new System.Drawing.Size(66, 23);
            this.hexEditLevelButton.TabIndex = 4;
            this.hexEditLevelButton.Text = "<hexEditLevelButton>";
            this.hexEditLevelButton.UseVisualStyleBackColor = true;
            this.hexEditLevelButton.Click += new System.EventHandler(this.hexEditLevelButton_Click);
            // 
            // exportLevelButton
            // 
            this.exportLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportLevelButton.Location = new System.Drawing.Point(88, 423);
            this.exportLevelButton.Name = "exportLevelButton";
            this.exportLevelButton.Size = new System.Drawing.Size(75, 23);
            this.exportLevelButton.TabIndex = 3;
            this.exportLevelButton.Text = "<exportLevelButton>";
            this.exportLevelButton.UseVisualStyleBackColor = true;
            this.exportLevelButton.Click += new System.EventHandler(this.exportLevelButton_Click);
            // 
            // importLevelButton
            // 
            this.importLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importLevelButton.Location = new System.Drawing.Point(7, 423);
            this.importLevelButton.Name = "importLevelButton";
            this.importLevelButton.Size = new System.Drawing.Size(75, 23);
            this.importLevelButton.TabIndex = 2;
            this.importLevelButton.Text = "<importLevelButton>";
            this.importLevelButton.UseVisualStyleBackColor = true;
            this.importLevelButton.Click += new System.EventHandler(this.importLevelButton_Click);
            // 
            // editLevelButton
            // 
            this.editLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editLevelButton.Location = new System.Drawing.Point(453, 423);
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
            this.levelTreeView.Size = new System.Drawing.Size(522, 411);
            this.levelTreeView.TabIndex = 0;
            this.levelTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.levelTreeView_AfterSelect);
            this.levelTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.levelTreeView_NodeMouseDoubleClick);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.filesystemBrowser1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(542, 452);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "<tabPage1>";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.patchesGroupbox);
            this.tabPage3.Controls.Add(this.nsmbToolsGroupbox);
            this.tabPage3.Controls.Add(this.asmToolsGroupbox);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Controls.Add(this.dumpMapButton);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(542, 452);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "<tabPage3>";
            this.tabPage3.UseVisualStyleBackColor = true;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.autoUpdate);
            this.groupBox1.Controls.Add(this.useMDI);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.languageListBox);
            this.groupBox1.Controls.Add(this.updateSpriteDataButton);
            this.groupBox1.Controls.Add(this.changeLanguageButton);
            this.groupBox1.Location = new System.Drawing.Point(6, 149);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(246, 161);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "<groupBox1>";
            // 
            // autoUpdate
            // 
            this.autoUpdate.AutoSize = true;
            this.autoUpdate.Location = new System.Drawing.Point(6, 130);
            this.autoUpdate.Name = "autoUpdate";
            this.autoUpdate.Size = new System.Drawing.Size(142, 17);
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
            this.useMDI.Size = new System.Drawing.Size(60, 17);
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
            // tilesetEditor
            // 
            this.tilesetEditor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tilesetEditor.Location = new System.Drawing.Point(6, 77);
            this.tilesetEditor.Name = "tilesetEditor";
            this.tilesetEditor.Size = new System.Drawing.Size(234, 23);
            this.tilesetEditor.TabIndex = 3;
            this.tilesetEditor.Text = "<tilesetEditor>";
            this.tilesetEditor.UseVisualStyleBackColor = true;
            this.tilesetEditor.Click += new System.EventHandler(this.tilesetEditor_Click);
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
            this.tabPage4.Size = new System.Drawing.Size(530, 416);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "<tabPage4>";
            this.tabPage4.UseVisualStyleBackColor = true;
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(202, 258);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(120, 13);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "NSMB Hacking Domain";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked_1);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(200, 245);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(124, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "NSMBe on Google Code";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(234, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 20);
            this.label9.TabIndex = 7;
            this.label9.Text = "Links";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(75, 203);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(375, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "And everyone else who\'s helped us make the editor and community awesome!";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(200, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(124, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Piranhaplant - Developer";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(195, 177);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Dirbaio - Current Developer";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(196, 164);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(133, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Treeki - Original Developer";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(227, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Credits";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(188, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 24);
            this.label3.TabIndex = 1;
            this.label3.Text = "Version 5.2 SVN";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(28, 16);
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
            // nsmbToolsGroupbox
            // 
            this.nsmbToolsGroupbox.Controls.Add(this.mpPatch);
            this.nsmbToolsGroupbox.Controls.Add(this.mpPatch2);
            this.nsmbToolsGroupbox.Controls.Add(this.tilesetEditor);
            this.nsmbToolsGroupbox.Controls.Add(this.dataFinderButton);
            this.nsmbToolsGroupbox.Location = new System.Drawing.Point(6, 6);
            this.nsmbToolsGroupbox.Name = "nsmbToolsGroupbox";
            this.nsmbToolsGroupbox.Size = new System.Drawing.Size(246, 137);
            this.nsmbToolsGroupbox.TabIndex = 6;
            this.nsmbToolsGroupbox.TabStop = false;
            this.nsmbToolsGroupbox.Text = "NSMB Tools";
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
            // filesystemBrowser1
            // 
            this.filesystemBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesystemBrowser1.Location = new System.Drawing.Point(3, 3);
            this.filesystemBrowser1.Name = "filesystemBrowser1";
            this.filesystemBrowser1.Size = new System.Drawing.Size(536, 446);
            this.filesystemBrowser1.TabIndex = 0;
            // 
            // LevelChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 502);
            this.Controls.Add(this.tabControl1);
            this.Name = "LevelChooser";
            this.Text = "<_TITLE>";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelChooser_FormClosing);
            this.Load += new System.EventHandler(this.LevelChooser_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.asmToolsGroupbox.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.nsmbToolsGroupbox.ResumeLayout(false);
            this.patchesGroupbox.ResumeLayout(false);
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
        private System.Windows.Forms.Button tilesetEditor;
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
    }
}