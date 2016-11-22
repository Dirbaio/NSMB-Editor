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

            extractFileDialog.Filter = LanguageManager.Get("Filters", "all");
            replaceFileDialog.Filter = LanguageManager.Get("Filters", "all");

            TreeNode main = new TreeNode(fs.mainDir.name, 0, 0);
            main.Tag = fs.mainDir;

            loadDir(main, fs.mainDir);

            fileTreeView.Nodes.Clear();
            fileTreeView.Nodes.Add(main);
            main.Expand();
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
            bool extract = false;

            if (n == null)
            {
                e = false;
                StatusMsg = LanguageManager.Get("FilesystemBrowser", "NoFileSelected");
            }
            else if (n.Tag is Directory)
            {
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FolderStatus"), n.Text, (n.Tag as Directory).id);
                e = false;
                extract = true;
            }
            else
            {
                File f = n.Tag as File;
                StatusMsg = string.Format(LanguageManager.Get("FilesystemBrowser", "FileStatus"), (f is PhysicalFile)?((PhysicalFile)f).fileBegin.ToString("X"):"?", f.fileSize.ToString(), f.id);
                e = true;
                extract = true;
            }


            extractFileButton.Enabled = extract;
            replaceFileButton.Enabled = extract;
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
            if (fileTreeView.SelectedNode.Tag is File)
            {
                File f = fileTreeView.SelectedNode.Tag as File;

                string FileName = f.name;
                extractFileDialog.FileName = FileName;
                if (extractFileDialog.ShowDialog() == DialogResult.OK)
                    extractFile(f, extractFileDialog.FileName);
            }
            else
            {
                Directory d = fileTreeView.SelectedNode.Tag as Directory;

                if (extractDirectoryDialog.ShowDialog() == DialogResult.OK)
                    extractDirectory(d, extractDirectoryDialog.SelectedPath);
            }
        }

        private void extractFile(File f, String fileName)
        {
            byte[] tempFile = f.getContents();
            FileStream wfs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            wfs.Write(tempFile, 0, tempFile.GetLength(0));
            wfs.Dispose();
        }

        private string extractDirectory(Directory d, String filePath)
        {
            String newFolderName;
            if (d.name.Contains(Path.DirectorySeparatorChar.ToString())) // This will be the root folder
                newFolderName = "FILESYSTEM";
            else
                newFolderName = d.name;
            String destDir = System.IO.Path.Combine(filePath, newFolderName);
            if (!System.IO.Directory.Exists(destDir))
                System.IO.Directory.CreateDirectory(destDir);

            foreach (File f in d.childrenFiles)
                extractFile(f, System.IO.Path.Combine(destDir, f.name));
            foreach (Directory subd in d.childrenDirs)
                extractDirectory(subd, destDir);
            return destDir;
        }

        private void replaceFileButton_Click(object sender, EventArgs e)
        {
            if (fileTreeView.SelectedNode.Tag is File)
            {
                File f = fileTreeView.SelectedNode.Tag as File;

                try {
                    f.beginEdit(this);
                } catch (AlreadyEditingException) {
                    MessageBox.Show(LanguageManager.Get("Errors", "File"));
                    return;
                }
                
                string FileName = f.name;
                replaceFileDialog.FileName = FileName;
                if (replaceFileDialog.ShowDialog() != DialogResult.OK) {
                    UpdateFileInfo();
                    f.endEdit(this);
                    return;
                }

                //if (f.id >= 0 && f.id <= ROM.OverlayCount)
                //{
                //    DialogResult r = MessageBox.Show(LanguageManager.Get("FilesystemBrowser", "ImportOverlay"), LanguageManager.Get("FilesystemBrowser", "ImportOverlayTitle"), MessageBoxButtons.YesNoCancel);
                //    if(r == DialogResult.Cancel)
                //    {
                //        UpdateFileInfo();
                //        f.endEdit(this);
                //        return;
                //    }

                //    f.isCompressed = r == DialogResult.Yes;
                //}
                replaceFile(f, replaceFileDialog.FileName);

                UpdateFileInfo();
                f.endEdit(this);
            }
            else
            {
                if (extractDirectoryDialog.ShowDialog() == DialogResult.OK)
                {
                    Directory d = fileTreeView.SelectedNode.Tag as Directory;
                    replaceDirectory(d, extractDirectoryDialog.SelectedPath);
                }
            }
        }

        private void replaceFile(File f, String inputFile)
        {
            // NOTE: f.beginEdit(this) must be called before this function
            FileStream rfs = new FileStream(inputFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] tempFile = new byte[rfs.Length];
            rfs.Read(tempFile, 0, (int)rfs.Length);
            rfs.Dispose();
            f.replace(tempFile, this);
        }

        private void replaceDirectory(Directory d, String inputDir)
        {
            foreach (File f in d.childrenFiles)
            {
                try
                {
                    String inputFile = System.IO.Path.Combine(inputDir, f.name);
                    if (System.IO.File.Exists(inputFile))
                    {
                        f.beginEdit(this);
                        replaceFile(f, inputFile);
                        f.endEdit(this);
                    }
                    else
                        Console.Out.WriteLine("Input file does not exist: " + inputFile);
                }
                catch (Exception ex) {
                    Console.Out.WriteLine("Failed to replace file: " + f.name + "\n\n" + ex.Message);
                }
            }
            foreach (Directory subd in d.childrenDirs)
            {
                String nextDir = System.IO.Path.Combine(inputDir, subd.name);
                replaceDirectory(subd, nextDir);
            }
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
                    new PaletteViewer(new LZFile(f, LZFile.CompressionType.MaybeLZ)).Show();
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
            File f = fileTreeView.SelectedNode.Tag as File;

            if (f.id < 0 || f.id > ROM.OverlayCount)
            {
                MessageBox.Show(LanguageManager.Get("FilesystemBrowser", "ErrorNotOverlay"));
                return;
            }

            Overlay ovdec = ROM.arm9ovs2[f.id];

            if (!ovdec.isCompressed)
                MessageBox.Show(LanguageManager.Get("FilesystemBrowser", "ErrorDecompressed"));

            ovdec.decompress();
        }

        private void fileTreeView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode node = e.Item as TreeNode;

            if (node.Tag is File)
            {
                // The dragged file must first be saved to disk
                File f = node.Tag as File;
                string FileName = f.name;
                string DestFileName = Path.Combine(System.IO.Path.GetTempPath(), FileName);
                extractFile(f, DestFileName);

                // Then start a drag and drop
                String[] files = new String[] { DestFileName };
                DragDropEffects drop = DoDragDrop(new DataObject(DataFormats.FileDrop, files), DragDropEffects.Move);
                // Delete the file if it wasn't dropped anywhere
                if (System.IO.File.Exists(DestFileName))
                    System.IO.File.Delete(DestFileName);
            }
            else if (node.Tag is Directory)
            {
                // Same process as above
                Directory d = node.Tag as Directory;
                String DestDir = System.IO.Path.GetTempPath();
                DestDir = extractDirectory(d, DestDir);

                String[] folders = new String[] { DestDir };
                DragDropEffects drop = DoDragDrop(new DataObject(DataFormats.FileDrop, folders), DragDropEffects.Move);
                if (System.IO.Directory.Exists(DestDir))
                    System.IO.Directory.Delete(DestDir, true);
            }
        }
    }
}
