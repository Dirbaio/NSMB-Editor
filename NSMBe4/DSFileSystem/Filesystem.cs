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
using System.Drawing;

namespace NSMBe4.DSFileSystem
{
    public abstract class Filesystem
    {
        public List<File> allFiles = new List<File>();
        public List<Directory> allDirs = new List<Directory>();
        protected Dictionary<int, File> filesById = new Dictionary<int, File>();
        protected Dictionary<int, Directory> dirsById = new Dictionary<int, Directory>();
        public Directory mainDir;

//        public FilesystemBrowser viewer;

        public File getFileById(int id)
        {
            if (!filesById.ContainsKey(id))
                return null;
            return filesById[id];
        }

        public File getFileByName(string name)
        {
            foreach (File f in allFiles)
                if (f.name == name)
                    return f;

            return null;
        }

        public Directory getDirByPath(string path)
        {
            string[] shit = path.Split(new char[] { '/' });
            Directory dir = mainDir;
            for (int i = 0; i < shit.Length; i++)
            {
                Directory newDir = null;
                foreach(Directory d in dir.childrenDirs)
                    if(d.name == shit[i])
                    {
                        newDir = d;
                        break;
                    }
                if(newDir == null) return null;

                dir = newDir;
            }
            return dir;
        }

        protected void addFile(File f)
        {
            allFiles.Add(f);
            if(f.id != -1)
                if(!filesById.ContainsKey(f.id))
                    filesById.Add(f.id, f);
//            filesByName.Add(f.name, f);
        }


        protected void addDir(Directory d)
        {
            allDirs.Add(d);
            if(d.id != -1)
               dirsById.Add(d.id, d);
//            dirsByName.Add(d.name, d);
        }

        public int alignUp(int what, int align)
        {
            if (what % align != 0)
                what += align - what % align;
            return what;
        }
        public int alignDown(int what, int align)
        {
            what -= what % align;
            return what;
        }



        public virtual void fileMoved(File f)
        {
        }


        public uint readUInt(Stream s)
        {
            uint res = 0;
            for (int i = 0; i < 4; i++)
            {
                res |= (uint)s.ReadByte() << 8 * i;
            }
            return res;
        }

		//Saving and closing
		public virtual void save() {}
		public virtual void close() {}

    }
}
