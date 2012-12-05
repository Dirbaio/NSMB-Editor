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
        public byte[] BGFile;
        public int LevelFileID = 0;
        public int BGFileID = 0;
        public string ErrorMessage = "";
        public string ErrorTitle = "";

        public ExportedLevel()
        {

        }

        public ExportedLevel(byte[] LevelFile, byte[] BGFile, int LevelFileID, int BGFileID)
        {
            this.LevelFile = LevelFile;
            this.BGFile = BGFile;
            this.LevelFileID = LevelFileID;
            this.BGFileID = BGFileID;
        }

        public ExportedLevel(string ErrorMessage, string ErrorTitle)
        {
            this.ErrorMessage = ErrorMessage;
            this.ErrorTitle = ErrorTitle;
        }

        public ExportedLevel(File LevelFile, File BGFile)
        {
            this.LevelFile = LevelFile.getContents();
            this.BGFile = BGFile.getContents();
            this.LevelFileID = LevelFile.id;
            this.BGFileID = BGFile.id;
        }

        public ExportedLevel(System.IO.BinaryReader br)
        {
            string Header = br.ReadString();
            if (Header != "NSMBe4 Exported Level")
            {
                ErrorMessage = LanguageManager.Get("NSMBLevel", "InvalidFile");
                ErrorTitle = LanguageManager.Get("NSMBLevel", "Unreadable");
                return;
            }

            ushort FileVersion = br.ReadUInt16();
            if (FileVersion > 1)
            {
                ErrorMessage = LanguageManager.Get("NSMBLevel", "OldVersion");
                ErrorTitle = LanguageManager.Get("NSMBLevel", "Unusable");
            }

            // This message conflitcs with the auto-backup and I think it's unecessary ~Piranhaplant
            LevelFileID = br.ReadUInt16();
            BGFileID = br.ReadUInt16();
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
            LevelFile = br.ReadBytes(LevelFileLength);

            int BGFileLength = br.ReadInt32();
            BGFile = br.ReadBytes(BGFileLength);
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
            Import(destLevelFile, destBGFile, LevelFile, BGFile);
        }

        public void Write(System.IO.BinaryWriter bw)
        {
            bw.Write("NSMBe4 Exported Level");
            bw.Write((ushort)1);
            bw.Write((ushort)LevelFileID);
            bw.Write((ushort)BGFileID);
            bw.Write(LevelFile.Length);
            bw.Write(LevelFile);
            bw.Write(BGFile.Length);
            bw.Write(BGFile);
        }

        public bool hasError()
        {
            return ErrorMessage != "";
        }
    }
}
