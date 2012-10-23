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
using System.Text;
using System.Windows.Forms;
using NSMBe4.DSFileSystem;
using System.Drawing;


namespace NSMBe4
{
    public class NSMBLevel
    {
        public File LevelFile;
        public File BGFile;
        public string ExportedFileName;
        public bool isExported = false;
        public ushort SavedLevelFileID = 0;
        public ushort SavedBGFileID = 0;
        public bool isClipboard = false;

        public byte[][] Blocks;
        public List<NSMBObject> Objects;
        public List<NSMBSprite> Sprites;
        public List<NSMBEntrance> Entrances;
        public List<NSMBView> Views, Zones;
        public List<NSMBPath> Paths, ProgressPaths;
        public NSMBGraphics GFX;

        public bool[] ValidSprites;

        private bool editing = false;

        public int[,] levelTilemap = new int[512, 256];

        public NSMBLevel(File levelFile, File bgFile, NSMBGraphics GFX)
        {
            this.LevelFile = levelFile;
            this.BGFile = bgFile;
            LoadLevel(levelFile.getContents(), bgFile.getContents(), GFX);
        }

        public NSMBLevel(File levelFile, File bgFile, byte[] eLevelFile, byte[] eBGFile, NSMBGraphics GFX)
        {
            this.LevelFile = levelFile;
            this.BGFile = bgFile;
            LoadLevel(eLevelFile, eBGFile, GFX);
        }

        public NSMBLevel(string fileName, byte[] eLevelFile, byte[] eBGFile, NSMBGraphics GFX)
        {
            if (fileName == "")
                isClipboard = true;
            else
                ExportedFileName = fileName;
            isExported = true;
            LoadLevel(eLevelFile, eBGFile, GFX);
        }

        private void LoadLevel(byte[] eLevelFile, byte[] eBGFile, NSMBGraphics GFX)
        {
            this.GFX = GFX;

            int FilePos;

            for(int x = 0; x < 512; x++)
                for (int y = 0; y < 256; y++)
                {
                    levelTilemap[x,y] = (x + y) % 512; 
                }

            // Level loading time yay.
            // Since I don't know the format for every block, I will just load them raw.
            Blocks = new byte[][] { null, null, null, null, null, null, null, null, null, null, null, null, null, null };

            FilePos = 0;
            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                int BlockOffset = eLevelFile[FilePos] | (eLevelFile[FilePos + 1] << 8) | (eLevelFile[FilePos + 2] << 16) | eLevelFile[FilePos + 3] << 24;
                FilePos += 4;
                int BlockSize = eLevelFile[FilePos] | (eLevelFile[FilePos + 1] << 8) | (eLevelFile[FilePos + 2] << 16) | eLevelFile[FilePos + 3] << 24;
                FilePos += 4;

                Blocks[BlockIdx] = new byte[BlockSize];
                Array.Copy(eLevelFile, BlockOffset, Blocks[BlockIdx], 0, BlockSize);
            }

            // Now objects.

            int ObjectCount = eBGFile.Length / 10;
            Objects = new List<NSMBObject>(ObjectCount);
            FilePos = 0;
            for (int ObjectIdx = 0; ObjectIdx < ObjectCount; ObjectIdx++) {
                int ObjID = eBGFile[FilePos] | (eBGFile[FilePos + 1] << 8);
                int ObjX = eBGFile[FilePos + 2] | (eBGFile[FilePos + 3] << 8);
                int ObjY = eBGFile[FilePos + 4] | (eBGFile[FilePos + 5] << 8);
                int ObjWidth = eBGFile[FilePos + 6] | (eBGFile[FilePos + 7] << 8);
                int ObjHeight = eBGFile[FilePos + 8] | (eBGFile[FilePos + 9] << 8);
                Objects.Add(new NSMBObject(ObjID & 4095, (ObjID & 61440) >> 12, ObjX, ObjY, ObjWidth, ObjHeight, GFX));
                FilePos += 10;
            }

            /*
             * Sprite struct:
             * Offs Len Dat
             * 0x0   2   Sprite id
             * 0x2   2   X
             * 0x4   2   Y
             * 0x6   6   Dat
             * 0xD   end
             */

