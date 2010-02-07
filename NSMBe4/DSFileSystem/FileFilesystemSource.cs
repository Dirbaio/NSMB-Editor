using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
    class FileFilesystemSource : FilesystemSource
    {

        File f;
        MemoryStream str;
        public FileFilesystemSource(File f)
        {
            this.f = f;
        }

        public override Stream load()
        {
            f.beginEdit(this);
            str = new MemoryStream();
            byte[] data = f.getContents();
            str.Write(data, 0, data.Length);

            return str;
        }

        public override void save()
        {
            f.replace(str.ToArray(), this);
        }

        public override void close()
        {
            save();
            str.Close();
            f.endEdit(this);
        }

        public override string getDescription()
        {
            return f.name;
        }

    }

}
