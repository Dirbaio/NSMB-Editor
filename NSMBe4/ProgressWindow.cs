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

namespace NSMBe4
{
    public partial class ProgressWindow : Form
    {
        public ProgressWindow(string title)
        {
            InitializeComponent();
            if (Properties.Settings.Default.mdi)
                this.MdiParent = MdiParentForm.instance;
            this.Text = title;
        }

        public void SetMax(int max)
        {
            progressBar1.Maximum = max;
        }

        public void setValue(int val)
        {
            progressBar1.Value = val;
            Application.DoEvents();
        }

        public void SetCurrentAction(string current)
        {
            currentAction.Text = current;
            Application.DoEvents();
        }

        public void WriteLine(string line)
        {
            textBox1.AppendText(line+"\r\n");
            Application.DoEvents();
        }
    }
}