            // Sprites
            byte[] SpriteBlock = Blocks[6];
            int SpriteCount = (SpriteBlock.Length - 2) / 12;
            Sprites = new List<NSMBSprite>(SpriteCount);
            FilePos = 0;
            for (int SpriteIdx = 0; SpriteIdx < SpriteCount; SpriteIdx++) {
                NSMBSprite Sprite = new NSMBSprite(this);
                Sprite.Type = SpriteBlock[FilePos] | (SpriteBlock[FilePos + 1] << 8);
                Sprite.X = SpriteBlock[FilePos + 2] | (SpriteBlock[FilePos + 3] << 8);
                Sprite.Y = SpriteBlock[FilePos + 4] | (SpriteBlock[FilePos + 5] << 8);
                Sprite.Data = new byte[6];
                FilePos += 6;
                Sprite.Data[0] = SpriteBlock[FilePos + 1];
                Sprite.Data[1] = SpriteBlock[FilePos + 0];
                Sprite.Data[2] = SpriteBlock[FilePos + 5];
                Sprite.Data[3] = SpriteBlock[FilePos + 4];
                Sprite.Data[4] = SpriteBlock[FilePos + 3];
                Sprite.Data[5] = SpriteBlock[FilePos + 2];
//                Array.Copy(SpriteBlock, FilePos + 6, Sprite.Data, 0, 6);
                Sprites.Add(Sprite);
                FilePos += 6;
            }

            // Entrances.
            byte[] EntranceBlock = Blocks[5];
            int EntranceCount = EntranceBlock.Length / 20;
            Entrances = new List<NSMBEntrance>(EntranceCount);
            FilePos = 0;
            for (int EntIdx = 0; EntIdx < EntranceCount; EntIdx++) {
                NSMBEntrance Entrance = new NSMBEntrance();
                Entrance.X = EntranceBlock[FilePos] | (EntranceBlock[FilePos + 1] << 8);
                Entrance.Y = EntranceBlock[FilePos + 2] | (EntranceBlock[FilePos + 3] << 8);
                Entrance.CameraX = EntranceBlock[FilePos + 4] | (EntranceBlock[FilePos + 5] << 8);
                Entrance.CameraY = EntranceBlock[FilePos + 6] | (EntranceBlock[FilePos + 7] << 8);
                Entrance.Number = EntranceBlock[FilePos + 8];
                Entrance.DestArea = EntranceBlock[FilePos + 9];
                Entrance.ConnectedPipeID = EntranceBlock[FilePos + 10];
                Entrance.DestEntrance = EntranceBlock[FilePos + 12];
                Entrance.Type = EntranceBlock[FilePos + 14];
                Entrance.Settings = EntranceBlock[FilePos + 15];
                Entrance.Unknown1 = EntranceBlock[FilePos + 16];
                Entrance.EntryView = EntranceBlock[FilePos + 18];
                Entrance.Unknown2 = EntranceBlock[FilePos + 19];
                //Array.Copy(EntranceBlock, FilePos, Entrance.Data, 0, 20);
                Entrances.Add(Entrance);
                FilePos += 20;
            }

            // Views
            ByteArrayInputStream ViewBlock = new ByteArrayInputStream(Blocks[7]);
            ByteArrayInputStream CamBlock = new ByteArrayInputStream(Blocks[1]);
            Views = new List<NSMBView>();
            while (ViewBlock.lengthAvailable(16))
                Views.Add(NSMBView.read(ViewBlock, CamBlock));

            // Zones
            ByteArrayInputStream ZoneBlock = new ByteArrayInputStream(Blocks[8]);
            Zones = new List<NSMBView>();
            while (ZoneBlock.lengthAvailable(12))
                Zones.Add(NSMBView.readZone(ZoneBlock));

            // Paths

            ByteArrayInputStream PathBlock = new ByteArrayInputStream(Blocks[10]);
            ByteArrayInputStream PathNodeBlock = new ByteArrayInputStream(Blocks[12]);

            Paths = new List<NSMBPath>();
            while (!PathBlock.end())
            {
                Paths.Add(NSMBPath.read(PathBlock, PathNodeBlock, false));
            }

            PathBlock = new ByteArrayInputStream(Blocks[9]);
            PathNodeBlock = new ByteArrayInputStream(Blocks[11]);

            ProgressPaths = new List<NSMBPath>();
            while (!PathBlock.end())
            {
                ProgressPaths.Add(NSMBPath.read(PathBlock, PathNodeBlock, true));
            }


            CalculateSpriteModifiers();
            repaintAllTilemap();
        }

        public void repaintAllTilemap()
        {
            repaintTilemap(0, 0, 512, 256);
        }

