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

        private void paletteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
                paletteListBox.Items.Remove(paletteListBox.SelectedItem);
            updateImage();
            Palette p = paletteListBox.SelectedItem as Palette;
            if (p == null) return;
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
                if (MessageBox.Show("Question", "Some textures will not be imported/exported because they aren't compatible. Do you want to continue?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
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

            try
            {
                gs.import();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
