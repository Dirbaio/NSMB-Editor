using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;
using System.IO;

namespace NSMBe4
{
    public class InternalLevelSource : LevelSource
    {
        DSFileSystem.File levelFile;
        DSFileSystem.File BGDatFile;
        string filename;
        string levelname;
        byte[] levelData;
        byte[] BGDatData;
        bool editing = false;

        public InternalLevelSource(string filename, string levelname)
            : this(filename, levelname, "") { }

        public InternalLevelSource(string filename, string levelname, string loadFileName)
        {
            levelFile = ROM.getLevelFile(filename);
            BGDatFile = ROM.getBGDatFile(filename);
            this.filename = filename;
            this.levelname = levelname;
            if (loadFileName == "")
            {
                levelData = levelFile.getContents();
                BGDatData = BGDatFile.getContents();
            }
            else
            {
                FileStream fs = new FileStream(loadFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                BinaryReader br = new BinaryReader(fs);
                ExportedLevel level = new ExportedLevel(br);
                br.Close();
                levelData = level.LevelFile;
                BGDatData = level.BGDatFile;
            }
        }

        public override byte[] getData()
        {
            return levelData;
        }

        public override byte[] getBGDatData()
        {
            return BGDatData;
        }

        public override int getLevelFileID()
        {
            return levelFile.id;
        }

        public override int getBGDatFileID()
        {
            return BGDatFile.id;
        }

        public override void save(ExportedLevel level)
        {
            levelFile.replace(level.LevelFile, this);
            BGDatFile.replace(level.BGDatFile, this);
        }

        public override string getLevelName()
        {
            return levelname;
        }

        public override string getBackupText()
        {
            return filename;
        }

        public override string getBackupFileName()
        {
            return filename + ".nml";
        }

        public override void enableWrite()
        {
            if (editing) return;
            levelFile.beginEdit(this);
            BGDatFile.beginEdit(this);
            editing = true;
        }

        public override void close()
        {
            if (editing)
            {
                levelFile.endEdit(this);
                BGDatFile.endEdit(this);
                editing = false;
            }
        }
    }
}
