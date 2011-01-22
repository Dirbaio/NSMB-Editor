/*
*   This file is part of NSMB Editor 5.
*
*   NSMB Editor 5 is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   NSMB Editor 5 is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with NSMB Editor 5.  If not, see <http://www.gnu.org/licenses/>.
*/

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
            compressWithHeaderButton.Enabled = false;
            decompressWithHeaderButton.Enabled = false;
            LanguageManager.ApplyToContainer(this, "FilesystemBrowser");

        }

        public new void Load(Filesystem fs)
        {
            this.fs = fs;
            fs.viewer = this;
            TreeNode main = new TreeNode(fs.mainDir.name, 0, 0);
            main.Tag = fs.mainDir;
            loadDir(main, fs.mainDir);
            fileTreeView.Nodes.Clear();
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
                compressWithHeaderButton.Enabled = false;
                decompressWithHeaderButton.Enabled = false;
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
                compressWithHeaderButton.Enabled = true;
                decompressWithHeaderButton.Enabled = true;
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
            f.replace(CompFile, this);
            UpdateFileInfo();
            f.endEdit(this);
        }

        private void decompressFileButton_Click(object sender, EventArgs e)
        {
            File f = fileTreeView.SelectedNode.Tag as File;
            try
            {

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
                if (f.beingEditedBy(this))
                    f.endEdit(this);
            }
        }

        private void compressWithHeaderButton_Click(object sender, EventArgs e)
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

                byte[] CompFileWithHeader = new byte[CompFile.Length + 4];
                Array.Copy(CompFile, 0, CompFileWithHeader, 4, CompFile.Length);
                f.replace(CompFileWithHeader, this);
                f.setUintAt(0, 0x37375A4C);
                UpdateFileInfo();
                f.endEdit(this);
        }

        private void decompressWithHeaderButton_Click(object sender, EventArgs e)
        {
            File f = fileTreeView.SelectedNode.Tag as File;
            try
            {

                try
                {
                    f.beginEdit(this);
                }
                catch (AlreadyEditingException)
                {
                    MessageBox.Show(LanguageManager.Get("Errors", "File"));
                    return;
                }

                if (f.getUintAt(0) != 0x37375A4C)
                {
                    MessageBox.Show(LanguageManager.Get("Errors", "NoLZHeader"));
                    return;
                }

                byte[] CompFile = f.getContents();
                byte[] CompFileWithoutHeader = new byte[CompFile.Length - 4];
                Array.Copy(CompFile, 4, CompFileWithoutHeader, 0, CompFileWithoutHeader.Length);
                byte[] RawFile = ROM.LZ77_Decompress(CompFileWithoutHeader);
                f.replace(RawFile, this);
                UpdateFileInfo();
                f.endEdit(this);
            }
            catch (Exception)
            {
                MessageBox.Show(LanguageManager.Get("FilesystemBrowser", "DecompressionFail"));
                if (f.beingEditedBy(this))
                    f.endEdit(this);
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

            String filename = f.name;
            filename = filename.ToLowerInvariant();

            if (filename.EndsWith(".nsbtx") || filename.EndsWith(".nsbmd"))
            {
                    new NSBTX(f);
            }
            else if (filename.EndsWith(".narc"))
                new FilesystemBrowserDialog(new NarcFilesystem(f)).Show();
            else if (filename.EndsWith(".carc"))
                new FilesystemBrowserDialog(new NarcFilesystem(f, true)).Show();
            else if (filename.EndsWith("_ncl.bin"))
            {
                new PaletteViewer(f).Show();
                //                LevelChooser.showImgMgr();
                //                LevelChooser.imgMgr.m.addPalette(new FilePalette(f));
            }
            else if (filename.EndsWith("_ncg.bin"))
            {
                LevelChooser.showImgMgr();
                LevelChooser.imgMgr.m.addImage(new Image2D(f, 256, false));
            }
            /*
            if (gv == null || gv.IsDisposed)
                        gv = new GraphicsViewer();

                    gv.Show();

                    byte[] file = f.getContents();
                    if (Control.ModifierKeys == Keys.Control)
                        gv.SetPalette(file);
                    else
                        gv.SetFile(file);
                    break;
            }*/
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
