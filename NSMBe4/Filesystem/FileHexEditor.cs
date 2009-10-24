using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace NSMBe4.Filesystem
{
    public partial class FileHexEditor : Form
    {
        NitroClass ROM;
        ushort FileID;

        public FileHexEditor(NitroClass ROM, ushort FileID)
        {
            InitializeComponent();
            this.ROM = ROM;
            this.FileID = FileID;
            LanguageManager.ApplyToContainer(this, "FileHexEditor");
            this.Text = string.Format(LanguageManager.Get("FileHexEditor", "_TITLE"), ROM.FileNames[FileID]);

            hexBox1.ByteProvider = new DynamicByteProvider(ROM.ExtractFile(FileID));
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            byte[] data = ((DynamicByteProvider)hexBox1.ByteProvider).Bytes.ToArray();
            ROM.ReplaceFile(FileID, data);
        }
    }
}
