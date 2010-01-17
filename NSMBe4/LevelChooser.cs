using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using NSMBe4.DSFileSystem;


namespace NSMBe4 {
    public partial class LevelChooser : Form
    {
        public LevelChooser() {
            InitializeComponent();
        }

        private void LevelChooser_Load(object sender, EventArgs e) {
            if (openROMDialog.ShowDialog() == DialogResult.Cancel) {
                Application.Exit();
            } else {
                importLevelButton.Enabled = false;
                exportLevelButton.Enabled = false;
                editLevelButton.Enabled = false;
                hexEditLevelButton.Enabled = false;

                ROM.load(openROMDialog.FileName);
                filesystemBrowser1.Load(ROM.FS);

                LoadLevelNames();

                LanguageManager.ApplyToContainer(this, "LevelChooser");
                openROMDialog.Filter = LanguageManager.Get("LevelChooser", "ROMFilter");
                importLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
                exportLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
                openPatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
                savePatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
            }
        }

        private void LoadLevelNames() {
            List<string> LevelNames = LanguageManager.GetList("LevelNames");

            TreeNode WorldNode = null;
            string WorldID = null;
            TreeNode LevelNode;
            TreeNode AreaNode;
            for (int NameIdx = 0; NameIdx < LevelNames.Count; NameIdx++) {
                LevelNames[NameIdx] = LevelNames[NameIdx].Trim();
                if (LevelNames[NameIdx] == "") continue;
                if (LevelNames[NameIdx][0] == '-') {
                    // Create a world
                    string[] ParseWorld = LevelNames[NameIdx].Substring(1).Split('|');
                    WorldNode = levelTreeView.Nodes.Add("ln" + NameIdx.ToString(), ParseWorld[0]);
                    WorldID = ParseWorld[1];
                } else {
                    // Create a level
                    string[] ParseLevel = LevelNames[NameIdx].Split('|');
                    if (ParseLevel[2] == "1") {
                        // One area only; no need for a subfolder here
                        LevelNode = WorldNode.Nodes.Add("ln" + NameIdx.ToString(), ParseLevel[0]);
                        LevelNode.Tag = WorldID + ParseLevel[1] + "_1";
                    } else {
                        // Create a subfolder
                        LevelNode = WorldNode.Nodes.Add("ln" + NameIdx.ToString(), ParseLevel[0]);
                        int AreaCount = int.Parse(ParseLevel[2]);
                        for (int AreaIdx = 1; AreaIdx <= AreaCount; AreaIdx++) {
                            AreaNode = LevelNode.Nodes.Add("ln" + NameIdx.ToString() + "a" + AreaIdx.ToString(), "Area " + AreaIdx.ToString());
                            AreaNode.Tag = WorldID + ParseLevel[1] + "_" + AreaIdx.ToString();
                        }
                    }
                }
            }
        }

        private void levelTreeView_AfterSelect(object sender, TreeViewEventArgs e) {
            if (e.Node.Tag != null) {
                importLevelButton.Enabled = true;
                exportLevelButton.Enabled = true;
                editLevelButton.Enabled = true;
                hexEditLevelButton.Enabled = true;
            } else {
                importLevelButton.Enabled = false;
                exportLevelButton.Enabled = false;
                editLevelButton.Enabled = false;
                hexEditLevelButton.Enabled = false;
            }
        }

