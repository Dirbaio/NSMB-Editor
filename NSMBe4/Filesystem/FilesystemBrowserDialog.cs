using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4.Filesystem
{
    public partial class FilesystemBrowserDialog : Form
    {
        NitroClass parentROM, newROM;
        ushort FileID;

        public FilesystemBrowserDialog(NitroClass ROM, ushort FileID)
        {
            InitializeComponent();
            this.parentROM = ROM;
            this.FileID = FileID;

            LanguageManager.ApplyToContainer(this, "FilesystemBrowserDialog");

            newROM = new NitroClass(ROM, FileID);

            filesystemBrowser1.Load(newROM);
        }
    }
}
