using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4
{
    public abstract class LevelSource
    {
        public abstract byte[] getData();
        public abstract byte[] getBGDatData();
        public abstract int getLevelFileID();
        public abstract int getBGDatFileID();
        public abstract void save(ExportedLevel level);
        public abstract string getLevelName();
        public abstract string getBackupText();     // The text that will be stored in the backup setting variable
        public abstract string getBackupFileName(); // The name of the file that will be stored in \Backup including .nml
        public abstract void enableWrite();
        public abstract void close();
        
        // Returns the proper LevelSource for a backed up level
        public static LevelSource getForBackupLevel(string backupText, string backupDirectory)
        {
            if (backupText == ClipboardLevelSource.backupInfoString)
                return new ClipboardLevelSource(Path.Combine(backupDirectory, ClipboardLevelSource.backupInfoString + ".nml"));
            if (backupText.Contains(Path.DirectorySeparatorChar.ToString()))
                return new ExternalLevelSource(backupText, Path.Combine(backupDirectory, Path.GetFileName(backupText)));
            return new InternalLevelSource(backupText, backupText, Path.Combine(backupDirectory, backupText + ".nml"));
        }
    }
}
