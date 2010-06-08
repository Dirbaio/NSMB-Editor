using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Security;
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public partial class ArmPatcher : Form
    {
        const int ArenaLoOffs = 0x02065F10; //FIXME: SUPPORT MORE REGIONS OR AUTODETECT

        System.IO.FileInfo romf;
        System.IO.DirectoryInfo romdir;
        public ArmPatcher(System.IO.FileInfo romFile)
        {
            InitializeComponent();
            this.romf = romFile;
            this.romdir = romf.Directory;
            this.Show();
        }

        private void ArmPatcher_Load(object sender, EventArgs e)
        {
            undoPatches();
            Console.Out.WriteLine("Patching");

            int codeAddr = (int)ROM.FS.readFromRamAddr(ArenaLoOffs, -1);

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "/C make CODEADDR=0x"+codeAddr.ToString("X8") + " && pause";
            info.CreateNoWindow = false;
            info.UseShellExecute = false;
            info.WorkingDirectory = romdir.FullName;

            Process p = Process.Start(info);
            p.WaitForExit();

            if (p.ExitCode == 0)
            {
                FileInfo f = new FileInfo(romdir.FullName + "/newcode.bin");
                FileStream fs = f.OpenRead();

                byte[] newdata = new byte[fs.Length];
                fs.Read(newdata, 0, (int)fs.Length);

                int hookAddr = codeAddr + (int)fs.Length;
                fs.Close();


                
                f = new FileInfo(romdir.FullName + "/newcode.sym");
                StreamReader s = f.OpenText();

                List<Replacement> reps = new List<Replacement>();
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

                    if(ind != -1)
                    {
                        Replacement r = new Replacement();
                        r.newRamAddr = parseUHex(l.Substring(0, 8));
                        r.oldRamAddr = parseUHex(l.Substring(ind + 5, 8));

                        string cmd = l.Substring(ind, 4);
                        if (cmd == "nsub") r.type = PatchType.funcReplacement;
                        if (cmd == "hook") r.type = PatchType.hook;
                        if (cmd == "repl") r.type = PatchType.replacementHook;

                        if (l.Contains("_ov_"))
                            r.ovId = parseHex(l.Substring(l.IndexOf("_ov_") + 4, 2));

                        reps.Add(r);
                    }
                }
                s.Close();



                ByteArrayOutputStream hooks = new ByteArrayOutputStream();
                hooks.write(newdata);
                foreach (Replacement r in reps)
                {
                    if (r.type == PatchType.hook)
                    {
                        r.hookRamAddr = (uint)hookAddr;
                        uint opcode = ROM.FS.readFromRamAddr((int)r.oldRamAddr, r.ovId);
                        hooks.writeUInt(opcode);
                        hookAddr += 4;
                        hooks.writeUInt(makeBranchOpcode((uint)hookAddr, r.newRamAddr, false));
                        hookAddr += 4;
                    }
                    else
                        r.hookRamAddr = r.newRamAddr;
                }

                TextWriter tw = new StreamWriter(romdir.FullName + "/romchanges.bak");
                tw.WriteLine(ROM.FS.arm9binFile.sections.Count.ToString("X8"));
                tw.WriteLine(codeAddr.ToString("X8"));
                foreach (Replacement r in reps)
                {
                    Console.Out.WriteLine(String.Format("{0:X8} {1:X8} {2:X8} {3:X2}", r.oldRamAddr, r.hookRamAddr, r.newRamAddr, r.ovId));
                    hooks.align(4);
                    uint opcode = ROM.FS.readFromRamAddr((int)r.oldRamAddr, r.ovId);
                    ROM.FS.writeToRamAddr((int)r.oldRamAddr, makeBranchOpcode(r.oldRamAddr, r.hookRamAddr, r.type != PatchType.funcReplacement), r.ovId);
                    tw.WriteLine(r.oldRamAddr.ToString("X8") + " " + opcode.ToString("X8"));
                }
                tw.Close();

                hooks.align(4);
                ROM.FS.writeToRamAddr(ArenaLoOffs, (uint)(codeAddr + hooks.getPos()), -1);
                ROM.FS.arm9binFile.sections.Add(new Arm9BinSection(hooks.getArray(), codeAddr, 0));
                ROM.FS.arm9binFile.saveSections();
            }

            Console.Out.WriteLine("Finished Patching!");
            Console.Out.WriteLine();
            Console.Out.WriteLine();
            Console.Out.WriteLine();
        }

        public static uint parseUHex(string s)
        {
            return uint.Parse(s, System.Globalization.NumberStyles.HexNumber);
        }

        public static int parseHex(string s)
        {
            return int.Parse(s, System.Globalization.NumberStyles.HexNumber);
        }

        public void undoPatches()
        {
            Console.Out.WriteLine("UNDOING PATCHES...");
            FileInfo f = new FileInfo(romdir.FullName + "/romchanges.bak");
            if (!f.Exists)
                return;

            StreamReader s = f.OpenText();

            string l = s.ReadLine();
            int sectionCount = int.Parse(l, System.Globalization.NumberStyles.HexNumber);
            l = s.ReadLine();
            uint arenaLo = uint.Parse(l, System.Globalization.NumberStyles.HexNumber);
            ROM.FS.writeToRamAddr(ArenaLoOffs, arenaLo, -1);
            int currSectionCount = ROM.FS.arm9binFile.sections.Count;
            if (currSectionCount > sectionCount)
                ROM.FS.arm9binFile.sections.RemoveRange(sectionCount, currSectionCount - sectionCount);

            while (!s.EndOfStream)
            {
                l = s.ReadLine();
                string[] ll = l.Split(new char[] { ' ' });
                int addr = int.Parse(ll[0], System.Globalization.NumberStyles.HexNumber);
                uint val = uint.Parse(ll[1], System.Globalization.NumberStyles.HexNumber);
                int ovId = int.Parse(ll[1]);

                ROM.FS.writeToRamAddr(addr, val, ovId);
            }

            s.Close();
            Console.Out.WriteLine("DONE UNDOING PATCHES...");
            Console.Out.WriteLine();
        }

        enum PatchType
        {
            funcReplacement,
            hook,
            replacementHook
        }

        class Replacement
        {
            public PatchType type;
            public uint newRamAddr;
            public uint oldRamAddr;
            public uint hookRamAddr;
            public int ovId = -1;
        }

        public uint makeBranchOpcode(uint srcAddr, uint destAddr, bool withLink)
        {
            uint res = 0xEA000000;
            if (withLink)
                res |= 0x01000000;

            uint offs = (destAddr / 4) - (srcAddr / 4) - 2;
            offs &= 0x00FFFFFF;
            res |= offs;

            return res;
        }
    }
}

