using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.Patcher
{
    public class PatchMaker
    {
        private int ArenaLoOffs;
        NSMBe4.DSFileSystem.Arm9BinaryHandler handler;
        DirectoryInfo romdir;

        public PatchMaker(DirectoryInfo romdir)
        {
            handler = new DSFileSystem.Arm9BinaryHandler(ROM.FS);
            this.romdir = romdir;
        }

        public void compilePatch()
        {
            loadArenaLoOffsFile(romdir);
            uint codeAddr = handler.readFromRamAddr(ArenaLoOffs);
            PatchCompiler.compilePatch(codeAddr, romdir);
        }

        public void generatePatch()
        {
            handler.restoreFromBackup();
            handler.load();
            compilePatch();

            loadArenaLoOffsFile(romdir);

            FileInfo f = new FileInfo(romdir.FullName + "/newcode.bin");
            if (!f.Exists) return;
            FileStream fs = f.OpenRead();

            byte[] newdata = new byte[fs.Length];
            fs.Read(newdata, 0, (int)fs.Length);
            fs.Close();


            ByteArrayOutputStream extradata = new ByteArrayOutputStream();

            int codeAddr = (int) handler.readFromRamAddr(ArenaLoOffs);
            Console.Out.WriteLine("Arena Lo Offs: [" + ArenaLoOffs.ToString("X8") + "] = " + codeAddr.ToString("X8"));

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
                            val = makeBranchOpcode(ramAddr, destRamAddr, false);
                            break;
                        case "repl":
                            val = makeBranchOpcode(ramAddr, destRamAddr, true);
                            break;
                        case "hook":
                            //Jump to the hook addr
                            thisHookAddr = hookAddr;
                            val = makeBranchOpcode(ramAddr, hookAddr, true);

                            uint originalOpcode = handler.readFromRamAddr(ramAddr);
                            extradata.writeUInt(originalOpcode);
                            hookAddr += 4;
                            extradata.writeUInt(makeBranchOpcode(hookAddr, destRamAddr, false));
                            hookAddr += 4;
                            extradata.writeUInt(0x12345678);
                            hookAddr += 4;
                            break;
                        default:
                            continue;
                    }

                    Console.Out.WriteLine(String.Format("{0:X8}:{1:X8} = {2:X8}", patchCategory, ramAddr, val));
                    Console.Out.WriteLine(String.Format("              {0:X8} {1:X8}", destRamAddr, thisHookAddr));

                    handler.writeToRamAddr(ramAddr, val);
                }
            }

            s.Close();

            int newArenaOffs = codeAddr + extradata.getPos();
            handler.writeToRamAddr(ArenaLoOffs, (uint)newArenaOffs);

            handler.sections.Add(new NSMBe4.DSFileSystem.Arm9BinSection(extradata.getArray(), codeAddr, 0));
            handler.saveSections();

        }

        private void loadArenaLoOffsFile(DirectoryInfo romdir)
        {
            FileInfo f = new FileInfo(romdir.FullName + "/arenaoffs.txt");
            StreamReader s = f.OpenText();
            string l = s.ReadLine();
            ArenaLoOffs = int.Parse(l, System.Globalization.NumberStyles.HexNumber);
        }


        public static uint makeBranchOpcode(int srcAddr, int destAddr, bool withLink)
        {
            unchecked
            {
                uint res = (uint)0xEA000000;

                if (withLink)
                    res |= 0x01000000;

                int offs = (destAddr / 4) - (srcAddr / 4) - 2;
                offs &= 0x00FFFFFF;
                res |= (uint)offs;

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
