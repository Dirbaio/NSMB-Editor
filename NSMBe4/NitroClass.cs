using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace NSMBe4 {
    public class NitroClass : FileSystem
    {
        /* Base */
        public string ROMFilename;
        public uint NTOffset;
        public uint NTSize;
        public uint FATOffset;
        public uint FATSize;
        public uint FileDataOffset = 0; //0 for NitroFS, start of GMIF in NARC

        private bool isNestedFile = false; //this file is inside other file (e.g. a narc)

        /* Misc */
        public ushort FileCount;
        public uint FileLastEnd; //Relative to FileDataOffset

        private Stream rfs;

        private NitroClass parentFS;
        private ushort parentFileID;

        public NitroClass(string FileName)
        {
            this.ROMFilename = FileName;
        }

        public NitroClass(NitroClass parentFS, ushort parentFileID)
        {
            this.isNestedFile = true;
            this.parentFileID = parentFileID;
            this.parentFS = parentFS;
            this.ROMFilename = parentFS.FileNames[parentFileID];
        }


        /* Load a ROM */
        public void Load(FileLister lister)
        {
            this.lister = lister;

            if (isNestedFile)
            {
                //tricky code to get a resizble MemoryStream
                byte[] file = parentFS.ExtractFile(parentFileID);
                MemoryStream mms = new MemoryStream(file.Length);
                mms.Write(file, 0, file.Length);
                rfs = mms;
                CalcNarcOffsets();
            }
            else
            {
                // init stream
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
                rfs.Seek(0x40, SeekOrigin.Begin);

                // obtain base info
                NTOffset = ReadUInt(rfs);
                NTSize = ReadUInt(rfs);
                FATOffset = ReadUInt(rfs);
                FATSize = ReadUInt(rfs);
            }

            LoadCommon();
        }

        private void LoadCommon()
        {
            ResetDictionaries();


            // start reading
            LoadDir(string.Format(LanguageManager.Get("NitroClass", "Root"), ROMFilename.Substring(ROMFilename.LastIndexOf("\\")+1)), 61440, 0);

            if (!isNestedFile)
            {
                loadOverlayTable(0x50, LanguageManager.Get("NitroClass", "Overlay9"), 65534);
                loadOverlayTable(0x58, LanguageManager.Get("NitroClass", "Overlay7"), 65535);
                rfs.Dispose();
            }
            lister = null; //we ensure not sending more to this lister.
        }

        private void CalcNarcOffsets()
        {
            //I have to do some tricky offset calculations here ...
            FATOffset = 0x1C;
            rfs.Seek(0x18, SeekOrigin.Begin); //number of files
            FATSize = ReadUInt(rfs) * 8;

            rfs.Seek(FATSize + FATOffset + 4, SeekOrigin.Begin); //size of FNTB
            NTSize = ReadUInt(rfs) - 8; //do not include header
            NTOffset = FATSize + FATOffset + 8;

            FileDataOffset = NTSize + NTOffset + 8;
        }

        private void loadOverlayTable(int locOffset, string name, ushort DirID)
        {
            //Creates a fake dir with all the overlay files

            if (lister != null)
                lister.DirReady(DirID, 0, name, true);

            rfs.Seek(locOffset, SeekOrigin.Begin);
            uint tableOffset = ReadUInt(rfs);
            uint tableSize = ReadUInt(rfs);

            rfs.Seek(tableOffset, SeekOrigin.Begin);

            for (int i = 0; i < tableSize / 32; i++)
            {
                uint ovId = ReadUInt(rfs);
                uint ramAddr = ReadUInt(rfs);
                uint ramSize = ReadUInt(rfs);
                uint bssSize = ReadUInt(rfs);
                uint staticInitStart = ReadUInt(rfs);
                uint staticInitEnd = ReadUInt(rfs);
                ushort fileID = ReadUShort(rfs);
                rfs.Seek(2, SeekOrigin.Current); //skip 0's
                rfs.Seek(4, SeekOrigin.Current); //skip 0's

                LoadFile(string.Format(LanguageManager.Get("NitroClass", "OverlayFile"), ovId, ramAddr.ToString("X"), ramSize.ToString("X")), fileID, DirID);
            }
        }
        /* Load a Directory */
        private void LoadDir(string DirName, ushort DirID, ushort Parent)
        {
            long PreviousSeek = rfs.Position;
            rfs.Seek(NTOffset + (8 * (DirID & 0xFFF)), SeekOrigin.Begin);
            uint EntryStart = ReadUInt(rfs);
            ushort EntryFileID = ReadUShort(rfs);
            ushort ParentID = ReadUShort(rfs);

            DirIDs[DirName] = DirID;

            // list to class owner
            if (lister != null)
                lister.DirReady(DirID, Parent, DirName, Parent == 0);
            
            // read FNT entries
            rfs.Seek(NTOffset + EntryStart, SeekOrigin.Begin);
            ushort CurFile = EntryFileID;
            while (true) {
                byte EntryData = (byte)rfs.ReadByte();
                byte NameLength = (byte)(EntryData & 127);
                if (NameLength == 0) break;
                // read entry
                string EntryName = ReadString(rfs, NameLength);
                if (EntryData > 127) {
                    // directory
                    ushort SubTableID = ReadUShort(rfs);
                    LoadDir(EntryName, SubTableID, DirID);
                } else {
                    // file
                    LoadFile(EntryName, CurFile, DirID);
                }
                CurFile++;
            }

            rfs.Seek(PreviousSeek, SeekOrigin.Begin);
        }

        /* Load a File */
        private void LoadFile(string FileName, ushort FileID, ushort Parent)
        {
            long PreviousSeek = rfs.Position;

            //read file pos and size
            rfs.Seek(FATOffset + (FileID * 8), SeekOrigin.Begin);
            uint FATStart = ReadUInt(rfs);
            uint FATEnd = ReadUInt(rfs);

            //list file
            if (lister != null) lister.FileReady(FileID, Parent, FileName);

            //get data
            FileIDs[FileName] = FileID;
            FileNames[FileID] = FileName;
            FileOffsets[FileID] = FATStart;
            FileSizes[FileID] = FATEnd - FATStart;
            FileParentIDs[FileID] = Parent;

            //keep things updated
            if (FATEnd > FileLastEnd) FileLastEnd = FATEnd;
            FileCount++;

            //leave position as it were
            rfs.Seek(PreviousSeek, SeekOrigin.Begin);
        }

        /* Extract a File */
        public override byte[] ExtractFile(ushort FileID) {
            if(!isNestedFile) //open file
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.Read, FileShare.Read);

            //read file
            rfs.Seek(FileOffsets[FileID]+FileDataOffset, SeekOrigin.Begin);
            byte[] TempFile = new byte[FileSizes[FileID]];
            rfs.Read(TempFile, 0, (int)FileSizes[FileID]);

            //close file
            if (!isNestedFile)
                rfs.Dispose();

            //return
            return TempFile;
        }

        /* Reinsert a File */
        public override void ReplaceFile(ushort FileID, byte[] NewFile) {
            if (!isNestedFile)
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            uint OldOffset = FileOffsets[FileID];
            uint TempEnd;

            int Diff = (int)(NewFile.Length - FileSizes[FileID]);

            if (Diff > 0) { //if file grows
                uint uDiff = (uint)Diff;
                //move all files after this one right
                //update the FAT
                Dictionary<ushort, string>.KeyCollection FileIDs = FileNames.Keys;
                foreach (ushort ModID in FileIDs)
                {
                    uint NewFileOffset = FileOffsets[ModID];
                    if (NewFileOffset > OldOffset)
                    {
                        NewFileOffset += uDiff;
                        FileOffsets[ModID] = NewFileOffset;
                        rfs.Seek(FATOffset + (ModID * 8), SeekOrigin.Begin);
                        WriteUInt(rfs, NewFileOffset);
                        TempEnd = ReadUInt(rfs) + uDiff;
                        rfs.Seek(-4, SeekOrigin.Current);
                        WriteUInt(rfs, TempEnd);
                    }
                }

                //Move the file data itself
                rfs.Seek(OldOffset + FileDataOffset, SeekOrigin.Begin);
                byte[] MoveBuffer = new byte[FileLastEnd - OldOffset + 1];
                rfs.Read(MoveBuffer, 0, MoveBuffer.Length);
                rfs.Seek(OldOffset + FileDataOffset + Diff, SeekOrigin.Begin);
                rfs.Write(MoveBuffer, 0, MoveBuffer.Length);
                FileLastEnd += uDiff;
            }

            //update THIS FILE

            //File data
            rfs.Seek(OldOffset+FileDataOffset, SeekOrigin.Begin);
            rfs.Write(NewFile, 0, NewFile.Length);

            //FAT
            rfs.Seek(FATOffset + 4 + (FileID * 8), SeekOrigin.Begin);
            TempEnd = ReadUInt(rfs);
            rfs.Seek(-4, SeekOrigin.Current);
            WriteUInt(rfs, Convert.ToUInt32(TempEnd + Diff));

            //Keep the sizes updated
            FileSizes[FileID] = (uint)NewFile.Length;

            //save modified file
            if (isNestedFile)
                parentFS.ReplaceFile(parentFileID, (rfs as MemoryStream).ToArray());
            else
                rfs.Dispose();
        }


    }
}
