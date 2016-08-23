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
using System.IO;

namespace NSMBe4.Patcher
{
    public class PatchMaker
    {
        private int ArenaLoOffs;
        Arm9BinaryHandler handler;
        DirectoryInfo romdir;

        public PatchMaker(DirectoryInfo romdir)
        {
            handler = new Arm9BinaryHandler();
            this.romdir = romdir;
        }

		public uint getCodeAddr()
		{
            handler.load();
            loadArenaLoOffsFile(romdir);
            uint codeAddr = handler.readFromRamAddr(ArenaLoOffs, -1);
            return codeAddr;
		}
		
		public void restore()
		{
            handler.restoreFromBackup();
		}
		
        public void compilePatch()
        {
            uint codeAddr = getCodeAddr();
            PatchCompiler.compilePatch(codeAddr, romdir);
        }

        public void generatePatch()
        {
        	int codeAddr = (int) getCodeAddr();
            Console.Out.WriteLine(String.Format("New code address: {0:X8}", codeAddr));

            FileInfo f = new FileInfo(romdir.FullName + "/newcode.bin");
            if (!f.Exists) return;
            FileStream fs = f.OpenRead();

            byte[] newdata = new byte[fs.Length];
            fs.Read(newdata, 0, (int)fs.Length);
            fs.Close();

            ByteArrayOutputStream extradata = new ByteArrayOutputStream();

            extradata.write(newdata);
            extradata.align(4);
            int hookAddr = codeAddr + extradata.getPos();


            f = new FileInfo(romdir.FullName + "/newcode.sym");
            StreamReader s = f.OpenText();

            while (!s.EndOfStream)
            {
                string l = s.ReadLine();

                int ind = -1;
                if (l.Contains("nsub_"))
                    ind = l.IndexOf("nsub_");
                if (l.Contains("hook_"))
                    ind = l.IndexOf("hook_");
                if (l.Contains("repl_"))
                    ind = l.IndexOf("repl_");
                if (l.Contains("xrpl_"))
                    ind = l.IndexOf("xrpl_");
                if (l.Contains("lrpl_"))
                    ind = l.IndexOf("lrpl_");

                if (ind != -1)
                {
                    int destRamAddr= parseHex(l.Substring(0, 8));    //Redirect dest addr
                    int ramAddr = parseHex(l.Substring(ind + 5, 8)); //Patched addr
                    uint val = 0;

                    int ovId = -1;
                    if (l.Contains("_ov_"))
                        ovId = parseHex(l.Substring(l.IndexOf("_ov_") + 4, 2));

                    int patchCategory = 0;

                    string cmd = l.Substring(ind, 4);
                    int thisHookAddr = 0;

                    switch(cmd)
                    {
                        case "nsub":
                            val = makeBranchOpcode(ramAddr, destRamAddr, 0);
                            break;
                        case "repl":
                            val = makeBranchOpcode(ramAddr, destRamAddr, 1);
                            break;
                        case "xrpl":
                            val = makeBranchOpcode(ramAddr, destRamAddr, 2);
                            break;
                        case "lrpl":
                            UInt16 lrvalue = 0xB500; //push {r14}
                            handler.writeToRamAddr(ramAddr, lrvalue, ovId);
                            ramAddr += 2;
                            val = makeBranchOpcode(ramAddr, destRamAddr, 2);
                            break;
                        case "hook":
                            //Jump to the hook addr
                            thisHookAddr = hookAddr;
                            val = makeBranchOpcode(ramAddr, hookAddr, 0);

                            uint originalOpcode = handler.readFromRamAddr(ramAddr, ovId);
                            
                            //TODO: Parse and fix original opcode in case of BL instructions
                            //so it's possible to hook over them too.
                            extradata.writeUInt(originalOpcode);
                            hookAddr += 4;
                            extradata.writeUInt(0xE92D5FFF); //push {r0-r12, r14}
                            hookAddr += 4;
                            extradata.writeUInt(makeBranchOpcode(hookAddr, destRamAddr, 1));
                            hookAddr += 4;
                            extradata.writeUInt(0xE8BD5FFF); //pop {r0-r12, r14}
                            hookAddr += 4;
                            extradata.writeUInt(makeBranchOpcode(hookAddr, ramAddr+4, 0));
                            hookAddr += 4;
                            extradata.writeUInt(0x12345678);
                            hookAddr += 4;
                            break;
                        default:
                            continue;
                    }

                    //Console.Out.WriteLine(String.Format("{0:X8}:{1:X8} = {2:X8}", patchCategory, ramAddr, val));
                    Console.Out.WriteLine(String.Format("              {0:X8} {1:X8}", destRamAddr, thisHookAddr));

                    handler.writeToRamAddr(ramAddr, val, ovId);
                }
            }

            s.Close();

            int newArenaOffs = codeAddr + extradata.getPos();
            handler.writeToRamAddr(ArenaLoOffs, (uint)newArenaOffs, -1);

            handler.sections.Add(new Arm9BinSection(extradata.getArray(), codeAddr, 0));
            handler.saveSections();
        }

        private void loadArenaLoOffsFile(DirectoryInfo romdir)
        {
            FileInfo f = new FileInfo(romdir.FullName + "/arenaoffs.txt");
            StreamReader s = f.OpenText();
            string l = s.ReadLine();
            ArenaLoOffs = int.Parse(l, System.Globalization.NumberStyles.HexNumber);
            s.Close();
        }


        public static uint makeBranchOpcode(int srcAddr, int destAddr, int withLink)
        {
            unchecked
            {
                uint res = (uint)0xEA000000;

                if (withLink == 1)
                    res |= 0x01000000;

                int offs = (destAddr / 4) - (srcAddr / 4) - 2;
                offs &= 0x00FFFFFF;

                res |= (uint)offs;

                if (withLink == 2)
                {
                    UInt16 res1 = 0xF000;
                    UInt16 res2 = 0xE800;
                    
                    offs = destAddr - srcAddr - 2;
                    offs >>= 2;
                    offs &= 0x003FFFFF;
                    
                    res1 |= (UInt16)((offs >> 10) & 0x7FF);
                    res2 |= (UInt16)((offs << 1)  & 0x7FF);

                    res = (uint)(((uint)res2 << 16) | res1);

                }

                return res;
            }
        }


        public static uint parseUHex(string s)
        {
            return uint.Parse(s, System.Globalization.NumberStyles.HexNumber);
        }

        public static int parseHex(string s)
        {
            return int.Parse(s, System.Globalization.NumberStyles.HexNumber);
        }
    }
}
