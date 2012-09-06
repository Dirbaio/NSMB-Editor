/*
*   This file is part of NSMB Editor 5.
*
*   NSMB Editor 5 is free software: you can redistribute it and/or modify
*   it under the terms of the GNU General Public License as published by
*   the Free Software Foundation, either version 3 of the License, or
*   (at your option) any later version.
*
*   NSMB Editor 5 is distributed in the hope that it will be useful,
*   but WITHOUT ANY WARRANTY; without even the implied warranty of
*   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*   GNU General Public License for more details.
*
*   You should have received a copy of the GNU General Public License
*   along with NSMB Editor 5.  If not, see <http://www.gnu.org/licenses/>.
*/

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4.DSFileSystem
{
    public class InlineFile : PhysicalFile
    {
        private int inlineOffs;
        private int inlineLen;
        private File parentFile;
        private CompressionType comp;

        public enum CompressionType : int
        {
            NoComp,
            LZComp,
            LZWithHeaderComp
        }

        public InlineFile(File parent, int offs, int len, string name, Directory parentDir) :
            this(parent, offs, len, name, parentDir, CompressionType.NoComp)
        {

        }

        public InlineFile(File parent, int offs, int len, string name, Directory parentDir, CompressionType comp)
            : base(parent.parent, parentDir, parent.name + " - " + name + ":" + offs.ToString("X") + ":" + len.ToString("X"))
        {
            parentFile = parent;
            inlineOffs = offs;
            inlineLen = len;
            this.comp = comp;
            this.fixedFile = true;
            this.canChangeOffset = false;
            refreshOffsets();
        }

        public override byte[] getContents()
        {
            if (comp != CompressionType.NoComp)
            {
                byte[] data;
                if(comp == CompressionType.LZWithHeaderComp)
                    data = ROM.LZ77_DecompressWithHeader(parentFile.getContents());
                else
                    data = ROM.LZ77_Decompress(parentFile.getContents());
                byte[] thisdata = new byte[inlineLen];
                Array.Copy(data, inlineOffs, thisdata, 0, inlineLen);
                return thisdata;
            }
            else return base.getContents();
        }

        public override void replace(byte[] newFile, object editor)
        {
            if (!isAGoodEditor(editor))
                throw new Exception("NOT CORRECT EDITOR " + name);

            if (comp != CompressionType.NoComp)
            {
                byte[] data;
                if (comp == CompressionType.LZWithHeaderComp)
                    data = ROM.LZ77_DecompressWithHeader(parentFile.getContents());
                else
                    data = ROM.LZ77_Decompress(parentFile.getContents());
                Array.Copy(newFile, 0, data, inlineOffs, inlineLen);
                parentFile.replace(ROM.LZ77_Compress(data, comp == CompressionType.LZWithHeaderComp), this);
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
            fileBegin = parentFile.fileBegin + inlineOffs;
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
