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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LevelChooser));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.selectedFileInfo = new System.Windows.Forms.Label();
            this.decompressFileButton = new System.Windows.Forms.Button();
            this.compressFileButton = new System.Windows.Forms.Button();
            this.replaceFileButton = new System.Windows.Forms.Button();
            this.extractFileButton = new System.Windows.Forms.Button();
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.hexEditLevelButton = new System.Windows.Forms.Button();
            this.exportLevelButton = new System.Windows.Forms.Button();
            this.importLevelButton = new System.Windows.Forms.Button();
            this.editLevelButton = new System.Windows.Forms.Button();
            this.levelTreeView = new System.Windows.Forms.TreeView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataFinderButton = new System.Windows.Forms.Button();
            this.changeLanguageButton = new System.Windows.Forms.Button();
            this.languageListBox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.openROMDialog = new System.Windows.Forms.OpenFileDialog();
            this.extractFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.replaceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.importLevelDialog = new System.Windows.Forms.OpenFileDialog();
            this.exportLevelDialog = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(13, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(361, 301);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.selectedFileInfo);
            this.tabPage1.Controls.Add(this.decompressFileButton);
            this.tabPage1.Controls.Add(this.compressFileButton);
            this.tabPage1.Controls.Add(this.replaceFileButton);
            this.tabPage1.Controls.Add(this.extractFileButton);
            this.tabPage1.Controls.Add(this.fileTreeView);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(353, 275);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "File Browser";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 227);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Selection Info:";
            // 
            // selectedFileInfo
            // 
            this.selectedFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedFileInfo.AutoSize = true;
            this.selectedFileInfo.Location = new System.Drawing.Point(94, 227);
            this.selectedFileInfo.Name = "selectedFileInfo";
            this.selectedFileInfo.Size = new System.Drawing.Size(13, 13);
            this.selectedFileInfo.TabIndex = 6;
            this.selectedFileInfo.Text = "--";
            // 
            // decompressFileButton
            // 
            this.decompressFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.decompressFileButton.Location = new System.Drawing.Point(251, 246);
            this.decompressFileButton.Name = "decompressFileButton";
            this.decompressFileButton.Size = new System.Drawing.Size(96, 23);
            this.decompressFileButton.TabIndex = 5;
            this.decompressFileButton.Text = "LZ Decompress";
            this.decompressFileButton.UseVisualStyleBackColor = true;
            this.decompressFileButton.Click += new System.EventHandler(this.decompressFileButton_Click);
            // 
            // compressFileButton
            // 
            this.compressFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.compressFileButton.Location = new System.Drawing.Point(168, 246);
            this.compressFileButton.Name = "compressFileButton";
            this.compressFileButton.Size = new System.Drawing.Size(77, 23);
            this.compressFileButton.TabIndex = 4;
            this.compressFileButton.Text = "LZ Compress";
            this.compressFileButton.UseVisualStyleBackColor = true;
            this.compressFileButton.Click += new System.EventHandler(this.compressFileButton_Click);
            // 
            // replaceFileButton
            // 
            this.replaceFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.replaceFileButton.Location = new System.Drawing.Point(87, 246);
            this.replaceFileButton.Name = "replaceFileButton";
            this.replaceFileButton.Size = new System.Drawing.Size(75, 23);
            this.replaceFileButton.TabIndex = 3;
            this.replaceFileButton.Text = "Replace File";
            this.replaceFileButton.UseVisualStyleBackColor = true;
            this.replaceFileButton.Click += new System.EventHandler(this.replaceFileButton_Click);
            // 
            // extractFileButton
            // 
            this.extractFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.extractFileButton.Location = new System.Drawing.Point(6, 246);
            this.extractFileButton.Name = "extractFileButton";
            this.extractFileButton.Size = new System.Drawing.Size(75, 23);
            this.extractFileButton.TabIndex = 2;
            this.extractFileButton.Text = "Extract File";
            this.extractFileButton.UseVisualStyleBackColor = true;
            this.extractFileButton.Click += new System.EventHandler(this.extractFileButton_Click);
            // 
            // fileTreeView
            // 
            this.fileTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTreeView.ImageIndex = 0;
            this.fileTreeView.ImageList = this.imageList1;
            this.fileTreeView.Location = new System.Drawing.Point(6, 6);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.SelectedImageIndex = 0;
            this.fileTreeView.Size = new System.Drawing.Size(341, 218);
            this.fileTreeView.TabIndex = 1;
            this.fileTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileTreeView_AfterSelect);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder.png");
            this.imageList1.Images.SetKeyName(1, "folder_open.png");
            this.imageList1.Images.SetKeyName(2, "document.png");
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
            this.tabPage2.Size = new System.Drawing.Size(353, 275);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Level Editor";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // hexEditLevelButton
            // 
            this.hexEditLevelButton.Location = new System.Drawing.Point(200, 246);
            this.hexEditLevelButton.Name = "hexEditLevelButton";
            this.hexEditLevelButton.Size = new System.Drawing.Size(66, 23);
            this.hexEditLevelButton.TabIndex = 4;
            this.hexEditLevelButton.Text = "Hex Edit";
            this.hexEditLevelButton.UseVisualStyleBackColor = true;
            this.hexEditLevelButton.Click += new System.EventHandler(this.hexEditLevelButton_Click);
            // 
            // exportLevelButton
            // 
            this.exportLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exportLevelButton.Location = new System.Drawing.Point(88, 246);
            this.exportLevelButton.Name = "exportLevelButton";
            this.exportLevelButton.Size = new System.Drawing.Size(75, 23);
            this.exportLevelButton.TabIndex = 3;
            this.exportLevelButton.Text = "Export Level";
            this.exportLevelButton.UseVisualStyleBackColor = true;
            this.exportLevelButton.Click += new System.EventHandler(this.exportLevelButton_Click);
            // 
            // importLevelButton
            // 
            this.importLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.importLevelButton.Location = new System.Drawing.Point(7, 246);
            this.importLevelButton.Name = "importLevelButton";
            this.importLevelButton.Size = new System.Drawing.Size(75, 23);
            this.importLevelButton.TabIndex = 2;
            this.importLevelButton.Text = "Import Level";
            this.importLevelButton.UseVisualStyleBackColor = true;
            this.importLevelButton.Click += new System.EventHandler(this.importLevelButton_Click);
            // 
            // editLevelButton
            // 
            this.editLevelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.editLevelButton.Location = new System.Drawing.Point(272, 246);
            this.editLevelButton.Name = "editLevelButton";
            this.editLevelButton.Size = new System.Drawing.Size(75, 23);
            this.editLevelButton.TabIndex = 1;
            this.editLevelButton.Text = "Edit Level";
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
            this.levelTreeView.Size = new System.Drawing.Size(341, 234);
            this.levelTreeView.TabIndex = 0;
            this.levelTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.levelTreeView_NodeMouseDoubleClick);
            this.levelTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.levelTreeView_AfterSelect);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataFinderButton);
            this.tabPage3.Controls.Add(this.changeLanguageButton);
            this.tabPage3.Controls.Add(this.languageListBox);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(353, 275);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataFinderButton
            // 
            this.dataFinderButton.Location = new System.Drawing.Point(132, 6);
            this.dataFinderButton.Name = "dataFinderButton";
            this.dataFinderButton.Size = new System.Drawing.Size(132, 23);
            this.dataFinderButton.TabIndex = 3;
            this.dataFinderButton.Text = "Data Finder";
            this.dataFinderButton.UseVisualStyleBackColor = true;
            this.dataFinderButton.Click += new System.EventHandler(this.dataFinderButton_Click);
            // 
            // changeLanguageButton
            // 
            this.changeLanguageButton.Location = new System.Drawing.Point(28, 120);
            this.changeLanguageButton.Name = "changeLanguageButton";
            this.changeLanguageButton.Size = new System.Drawing.Size(75, 23);
            this.changeLanguageButton.TabIndex = 2;
            this.changeLanguageButton.Text = "Change";
            this.changeLanguageButton.UseVisualStyleBackColor = true;
            this.changeLanguageButton.Click += new System.EventHandler(this.changeLanguageButton_Click);
            // 
            // languageListBox
            // 
            this.languageListBox.FormattingEnabled = true;
            this.languageListBox.Items.AddRange(new object[] {
            "English",
            "Spanish"});
            this.languageListBox.Location = new System.Drawing.Point(6, 19);
            this.languageListBox.Name = "languageListBox";
            this.languageListBox.Size = new System.Drawing.Size(120, 95);
            this.languageListBox.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Language:";
            // 
            // openROMDialog
            // 
            this.openROMDialog.Filter = "Nintendo DS ROMs (*.nds)|*.nds|All files (*.*)|*.*";
            // 
            // extractFileDialog
            // 
            this.extractFileDialog.Filter = "All files (*.*)|*.*";
            // 
            // replaceFileDialog
            // 
            this.replaceFileDialog.Filter = "All files (*.*)|*.*";
            // 
            // importLevelDialog
            // 
            this.importLevelDialog.Filter = "NSMB Editor Exported Levels (*.nml)|*.nml|All files (*.*)|*.*";
            // 
            // exportLevelDialog
            // 
            this.exportLevelDialog.Filter = "NSMB Editor Exported Levels (*.nml)|*.nml|All files (*.*)|*.*";
            // 
            // LevelChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 325);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LevelChooser";
            this.Text = "NSMB Editor 4.8";
            this.Load += new System.EventHandler(this.LevelChooser_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LevelChooser_FormClosing);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button compressFileButton;
        private System.Windows.Forms.Button replaceFileButton;
        private System.Windows.Forms.Button extractFileButton;
        private System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button exportLevelButton;
        private System.Windows.Forms.Button importLevelButton;
        private System.Windows.Forms.Button editLevelButton;
        private System.Windows.Forms.TreeView levelTreeView;
        private System.Windows.Forms.Button decompressFileButton;
        private System.Windows.Forms.Label selectedFileInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openROMDialog;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SaveFileDialog extractFileDialog;
        private System.Windows.Forms.OpenFileDialog replaceFileDialog;
        private System.Windows.Forms.OpenFileDialog importLevelDialog;
        private System.Windows.Forms.SaveFileDialog exportLevelDialog;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button changeLanguageButton;
        private System.Windows.Forms.ListBox languageListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button dataFinderButton;
        private System.Windows.Forms.Button hexEditLevelButton;
    }
}