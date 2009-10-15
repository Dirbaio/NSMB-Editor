using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class GraphicsEditor : UserControl {
        public GraphicsEditor() {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "GraphicsEditor");
        }

        public Color[] Palettes;
        public int PalSize;
        public int SelectedPal;
        public int CanvasWidth;

        public Bitmap DrawBuffer;
        public byte[] GFXData;

        private int TileCount;

        public int ZoomLevel;

        public void load(Color[] pal, int palsize, byte[] data, int canvaswidth) {
            Palettes = pal;
            PalSize = palsize;
            GFXData = data;
            TileCount = data.Length / 64;
            CanvasWidth = canvaswidth;
            DrawBuffer = new Bitmap(CanvasWidth, TileCount / (CanvasWidth / 8) * 8);

            palettePicker1.SetPalette(pal, palsize);
            palettePicker1.Height = PalSize / 16 * 12 + 2;
            if (palettePicker1.PalCount == 0) {
                paletteChooserLabel.Visible = false;
                paletteChooser.Visible = false;
                SelectedPal = 0;
                RenderBuffer();
            } else {
                for (int i = 0; i < palettePicker1.PalCount; i++) {
                    paletteChooser.Items.Add(i.ToString());
                }
                paletteChooser.SelectedIndex = 0;
            }

            SetZoomLevel(1);
        }

        private void paletteChooser_SelectedIndexChanged(object sender, EventArgs e) {
            SelectedPal = paletteChooser.SelectedIndex;
            palettePicker1.SetViewPal(paletteChooser.SelectedIndex);
            RenderBuffer();
            drawingBox.Invalidate();
        }

        private void RenderBuffer() {
            int PalOffset = SelectedPal * PalSize;
            int FilePos = 0;
            int TileSrcX = 0;
            int TileSrcY = 0;
            for (int TileIdx = 0; TileIdx < TileCount; TileIdx++) {
                for (int TileY = 0; TileY < 8; TileY++) {
                    for (int TileX = 0; TileX < 8; TileX++) {
                        DrawBuffer.SetPixel(TileSrcX + TileX, TileSrcY + TileY, Palettes[PalOffset + GFXData[FilePos]]);
                        FilePos++;
                    }
                }
                TileSrcX += 8;
                if (TileSrcX >= DrawBuffer.Width) {
                    TileSrcX = 0;
                    TileSrcY += 8;
                }
            }
        }

        private void drawingBox_Paint(object sender, PaintEventArgs e) {
            if (DrawBuffer != null) {
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                e.Graphics.DrawImage(DrawBuffer, 0, 0, drawingBox.Width, drawingBox.Height);
            }
        }

        private void SetZoomLevel(int zoom) {
            ZoomLevel = zoom;
            drawingBox.Size = new Size(DrawBuffer.Width * zoom, DrawBuffer.Height * zoom);

            zoomIn.Enabled = true;
            zoomActualSize.Enabled = true;
            zoomOut.Enabled = true;

            if (zoom == 1) {
                zoomActualSize.Enabled = false;
                zoomOut.Enabled = false;
            }

            if (zoom == 8) {
                zoomIn.Enabled = false;
            }

            drawingBox.Invalidate();
        }

        private void zoomIn_Click(object sender, EventArgs e) {
            SetZoomLevel(ZoomLevel + 1);
        }

        private void zoomActualSize_Click(object sender, EventArgs e) {
            SetZoomLevel(1);
        }

        private void zoomOut_Click(object sender, EventArgs e) {
            SetZoomLevel(ZoomLevel - 1);
        }
    }
}
