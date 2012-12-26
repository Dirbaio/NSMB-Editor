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
using NSMBe4.TilemapEditor;

namespace NSMBe4.DSFileSystem
{
    public partial class FilesystemBrowser : UserControl
    {
        private Filesystem fs;

        public FilesystemBrowser()
        {
            InitializeComponent();

            LanguageManager.ApplyToContainer(this, "FilesystemBrowser");

            UpdateFileInfo();

            //The ImageList is created here rather than in Visual Studio
            //because it looks like the Mono compiler can't handle ImageList resources.
            //So, please don't create ImageLists using the Designer!!

            fileTreeView.ImageList = new ImageList();
            fileTreeView.ImageList.ColorDepth = ColorDepth.Depth32Bit;

            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.folder_open);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file_narc);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file_ncg);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file_ncl);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file_nsc);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file_nsbmd);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file_nsbtx);
            fileTreeView.ImageList.Images.Add(NSMBe4.Properties.Resources.file_sdat);
        }

        public new void Load(Filesystem fs)
        {
            this.fs = fs;

            TreeNode main = new TreeNode(fs.mainDir.name, 0, 0);
            main.Tag = fs.mainDir;

            loadDir(main, fs.mainDir);

            fileTreeView.Nodes.Clear();
            fileTreeView.Nodes.Add(main);
        }

        private int getIconForFile(File f)
        {
            //TODO: More icons!

            string name = f.name.ToLowerInvariant();

            if (name.EndsWith(".narc")) return 2;
            if (name.EndsWith(".carc")) return 2;
            if (name.EndsWith("_ncg.bin")) return 3;
            if (name.EndsWith(".ncgr")) return 3;
            if (name.EndsWith("_ncl.bin")) return 4;
            if (name.EndsWith(".nclr")) return 4;
            if (name.EndsWith("_nsc.bin")) return 5;
            if (name.EndsWith(".nscr")) return 5;

            if (name.EndsWith(".nsbmd")) return 6;
            if (name.EndsWith(".nsbtx")) return 7;
            if (name.EndsWith(".sdat")) return 8;
            return 1;
        }

        private void loadDir(TreeNode node, Directory dir)
        {
            foreach (File f in dir.childrenFiles)
            {
                int ic = getIconForFile(f);
                TreeNode fileNode = new TreeNode(f.name, ic, ic);
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
            bool e;

            if (n == null)
            {
                e = false;
                StatusMsg = "No file selected";
            }
            else if (n.Tag is Directory)
            {
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FolderStatus"), n.Text, (n.Tag as Directory).id);
                e = false;
            }
            else
            {
                File f = n.Tag as File;
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FileStatus"), (f is PhysicalFile)?((PhysicalFile)f).fileBegin.ToString("X"):"?", f.fileSize.ToString(), f.id);
                e = true;
            }


            extractFileButton.Enabled = e;
            replaceFileButton.Enabled = e;
            compressFileButton.Enabled = e;
            decompressFileButton.Enabled = e;
            hexEdButton.Enabled = e;
            compressWithHeaderButton.Enabled = e;
            decompressWithHeaderButton.Enabled = e;
            decompressOverlayButton.Enabled = e;

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
            if (replaceFileDialog.ShowDialog() != DialogResult.OK)
            {
                UpdateFileInfo();
                f.endEdit(this);
                return;
            }
/*
            if(f is OverlayFile)
            {
                DialogResult r = MessageBox.Show("You're importing an overlay file. Is it a compressed overlay?\n\n(Overlays are compressed by default, so it probably is unless you decompressed it)", "Something", MessageBoxButtons.YesNoCancel);
                if(r == DialogResult.Cancel)
                {
                    UpdateFileInfo();
                    f.endEdit(this);
                    return;
                }

                (f as OverlayFile).isCompressed = r == DialogResult.Yes;
            }*/

            string SrcFileName = replaceFileDialog.FileName;
            FileStream rfs = new FileStream(SrcFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] TempFile = new byte[rfs.Length];
            rfs.Read(TempFile, 0, (int)rfs.Length);
            rfs.Dispose();
            f.replace(TempFile, this);

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
                    f.endEdit(this);
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

            try
            {
                if (filename == "banner.bin")
                {
                    LevelChooser.showImgMgr();
                    File imgFile = new InlineFile(f, 0x20, 0x200, f.name);
                    File palFile = new InlineFile(f, 0x220, 0x20, f.name);
                    LevelChooser.imgMgr.m.addImage(new Image2D(imgFile, 32, true, false));
                    LevelChooser.imgMgr.m.addPalette(new FilePalette(palFile));
                }
                else if (filename.EndsWith(".enpg"))
                {
                    LevelChooser.showImgMgr();
		            LZFile fileLz = new LZFile(f, LZFile.CompressionType.LZ);
                    File imgFile = new InlineFile(fileLz, 0, 0x10000, f.name);
                    File palFile = new InlineFile(fileLz, 0x10000, 0x200, f.name);
                    LevelChooser.imgMgr.m.addImage(new EnpgImage2D(imgFile));
                    LevelChooser.imgMgr.m.addPalette(new FilePalette(palFile));
                }
                else if (filename.EndsWith(".bncd"))
                	new Bncd(f);
                if (filename.EndsWith(".nsbtx") || filename.EndsWith(".nsbmd"))
                    new NSBTX(f);
                else if (filename.EndsWith(".nscr") ||
                         filename.EndsWith(".ncgr") ||
                         filename.EndsWith(".nclr"))
                    SectionFileLoader.load(f);
                else if (filename.EndsWith(".narc"))
                    new FilesystemBrowserDialog(new NarcFilesystem(f)).Show();
                else if (filename.EndsWith(".carc"))
                    new FilesystemBrowserDialog(new NarcFilesystem(f, true)).Show();
                else if (filename.Contains("_ncl.bin"))
                    new PaletteViewer(new LZFile(f, LZFile.CompressionType.LZ)).Show();
                else if (filename.Contains("_nsc.bin"))
                {
                    if (LevelChooser.imgMgr == null) return;
                    Image2D img = LevelChooser.imgMgr.m.getSelectedImage();
                    Palette[] pals = LevelChooser.imgMgr.m.getPalettes();
                    if (img == null) return;
                    if (pals == null) return;
                    if (pals.Length == 0) return;

                    Tilemap t = new Tilemap(f, 32, img, pals, 0, 0);
                    new TilemapEditorWindow(t).Show();
                }
                else if (filename.Contains("_ncg.bin"))
                {
                    LevelChooser.showImgMgr();
                    LevelChooser.imgMgr.m.addImage(new Image2D(f, 256, false));
                }
            }
            catch (AlreadyEditingException ex)
            {
                MessageBox.Show(this, (LanguageManager.Get("Errors", "File")));
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

        private void decompressOverlayButton_Click(object sender, EventArgs e)
        {
        	//TODO
/*            OverlayFile f = fileTreeView.SelectedNode.Tag as OverlayFile;

            if (f == null)
            {
                MessageBox.Show("Error: Not an overlay file");
                return;
            }

            if (!f.isCompressed)
                MessageBox.Show("Error: Overlay file is already decompressed");

            f.decompress();*/
        }
    }
}