        public void repaintTilemap(int x, int y, int w, int h)
        {
            if (w == 0) return;
            if (h == 0) return;

            for (int xx = 0; xx < w; xx++)
                for (int yy = 0; yy < h; yy++)
                    levelTilemap[xx + x, yy + y] = -1;

            Rectangle r = new Rectangle(x, y, w, h);
            
            for (int ObjIdx = 0; ObjIdx < Objects.Count; ObjIdx++) {
                Rectangle ObjRect = new Rectangle(Objects[ObjIdx].X, Objects[ObjIdx].Y, Objects[ObjIdx].Width, Objects[ObjIdx].Height);
                if (ObjRect.IntersectsWith(r)) {
                    Objects[ObjIdx].renderTilemap(levelTilemap, r);
                }
            }
        }

        public void Remove(List<LevelItem> objs)
        {
            foreach (LevelItem obj in objs)
                Remove(obj);
        }

        public void Remove(LevelItem obj)
        {
            if (obj is NSMBObject)
                Objects.Remove(obj as NSMBObject);
            if (obj is NSMBSprite)
                Sprites.Remove(obj as NSMBSprite);
            if (obj is NSMBEntrance)
                Entrances.Remove(obj as NSMBEntrance);
            if (obj is NSMBView) {
                NSMBView v = obj as NSMBView;
                if (v.isZone)
                    Zones.Remove(v);
                else
                    Views.Remove(v);
            }
            if (obj is NSMBPathPoint) {
                NSMBPathPoint pp = obj as NSMBPathPoint;
                pp.parent.points.Remove(pp);
                if (pp.parent.points.Count == 0) {
                    if (pp.parent.isProgressPath)
                        ProgressPaths.Remove(pp.parent);
                    else
                        Paths.Remove(pp.parent);
                }
            }
        }

        public void Add(List<LevelItem> objs)
        {
            foreach (LevelItem obj in objs)
                Add(obj);
        }

        public void Add(LevelItem obj)
        {
            if (obj is NSMBObject)
                Objects.Add(obj as NSMBObject);
            if (obj is NSMBSprite)
                Sprites.Add(obj as NSMBSprite);
            if (obj is NSMBEntrance)
                Entrances.Add(obj as NSMBEntrance);
            if (obj is NSMBView)  {
                NSMBView v = obj as NSMBView;
                if (v.isZone)
                    Zones.Add(v);
                else
                    Views.Add(v);
            }
            if (obj is NSMBPathPoint) {
                NSMBPathPoint pp = obj as NSMBPathPoint;
                pp.parent.points.Add(pp);
                if (pp.parent.isProgressPath) {
                    if (!ProgressPaths.Contains(pp.parent))
                        ProgressPaths.Add(pp.parent);
                } else {
                    if (!Paths.Contains(pp.parent))
                        Paths.Add(pp.parent);
                }
            }
        }

        public void enableWrite()
        {
            if (!isExported) {
                try
                {
                    BGFile.beginEdit(this);
                    LevelFile.beginEdit(this);
                }
                catch (AlreadyEditingException ex)
                {
                    if (BGFile.beingEditedBy(this))
                        BGFile.endEdit(this);
                    if (LevelFile.beingEditedBy(this))
                        LevelFile.endEdit(this);

                    throw ex;
                }
            }
            editing = true;
        }

        public void close()
        {
            if (editing && !isExported)
            {
                BGFile.endEdit(this);
                LevelFile.endEdit(this);
            }
        }

