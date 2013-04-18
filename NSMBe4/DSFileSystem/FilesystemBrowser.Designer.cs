namespace NSMBe4.DSFileSystem
{
    partial class FilesystemBrowser
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
            this.label1 = new System.Windows.Forms.Label();
            this.selectedFileInfo = new System.Windows.Forms.Label();
            this.decompressFileButton = new System.Windows.Forms.Button();
            this.compressFileButton = new System.Windows.Forms.Button();
            this.replaceFileButton = new System.Windows.Forms.Button();
            this.extractFileButton = new System.Windows.Forms.Button();
            this.fileTreeView = new System.Windows.Forms.TreeView();
            this.extractFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.replaceFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.hexEdButton = new System.Windows.Forms.Button();
            this.compressWithHeaderButton = new System.Windows.Forms.Button();
            this.decompressWithHeaderButton = new System.Windows.Forms.Button();
            this.decompressOverlayButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.extractDirectoryDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "<label1>";
            // 
            // selectedFileInfo
            // 
            this.selectedFileInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectedFileInfo.AutoSize = true;
            this.selectedFileInfo.Location = new System.Drawing.Point(99, 4);
            this.selectedFileInfo.Name = "selectedFileInfo";
            this.selectedFileInfo.Size = new System.Drawing.Size(13, 13);
            this.selectedFileInfo.TabIndex = 13;
            this.selectedFileInfo.Text = "--";
            // 
            // decompressFileButton
            // 
            this.decompressFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.decompressFileButton.Location = new System.Drawing.Point(410, 20);
            this.decompressFileButton.Name = "decompressFileButton";
            this.decompressFileButton.Size = new System.Drawing.Size(96, 23);
            this.decompressFileButton.TabIndex = 12;
            this.decompressFileButton.Text = "<decompressFileButton>";
            this.decompressFileButton.UseVisualStyleBackColor = true;
            this.decompressFileButton.Click += new System.EventHandler(this.decompressFileButton_Click);
            // 
            // compressFileButton
            // 
            this.compressFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.compressFileButton.Location = new System.Drawing.Point(327, 20);
            this.compressFileButton.Name = "compressFileButton";
            this.compressFileButton.Size = new System.Drawing.Size(77, 23);
            this.compressFileButton.TabIndex = 11;
            this.compressFileButton.Text = "<compressFileButton>";
            this.compressFileButton.UseVisualStyleBackColor = true;
            this.compressFileButton.Click += new System.EventHandler(this.compressFileButton_Click);
            // 
            // replaceFileButton
            // 
            this.replaceFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.replaceFileButton.Location = new System.Drawing.Point(84, 20);
            this.replaceFileButton.Name = "replaceFileButton";
            this.replaceFileButton.Size = new System.Drawing.Size(75, 23);
            this.replaceFileButton.TabIndex = 10;
            this.replaceFileButton.Text = "<replaceFileButton>";
            this.replaceFileButton.UseVisualStyleBackColor = true;
            this.replaceFileButton.Click += new System.EventHandler(this.replaceFileButton_Click);
            // 
            // extractFileButton
            // 
            this.extractFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.extractFileButton.Location = new System.Drawing.Point(3, 20);
            this.extractFileButton.Name = "extractFileButton";
            this.extractFileButton.Size = new System.Drawing.Size(75, 23);
            this.extractFileButton.TabIndex = 9;
            this.extractFileButton.Text = "<extractFileButton>";
            this.extractFileButton.UseVisualStyleBackColor = true;
            this.extractFileButton.Click += new System.EventHandler(this.extractFileButton_Click);
            // 
            // fileTreeView
            // 
            this.fileTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileTreeView.Location = new System.Drawing.Point(0, 0);
            this.fileTreeView.Name = "fileTreeView";
            this.fileTreeView.Size = new System.Drawing.Size(509, 234);
            this.fileTreeView.TabIndex = 8;
            this.fileTreeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.fileTreeView_ItemDrag);
            this.fileTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.fileTreeView_AfterSelect);
            this.fileTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.fileTreeView_NodeMouseDoubleClick);
            // 
            // extractFileDialog
            // 
            this.extractFileDialog.Filter = "All files (*.*)|*.*";
            // 
            // replaceFileDialog
            // 
            this.replaceFileDialog.Filter = "All files (*.*)|*.*";
            // 
            // hexEdButton
            // 
            this.hexEdButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.hexEdButton.Location = new System.Drawing.Point(244, 20);
            this.hexEdButton.Name = "hexEdButton";
            this.hexEdButton.Size = new System.Drawing.Size(77, 23);
            this.hexEdButton.TabIndex = 11;
            this.hexEdButton.Text = "<hexEdButton>";
            this.hexEdButton.UseVisualStyleBackColor = true;
            this.hexEdButton.Click += new System.EventHandler(this.hexEdButton_Click);
            // 
            // compressWithHeaderButton
            // 
            this.compressWithHeaderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.compressWithHeaderButton.Location = new System.Drawing.Point(151, 50);
            this.compressWithHeaderButton.Name = "compressWithHeaderButton";
            this.compressWithHeaderButton.Size = new System.Drawing.Size(170, 23);
            this.compressWithHeaderButton.TabIndex = 11;
            this.compressWithHeaderButton.Text = "<compressFileButton>";
            this.compressWithHeaderButton.UseVisualStyleBackColor = true;
            this.compressWithHeaderButton.Click += new System.EventHandler(this.compressWithHeaderButton_Click);
            // 
            // decompressWithHeaderButton
            // 
            this.decompressWithHeaderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.decompressWithHeaderButton.Location = new System.Drawing.Point(327, 50);
            this.decompressWithHeaderButton.Name = "decompressWithHeaderButton";
            this.decompressWithHeaderButton.Size = new System.Drawing.Size(179, 23);
            this.decompressWithHeaderButton.TabIndex = 12;
            this.decompressWithHeaderButton.Text = "<decompressFileButton>";
            this.decompressWithHeaderButton.UseVisualStyleBackColor = true;
            this.decompressWithHeaderButton.Click += new System.EventHandler(this.decompressWithHeaderButton_Click);
            // 
            // decompressOverlayButton
            // 
            this.decompressOverlayButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.decompressOverlayButton.Location = new System.Drawing.Point(20, 50);
            this.decompressOverlayButton.Name = "decompressOverlayButton";
            this.decompressOverlayButton.Size = new System.Drawing.Size(125, 23);
            this.decompressOverlayButton.TabIndex = 11;
            this.decompressOverlayButton.Text = "Decompress overlay";
            this.decompressOverlayButton.UseVisualStyleBackColor = true;
            this.decompressOverlayButton.Click += new System.EventHandler(this.decompressOverlayButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.extractFileButton);
            this.panel1.Controls.Add(this.selectedFileInfo);
            this.panel1.Controls.Add(this.replaceFileButton);
            this.panel1.Controls.Add(this.decompressWithHeaderButton);
            this.panel1.Controls.Add(this.compressFileButton);
            this.panel1.Controls.Add(this.decompressFileButton);
            this.panel1.Controls.Add(this.hexEdButton);
            this.panel1.Controls.Add(this.decompressOverlayButton);
            this.panel1.Controls.Add(this.compressWithHeaderButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 234);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 81);
            this.panel1.TabIndex = 15;
            // 
            // FilesystemBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileTreeView);
            this.Controls.Add(this.panel1);
            this.Name = "FilesystemBrowser";
            this.Size = new System.Drawing.Size(509, 315);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label selectedFileInfo;
        private System.Windows.Forms.Button decompressFileButton;
        private System.Windows.Forms.Button compressFileButton;
        private System.Windows.Forms.Button replaceFileButton;
        private System.Windows.Forms.Button extractFileButton;
        private System.Windows.Forms.TreeView fileTreeView;
        private System.Windows.Forms.SaveFileDialog extractFileDialog;
        private System.Windows.Forms.OpenFileDialog replaceFileDialog;
        private System.Windows.Forms.Button hexEdButton;
        private System.Windows.Forms.Button compressWithHeaderButton;
        private System.Windows.Forms.Button decompressWithHeaderButton;
        private System.Windows.Forms.Button decompressOverlayButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FolderBrowserDialog extractDirectoryDialog;
    }
}
