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
using NSMBe4.DSFileSystem;


namespace NSMBe4 {
    public partial class LevelConfig : Form {
        public LevelConfig(LevelEditorControl EdControl) {
            InitializeComponent();
            this.EdControl = EdControl;
            this.Level = EdControl.Level;
            if (Properties.Settings.Default.mdi)
                this.MdiParent = MdiParentForm.instance;
            tabControl1.SelectTab(0);

            LanguageManager.ApplyToContainer(this, "LevelConfig");

            // Load lists
            loadList("Foregrounds", bgTopLayerComboBox);
            loadList("Backgrounds", bgBottomLayerComboBox);
            loadList("Tilesets", tilesetComboBox);

            // Load modifier lists
            ComboBox target = null;
            foreach (string name in LanguageManager.GetList("Modifiers")) {
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                if (trimmedname[0] == '-') {
                    switch (trimmedname) {
                        case "-1": target = set1ComboBox; break;
                        case "-2": target = set2ComboBox; break;
                        case "-3": target = set3ComboBox; break;
                        case "-4": target = set4ComboBox; break;
                        case "-5": target = set5ComboBox; break;
                        case "-6": target = set6ComboBox; break;
                        case "-7": target = set7ComboBox; break;
                        case "-8": target = set8ComboBox; break;
                        case "-9": target = set9ComboBox; break;
                        case "-10": target = set10ComboBox; break;
                        case "-16": target = set16ComboBox; break;
                    }
                } else {
                    target.Items.Add(trimmedname);
                }
            }
        }

        private NSMBLevel Level;
        private LevelEditorControl EdControl;

        public delegate void ReloadTilesetDelegate();
        public event ReloadTilesetDelegate ReloadTileset;

        public delegate void RefreshMainWindowDelegate();
        public event RefreshMainWindowDelegate RefreshMainWindow;

        private void loadList(string name, ComboBox dest)
        {
            dest.Items.Clear();
            foreach (string s in LanguageManager.GetList(name))
            {
                string ss = s.Trim();
                if (ss == "") continue;
                dest.Items.Add(ss);
            }
        }

        public void LoadSettings() {
            startEntranceUpDown.Value = Level.Blocks[0][0];
            midwayEntranceUpDown.Value = Level.Blocks[0][1];
            timeLimitUpDown.Value = Level.Blocks[0][4] | (Level.Blocks[0][5] << 8);
            soundSetUpDown.Value = Level.Blocks[0][26];
            levelWrapCheckBox.Checked = ((Level.Blocks[0][2] & 0x20) != 0);
            forceMiniCheckBox.Checked = ((Level.Blocks[0][2] & 0x01) != 0);
            miniMarioPhysicsCheckBox.Checked = ((Level.Blocks[0][2] & 0x02) != 0);

            tilesetComboBox.SelectedIndex = Level.Blocks[0][0xC];
            int FGIndex = Level.Blocks[0][0x12];
            if (FGIndex == 255) FGIndex = bgTopLayerComboBox.Items.Count - 1;
            bgTopLayerComboBox.SelectedIndex = FGIndex;
            int BGIndex = Level.Blocks[0][6];
            if (BGIndex == 255) BGIndex = bgBottomLayerComboBox.Items.Count - 1;
            bgBottomLayerComboBox.SelectedIndex = BGIndex;

            ComboBox[] checkthese = new ComboBox[] {
                set1ComboBox, set2ComboBox, set3ComboBox, set4ComboBox,
                set5ComboBox, set6ComboBox, set7ComboBox, set8ComboBox,
                set9ComboBox, set10ComboBox, set16ComboBox
            };

            int[] checkthese_idx = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 15 };

            if (Level.Blocks[13].Length == 0) {
                // works around levels like 1-4 area 2 which have a blank modifier block
                Level.Blocks[13] = new byte[16];
            }

            for (int CheckIdx = 0; CheckIdx < checkthese.Length; CheckIdx++) {
                int valid = Level.Blocks[13][checkthese_idx[CheckIdx]];
                for (int ItemIdx = 0; ItemIdx < checkthese[CheckIdx].Items.Count; ItemIdx++) {
                    string Item = (string)(checkthese[CheckIdx].Items[ItemIdx]);
                    int cpos = Item.IndexOf(':');
                    int modifierval = int.Parse(Item.Substring(0, cpos));
                    if (modifierval == valid) {
                        checkthese[CheckIdx].SelectedIndex = ItemIdx;
                        break;
                    }
                }
            }

        }

