using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public class ExportedLevel
    {
        public byte[] LevelFile;
        public byte[] BGDatFile;
        public int LevelFileID = 0;
        public int BGDatFileID = 0;

        public ExportedLevel() { }

        public ExportedLevel(byte[] LevelFile, byte[] BGDatFile, int LevelFileID, int BGDatFileID)
        {
            this.LevelFile = LevelFile;
            this.BGDatFile = BGDatFile;
            this.LevelFileID = LevelFileID;
            this.BGDatFileID = BGDatFileID;
        }

        public ExportedLevel(File LevelFile, File BGFile)
        {
            this.LevelFile = LevelFile.getContents();
            this.BGDatFile = BGFile.getContents();
            this.LevelFileID = LevelFile.id;
            this.BGDatFileID = BGFile.id;
        }

        public ExportedLevel(System.IO.BinaryReader br)
        {
            string Header = br.ReadString();
            if (Header != "NSMBe4 Exported Level")
            {
                throw new Exception(LanguageManager.Get("NSMBLevel", "InvalidFile"));
            }

            ushort FileVersion = br.ReadUInt16();
            if (FileVersion > 1)
            {
                throw new Exception(LanguageManager.Get("NSMBLevel", "OldVersion"));
            }

            LevelFileID = br.ReadUInt16();
            BGDatFileID = br.ReadUInt16();
            int LevelFileLength = br.ReadInt32();
            LevelFile = br.ReadBytes(LevelFileLength);

            int BGFileLength = br.ReadInt32();
            BGDatFile = br.ReadBytes(BGFileLength);
        }

        public static void Import(File destLevelFile, File destBGFile, byte[] levelFile, byte[] bgFile)
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

        public void Import(File destLevelFile, File destBGFile)
        {
            Import(destLevelFile, destBGFile, LevelFile, BGDatFile);
        }

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write("NSMBe4 Exported Level");
            bw.Write((ushort)1);
            bw.Write((ushort)LevelFileID);
            bw.Write((ushort)BGDatFileID);
            bw.Write(LevelFile.Length);
            bw.Write(LevelFile);
            bw.Write(BGDatFile.Length);
            bw.Write(BGDatFile);
        }
    }
}
