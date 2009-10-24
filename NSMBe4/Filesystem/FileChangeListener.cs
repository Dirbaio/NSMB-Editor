using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.Filesystem
{
    public interface FileChangeListener
    {
        void FileChanged(File f);
    }
}
