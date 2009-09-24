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
        public uint FileLocations = 0; //0 for NitroFS, start of GMIF in NARC

        private bool isNestedFile = false; //this file is inside other file (e.g. a narc)

        /* Misc */
        public ushort FileCount;
        public uint FileLastEnd;

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
            LoadDir("Root", 61440, 0);

            // clear up
            if(!isNestedFile)
                rfs.Dispose();
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

            FileLocations = NTSize + NTOffset + 8;
        }

        /* Load a Directory */
        private void LoadDir(string DirName, ushort DirID, ushort Parent) {
            long PreviousSeek = rfs.Position;
            rfs.Seek(NTOffset + (8 * (DirID & 0xFFF)), SeekOrigin.Begin);
            uint EntryStart = ReadUInt(rfs);
            ushort EntryFileID = ReadUShort(rfs);
            ushort ParentID = ReadUShort(rfs);

            DirIDs[DirName] = DirID;

            // list to class owner
            if (Parent == 0) {
                if (lister != null) lister.DirReady(61440, 0, "Root", true);
            } else {
                if (lister != null) lister.DirReady(DirID, Parent, DirName, false);
            }

            // read FNT entries
            rfs.Seek(NTOffset + EntryStart, SeekOrigin.Begin);
            ushort CurFile = EntryFileID;
            while (true) {
                byte EntryLength = (byte)rfs.ReadByte();
                byte NameLength = (byte)(EntryLength & 127);
                if (NameLength == 0) break;
                // read entry
                string EntryName = ReadString(rfs, NameLength);
                if (EntryLength > 127) {
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
        private void LoadFile(string FileName, ushort FileID, ushort Parent) {
            long PreviousSeek = rfs.Position;
            rfs.Seek(FATOffset + (FileID * 8), SeekOrigin.Begin);
            uint FATStart = ReadUInt(rfs) + FileLocations;
            uint FATEnd = ReadUInt(rfs) + FileLocations;
            if (lister != null) lister.FileReady(FileID, Parent, FileName);

            FileIDs[FileName] = FileID;
            FileNames[FileID] = FileName;
            FileOffsets[FileID] = FATStart;
            FileSizes[FileID] = FATEnd - FATStart;
            FileParentIDs[FileID] = Parent;

            if (FATEnd > FileLastEnd) FileLastEnd = FATEnd;
            FileCount++;
            rfs.Seek(PreviousSeek, SeekOrigin.Begin);
        }

        /* Extract a File */
        public override byte[] ExtractFile(ushort FileID) {
            if(!isNestedFile)
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
            rfs.Seek(FileOffsets[FileID], SeekOrigin.Begin);
            byte[] TempFile = new byte[FileSizes[FileID]];
            rfs.Read(TempFile, 0, (int)FileSizes[FileID]);
            if (!isNestedFile)
                rfs.Dispose();
            return TempFile;
        }

        /* Reinsert a File */
        public override void ReplaceFile(ushort FileID, byte[] NewFile) {
            if (!isNestedFile)
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            uint OldOffset = FileOffsets[FileID];
            uint TempEnd;
            int cDiff = (int)(NewFile.Length - FileSizes[FileID]);
            if (cDiff > 0) {
                uint Diff = (uint)cDiff;
                Dictionary<ushort, string>.KeyCollection EnumThis = FileNames.Keys;
                foreach (ushort ModID in EnumThis) {
                    uint ModOffset = FileOffsets[ModID];
                    if (ModOffset > OldOffset) {
                        ModOffset += Diff;
                        FileOffsets[ModID] = ModOffset;
                        rfs.Seek(FATOffset + (ModID * 8), SeekOrigin.Begin);
                        WriteUInt(rfs, ModOffset);
                        TempEnd = ReadUInt(rfs) + Diff;
                        rfs.Seek(-4, SeekOrigin.Current);
                        WriteUInt(rfs, TempEnd);
                    }
                }
                rfs.Seek(OldOffset, SeekOrigin.Begin);
                byte[] MoveBuffer = new byte[FileLastEnd - OldOffset + 1];
                rfs.Read(MoveBuffer, 0, MoveBuffer.Length);
                rfs.Seek(OldOffset + Diff, SeekOrigin.Begin);
                rfs.Write(MoveBuffer, 0, MoveBuffer.Length);
                FileLastEnd += Diff;
            }
            rfs.Seek(OldOffset, SeekOrigin.Begin);
            rfs.Write(NewFile, 0, NewFile.Length);
            rfs.Seek(FATOffset + 4 + (FileID * 8), SeekOrigin.Begin);
            TempEnd = ReadUInt(rfs);
            rfs.Seek(-4, SeekOrigin.Current);
            WriteUInt(rfs, Convert.ToUInt32((int)TempEnd + cDiff));
            FileSizes[FileID] = (uint)NewFile.Length;
            if (isNestedFile)
                parentFS.ReplaceFile(parentFileID, (rfs as MemoryStream).ToArray());
            else
                rfs.Dispose();
        }


    }
}
