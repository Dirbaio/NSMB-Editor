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
using Be.Windows.Forms;
using NSMBe4.DSFileSystem;


namespace NSMBe4 
{
    public partial class LevelHexEditor : Form
    {
        File LevelFile;

        public LevelHexEditor(string LevelFilename)
        {
            InitializeComponent();
#if MDI
            this.MdiParent = MdiParentForm.instance;
#endif
            this.LevelFilename = LevelFilename;

            LevelFile = ROM.FS.getFileByName(LevelFilename + ".bin");
            LevelFile.beginEdit(this);
            byte[] eLevelFile = LevelFile.getContents();
            Blocks = new byte[][] { null, null, null, null, null, null, null, null, null, null, null, null, null, null };

            int FilePos = 0;
            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                int BlockOffset = eLevelFile[FilePos] | (eLevelFile[FilePos + 1] << 8) | (eLevelFile[FilePos + 2] << 16) | eLevelFile[FilePos + 3] << 24;
                FilePos += 4;
                int BlockSize = eLevelFile[FilePos] | (eLevelFile[FilePos + 1] << 8) | (eLevelFile[FilePos + 2] << 16) | eLevelFile[FilePos + 3] << 24;
                FilePos += 4;

                Blocks[BlockIdx] = new byte[BlockSize];
                Array.Copy(eLevelFile, BlockOffset, Blocks[BlockIdx], 0, BlockSize);
            }

            LoadBlock(0);
        }

        private void LevelHexEditor_Load(object sender, EventArgs e) {
            LanguageManager.ApplyToContainer(this, "LevelHexEditor");
        }

        public string LevelFilename;
        private bool Dirty;
        private bool DataUpdateFlag;
        private byte[][] Blocks;
        private int BlockID;

        public bool ForceClose() {
            if (Dirty) {
                DialogResult dr;
                dr = MessageBox.Show(LanguageManager.Get("LevelHexEditor", "UnsavedLevel"), LanguageManager.Get("General", "Question"), MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                if (dr == DialogResult.Yes) {
                    Save();
                } else if (dr == DialogResult.Cancel) {
                    return true;
                }
            }

            return false;
        }

        private bool LoadBlock(int BlockID) {
            if (ForceClose()) return false;

            DataUpdateFlag = true;
            this.BlockID = BlockID;
            hexBox1.ByteProvider = new DynamicByteProvider(Blocks[BlockID]);
            ((DynamicByteProvider)hexBox1.ByteProvider).Changed += new EventHandler(LevelHexEditor_Changed);
            blockComboBox.SelectedIndex = BlockID;
            DataUpdateFlag = false;

            return true;
        }

        private void LevelHexEditor_Changed(object sender, EventArgs e) {
            Dirty = true;
        }

        private void Save() {
            Dirty = false;
            Blocks[BlockID] = ((DynamicByteProvider)hexBox1.ByteProvider).Bytes.ToArray();

            int LevelFileSize = 8 * 14;

            // Find out how long the file must be
            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                LevelFileSize += Blocks[BlockIdx].Length;
            }

            // Now allocate + save it
            int FilePos = 0;
            int CurBlockOffset = 8 * 14;
            byte[] LevelFileData = new byte[LevelFileSize];

            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                LevelFileData[FilePos] = (byte)(CurBlockOffset & 0xFF);
                LevelFileData[FilePos + 1] = (byte)((CurBlockOffset >> 8) & 0xFF);
                LevelFileData[FilePos + 2] = (byte)((CurBlockOffset >> 16) & 0xFF);
                LevelFileData[FilePos + 3] = (byte)((CurBlockOffset >> 24) & 0xFF);
                LevelFileData[FilePos + 4] = (byte)(Blocks[BlockIdx].Length & 0xFF);
                LevelFileData[FilePos + 5] = (byte)((Blocks[BlockIdx].Length >> 8) & 0xFF);
                LevelFileData[FilePos + 6] = (byte)((Blocks[BlockIdx].Length >> 16) & 0xFF);
                LevelFileData[FilePos + 7] = (byte)((Blocks[BlockIdx].Length >> 24) & 0xFF);
                FilePos += 8;
                Array.Copy(Blocks[BlockIdx], 0, LevelFileData, CurBlockOffset, Blocks[BlockIdx].Length);
                CurBlockOffset += Blocks[BlockIdx].Length;
            }

            LevelFile.replace(LevelFileData, this);
        }

        private void blockComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (DataUpdateFlag) return;

            int OldBlockID = BlockID;
            if (!LoadBlock(blockComboBox.SelectedIndex)) {
                DataUpdateFlag = true;
                blockComboBox.SelectedIndex = OldBlockID;
                DataUpdateFlag = false;
            }
        }

        private void saveBlockButton_Click(object sender, EventArgs e) {
            Save();
        }

        private void LevelHexEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            LevelFile.endEdit(this);
        }
    }
}
