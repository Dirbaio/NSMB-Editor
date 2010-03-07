using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NSMBe4.DSFileSystem;

namespace NSMBe4.NSBMD
{
    public partial class TextureEditor : Form
    {
        File f;
        NSBTX tx;
        int tw, th;

        public TextureEditor(File f)
        {
            InitializeComponent();
            this.f = f;
            tx = new NSBTX(f);
            if (tx.textures.Length == 0) return;

            textureListBox.Items.AddRange(tx.textures);
            paletteListBox.Items.AddRange(tx.palettes);
            textureListBox.SelectedIndex = 0;
            paletteListBox.SelectedIndex = 0;
            refreshImage();
        }

        private void refreshImage()
        {
            pictureBox1.Image = renderImage();
        }

        private Bitmap renderImage()
        {
            Texture t = selectedTexture();
            Palette p = selectedPalette();
            if (t == null) return null;
            if (p == null) return null;
            return t.render(p, calcPalOffset());
        }

        private Texture selectedTexture()
        {
            return (Texture)textureListBox.SelectedItem;
        }
        private Palette selectedPalette()
        {
            return (Palette)paletteListBox.SelectedItem;
        }

        private void textureListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Palette matchingPal = null;
            foreach (Palette p in tx.palettes)
                if (p.name == selectedTexture().name)
                    matchingPal = p;
            if(matchingPal == null)
                foreach (Palette p in tx.palettes)
                    if (p.name.Contains(selectedTexture().name))
                        matchingPal = p;

            if (matchingPal != null)
                paletteListBox.SelectedItem = matchingPal;

            refreshImage();
            if(selectedTexture().usesPalette())
                paletteSize.Value = selectedTexture().palSize;
        }

        private void paletteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            refreshImage();
        }

        private void TextureEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            tx.close();
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string filename = saveFileDialog1.FileName;
            renderImage().Save(filename);
        }

        int calcPalOffset()
        {
            return (int)(paletteNum.Value * paletteSize.Value);
        }

        void calcTotalSize()
        {
            tw = 0;
            th = 0;
            foreach (Texture t in tx.textures)
            {
                tw += t.width;
                if (th < t.height)
                    th = t.height;
            }
        }
        private void importButton_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;
            string filename = openFileDialog1.FileName;

            Bitmap b = new Bitmap(filename);
            if (b.Size != new Size(selectedTexture().width, selectedTexture().height))
            {
                MessageBox.Show(LanguageManager.Get("TextureEditor", "WrongSize"));
                return;
            }

            if (selectedTexture().needsPal)
            {
                ImageIndexer ii = new ImageIndexer(b, (int)paletteSize.Value);
                Array.Copy(ii.palette, 0, selectedPalette().pal, calcPalOffset(), (int)paletteSize.Value);
                selectedPalette().save();
            }
            selectedTexture().replace(b, 0, 0, selectedPalette(), calcPalOffset(), (int)paletteSize.Value);
            b.Dispose();
        }

        private void exportAll_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            string filename = saveFileDialog1.FileName;

            calcTotalSize();
            Bitmap b = new Bitmap(tw, th * (1+(int)paletteNum.Value));
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Transparent);
            int x = 0;
            foreach (Texture t in tx.textures)
            {
                for (int i = 0; i <= paletteExpCount.Value; i++)
                {
                    g.DrawImage(t.render(selectedPalette(), (int)((i + paletteNum.Value) * paletteSize.Value)), x, th * i);
                }
                x += t.width;
            }

            b.Save(filename);
            b.Dispose();
        }

        private void importAll_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
                return;

            calcTotalSize();
            string filename = openFileDialog1.FileName;
            Bitmap b = new Bitmap(filename);
            if(b.Size != new Size(tw, th*(int)paletteExpCount.Value))
            {
                MessageBox.Show(LanguageManager.Get("TextureEditor", "WrongSize"));
                return;
            }
            new ImagePreviewer(b).Show();

            Bitmap b3 = new Bitmap(tw, th);
            Graphics gr = Graphics.FromImage(b3);
            gr.Clear(Color.Transparent);
            gr.DrawImage(b, 0, 0);
            ImageIndexer ii = new ImageIndexer(b3, selectedPalette().colorCount);
            Array.Copy(ii.palette, 0, selectedPalette().pal, calcPalOffset(), (int)paletteSize.Value);
            b3.Dispose();

            int xx = 0;
            foreach (Texture t in tx.textures)
            {
                t.replace(b, xx, 0, selectedPalette(), calcPalOffset(), (int)paletteSize.Value);
                for (int i = 1; i < paletteExpCount.Value; i++)
                    for(int x = 0; x < t.width; x++)
                        for (int y = 0; y < t.height; y++)
                        {
                            byte ind = t.getPixel(x, y);
                            Color c = b.GetPixel(xx + x, th * i + y);
                            selectedPalette().pal[ind + calcPalOffset() + (int)(paletteSize.Value * i)] = c;
                        }

                xx += t.width;
            }

            selectedPalette().save();

//            b.Dispose();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            refreshImage();
        }
    }
}

