using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NSMBe4.NSBMD;

namespace NSMBe4.DSFileSystem
{
    public partial class FilesystemBrowser : UserControl
    {
        public static GraphicsViewer gv;
        private Filesystem fs;

        public FilesystemBrowser()
        {
            InitializeComponent();

            extractFileButton.Enabled = false;
            replaceFileButton.Enabled = false;
            compressFileButton.Enabled = false;
            decompressFileButton.Enabled = false;
            hexEdButton.Enabled = false;
            LanguageManager.ApplyToContainer(this, "FilesystemBrowser");

        }

        public new void Load(Filesystem fs)
        {
            this.fs = fs;

            TreeNode main = new TreeNode(fs.mainDir.name, 0, 0);
            main.Tag = fs.mainDir;
            loadDir(main, fs.mainDir);
            fileTreeView.Nodes.Add(main);
        }

        private void loadDir(TreeNode node, Directory dir)
        {
            foreach (File f in dir.childrenFiles)
            {
                TreeNode fileNode = new TreeNode(f.name, 2, 2);
                fileNode.Tag = f;
                node.Nodes.Add(fileNode);
            }

            foreach (Directory d in dir.childrenDirs)
            {
                TreeNode dirNode = new TreeNode(d.name, 0, 0);
                dirNode.Tag = d;
                loadDir(dirNode, d);
                node.Nodes.Add(dirNode);
            }
        }

        private void fileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateFileInfo();
        }

        private void UpdateFileInfo()
        {
            TreeNode n = fileTreeView.SelectedNode;

            string StatusMsg;
            if (n.Tag is Directory)
            {
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FolderStatus"), n.Text, (n.Tag as Directory).id);
                extractFileButton.Enabled = false;
                replaceFileButton.Enabled = false;
                compressFileButton.Enabled = false;
                decompressFileButton.Enabled = false;
                hexEdButton.Enabled = false;
            }
            else
            {
                File f = n.Tag as File;
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FileStatus"), f.fileBegin.ToString("X"), f.fileSize.ToString(), f.id);
                extractFileButton.Enabled = true;
                replaceFileButton.Enabled = true;
                compressFileButton.Enabled = true;
                decompressFileButton.Enabled = true;
                hexEdButton.Enabled = true;
            }
            selectedFileInfo.Text = StatusMsg;
        }

        private void extractFileButton_Click(object sender, EventArgs e)
        {
            File f = fileTreeView.SelectedNode.Tag as File;

            string FileName = f.name;
            extractFileDialog.FileName = FileName;
            if (extractFileDialog.ShowDialog() == DialogResult.OK)
            {
                string DestFileName = extractFileDialog.FileName;
                byte[] TempFile = f.getContents();
                FileStream wfs = new FileStream(DestFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                wfs.Write(TempFile, 0, TempFile.GetLength(0));
                wfs.Dispose();
            }
        }

        private void replaceFileButton_Click(object sender, EventArgs e)
        {
            File f = fileTreeView.SelectedNode.Tag as File;

            try
            {
                f.beginEdit(this);
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "File"));
                return;
            }                
                
            string FileName = f.name;
            replaceFileDialog.FileName = FileName;
            if (replaceFileDialog.ShowDialog() == DialogResult.OK)
            {
                string SrcFileName = replaceFileDialog.FileName;
                FileStream rfs = new FileStream(SrcFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] TempFile = new byte[rfs.Length];
                rfs.Read(TempFile, 0, (int)rfs.Length);
                rfs.Dispose();
                f.replace(TempFile, this);
            }
            UpdateFileInfo();
            f.endEdit(this);
        }

        private void compressFileButton_Click(object sender, EventArgs e)
        {
            File f = fileTreeView.SelectedNode.Tag as File;

            try
            {
                f.beginEdit(this);
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "File"));
                return;
            }     
            byte[] RawFile = f.getContents();
            byte[] CompFile = ROM.LZ77_Compress(RawFile);
            f.replace(CompFile, this );
            UpdateFileInfo();
            f.endEdit(this);
        }

        private void decompressFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                File f = fileTreeView.SelectedNode.Tag as File;

                try
                {
                    f.beginEdit(this);
                }
                catch (AlreadyEditingException)
                {
                    MessageBox.Show(LanguageManager.Get("Errors", "File"));
                    return;
                }     
                byte[] CompFile = f.getContents();
                byte[] RawFile = ROM.LZ77_Decompress(CompFile);
                f.replace(RawFile, this);
                UpdateFileInfo();
                f.endEdit(this);
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageManager.Get("FilesystemBrowser", "DecompressionFail"));
            }
        }

        private void fileTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Directory)
            {
                e.Node.Expand();
                return;
            }
            File f = e.Node.Tag as File;

            String FileName = f.name;

            if(!FileName.Contains("."))
                return;
            string ext = FileName.Substring(FileName.LastIndexOf(".")+1);
            ext = ext.ToUpperInvariant();
            switch (ext)
            {
                case "NSBTX":
                case "NSBMD":
                    try
                    {
                        new TextureEditor(f).Show();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                    break;
                case "NARC":
                    new FilesystemBrowserDialog(new NarcFilesystem(f)).Show();
                    break;
                default:
                    if (gv == null || gv.IsDisposed)
                        gv = new GraphicsViewer();

                    gv.Show();

                    byte[] file = f.getContents();
                    if (Control.ModifierKeys == Keys.Control)
                        gv.SetPalette(file);
                    else
                        gv.SetFile(file);
                    break;
            }
        }

        private void hexEdButton_Click(object sender, EventArgs e)
        {
            File f = fileTreeView.SelectedNode.Tag as File;

            try
            {
                new FileHexEditor(f).Show();
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "File"));
                return;
            }     
        }
    }
}
