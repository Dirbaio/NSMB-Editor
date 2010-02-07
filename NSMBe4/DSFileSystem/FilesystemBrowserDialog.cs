using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4.DSFileSystem
{
    public partial class FilesystemBrowserDialog : Form
    {
        Filesystem fs;

        public FilesystemBrowserDialog(Filesystem fs)
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "FilesystemBrowserDialog");

            this.fs = fs;
            filesystemBrowser1.Load(fs);
        }

        private void FilesystemBrowserDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            fs.close();
        }
    }
}
