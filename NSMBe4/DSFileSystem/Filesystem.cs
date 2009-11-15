using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    public abstract class Filesystem
    {
        protected FilesystemSource source;
        public List<File> allFiles = new List<File>();
        public List<Directory> allDirs = new List<Directory>();
        protected Dictionary<int, File> filesById = new Dictionary<int, File>();
        protected Dictionary<string, File> filesByName = new Dictionary<string, File>();
        protected Dictionary<int, Directory> dirsById = new Dictionary<int, Directory>();
        protected Dictionary<string, Directory> dirsByName = new Dictionary<string, Directory>();
        public Stream s;
        public Directory mainDir;
        protected File freeSpaceDelimiter;

        protected Filesystem(FilesystemSource fs)
        {
            this.source = fs;
            this.s = source.load();
        }

        public File getFileById(int id)
        {
            if (!filesById.ContainsKey(id))
                return null;
            return filesById[id];
        }

        public File getFileByName(string name)
        {
            if (!filesByName.ContainsKey(name))
                return null;
            return filesByName[name];
        }

        protected void addFile(File f)
        {
            allFiles.Add(f);
            filesById.Add(f.id, f);
            filesByName.Add(f.name, f);
        }

        protected void addDir(Directory d)
        {
            allDirs.Add(d);
            dirsById.Add(d.id, d);
            dirsByName.Add(d.name, d);
        }

        //Tries to find LEN bytes of continuous unused space AFTER the freeSpaceDelimiter (usually fat or fnt)
        public File findFreeSpace(uint len)
        {
            allFiles.Sort(); //sort by offset

            File bestSpace = null;
            uint bestSpaceLeft = uint.MaxValue;

            for (int i = allFiles.IndexOf(freeSpaceDelimiter); i < allFiles.Count - 1; i++)
            {
                uint spBegin = allFiles[i].fileBegin + allFiles[i].fileSize; //- 1 + 1;
                uint spEnd = allFiles[i + 1].fileBegin - 1;
                uint spSize = spEnd - spBegin + 1;
                if (spSize >= len)
                {
                    uint spLeft = len - spSize;
                    if (spLeft < bestSpaceLeft)
                    {
                        bestSpaceLeft = spLeft;
                        bestSpace = allFiles[i];
                    }
                }
            }

            if (bestSpace != null)
                return bestSpace;
            else //if theres no space
                return allFiles[allFiles.Count - 1]; //just add the file at the very end 
        }
    }
}
