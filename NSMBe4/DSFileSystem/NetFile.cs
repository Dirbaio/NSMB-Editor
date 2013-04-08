using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class NetFile : FileWithLock
    {
        NetFilesystem netfs;
        public NetFile(NetFilesystem parent, Directory parentDir, string name, int id, int ffileSize)
            : base(parent, parentDir, name, id)
        {
            this.netfs = parent;
            this.fileSizeP = ffileSize;
        }


        public override byte[] getContents()
        {
            ByteArrayOutputStream request = new ByteArrayOutputStream();
            request.writeInt(id);
            return netfs.doRequest(2, request.getArray(), this);
        }

        public override byte[] getInterval(int start, int end)
        {
            ByteArrayOutputStream request = new ByteArrayOutputStream();
            request.writeInt(id);
            request.writeInt(start);
            request.writeInt(end);
            return netfs.doRequest(3, request.getArray(), this);
        }

        public override void startEdition()
        {
            ByteArrayOutputStream request = new ByteArrayOutputStream();
            request.writeInt(id);
            netfs.doRequest(4, request.getArray(), this);
        }

        public override void endEdition()
        {
            ByteArrayOutputStream request = new ByteArrayOutputStream();
            request.writeInt(id);
            netfs.doRequest(5, request.getArray(), this);
        }

        public override void replace(byte[] newFile, object editor)
        {
            ByteArrayOutputStream request = new ByteArrayOutputStream();
            request.writeInt(id);
            request.writeInt(newFile.Length);
            request.write(newFile);
            netfs.doRequest(6, request.getArray(), this);
        }
        public override void replaceInterval(byte[] newFile, int start)
        {
            ByteArrayOutputStream request = new ByteArrayOutputStream();
            request.writeInt(id);
            request.writeInt(start);
            request.writeInt(start + newFile.Length);
            request.write(newFile);
            netfs.doRequest(7, request.getArray(), this);
        }
    }
}