        #region Previews
        private void tilesetPreviewButton_Click(object sender, EventArgs e) {
            ushort GFXFileID = ROM.GetFileIDFromTable(tilesetComboBox.SelectedIndex, ROM.Data.Table_TS_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(tilesetComboBox.SelectedIndex, ROM.Data.Table_TS_NCL);

            GraphicsViewer gv = new GraphicsViewer();
            gv.SetPreferredWidth(256);
            gv.SetFile(ROM.FS.getFileById(GFXFileID).getContents());
            gv.SetPalette(ROM.FS.getFileById(PalFileID).getContents());
            gv.Show();
        }

        private void bgTopLayerPreviewButton_Click(object sender, EventArgs e) {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1) {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            new ImagePreviewer(RenderBackground(GFXFile, PalFile, LayoutFile, 256)).Show();
        }

        private void bgBottomLayerPreviewButton_Click(object sender, EventArgs e) {
            if (bgBottomLayerComboBox.SelectedIndex == bgBottomLayerComboBox.Items.Count - 1) {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NSC);
            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null) {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            new ImagePreviewer(RenderBackground(GFXFile, PalFile, LayoutFile, 576)).Show();
        }

        private Bitmap RenderBackground(File GFXFile, File PalFile, File LayoutFile, int TileOffset)
        {
            int FilePos;

            // First get the palette out
            byte[] ePalFile = ROM.LZ77_Decompress(PalFile.getContents());
            Color[] Palette = new Color[512];

            for (int PalIdx = 0; PalIdx < 512; PalIdx++) {
                int ColourVal = ePalFile[PalIdx * 2] + (ePalFile[(PalIdx * 2) + 1] << 8);
                int cR = (ColourVal & 31) * 8;
                int cG = ((ColourVal >> 5) & 31) * 8;
                int cB = ((ColourVal >> 10) & 31) * 8;
                Palette[PalIdx] = Color.FromArgb(cR, cG, cB);
            }

            Palette[0] = Color.Transparent;
            Palette[256] = Color.Transparent;

            // Load graphics
            byte[] eGFXFile = ROM.LZ77_Decompress(GFXFile.getContents());
            int TileCount = eGFXFile.Length / 64;
            Bitmap TilesetBuffer = new Bitmap(TileCount * 8, 16);

            FilePos = 0;
            for (int TileIdx = 0; TileIdx < TileCount; TileIdx++)
            {
                int TileSrcX = TileIdx * 8;
                for (int TileY = 0; TileY < 8; TileY++)
                {
                    for (int TileX = 0; TileX < 8; TileX++)
                    {
                        TilesetBuffer.SetPixel(TileSrcX + TileX, TileY, Palette[eGFXFile[FilePos]]);
                        TilesetBuffer.SetPixel(TileSrcX + TileX, TileY + 8, Palette[eGFXFile[FilePos] + 256]);
                        FilePos++;
                    }
                }
            }

            // Load layout
            byte[] eLayoutFile = ROM.LZ77_Decompress(LayoutFile.getContents());
            int LayoutCount = eLayoutFile.Length / 2;
            Bitmap BG = new Bitmap(512, 512, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics BGGraphics = Graphics.FromImage(BG);
            BGGraphics.Clear(Color.Transparent);

            FilePos = 0;
            int TileNum;
            byte ControlByte;
            Rectangle SrcRect;
            int SrcX = 0;
            int SrcY = 0;
            Bitmap fliptile = new Bitmap(8, 8);
            Graphics g = Graphics.FromImage(fliptile);
            for (int TileIdx = 0; TileIdx < LayoutCount; TileIdx++) {
                TileNum = eLayoutFile[FilePos];
                ControlByte = eLayoutFile[FilePos + 1];
                TileNum |= (ControlByte & 3) << 8;
                TileNum -= TileOffset;
                SrcRect = new Rectangle(TileNum * 8, (ControlByte & 16) != 0 ? 8 : 0, 8, 8);
                if ((ControlByte & 4) != 0 || (ControlByte & 8) != 0) {
                    g.DrawImage(TilesetBuffer, 0, 0, SrcRect, GraphicsUnit.Pixel);
                    if ((ControlByte & 4) != 0)
                        fliptile.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    if ((ControlByte & 8) != 0)
                        fliptile.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    BGGraphics.DrawImage(fliptile, SrcX, SrcY);
                } else {
                    BGGraphics.DrawImage(TilesetBuffer, SrcX, SrcY, SrcRect, GraphicsUnit.Pixel);
                }
                SrcX += 8;
                if (SrcX >= 512) { SrcX = 0; SrcY += 8; }
                FilePos += 2;
            }

            return BG;
        }
        #endregion

        private void OKButton_Click(object sender, EventArgs e) {
            byte[][] newData = UndoManager.Clone(Level.Blocks);
            newData[0][0] = (byte)startEntranceUpDown.Value;
            newData[0][1] = (byte)midwayEntranceUpDown.Value;
            newData[0][4] = (byte)((int)timeLimitUpDown.Value & 255);
            newData[0][5] = (byte)((int)timeLimitUpDown.Value >> 8);
            newData[0][26] = (byte)((int)soundSetUpDown.Value & 255);

            byte settingsByte = 0x00;

            if (levelWrapCheckBox.Checked)
                settingsByte |= 0x20;
            if (forceMiniCheckBox.Checked)
                settingsByte |= 0x01;
            if (miniMarioPhysicsCheckBox.Checked)
                settingsByte |= 0x02;
            newData[0][2] = settingsByte;

            int oldTileset = newData[0][0xC];

            newData[0][0xC] = (byte)tilesetComboBox.SelectedIndex; // ncg
            newData[3][4] = (byte)tilesetComboBox.SelectedIndex; // ncl

            int FGIndex = bgTopLayerComboBox.SelectedIndex;
            if (FGIndex == bgTopLayerComboBox.Items.Count - 1) FGIndex = 0xFFFF;
            newData[0][0x12] = (byte)FGIndex; // ncg
            newData[0][0x13] = (byte)(FGIndex>>8); // ncg
            newData[4][4] = (byte)FGIndex; // ncl
            newData[4][5] = (byte)(FGIndex >> 8); // ncg
            newData[4][2] = (byte)FGIndex; // nsc
            newData[4][3] = (byte)(FGIndex >> 8); // ncg

            int BGIndex = bgBottomLayerComboBox.SelectedIndex;
            if (BGIndex == bgBottomLayerComboBox.Items.Count - 1) BGIndex = 0xFFFF;
            newData[0][6] = (byte)BGIndex; // ncg
            newData[0][7] = (byte)(BGIndex >> 8); // ncg
            newData[2][4] = (byte)BGIndex; // ncl
            newData[2][5] = (byte)(BGIndex >> 8); // ncg
            newData[2][2] = (byte)BGIndex; // nsc
            newData[2][3] = (byte)(BGIndex >> 8); // ncg


            ComboBox[] checkthese = new ComboBox[] {
                set1ComboBox, set2ComboBox, set3ComboBox, set4ComboBox,
                set5ComboBox, set6ComboBox, set7ComboBox, set8ComboBox,
                set9ComboBox, set10ComboBox, set16ComboBox
            };

            int[] checkthese_idx = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 15 };

            for (int CheckIdx = 0; CheckIdx < checkthese.Length; CheckIdx++) {
                string Item = (string)(checkthese[CheckIdx].Items[Math.Max(0, checkthese[CheckIdx].SelectedIndex)]);
                int cpos = Item.IndexOf(':');
                int modifierval = int.Parse(Item.Substring(0, cpos));
                newData[13][checkthese_idx[CheckIdx]] = (byte)modifierval;
            }

            EdControl.UndoManager.Do(new ChangeLevelSettingsAction(newData));
            if (oldTileset != newData[0][0xC])
            {
                ReloadTileset();
            }

            RefreshMainWindow();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }

        private void bgTopLayerExportButton_Click(object sender, EventArgs e)
        {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                RenderBackground(GFXFile, PalFile, LayoutFile, 256).Save(filename);
            }
        }

