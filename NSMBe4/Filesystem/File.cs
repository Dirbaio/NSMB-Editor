using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.Filesystem
{
    public class File
    {
        private ushort fileID;
        private FileSystem parentF;

        public FileSystem parent { get { return parentF; } }
        public List<FileReference> references = new List<FileReference>();

        public File(ushort fileID, FileSystem parent)
        {
            this.fileID = fileID;
            this.parentF = parent;
        }

        public FileReference OpenFile(bool readOnly)
        {
            if (!readOnly)
                if (isWriting())
                    throw new Exception("Another file already opened as read/write");

            FileReference res = new FileReference(this, readOnly);
            references.Add(res);

            return res;
        }

        public bool isWriting()
        {
            foreach (FileReference r in references)
                if (!r.isReadOnly)
                    return true;

            return false;
        }

        public void replaceFile(byte[] data)
        {
            parent.ReplaceFile(fileID, data);
            foreach(FileReference r in references)
                if(r.changeListener != null)
                    r.changeListener.FileChanged(this);
        }

        public byte[] extractFile()
        {
            return parent.ExtractFile(fileID);
        }

    }
}