        public void Save() {
            byte[] LevelFileData;
            byte[] BGDatFileData;

            getSaveFiles(out LevelFileData, out BGDatFileData);
            if (isClipboard)
            {
                ByteArrayInputStream strm = new ByteArrayInputStream(new byte[0]);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(strm);

                NSMBLevel.ExportLevel(SavedLevelFileID, SavedBGFileID, LevelFileData, BGDatFileData, bw);
                Clipboard.SetText("NSMBeLevel|" + Convert.ToBase64String(ROM.LZ77_Compress(strm.getData())) + "|");
            }
            else if (isExported)
            {
                System.IO.FileStream fs = new System.IO.FileStream(ExportedFileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                ExportLevel(SavedLevelFileID, SavedBGFileID, LevelFileData, BGDatFileData, bw);
            }
            else
            {
                BGFile.replace(BGDatFileData, this);
                LevelFile.replace(LevelFileData, this);
            }
        }

        public void SaveToClipboard()
        {
            if (isClipboard)
                Save();
            else
            {
                isClipboard = true;
                Save();
                isClipboard = false;
            }
        }


        public void getSaveFiles(out byte[] LevelFileData, out byte[] BGDatFileData)
        {
            int FilePos;

            // First off, save sprites. These go in the main level file so we must do them before blocks
            // Find out how long the block must be
            int SpriteBlockSize = (Sprites.Count * 12) + 4;
            Blocks[6] = new byte[SpriteBlockSize];
            FilePos = 0;

            for (int SpriteIdx = 0; SpriteIdx < Sprites.Count; SpriteIdx++)
            {
                Blocks[6][FilePos] = (byte)(Sprites[SpriteIdx].Type & 0xFF);
                Blocks[6][FilePos + 1] = (byte)((Sprites[SpriteIdx].Type >> 8) & 0xFF);
                Blocks[6][FilePos + 2] = (byte)(Sprites[SpriteIdx].X & 0xFF);
                Blocks[6][FilePos + 3] = (byte)((Sprites[SpriteIdx].X >> 8) & 0xFF);
                Blocks[6][FilePos + 4] = (byte)(Sprites[SpriteIdx].Y & 0xFF);
                Blocks[6][FilePos + 5] = (byte)((Sprites[SpriteIdx].Y >> 8) & 0xFF);
                FilePos += 6;
                Blocks[6][FilePos + 0] = Sprites[SpriteIdx].Data[1];
                Blocks[6][FilePos + 1] = Sprites[SpriteIdx].Data[0];
                Blocks[6][FilePos + 2] = Sprites[SpriteIdx].Data[5];
                Blocks[6][FilePos + 3] = Sprites[SpriteIdx].Data[4];
                Blocks[6][FilePos + 4] = Sprites[SpriteIdx].Data[3];
                Blocks[6][FilePos + 5] = Sprites[SpriteIdx].Data[2];
                FilePos += 6;
            }

            Blocks[6][FilePos] = 0xFF;
            Blocks[6][FilePos + 1] = 0xFF;
            Blocks[6][FilePos + 2] = 0xFF;
            Blocks[6][FilePos + 3] = 0xFF;

            // Then save entrances
            int EntBlockSize = Entrances.Count * 20;
            Blocks[5] = new byte[EntBlockSize];
            FilePos = 0;

            for (int EntIdx = 0; EntIdx < Entrances.Count; EntIdx++)
            {
                Blocks[5][FilePos] = (byte)(Entrances[EntIdx].X & 0xFF);
                Blocks[5][FilePos + 1] = (byte)((Entrances[EntIdx].X >> 8) & 0xFF);
                Blocks[5][FilePos + 2] = (byte)(Entrances[EntIdx].Y & 0xFF);
                Blocks[5][FilePos + 3] = (byte)((Entrances[EntIdx].Y >> 8) & 0xFF);
                Blocks[5][FilePos + 4] = (byte)(Entrances[EntIdx].CameraX & 0xFF);
                Blocks[5][FilePos + 5] = (byte)((Entrances[EntIdx].CameraX >> 8) & 0xFF);
                Blocks[5][FilePos + 6] = (byte)(Entrances[EntIdx].CameraY & 0xFF);
                Blocks[5][FilePos + 7] = (byte)((Entrances[EntIdx].CameraY >> 8) & 0xFF);
                Blocks[5][FilePos + 8] = (byte)Entrances[EntIdx].Number;
                Blocks[5][FilePos + 9] = (byte)Entrances[EntIdx].DestArea;
                Blocks[5][FilePos + 10] = (byte)Entrances[EntIdx].ConnectedPipeID;
                Blocks[5][FilePos + 12] = (byte)Entrances[EntIdx].DestEntrance;
                Blocks[5][FilePos + 13] = 0x10;
                Blocks[5][FilePos + 14] = (byte)Entrances[EntIdx].Type;
                Blocks[5][FilePos + 15] = (byte)Entrances[EntIdx].Settings;
                Blocks[5][FilePos + 16] = (byte)Entrances[EntIdx].Unknown1;
                Blocks[5][FilePos + 18] = (byte)Entrances[EntIdx].EntryView;
                Blocks[5][FilePos + 19] = (byte)Entrances[EntIdx].Unknown2;
                FilePos += 20;
            }

            // Save Paths

            ByteArrayOutputStream block11 = new ByteArrayOutputStream();
            ByteArrayOutputStream block13 = new ByteArrayOutputStream();
            foreach (NSMBPath p in Paths)
                p.write(block11, block13);

            Blocks[10] = block11.getArray(); //save streams
            Blocks[12] = block13.getArray();

            // Save ProgressPaths

            ByteArrayOutputStream block10 = new ByteArrayOutputStream();
            ByteArrayOutputStream block12 = new ByteArrayOutputStream();
            foreach (NSMBPath p in ProgressPaths)
                p.write(block10, block12);

            Blocks[9] = block10.getArray(); //save streams
            Blocks[11] = block12.getArray();

            // Save Views

            ByteArrayOutputStream Block8 = new ByteArrayOutputStream();
            ByteArrayOutputStream Block2 = new ByteArrayOutputStream();
            int camCount = 0;
            foreach (NSMBView v in Views)
                v.write(Block8, Block2, camCount++);
            Blocks[7] = Block8.getArray();
            Blocks[1] = Block2.getArray();

            //save Zones
            ByteArrayOutputStream Block9 = new ByteArrayOutputStream();
            foreach (NSMBView v in Zones)
                v.writeZone(Block9);
            Blocks[8] = Block9.getArray();


            // Save blocks
            int LevelFileSize = 8 * 14;

            // Find out how long the file must be
            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++)
            {
                LevelFileSize += Blocks[BlockIdx].Length;
            }

            // Now allocate + save it
            FilePos = 0;
            int CurBlockOffset = 8 * 14;
            LevelFileData = new byte[LevelFileSize];

            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++)
            {
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

            // Next up, objects!
            FilePos = 0;
            int BGDatFileSize = (Objects.Count * 10) + 2;
            BGDatFileData = new byte[BGDatFileSize];

            for (int ObjIdx = 0; ObjIdx < Objects.Count; ObjIdx++)
            {
                int ObjType = Objects[ObjIdx].ObjNum | (Objects[ObjIdx].Tileset << 12);
                BGDatFileData[FilePos] = (byte)(ObjType & 0xFF);
                BGDatFileData[FilePos + 1] = (byte)((ObjType >> 8) & 0xFF);
                BGDatFileData[FilePos + 2] = (byte)(Objects[ObjIdx].X & 0xFF);
                BGDatFileData[FilePos + 3] = (byte)((Objects[ObjIdx].X >> 8) & 0xFF);
                BGDatFileData[FilePos + 4] = (byte)(Objects[ObjIdx].Y & 0xFF);
                BGDatFileData[FilePos + 5] = (byte)((Objects[ObjIdx].Y >> 8) & 0xFF);
                BGDatFileData[FilePos + 6] = (byte)(Objects[ObjIdx].Width & 0xFF);
                BGDatFileData[FilePos + 7] = (byte)((Objects[ObjIdx].Width >> 8) & 0xFF);
                BGDatFileData[FilePos + 8] = (byte)(Objects[ObjIdx].Height & 0xFF);
                BGDatFileData[FilePos + 9] = (byte)((Objects[ObjIdx].Height >> 8) & 0xFF);
                FilePos += 10;
            }

            BGDatFileData[FilePos] = 0xFF;
            BGDatFileData[FilePos + 1] = 0xFF;
        }

