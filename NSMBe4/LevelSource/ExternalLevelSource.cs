using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4
{
    public class ExternalLevelSource : LevelSource
    {
        string filename;
        public ExportedLevel level;

        public ExternalLevelSource(string filename)
            : this(filename, filename) { }

        public ExternalLevelSource(string filename, string loadFileName)
        {
            this.filename = filename;
            FileStream fs = new FileStream(loadFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader br = new BinaryReader(fs);
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
            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
            BinaryWriter bw = new BinaryWriter(fs);
            level.Write(bw);
            bw.Close();
        }

        public override string getLevelName()
        {
            return filename;
        }

        public override string getBackupText()
        {
            return filename;
        }

        public override string getBackupFileName()
        {
            return Path.GetFileName(filename);
        }

        public override void enableWrite()
        {
            // nothing to do here
        }

        public override void close()
        {
            // nothing to do here
        }
    }
}
