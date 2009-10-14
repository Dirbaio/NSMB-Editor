using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

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

                ROM = new NitroClass(openROMDialog.FileName);
                filesystemBrowser1.Load(ROM);
                NSMBDataHandler.load(ROM);

                LoadLevelNames();

                OpenEditors = new List<LevelEditor>();
                OpenLevelHexEditors = new List<LevelHexEditor>();

                LanguageManager.ApplyToContainer(this, "LevelChooser");
                openROMDialog.Filter = LanguageManager.Get("LevelChooser", "ROMFilter");
                importLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
                exportLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
                openPatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
                savePatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
            }
        }

        public static NitroClass ROM;

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

        private void editLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Make sure this level isn't already open
            if (OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        le.Show();
                        return;
                    }
                }
            }

            if (OpenLevelHexEditors.Count > 0) {
                foreach (LevelHexEditor le in OpenLevelHexEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        MessageBox.Show(LanguageManager.Get("LevelChooser", "AlreadyHexEditing"), "NSMB Editor 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            // Make a caption
            string EditorCaption = LanguageManager.Get("General", "EditingSomething") + " ";

            if (levelTreeView.SelectedNode.Parent.Parent == null) {
                EditorCaption += levelTreeView.SelectedNode.Text;
            } else {
                EditorCaption += levelTreeView.SelectedNode.Parent.Text + ", " + levelTreeView.SelectedNode.Text;
            }

            // Open it
            LevelEditor NewEditor = new LevelEditor(ROM, (string)levelTreeView.SelectedNode.Tag);
            OpenEditors.Add(NewEditor);
            NewEditor.Text = EditorCaption;
            NewEditor.Show();
        }

        private void hexEditLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Make sure this level isn't already open
            if (OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        MessageBox.Show(LanguageManager.Get("LevelChooser", "AlreadyNormalEditing"), "NSMB Editor 4", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            if (OpenLevelHexEditors.Count > 0) {
                foreach (LevelHexEditor le in OpenLevelHexEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        le.Show();
                        return;
                    }
                }
            }

            // Make a caption
            string EditorCaption = LanguageManager.Get("General", "EditingSomething") + " ";

            if (levelTreeView.SelectedNode.Parent.Parent == null) {
                EditorCaption += levelTreeView.SelectedNode.Text;
            } else {
                EditorCaption += levelTreeView.SelectedNode.Parent.Text + ", " + levelTreeView.SelectedNode.Text;
            }

            // Open it
            LevelHexEditor NewEditor = new LevelHexEditor(ROM, (string)levelTreeView.SelectedNode.Tag);
            OpenLevelHexEditors.Add(NewEditor);
            NewEditor.Text = EditorCaption;
            NewEditor.Show();
        }

        private List<LevelEditor> OpenEditors;
        private List<LevelHexEditor> OpenLevelHexEditors;

        private void LevelChooser_FormClosing(object sender, FormClosingEventArgs e) {
            if (OpenEditors != null && OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.ForceClose()) {
                        e.Cancel = true;
                        return;
                    }
                }
            }
        }

        private void levelTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node.Tag != null) {
                editLevelButton_Click(null, null);
            }
        }


        private void importLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Make sure this level isn't already open
            if (OpenEditors.Count > 0) {
                foreach (LevelEditor le in OpenEditors) {
                    if (!le.IsDisposed && le.LevelFilename == (string)levelTreeView.SelectedNode.Tag) {
                        MessageBox.Show(
                            LanguageManager.Get("LevelChooser", "CantImport"),
                            LanguageManager.Get("LevelChooser", "CantImportTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // Figure out what file to import
            if (importLevelDialog.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            // Get the file IDs
            string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
            ushort LevelFileID = ROM.FileIDs[LevelFilename + ".bin"];
            ushort BGFileID = ROM.FileIDs[LevelFilename + "_bgdat.bin"];

            // Load it
            FileStream fs = new FileStream(importLevelDialog.FileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            NSMBLevel.ImportLevel(ROM, LevelFileID, BGFileID, br);
            br.Close();
        }

        private void exportLevelButton_Click(object sender, EventArgs e) {
            if (levelTreeView.SelectedNode == null) return;

            // Figure out what file to export to
            if (exportLevelDialog.ShowDialog() == DialogResult.Cancel) {
                return;
            }

            // Get the file IDs
            string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
            ushort LevelFileID = ROM.FileIDs[LevelFilename + ".bin"];
            ushort BGFileID = ROM.FileIDs[LevelFilename + "_bgdat.bin"];

            // Load it
            FileStream fs = new FileStream(exportLevelDialog.FileName, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            NSMBLevel.ExportLevel(ROM, LevelFileID, BGFileID, bw);
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
                DataFinderForm = new DataFinder(ROM);
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
            //input
            bool onlyCourse = false;

            //output to show to the user
            bool differentRomsWarning = false; // tells if we have shown the warning
            int fileCount = 0;

            //load the original rom
            MessageBox.Show(LanguageManager.Get("Patch", "SelectROM"), LanguageManager.Get("Patch", "Export"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (openROMDialog.ShowDialog() == DialogResult.Cancel)
                return;
            NitroClass origROM = new NitroClass(openROMDialog.FileName);
            origROM.Load(null);

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
            progress.SetMax(ROM.FileCount);
            int progVal = 0;
            MessageBox.Show(LanguageManager.Get("Patch", "StartingPatch"), LanguageManager.Get("Patch", "Export"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            ushort CourseDirID = ROM.DirIDs["course"];

            foreach (ushort id in ROM.FileNames.Keys)
            {
                if (onlyCourse && ROM.FileParentIDs[id] != CourseDirID)
                    continue;

                Console.Out.WriteLine("Checking " + ROM.FileNames[id]);
                progress.SetCurrentAction(string.Format(LanguageManager.Get("Patch", "ComparingFile"), ROM.FileNames[id]));

                //check same version
                if(!differentRomsWarning && ROM.FileNames[id] != origROM.FileNames[id])
                {
                    if (MessageBox.Show(LanguageManager.Get("Patch", "ExportDiffVersions"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        differentRomsWarning = true;
                    else
                    {
                        fs.Close();
                        return;
                    }
                }

                byte[] oldFile = origROM.ExtractFile(id);
                byte[] newFile = ROM.ExtractFile(id);

                if (!arrayEqual(oldFile, newFile))
                {
                    //include file in patch
                    string fileName = origROM.FileNames[id];
                    Console.Out.WriteLine("Including: " + fileName);
                    progress.WriteLine(string.Format(LanguageManager.Get("Patch", "IncludedFile"), fileName));
                    fileCount++;

                    bw.Write((byte)1);
                    bw.Write(fileName);
                    bw.Write(id);
                    bw.Write((uint)newFile.Length);
                    bw.Write(newFile, 0, newFile.Length);
                }
                progress.setValue(++progVal);
            }
            bw.Write((byte)0);
            bw.Close();
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


            //shitty way to show progress: I don't know how many files, so i do it size-based...

            ProgressWindow progress = new ProgressWindow(LanguageManager.Get("Patch", "ImportProgressTitle"));
            progress.Show();
            progress.SetMax((int)br.BaseStream.Length);//i don't think there are such big patches
            int progVal = 0;

            byte filestartByte = br.ReadByte();
            while (filestartByte == 1)
            {
                string fileName = br.ReadString();
                progress.WriteLine(string.Format(LanguageManager.Get("Patch", "ReplacingFile"), fileName));
                ushort origFileID = br.ReadUInt16();
                ushort fileID = ROM.FileIDs[fileName];

                uint length = br.ReadUInt32();
                progVal += (int)length;
                progress.setValue(progVal);

                byte[] newFile = new byte[length];
                br.Read(newFile, 0, (int)length);
                filestartByte = br.ReadByte();

                if (!differentRomsWarning && origFileID != fileID)
                {
                    MessageBox.Show(LanguageManager.Get("Patch", "ImportDiffVersions"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    differentRomsWarning = true;
                }

                Console.Out.WriteLine("Replace " + fileName);
                ROM.ReplaceFile(fileID, newFile);
                fileCount++;
            }
            br.Close();
            progress.setValue(0);
            progress.SetMax(100);
            progress.setValue(100);
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
            NitroClass NARC = new NitroClass(ROM, ROM.FileIDs[NarcName]);
            NARC.Load(null);

            byte[] file = ROM.ExtractFile(ROM.FileIDs[f1]);
            NARC.ReplaceFile(NARC.FileIDs[f1], file);
            file = ROM.ExtractFile(ROM.FileIDs[f2]);
            NARC.ReplaceFile(NARC.FileIDs[f2], file);
        }

        private void lzUncompressAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(LanguageManager.Get("LevelChooser", "LZUncompWarning"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
                return;

            bool UncompressNARCS = MessageBox.Show(LanguageManager.Get("LevelChooser", "DecompInNarcs"), LanguageManager.Get("General", "Question"), MessageBoxButtons.YesNo) == DialogResult.Yes;

            lzUncompress(ROM, UncompressNARCS);
        }

        private void lzUncompress(NitroClass ROM, bool narcs)
        {
            foreach (ushort FileID in ROM.FileIDs.Values)
            {
                Console.Out.WriteLine("Uncompressing " + ROM.FileNames[FileID]);

                if (ROM.FileNames[FileID].EndsWith("narc") || ROM.FileNames[FileID].EndsWith("NARC"))
                {
                    if (narcs)
                    {
                        NitroClass narc = new NitroClass(ROM, FileID);
                        narc.Load(null);
                        lzUncompress(narc, narcs);
                    }
                }
                else
                {
                    byte[] file = ROM.ExtractFile(FileID);
                    bool success = false;
                    try
                    {
                        file = FileSystem.LZ77_Decompress(file);
                        success = true;
                    }
                    catch (Exception) { }

                    if (success)
                        ROM.ReplaceFile(FileID, file);
                }
            }
        }

        private void tilesetEditor_Click(object sender, EventArgs e)
        {
            new TilesetChooser(ROM).Show();
        }
    }
}
