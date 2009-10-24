using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NSMBe4.Filesystem
{
    public partial class FilesystemBrowser : UserControl, FileLister
    {
        private Dictionary<int, TreeNode> DirHolder;
        public NitroClass ROM;

        public static GraphicsViewer gv;

        public FilesystemBrowser()
        {
            InitializeComponent();

            extractFileButton.Enabled = false;
            replaceFileButton.Enabled = false;
            compressFileButton.Enabled = false;
            decompressFileButton.Enabled = false;
            hexEdButton.Enabled = false;
            LanguageManager.ApplyToContainer(this, "FilesystemBrowser");

            DirHolder = new Dictionary<int, TreeNode>();
        }

        public new void Load(NitroClass ROM)
        {
            this.ROM = ROM;
            ROM.Load(this);
        }

        private void fileTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateFileInfo();
        }

        private void UpdateFileInfo()
        {
            TreeNode n = fileTreeView.SelectedNode;

            ushort FSObjId = Convert.ToUInt16(n.Tag);
            string StatusMsg;
            if (FSObjId >= 61440)
            {
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FolderStatus"), n.Text, n.Tag);
                extractFileButton.Enabled = false;
                replaceFileButton.Enabled = false;
                compressFileButton.Enabled = false;
                decompressFileButton.Enabled = false;
                hexEdButton.Enabled = false;
            }
            else
            {
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FileStatus"), ROM.FileOffsets[FSObjId].ToString("X"), ROM.FileSizes[FSObjId].ToString(), n.Tag);
                extractFileButton.Enabled = true;
                replaceFileButton.Enabled = true;
                compressFileButton.Enabled = true;
                decompressFileButton.Enabled = true;
                hexEdButton.Enabled = true;
            }
            selectedFileInfo.Text = StatusMsg;
        }

        public void DirReady(int DirID, int ParentID, string DirName, bool IsRoot)
        {
            if(IsRoot)
                DirHolder[DirID] = fileTreeView.Nodes.Add(DirID.ToString(), DirName, 0, 0);
            else
                DirHolder[DirID] = DirHolder[ParentID].Nodes.Add(DirID.ToString(), DirName, 0, 0);
           DirHolder[DirID].Tag = DirID.ToString();
        }

        public void FileReady(int FileID, int ParentID, string FileName)
        {
            DirHolder[ParentID].Nodes.Add(FileID.ToString(), FileName, 2, 2).Tag = FileID.ToString();
        }

        private void extractFileButton_Click(object sender, EventArgs e)
        {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            string FileName = ROM.FileNames[FSObjID];
            extractFileDialog.FileName = FileName;
            if (extractFileDialog.ShowDialog() == DialogResult.OK)
            {
                string DestFileName = extractFileDialog.FileName;
                byte[] TempFile = ROM.ExtractFile(FSObjID);
                FileStream wfs = new FileStream(DestFileName, FileMode.Create, FileAccess.Write, FileShare.None);
                wfs.Write(TempFile, 0, TempFile.GetLength(0));
                wfs.Dispose();
            }
        }

        private void replaceFileButton_Click(object sender, EventArgs e)
        {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            string FileName = ROM.FileNames[FSObjID];
            replaceFileDialog.FileName = FileName;
            if (replaceFileDialog.ShowDialog() == DialogResult.OK)
            {
                string SrcFileName = replaceFileDialog.FileName;
                FileStream rfs = new FileStream(SrcFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                byte[] TempFile = new byte[rfs.Length];
                rfs.Read(TempFile, 0, (int)rfs.Length);
                rfs.Dispose();
                ROM.ReplaceFile(FSObjID, TempFile);
            }
            UpdateFileInfo();
        }

        private void compressFileButton_Click(object sender, EventArgs e)
        {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            byte[] RawFile = ROM.ExtractFile(FSObjID);
            byte[] CompFile = FileSystem.LZ77_Compress(RawFile);
            ROM.ReplaceFile(FSObjID, CompFile);
            UpdateFileInfo();
        }

        private void decompressFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
                byte[] CompFile = ROM.ExtractFile(FSObjID);
                byte[] RawFile = FileSystem.LZ77_Decompress(CompFile);
                ROM.ReplaceFile(FSObjID, RawFile);
                UpdateFileInfo();
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageManager.Get("FilesystemBrowser", "DecompressionFail"));
            }
        }

        private void fileTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            ushort FileID = Convert.ToUInt16(e.Node.Tag);
            if (FileID >= 61440)
            {
                e.Node.Expand();
                return;
            }

            String FileName = ROM.FileNames[FileID];
            if(!FileName.Contains("."))
                return;
            string ext = FileName.Substring(FileName.LastIndexOf(".")+1);
            if (ext.ToUpperInvariant() == "NARC")
            {
                new FilesystemBrowserDialog(ROM, FileID).Show();
            }
            else //send to GraphicsViewer
            {
                if (gv == null || gv.IsDisposed)
                    gv = new GraphicsViewer();

                gv.Show();

                byte[] file = ROM.ExtractFile(FileID);
                if (Control.ModifierKeys == Keys.Control)
                    gv.SetPalette(file);
                else
                    gv.SetFile(file);
            }
        }

        private void hexEdButton_Click(object sender, EventArgs e)
        {
            ushort FSObjID = Convert.ToUInt16(fileTreeView.SelectedNode.Tag);
            new FileHexEditor(ROM, FSObjID).Show();
        }
    }
}
