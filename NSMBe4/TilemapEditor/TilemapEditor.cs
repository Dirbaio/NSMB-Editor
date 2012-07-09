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

namespace NSMBe4.TilemapEditor
{
    public partial class TilemapEditor : UserControl
    {
        Tilemap t;

        public TilemapEditor()
        {
            InitializeComponent();
        }

        public void load(Tilemap t)
        {
            this.t = t;
            panel1.Width = t.width * 8 + 30;
            tilePicker1.init(t.buffers, 8);
            tilemapEditorControl1.picker = tilePicker1;
            tilemapEditorControl1.load(t);
        }

        public void reload()
        {
            tilePicker1.init(t.buffers, 8);
        }

        private void uncheckButtons()
        {
            drawToolButton.Checked = false;
            xFlipToolButton.Checked = false;
            yFlipToolButton.Checked = false;
            copyToolButton.Checked = false;
            pasteToolButton.Checked = false;
        }

        private void drawToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.DRAW;
            uncheckButtons();
            drawToolButton.Checked = true;
        }

        private void xFlipToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.XFLIP;
            uncheckButtons();
            xFlipToolButton.Checked = true;
        }

        private void yFlipToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.YFLIP;
            uncheckButtons();
            yFlipToolButton.Checked = true;
        }

        private void copyToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.COPY;
            uncheckButtons();
            copyToolButton.Checked = true;
        }

        private void pasteToolButton_Click(object sender, EventArgs e)
        {
            tilemapEditorControl1.mode = TilemapEditorControl.EditionMode.PASTE;
            uncheckButtons();
            pasteToolButton.Checked = true;
        }

        public void showSaveButton()
        {
            saveButton.Visible = true;
            toolStripSeparator1.Visible = true;
        }
        private void saveButton_Click(object sender, EventArgs e)
        {
            t.save();
        }
    }
}
