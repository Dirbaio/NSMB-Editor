using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class LevelConfig : Form {
        public LevelConfig(NSMBLevel Level, NitroClass ROM) {
            InitializeComponent();
            this.Level = Level;
            this.ROM = ROM;
            tabControl1.SelectTab(0);

            // Translate
            if (Properties.Settings.Default.Language == 1) {
                tabPage1.Text = "Opciones";
                tabPage2.Text = "Graficos";
                tabPage3.Text = "Surtido Sprites (1)";
                tabPage4.Text = "Surtido Sprites (2)";

                groupBox1.Text = "Serie de objetos";
                groupBox2.Text = "Fondo delantero";
                groupBox3.Text = "Fondo detras";

                label1.Text = "Limite de tiempo:";
                label2.Text = "Entrada punto empezar:";
                label3.Text = "Entrada punto medio:";

                levelWrapCheckBox.Text = "Movimiento camara transversal (como 7-2)";

                OKButton.Text = "Vale";
                cancelButton.Text = "Cancelar";
            }

            string[] rawlist;
            string[] parsedlist;
            int index;

            // Add tilesets to list
            if (Properties.Settings.Default.Language != 1) {
                rawlist = Properties.Resources.tilesetlist.Split('\n');
            } else {
                rawlist = Properties.Resources.tilesetlist_lang1.Split('\n');
            }

            index = 0;
            parsedlist = new string[76];
            foreach (string name in rawlist) {
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                parsedlist[index] = trimmedname;
                index += 1;
            }

            tilesetComboBox.Items.AddRange(parsedlist);

            // Add foregrounds to list
            if (Properties.Settings.Default.Language != 1) {
                rawlist = Properties.Resources.fglist.Split('\n');
            } else {
                rawlist = Properties.Resources.fglist_lang1.Split('\n');
            }

            index = 0;
            parsedlist = new string[77];
            foreach (string name in rawlist) {
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                parsedlist[index] = trimmedname;
                index += 1;
            }

            bgTopLayerComboBox.Items.AddRange(parsedlist);

            // Add backgrounds
            if (Properties.Settings.Default.Language != 1) {
                rawlist = Properties.Resources.bglist.Split('\n');
            } else {
                rawlist = Properties.Resources.bglist_lang1.Split('\n');
            }

            index = 0;
            parsedlist = new string[77];
            foreach (string name in rawlist) {
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                parsedlist[index] = trimmedname;
                index += 1;
            }

            bgBottomLayerComboBox.Items.AddRange(parsedlist);

            // Load modifier lists
            if (Properties.Settings.Default.Language != 1) {
                rawlist = Properties.Resources.modifierchooser.Split('\n');
            } else {
                rawlist = Properties.Resources.modifierchooser_lang1.Split('\n');
            }

            ComboBox target = null;
            foreach (string name in rawlist) {
                string trimmedname = name.Trim();
                if (trimmedname == "") continue;
                if (trimmedname[0] == '[') {
                    switch (trimmedname) {
                        case "[1]": target = set1ComboBox; break;
                        case "[2]": target = set2ComboBox; break;
                        case "[3]": target = set3ComboBox; break;
                        case "[4]": target = set4ComboBox; break;
                        case "[5]": target = set5ComboBox; break;
                        case "[6]": target = set6ComboBox; break;
                        case "[7]": target = set7ComboBox; break;
                        case "[8]": target = set8ComboBox; break;
                        case "[9]": target = set9ComboBox; break;
                        case "[10]": target = set10ComboBox; break;
                        case "[16]": target = set16ComboBox; break;
                    }
                } else {
                    target.Items.Add(trimmedname);
                }
            }
        }

        private NSMBLevel Level;
        private NitroClass ROM;

        public delegate void SetDirtyFlagDelegate();
        public event SetDirtyFlagDelegate SetDirtyFlag;

        public delegate void ReloadTilesetDelegate();
        public event ReloadTilesetDelegate ReloadTileset;

        public delegate void RefreshMainWindowDelegate();
        public event RefreshMainWindowDelegate RefreshMainWindow;

        public void LoadSettings() {
            startEntranceUpDown.Value = Level.Blocks[0][0];
            midwayEntranceUpDown.Value = Level.Blocks[0][1];
            timeLimitUpDown.Value = Level.Blocks[0][4] | (Level.Blocks[0][5] << 8);
            levelWrapCheckBox.Checked = ((Level.Blocks[0][2] & 0x20) != 0);

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

        private void tilesetPreviewButton_Click(object sender, EventArgs e) {
            int TilesetIndex = tilesetComboBox.SelectedIndex * 4;
            ushort GFXFile = (ushort)(Properties.Resources.ts_ncg_table[TilesetIndex] | (Properties.Resources.ts_ncg_table[TilesetIndex + 1] << 8));
            ushort PalFile = (ushort)(Properties.Resources.ts_ncl_table[TilesetIndex] | (Properties.Resources.ts_ncl_table[TilesetIndex + 1] << 8));

            // Since these tables are from the US ROM, counteract it by finding an offset
            GFXFile += (ushort)(ROM.FileIDs["d_2d_A_J_jyotyu_ncg.bin"] - 235 + 131);
            PalFile += (ushort)(ROM.FileIDs["d_2d_A_J_jyotyu_ncl.bin"] - 408 + 131);

            // Is this version of the ROM missing files?
            if (!ROM.FileIDs.ContainsKey("d_2d_TEN_W_kazangake2_ncg.bin") && GFXFile > ROM.FileIDs["d_2d_TEN_W_kazangake_ncg.bin"]) {
                GFXFile -= 2;
            }

            int FilePos;

            // First get the palette out
            byte[] ePalFile = FileSystem.LZ77_Decompress(ROM.ExtractFile(PalFile));
            Color[] Palette = new Color[512];

            for (int PalIdx = 0; PalIdx < 512; PalIdx++) {
                int ColourVal = ePalFile[PalIdx * 2] + (ePalFile[(PalIdx * 2) + 1] << 8);
                int cR = (ColourVal & 31) * 8;
                int cG = ((ColourVal >> 5) & 31) * 8;
                int cB = ((ColourVal >> 10) & 31) * 8;
                Palette[PalIdx] = Color.FromArgb(cR, cG, cB);
            }

            //Palette[0] = Color.Fuchsia;
            //Palette[256] = Color.Fuchsia;
            Palette[0] = Color.LightSlateGray;
            Palette[256] = Color.LightSlateGray;

            // Load graphics
            byte[] eGFXFile = FileSystem.LZ77_Decompress(ROM.ExtractFile(GFXFile));
            int TileCount = eGFXFile.Length / 64;
            Bitmap TilesetBuffer = new Bitmap(256, 224);

            FilePos = 0;
            int TileSrcX = 0;
            int TileSrcY = 0;
            for (int TileIdx = 0; TileIdx < TileCount; TileIdx++) {
                for (int TileY = 0; TileY < 8; TileY++) {
                    for (int TileX = 0; TileX < 8; TileX++) {
                        TilesetBuffer.SetPixel(TileSrcX + TileX, TileSrcY + TileY, Palette[eGFXFile[FilePos]]);
                        TilesetBuffer.SetPixel(TileSrcX + TileX, TileSrcY + TileY + 112, Palette[eGFXFile[FilePos] + 256]);
                        FilePos++;
                    }
                }
                TileSrcX += 8;
                if (TileSrcX >= 256) {
                    TileSrcX = 0;
                    TileSrcY += 8;
                }
            }

            new ImagePreviewer(TilesetBuffer).Show();
        }

        private void bgTopLayerPreviewButton_Click(object sender, EventArgs e) {
            if (bgTopLayerComboBox.SelectedIndex == bgTopLayerComboBox.Items.Count - 1) {
                MessageBox.Show(Properties.Settings.Default.Language != 1 ? "This background is blank and has nothing to see." : "Este fondo está en blanco y no tiene nada para ver.");
                return;
            }

            int BGIndex = bgTopLayerComboBox.SelectedIndex * 4;
            ushort GFXFile = (ushort)((Properties.Resources.fg_ncg_table[BGIndex] | (Properties.Resources.fg_ncg_table[BGIndex + 1] << 8)) + 131);
            ushort PalFile = (ushort)((Properties.Resources.fg_ncl_table[BGIndex] | (Properties.Resources.fg_ncl_table[BGIndex + 1] << 8)) + 131);
            ushort LayoutFile = (ushort)((Properties.Resources.fg_nsc_table[BGIndex] | (Properties.Resources.fg_nsc_table[BGIndex + 1] << 8)) + 131);

            if (GFXFile == 2088 || PalFile == 2088 || LayoutFile == 2088) {
                MessageBox.Show(Properties.Settings.Default.Language != 1 ? "This background doesn't work." : "Este fondo no funciona.");
                return;
            }

            // Since these tables are from the US ROM, counteract it by finding an offset
            GFXFile += (ushort)(ROM.FileIDs["d_2d_I_M_free_chika2_ncg.bin"] - 273);
            PalFile += (ushort)(ROM.FileIDs["d_2d_I_M_free_chika2_ncl.bin"] - 453);
            LayoutFile += (ushort)(ROM.FileIDs["d_2d_I_M_free_chika2_nsc.bin"] - 611);

            //System.Diagnostics.Debug.Print("Files: {0} {1} {2}", ROM.FileNames[GFXFile], ROM.FileNames[PalFile], ROM.FileNames[LayoutFile]);

            ShowBackground(GFXFile, PalFile, LayoutFile, 256);
        }

        private void bgBottomLayerPreviewButton_Click(object sender, EventArgs e) {
            if (bgBottomLayerComboBox.SelectedIndex == bgBottomLayerComboBox.Items.Count - 1) {
                MessageBox.Show(Properties.Settings.Default.Language != 1 ? "This background is blank and has nothing to see." : "Este fondo es blanco y no tiene nada para ver.");
                return;
            }

            int BGIndex = bgBottomLayerComboBox.SelectedIndex * 4;
            ushort GFXFile = (ushort)((Properties.Resources.bg_ncg_table[BGIndex] | (Properties.Resources.bg_ncg_table[BGIndex + 1] << 8)) + 131);
            ushort PalFile = (ushort)((Properties.Resources.bg_ncl_table[BGIndex] | (Properties.Resources.bg_ncl_table[BGIndex + 1] << 8)) + 131);
            ushort LayoutFile = (ushort)((Properties.Resources.bg_nsc_table[BGIndex] | (Properties.Resources.bg_nsc_table[BGIndex + 1] << 8)) + 131);

            if (GFXFile == 2088 || PalFile == 2088 || LayoutFile == 2088) {
                MessageBox.Show(Properties.Settings.Default.Language != 1 ? "This background doesn't work." : "Este fondo no trabaja.");
                return;
            }

            // Since these tables are from the US ROM, counteract it by finding an offset
            GFXFile += (ushort)(ROM.FileIDs["d_2d_I_M_back_chika2_ncg.bin"] - 236);
            PalFile += (ushort)(ROM.FileIDs["d_2d_I_M_back_chika2_ncl.bin"] - 416);
            LayoutFile += (ushort)(ROM.FileIDs["d_2d_I_M_back_chika2_R_nsc.bin"] - 574);

            //System.Diagnostics.Debug.Print("Offsets: {0} {1} {2}", ROM.FileIDs["d_2d_I_M_back_chika2_ncg.bin"] - 236, ROM.FileIDs["d_2d_I_M_back_chika2_ncl.bin"] - 416, ROM.FileIDs["d_2d_I_M_back_chika2_R_nsc.bin"] - 574);
            //System.Diagnostics.Debug.Print("Files: {0} {1} {2}", ROM.FileNames[GFXFile], ROM.FileNames[PalFile], ROM.FileNames[LayoutFile]);

            ShowBackground(GFXFile, PalFile, LayoutFile, 576);
        }

        private void ShowBackground(ushort GFXFile, ushort PalFile, ushort LayoutFile, int WeirdVariable) {
            int FilePos;

            // First get the palette out
            byte[] ePalFile = FileSystem.LZ77_Decompress(ROM.ExtractFile(PalFile));
            Color[] Palette = new Color[512];

            for (int PalIdx = 0; PalIdx < 512; PalIdx++) {
                int ColourVal = ePalFile[PalIdx * 2] + (ePalFile[(PalIdx * 2) + 1] << 8);
                int cR = (ColourVal & 31) * 8;
                int cG = ((ColourVal >> 5) & 31) * 8;
                int cB = ((ColourVal >> 10) & 31) * 8;
                Palette[PalIdx] = Color.FromArgb(cR, cG, cB);
            }

            //Palette[0] = Color.Fuchsia;
            //Palette[256] = Color.Fuchsia;
            Palette[0] = Color.LightSlateGray;
            Palette[256] = Color.LightSlateGray;

            // Load graphics
            byte[] eGFXFile = FileSystem.LZ77_Decompress(ROM.ExtractFile(GFXFile));
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

            Graphics TilesetGraphics = Graphics.FromImage(TilesetBuffer);
            //IntPtr TilesetBufferHDC = TilesetGraphics.GetHdc();
            //IntPtr TilesetBufferHandle = TilesetBuffer.GetHbitmap();
            //GDIImports.SelectObject(TilesetBufferHDC, TilesetBufferHandle);

            // Load layout
            byte[] eLayoutFile = FileSystem.LZ77_Decompress(ROM.ExtractFile(LayoutFile));
            int LayoutCount = eLayoutFile.Length / 2;
            Bitmap BG = new Bitmap(512, 512, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            Graphics BGGraphics = Graphics.FromImage(BG);
            BGGraphics.Clear(Color.LightSlateGray);
            //IntPtr BGHDC = BGGraphics.GetHdc();

            FilePos = 0;
            int TileNum;
            byte ControlByte;
            Rectangle SrcRect, DestRect;
            int SrcX = 0;
            int SrcY = 0;
            for (int TileIdx = 0; TileIdx < LayoutCount; TileIdx++) {
                TileNum = eLayoutFile[FilePos];
                ControlByte = eLayoutFile[FilePos + 1];
                TileNum |= (ControlByte & 3) << 8;
                TileNum -= WeirdVariable;
                SrcRect = new Rectangle(TileNum * 8, (ControlByte & 16) != 0 ? 8 : 0, 8, 8);
                DestRect = new Rectangle(SrcX, SrcY, 8, 8);
                if ((ControlByte & 4) != 0) { DestRect.Width = -8; DestRect.X += 8; }
                if ((ControlByte & 8) != 0) { DestRect.Height = -8; DestRect.Y += 8; }
                //System.Diagnostics.Debug.Print(TileNum.ToString());
                //if (TileNum != 0 || ControlByte != 0) {
                //GDIImports.StretchBlt(BGHDC, DestRect.X, DestRect.Y, DestRect.Width, DestRect.Height, TilesetBufferHDC, SrcRect.X, SrcRect.Y, 8, 8, GDIImports.TernaryRasterOperations.SRCCOPY);
                BGGraphics.DrawImage(TilesetBuffer, DestRect, SrcRect.X, SrcRect.Y, SrcRect.Width, SrcRect.Height, GraphicsUnit.Pixel);
                //}
                SrcX += 8;
                if (SrcX >= 512) { SrcX = 0; SrcY += 8; }
                FilePos += 2;
            }

            //BGGraphics.ReleaseHdc(BGHDC);
            //GDIImports.DeleteObject(TilesetBufferHandle);
            //TilesetGraphics.ReleaseHdc(TilesetBufferHDC);

            new ImagePreviewer(BG).Show();
        }

        private void OKButton_Click(object sender, EventArgs e) {
            Level.Blocks[0][0] = (byte)startEntranceUpDown.Value;
            Level.Blocks[0][1] = (byte)midwayEntranceUpDown.Value;
            Level.Blocks[0][4] = (byte)((int)timeLimitUpDown.Value & 255);
            Level.Blocks[0][5] = (byte)((int)timeLimitUpDown.Value >> 8);

            if (levelWrapCheckBox.Checked) {
                Level.Blocks[0][2] = (byte)(Level.Blocks[0][2] | 32);
            } else {
                Level.Blocks[0][2] = (byte)(Level.Blocks[0][2] & 223);
            }

            int oldTileset = Level.Blocks[0][0xC];

            Level.Blocks[0][0xC] = (byte)tilesetComboBox.SelectedIndex; // ncg
            Level.Blocks[3][4] = (byte)tilesetComboBox.SelectedIndex; // ncl

            int FGIndex = bgTopLayerComboBox.SelectedIndex;
            if (FGIndex == bgTopLayerComboBox.Items.Count - 1) FGIndex = 255;
            Level.Blocks[0][0x12] = (byte)FGIndex; // ncg
            Level.Blocks[4][4] = (byte)FGIndex; // ncl
            Level.Blocks[4][2] = (byte)FGIndex; // nsc

            int BGIndex = bgBottomLayerComboBox.SelectedIndex;
            if (BGIndex == bgBottomLayerComboBox.Items.Count - 1) BGIndex = 255;
            Level.Blocks[0][6] = (byte)BGIndex; // ncg
            Level.Blocks[2][4] = (byte)BGIndex; // ncl
            Level.Blocks[2][2] = (byte)BGIndex; // nsc

            if (oldTileset != Level.Blocks[0][0xC]) {
                ReloadTileset();
            }

            ComboBox[] checkthese = new ComboBox[] {
                set1ComboBox, set2ComboBox, set3ComboBox, set4ComboBox,
                set5ComboBox, set6ComboBox, set7ComboBox, set8ComboBox,
                set9ComboBox, set10ComboBox, set16ComboBox
            };

            int[] checkthese_idx = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 15 };

            for (int CheckIdx = 0; CheckIdx < checkthese.Length; CheckIdx++) {
                string Item = (string)(checkthese[CheckIdx].Items[checkthese[CheckIdx].SelectedIndex]);
                int cpos = Item.IndexOf(':');
                int modifierval = int.Parse(Item.Substring(0, cpos));
                Level.Blocks[13][checkthese_idx[CheckIdx]] = (byte)modifierval;
            }

            Level.CalculateSpriteModifiers();

            SetDirtyFlag();
            RefreshMainWindow();
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
