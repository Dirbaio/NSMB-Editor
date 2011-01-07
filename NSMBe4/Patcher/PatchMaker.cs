using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NSMBe4.Patcher
{
    public class PatchMaker
    {
        private static int ArenaLoOffs;

        public static void compilePatch(DirectoryInfo romdir)
        {
            loadArenaLoOffsFile(romdir);
            uint codeAddr = ROM.FS.arm9bin.readFromRamAddr(ArenaLoOffs, -1);
            PatchCompiler.compilePatch(codeAddr, romdir);
        }

        public static void generatePatch(DirectoryInfo romdir)
        {
            loadArenaLoOffsFile(romdir);

            FileInfo f = new FileInfo(romdir.FullName + "/newcode.bin");
            if (!f.Exists) return;
            FileStream fs = f.OpenRead();

            byte[] newdata = new byte[fs.Length];
            fs.Read(newdata, 0, (int)fs.Length);
            fs.Close();


            ByteArrayOutputStream patchdata = new ByteArrayOutputStream();
            ByteArrayOutputStream patchstruct = new ByteArrayOutputStream();
            int codeAddr = (int) ROM.FS.arm9bin.readFromRamAddr(ArenaLoOffs, -1);
            Console.Out.WriteLine("Arena Lo Offs: [" + ArenaLoOffs.ToString("X8") + "] = " + codeAddr.ToString("X8"));

            patchdata.write(newdata);
            patchdata.align(4);
            int hookAddr = codeAddr + patchdata.getPos();


            f = new FileInfo(romdir.FullName + "/newcode.sym");
            StreamReader s = f.OpenText();

            int setupAddr = -1;
            int mainPatchAddr = -1;
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

                if (l.Contains("NSMBEPatcher_Setup"))
                    setupAddr = parseHex(l.Substring(0, 8));
                if (l.Contains("NSMBEPatcher_ApplyMainPatch"))
                    mainPatchAddr = parseHex(l.Substring(0, 8));

                if (ind != -1)
                {
                    int destRamAddr= parseHex(l.Substring(0, 8));    //Redirect dest addr
                    int ramAddr = parseHex(l.Substring(ind + 5, 8)); //Patched addr
                    int val = 0;

                    int ovId = -1;
                    if (l.Contains("_ov_"))
                        ovId = parseHex(l.Substring(l.IndexOf("_ov_") + 4, 2));

                    int patchCategory = ovId + 1;
                    if (patchCategory == 0)
                        patchCategory = ROM.FS.arm9bin.getPatchCategoryAtAddr(ramAddr);

                    string cmd = l.Substring(ind, 4);
                    int thisHookAddr = 0;
                    if (cmd == "nsub")
                        val = makeBranchOpcode(ramAddr, destRamAddr, false);
                    else if (cmd == "repl")
                        val = makeBranchOpcode(ramAddr, destRamAddr, true);
                    else if (cmd == "hook")
                    {
                        //Jump to the hook addr
                        thisHookAddr = hookAddr;
                        val = makeBranchOpcode(ramAddr, hookAddr, true);

                        uint originalOpcode = ROM.FS.arm9bin.readFromRamAddr(ramAddr, ovId);
                        patchdata.writeUInt(originalOpcode);
                        hookAddr += 4;
                        patchdata.writeInt(makeBranchOpcode(hookAddr, destRamAddr, false));
                        hookAddr += 4;
                    }

                    Console.Out.WriteLine(String.Format("{0:X8}:{1:X8} = {2:X8}", patchCategory, ramAddr, val));
                    Console.Out.WriteLine(String.Format("              {0:X8} {1:X8}", destRamAddr, thisHookAddr));
                    patchstruct.writeInt(patchCategory);
                    patchstruct.writeInt(ramAddr);
                    patchstruct.writeInt(val);

                }
            }

            s.Close();

            patchstruct.writeUInt(0);
            patchstruct.writeInt(ArenaLoOffs);
            patchstruct.writeInt(codeAddr + patchdata.getPos() + patchstruct.getPos() + 0x20);
            patchstruct.writeUInt(0xFFFFFFFF);

            int patchStructOffs = codeAddr + patchdata.getPos();
            patchdata.write(patchstruct.getArray());

            //Now, remove the old patch (if exists)

            byte[] arm9 = ROM.FS.arm9binFile.getContents();

            uint offs = ROM.FS.arm9binFile.getUintAt(0x0FA0); //newDataFileOffs
            if (offs != 0)
            {
                byte[] newarm9 = new byte[offs];
                Array.Copy(arm9, 0, newarm9, 0, offs);
                arm9 = newarm9;
            }

            offs = (uint)arm9.Length;

            byte[] patchDataArray = patchdata.getArray();

            byte[] newarm9b = new byte[arm9.Length + patchDataArray.Length];
            Array.Copy(arm9, 0, newarm9b, 0, arm9.Length);
            Array.Copy(patchDataArray, 0, newarm9b, arm9.Length, patchDataArray.Length);
            arm9 = newarm9b;


            String a = "PatchMaker.cs:1";
            ROM.FS.arm9binFile.beginEdit(a);
            ROM.FS.arm9binFile.replace(arm9, a);
            ROM.FS.arm9binFile.endEdit(a);

            ROM.FS.arm9binFile.setUintAt(0x800, 0xE1A00000);
            int preSetupAddr = (int)( setupAddr-codeAddr + offs+0x02000000);
            ROM.FS.arm9binFile.setUintAt(0x804, (uint)makeBranchOpcode(0x02000804, preSetupAddr, true));
            ROM.FS.arm9binFile.setUintAt(preSetupAddr - 0x0200000C, offs + 0x02000000);
            ROM.FS.arm9binFile.setUintAt(preSetupAddr - 0x02000008, offs + 0x02000000 + (uint)patchDataArray.Length);
            ROM.FS.arm9binFile.setUintAt(preSetupAddr - 0x02000004, (uint)codeAddr);


            int prePatchAddr = (int)(mainPatchAddr - codeAddr + offs + 0x02000000);
            ROM.FS.arm9binFile.setUintAt(prePatchAddr - 0x02000004, (uint)patchStructOffs);
            ROM.FS.arm9binFile.setUintAt(0x8E4, (uint)makeBranchOpcode(0x020008E4, mainPatchAddr, true));

            ROM.FS.arm9binFile.setUintAt(0x0FA0, offs);




        }

        private static void loadArenaLoOffsFile(DirectoryInfo romdir)
        {
            FileInfo f = new FileInfo(romdir.FullName + "/arenaoffs.txt");
            StreamReader s = f.OpenText();
            string l = s.ReadLine();
            ArenaLoOffs = int.Parse(l, System.Globalization.NumberStyles.HexNumber);
        }


        public static int makeBranchOpcode(int srcAddr, int destAddr, bool withLink)
        {
            unchecked
            {
                int res = (int)0xEA000000;

                if (withLink)
                    res |= 0x01000000;

                int offs = (destAddr / 4) - (srcAddr / 4) - 2;
                offs &= 0x00FFFFFF;
                res |= offs;

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
