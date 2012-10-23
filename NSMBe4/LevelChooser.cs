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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using NSMBe4.DSFileSystem;
using NSMBe4.NSBMD;
using NSMBe4.Patcher;
using System.Net;


namespace NSMBe4 {
    public partial class LevelChooser : Form
    {
        public static ImageManagerWindow imgMgr;
        public TextInputForm textForm = new TextInputForm();
        // init has to be used because Winforms is setting the value of autoBackupTime before the form loads
        //   This causes it to be saved in the settings before the settings value is loaded.
        public bool init = false;

        public static void showImgMgr()
        {
            if (imgMgr == null || imgMgr.IsDisposed)
                imgMgr = new ImageManagerWindow();

            imgMgr.Show();
        }

        public LevelChooser()
        {
            InitializeComponent();
        }

        private void LevelChooser_Load(object sender, EventArgs e)
        {
            useMDI.Checked = Properties.Settings.Default.mdi;
            autoUpdate.Checked = Properties.Settings.Default.AutoUpdateSD;
            chkAutoBackup.Checked = Properties.Settings.Default.AutoBackup > 0;
            if (chkAutoBackup.Checked)
                autoBackupTime.Value = Properties.Settings.Default.AutoBackup;
            init = true;

            filesystemBrowser1.Load(ROM.FS);

            LoadLevelNames();
            if (ROM.UserInfo != null)
                musicList.Items.AddRange(ROM.UserInfo.getFullList("Music").ToArray());

            LanguageManager.ApplyToContainer(this, "LevelChooser");
            importLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
            exportLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
            openPatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
            savePatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
            this.Activate();
            //Get Language Files
			string langDir = System.IO.Path.Combine(Application.StartupPath, "Languages");
            if (System.IO.Directory.Exists(langDir)) {
                string[] files = System.IO.Directory.GetFiles(langDir);
                for (int l = 0; l < files.Length; l++) {
                    if (files[l].EndsWith(".ini")) {
                        int startPos = files[l].LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1;
                        languageListBox.Items.Add(files[l].Substring(startPos, files[l].LastIndexOf('.') - startPos));
                    }
                }
            }

            // Load filebackups from crash
            string backupPath = Path.Combine(Application.StartupPath, "Backup");
            if (ROM.fileBackups.Count > 0)
                foreach (string filename in ROM.fileBackups)
                    try
                    {
                        byte[] levelFile; byte[] bgFile; string[] error;
                        FileStream fs = new System.IO.FileStream(Path.Combine(backupPath, filename + ".nml"), FileMode.Open);
                        BinaryReader br = new BinaryReader(fs);
                        NSMBLevel.getImportLevel(out levelFile, out bgFile, out error, br);
                        br.Close();
                        if (error == null)
                        {
                            LevelEditor newEditor = new LevelEditor(filename, filename + " - Recovered Level", levelFile, bgFile);
                            newEditor.Show();
                        }
                    }
                    catch (Exception) { }


            this.Text = "NSMB Editor 5.2 Beta";
            label3.Text = "NSMB Editor 5.2 " + Properties.Resources.version.Trim();
            this.Icon = Properties.Resources.nsmbe;

            if (!ROM.isNSMBRom)
            {
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage5);
                tabControl1.TabPages.Remove(tabPage6);
                nsmbToolsGroupbox.Enabled = false;
                musicSlotsGrp.Enabled = false;
            }

//            new LevelEditor("A01_1", "LOL").Show();
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
            bool enabled = e.Node.Tag != null;
            importLevelButton.Enabled = enabled;
            exportLevelButton.Enabled = enabled;
            editLevelButton.Enabled = enabled;
            hexEditLevelButton.Enabled = enabled;
            importClipboard.Enabled = enabled;
            exportClipboard.Enabled = enabled;
        }