        public void ReRenderAll() {
            for (int ObjIdx = 0; ObjIdx < Objects.Count; ObjIdx++) {
                Objects[ObjIdx].UpdateObjCache();
            }
        }


        public void CalculateSpriteModifiers() {
            ValidSprites = new bool[ROM.SpriteCount];
            byte[] ModifierTable = ROM.GetInlineFile(ROM.Data.File_Modifiers);

            for (int idx = 0; idx < ROM.SpriteCount; idx++) {
                int ModifierOffset = ModifierTable[idx << 1];
                int ModifierValue = ModifierTable[(idx << 1) + 1];
                if (ModifierValue == 0) {
                    ValidSprites[idx] = true;
                } else {
                    // works around levels like 1-4 area 2 which have a blank modifier block
                    if (Blocks[13].Length > 0 && Blocks[13][ModifierOffset] == ModifierValue) {
                        ValidSprites[idx] = true;
                    }
                }
            }
        }

        public static void ImportLevel(File destLevelFile, File destBGFile, byte[] levelFile, byte[] bgFile)
        {
            try {
                destLevelFile.beginEdit(destBGFile);
            } catch (AlreadyEditingException) {
                MessageBox.Show(LanguageManager.Get("Errors", "Level"));
                return;
            }

            try {
                destBGFile.beginEdit(destLevelFile);
            } catch (AlreadyEditingException) {
                MessageBox.Show(LanguageManager.Get("Errors", "Level"));
                return;
            }

            destLevelFile.replace(levelFile, destBGFile);
            destLevelFile.endEdit(destBGFile);
            destBGFile.replace(bgFile, destLevelFile);
            destBGFile.endEdit(destLevelFile);
        }

