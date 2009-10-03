using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public interface FileLister
    {
        void DirReady(int DirID, int ParentID, string DirName, bool IsRoot);
        void FileReady(int FileID, int ParentID, string FileName);
    }
}
