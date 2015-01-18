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
    public class LZFile : FileWithLock
    {
        private File parentFile;
        private CompressionType comp;

        public enum CompressionType : int
        {
            None,
            LZ,
            // Used for palette files, they might be compressed, might not
            MaybeLZ,
            LZWithHeader
        }
        
        public LZFile(File parent, CompressionType ct)
        {
        	nameP = parent.name;
            parentFile = parent;
			comp = ct;

            // If we think it might be compressed, try decompressing it. If it succeeds, assume it was compressed.
            if (comp == CompressionType.MaybeLZ) {
                try {
                    ROM.LZ77_Decompress(parentFile.getContents());
                    comp = CompressionType.LZ;
                } catch {
                    comp = CompressionType.None;
                }
            }

            if (comp == CompressionType.None)
        		fileSizeP = parent.fileSize;
            else if (comp == CompressionType.LZ)
            	fileSizeP = ROM.LZ77_GetDecompressedSize(parent.getInterval(0, 4));
            else if (comp == CompressionType.LZWithHeader)
            	fileSizeP = ROM.LZ77_GetDecompressedSizeWithHeader(parent.getInterval(0, 8));
        }

        public override byte[] getContents()
        {
            if(comp == CompressionType.LZWithHeader)
                return ROM.LZ77_DecompressWithHeader(parentFile.getContents());
            else if(comp == CompressionType.LZ)
                return ROM.LZ77_Decompress(parentFile.getContents());
            else 
            	return parentFile.getContents();
        }

        public override void replace(byte[] newFile, object editor)
        {
            if (!isAGoodEditor(editor))
                throw new Exception("NOT CORRECT EDITOR " + name);

            if(comp == CompressionType.LZWithHeader)
                parentFile.replace(ROM.LZ77_Compress(newFile, true), this);
            else if(comp == CompressionType.LZ)
                parentFile.replace(ROM.LZ77_Compress(newFile, false), this);
            else 
            	parentFile.replace(newFile, this);
        }
        
		public override byte[] getInterval(int start, int end)
		{
            if (comp == CompressionType.None)
	            return parentFile.getInterval(start, end);
    
            byte[] data;
            if(comp == CompressionType.LZWithHeader)
                data = ROM.LZ77_DecompressWithHeader(parentFile.getContents());
            else
                data = ROM.LZ77_Decompress(parentFile.getContents());

            int len = end-start;
            byte[] thisdata = new byte[len];
            Array.Copy(data, start, thisdata, 0, len);
            return thisdata;
   		}
		
        public override void replaceInterval(byte[] newFile, int start)
		{
			validateInterval(start, start+newFile.Length);

            if (comp == CompressionType.None)
            	parentFile.replaceInterval(newFile, start);
			else
            {
                byte[] data;
                if (comp == CompressionType.LZWithHeader)
                    data = ROM.LZ77_DecompressWithHeader(parentFile.getContents());
                else
                    data = ROM.LZ77_Decompress(parentFile.getContents());
                Array.Copy(newFile, 0, data, start, newFile.Length);
                parentFile.replace(ROM.LZ77_Compress(data, comp == CompressionType.LZWithHeader), this);
            }
        }

        public override void startEdition() 
        {
        	parentFile.beginEdit(this);
        }
        public override void endEdition() 
        {
        	parentFile.endEdit(this);
        }
    }
}

