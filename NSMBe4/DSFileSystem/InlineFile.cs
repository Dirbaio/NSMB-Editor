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
    public class InlineFile : FileWithLock
    {
        private int inlineOffs;
        private int inlineLen;
        private File parentFile;
        public InlineFile(File parent, int offs, int len, string name)
        {
        	nameP = name;
        	fileSizeP = len;
            parentFile = parent;
            inlineOffs = offs;
            inlineLen = len;
        }

        public override byte[] getContents()
        {
        	return parentFile.getInterval(inlineOffs, inlineOffs+inlineLen);
        }

        public override void replace(byte[] newFile, object editor)
        {
            if (!isAGoodEditor(editor))
                throw new Exception("NOT CORRECT EDITOR " + name);
            if(newFile.Length != inlineLen)
            	throw new Exception("Trying to resize an InlineFile: "+name);
            
            parentFile.replaceInterval(newFile, inlineOffs);
        }
        
		public override byte[] getInterval(int start, int end)
		{
			validateInterval(start, end);
			return parentFile.getInterval(inlineOffs+start, inlineOffs+end);
		}
		
        public override void replaceInterval(byte[] newFile, int start)
		{
			validateInterval(start, start+newFile.Length);
			parentFile.replaceInterval(newFile, inlineOffs+start);
		}

        public override void editionStarted() 
        {
        	parentFile.beginEditInterval(inlineOffs, inlineOffs+inlineLen);
        }

        public override void editionEnded() 
        {
        	parentFile.endEditInterval(inlineOffs, inlineOffs+inlineLen);
        }

    }
}
