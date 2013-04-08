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

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace NSMBe4
{
    public partial class MdiParentForm : Form
    {
        public static MdiParentForm instance = null;

        public MdiParentForm()
        {
            InitializeComponent();
            instance = this;
            /*
            foreach (Control c in this.Controls)
            {
                if (c is MdiClient)
                {
                    (c as MdiClient).BackColor = Color.Black;
                    (c as MdiClient).BackgroundImage = Properties.Resources.BulletBillCannonTop;
                    (c as MdiClient).BackgroundImageLayout = ImageLayout.Center; 
                }
            } */
            
            this.Text = "NSMB Editor 5.2 " + Properties.Resources.version.Trim();
            if (Properties.Settings.Default.MDIWindowInit)
            {
                this.Location = Properties.Settings.Default.MDIWindowPos;
                this.Size = Properties.Settings.Default.MDIWindowSize;
                if (Properties.Settings.Default.MDIWindowMax) this.WindowState = FormWindowState.Maximized;
            }
            this.Icon = Properties.Resources.nsmbe;
        }


        private void MdiParentForm_Load(object sender, EventArgs e)
        {
            LevelChooser lc = new LevelChooser();
            lc.MdiParent = this;
            lc.Show();
            
            //For some reason, without this, the MDI form is created behind other windows. WTF?
            this.Activate();
        }

        private void MdiParentForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.MDIWindowInit = true;
            if (this.WindowState == FormWindowState.Maximized) {
                Properties.Settings.Default.MDIWindowMax = true;
                Properties.Settings.Default.MDIWindowSize = this.RestoreBounds.Size;
                Properties.Settings.Default.MDIWindowPos = this.RestoreBounds.Location;
            } else {
                Properties.Settings.Default.MDIWindowMax = false;
                Properties.Settings.Default.MDIWindowSize = this.Size;
                Properties.Settings.Default.MDIWindowPos = this.Location;
            }
            Properties.Settings.Default.Save();

            Application.Exit();
        }

        private void MdiParentForm_SizeChanged(object sender, EventArgs e)
        {
        }
    }
}
