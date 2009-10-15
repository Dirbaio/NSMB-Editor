using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4 {
    public class NSMBGraphics {
        public NSMBGraphics(NitroClass ROM) {
            this.ROM = ROM;
        }

        public void LoadTilesets(ushort TilesetID) {
            // backup
            LoadTilesets(TilesetID, 8);
        }

        public void LoadTilesets(ushort TilesetID, int JyotyuPalOverride) {
            Tilesets = new NSMBTileset[3];

            Console.WriteLine("JyotyuPalOverride = {0}, JyotyuPal offset = {1}...", JyotyuPalOverride, NSMBDataHandler.Overlay0[NSMBDataHandler.GetOffset(NSMBDataHandler.Data.Table_Jyotyu_NCL) + JyotyuPalOverride]);

            byte JyotyuPalID = NSMBDataHandler.Overlay0[NSMBDataHandler.GetOffset(NSMBDataHandler.Data.Table_Jyotyu_NCL) + JyotyuPalOverride];
            ushort JyotyuPalFileID = 0;
            if (JyotyuPalID == 1)
                JyotyuPalFileID = ROM.FileIDs["d_2d_A_J_jyotyu_B_ncl.bin"];
            else if (JyotyuPalID == 2)
                JyotyuPalFileID = ROM.FileIDs["d_2d_A_J_jyotyu_R_ncl.bin"];
            else if (JyotyuPalID == 3)
                JyotyuPalFileID = ROM.FileIDs["d_2d_A_J_jyotyu_W_ncl.bin"];
            else
                JyotyuPalFileID = ROM.FileIDs["d_2d_A_J_jyotyu_ncl.bin"];

            Tilesets[0] = new NSMBTileset(ROM,
                ROM.FileIDs["d_2d_A_J_jyotyu_ncg.bin"],
                JyotyuPalFileID,
                ROM.FileIDs["d_2d_PA_A_J_jyotyu.bin"],
                ROM.FileIDs["A_J_jyotyu.bin"],
                ROM.FileIDs["A_J_jyotyu_hd.bin"],
                65535, true, 0);

            LoadTileset1(TilesetID);

            Tilesets[2] = new NSMBTileset(ROM,
                ROM.FileIDs["d_2d_I_S_tikei_nohara_ncg.bin"],
                ROM.FileIDs["d_2d_I_S_tikei_nohara_ncl.bin"],
                ROM.FileIDs["d_2d_PA_I_S_nohara.bin"],
                ROM.FileIDs["I_S_nohara.bin"],
                ROM.FileIDs["I_S_nohara_hd.bin"],
                ROM.FileIDs["NoHaRaSubUnitChangeData.bin"], false, 2);

            // Patch in a bunch of overrides to the normal tileset
            // Now works directly on the map16 data
            Tilesets[0].EditorOverrides[36] = 135;
            Tilesets[0].Overrides[112] = 26;
            Tilesets[0].Overrides[113] = 27;
            Tilesets[0].Overrides[114] = 53;
            Tilesets[0].Overrides[115] = 55;
            Tilesets[0].Overrides[116] = 28;
            Tilesets[0].Overrides[117] = 57;
            Tilesets[0].Overrides[118] = 0;
            Tilesets[0].Overrides[119] = 1;
            Tilesets[0].Overrides[120] = 4;
            Tilesets[0].Overrides[121] = 5;
            Tilesets[0].Overrides[122] = 30;
            Tilesets[0].Overrides[123] = 31;
            Tilesets[0].Overrides[124] = 8;
            Tilesets[0].Overrides[125] = 9;
            Tilesets[0].Overrides[126] = 20;
            Tilesets[0].Overrides[127] = 21;
            Tilesets[0].Overrides[128] = 24;
            Tilesets[0].Overrides[129] = 25;
            Tilesets[0].Overrides[132] = 29;
            Tilesets[0].Overrides[130] = 54;
            Tilesets[0].Overrides[131] = 56;
            Tilesets[0].Overrides[133] = 58;
            Tilesets[0].Overrides[134] = 2;
            Tilesets[0].Overrides[135] = 3;
            Tilesets[0].Overrides[136] = 6;
            Tilesets[0].Overrides[137] = 7;
            Tilesets[0].Overrides[138] = 12;
            Tilesets[0].Overrides[139] = 13;
            Tilesets[0].Overrides[140] = 10;
            Tilesets[0].Overrides[141] = 11;
            Tilesets[0].Overrides[142] = 22;
            Tilesets[0].Overrides[143] = 23;
            Tilesets[0].Overrides[145] = 32;
            Tilesets[0].Overrides[146] = 19;
            Tilesets[0].Overrides[147] = 17;
            Tilesets[0].Overrides[148] = 18;
            Tilesets[0].Overrides[149] = 14;
            Tilesets[0].Overrides[150] = 22;
            Tilesets[0].Overrides[151] = 23;
            Tilesets[0].Overrides[152] = 71;
            Tilesets[0].Overrides[153] = 72;
            Tilesets[0].Overrides[154] = 15;
            Tilesets[0].Overrides[155] = 16;
            Tilesets[0].Overrides[156] = 20;
            Tilesets[0].Overrides[157] = 21;
            Tilesets[0].Overrides[158] = 28;
            Tilesets[0].Overrides[159] = 29;
            Tilesets[0].Overrides[160] = 71;
            Tilesets[0].Overrides[161] = 72;
            Tilesets[0].Overrides[171] = 53;
            Tilesets[0].Overrides[172] = 57;
            Tilesets[0].Overrides[173] = 55;
            Tilesets[0].Overrides[174] = 26;
            Tilesets[0].Overrides[175] = 27;
            Tilesets[0].Overrides[187] = 54;
            Tilesets[0].Overrides[188] = 58;
            Tilesets[0].Overrides[189] = 56;
            Tilesets[0].Overrides[190] = 24;
            Tilesets[0].Overrides[191] = 25;
            Tilesets[0].Overrides[192] = 59;
            Tilesets[0].Overrides[193] = 63;
            Tilesets[0].Overrides[194] = 61;
            Tilesets[0].Overrides[195] = 65;
            Tilesets[0].Overrides[196] = 69;
            Tilesets[0].Overrides[197] = 67;
            Tilesets[0].Overrides[198] = 33;
            Tilesets[0].Overrides[199] = 34;
            Tilesets[0].Overrides[200] = 41;
            Tilesets[0].Overrides[201] = 42;
            Tilesets[0].Overrides[202] = 37;
            Tilesets[0].Overrides[203] = 38;
            Tilesets[0].Overrides[204] = 47;
            Tilesets[0].Overrides[205] = 48;
            Tilesets[0].Overrides[206] = 51;
            Tilesets[0].Overrides[207] = 52;
            Tilesets[0].Overrides[208] = 60;
            Tilesets[0].Overrides[209] = 64;
            Tilesets[0].Overrides[210] = 62;
            Tilesets[0].Overrides[211] = 66;
            Tilesets[0].Overrides[212] = 70;
            Tilesets[0].Overrides[213] = 68;
            Tilesets[0].Overrides[214] = 35;
            Tilesets[0].Overrides[215] = 36;
            Tilesets[0].Overrides[216] = 43;
            Tilesets[0].Overrides[217] = 44;
            Tilesets[0].Overrides[218] = 39;
            Tilesets[0].Overrides[219] = 40;
            Tilesets[0].Overrides[220] = 49;
            Tilesets[0].Overrides[221] = 50;
            Tilesets[0].Overrides[222] = 45;
            Tilesets[0].Overrides[223] = 46;
            Tilesets[0].Overrides[228] = 41;
            Tilesets[0].Overrides[229] = 42;
            Tilesets[0].Overrides[230] = 45;
            Tilesets[0].Overrides[231] = 65;
            Tilesets[0].Overrides[232] = 69;
            Tilesets[0].Overrides[233] = 67;
            Tilesets[0].Overrides[234] = 47;
            Tilesets[0].Overrides[235] = 48;
            Tilesets[0].Overrides[236] = 52;
            Tilesets[0].Overrides[237] = 59;
            Tilesets[0].Overrides[238] = 63;
            Tilesets[0].Overrides[239] = 61;
            Tilesets[0].Overrides[244] = 43;
            Tilesets[0].Overrides[245] = 44;
            Tilesets[0].Overrides[246] = 46;
            Tilesets[0].Overrides[247] = 66;
            Tilesets[0].Overrides[248] = 70;
            Tilesets[0].Overrides[249] = 68;
            Tilesets[0].Overrides[250] = 49;
            Tilesets[0].Overrides[251] = 50;
            Tilesets[0].Overrides[252] = 51;
            Tilesets[0].Overrides[253] = 60;
            Tilesets[0].Overrides[254] = 64;
            Tilesets[0].Overrides[255] = 62;
            Tilesets[0].repaintAllMap16();

            /*Tilesets[0].Objects[0] = Tilesets[0].Objects[1];

            Tilesets[0].Objects[57] = new NSMBTileset.ObjectDef(new byte[] { 64, 0, 0, 64, 1, 0, 0xFE, 64, 2, 0, 64, 3, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[58] = new NSMBTileset.ObjectDef(new byte[] { 64, 4, 0, 64, 5, 0, 0xFE, 64, 6, 0, 64, 7, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[59] = new NSMBTileset.ObjectDef(new byte[] { 64, 8, 0, 64, 9, 0, 0xFE, 64, 10, 0, 64, 11, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[60] = new NSMBTileset.ObjectDef(new byte[] { 64, 12, 0, 64, 13, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[61] = new NSMBTileset.ObjectDef(new byte[] { 64, 14, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[66] = new NSMBTileset.ObjectDef(new byte[] { 64, 15, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[67] = new NSMBTileset.ObjectDef(new byte[] { 64, 16, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[88] = new NSMBTileset.ObjectDef(new byte[] { 64, 17, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[89] = new NSMBTileset.ObjectDef(new byte[] { 64, 18, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[96] = new NSMBTileset.ObjectDef(new byte[] { 64, 19, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[68] = Tilesets[0].Objects[97] = new NSMBTileset.ObjectDef(new byte[] { 64, 20, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[69] = Tilesets[0].Objects[98] = new NSMBTileset.ObjectDef(new byte[] { 64, 21, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[62] = Tilesets[0].Objects[99] = new NSMBTileset.ObjectDef(new byte[] { 64, 22, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[63] = Tilesets[0].Objects[100] = new NSMBTileset.ObjectDef(new byte[] { 64, 23, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[54] = Tilesets[0].Objects[101] = new NSMBTileset.ObjectDef(new byte[] { 66, 28, 0, 66, 29, 0, 0xFE, 64, 24, 0, 64, 25, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[53] = Tilesets[0].Objects[102] = new NSMBTileset.ObjectDef(new byte[] { 64, 26, 0, 64, 27, 0, 0xFE, 66, 28, 0, 66, 29, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[103] = new NSMBTileset.ObjectDef(new byte[] { 64, 30, 0, 64, 31, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[104] = new NSMBTileset.ObjectDef(new byte[] { 64, 32, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[105] = new NSMBTileset.ObjectDef(new byte[] { 64, 33, 0, 64, 34, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[106] = new NSMBTileset.ObjectDef(new byte[] { 64, 35, 0, 64, 36, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[107] = new NSMBTileset.ObjectDef(new byte[] { 64, 37, 0, 64, 38, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[108] = new NSMBTileset.ObjectDef(new byte[] { 64, 39, 0, 64, 40, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[78] = Tilesets[0].Objects[109] = new NSMBTileset.ObjectDef(new byte[] { 64, 41, 0, 64, 42, 0, 0xFE, 66, 45, 0, 66, 46, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[79] = Tilesets[0].Objects[110] = new NSMBTileset.ObjectDef(new byte[] { 66, 45, 0, 66, 46, 0, 0xFE, 64, 43, 0, 64, 44, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[82] = Tilesets[0].Objects[111] = new NSMBTileset.ObjectDef(new byte[] { 64, 47, 0, 64, 48, 0, 0xFE, 66, 51, 0, 66, 52, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[83] = Tilesets[0].Objects[112] = new NSMBTileset.ObjectDef(new byte[] { 66, 51, 0, 66, 52, 0, 0xFE, 64, 49, 0, 64, 50, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[55] = Tilesets[0].Objects[113] = new NSMBTileset.ObjectDef(new byte[] { 64, 53, 0, 65, 57, 0, 0xFE, 64, 54, 0, 65, 58, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[56] = Tilesets[0].Objects[114] = new NSMBTileset.ObjectDef(new byte[] { 65, 57, 0, 64, 55, 0, 0xFE, 65, 58, 0, 64, 56, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[84] = Tilesets[0].Objects[115] = new NSMBTileset.ObjectDef(new byte[] { 64, 59, 0, 65, 63, 0, 0xFE, 64, 60, 0, 65, 64, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[85] = Tilesets[0].Objects[116] = new NSMBTileset.ObjectDef(new byte[] { 65, 63, 0, 64, 61, 0, 0xFE, 65, 64, 0, 64, 62, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[80] = Tilesets[0].Objects[117] = new NSMBTileset.ObjectDef(new byte[] { 64, 65, 0, 65, 69, 0, 0xFE, 64, 66, 0, 65, 70, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[81] = Tilesets[0].Objects[118] = new NSMBTileset.ObjectDef(new byte[] { 65, 69, 0, 64, 67, 0, 0xFE, 65, 70, 0, 64, 68, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[64] = Tilesets[0].Objects[119] = new NSMBTileset.ObjectDef(new byte[] { 64, 71, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[65] = Tilesets[0].Objects[120] = new NSMBTileset.ObjectDef(new byte[] { 64, 72, 0, 0xFE, 0xFF });
            Tilesets[0].Objects[26] = new NSMBTileset.ObjectDef(new byte[] { 64, 135, 0, 0xFE, 0xFF });*/

            //RepatchBlocks(Properties.Settings.Default.SmallBlockOverlays);

            // Enable notes for the normal tileset
            Tilesets[0].UseNotes = true;
            Tilesets[0].ObjNotes = new string[Tilesets[0].Objects.Length];
            List<string> RawNotes = LanguageManager.GetList("ObjNotes");
            for (int NoteIdx = 0; NoteIdx < RawNotes.Count; NoteIdx++) {
                if (RawNotes[NoteIdx] == "") continue;

                int equalPos = RawNotes[NoteIdx].IndexOf('=');
                if (equalPos != -1) {
                    int ObjTarget = int.Parse(RawNotes[NoteIdx].Substring(0, equalPos));
                    Tilesets[0].ObjNotes[ObjTarget] = RawNotes[NoteIdx].Substring(equalPos + 1).Replace('|', '\n');
                }
            }
        }

        public void LoadTileset1(ushort TilesetID)
        {
            // Create a reference to each table to make the code less messy
            /*int TSOffset = TilesetID * 4;
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

            table = NSMBe4.Properties.Resources.ts_chk_table;
            ushort TileBehaviorFile = (ushort)((table[TSOffset] | (table[TSOffset + 1] << 8)) + 131);

            // Since these tables are from the US ROM, counteract it by finding an offset
            GFXFile += (ushort)(ROM.FileIDs["d_2d_A_J_jyotyu_ncg.bin"] - 235);
            PalFile += (ushort)(ROM.FileIDs["d_2d_A_J_jyotyu_ncl.bin"] - 408);
            Map16File += (ushort)(ROM.FileIDs["d_2d_PA_A_J_jyotyu.bin"] - 686);
            ObjFile += (ushort)(ROM.FileIDs["A_J_jyotyu.bin"] - 730);
            ObjIndexFile += (ushort)(ROM.FileIDs["A_J_jyotyu_hd.bin"] - 731);
            TileBehaviorFile += (ushort)(ROM.FileIDs["ChiKa2MainUnitChangeData.bin"] - 180);

            // Is this version of the ROM missing files?
            if (!ROM.FileIDs.ContainsKey("d_2d_TEN_W_kazangake2_ncg.bin") && GFXFile > ROM.FileIDs["d_2d_TEN_W_kazangake_ncg.bin"]) {
                GFXFile -= 2;
            }*/

            ushort GFXFile = NSMBDataHandler.GetFileIDFromTable(TilesetID, NSMBDataHandler.Data.Table_TS_NCG);
            ushort PalFile = NSMBDataHandler.GetFileIDFromTable(TilesetID, NSMBDataHandler.Data.Table_TS_NCL);
            ushort Map16File = NSMBDataHandler.GetFileIDFromTable(TilesetID, NSMBDataHandler.Data.Table_TS_PNL);
            ushort ObjFile = NSMBDataHandler.GetFileIDFromTable(TilesetID, NSMBDataHandler.Data.Table_TS_UNT);
            ushort ObjIndexFile = NSMBDataHandler.GetFileIDFromTable(TilesetID, NSMBDataHandler.Data.Table_TS_UNT_HD);
            ushort TileBehaviorFile = NSMBDataHandler.GetFileIDFromTable(TilesetID, NSMBDataHandler.Data.Table_TS_CHK);

            Tilesets[1] = new NSMBTileset(ROM, GFXFile, PalFile, Map16File, ObjFile, ObjIndexFile, TileBehaviorFile, false, 1);
        }

        public void RepatchBlocks(bool type) {
            /* Question blocks */
            //for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
            //    Tilesets[0].Objects[BlockIdx + 30] = new NSMBTileset.ObjectDef(new byte[] { 64, (byte)(BlockIdx + (type ? 89 : 73)), 0, 0xFE, 0xFF });
            //}

            //Tilesets[0].Objects[90] = new NSMBTileset.ObjectDef(new byte[] { 64, (byte)(type ? 103 : 87), 0, 0xFE, 0xFF });
            //Tilesets[0].Objects[91] = new NSMBTileset.ObjectDef(new byte[] { 64, (byte)(type ? 104 : 88), 0, 0xFE, 0xFF });

            /* Brick blocks */
            //for (int BlockIdx = 0; BlockIdx < 9; BlockIdx++) {
            //    Tilesets[0].Objects[BlockIdx + 44] = new NSMBTileset.ObjectDef(new byte[] { 64, (byte)(BlockIdx + (type ? 114 : 105)), 0, 0xFE, 0xFF });
            //}

            /* Invisible blocks */
            //for (int BlockIdx = 0; BlockIdx < 6; BlockIdx++) {
            //    Tilesets[0].Objects[BlockIdx + 70] = new NSMBTileset.ObjectDef(new byte[] { 64, (byte)(BlockIdx + (type ? 129 : 123)), 0, 0xFE, 0xFF });
            //}
        }

        public NitroClass ROM;
        public NSMBTileset[] Tilesets;

        public static Font SmallInfoFont = new Font("Small Fonts", 7);
        public static Font InfoFont = new Font("Tahoma", 8);
        public static Pen PathPen = new Pen(Color.FromArgb(128, 255, 255, 255), 3);
    }
}