        private void editLevelButton_Click(object sender, EventArgs e)
        {
            // Make a caption
            string EditorCaption = LanguageManager.Get("General", "EditingSomething") + " ";

            if (levelTreeView.SelectedNode.Parent.Parent == null) {
                EditorCaption += levelTreeView.SelectedNode.Text;
            } else {
                EditorCaption += levelTreeView.SelectedNode.Parent.Text + ", " + levelTreeView.SelectedNode.Text;
            }

            // Open it
            try
            {
                LevelEditor NewEditor = new LevelEditor((string)levelTreeView.SelectedNode.Tag);
                NewEditor.Text = EditorCaption;
                NewEditor.Show();
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "Level"));
            }                
        }

        private void hexEditLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Make a caption
            string EditorCaption = LanguageManager.Get("General", "EditingSomething") + " ";

            if (levelTreeView.SelectedNode.Parent.Parent == null) {
                EditorCaption += levelTreeView.SelectedNode.Text;
            } else {
                EditorCaption += levelTreeView.SelectedNode.Parent.Text + ", " + levelTreeView.SelectedNode.Text;
            }

            // Open it
            try
            {
                LevelHexEditor NewEditor = new LevelHexEditor((string)levelTreeView.SelectedNode.Tag);
                NewEditor.Text = EditorCaption;
                NewEditor.Show();
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "Level"));
            }                
        }

        private void levelTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node.Tag != null) {
                editLevelButton_Click(null, null);
            }
        }


        private void importLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Figure out what file to import
            if (importLevelDialog.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            // Get the files
            string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
            NSMBe4.DSFileSystem.File LevelFile = ROM.FS.getFileByName(LevelFilename + ".bin");
            NSMBe4.DSFileSystem.File BGFile = ROM.FS.getFileByName(LevelFilename + "_bgdat.bin");

            // Load it
            FileStream fs = new FileStream(importLevelDialog.FileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            NSMBLevel.ImportLevel(LevelFile, BGFile, br);
            br.Close();
        }

        private void exportLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Figure out what file to export to
            if (exportLevelDialog.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            // Get the files
            string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
            NSMBe4.DSFileSystem.File LevelFile = ROM.FS.getFileByName(LevelFilename + ".bin");
            NSMBe4.DSFileSystem.File BGFile = ROM.FS.getFileByName(LevelFilename + "_bgdat.bin");

            // Load it
            FileStream fs = new FileStream(exportLevelDialog.FileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            NSMBLevel.ExportLevel(LevelFile, BGFile, bw);
            bw.Close();
        }

        private void changeLanguageButton_Click(object sender, EventArgs e) {
            if (languageListBox.SelectedItem != null) {
                Properties.Settings.Default.Language = languageListBox.SelectedIndex;
                Properties.Settings.Default.Save();

                    MessageBox.Show(
                        LanguageManager.Get("LevelChooser", "LangChanged"),
                        LanguageManager.Get("LevelChooser", "LangChangedTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private DataFinder DataFinderForm;

        private void dataFinderButton_Click(object sender, EventArgs e) {
            if (DataFinderForm == null || DataFinderForm.IsDisposed) {
                DataFinderForm = new DataFinder();
            }

            DataFinderForm.Show();
            DataFinderForm.Activate();
        }


        /**
         * PATCH FILE FORMAT
         * 
         * - String "NSMBe4 Exported Patch"
         * - Some files (see below)
         * - byte 0
         * 
         * STRUCTURE OF A FILE
         * - byte 1
         * - File name as a string
         * - File ID as ushort (to check for different versions, only gives a warning)
         * - File length as uint
         * - File contents as byte[]
         */

        private void patchExport_Click(object sender, EventArgs e)
        {
            //output to show to the user
            bool differentRomsWarning = false; // tells if we have shown the warning
            int fileCount = 0;

            //load the original rom
            MessageBox.Show(LanguageManager.Get("Patch", "SelectROM"), LanguageManager.Get("Patch", "Export"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (openROMDialog.ShowDialog() == DialogResult.Cancel)
                return;
            NitroFilesystem origROM = new NitroFilesystem(openROMDialog.FileName);

            //open the output patch
            MessageBox.Show(LanguageManager.Get("Patch", "SelectLocation"), LanguageManager.Get("Patch", "Export"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (savePatchDialog.ShowDialog() == DialogResult.Cancel)
                return;

            FileStream fs = new FileStream(savePatchDialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write("NSMBe4 Exported Patch");

            //DO THE PATCH!!
            ProgressWindow progress = new ProgressWindow(LanguageManager.Get("Patch", "ExportProgressTitle"));
            progress.Show();
            progress.SetMax(ROM.FS.allFiles.Count);
            int progVal = 0;
            MessageBox.Show(LanguageManager.Get("Patch", "StartingPatch"), LanguageManager.Get("Patch", "Export"), MessageBoxButtons.OK, MessageBoxIcon.Information);

            foreach (NSMBe4.DSFileSystem.File f in ROM.FS.allFiles)
            {
                if (f.isSystemFile) continue;

                Console.Out.WriteLine("Checking " + f.name);
                progress.SetCurrentAction(string.Format(LanguageManager.Get("Patch", "ComparingFile"), f.name));

                NSMBe4.DSFileSystem.File orig = origROM.getFileByName(f.name);
                //check same version
                if(!differentRomsWarning && f.id != orig.id)
                {
                    if (MessageBox.Show(LanguageManager.Get("Patch", "ExportDiffVersions"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        differentRomsWarning = true;
                    else
                    {
                        fs.Close();
                        return;
                    }
                }

                byte[] oldFile = orig.getContents();
                byte[] newFile = f.getContents();

                if (!arrayEqual(oldFile, newFile))
                {
                    //include file in patch
                    string fileName = orig.name;
                    Console.Out.WriteLine("Including: " + fileName);
                    progress.WriteLine(string.Format(LanguageManager.Get("Patch", "IncludedFile"), fileName));
                    fileCount++;

                    bw.Write((byte)1);
                    bw.Write(fileName);
                    bw.Write((ushort)f.id);
                    bw.Write((uint)newFile.Length);
                    bw.Write(newFile, 0, newFile.Length);
                }
                progress.setValue(++progVal);
            }
            bw.Write((byte)0);
            bw.Close();
            origROM.close();
            progress.SetCurrentAction("");
            progress.WriteLine(string.Format(LanguageManager.Get("Patch", "ExportReady"), fileCount));
        }

        public bool arrayEqual(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
                if (a[i] != b[i])
                    return false;

            return true;
        }

        private void patchImport_Click(object sender, EventArgs e)
        {
            //output to show to the user
            bool differentRomsWarning = false; // tells if we have shown the warning
            int fileCount = 0;

            //open the input patch
            if (openPatchDialog.ShowDialog() == DialogResult.Cancel)
                return;

            FileStream fs = new FileStream(openPatchDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader br = new BinaryReader(fs);

            string header = br.ReadString();
            if (header != "NSMBe4 Exported Patch")
            {
                MessageBox.Show(
                    LanguageManager.Get("Patch", "InvalidFile"),
                    LanguageManager.Get("Patch", "Unreadable"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                br.Close();
                return;
            }


            ProgressWindow progress = new ProgressWindow(LanguageManager.Get("Patch", "ImportProgressTitle"));
            progress.Show();

            byte filestartByte = br.ReadByte();
            try
            {
                while (filestartByte == 1)
                {
                    string fileName = br.ReadString();
                    progress.WriteLine(string.Format(LanguageManager.Get("Patch", "ReplacingFile"), fileName));
                    ushort origFileID = br.ReadUInt16();
                    NSMBe4.DSFileSystem.File f = ROM.FS.getFileByName(fileName);
                    uint length = br.ReadUInt32();

                    byte[] newFile = new byte[length];
                    br.Read(newFile, 0, (int)length);
                    filestartByte = br.ReadByte();

                    if (f != null)
                    {
                        ushort fileID = (ushort)f.id;

                        if (!differentRomsWarning && origFileID != fileID)
                        {
                            MessageBox.Show(LanguageManager.Get("Patch", "ImportDiffVersions"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            differentRomsWarning = true;
                        }

                        Console.Out.WriteLine("Replace " + fileName);
                        f.beginEdit(this);
                        f.replace(newFile, this);
                        f.endEdit(this);
                        fileCount++;
                    }
                }
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(string.Format(LanguageManager.Get("Patch", "Error"), fileCount), LanguageManager.Get("General", "Completed"), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            br.Close();
            MessageBox.Show(string.Format(LanguageManager.Get("Patch", "ImportReady"), fileCount), LanguageManager.Get("General", "Completed"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            progress.Close();
        }

        private void mpPatch_Click(object sender, EventArgs e)
        {
            NarcReplace("Dat_Field.narc",    "J01_1.bin", "J01_1_bgdat.bin");
            NarcReplace("Dat_Basement.narc", "J02_1.bin", "J02_1_bgdat.bin");
            NarcReplace("Dat_Ice.narc",      "J03_1.bin", "J03_1_bgdat.bin");
            NarcReplace("Dat_Pipe.narc",     "J04_1.bin", "J04_1_bgdat.bin");
            NarcReplace("Dat_Fort.narc",     "J05_1.bin", "J05_1_bgdat.bin");

            MessageBox.Show(LanguageManager.Get("General", "Completed"));
        }

        private void NarcReplace(string NarcName, string f1, string f2)
        {
            throw new NotImplementedException("Not implemented yet !!");
            /*
            NitroClass NARC = new NitroClass(ROM, ROM.FileIDs[NarcName]);
            NARC.Load(null);

            byte[] file = ROM.ExtractFile(ROM.FileIDs[f1]);
            NARC.ReplaceFile(NARC.FileIDs[f1], file);
            file = ROM.ExtractFile(ROM.FileIDs[f2]);
            NARC.ReplaceFile(NARC.FileIDs[f2], file);*/
        }

        private void tilesetEditor_Click(object sender, EventArgs e)
        {
            new TilesetChooser().Show();
        }
    }
}
