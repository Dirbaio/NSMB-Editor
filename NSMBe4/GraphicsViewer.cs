using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class GraphicsViewer : Form
    {
        private byte[] file, paletteFile;
        private Color[] palette;
        private int paletteSize = 256;
        private int lastImageSize = -1;
        private Bitmap TileBuffer, buffer;

        private int PreferredWidth = -1;

        public GraphicsViewer()
        {
            InitializeComponent();
            palette = new Color[256];

            int colorNum = 0;
            for (int r = 0; r < 256; r += 256 / 8)
                for (int g = 0; g < 256; g += 256 / 8)
                    for (int b = 0; b < 256; b += 256 / 4)
                        palette[colorNum++] = Color.FromArgb(r, g, b);

            updatePaletteCount();

            LanguageManager.ApplyToContainer(this, "GraphicsViewer");
        }

        private void updatePaletteCount()
        {
            int newPalCount = palette.Length / paletteSize;
            if (newPalCount <= 0)
                newPalCount = 1;
            if (paletteNum.Value > newPalCount - 1)
                paletteNum.Value = newPalCount - 1;
            paletteNum.Maximum = newPalCount - 1;
        }

        private byte[] tryDecompress(byte[] file)
        {
            try
            {
                byte[] decomp = FileSystem.LZ77_Decompress(file);
                if (decomp.Length == 0)
                    return file;
                return decomp;
            }
            catch (Exception)
            {
                return file;
            }
        }

        public void SetFile(byte[] file)
        {
            Console.Out.WriteLine("setfile");
            this.file = tryDecompress(file);
            RefreshTileBuffer();
        }

        public void SetPalette(byte[] palette)
        {
            this.paletteFile = tryDecompress(palette);
            RefreshPalette();
        }

        public void SetPreferredWidth(int value) {
            PreferredWidth = value;
        }

        public void RefreshTileBuffer()
        {
            Console.Out.WriteLine("rtb");
            if (file == null)
                return;

            if (palette.Length < paletteSize)
                return;
            Console.Out.WriteLine("rtb2");

            // Load graphics
            int TileCount = file.Length / (use4bpp.Checked?32:64);
            if (TileCount == 0)
                return;
            Console.Out.WriteLine("rtb3");

            TileBuffer = new Bitmap(TileCount * 8, 8);

            int FilePos = 0;
            for (int i = 0; i < TileCount; i++)
            {
                for (int TileY = 0; TileY < 8; TileY++)
                {
                    for (int TileX = 0; TileX < (use4bpp.Checked?4:8); TileX++)
                    {
                        if (use4bpp.Checked)
                        {
                            TileBuffer.SetPixel(i * 8 + TileX * 2+1, TileY, palette[file[FilePos] / 16 + (int)paletteNum.Value * paletteSize]);
                            TileBuffer.SetPixel(i * 8 + TileX * 2, TileY, palette[file[FilePos] % 16 + (int)paletteNum.Value * paletteSize]);
                        }
                        else
                            TileBuffer.SetPixel(i * 8 + TileX, TileY, palette[file[FilePos] + (int)paletteNum.Value * paletteSize]);
                        FilePos++;
                    }
                }
            }

            viewport.Image = TileBuffer;
            RefreshImageSizes();
            RefreshImage();
        }

        private void RefreshImage()
        {
            String selectedSize = (string) imageSizes.SelectedItem;
            selectedSize = selectedSize.Substring(0, selectedSize.IndexOf('x')).Trim();
            int imageWidth = int.Parse(selectedSize);

            buffer = CutImage(TileBuffer, imageWidth, 1);
            viewport.Image = buffer;
        }

        public static Bitmap CutImage(Image im, int width, int blockrows)
        {
            int blocksize = im.Height / blockrows;
            int blockcount = im.Width / blocksize;

            int cols = width / blocksize;
            int rows = blockcount / cols;

            Bitmap b = new Bitmap(cols * blocksize, rows * blocksize * blockrows);
            Graphics g = Graphics.FromImage(b);
            g.Clear(Color.Gray);

            Rectangle SourceRect = new Rectangle(0, 0, cols * blocksize, blocksize);
            Rectangle DestRect = new Rectangle(0, 0, cols * blocksize, blocksize);


            for (int r = 0; r < blockrows; r++)
            {
                SourceRect.Y = r * blocksize;
                for (int i = 0; i < rows; i++)
                {
                    SourceRect.X = i * cols * blocksize;
                    DestRect.Y = i * blocksize + r * rows * blocksize;

                    g.DrawImage(im, DestRect, SourceRect, GraphicsUnit.Pixel);
                }
            }

            return b;
        }

        private void RefreshImageSizes()
        {
            int imageSize = TileBuffer.Width / 8;
            if (imageSize != lastImageSize)
            {
                lastImageSize = imageSize;
                imageSizes.Items.Clear();

                //by default we select the most square file size
                // unless PreferredWidth is set
                int bestImageWidth = -1;
                int bestSizeIndex = 0;
                int bestValue = 0;

                for (int i = 1; i <= imageSize; i++)
                {
                    if (imageSize % i == 0)
                    {
                        imageSizes.Items.Add(i*8 + " x " + imageSize / i*8);
                        if (PreferredWidth == -1) {
                            if (bestValue > i + imageSize / i || bestImageWidth == -1) {
                                bestImageWidth = i;
                                bestValue = i + imageSize / i;
                                bestSizeIndex = imageSizes.Items.Count - 1;
                            }
                        } else {
                            if (i * 8 == PreferredWidth) {
                                bestSizeIndex = imageSizes.Items.Count - 1;
                            }
                        }
                    }
                }

                imageSizes.SelectedIndex = bestSizeIndex;
            }
        }

        private void RefreshPalette()
        {
            if (paletteFile == null)
            {
                RefreshTileBuffer();
                return;
            }

            Color[] oldPal = palette;

            //loads a palette into rgb15 format

//            try
//            {
                palette = new Color[paletteFile.Length / 2];

                for (int PalIdx = 0; PalIdx < palette.Length; PalIdx++)
                {
                    int ColourVal = paletteFile[PalIdx * 2] + (paletteFile[(PalIdx * 2) + 1] << 8);
                    int cR = (ColourVal & 31) * 8;
                    int cG = ((ColourVal >> 5) & 31) * 8;
                    int cB = ((ColourVal >> 10) & 31) * 8;
                    palette[PalIdx] = Color.FromArgb(cR, cG, cB);
                }

    //            for(int i = 0; i < palette.Length; i+=paletteSize)
    //                Palette[0] = Color.LightSlateGray;
//            }
//            catch(Exception)
//            {
//                palette = oldPal;
//                MessageBox.Show("Error loading palette");
//            }

            updatePaletteCount();
            RefreshTileBuffer();
        }

        private void paletteNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshTileBuffer();
        }

        private void imageSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshImage();
        }

        private void use4bpp_CheckedChanged(object sender, EventArgs e)
        {
            updatePaletteCount();
            RefreshPalette();
            paletteSize = use4bpp.Checked ? 16 : 256;
        }
    }
}
