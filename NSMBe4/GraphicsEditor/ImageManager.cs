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
    public partial class ImageManager : UserControl
    {
        public ImageManager()
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "ImageManager");
        }

        public bool standalone = false;

        public void addImage(PalettedImage i)
        {
            if (standalone)
                i.beginEdit();

            imageListBox.Items.Add(i);
            if (imageListBox.Items.Count == 1)
                imageListBox.SelectedItem = i;
        }

        public Image2D getSelectedImage()
        {
            return imageListBox.SelectedItem as Image2D;
        }

        public Palette[] getPalettes()
        {
            List<Palette> res = new List<Palette>();

            foreach (object o in paletteListBox.Items)
                if (o is Palette)
                    res.Add(o as Palette);

            return res.ToArray();
        }

        public void addPalette(Palette p)
        {
            if (standalone)
                p.beginEdit();

            paletteListBox.Items.Add(p);
            if (paletteListBox.Items.Count == 1)
                paletteListBox.SelectedItem = p;
        }

        private void updateImage()
        {
            setImageAndPalette(imageListBox.SelectedItem as PalettedImage, paletteListBox.SelectedItem as Palette);
        }

        private void setImageAndPalette(PalettedImage i, Palette p)
        {
            if (!(i is PalettedImage)) return;
            if (i == null || p == null) return;

//            Console.WriteLine(i + " " + p);

            graphicsEditor1.setPalette(p);
            graphicsEditor1.setImage(i as PalettedImage);

            tileWidthNumber.Enabled = i is Image2D;
            tileOffsetNumber.Enabled = i is Image2D;
            fourBppCheckBox.Enabled = i is Image2D;

            if (i is Image2D)
            {
                tileWidthNumber.Value = (i as Image2D).width / 8;
                tileOffsetNumber.Value = (i as Image2D).tileOffset;
                fourBppCheckBox.Checked = (i as Image2D).is4bpp;
            }
        }

        private void imageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                if(imageListBox.SelectedItem is PalettedImage)
                	(imageListBox.SelectedItem as PalettedImage).endEdit();
                imageListBox.Items.Remove(imageListBox.SelectedItem);
            }
            else
            {
                updateImage();
                if (autopaletteCheckBox.Checked)
                {
                    int i = imageListBox.SelectedIndex;
                    if (i < 0) return;
                    if (i >= paletteListBox.Items.Count) return;
                    paletteListBox.SelectedIndex = i;
                }
            }            

        }

        private void paletteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
            	if(paletteListBox.SelectedItem is Palette)
	                (paletteListBox.SelectedItem as Palette).endEdit();
                paletteListBox.Items.Remove(paletteListBox.SelectedItem);
            }
            else
                updateImage();
        }

        public void close()
        {
            foreach (object o in imageListBox.Items)
                (o as PalettedImage).endEdit();
            foreach (object o in paletteListBox.Items)
                (o as Palette).endEdit();
        }

        private void imageListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                if (imageListBox.SelectedItem != null)
                    imageListBox.Items.Remove(imageListBox.SelectedItem);
        }

        private void paletteListBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                if (paletteListBox.SelectedItem != null)
                    paletteListBox.Items.Remove(paletteListBox.SelectedItem);
        }

        private void tileWidthNumber_ValueChanged(object sender, EventArgs e)
        {
            PalettedImage i = imageListBox.SelectedItem as PalettedImage;
            if (i is Image2D)
            {
                (i as Image2D).width = (int)(tileWidthNumber.Value * 8);
                (i as Image2D).tileOffset = (int)(tileOffsetNumber.Value);
                (i as Image2D).is4bpp = fourBppCheckBox.Checked;
                graphicsEditor1.setImage(i as Image2D);
                updateImage();
            }
        }

        private GraphicsSet createGraphicsSet()
        {
            GraphicsSet gs = new GraphicsSet();
            bool err = false;
            foreach (object o in imageListBox.Items)
            {
                if (o is PixelPalettedImage)
                    gs.imgs.Add(o as PixelPalettedImage);
                else
                    err = true;
            }

            foreach (object o in paletteListBox.Items)
                if (o is Palette)
                    gs.pals.Add(o as Palette);

            gs.win = this;

            if (err)
                if (MessageBox.Show("Some textures will not be imported/exported because they aren't compatible. Do you want to continue?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return null;

            return gs;

        }
        private void exportAllBtn_Click(object sender, EventArgs e)
        {
            GraphicsSet gs = createGraphicsSet();
            if (gs == null)
                return;

            try
            {
                gs.export();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void importAllBtn_Click(object sender, EventArgs e)
        {
            GraphicsSet gs = createGraphicsSet();
            if (gs == null)
                return;

            gs.import();
            try
            {
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void exportThisBtn_Click(object sender, EventArgs e)
        {
            if (!(imageListBox.SelectedItem is PalettedImage)) { MessageBox.Show(this, "Error: No image selected!"); return; }
            if (!(paletteListBox.SelectedItem is Palette)) { MessageBox.Show(this, "Error: No palette selected!"); return; }

            SaveFileDialog ofd = new SaveFileDialog();
            ofd.Filter = "PNG Files|*.png";
            if (ofd.ShowDialog(this) == DialogResult.Cancel) return;

            Bitmap b = (imageListBox.SelectedItem as PalettedImage).render(paletteListBox.SelectedItem as Palette);
            b.Save(ofd.FileName);
            b.Dispose();
        }

        private void importThisBtn_Click(object sender, EventArgs e)
        {
            if (!(imageListBox.SelectedItem is PalettedImage)) { MessageBox.Show(this, "Error: No image selected!"); return; }
            if (!(paletteListBox.SelectedItem is Palette)) { MessageBox.Show(this, "Error: No palette selected!"); return; }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Files|*.png";
            if (ofd.ShowDialog(this) == DialogResult.Cancel) return;

            Bitmap b = new Bitmap(ofd.FileName);
            (imageListBox.SelectedItem as PalettedImage).replaceWithPal(b, paletteListBox.SelectedItem as Palette);
            b.Dispose();
        }

        private void importThisWithPalBtn_Click(object sender, EventArgs e)
        {
            if (!(imageListBox.SelectedItem is PalettedImage)) { MessageBox.Show(this, "Error: No image selected!"); return; }
            if (!(paletteListBox.SelectedItem is Palette)) { MessageBox.Show(this, "Error: No palette selected!"); return; }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PNG Files|*.png";
            if (ofd.ShowDialog(this) == DialogResult.Cancel) return;

            Bitmap b = new Bitmap(ofd.FileName);
            (imageListBox.SelectedItem as PalettedImage).replaceImgAndPal(b, paletteListBox.SelectedItem as Palette);
            b.Dispose();
        }

        public delegate void SomethingSavedD();
        public event SomethingSavedD SomethingSaved;

        private void saveAllBtn_Click(object sender, EventArgs e)
        {
            saveAll();

            if (SomethingSaved != null)
                SomethingSaved();

        }

        public void saveAll()
        {
            foreach (object o in imageListBox.Items)
            {
                if (o is PalettedImage)
                    (o as PalettedImage).save();
            }
            foreach (object o in paletteListBox.Items)
            {
                if (o is Palette)
                    (o as Palette).save();
            }
        }

        private void graphicsEditor1_SomethingSaved()
        {
            if (SomethingSaved != null)
                SomethingSaved();
        }


    }
}
