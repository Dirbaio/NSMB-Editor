using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace NSMBe4 {

    public class NSMBLevel {


        private NitroClass ROM;
        private ushort LevelFileID;
        private ushort BGFileID;

        public byte[][] Blocks;
        public List<NSMBObject> Objects;
        public List<NSMBSprite> Sprites;
        public List<NSMBEntrance> Entrances;
        public List<NSMBView> Views, Zones;
        public List<NSMBPath> Paths;

        public bool[] ValidSprites;        
        
        public NSMBLevel(NitroClass ROM, ushort LevelFileID, ushort BGFileID, NSMBGraphics GFX) {
            this.ROM = ROM;
            this.LevelFileID = LevelFileID;
            this.BGFileID = BGFileID;

            int FilePos;

            // Level loading time yay.
            // Since I don't know the format for every block, I will just load them raw.
            byte[] eLevelFile = ROM.ExtractFile(LevelFileID);
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
            byte[] eBGFile = ROM.ExtractFile(BGFileID);

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
                Array.Copy(SpriteBlock, FilePos + 6, Sprite.Data, 0, 6);
                Sprites.Add(Sprite);
                FilePos += 12;
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
            Views = new List<NSMBView>();
            while (ViewBlock.available(16))
                Views.Add(NSMBView.read(ViewBlock));

            // Zones
            ByteArrayInputStream ZoneBlock = new ByteArrayInputStream(Blocks[8]);
            Zones = new List<NSMBView>();
            while (ZoneBlock.available(12))
                Zones.Add(NSMBView.readZone(ZoneBlock));

            // Paths!!! (warning, cool feature)

            ByteArrayInputStream PathBlock = new ByteArrayInputStream(Blocks[10]);
            ByteArrayInputStream PathNodeBlock = new ByteArrayInputStream(Blocks[12]);

            Paths = new List<NSMBPath>();
            while (!PathBlock.end())
            {
                Paths.Add(NSMBPath.read(PathBlock, PathNodeBlock));
            }


            CalculateSpriteModifiers();
        }

        public void Save() {
            int FilePos;

            // First off, save sprites. These go in the main level file so we must do them before blocks
            // Find out how long the block must be
            int SpriteBlockSize = (Sprites.Count * 12) + 4;
            Blocks[6] = new byte[SpriteBlockSize];
            FilePos = 0;

            for (int SpriteIdx = 0; SpriteIdx < Sprites.Count; SpriteIdx++) {
                Blocks[6][FilePos] = (byte)(Sprites[SpriteIdx].Type & 0xFF);
                Blocks[6][FilePos + 1] = (byte)((Sprites[SpriteIdx].Type >> 8) & 0xFF);
                Blocks[6][FilePos + 2] = (byte)(Sprites[SpriteIdx].X & 0xFF);
                Blocks[6][FilePos + 3] = (byte)((Sprites[SpriteIdx].X >> 8) & 0xFF);
                Blocks[6][FilePos + 4] = (byte)(Sprites[SpriteIdx].Y & 0xFF);
                Blocks[6][FilePos + 5] = (byte)((Sprites[SpriteIdx].Y >> 8) & 0xFF);
                Array.Copy(Sprites[SpriteIdx].Data, 0, Blocks[6], FilePos + 6, 6);
                FilePos += 12;
            }

            Blocks[6][FilePos] = 0xFF;
            Blocks[6][FilePos + 1] = 0xFF;
            Blocks[6][FilePos + 2] = 0xFF;
            Blocks[6][FilePos + 3] = 0xFF;

            // Then save entrances
            int EntBlockSize = Entrances.Count * 20;
            Blocks[5] = new byte[EntBlockSize];
            FilePos = 0;

            for (int EntIdx = 0; EntIdx < Entrances.Count; EntIdx++) {
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

            // Save Views

            ByteArrayOutputStream Block8 = new ByteArrayOutputStream();
            foreach (NSMBView v in Views)
                v.write(Block8);
            // Save Views
            Blocks[7] = Block8.getArray();

            ByteArrayOutputStream Block9 = new ByteArrayOutputStream();
            foreach (NSMBView v in Zones)
                v.writeZone(Block9);
            Blocks[8] = Block9.getArray();


            // Save blocks
            int LevelFileSize = 8 * 14;

            // Find out how long the file must be
            for (int BlockIdx = 0; BlockIdx < 14; BlockIdx++) {
                LevelFileSize += Blocks[BlockIdx].Length;
            }

            // Now allocate + save it
            FilePos = 0;
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

            // Next up, objects!
            FilePos = 0;
            int BGDatFileSize = (Objects.Count * 10) + 2;
            byte[] BGDatFile = new byte[BGDatFileSize];

            for (int ObjIdx = 0; ObjIdx < Objects.Count; ObjIdx++) {
                int ObjType = Objects[ObjIdx].ObjNum | (Objects[ObjIdx].Tileset << 12);
                BGDatFile[FilePos] = (byte)(ObjType & 0xFF);
                BGDatFile[FilePos + 1] = (byte)((ObjType >> 8) & 0xFF);
                BGDatFile[FilePos + 2] = (byte)(Objects[ObjIdx].X & 0xFF);
                BGDatFile[FilePos + 3] = (byte)((Objects[ObjIdx].X >> 8) & 0xFF);
                BGDatFile[FilePos + 4] = (byte)(Objects[ObjIdx].Y & 0xFF);
                BGDatFile[FilePos + 5] = (byte)((Objects[ObjIdx].Y >> 8) & 0xFF);
                BGDatFile[FilePos + 6] = (byte)(Objects[ObjIdx].Width & 0xFF);
                BGDatFile[FilePos + 7] = (byte)((Objects[ObjIdx].Width >> 8) & 0xFF);
                BGDatFile[FilePos + 8] = (byte)(Objects[ObjIdx].Height & 0xFF);
                BGDatFile[FilePos + 9] = (byte)((Objects[ObjIdx].Height >> 8) & 0xFF);
                FilePos += 10;
            }

            BGDatFile[FilePos] = 0xFF;
            BGDatFile[FilePos + 1] = 0xFF;

            ROM.ReplaceFile(BGFileID, BGDatFile);
        }

        public void ReRenderAll() {
            for (int ObjIdx = 0; ObjIdx < Objects.Count; ObjIdx++) {
                Objects[ObjIdx].UpdateObjCache();
            }
        }


        public void CalculateSpriteModifiers() {
            ValidSprites = new bool[324];
            byte[] ModifierTable = Properties.Resources.modifiertable;

            for (int idx = 0; idx < 324; idx++) {
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

        public static void ImportLevel(NitroClass ROM, ushort LevelFileID, ushort BGFileID, BinaryReader br) {
            string Header = br.ReadString();
            if (Header != "NSMBe4 Exported Level") {
                MessageBox.Show(
                    LanguageManager.Get("NSMBLevel", "InvalidFile"),
                    LanguageManager.Get("NSMBLevel", "Unreadable"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ushort FileVersion = br.ReadUInt16();
            if (FileVersion > 1) {
                MessageBox.Show(
                    LanguageManager.Get("NSMBLevel", "OldVersion"),
                    LanguageManager.Get("NSMBLevel", "Unusable"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ushort SavedLevelFileID = br.ReadUInt16();
            ushort SavedBGFileID = br.ReadUInt16();
            if (SavedLevelFileID != LevelFileID) {
                DialogResult dr = MessageBox.Show(
                    LanguageManager.Get("NSMBLevel", "Mismatch"),
                    LanguageManager.Get("General", "Warning"),
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dr == DialogResult.No) {
                    return;
                }
            }

            int LevelFileLength = br.ReadInt32();
            byte[] LevelFile = br.ReadBytes(LevelFileLength);
            ROM.ReplaceFile(LevelFileID, LevelFile);

            int BGFileLength = br.ReadInt32();
            byte[] BGFile = br.ReadBytes(BGFileLength);
            ROM.ReplaceFile(BGFileID, BGFile);
        }

        public static void ExportLevel(NitroClass ROM, ushort LevelFileID, ushort BGFileID, BinaryWriter bw) {
            bw.Write("NSMBe4 Exported Level");
            bw.Write((ushort)1);
            bw.Write(LevelFileID);
            bw.Write(BGFileID);
            byte[] LevelFile = ROM.ExtractFile(LevelFileID);
            bw.Write(LevelFile.Length);
            bw.Write(LevelFile);
            byte[] BGFile = ROM.ExtractFile(BGFileID);
            bw.Write(BGFile.Length);
            bw.Write(BGFile);
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

        private bool isEntranceNumberUsed(int n)
        {
            foreach (NSMBEntrance e in Entrances)
            {
                if (e.Number == n)
                    return true;
            }

            return false;
        }

        private bool isViewNumberUsed(int n, List<NSMBView> l)
        {
            foreach (NSMBView e in l)
            {
                if (e.Number == n)
                    return true;
            }

            return false;
        }
    }
}
