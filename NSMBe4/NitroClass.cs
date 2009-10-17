using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace NSMBe4 {
    public class NitroClass : FileSystem {
        /* Base */
        public string ROMFilename;
        public uint NTOffset;
        public uint NTSize;
        public uint FATOffset;
        public uint FATSize;
        public uint FileDataOffset = 0; //0 for NitroFS, start of GMIF in NARC

        private uint IconOffset; // used in NitroFS to fix overlay replacements..
        private uint ARM9Offset;
        private uint ARM7Offset;
        private uint Overlay9Offset;

        private bool isNestedFile = false; //this file is inside other file (e.g. a narc)

        /* Misc */
        public ushort FileCount;
        public uint FileLastEnd; //Relative to FileDataOffset

        private Stream rfs;

        private NitroClass parentFS;
        private ushort parentFileID;

        public NitroClass(string FileName) {
            this.ROMFilename = FileName;
        }

        public NitroClass(NitroClass parentFS, ushort parentFileID) {
            this.isNestedFile = true;
            this.parentFileID = parentFileID;
            this.parentFS = parentFS;
            this.ROMFilename = parentFS.FileNames[parentFileID];
        }


        /* Load a ROM */
        public void Load(FileLister lister) {
            this.lister = lister;

            if (isNestedFile) {
                //tricky code to get a resizble MemoryStream
                byte[] file = parentFS.ExtractFile(parentFileID);
                MemoryStream mms = new MemoryStream(file.Length);
                mms.Write(file, 0, file.Length);
                rfs = mms;
                CalcNarcOffsets();
            } else {
                // init stream
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.Read, FileShare.Read);
                rfs.Seek(0x40, SeekOrigin.Begin);

                // obtain base info
                NTOffset = ReadUInt(rfs);
                NTSize = ReadUInt(rfs);
                FATOffset = ReadUInt(rfs);
                FATSize = ReadUInt(rfs);

                // get the other offsets
                rfs.Seek(0x68, SeekOrigin.Begin);
                IconOffset = ReadUInt(rfs);

                rfs.Seek(0x20, SeekOrigin.Begin);
                ARM9Offset = ReadUInt(rfs);

                rfs.Seek(0x30, SeekOrigin.Begin);
                ARM7Offset = ReadUInt(rfs);

                rfs.Seek(0x50, SeekOrigin.Begin);
                Overlay9Offset = ReadUInt(rfs);
            }

            LoadCommon();
        }

        private void LoadCommon() {
            ResetDictionaries();


            // start reading
            if (!isNestedFile) {
                loadOverlayTable(0x50, LanguageManager.Get("NitroClass", "Overlay9"), 65534);
                loadOverlayTable(0x58, LanguageManager.Get("NitroClass", "Overlay7"), 65535);
            }

            LoadDir(string.Format(LanguageManager.Get("NitroClass", "Root"), ROMFilename.Substring(ROMFilename.LastIndexOf("\\") + 1)), 61440, 0);

            if (!isNestedFile) {
                rfs.Dispose();
            }
            lister = null; //we ensure not sending more to this lister.
        }

        private void CalcNarcOffsets() {
            //I have to do some tricky offset calculations here ...
            FATOffset = 0x1C;
            rfs.Seek(0x18, SeekOrigin.Begin); //number of files
            FATSize = ReadUInt(rfs) * 8;

            rfs.Seek(FATSize + FATOffset + 4, SeekOrigin.Begin); //size of FNTB
            NTSize = ReadUInt(rfs) - 8; //do not include header
            NTOffset = FATSize + FATOffset + 8;

            FileDataOffset = NTSize + NTOffset + 8;
        }

        private void loadOverlayTable(int locOffset, string name, ushort DirID) {
            //Creates a fake dir with all the overlay files

            if (lister != null)
                lister.DirReady(DirID, 0, name, true);

            rfs.Seek(locOffset, SeekOrigin.Begin);
            uint tableOffset = ReadUInt(rfs);
            uint tableSize = ReadUInt(rfs);

            rfs.Seek(tableOffset, SeekOrigin.Begin);

            for (int i = 0; i < tableSize / 32; i++) {
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
        private void LoadDir(string DirName, ushort DirID, ushort Parent) {
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
        private void LoadFile(string FileName, ushort FileID, ushort Parent) {
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
            if (!isNestedFile) //open file
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.Read, FileShare.Read);

            //read file
            rfs.Seek(FileOffsets[FileID] + FileDataOffset, SeekOrigin.Begin);
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
            //Console.WriteLine("Replacing file {0}.. old size {1}, new size {2}", FileNames[FileID], FileSizes[FileID], NewFile.Length);

            if (Diff > 0) { //if file grows
                //Console.WriteLine("File grew!");
                uint uDiff = (uint)Diff;
                //move all files after this one right
                //update the FAT
                Dictionary<ushort, string>.KeyCollection FileIDs = FileNames.Keys;

                foreach (ushort ModID in FileIDs) {
                    uint NewFileOffset = FileOffsets[ModID];
                    if (NewFileOffset > OldOffset) {
                        //Console.WriteLine("Moving file ID {0} named {1}", ModID, FileNames[ModID]);
                        NewFileOffset += uDiff;
                        FileOffsets[ModID] = NewFileOffset;
                        rfs.Seek(FATOffset + (ModID * 8), SeekOrigin.Begin);
                        WriteUInt(rfs, NewFileOffset);
                        TempEnd = ReadUInt(rfs) + uDiff;
                        rfs.Seek(-4, SeekOrigin.Current);
                        WriteUInt(rfs, TempEnd);
                    }
                }

                //Console.WriteLine("Moving up the data after by {0} bytes", uDiff);
                //Move the file data itself
                rfs.Seek(OldOffset + FileDataOffset, SeekOrigin.Begin);
                byte[] MoveBuffer = new byte[FileLastEnd - OldOffset + 1];
                rfs.Read(MoveBuffer, 0, MoveBuffer.Length);
                rfs.Seek(OldOffset + FileDataOffset + Diff, SeekOrigin.Begin);
                rfs.Write(MoveBuffer, 0, MoveBuffer.Length);
                FileLastEnd += uDiff;

                // nitroFS is retarded, overlay files are located before the FAT/FNT
                // let's fix that!
                // narcs always have the FAT/FNT before the files so we don't need to worry
                // about using FileDataOffset here
                if (!isNestedFile) {
                    bool headerchanged = false;

                    if (OldOffset < FATOffset) {
                        FATOffset += uDiff;
                        rfs.Seek(0x48, SeekOrigin.Begin);
                        WriteUInt(rfs, FATOffset);
                        headerchanged = true;
                    }

                    if (OldOffset < NTOffset) {
                        NTOffset += uDiff;
                        rfs.Seek(0x40, SeekOrigin.Begin);
                        WriteUInt(rfs, NTOffset);
                        headerchanged = true;
                    }

                    if (OldOffset < IconOffset) {
                        IconOffset += uDiff;
                        rfs.Seek(0x68, SeekOrigin.Begin);
                        WriteUInt(rfs, IconOffset);
                        headerchanged = true;
                    }

                    if (OldOffset < ARM9Offset) {
                        ARM9Offset += uDiff;
                        rfs.Seek(0x20, SeekOrigin.Begin);
                        WriteUInt(rfs, ARM9Offset);
                        headerchanged = true;
                    }

                    if (OldOffset < ARM7Offset) {
                        ARM7Offset += uDiff;
                        rfs.Seek(0x30, SeekOrigin.Begin);
                        WriteUInt(rfs, ARM7Offset);
                        headerchanged = true;
                    }

                    if (OldOffset < Overlay9Offset) {
                        Overlay9Offset += uDiff;
                        rfs.Seek(0x50, SeekOrigin.Begin);
                        WriteUInt(rfs, Overlay9Offset);
                        headerchanged = true;
                    }

                    if (headerchanged) UpdateHeaderCRC();
                }
            }

            //update THIS FILE

            //File data
            rfs.Seek(OldOffset + FileDataOffset, SeekOrigin.Begin);
            rfs.Write(NewFile, 0, NewFile.Length);
            //Console.WriteLine("Writing the new file at {0}", OldOffset+FileDataOffset);

            //FAT
            rfs.Seek(FATOffset + 4 + (FileID * 8), SeekOrigin.Begin);
            TempEnd = ReadUInt(rfs);
            rfs.Seek(-4, SeekOrigin.Current);
            WriteUInt(rfs, Convert.ToUInt32(TempEnd + Diff));

            //Keep the sizes updated
            FileSizes[FileID] = (uint)NewFile.Length;

            // if it's overlay 0, update the overlay table
            // I need to figure out a better way to do this..
            if (FileID == 0) {
                // THIS DISABLES COMPRESSION!!
                // setting 0x1F in the overlay table to 02 is what causes the game
                // to bypass it - I'm not sure if the RAM size needs to be written
                // as well, but I do it anyway just in case..
                rfs.Seek(Overlay9Offset + 8, SeekOrigin.Begin);
                WriteUInt(rfs, (uint)NewFile.Length);
                rfs.Seek(Overlay9Offset + 0x1F, SeekOrigin.Begin);
                rfs.WriteByte(2);
            }

            //save modified file
            if (isNestedFile)
                parentFS.ReplaceFile(parentFileID, (rfs as MemoryStream).ToArray());
            else
                rfs.Dispose();
        }

        private ushort[] CRC16Table = {
            0x0000, 0xC0C1, 0xC181, 0x0140, 0xC301, 0x03C0, 0x0280, 0xC241,
            0xC601, 0x06C0, 0x0780, 0xC741, 0x0500, 0xC5C1, 0xC481, 0x0440,
            0xCC01, 0x0CC0, 0x0D80, 0xCD41, 0x0F00, 0xCFC1, 0xCE81, 0x0E40,
            0x0A00, 0xCAC1, 0xCB81, 0x0B40, 0xC901, 0x09C0, 0x0880, 0xC841,
            0xD801, 0x18C0, 0x1980, 0xD941, 0x1B00, 0xDBC1, 0xDA81, 0x1A40,
            0x1E00, 0xDEC1, 0xDF81, 0x1F40, 0xDD01, 0x1DC0, 0x1C80, 0xDC41,
            0x1400, 0xD4C1, 0xD581, 0x1540, 0xD701, 0x17C0, 0x1680, 0xD641,
            0xD201, 0x12C0, 0x1380, 0xD341, 0x1100, 0xD1C1, 0xD081, 0x1040,
            0xF001, 0x30C0, 0x3180, 0xF141, 0x3300, 0xF3C1, 0xF281, 0x3240,
            0x3600, 0xF6C1, 0xF781, 0x3740, 0xF501, 0x35C0, 0x3480, 0xF441,
            0x3C00, 0xFCC1, 0xFD81, 0x3D40, 0xFF01, 0x3FC0, 0x3E80, 0xFE41,
            0xFA01, 0x3AC0, 0x3B80, 0xFB41, 0x3900, 0xF9C1, 0xF881, 0x3840,
            0x2800, 0xE8C1, 0xE981, 0x2940, 0xEB01, 0x2BC0, 0x2A80, 0xEA41,
            0xEE01, 0x2EC0, 0x2F80, 0xEF41, 0x2D00, 0xEDC1, 0xEC81, 0x2C40,
            0xE401, 0x24C0, 0x2580, 0xE541, 0x2700, 0xE7C1, 0xE681, 0x2640,
            0x2200, 0xE2C1, 0xE381, 0x2340, 0xE101, 0x21C0, 0x2080, 0xE041,
            0xA001, 0x60C0, 0x6180, 0xA141, 0x6300, 0xA3C1, 0xA281, 0x6240,
            0x6600, 0xA6C1, 0xA781, 0x6740, 0xA501, 0x65C0, 0x6480, 0xA441,
            0x6C00, 0xACC1, 0xAD81, 0x6D40, 0xAF01, 0x6FC0, 0x6E80, 0xAE41,
            0xAA01, 0x6AC0, 0x6B80, 0xAB41, 0x6900, 0xA9C1, 0xA881, 0x6840,
            0x7800, 0xB8C1, 0xB981, 0x7940, 0xBB01, 0x7BC0, 0x7A80, 0xBA41,
            0xBE01, 0x7EC0, 0x7F80, 0xBF41, 0x7D00, 0xBDC1, 0xBC81, 0x7C40,
            0xB401, 0x74C0, 0x7580, 0xB541, 0x7700, 0xB7C1, 0xB681, 0x7640,
            0x7200, 0xB2C1, 0xB381, 0x7340, 0xB101, 0x71C0, 0x7080, 0xB041,
            0x5000, 0x90C1, 0x9181, 0x5140, 0x9301, 0x53C0, 0x5280, 0x9241,
            0x9601, 0x56C0, 0x5780, 0x9741, 0x5500, 0x95C1, 0x9481, 0x5440,
            0x9C01, 0x5CC0, 0x5D80, 0x9D41, 0x5F00, 0x9FC1, 0x9E81, 0x5E40,
            0x5A00, 0x9AC1, 0x9B81, 0x5B40, 0x9901, 0x59C0, 0x5880, 0x9841,
            0x8801, 0x48C0, 0x4980, 0x8941, 0x4B00, 0x8BC1, 0x8A81, 0x4A40,
            0x4E00, 0x8EC1, 0x8F81, 0x4F40, 0x8D01, 0x4DC0, 0x4C80, 0x8C41,
            0x4400, 0x84C1, 0x8581, 0x4540, 0x8701, 0x47C0, 0x4680, 0x8641,
            0x8201, 0x42C0, 0x4380, 0x8341, 0x4100, 0x81C1, 0x8081, 0x4040
        };

        // from ndstool
        public ushort CalcCRC16(byte[] data) {
            ushort crc = 0xFFFF;

            for (int i = 0; i < data.Length; i++) {
                crc = (ushort)((crc >> 8) ^ CRC16Table[(crc ^ data[i]) & 0xFF]);
            }

            return crc;
        }

        public void UpdateHeaderCRC() {
            byte[] header = new byte[0x15E];

            bool CreatedRFS = false;
            if (rfs != null) {
                try {
                    rfs.Seek(0, SeekOrigin.Begin);
                } catch (ObjectDisposedException) {
                    rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                    CreatedRFS = true;
                }
            } else {
                rfs = new FileStream(ROMFilename, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
                CreatedRFS = true;
            }

            rfs.Seek(0, SeekOrigin.Begin);
            rfs.Read(header, 0, 0x15E);
            WriteUShort(rfs, CalcCRC16(header));

            if (CreatedRFS) {
                rfs.Dispose();
            }
        }
    }
}
