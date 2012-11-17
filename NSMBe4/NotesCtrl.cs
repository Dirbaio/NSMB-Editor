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
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class NotesCtrl : UserControl
    {
        bool hovered = false;

        public NotesCtrl()
        {
            InitializeComponent();
        }

        private void NotesCtrl_Paint(object sender, PaintEventArgs e)
        {
            if (hovered)
                e.Graphics.DrawImage(Properties.Resources.note_bright, 0, 0, 16, 16);
            else
                e.Graphics.DrawImage(Properties.Resources.note, 0, 0, 16, 16);
        }

        private void NotesCtrl_Click(object sender, EventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            pt.Offset(8, 8);
            toolTip1.Show(this.Text, this, pt, this.Text.Length * 75);
        }

        private void NotesCtrl_MouseEnter(object sender, EventArgs e)
        {
            hovered = true;
            Invalidate();
        }

        private void NotesCtrl_MouseLeave(object sender, EventArgs e)
        {
            hovered = false;
            Invalidate();
        }
    }
}
