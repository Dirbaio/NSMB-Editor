using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace NSMBe4 {
    public partial class LevelHexEditor : Form {
        public LevelHexEditor(NitroClass ROM, string LevelFilename) {
            InitializeComponent();
            this.ROM = ROM;
            this.LevelFilename = LevelFilename;

            ushort LevelFileID = ROM.FileIDs[LevelFilename + ".bin"];
            byte[] eLevelFile = ROM.ExtractFile(LevelFileID);
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
            if (Properties.Settings.Default.Language == 1) {
                saveBlockButton.Text = "Guardar Bloque";
                toolStripLabel1.Text = "Bloque:";
            }
        }

        private NitroClass ROM;
        public string LevelFilename;
        private bool Dirty;
        private bool DataUpdateFlag;
        private byte[][] Blocks;
        private int BlockID;

        public bool ForceClose() {
            if (Dirty) {
                DialogResult dr;
                if (Properties.Settings.Default.Language != 1) {
                    dr = MessageBox.Show("This level contains unsaved changes.\nIf you close the editor without saving, you will lose them.\nDo you want to save?", "NSMB Editor 4", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                } else {
                    dr = MessageBox.Show("Este nivel tiene cambios sin guardar.\nSi cierras el editor sin guardarlo, los pierderas.\nQuiere guardarlos?", "NSMB Editor 4", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                }
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

            ushort LevelFileID = ROM.FileIDs[LevelFilename + ".bin"];
            int LevelFileSize = 8 * 14;

            // Find out how long the file must be
            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                LevelFileSize += Blocks[BlockIdx].Length;
            }

            // Now allocate + save it
            int FilePos = 0;
            int CurBlockOffset = 8 * 14;
            byte[] LevelFile = new byte[LevelFileSize];

            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                LevelFile[FilePos] = (byte)(CurBlockOffset & 0xFF);
                LevelFile[FilePos + 1] = (byte)((CurBlockOffset >> 8) & 0xFF);
                LevelFile[FilePos + 2] = (byte)((CurBlockOffset >> 16) & 0xFF);
                LevelFile[FilePos + 3] = (byte)((CurBlockOffset >> 24) & 0xFF);
                LevelFile[FilePos + 4] = (byte)(Blocks[BlockIdx].Length & 0xFF);
                LevelFile[FilePos + 5] = (byte)((Blocks[BlockIdx].Length >> 8) & 0xFF);
                LevelFile[FilePos + 6] = (byte)((Blocks[BlockIdx].Length >> 16) & 0xFF);
                LevelFile[FilePos + 7] = (byte)((Blocks[BlockIdx].Length >> 24) & 0xFF);
                FilePos += 8;
                Array.Copy(Blocks[BlockIdx], 0, LevelFile, CurBlockOffset, Blocks[BlockIdx].Length);
                CurBlockOffset += Blocks[BlockIdx].Length;
            }

            ROM.ReplaceFile(LevelFileID, LevelFile);
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
    }
}