        private void editLevelButton_Click(object sender, EventArgs e)
        {
            // Make a caption
            string EditorCaption = "";

            if (levelTreeView.SelectedNode.Parent.Parent == null) {
                EditorCaption += levelTreeView.SelectedNode.Text;
            } else {
                EditorCaption += levelTreeView.SelectedNode.Parent.Text + ", " + levelTreeView.SelectedNode.Text;
            }

            // Open it
            try
            {
                LevelEditor NewEditor = new LevelEditor((string)levelTreeView.SelectedNode.Tag, EditorCaption);
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
            if (e.Node.Tag != null && editLevelButton.Enabled == true) {
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

        private void openLevel_Click(object sender, EventArgs e)
        {
            if (importLevelDialog.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
                return;
            byte[] levelFile; byte[] bgFile; string[] error;
            ushort LevelFileID, BGFileID;
            try {
                FileStream fs = new System.IO.FileStream(importLevelDialog.FileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);
                NSMBLevel.getImportLevel(out levelFile, out bgFile, out error, out LevelFileID, out BGFileID, br);
                br.Close();
                if (error == null) {
                    LevelEditor newEditor = new LevelEditor(importLevelDialog.FileName, levelFile, bgFile, LevelFileID, BGFileID);
                    newEditor.Show();
                } else
                    MessageBox.Show(error[0], error[1], MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch (Exception ex) {
                MessageBox.Show("Failed to open level file: " + ex.Message);
            }
        }

        private void importClipboard_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to overwrite this level?", "Replace level from clipboard", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                return;
            try
            {
                string leveltxt = Clipboard.GetText();
                if (!(leveltxt.StartsWith("NSMBeLevel|") && leveltxt.EndsWith("|")))
                    throw new Exception();
                leveltxt = leveltxt.Substring(11, leveltxt.Length - 12);
                byte[] levelfile = ROM.LZ77_Decompress(Convert.FromBase64String(leveltxt));
                ByteArrayInputStream strm = new ByteArrayInputStream(levelfile);
                BinaryReader br = new BinaryReader(strm);

                string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
                NSMBe4.DSFileSystem.File LevelFile = ROM.FS.getFileByName(LevelFilename + ".bin");
                NSMBe4.DSFileSystem.File BGFile = ROM.FS.getFileByName(LevelFilename + "_bgdat.bin");
                NSMBLevel.ImportLevel(LevelFile, BGFile, br);
                br.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clipboard did not contain a vaild level");
            }
        }

        private void exportClipboard_Click(object sender, EventArgs e)
        {
            string LevelFilename = (string)levelTreeView.SelectedNode.Tag;
            NSMBe4.DSFileSystem.File LevelFile = ROM.FS.getFileByName(LevelFilename + ".bin");
            NSMBe4.DSFileSystem.File BGFile = ROM.FS.getFileByName(LevelFilename + "_bgdat.bin");

            ByteArrayInputStream strm = new ByteArrayInputStream(new byte[0]);
            BinaryWriter bw = new BinaryWriter(strm);

            NSMBLevel.ExportLevel(LevelFile, BGFile, bw);
            Clipboard.SetText("NSMBeLevel|" + Convert.ToBase64String(ROM.LZ77_Compress(strm.getData())) + "|");
            bw.Close();
        }

        private void openClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                string leveltxt = Clipboard.GetText();
                if (!(leveltxt.StartsWith("NSMBeLevel|") && leveltxt.EndsWith("|")))
                    throw new Exception();
                leveltxt = leveltxt.Substring(11, leveltxt.Length - 12);
                byte[] leveldata = ROM.LZ77_Decompress(Convert.FromBase64String(leveltxt));
                ByteArrayInputStream strm = new ByteArrayInputStream(leveldata);
                BinaryReader br = new BinaryReader(strm);

                byte[] levelFile; byte[] bgFile; string[] error;
                ushort LevelFileID, BGFileID;

                NSMBLevel.getImportLevel(out levelFile, out bgFile, out error, out LevelFileID, out BGFileID, br);
                LevelEditor newEditor = new LevelEditor("", levelFile, bgFile, LevelFileID, BGFileID);
                newEditor.Show();
                br.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Clipboard did not contain a vaild level");
            }
        }

        private void changeLanguageButton_Click(object sender, EventArgs e) {
            if (languageListBox.SelectedItem != null) {
                Properties.Settings.Default.LanguageFile = languageListBox.SelectedItem.ToString();
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
            NitroROMFilesystem origROM = new NitroROMFilesystem(openROMDialog.FileName);

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
                        if (!f.isSystemFile)
                        {
                            Console.Out.WriteLine("Replace " + fileName);
                            f.beginEdit(this);
                            f.replace(newFile, this);
                            f.endEdit(this);
                        }
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
//            progress.Close();
        }

        private void mpPatch_Click(object sender, EventArgs e)
        {
            NarcReplace("Dat_Field.narc",    "J01_1.bin");
            NarcReplace("Dat_Basement.narc", "J02_1.bin");
            NarcReplace("Dat_Ice.narc",      "J03_1.bin");
            NarcReplace("Dat_Pipe.narc",     "J04_1.bin");
            NarcReplace("Dat_Fort.narc",     "J05_1.bin");
            NarcReplace("Dat_Field.narc",    "J01_1_bgdat.bin");
            NarcReplace("Dat_Basement.narc", "J02_1_bgdat.bin");
            NarcReplace("Dat_Ice.narc",      "J03_1_bgdat.bin");
            NarcReplace("Dat_Pipe.narc",     "J04_1_bgdat.bin");
            NarcReplace("Dat_Fort.narc",     "J05_1_bgdat.bin");

            MessageBox.Show(LanguageManager.Get("General", "Completed"));
        }

        /*
        private void patchNarcTilesets(string narc, string level, string prefix, string lowername, string uppername, string nscprefix, bool hasFG)
        {
            NSMBLevel lvl = new NSMBLevel(ROM.FS.getFileByName(level+".bin"), ROM.FS.getFileByName(level+"_bgdat.bin"), null);
            NarcReplace(narc, prefix + lowername + ".bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT));
            NarcReplace(narc, prefix + lowername + "_hd.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT_HD));
            NarcReplace(narc, uppername+"MainUnitChangeData.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_CHK));
            NarcReplace(narc, "d_2d_"+prefix+"tikei_"+lowername+"_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCG));
            NarcReplace(narc, "d_2d_"+prefix+"tikei_"+lowername+"_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCL));
            NarcReplace(narc, "d_2d_PA_"+prefix+lowername+".bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_PNL));

            NarcReplace(narc, "d_2d_I_M_back_nohara_VS_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NSC));
            NarcReplace(narc, "d_2d_I_M_back_nohara_VS_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCG));
            NarcReplace(narc, "d_2d_I_M_back_nohara_VS_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCL));

            NarcReplace(narc, "d_2d_I_M_free_nohara_VS_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_FG_NSC));
            NarcReplace(narc, "d_2d_I_M_free_nohara_VS_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_FG_NCG));
            NarcReplace(narc, "d_2d_I_M_free_nohara_VS_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_FG_NCL));

        }*/

        private void mpPatch2_Click(object sender, EventArgs e)
        {
            NSMBLevel lvl = new NSMBLevel(ROM.FS.getFileByName("J01_1.bin"), ROM.FS.getFileByName("J01_1_bgdat.bin"), null);
            NarcReplace("Dat_Field.narc", "I_M_nohara.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT));
            NarcReplace("Dat_Field.narc", "I_M_nohara_hd.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT_HD));
            NarcReplace("Dat_Field.narc", "d_2d_PA_I_M_nohara.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_PNL));
            NarcReplace("Dat_Field.narc", "NoHaRaMainUnitChangeData.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_CHK));
            NarcReplace("Dat_Field.narc", "d_2d_I_M_tikei_nohara_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCG));
            NarcReplace("Dat_Field.narc", "d_2d_I_M_tikei_nohara_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCL));

            NarcReplace("Dat_Field.narc", "d_2d_I_M_back_nohara_VS_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NSC));
            NarcReplace("Dat_Field.narc", "d_2d_I_M_back_nohara_VS_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCG));
            NarcReplace("Dat_Field.narc", "d_2d_I_M_back_nohara_VS_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCL));

            NarcReplace("Dat_Field.narc", "d_2d_I_M_free_nohara_VS_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NSC));
            NarcReplace("Dat_Field.narc", "d_2d_I_M_free_nohara_VS_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCG));
            NarcReplace("Dat_Field.narc", "d_2d_I_M_free_nohara_VS_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCL));


            lvl = new NSMBLevel(ROM.FS.getFileByName("J02_1.bin"), ROM.FS.getFileByName("J02_1_bgdat.bin"), null);
            NarcReplace("Dat_Basement.narc", "I_M_chika3.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT));
            NarcReplace("Dat_Basement.narc", "I_M_chika3_hd.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT_HD));
            NarcReplace("Dat_Basement.narc", "d_2d_PA_I_M_chika3.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_PNL));
            NarcReplace("Dat_Basement.narc", "ChiKa3MainUnitChangeData.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_CHK));
            NarcReplace("Dat_Basement.narc", "d_2d_I_M_tikei_chika3_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCG));
            NarcReplace("Dat_Basement.narc", "d_2d_I_M_tikei_chika3_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCL));

            NarcReplace("Dat_Basement.narc", "d_2d_I_M_back_chika3_R_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NSC));
            NarcReplace("Dat_Basement.narc", "d_2d_I_M_back_chika3_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCG));
            NarcReplace("Dat_Basement.narc", "d_2d_I_M_back_chika3_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCL));


            lvl = new NSMBLevel(ROM.FS.getFileByName("J03_1.bin"), ROM.FS.getFileByName("J03_1_bgdat.bin"), null);
            NarcReplace("Dat_Ice.narc", "I_M_setsugen2.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT));
            NarcReplace("Dat_Ice.narc", "I_M_setsugen2_hd.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT_HD));
            NarcReplace("Dat_Ice.narc", "d_2d_PA_I_M_setsugen2.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_PNL));
            NarcReplace("Dat_Ice.narc", "SeTsuGen2MainUnitChangeData.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_CHK));
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_tikei_setsugen2_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCG));
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_tikei_setsugen2_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCL));

            NarcReplace("Dat_Ice.narc", "d_2d_I_M_back_setsugen2_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NSC));
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_back_setsugen2_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCG));
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_back_setsugen2_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCL));

            NarcReplace("Dat_Ice.narc", "d_2d_I_M_free_setsugen2_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NSC));
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_free_setsugen2_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCG));
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_free_setsugen2_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCL));

            
            lvl = new NSMBLevel(ROM.FS.getFileByName("J04_1.bin"), ROM.FS.getFileByName("J04_1_bgdat.bin"), null);
            NarcReplace("Dat_Pipe.narc", "W_M_dokansoto.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT));
            NarcReplace("Dat_Pipe.narc", "W_M_dokansoto_hd.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT_HD));
            NarcReplace("Dat_Pipe.narc", "d_2d_PA_W_M_dokansoto.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_PNL));
            NarcReplace("Dat_Pipe.narc", "dokansotoMainUnitChangeData.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_CHK));
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_tikei_dokansoto_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCG));
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_tikei_dokansoto_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCL));

            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_back_dokansoto_R_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NSC));
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_back_dokansoto_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCG));
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_back_dokansoto_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCL));

            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_free_dokansoto_R_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NSC));
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_free_dokansoto_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCG));
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_free_dokansoto_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCL));


            lvl = new NSMBLevel(ROM.FS.getFileByName("J05_1.bin"), ROM.FS.getFileByName("J05_1_bgdat.bin"), null);
            NarcReplace("Dat_Fort.narc", "I_M_yakata.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT));
            NarcReplace("Dat_Fort.narc", "I_M_yakata_hd.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_UNT_HD));
            NarcReplace("Dat_Fort.narc", "d_2d_PA_I_M_yakata.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_PNL));
            NarcReplace("Dat_Fort.narc", "YaKaTaMainUnitChangeData.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_CHK));
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_tikei_yakata_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCG));
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_tikei_yakata_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0xC], ROM.Data.Table_TS_NCL));

            NarcReplace("Dat_Fort.narc", "d_2d_I_M_back_yakata_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NSC));
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_back_yakata_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCG));
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_back_yakata_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x6], ROM.Data.Table_BG_NCL));

            NarcReplace("Dat_Fort.narc", "d_2d_I_M_free_yakata_UR_nsc.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NSC));
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_free_yakata_ncg.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCG));
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_free_yakata_ncl.bin", ROM.GetFileIDFromTable(lvl.Blocks[0][0x12], ROM.Data.Table_FG_NCL));

            MessageBox.Show(LanguageManager.Get("General", "Completed"));
        }

        //WTF was this for?!
        //private void NarcReplace(string NarcName, string f1, string f2) { }

        private void NarcReplace(string NarcName, string f1, ushort f2)
        {
            NarcFilesystem fs = new NarcFilesystem(ROM.FS.getFileByName(NarcName));

            NSMBe4.DSFileSystem.File f = fs.getFileByName(f1);
            if (f == null)
                Console.Out.WriteLine("No File: " + NarcName + "/" + f1);
            else
            {
                f.beginEdit(this);
                f.replace(ROM.FS.getFileById(f2).getContents(), this);
                f.endEdit(this);
            }
            fs.close();            
        }

        private void NarcReplace(string NarcName, string f1)
        {
            NarcFilesystem fs = new NarcFilesystem(ROM.FS.getFileByName(NarcName));

            NSMBe4.DSFileSystem.File f = fs.getFileByName(f1);
            f.beginEdit(this);
            f.replace(ROM.FS.getFileByName(f1).getContents(), this);
            f.endEdit(this);

            fs.close();
        }

        private void tilesetEditor_Click(object sender, EventArgs e)
        {
            new TilesetChooser().Show();
        }

        private void decompArm9Bin_Click(object sender, EventArgs e)
        {
            Arm9BinaryHandler bh = new Arm9BinaryHandler(ROM.FS);
            bh.decompress();
        }

        private void padarm7bin_Click(object sender, EventArgs e)
        {
            PatchMaker pm = new PatchMaker(ROM.romfile.Directory);
            pm.compilePatch();
            pm.generatePatch();
        }

        private void parseFileListBtn_Click(object sender, EventArgs e)
        {
            if (openTextFileDialog.ShowDialog() != DialogResult.OK)
                return;

            StreamReader sr = new StreamReader(openTextFileDialog.OpenFile());
            StreamWriter sw = new StreamWriter(openTextFileDialog.FileName + ".parsed.txt");
            while (!sr.EndOfStream)
            {
                string l = sr.ReadLine();

                string addrs, sizs;

                if (l.Length == 22)
                {
                    addrs = l.Substring(5, 8);
                    sizs = l.Substring(14, 8);
                }
                else if (l.Length == 22 + 8 * 2 + 2)
                {
                    addrs = l.Substring(5 + 18, 8);
                    sizs = l.Substring(14 + 18, 8);
                }
                else
                {
                    sw.WriteLine(l);
                    continue;
                }

                int addr = int.Parse(addrs, System.Globalization.NumberStyles.HexNumber);
                int siz = int.Parse(sizs, System.Globalization.NumberStyles.HexNumber);

                NSMBe4.DSFileSystem.File found = null;
                foreach (NSMBe4.DSFileSystem.File f in ROM.FS.allFiles)
                {
                    if (f.isAddrInFile(addr))
                        found = f;
                }

                sw.Write(l + " ");
                int fileoffs = addr - found.fileBegin;
                if (fileoffs == 0 && siz == found.fileSize)
                    //        12345678 
                    sw.Write("ALL      ");
                else
                    sw.Write(fileoffs.ToString("X8")+" ");
                sw.Write(found.name);
                sw.WriteLine();
            }

            sw.Close();
            sr.Close();
        }

        private void encryptFAT_Click(object sender, EventArgs e)
        {
            //LOL. Nothing for you here.
            String path = ROM.romfile.Directory.FullName;
            FileStream fs = new FileStream(path + "/introload.arm9", FileMode.Open);
            byte[] arm9 = new byte[fs.Length];
            fs.Read(arm9, 0, (int)fs.Length);
            fs.Close();

            byte[] oldarm9 = ROM.FS.arm9binFile.getContents();
            Array.Copy(oldarm9, 0, arm9, 0, 0x800); //Weird secure area shit??
            ROM.FS.arm9binFile.beginEdit(this);
            ROM.FS.arm9binFile.replace(arm9, this);
            ROM.FS.arm9binFile.endEdit(this);

            fs = new FileStream(path + "/introload.ewram.arm7", FileMode.Open);
            byte[] arm7 = new byte[fs.Length];
            fs.Read(arm7, 0, (int)fs.Length);
            fs.Close();

            ROM.FS.arm7binFile.beginEdit(this);
            ROM.FS.arm7binFile.replace(arm7, this);
            ROM.FS.arm7binFile.endEdit(this);
        }

        private void dumpMapButton_Click(object sender, EventArgs e)
        {
            if (saveTextFileDialog.ShowDialog() != DialogResult.OK) return;

            TextWriter tw = new StreamWriter(new FileStream(saveTextFileDialog.FileName, FileMode.Create, FileAccess.ReadWrite));
            ROM.FS.dumpFilesOrdered(tw);
            tw.Close();
        }

        private void insertRomButton_Click(object sender, EventArgs e)
        {
            if (openROMDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = openROMDialog.FileName;
                FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                byte[] data = new byte[fs.Length];
                fs.Read(data, 0, (int)fs.Length);
                fs.Close();

                /*
                int arm9offs = data[0x22] | data[0x23] << 8;
                arm9offs += 0x01F0;
                data[0x22] = (byte)(arm9offs >> 0);
                data[0x23] = (byte)(arm9offs >> 8);

                int arm7offs = data[0x32] | data[0x33] << 8;
                arm7offs += 0x01F0;
                data[0x32] = (byte)(arm7offs >> 0);
                data[0x33] = (byte)(arm7offs >> 8);*/

                Stream s = ROM.FS.s;
                s.Seek(0x1F00000, SeekOrigin.Begin);
                s.Write(data, 0, data.Length);
                
            }
        }

        private void useMDI_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.mdi = useMDI.Checked;
            Properties.Settings.Default.Save();
        }

