using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class InlineFile : File
    {
        private int inlineOffs;
        private int inlineLen;
        private File parentFile;
        private bool isLzCompressed;

        public InlineFile(File parent, int offs, int len, string name, Directory parentDir) :
            this(parent, offs, len, name, parentDir, false)
        {

        }

        public InlineFile(File parent, int offs, int len, string name, Directory parentDir, bool isLzCompressed)
            :base(parent.parent, parentDir, parent.name+" - "+name)
        {
            parentFile = parent;
            inlineOffs = offs;
            inlineLen = len;
            this.isLzCompressed = isLzCompressed;
            this.fixedFile = true;
            this.canChangeOffset = false;
            refreshOffsets();
        }

        public override byte[] getContents()
        {
            if (isLzCompressed)
            {
                byte[] data = ROM.LZ77_Decompress(parentFile.getContents());
                byte[] thisdata = new byte[inlineLen];
                Array.Copy(data, inlineOffs, thisdata, 0, inlineLen);
                return thisdata;
            }
            else return base.getContents();
        }

        public override void replace(byte[] newFile, object editor)
        {
            if (isLzCompressed)
            {
                if (!isAGoodEditor(editor))
                    throw new Exception("NOT CORRECT EDITOR " + name);

                byte[] olddata = ROM.LZ77_Decompress(parentFile.getContents());
                Array.Copy(newFile, 0, olddata, inlineOffs, inlineLen);
                parentFile.replace(ROM.LZ77_Compress(olddata), this);
            }
            else base.replace(newFile, editor);
        }

        public override void beginEdit(object editor)
        {
            parentFile.beginEditInline(this);
            base.beginEdit(editor);
        }

        public override void endEdit(object editor)
        {
            parentFile.endEditInline(this);
            base.endEdit(editor);
        }

        public override void refreshOffsets()
        {
            fileBegin = parentFile.fileBegin + inlineLen;
            fileSize = inlineLen;
        }

        public override void saveOffsets()
        {
        }

        public override void enableEdition()
        {
            refreshOffsets(); // In case the parent file gets moved...
            base.enableEdition();
        }
    }
}
