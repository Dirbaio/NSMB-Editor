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


namespace NSMBe4 {
    public partial class LevelChooser : Form
    {
        private List<LevelEditor> editors;

        public LevelChooser() {
            InitializeComponent();
        }

        private void LevelChooser_Load(object sender, EventArgs e) {
            openROMDialog.Filter = LanguageManager.Get("LevelChooser", "ROMFilter");
            if (openROMDialog.ShowDialog() == DialogResult.Cancel) {
                Application.Exit();
            } else {
                editors = new List<LevelEditor>();

                importLevelButton.Enabled = false;
                exportLevelButton.Enabled = false;
                editLevelButton.Enabled = false;
                hexEditLevelButton.Enabled = false;

                ROM.load(openROMDialog.FileName);
                filesystemBrowser1.Load(ROM.FS);

                LoadLevelNames();

                LanguageManager.ApplyToContainer(this, "LevelChooser");
                importLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
                exportLevelDialog.Filter = LanguageManager.Get("LevelChooser", "LevelFilter");
                openPatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
                savePatchDialog.Filter = LanguageManager.Get("LevelChooser", "PatchFilter");
                this.Activate();
            }
            /*
            List<string> spriteNames = LanguageManager.GetList("Sprites");
            
            for (int i = 0; i < 370; i++)
            {
                Console.Out.Write(i.ToString("X2") + ": ");
                for (int j = 0; j <= 323; j++)
                {
                    int offs = ROM.GetOffset(ROM.Data.Table_Sprite_CLASSID) + j*2;
                    int classid = ROM.Overlay0[offs] | ROM.Overlay0[offs + 1] << 8;
                    if (classid == i)
                        Console.Out.Write(spriteNames[j]);
                    if (classid == 325)
                        spriteNames[j] = j + "=Nothing";
                }
                Console.Out.WriteLine();
            }*/
//            ROM.FS.dumpFilesOrdered();
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

        private void loadROMButton_Click(object sender, EventArgs e)
        {
            openROMDialog.Filter = LanguageManager.Get("LevelChooser", "ROMFilter");
            if (openROMDialog.ShowDialog() == DialogResult.Cancel) {
                return;
            }
            else {
                for (int l = editors.Count - 1; l >= 0; l--)
                {
                    if (l < editors.Count - 1)
                        return;
                    editors[l].Close();
                }
                ROM.close();
                ROM.load(openROMDialog.FileName);
                filesystemBrowser1 = new FilesystemBrowser();
                filesystemBrowser1.Load(ROM.FS);
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
                editors.Add(NewEditor);
                NewEditor.FormClosed += new FormClosedEventHandler(editorClosing);
                NewEditor.Text = EditorCaption;
                NewEditor.Show();
            }
            catch (AlreadyEditingException)
            {
                MessageBox.Show(LanguageManager.Get("Errors", "Level"));
            }                
        }

        private void editorClosing(object sender, FormClosedEventArgs e)
        {
            editors.Remove(sender as LevelEditor);
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
            NarcReplace("Dat_Field.narc",    "J01_1.bin", "J01_1_bgdat.bin");
            NarcReplace("Dat_Basement.narc", "J02_1.bin", "J02_1_bgdat.bin");
            NarcReplace("Dat_Ice.narc",      "J03_1.bin", "J03_1_bgdat.bin");
            NarcReplace("Dat_Pipe.narc",     "J04_1.bin", "J04_1_bgdat.bin");
            NarcReplace("Dat_Fort.narc",     "J05_1.bin", "J05_1_bgdat.bin");

            MessageBox.Show(LanguageManager.Get("General", "Completed"));
        }

        private void mpPatch2_Click(object sender, EventArgs e)
        {

            NarcReplace("Dat_Field.narc", "I_M_nohara.bin", "I_M_nohara_hd.bin");
            NarcReplace("Dat_Field.narc", "NoHaRaMainUnitChangeData.bin", "d_2d_I_M_back_nohara_VS_UR_nsc.bin");
            NarcReplace("Dat_Field.narc", "d_2d_I_M_back_nohara_VS_ncg.bin", "d_2d_I_M_back_nohara_VS_ncl.bin");
            NarcReplace("Dat_Field.narc", "d_2d_I_M_free_nohara_VS_UR_nsc.bin", "d_2d_I_M_free_nohara_VS_ncg.bin");
            NarcReplace("Dat_Field.narc", "d_2d_I_M_free_nohara_VS_ncl.bin", "");
            NarcReplace("Dat_Field.narc", "d_2d_I_M_tikei_nohara_ncg.bin", "d_2d_I_M_tikei_nohara_ncl.bin");
            NarcReplace("Dat_Field.narc", "d_2d_PA_I_M_nohara.bin", "");

            NarcReplace("Dat_Basement.narc", "I_M_chika3.bin", "I_M_chika3_hd.bin");
            NarcReplace("Dat_Basement.narc", "ChiKa3MainUnitChangeData.bin", "d_2d_I_M_back_chika3_R_nsc.bin");
            NarcReplace("Dat_Basement.narc", "d_2d_I_M_back_chika3_ncg.bin", "d_2d_I_M_back_chika3_ncl.bin");
            NarcReplace("Dat_Basement.narc", "d_2d_I_M_tikei_chika3_ncg.bin", "d_2d_I_M_tikei_chika3_ncl.bin");
            NarcReplace("Dat_Basement.narc", "d_2d_PA_I_M_chika3.bin", "");

            NarcReplace("Dat_Ice.narc", "I_M_setsugen2.bin", "I_M_setsugen2_hd.bin");
            NarcReplace("Dat_Ice.narc", "SeTsuGeN2MainUnitChangeData.bin", "d_2d_I_M_back_setsugen2_UR_nsc.bin");
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_back_setsugen2_ncg.bin", "d_2d_I_M_back_setsugen2_ncl.bin");
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_free_setsugen2_UR_nsc.bin", "d_2d_I_M_free_setsugen2_ncg.bin");
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_free_setsugen2_ncl.bin", "");
            NarcReplace("Dat_Ice.narc", "d_2d_I_M_tikei_setsugen2_ncg.bin", "d_2d_I_M_tikei_setsugen2_ncl.bin");
            NarcReplace("Dat_Ice.narc", "d_2d_PA_I_M_setsugen2.bin", "");

            NarcReplace("Dat_Pipe.narc", "W_M_dokansoto.bin", "W_M_dokansoto_hd.bin");
            NarcReplace("Dat_Pipe.narc", "DoKaNSoToMainUnitChangeData.bin", "d_2d_W_M_back_dokansoto_R_nsc.bin");
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_back_dokansoto_ncg.bin", "d_2d_W_M_back_dokansoto_ncl.bin");
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_free_dokansoto_R_nsc.bin", "d_2d_W_M_free_dokansoto_ncg.bin");
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_free_dokansoto_ncl.bin", "");
            NarcReplace("Dat_Pipe.narc", "d_2d_W_M_tikei_dokansoto_ncg.bin", "d_2d_W_M_tikei_dokansoto_ncl.bin");
            NarcReplace("Dat_Pipe.narc", "d_2d_PA_W_M_dokansoto.bin", "");

            NarcReplace("Dat_Fort.narc", "I_M_yakata.bin", "I_M_yakata_hd.bin");
            NarcReplace("Dat_Fort.narc", "YaKaTaMainUnitChangeData.bin", "d_2d_I_M_back_yakata_UR_nsc.bin");
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_back_yakata_ncg.bin", "d_2d_I_M_back_yakata_ncl.bin");
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_free_yakata_UR_nsc.bin", "d_2d_I_M_free_yakata_ncg.bin");
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_free_yakata_ncl.bin", "");
            NarcReplace("Dat_Fort.narc", "d_2d_I_M_tikei_yakata_ncg.bin", "d_2d_I_M_tikei_yakata_ncl.bin");
            NarcReplace("Dat_Fort.narc", "d_2d_PA_I_M_yakata.bin", "d_2d_TEN_I_yakata_ncg.bin");

            MessageBox.Show(LanguageManager.Get("General", "Completed"));
        }

        private void NarcReplace(string NarcName, string f1, string f2)
        {
            NarcFilesystem fs = new NarcFilesystem(ROM.FS.getFileByName(NarcName));

            NSMBe4.DSFileSystem.File f = fs.getFileByName(f1);
            if (f == null)
            {
                MessageBox.Show(f2);
            }
            f.beginEdit(this);
            f.replace(ROM.FS.getFileByName(f1).getContents(), this);
            f.endEdit(this);

            if (f2 != "")
            {
                f = fs.getFileByName(f2);
                if (f == null)
                {
                    MessageBox.Show(f2);
                }
                f.beginEdit(this);
                f.replace(ROM.FS.getFileByName(f2).getContents(), this);
                f.endEdit(this);
            }

            fs.close();
        }

        private void tilesetEditor_Click(object sender, EventArgs e)
        {
            new TilesetChooser().Show();
        }

        private void decompArm9Bin_Click(object sender, EventArgs e)
        {
            NSMBe4.DSFileSystem.File arm9 = ROM.FS.arm9binFile;
            arm9.beginEdit(this);

            int decompressionOffs = (int) arm9.getUintAt(0xB5C);
            if (decompressionOffs != 0)
            {
                decompressionOffs -= 0x02000000;
                int compDatSize = (int)(arm9.getUintAt(decompressionOffs - 8) & 0xFFFFFF);
                int compDatOffs = decompressionOffs - compDatSize;
                Console.Out.WriteLine("OFFS: " + compDatOffs.ToString("X"));
                Console.Out.WriteLine("SIZE: " + compDatSize.ToString("X"));

                byte[] data = arm9.getContents();
                byte[] compData = new byte[compDatSize];
                Array.Copy(data, compDatOffs, compData, 0, compDatSize);
                byte[] decompData = ROM.DecompressOverlay(compData);
                byte[] newData = new byte[data.Length - compData.Length + decompData.Length];
                Array.Copy(data, newData, data.Length);
                Array.Copy(decompData, 0, newData, compDatOffs, decompData.Length);

                arm9.replace(newData, this);
                arm9.setUintAt(0xB5C, 0); // NUKE THE COMPRESSION!!! :P
                arm9.endEdit(this);
            }
        }

    }
}