        private void autoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoUpdateSD = autoUpdate.Checked;
            Properties.Settings.Default.Save();
        }

        private void bootInsertButton_Click(object sender, EventArgs e)
        {
            DirectoryInfo dir = new DirectoryInfo(ROM.romfile.Directory.FullName + "/loader");
            PatchCompiler.compilePatch(0x02000C00, dir);
            ROM.FS.arm9binFile.beginEdit(this);

            byte[] data = ROM.FS.arm9binFile.getContents();


            FileInfo f = new FileInfo(dir.FullName + "/newcode.bin");
            FileStream fs = f.OpenRead();
            byte[] newdata = new byte[fs.Length];
            fs.Read(newdata, 0, (int)fs.Length);
            fs.Close();

            Array.Copy(newdata, 0, data, 0xC00, newdata.Length);
            ROM.FS.arm9binFile.replace(data, this);
            ROM.FS.arm9binFile.endEdit(this);
        }

        private void LevelChooser_FormClosing(object sender, FormClosingEventArgs e)
        {
            ROM.close();
            Console.Out.WriteLine(e.CloseReason.ToString());
            if (MdiParentForm.instance != null && e.CloseReason != CloseReason.MdiFormClosing)
                MdiParentForm.instance.Close();
            Properties.Settings.Default.BackupFiles = "";
            Properties.Settings.Default.Save();
        }

        private void makeclean_Click(object sender, EventArgs e)
        {
            PatchCompiler.cleanPatch(ROM.romfile.Directory);
        }

        private void updateSpriteDataButton_Click(object sender, EventArgs e)
        {
            SpriteData.update();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/Dirbaio/NSMB-Editor");
        }

        private void linkLabel2_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://nsmbhd.net/");
        }

        private void renameBtn_Click(object sender, EventArgs e)
        {
            if (musicList.SelectedIndex == -1)
                return;
            string newName;
            string oldName = musicList.SelectedItem.ToString();
            oldName = oldName.Substring(oldName.IndexOf(" ") + 1);
            if (textForm.ShowDialog("Enter new music name:", oldName, out newName) == DialogResult.OK)
            {
                if (newName == string.Empty)
                {
                    ROM.UserInfo.removeListItem("Music", musicList.SelectedIndex, true);
                    musicList.Items[musicList.SelectedIndex] = string.Format("{0:X2}: {1}", ROM.MusicNumbers[musicList.SelectedIndex], LanguageManager.GetList("Music")[musicList.SelectedIndex]);
                    return;
                }
                ROM.UserInfo.setListItem("Music", musicList.SelectedIndex, newName, true);
                musicList.Items[musicList.SelectedIndex] = string.Format("{0:X2}: {1}", ROM.MusicNumbers[musicList.SelectedIndex], newName);
            }
        }

        private void autoBackupTime_ValueChanged(object sender, EventArgs e)
        {
            if (init)
            {
                Properties.Settings.Default.AutoBackup = chkAutoBackup.Checked ? (int)autoBackupTime.Value : 0;
                Properties.Settings.Default.Save();
            }
        }

        private void deleteBackups_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete all level backups?", "Delete backups?", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                System.IO.Directory.Delete(Path.Combine(Application.StartupPath, "Backup"), true);
        }
    }
}
