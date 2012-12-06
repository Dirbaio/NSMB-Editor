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
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public partial class PaletteViewer : Form
    {
        File f;
        File fileLz;
        Color[] pal;

        public PaletteViewer(File f)
        {
            InitializeComponent();
            this.MdiParent = MdiParentForm.instance;
            this.f = f;
	        fileLz = new LZFile(f, LZFile.CompressionType.LZ);
            this.pal = FilePalette.arrayToPalette(ROM.LZ77_Decompress(f.getContents()));
            if (pal.Length < 256)
                is4bpp.Checked = true;
            updatePalettes();
            pictureBox1.Invalidate();
            this.Icon = Properties.Resources.nsmbe;
        }

        public void updatePalettes()
        {
            paletteList.Items.Clear();
            int palSize = 256;
            if (is4bpp.Checked) palSize = 16;

            for(int i = 0; i + palSize <= pal.Length; i+=palSize)
            {
                int colorcount = palSize;
                if(pal.Length - i < palSize)
                    colorcount = pal.Length - i;

                paletteList.Items.Add(i+": "+colorcount+" "+LanguageManager.Get("PaletteViewer", "Colors"));
            }
            pictureBox1.Update();
        }

        public void renderPalette(Graphics g)
        {
            if (paletteList.SelectedItem == null) return;

            int palSize = 256;
            if (is4bpp.Checked) palSize = 16;
            int palOffs = paletteList.SelectedIndex * palSize;

            int size = 12;
            int x = 0;
            int y = 0;
            for (int i = palOffs; i < pal.Length && i < palOffs + palSize; i++)
            {
                g.FillRectangle(new SolidBrush(pal[i]), x * size, y * size, size, size);
                g.DrawRectangle(Pens.Black, x * size, y * size, size, size);
                x++;
                if (x == 16)
                {
                    x = 0;
                    y++;
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            renderPalette(e.Graphics);
        }

        private void paletteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        private void is4bpp_CheckedChanged(object sender, EventArgs e)
        {
            updatePalettes();
        }

        private void addPalette(int i)
        {
            int palSize = 256;
            if (is4bpp.Checked) palSize = 16;

            int palOffs = i * palSize;
            int palLen = palSize;
            if (palOffs + palLen > pal.Length)
                palLen = pal.Length - palOffs;
            File ifl = new InlineFile(fileLz, palOffs * 2, palLen * 2, f.name + " - "+i);
            LevelChooser.showImgMgr();
            LevelChooser.imgMgr.m.addPalette(new FilePalette(ifl));
        }

        private void addToManager_Click(object sender, EventArgs e)
        {
            if (paletteList.SelectedItem == null) return;

            addPalette(paletteList.SelectedIndex);
        }

        private void addAllToManager_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < paletteList.Items.Count; i++)
                addPalette(i);
        }
    }
}
