using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.Filesystem
{
    public class FileReference
    {
        private File file;
        private bool readOnlyf, closed;
        public bool isReadOnly {get{return readOnlyf;}}

        public FileChangeListener changeListener;

        public FileReference(File f, bool readOnly)
        {
            this.file = f;
            this.readOnlyf = readOnly;
        }

        public void enableWrite()
        {
            if (file.isWriting())
                throw new Exception("Another file already opened as read/write");
            readOnlyf = false;
        }

        public void replaceFile(byte[] data)
        {
            if (readOnlyf)
                throw new Exception("Trying to replace a read-only file");
            if (closed)
                throw new Exception("Trying to replace a closed file");
            file.replaceFile(data);
        }
        public byte[] extractFile()
        {
            if (closed)
                throw new Exception("Trying to access a closed file");
            return file.extractFile();
        }

        public void close()
        {
            if (closed) return;

            closed = true;
            file.references.Remove(this);
        }

        ~FileReference()
        {
            close();
        }
    }
}
