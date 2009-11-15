using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class Directory
    {
        private bool isSystemFolderP;
        public bool isSystemFolder { get { return isSystemFolderP; } }

        private string nameP;
        public string name { get { return nameP; } }

        private int idP;
        public int id { get { return idP; } }

        private Directory parentDirP;
        public Directory parentDir { get { return parentDirP; } }

        public List<File> childrenFiles = new List<File>();
        public List<Directory> childrenDirs = new List<Directory>();

        private Filesystem parent;

        public Directory(Filesystem parent, Directory parentDir, bool system, string name, int id)
        {
            this.parent = parent;
            this.parentDirP = parentDir;
            this.isSystemFolderP = system;
            this.nameP = name;
            this.idP = id;
        }

        public void dumpFiles()
        {
            dumpFiles(2);
        }

        public void dumpFiles(int ind)
        {
            for (int i = 0; i < ind; i++)
                Console.Out.Write(" ");
            Console.Out.WriteLine("[DIR"+id+"] " + name);
            foreach (Directory d in childrenDirs)
                d.dumpFiles(ind + 4);
            foreach (File f in childrenFiles)
                f.dumpFile(ind + 4);
        }
    }
}
