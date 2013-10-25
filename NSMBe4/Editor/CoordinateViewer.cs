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

namespace NSMBe4.Editor
{
    public partial class CoordinateViewer : UserControl
    {
        public CoordinateViewer()
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "CoordinateViewer");

            xUpDown.Maximum = 512 * 16;
            yUpDown.Maximum = 256 * 16;
            widthUpDown.Minimum = 1;
            heightUpDown.Minimum = 1;
            widthUpDown.Maximum = 512 * 16;
            heightUpDown.Maximum = 256 * 16;
        }

        public LevelEditorControl EdControl;
        public LevelItem it = null;

        bool updating = false;

        public void setLevelItem(LevelItem it)
        {
            this.it = it;
            updating = true;

            if (it == null)
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;

                xUpDown.Value = Math.Min(xUpDown.Maximum, Math.Max(xUpDown.Minimum, it.rx / it.snap));
                yUpDown.Value = Math.Min(yUpDown.Maximum, Math.Max(yUpDown.Minimum, it.ry / it.snap));

                widthUpDown.Enabled = it.isResizable;
                heightUpDown.Enabled = it.isResizable;
                widthUpDown.Value = Math.Min(widthUpDown.Maximum, Math.Max(widthUpDown.Minimum, it.rwidth / it.snap));
                heightUpDown.Value = Math.Min(heightUpDown.Maximum, Math.Max(heightUpDown.Minimum, it.rheight / it.snap));
            }

            updating = false;
        }

        private void anyUpDownValueChanged(object sender, EventArgs e)
        {
            if (updating || it == null) return;

            EdControl.UndoManager.Do(new MoveResizeLvlItemAction(UndoManager.ObjToList(it),
                (int)(xUpDown.Value * it.snap - it.rx),
                (int)(yUpDown.Value * it.snap - it.ry),
                (int)(widthUpDown.Value * it.snap - it.rwidth),
                (int)(heightUpDown.Value * it.snap - it.rheight)));
        }
    }
}
