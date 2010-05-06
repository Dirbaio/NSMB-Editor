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

namespace NSMBe4
{
    public partial class ArmPatcher : Form
    {
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

            int arm7binRamAddr = (int)ROM.FS.headerFile.getUintAt(0x38);
            arm7binRamAddr += ROM.FS.arm7binFile.fileSize;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "/C make CODEADDR=0x"+arm7binRamAddr.ToString("X8") + " && pause";
            info.CreateNoWindow = false;
            info.UseShellExecute = false;
            info.WorkingDirectory = romdir.FullName;

            Process p = Process.Start(info);
            p.WaitForExit();

            if (p.ExitCode == 0)
            {
                FileInfo f = new FileInfo(romdir.FullName + "/newcode.bin");
                FileStream fs = f.OpenRead();
                int hookAddr = arm7binRamAddr + (int)fs.Length;
                byte[] data = ROM.FS.arm7binFile.getContents();
                byte[] newdata = new byte[data.Length + fs.Length];
                Array.Copy(data, newdata, data.Length);
                fs.Read(newdata, data.Length, (int)fs.Length);

                fs.Close();

                
                f = new FileInfo(romdir.FullName + "/newcode.sym");
                StreamReader s = f.OpenText();

                List<Replacement> reps = new List<Replacement>();
                while (!s.EndOfStream)
                {
                    string l = s.ReadLine();
                    if (l.Contains("newsub_"))
                    {
                        Replacement r = new Replacement();
                        string ofname = l.Substring(0, 8);
                        r.newRamAddr = uint.Parse(ofname, System.Globalization.NumberStyles.HexNumber);
                        string fname = l.Substring(l.IndexOf("newsub_") + 7, 8);
                        r.oldRamAddr = uint.Parse(fname, System.Globalization.NumberStyles.HexNumber);
                        r.type = PatchType.funcReplacement;
                        reps.Add(r);
                    }
                    if (l.Contains("hook_"))
                    {
                        Replacement r = new Replacement();
                        string ofname = l.Substring(0, 8);
                        r.newRamAddr = uint.Parse(ofname, System.Globalization.NumberStyles.HexNumber);
                        string fname = l.Substring(l.IndexOf("hook_") + 5, 8);
                        r.oldRamAddr = uint.Parse(fname, System.Globalization.NumberStyles.HexNumber);
                        r.type = PatchType.hook;
                        reps.Add(r);
                    }
                    if (l.Contains("replace_"))
                    {
                        Replacement r = new Replacement();
                        string ofname = l.Substring(0, 8);
                        r.newRamAddr = uint.Parse(ofname, System.Globalization.NumberStyles.HexNumber);
                        string fname = l.Substring(l.IndexOf("replace_") + 8, 8);
                        r.oldRamAddr = uint.Parse(fname, System.Globalization.NumberStyles.HexNumber);
                        r.type = PatchType.replacementHook;
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
                        uint opcode = ROM.FS.readFromRamAddr((int)r.oldRamAddr);
                        hooks.writeUInt(opcode);
                        hookAddr += 4;
                        hooks.writeUInt(makeBranchOpcode((uint)hookAddr, r.newRamAddr, false));
                        hookAddr += 4;
                    }
                    else
                        r.hookRamAddr = r.newRamAddr;
                }

                TextWriter tw = new StreamWriter(romdir.FullName + "/romchanges.bak");
                tw.WriteLine(ROM.FS.arm7binFile.fileSize.ToString("X8"));
                foreach (Replacement r in reps)
                {
                    uint opcode = ROM.FS.readFromRamAddr((int)r.oldRamAddr);
                    ROM.FS.writeToRamAddr((int)r.oldRamAddr, makeBranchOpcode(r.oldRamAddr, r.hookRamAddr, r.type != PatchType.funcReplacement));
                    tw.WriteLine(r.oldRamAddr.ToString("X8") + " " + opcode.ToString("X8"));
                }
                tw.Close();

                ROM.FS.arm7binFile.beginEdit(this);
                ROM.FS.arm7binFile.replace(hooks.getArray(), this);
                ROM.FS.arm7binFile.endEdit(this);
            }
        }

        public void undoPatches()
        {
            FileInfo f = new FileInfo(romdir.FullName + "/romchanges.bak");
            if (!f.Exists)
                return;

            StreamReader s = f.OpenText();

            string l = s.ReadLine();
            uint arm7offs = uint.Parse(l, System.Globalization.NumberStyles.HexNumber);
            ROM.FS.arm7binFile.beginEdit(this);
            byte[] data = ROM.FS.arm7binFile.getContents();
            byte[] newdata = new byte[arm7offs];
            Array.Copy(data, newdata, arm7offs);
            ROM.FS.arm7binFile.replace(newdata, this);
            ROM.FS.arm7binFile.endEdit(this);

            while (!s.EndOfStream)
            {
                l = s.ReadLine();
                string[] ll = l.Split(new char[] { ' ' });
                int addr = int.Parse(ll[0], System.Globalization.NumberStyles.HexNumber);
                uint val = uint.Parse(ll[1], System.Globalization.NumberStyles.HexNumber);

                ROM.FS.writeToRamAddr(addr, val);
            }

            s.Close();
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

