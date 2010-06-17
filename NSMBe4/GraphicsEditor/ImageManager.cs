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
            updateImage();
        }

        private void paletteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateImage();
        }

        public void updateImage()
        {
            PalettedImage i = imageListBox.SelectedItem as PalettedImage;
            Palette p = paletteListBox.SelectedItem as Palette;

            if (i == null) return;
            if (p == null) return;

            pictureBox1.Image = i.render(p);
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

    }
}
