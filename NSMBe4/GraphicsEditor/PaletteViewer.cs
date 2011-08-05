using System;
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
        Color[] pal;

        public PaletteViewer(File f)
        {
            InitializeComponent();
            this.MdiParent = MdiParentForm.instance;
            this.f = f;
            this.pal = FilePalette.arrayToPalette(ROM.LZ77_Decompress(f.getContents()));
            if (pal.Length < 256)
                is4bpp.Checked = true;
            updatePalettes();
            pictureBox1.Invalidate();
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
            File ifl = new InlineFile(f, palOffs * 2, palLen * 2, f.name + " - "+i, null, InlineFile.CompressionType.LZComp);
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