        public static void ImportLevel(File destLevelFile, File destBGFile, System.IO.BinaryReader br)
        {
            byte[] levelFile; byte[] bgFile; string[] error;
            getImportLevel(out levelFile, out bgFile, out error, br);
            if (error != null)
                MessageBox.Show(error[0], error[1], MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                ImportLevel(destLevelFile, destBGFile, levelFile, bgFile);
        }

        public static void getImportLevel(out byte[] levelFile, out byte[] bgFile, out string[] error, System.IO.BinaryReader br)
        {
            ushort SavedLevelFileID, SavedBGFileID;
            getImportLevel(out levelFile, out bgFile, out error, out SavedLevelFileID, out SavedBGFileID, br);
        }

        public static void getImportLevel(out byte[] levelFile, out byte[] bgFile, out string[] error, out ushort SavedLevelFileID, out ushort SavedBGFileID, System.IO.BinaryReader br) {
            string Header = br.ReadString();
            levelFile = null; bgFile = null; error = null;
            SavedLevelFileID = 0; SavedBGFileID = 0;
            if (Header != "NSMBe4 Exported Level") {
                error = new string[] { LanguageManager.Get("NSMBLevel", "InvalidFile") , LanguageManager.Get("NSMBLevel", "Unreadable") };
                return;
            }

            ushort FileVersion = br.ReadUInt16();
            if (FileVersion > 1) {
                error = new string[] { LanguageManager.Get("NSMBLevel", "OldVersion"), LanguageManager.Get("NSMBLevel", "Unusable") };
                return;
            }

            // This message conflitcs with the auto-backup and I think it's unecessary ~Piranhaplant
            SavedLevelFileID = br.ReadUInt16();
            SavedBGFileID = br.ReadUInt16();
            //if (SavedLevelFileID != destLevelFile.id) {
            //    DialogResult dr = MessageBox.Show(
            //        LanguageManager.Get("NSMBLevel", "Mismatch"),
            //        LanguageManager.Get("General", "Warning"),
            //        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            //    if (dr == DialogResult.No) {
            //        return;
            //    }
            //}

            int LevelFileLength = br.ReadInt32();
            levelFile = br.ReadBytes(LevelFileLength);

            int BGFileLength = br.ReadInt32();
            bgFile = br.ReadBytes(BGFileLength);
        }

        public static void ExportLevel(File srcLevelFile, File srcBGFile, System.IO.BinaryWriter bw) {
            ExportLevel(srcLevelFile.id, srcBGFile.id, srcLevelFile.getContents(), srcBGFile.getContents(), bw);
        }

        public static void ExportLevel(int LevelFileID, int BGFileID, byte[] LevelFileData, byte[] BGFileData, System.IO.BinaryWriter bw)
        {
            bw.Write("NSMBe4 Exported Level");
            bw.Write((ushort)1);
            bw.Write((ushort)LevelFileID);
            bw.Write((ushort)BGFileID);
            bw.Write(LevelFileData.Length);
            bw.Write(LevelFileData);
            bw.Write(BGFileData.Length);
            bw.Write(BGFileData);
        }

        public int getFreeEntranceNumber()
        {
            int n = 0;

            while (true)
            {
                if (!isEntranceNumberUsed(n))
                    return n;
                n++;
            }
        }
        public int getFreeViewNumber(List<NSMBView> l)
        {
            int n = 0;

            while (true)
            {
                if (!isViewNumberUsed(n, l))
                    return n;
                n++;
            }
        }
        public int getFreePathNumber(List<NSMBPath> l, int startID)
        {
            int n = startID;

            while (true)
            {
                if (!isPathNumberUsed(n, l))
                    return n;
                n++;
            }
        }

        public bool isEntranceNumberUsed(int n)
        {
            foreach (NSMBEntrance e in Entrances)
            {
                if (e.Number == n)
                    return true;
            }

            return false;
        }

        public bool isViewNumberUsed(int n, List<NSMBView> l)
        {
            foreach (NSMBView e in l)
            {
                if (e.Number == n)
                    return true;
            }

            return false;
        }

        public bool isPathNumberUsed(int n, List<NSMBPath> l)
        {
            foreach (NSMBPath p in l)
            {
                if (p.id == n)
                    return true;
            }

            return false;
        }
    }
}
