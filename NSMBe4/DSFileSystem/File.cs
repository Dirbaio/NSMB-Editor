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

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.DSFileSystem
{
	public abstract class File
	{
        private Filesystem parentP;
        public Filesystem parent { get { return parentP; } }

        private Directory parentDirP;
        public Directory parentDir { get { return parentDirP; } }

        protected string nameP;
        public string name { get { return nameP; } }

        private int idP;
        public int id { get { return idP; } }
        public bool isSystemFile { get { return idP<0; } }

        protected int fileSizeP;
        public int fileSize { get { return fileSizeP; } }

        private Object editedBy = null;
        public Boolean beingEdited { get { return editedBy != null; } }

		public File(Filesystem parent, Directory parentDir, string name, int id)
		{
			this.parentP = parent;
			this.parentDirP = parentDir;
			this.nameP = name;
			this.idP = id;
		}

		//File functions
        public abstract byte[] getContents();
        public abstract void replace(byte[] newFile, object editor);

        public abstract uint getUintAt(int offset);
        public abstract void setUintAt(int offset, uint val);
        public abstract ushort getUshortAt(int offset);
        public abstract void setUshortAt(int offset, ushort val);
        public abstract byte getByteAt(int offset);
        public abstract void setByteAt(int offset, byte val);
        
		//Helper functions
        public bool isAGoodEditor(object editor)
        {
            if (!beingEdited)
                return false;

            if (editor == editedBy)
                return true;

            if (editor is InlineFile && inlineEditors.Contains(editor as InlineFile))
                return true;

            return false;
        }
        

        public virtual void beginEdit(Object editor)
        {
            if (beingEdited)
                throw new AlreadyEditingException(this);
            else
                editedBy = editor;
        }

        public virtual void endEdit(Object editor)
        {
            if (!beingEdited)
                throw new Exception("NOT EDITING FILE " + name);
            if (editor != editedBy)
                throw new Exception("NOT CORRECT EDITOR" + name);

            editedBy = null;
        }

        public bool beingEditedBy(Object ed)
        {
            return ed == editedBy;
        }

        public string getPath()
        {
            return parentDir.getPath() + "/" + name;
        }

		//Hack that needs to die.
		//Or needs to done better (like, not 2 inline files at the same region at the same time)
        private List<InlineFile> inlineEditors = new List<InlineFile>();

        public void beginEditInline(InlineFile f)
        {
            if (inlineEditors.Count == 0)
                beginEdit(this);

            inlineEditors.Add(f);
        }

        public void endEditInline(InlineFile f)
        {
            if (!inlineEditors.Contains(f))
                throw new Exception("ERROR: INLINE FILE");
            inlineEditors.Remove(f);
            if (inlineEditors.Count == 0)
                endEdit(this);
        }        
	}
}