        private void bgBottomLayerExportButton_Click(object sender, EventArgs e)
        {
            if (bgBottomLayerComboBox.SelectedIndex == bgBottomLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NSC);
            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = saveFileDialog1.FileName;
                RenderBackground(GFXFile, PalFile, LayoutFile, 576).Save(filename);
            }
        }

        private void bgBottomLayerImportButton_Click(object sender, EventArgs e)
        {
            if (bgBottomLayerComboBox.SelectedIndex == bgBottomLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NSC);
            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            replaceBG(GFXFile, PalFile, LayoutFile, 576, 0xA000);
        }

        private void replaceBG(File GFXFile, File PalFile, File LayoutFile, int offs, int palOffs)
        {

            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            
            string filename = openFileDialog1.FileName;
            Bitmap b = new Bitmap(filename);

            ImageTiler t = new ImageTiler(b);
            Color[] palette = ImageIndexer.createPaletteForImage(b);
            byte[] pal = new byte[1024];
            NSMBTileset.paletteToRawData(palette).CopyTo(pal, 0);
            PalFile.beginEdit(this);
            PalFile.replace(ROM.LZ77_Compress(pal), this);
            PalFile.endEdit(this);
            GFXFile.beginEdit(this);
            GFXFile.replace(ROM.LZ77_Compress(ImageIndexer.indexImageWithPalette(t.tileBuffer, palette)), this);
            GFXFile.endEdit(this);
            b.Dispose();

            ByteArrayOutputStream layout = new ByteArrayOutputStream();
            for (int y = 0; y < 64; y++)
                for (int x = 0; x < 64; x++)
                    layout.writeUShort((ushort)((t.tileMap[x, y] + offs) | palOffs));

            LayoutFile.beginEdit(this);
            LayoutFile.replace(ROM.LZ77_Compress(layout.getArray()), this);
            LayoutFile.endEdit(this);
            
        }

        private void bgTopLayerImportButton_Click(object sender, EventArgs e)
        {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }
            replaceBG(GFXFile, PalFile, LayoutFile, 256, 0x8000);
        }

        private void bgTopLayerFileButton_Click(object sender, EventArgs e)
        {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }
            int id = bgTopLayerComboBox.SelectedIndex;
            ushort fid = ushort.Parse(bgFileID.Text);
            ROM.SetFileIDFromTable(id, ROM.Data.Table_FG_NCL, fid);
            ROM.SetFileIDFromTable(id, ROM.Data.Table_FG_NCG, (ushort)(fid+1));
            ROM.SetFileIDFromTable(id, ROM.Data.Table_FG_NSC, (ushort)(fid+2));
            ROM.SaveOverlay0();
        }

        private void bgBottomLayerFileButton_Click(object sender, EventArgs e)
        {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }
            int id = bgBottomLayerComboBox.SelectedIndex;
            ushort fid = ushort.Parse(bgFileID.Text);
            ROM.SetFileIDFromTable(id, ROM.Data.Table_BG_NCL, fid);
            ROM.SetFileIDFromTable(id, ROM.Data.Table_BG_NCG, (ushort)(fid + 1));
            ROM.SetFileIDFromTable(id, ROM.Data.Table_BG_NSC, (ushort)(fid + 2));
            ROM.SaveOverlay0();
        }

        private void bgTopLayerExportBG_Click(object sender, EventArgs e)
        {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            saveFileDialog2.ShowDialog();
            string filename = saveFileDialog2.FileName;
            exportBackground(GFXFile, PalFile, LayoutFile, filename);
        }

        void exportBackground(File GFXFile, File PalFile, File LayoutFile, string filename)
        {
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(
                new System.IO.FileStream(filename, System.IO.FileMode.Create, System.IO.FileAccess.Write));
            bw.Write("NSMBe Exported Background");
            writeFileContents(GFXFile, bw);
            writeFileContents(PalFile, bw);
            writeFileContents(LayoutFile, bw);
            bw.Close();
        }

        void importBackground(File GFXFile, File PalFile, File LayoutFile, string filename)
        {
            System.IO.BinaryReader br = new System.IO.BinaryReader(
                new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read));
            string header = br.ReadString();
            if (header != "NSMBe Exported Background")
            {
                MessageBox.Show(
                    LanguageManager.Get("NSMBLevel", "InvalidFile"),
                    LanguageManager.Get("NSMBLevel", "Unreadable"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            readFileContents(GFXFile, br);
            readFileContents(PalFile, br);
            readFileContents(LayoutFile, br);
            br.Close();
        }

        private void writeFileContents(File f, System.IO.BinaryWriter bw)
        {
            bw.Write((int)f.fileSize);
            bw.Write(f.getContents());
        }

        void readFileContents(File f, System.IO.BinaryReader br)
        {
            int len = br.ReadInt32();
            byte[] data = new byte[len];
            br.Read(data, 0, len);
            f.beginEdit(this);
            f.replace(data, this);
            f.endEdit(this);
        }

        private void bgTopLayerImportBG_Click(object sender, EventArgs e)
        {

            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgTopLayerComboBox.SelectedIndex, ROM.Data.Table_FG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            openFileDialog2.ShowDialog();
            string filename = openFileDialog2.FileName;
 
            importBackground(GFXFile, PalFile, LayoutFile, filename);
        }

        private void bgBottomLayerExportBG_Click(object sender, EventArgs e)
        {

            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            saveFileDialog2.ShowDialog();
            string filename = saveFileDialog2.FileName;
            exportBackground(GFXFile, PalFile, LayoutFile, filename);
        }

        private void bgBottomLayerImportBG_Click(object sender, EventArgs e)
        {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BlankBG"));
                return;
            }

            ushort GFXFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCG);
            ushort PalFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NCL);
            ushort LayoutFileID = ROM.GetFileIDFromTable(bgBottomLayerComboBox.SelectedIndex, ROM.Data.Table_BG_NSC);

            File GFXFile = ROM.FS.getFileById(GFXFileID);
            File PalFile = ROM.FS.getFileById(PalFileID);
            File LayoutFile = ROM.FS.getFileById(LayoutFileID);

            if (GFXFile == null || PalFile == null || LayoutFile == null)
            {
                MessageBox.Show(LanguageManager.Get("LevelConfig", "BrokenBG"));
                return;
            }

            openFileDialog2.ShowDialog();
            string filename = openFileDialog2.FileName;
 
            importBackground(GFXFile, PalFile, LayoutFile, filename);
        }
    }
}
