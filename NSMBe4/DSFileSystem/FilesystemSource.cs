using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    public abstract class FilesystemSource
    {
        protected Stream s;

        public abstract Stream load();
        public abstract void save();
        public abstract void close();
        public abstract string getDescription();
    }
}
