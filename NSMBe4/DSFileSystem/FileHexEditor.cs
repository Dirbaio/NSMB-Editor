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
using Be.Windows.Forms;
using System.Globalization;

namespace NSMBe4.DSFileSystem
{
    public partial class FileHexEditor : Form
    {
        File f;

        public FileHexEditor(File f)
        {
            InitializeComponent();
#if MDI
            this.MdiParent = MdiParentForm.instance;
#endif
            this.f = f;
            f.beginEdit(this);

            LanguageManager.ApplyToContainer(this, "FileHexEditor");
            this.Text = string.Format(LanguageManager.Get("FileHexEditor", "_TITLE"), f.name);

            hexBox1.ByteProvider = new DynamicByteProvider(f.getContents());
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            byte[] data = ((DynamicByteProvider)hexBox1.ByteProvider).Bytes.ToArray();
            f.replace(data, this);
        }

        private void FileHexEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            f.endEdit(this);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            uint offset;
            if (uint.TryParse(toolStripTextBox1.Text, NumberStyles.HexNumber, new CultureInfo("en-US"), out offset))
                hexBox1.Select(offset, 1);
        }
    }
}
