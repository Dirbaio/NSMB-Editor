using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace NSMBe4
{
    public class ClipboardLevelSource : LevelSource
    {
        public const string backupInfoString = "Clipboard";
        public const string clipboardHeader = "NSMBeLevel|";
        public const string clipboardFooter = "|";

        public ExportedLevel level;

        public ClipboardLevelSource()
            : this("") { }

        public ClipboardLevelSource(string loadFileName)
        {
            BinaryReader br;
            if (loadFileName == "")
            {
                string leveltxt = Clipboard.GetText();
                if (!(leveltxt.StartsWith(clipboardHeader) && leveltxt.EndsWith(clipboardFooter)))
                    throw new Exception();
                leveltxt = leveltxt.Substring(11, leveltxt.Length - 12);
                byte[] leveldata = ROM.LZ77_Decompress(Convert.FromBase64String(leveltxt));
                ByteArrayInputStream strm = new ByteArrayInputStream(leveldata);
                br = new BinaryReader(strm);
            }
            else
            {
                FileStream fs = new FileStream(loadFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                br = new BinaryReader(fs);
            }
            level = new ExportedLevel(br);
            br.Close();
        }

        public override byte[] getData()
        {
            return level.LevelFile;
        }

        public override byte[] getBGDatData()
        {
            return level.BGDatFile;
        }

        public override int getLevelFileID()
        {
            return level.LevelFileID;
        }

        public override int getBGDatFileID()
        {
            return level.BGDatFileID;
        }

        public override void save(ExportedLevel level)
        {
            ByteArrayInputStream strm = new ByteArrayInputStream(new byte[0]);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(strm);
            level.Write(bw);
            copyData(strm.getData());
        }

        public override string getLevelName()
        {
            return LanguageManager.Get("NSMBLevel", "ClipboardLevel");
        }

        public override string getBackupText()
        {
            return backupInfoString;
        }

        public override string getBackupFileName()
        {
            return backupInfoString + ".nml";
        }

        public override void enableWrite()
        {
            // Nothing to do here
        }

        public override void close()
        {
            // Nothing to do here
        }

        public static void copyData(byte[] data)
        {
            Clipboard.SetText(clipboardHeader + Convert.ToBase64String(ROM.LZ77_Compress(data)) + clipboardFooter);
        }
    }
}
