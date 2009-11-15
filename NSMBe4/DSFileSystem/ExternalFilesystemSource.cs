using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    public class ExternalFilesystemSource : FilesystemSource
    {
        String path;

        public string fileName;

        public ExternalFilesystemSource(string n)
        {
            this.fileName = n;
        }

        public override Stream load()
        {
            s = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
            return s;
        }

        public override void save()
        {
            //just do nothing, any modifications are directly written to disk
        }

        public override void close()
        {
            s.Close();
        }
    }
}
