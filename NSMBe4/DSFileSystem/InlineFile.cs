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
    public class InlineFile : File
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
            : base(parent.parent, parentDir, parent.name + " - " + name + ":" + offs.ToString("X") + ":" + len.ToString("X"), -1)
        {
            parentFile = parent;
            inlineOffs = offs;
            inlineLen = len;
            this.comp = comp;
        }

        public override byte[] getContents()
        {
            byte[] data = parentFile.getContents();
            if(comp == CompressionType.LZWithHeaderComp) data = ROM.LZ77_DecompressWithHeader(data);
            else if(comp == CompressionType.LZComp) data = ROM.LZ77_Decompress(data);
            
            byte[] thisdata = new byte[inlineLen];
            Array.Copy(data, inlineOffs, thisdata, 0, inlineLen);
            return thisdata;
        }

        public override void replace(byte[] newFile, object editor)
        {
            if (!isAGoodEditor(editor))
                throw new Exception("NOT CORRECT EDITOR " + name);

            byte[] data = parentFile.getContents();
            if(comp == CompressionType.LZWithHeaderComp) data = ROM.LZ77_DecompressWithHeader(data);
            else if(comp == CompressionType.LZComp) data = ROM.LZ77_Decompress(data);

            Array.Copy(newFile, 0, data, inlineOffs, inlineLen);

            if(comp == CompressionType.LZWithHeaderComp) data = ROM.LZ77_Compress(data, true);
            else if(comp == CompressionType.LZComp) data = ROM.LZ77_Compress(data, false);

            parentFile.replace(data, this);
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

        public override uint getUintAt(int offset)
        {
            if(offset + 4 > fileSize) throw new Exception("Offset out of file bounds");
            return parentFile.getUintAt(offset + inlineOffs);
        }

        public override void setUintAt(int offset, uint val)
        {
            if(offset + 4 > fileSize) throw new Exception("Offset out of file bounds");
            parentFile.setUintAt(offset + inlineOffs, val);
        }

        public override ushort getUshortAt(int offset)
        {
            if(offset + 2 > fileSize) throw new Exception("Offset out of file bounds");
            return parentFile.getUshortAt(offset + inlineOffs);
        }

        public override void setUshortAt(int offset, ushort val)
        {
            if(offset + 2 > fileSize) throw new Exception("Offset out of file bounds");
            parentFile.setUshortAt(offset + inlineOffs, val);
        }

        public override byte getByteAt(int offset)
        {
            if(offset + 1 > fileSize) throw new Exception("Offset out of file bounds");
            return parentFile.getByteAt(offset + inlineOffs);
        }

        public override void setByteAt(int offset, byte val)
        {
            if(offset + 1 > fileSize) throw new Exception("Offset out of file bounds");
            parentFile.setByteAt(offset + inlineOffs, val);
        }
    }
}
