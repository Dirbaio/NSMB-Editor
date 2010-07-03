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
            this.MdiParent = MdiParentForm.instance;
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
