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
            uint addr = 0x02041234;

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "cmd";
            info.Arguments = "/C make CODEADDR="+addr.ToString("X8") + " && pause";
            info.CreateNoWindow = false;
            info.UseShellExecute = true;
            info.WorkingDirectory = romdir.FullName;

            Process p = Process.Start(info);
            p.WaitForExit();

            if (p.ExitCode == 0)
            {
                FileInfo f = new FileInfo(romdir.FullName + "/build/newcode.map");
                StreamReader s = f.OpenText();
                while (!s.EndOfStream)
                    Console.Out.WriteLine(s.ReadLine());
            }
        }
    }
}

