using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4 {
    public class NSMBGraphics {
        public NSMBGraphics(NitroClass ROM) {
            this.ROM = ROM;
        }

        public void LoadTilesets(ushort TilesetID, ushort TilesetPalID) {
            Tilesets = new NSMBTileset[3];

            Tilesets[0] = new NSMBTileset(ROM, ROM.FileIDs["d_2d_A_J_jyotyu_ncg.bin"], ROM.FileIDs["d_2d_A_J_jyotyu_ncl.bin"], ROM.FileIDs["d_2d_PA_A_J_jyotyu.bin"], ROM.FileIDs["A_J_jyotyu.bin"], ROM.FileIDs["A_J_jyotyu_hd.bin"], true);
            LoadTileset1(TilesetID, TilesetPalID);
            Tilesets[2] = new NSMBTileset(ROM, ROM.FileIDs["d_2d_I_S_tikei_nohara_ncg.bin"], ROM.FileIDs["d_2d_I_S_tikei_nohara_ncl.bin"], ROM.FileIDs["d_2d_PA_I_S_nohara.bin"], ROM.FileIDs["I_S_nohara.bin"], ROM.FileIDs["I_S_nohara_hd.bin"], false);

            // Patch in a bunch of overrides to the normal tileset
            Tilesets[0].Objects[57] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 0, 0, 64, 1, 0, 0xFE, 64, 2, 0, 64, 3, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[58] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 4, 0, 64, 5, 0, 0xFE, 64, 6, 0, 64, 7, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[59] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 8, 0, 64, 9, 0, 0xFE, 64, 10, 0, 64, 11, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[60] = new NSMBTileset.ObjectDef(-1, 2, 1, new byte[] { 64, 12, 0, 64, 13, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[61] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 14, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[66] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 15, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[67] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 16, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[88] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 17, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[89] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 18, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[96] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 19, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[68] = Tilesets[0].Objects[97] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 20, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[69] = Tilesets[0].Objects[98] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 21, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[62] = Tilesets[0].Objects[99] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 22, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[63] = Tilesets[0].Objects[100] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 23, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[54] = Tilesets[0].Objects[101] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 66, 28, 0, 66, 29, 0, 0xFE, 64, 24, 0, 64, 25, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[53] = Tilesets[0].Objects[102] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 26, 0, 64, 27, 0, 0xFE, 66, 28, 0, 66, 29, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[103] = new NSMBTileset.ObjectDef(-1, 2, 1, new byte[] { 64, 30, 0, 64, 31, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[104] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 32, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[105] = new NSMBTileset.ObjectDef(-1, 2, 1, new byte[] { 64, 33, 0, 64, 34, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[106] = new NSMBTileset.ObjectDef(-1, 2, 1, new byte[] { 64, 35, 0, 64, 36, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[107] = new NSMBTileset.ObjectDef(-1, 2, 1, new byte[] { 64, 37, 0, 64, 38, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[108] = new NSMBTileset.ObjectDef(-1, 2, 1, new byte[] { 64, 39, 0, 64, 40, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[78] = Tilesets[0].Objects[109] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 41, 0, 64, 42, 0, 0xFE, 66, 45, 0, 66, 46, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[79] = Tilesets[0].Objects[110] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 66, 45, 0, 66, 46, 0, 0xFE, 64, 43, 0, 64, 44, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[82] = Tilesets[0].Objects[111] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 47, 0, 64, 48, 0, 0xFE, 66, 51, 0, 66, 52, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[83] = Tilesets[0].Objects[112] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 66, 51, 0, 66, 52, 0, 0xFE, 64, 49, 0, 64, 50, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[55] = Tilesets[0].Objects[113] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 53, 0, 65, 57, 0, 0xFE, 64, 54, 0, 65, 58, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[56] = Tilesets[0].Objects[114] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 65, 57, 0, 64, 55, 0, 0xFE, 65, 58, 0, 64, 56, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[84] = Tilesets[0].Objects[115] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 59, 0, 65, 63, 0, 0xFE, 64, 60, 0, 65, 64, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[85] = Tilesets[0].Objects[116] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 65, 63, 0, 64, 61, 0, 0xFE, 65, 64, 0, 64, 62, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[80] = Tilesets[0].Objects[117] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 64, 65, 0, 65, 69, 0, 0xFE, 64, 66, 0, 65, 70, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[81] = Tilesets[0].Objects[118] = new NSMBTileset.ObjectDef(-1, 2, 2, new byte[] { 65, 69, 0, 64, 67, 0, 0xFE, 65, 70, 0, 64, 68, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[64] = Tilesets[0].Objects[119] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 71, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[65] = Tilesets[0].Objects[120] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 72, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[26] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, 135, 0, 0xFE, 0xFF });

            RepatchBlocks(Properties.Settings.Default.SmallBlockOverlays);

            // Enable notes for the normal tileset
            Tilesets[0].UseNotes = true;
            Tilesets[0].ObjNotes = new string[Tilesets[0].Objects.Length];
            string[] RawNotes;
            if (Properties.Settings.Default.Language != 1) {
                RawNotes = Properties.Resources.normal_objnotes.Split('\n');
            } else {
                RawNotes = Properties.Resources.normal_objnotes_lang1.Split('\n');
            }
            for (int NoteIdx = 0; NoteIdx < RawNotes.Length; NoteIdx++) {
                if (RawNotes[NoteIdx] == "") continue;

                int equalPos = RawNotes[NoteIdx].IndexOf('=');
                if (equalPos != -1) {
                    int ObjTarget = int.Parse(RawNotes[NoteIdx].Substring(0, equalPos));
                    Tilesets[0].ObjNotes[ObjTarget] = RawNotes[NoteIdx].Substring(equalPos + 1).Replace('|', '\n');
                }
            }
        }

        public void LoadTileset1(ushort TilesetID, ushort TilesetPalID) {
            // Create a reference to each table to make the code less messy
            int TSOffset = TilesetID * 4;
            int TSPalOffset = TilesetPalID * 4;
            byte[] table = null;

            table = NSMBe4.Properties.Resources.ts_ncg_table;
            ushort GFXFile = (ushort)((table[TSOffset] | (table[TSOffset + 1] << 8)) + 131);

            table = NSMBe4.Properties.Resources.ts_ncl_table;
            ushort PalFile = (ushort)((table[TSOffset] | (table[TSOffset + 1] << 8)) + 131);

            table = NSMBe4.Properties.Resources.ts_pnl_table;
            ushort Map16File = (ushort)((table[TSOffset] | (table[TSOffset + 1] << 8)) + 131);

            table = NSMBe4.Properties.Resources.ts_unt_table;
            ushort ObjFile = (ushort)((table[TSOffset] | (table[TSOffset + 1] << 8)) + 131);

            table = NSMBe4.Properties.Resources.ts_unt_hd_table;
            ushort ObjIndexFile = (ushort)((table[TSOffset] | (table[TSOffset + 1] << 8)) + 131);

            // Since these tables are from the US ROM, counteract it by finding an offset
            GFXFile += (ushort)(ROM.FileIDs["d_2d_A_J_jyotyu_ncg.bin"] - 235);
            PalFile += (ushort)(ROM.FileIDs["d_2d_A_J_jyotyu_ncl.bin"] - 408);
            Map16File += (ushort)(ROM.FileIDs["d_2d_PA_A_J_jyotyu.bin"] - 686);
            ObjFile += (ushort)(ROM.FileIDs["A_J_jyotyu.bin"] - 730);
            ObjIndexFile += (ushort)(ROM.FileIDs["A_J_jyotyu_hd.bin"] - 731);

            // Is this version of the ROM missing files?
            if (!ROM.FileIDs.ContainsKey("d_2d_TEN_W_kazangake2_ncg.bin") && GFXFile > ROM.FileIDs["d_2d_TEN_W_kazangake_ncg.bin"]) {
                GFXFile -= 2;
            }

            Tilesets[1] = new NSMBTileset(ROM, GFXFile, PalFile, Map16File, ObjFile, ObjIndexFile, false);
        }

        public void RepatchBlocks(bool type) {
            /* Question blocks */
            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                Tilesets[0].Objects[BlockIdx + 30] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, (byte)(BlockIdx + (type ? 89 : 73)), 0, 0xFE, 0xFF });
            }

            Tilesets[0].Objects[90] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, (byte)(type ? 103 : 87), 0, 0xFE, 0xFF });
            Tilesets[0].Objects[91] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, (byte)(type ? 104 : 88), 0, 0xFE, 0xFF });

            /* Brick blocks */
            for (int BlockIdx = 0; BlockIdx < 9; BlockIdx++) {
                Tilesets[0].Objects[BlockIdx + 44] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, (byte)(BlockIdx + (type ? 114 : 105)), 0, 0xFE, 0xFF });
            }

            /* Invisible blocks */
            for (int BlockIdx = 0; BlockIdx < 6; BlockIdx++) {
                Tilesets[0].Objects[BlockIdx + 70] = new NSMBTileset.ObjectDef(-1, 1, 1, new byte[] { 64, (byte)(BlockIdx + (type ? 129 : 123)), 0, 0xFE, 0xFF });
            }
        }

        public NitroClass ROM;
        public NSMBTileset[] Tilesets;

        public static Font SmallInfoFont = new Font("Small Fonts", 7);
        public static Font InfoFont = new Font("Tahoma", 8);
        public static Pen PathPen = new Pen(Color.FromArgb(128, 255, 255, 255), 3);
    }
}
