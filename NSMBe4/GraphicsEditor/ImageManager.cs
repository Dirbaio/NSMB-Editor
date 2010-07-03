using System;
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
        }

        public void addImage(PalettedImage i)
        {
            imageListBox.Items.Add(i);
        }

        public void addPalette(Palette p)
        {
            paletteListBox.Items.Add(p);
        }

        private void imageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
                imageListBox.Items.Remove(imageListBox.SelectedItem);

            updateImage();
            PalettedImage i = imageListBox.SelectedItem as PalettedImage;
            if(i is PixelPalettedImage)
                graphicsEditor1.setImage(i as PixelPalettedImage);

            if (i is Image2D)
            {
                tileWidthNumber.Enabled = true;
                tileWidthNumber.Value = (i as Image2D).width / 8;
            }
            else
                tileWidthNumber.Enabled = false;
        }

        private void paletteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
                paletteListBox.Items.Remove(paletteListBox.SelectedItem);
            updateImage();
            Palette p = paletteListBox.SelectedItem as Palette;
            graphicsEditor1.setPalette(p);
        }

        public void updateImage()
        {
            PalettedImage i = imageListBox.SelectedItem as PalettedImage;
            Palette p = paletteListBox.SelectedItem as Palette;

            if (i == null) return;
            if (p == null) return;

//            pictureBox1.Image = i.render(p);
        }

        public void close()
        {
            foreach (object o in imageListBox.Items)
                (o as PalettedImage).close();
            foreach (object o in paletteListBox.Items)
                (o as Palette).close();
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
                graphicsEditor1.setImage(i as Image2D);
                updateImage();
            }
        }

    }
}
