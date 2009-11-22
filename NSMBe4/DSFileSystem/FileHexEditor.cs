using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace NSMBe4.DSFileSystem
{
    public partial class FileHexEditor : Form
    {
        File f;

        public FileHexEditor(File f)
        {
            InitializeComponent();
            this.f = f;
            f.beginEdit();

            LanguageManager.ApplyToContainer(this, "FileHexEditor");
            this.Text = string.Format(LanguageManager.Get("FileHexEditor", "_TITLE"), f.name);

            hexBox1.ByteProvider = new DynamicByteProvider(f.getContents());
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            byte[] data = ((DynamicByteProvider)hexBox1.ByteProvider).Bytes.ToArray();
            f.replace(data);
        }

        private void FileHexEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            f.endEdit();
        }
    }
}
