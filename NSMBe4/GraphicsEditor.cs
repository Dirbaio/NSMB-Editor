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
        private Bitmap ZoomCache;
        public byte[] GFXData;
        public bool Is4bpp;

        private int TileCount;

        public int ZoomLevel;
        public bool GridEnabled;

        public enum ToolType {
            Brush,
            Eraser,
            Picker,
            Fill,
            Line
        }

        public ToolType Tool;

        public void load(Color[] pal, int palsize, byte[] data, int canvaswidth) {
            Palettes = pal;
            PalSize = palsize;
            GFXData = data;
            Is4bpp = (palsize == 16);
            TileCount = data.Length / 64 * (Is4bpp ? 2 : 1);
            CanvasWidth = canvaswidth;
            SelectTool(ToolType.Brush);
            DrawBuffer = new Bitmap(CanvasWidth, TileCount / (CanvasWidth / 8) * 8);
            ZoomCache = new Bitmap(DrawBuffer.Width * 8, DrawBuffer.Height * 8);

            palettePicker1.SetPalette(pal, palsize);
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
            RenderZoomCache();
            drawingBox.Invalidate();
        }

        private int GetOffsetFromPos(int x, int y) {
            // figure out the tile number
            int TileX = x / 8;
            int TileY = y / 8;
            int TileIdx = (TileY * (CanvasWidth / 8)) + TileX;
            // figure out the individual offset
            return ((TileIdx * 64) + ((y % 8) * 8) + (x % 8)) >> (Is4bpp ? 1 : 0);
        }

        private byte GetPixel(int x, int y) {
            int offset = GetOffsetFromPos(x, y);

            if (Is4bpp) {
                if ((x % 2) == 0) {
                    return (byte)(GFXData[offset] & 15);
                } else {
                    return (byte)((GFXData[offset] & 240) >> 4);
                }
            } else {
                return GFXData[offset];
            }
        }

        private void RenderBuffer() {
            int PalOffset = SelectedPal * PalSize;
            int FilePos = 0;
            int TileSrcX = 0;
            int TileSrcY = 0;
            bool LowNibble = false;

            for (int TileIdx = 0; TileIdx < TileCount; TileIdx++) {
                for (int TileY = 0; TileY < 8; TileY++) {
                    for (int TileX = 0; TileX < 8; TileX++) {
                        if (!Is4bpp) {
                            DrawBuffer.SetPixel(TileSrcX + TileX, TileSrcY + TileY, Palettes[PalOffset + GFXData[FilePos]]);
                            FilePos++;
                        } else {
                            LowNibble = !LowNibble;
                            if (LowNibble) {
                                DrawBuffer.SetPixel(TileSrcX + TileX, TileSrcY + TileY, Palettes[PalOffset + (GFXData[FilePos] & 15)]);
                            } else {
                                DrawBuffer.SetPixel(TileSrcX + TileX, TileSrcY + TileY, Palettes[PalOffset + ((GFXData[FilePos] & 240) >> 4)]);
                                FilePos++;
                            }
                        }
                    }
                }

                TileSrcX += 8;
                if (TileSrcX >= DrawBuffer.Width) {
                    TileSrcX = 0;
                    TileSrcY += 8;
                }
            }
        }

        private unsafe void RenderZoomCache() {
            if (ZoomLevel == 0) return;

            System.Drawing.Imaging.BitmapData source = DrawBuffer.LockBits(new Rectangle(0, 0, DrawBuffer.Width, DrawBuffer.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData dest = ZoomCache.LockBits(new Rectangle(0, 0, DrawBuffer.Width * ZoomLevel, DrawBuffer.Height * ZoomLevel), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            byte* SourceLine = (byte*)source.Scan0;
            byte* DestLine = (byte*)dest.Scan0;

            for (int y = 0; y < DrawBuffer.Height; y++) {
                for (int zy = 0; zy < ZoomLevel; zy++) {
                    byte* SourcePtr = SourceLine;
                    byte* DestPtr = DestLine;

                    for (int x = 0; x < DrawBuffer.Width; x++) {
                        byte r = *SourcePtr++;
                        byte g = *SourcePtr++;
                        byte b = *SourcePtr++;
                        for (int dx = 0; dx < ZoomLevel; dx++) {
                            *DestPtr++ = r;
                            *DestPtr++ = g;
                            *DestPtr++ = b;
                        }
                    }

                    DestLine += dest.Stride;
                }

                SourceLine += source.Stride;
            }

            DrawBuffer.UnlockBits(source);
            ZoomCache.UnlockBits(dest);
        }

        private void drawingBox_Paint(object sender, PaintEventArgs e) {
            if (DrawBuffer != null) {
                e.Graphics.DrawImage(ZoomCache, 0, 0);

                if (GridEnabled) {
                    int Offset = 8 * ZoomLevel;
                    for (int y = 0; y < drawingBox.Height; y += Offset) {
                        for (int x = 0; x < drawingBox.Width; x += Offset) {
                            e.Graphics.DrawRectangle(Pens.LightGray, x, y, Offset, Offset);
                        }
                    }
                }
            }
        }

        private void SetZoomLevel(int zoom) {
            ZoomLevel = zoom;
            drawingBox.Size = new Size(DrawBuffer.Width * zoom, DrawBuffer.Height * zoom);
            RenderZoomCache();

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

            zoomLabel.Text = string.Format(LanguageManager.Get("GraphicsEditor", "zoomLabel"), ZoomLevel * 100);

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

        private void showGrid_Click(object sender, EventArgs e) {
            GridEnabled = !GridEnabled;
            showGrid.FlatStyle = GridEnabled ? FlatStyle.Flat : FlatStyle.Standard;
            drawingBox.Invalidate();
        }

        private void SelectTool(ToolType NewTool) {
            Tool = NewTool;
            brushTool.FlatStyle = (Tool == ToolType.Brush) ? FlatStyle.Flat : FlatStyle.Standard;
            eraserTool.FlatStyle = (Tool == ToolType.Eraser) ? FlatStyle.Flat : FlatStyle.Standard;
            pickerTool.FlatStyle = (Tool == ToolType.Picker) ? FlatStyle.Flat : FlatStyle.Standard;
            fillTool.FlatStyle = (Tool == ToolType.Fill) ? FlatStyle.Flat : FlatStyle.Standard;
            lineTool.FlatStyle = (Tool == ToolType.Line) ? FlatStyle.Flat : FlatStyle.Standard;
        }

        private void toolButton_Click(object sender, EventArgs e) {
            if (sender == brushTool) SelectTool(ToolType.Brush);
            if (sender == eraserTool) SelectTool(ToolType.Eraser);
            if (sender == pickerTool) SelectTool(ToolType.Picker);
            if (sender == fillTool) SelectTool(ToolType.Fill);
            if (sender == lineTool) SelectTool(ToolType.Line);
        }

        private void drawingBox_MouseDown(object sender, MouseEventArgs e) {
            HandleDraw(true, e);
        }

        private void drawingBox_MouseMove(object sender, MouseEventArgs e) {
            HandleDraw(false, e);
        }

        private void HandleDraw(bool newclick, MouseEventArgs e) {
            Point pos = new Point(e.X / ZoomLevel, e.Y / ZoomLevel);

            if (Control.ModifierKeys == Keys.Control) {
                if (newclick) {
                    // auto pick
                    DoPickTool(e, pos);
                }
            } else {
            }
        }

        private void DoPickTool(MouseEventArgs e, Point clicked) {
            Console.WriteLine("Picking " + GetOffsetFromPos(clicked.X, clicked.Y).ToString());
            Console.WriteLine("From " + clicked.X.ToString() + " " + clicked.Y.ToString());
            byte picked = GetPixel(clicked.X, clicked.Y);

            if (e.Button == MouseButtons.Left) {
                palettePicker1.SelectedFG = picked;
                palettePicker1.Invalidate();
            }

            if (e.Button == MouseButtons.Right) {
                palettePicker1.SelectedBG = picked;
                palettePicker1.Invalidate();
            }
        }
    }
}
