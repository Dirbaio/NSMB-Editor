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

        private int ImageWidth;
        private int ImageHeight;
        private int ViewWidth;
        private int ViewHeight;

        private int TileCount;

        public int ZoomLevel;
        public bool GridEnabled;

        private int HoverX = -1;
        private int HoverY = -1;

        private bool DrawingLine = false;
        private int DrawLineX1;
        private int DrawLineX2;
        private int DrawLineY1;
        private int DrawLineY2;
        private byte DrawLineColour;
        private Pen DrawLinePen;

        private bool DrawingRect = false;
        private int DrawRectX1;
        private int DrawRectX2;
        private int DrawRectY1;
        private int DrawRectY2;
        private byte DrawRectColour;
        private Pen DrawRectPen;

        // saves a dictionary lookup every hover
        private string HoverStatusString = LanguageManager.Get("GraphicsEditor", "hoverStatus");

        public enum ToolType {
            Brush,
            Eraser,
            Picker,
            Fill,
            Line,
            Rectangle
        }

        public ToolType Tool;

        public delegate void SaveGraphicsHandler();
        public event SaveGraphicsHandler SaveGraphics;

        public void load(Color[] pal, bool is4bpp, byte[] data, int canvaswidth) {
            Palettes = pal;
            PalSize = is4bpp ? 16 : 256;
            GFXData = data;
            Is4bpp = is4bpp;
            TileCount = data.Length / 64 * (Is4bpp ? 2 : 1);
            CanvasWidth = canvaswidth;
            SelectTool(ToolType.Brush);
            DrawBuffer = new Bitmap(CanvasWidth, TileCount / (CanvasWidth / 8) * 8);
            ImageWidth = DrawBuffer.Width; ImageHeight = DrawBuffer.Height;
            ZoomCache = new Bitmap(ImageWidth * 8, ImageHeight * 8);

            hoverStatus.Text = "";
            imageStatus.Text = string.Format(LanguageManager.Get("GraphicsEditor", "imageStatus"), Is4bpp ? 4 : 8, ImageWidth, ImageHeight);

            palettePicker1.SetPalette(pal, PalSize);
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

        private void saveButton_Click(object sender, EventArgs e) {
            SaveGraphics();
        }

        private void paletteChooser_SelectedIndexChanged(object sender, EventArgs e) {
            SelectedPal = paletteChooser.SelectedIndex;
            palettePicker1.SetViewPal(paletteChooser.SelectedIndex);
            RenderBuffer();
            RenderZoomCache();
            drawingBox.Invalidate();
        }

        private int GetOffsetFromPos(int x, int y) {
            // return -1 for invalids
            if (x < 0) return -1;
            if (y < 0) return -1;
            if (x >= ImageWidth) return -1;
            if (y >= ImageHeight) return -1;
            // figure out the tile number
            int TileX = x / 8;
            int TileY = y / 8;
            int TileIdx = (TileY * (CanvasWidth / 8)) + TileX;
            // figure out the individual offset
            return ((TileIdx * 64) + ((y % 8) * 8) + (x % 8)) >> (Is4bpp ? 1 : 0);
        }

        private byte GetPixel(int x, int y) {
            int offset = GetOffsetFromPos(x, y);
            if (offset == -1) return 0;

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

        private void SetPixel(int x, int y, byte value) {
            int offset = GetOffsetFromPos(x, y);
            if (offset == -1) return;

            if (Is4bpp) {
                if ((x % 2) == 0) {
                    GFXData[offset] = (byte)((GFXData[offset] & 240) | value);
                } else {
                    GFXData[offset] = (byte)((GFXData[offset] & 15) | (value << 4));
                }
            } else {
                GFXData[offset] = value;
            }

            Color PalValue = Palettes[SelectedPal * PalSize + value];
            DrawBuffer.SetPixel(x, y, PalValue);

            int px = x * ZoomLevel;
            int py = y * ZoomLevel;
            for (int zy = 0; zy < ZoomLevel; zy++) {
                for (int zx = 0; zx < ZoomLevel; zx++) {
                    ZoomCache.SetPixel(px + zx, py, PalValue);
                }
                py += 1;
            }

            NewUndo.ContainsChanges = true;
        }

        private void DrawLine(int x0, int y0, int x1, int y1, byte colour) {
            // http://en.wikipedia.org/wiki/Bresenham's_line_algorithm
            bool steep = (Math.Abs(y1 - y0) > Math.Abs(x1 - x0));
            int swap;

            if (steep) {
                swap = y0;
                y0 = x0;
                x0 = swap;
                swap = y1;
                y1 = x1;
                x1 = swap;
            }
            if (x0 > x1) {
                swap = x1;
                x1 = x0;
                x0 = swap;
                swap = y1;
                y1 = y0;
                y0 = swap;
            }

            int deltax = x1 - x0;
            int deltay = Math.Abs(y1 - y0);
            int error = deltax / 2;
            int ystep;
            int y = y0;
            if (y0 < y1)
                ystep = 1;
            else
                ystep = -1;

            x1 += 1; // take into account "for x from x0 to x1" includes x1
            for (int x = x0; x < x1; x++) {
                if (steep)
                    SetPixel(y, x, colour);
                else
                    SetPixel(x, y, colour);

                error -= deltay;
                if (error < 0) {
                    y += ystep;
                    error += deltax;
                }
            }
        }

        private void DrawRect(int x0, int y0, int x1, int y1, byte colour) {
            DrawLine(x0, y0, x1, y0, colour); // top
            DrawLine(x0, y0, x0, y1, colour); // left
            DrawLine(x1, y0, x1, y1, colour); // right
            DrawLine(x0, y1, x1, y1, colour); // bottom
        }

        private void FillArea(int sourcex, int sourcey, byte source, byte dest) {
            Stack<Point> stack = new Stack<Point>();
            stack.Push(new Point(sourcex, sourcey));
            int w = DrawBuffer.Width;
            int h = DrawBuffer.Height;

            while (stack.Count > 0) {
                Point p = stack.Pop();
                int x = p.X;
                int y = p.Y;
                if (x < 0 || x >= w || y < 0 || y >= h) continue;

                int offset = GetOffsetFromPos(x, y);
                byte val = 0;
                if (Is4bpp) {
                    if ((x % 2) == 0) {
                        val = (byte)(GFXData[offset] & 15);
                    } else {
                        val = (byte)((GFXData[offset] & 240) >> 4);
                    }
                } else {
                    val = GFXData[offset];
                }

                if (val == source) {
                    if (Is4bpp) {
                        if ((x % 2) == 0) {
                            GFXData[offset] = (byte)((GFXData[offset] & 240) | dest);
                        } else {
                            GFXData[offset] = (byte)((GFXData[offset] & 15) | (dest << 4));
                        }
                    } else {
                        GFXData[offset] = dest;
                    }

                    stack.Push(new Point(x + 1, y));
                    stack.Push(new Point(x - 1, y));
                    stack.Push(new Point(x, y + 1));
                    stack.Push(new Point(x, y - 1));
                }
            }

            RenderBuffer();
            RenderZoomCache();
            drawingBox.Invalidate();

            NewUndo.ContainsChanges = true;
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
                if (TileSrcX >= ImageWidth) {
                    TileSrcX = 0;
                    TileSrcY += 8;
                }
            }
        }

        private unsafe void RenderZoomCache() {
            if (ZoomLevel == 0) return;

            System.Drawing.Imaging.BitmapData source = DrawBuffer.LockBits(new Rectangle(0, 0, ImageWidth, ImageHeight), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            System.Drawing.Imaging.BitmapData dest = ZoomCache.LockBits(new Rectangle(0, 0, ImageWidth * ZoomLevel, ImageHeight * ZoomLevel), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            byte* SourceLine = (byte*)source.Scan0;
            byte* DestLine = (byte*)dest.Scan0;

            for (int y = 0; y < ImageHeight; y++) {
                for (int zy = 0; zy < ZoomLevel; zy++) {
                    byte* SourcePtr = SourceLine;
                    byte* DestPtr = DestLine;

                    for (int x = 0; x < ImageWidth; x++) {
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

                if (DrawingLine) {
                    e.Graphics.DrawLine(DrawLinePen,
                        DrawLineX1 * ZoomLevel + (ZoomLevel / 2), DrawLineY1 * ZoomLevel + (ZoomLevel / 2),
                        DrawLineX2 * ZoomLevel + (ZoomLevel / 2), DrawLineY2 * ZoomLevel + (ZoomLevel / 2));
                }

                if (DrawingRect) {
                    e.Graphics.DrawRectangle(DrawRectPen,
                        DrawRectX1 * ZoomLevel + (ZoomLevel / 2), DrawRectY1 * ZoomLevel + (ZoomLevel / 2),
                        (DrawRectX2 - DrawRectX1) * ZoomLevel, (DrawRectY2 - DrawRectY1) * ZoomLevel);
                }

                if (GridEnabled) {
                    int Offset = 8 * ZoomLevel;
                    for (int y = 0; y < ViewHeight; y += Offset) {
                        for (int x = 0; x < ViewWidth; x += Offset) {
                            e.Graphics.DrawRectangle(Pens.LightGray, x, y, Offset, Offset);
                        }
                    }
                }
            }
        }

        private void SetZoomLevel(int zoom) {
            ZoomLevel = zoom;
            drawingBox.Size = new Size(ImageWidth * zoom, ImageHeight * zoom);
            ViewWidth = drawingBox.Width; ViewHeight = drawingBox.Height;
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
            rectangleTool.FlatStyle = (Tool == ToolType.Rectangle) ? FlatStyle.Flat : FlatStyle.Standard;
        }

        private void toolButton_Click(object sender, EventArgs e) {
            if (sender == brushTool) SelectTool(ToolType.Brush);
            if (sender == eraserTool) SelectTool(ToolType.Eraser);
            if (sender == pickerTool) SelectTool(ToolType.Picker);
            if (sender == fillTool) SelectTool(ToolType.Fill);
            if (sender == lineTool) SelectTool(ToolType.Line);
            if (sender == rectangleTool) SelectTool(ToolType.Rectangle);
        }

        private void drawingBox_MouseDown(object sender, MouseEventArgs e) {
            HandleDraw(true, e);
        }

        private void drawingBox_MouseMove(object sender, MouseEventArgs e) {
            HandleDraw(false, e);
        }

        private void drawingBox_MouseUp(object sender, MouseEventArgs e) {
            Point pos = new Point(e.X / ZoomLevel, e.Y / ZoomLevel);

            if (Tool == ToolType.Line) {
                if (DrawingLine) {
                    DrawingLine = false;
                    DrawLine(DrawLineX1, DrawLineY1, pos.X, pos.Y, DrawLineColour);
                    drawingBox.Invalidate();
                }
            }

            if (Tool == ToolType.Rectangle) {
                if (DrawingRect) {
                    DrawingRect = false;
                    DrawRect(DrawRectX1, DrawRectY1, pos.X, pos.Y, DrawRectColour);
                    drawingBox.Invalidate();
                }
            }

            CommitUndo();
        }

        private void drawingBox_MouseLeave(object sender, EventArgs e) {
            HoverX = -1;
            HoverY = -1;
            hoverStatus.Text = "";
        }

        private void HandleDraw(bool newclick, MouseEventArgs e) {
            /*int FilterX = e.X, FilterY = e.Y;

            if (FilterX < 0) FilterX = 0;
            if (FilterX >= drawingBox.Width) FilterX = drawingBox.Width - 1;
            if (FilterY < 0) FilterY = 0;
            if (FilterY >= drawingBox.Height) FilterY = drawingBox.Height - 1;

            Point pos = new Point(FilterX / ZoomLevel, FilterY / ZoomLevel);*/
            Point pos = new Point(e.X / ZoomLevel, e.Y / ZoomLevel);
            if (pos.X != HoverX || pos.Y != HoverY) {
                hoverStatus.Text = string.Format(HoverStatusString, pos.X, pos.Y);
            }
            HoverX = pos.X; HoverY = pos.Y;

            if (Control.ModifierKeys == Keys.Control) {
                if (newclick) {
                    // auto pick
                    DoPickTool(e, pos);
                }
            } else {
                if (newclick)
                    StartUndo();

                switch (Tool) {
                    case ToolType.Brush:
                        DoBrushTool(e, pos);
                        break;
                    case ToolType.Eraser:
                        DoEraserTool(e, pos);
                        break;
                    case ToolType.Picker:
                        if (newclick) DoPickTool(e, pos);
                        break;
                    case ToolType.Fill:
                        if (newclick) DoFillTool(e, pos);
                        break;
                    case ToolType.Line:
                        DoLineTool(e, pos, newclick);
                        break;
                    case ToolType.Rectangle:
                        DoRectTool(e, pos, newclick);
                        break;
                }
            }
        }

        private void DoBrushTool(MouseEventArgs e, Point clicked) {
            byte old = GetPixel(clicked.X, clicked.Y);

            if (e.Button == MouseButtons.Left) {
                if (old != palettePicker1.SelectedFG) {
                    SetPixel(clicked.X, clicked.Y, (byte)palettePicker1.SelectedFG);
                    drawingBox.Invalidate();
                }
            }

            if (e.Button == MouseButtons.Right) {
                if (old != palettePicker1.SelectedBG) {
                    SetPixel(clicked.X, clicked.Y, (byte)palettePicker1.SelectedBG);
                    drawingBox.Invalidate();
                }
            }
        }

        private void DoEraserTool(MouseEventArgs e, Point clicked) {
            if (e.Button == MouseButtons.Left) {
                if (GetPixel(clicked.X, clicked.Y) != 0) {
                    SetPixel(clicked.X, clicked.Y, 0);
                    drawingBox.Invalidate();
                }
            }
        }

        private void DoPickTool(MouseEventArgs e, Point clicked) {
            //Console.WriteLine("Picking " + GetOffsetFromPos(clicked.X, clicked.Y).ToString());
            //Console.WriteLine("From " + clicked.X.ToString() + " " + clicked.Y.ToString());
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

        private void DoFillTool(MouseEventArgs e, Point clicked) {
            byte source = GetPixel(clicked.X, clicked.Y);
            byte dest = 0;

            if (e.Button == MouseButtons.Left) {
                dest = (byte)palettePicker1.SelectedFG;
            }

            if (e.Button == MouseButtons.Right) {
                dest = (byte)palettePicker1.SelectedBG;
            }

            if (source != dest) {
                FillArea(clicked.X, clicked.Y, source, dest);
            }
        }

        private void DoLineTool(MouseEventArgs e, Point clicked, bool newclick) {
            if (newclick) {
                DrawingLine = true;
                DrawLineX1 = clicked.X;
                DrawLineX2 = clicked.X;
                DrawLineY1 = clicked.Y;
                DrawLineY2 = clicked.Y;
                DrawLineColour = (e.Button == MouseButtons.Left) ? (byte)palettePicker1.SelectedFG : (byte)palettePicker1.SelectedBG;
                DrawLinePen = new Pen(Palettes[SelectedPal * PalSize + DrawLineColour], ZoomLevel);
                DrawLinePen.StartCap = System.Drawing.Drawing2D.LineCap.Square;
                DrawLinePen.EndCap = System.Drawing.Drawing2D.LineCap.Square;
                drawingBox.Invalidate();
            } else {
                if (DrawingLine) {
                    if (clicked.X != DrawLineX2 || clicked.Y != DrawLineY2) {
                        DrawLineX2 = clicked.X;
                        DrawLineY2 = clicked.Y;
                        drawingBox.Invalidate();
                    }
                }
            }
        }

        private void DoRectTool(MouseEventArgs e, Point clicked, bool newclick) {
            if (newclick) {
                DrawingRect = true;
                DrawRectX1 = clicked.X;
                DrawRectX2 = clicked.X;
                DrawRectY1 = clicked.Y;
                DrawRectY2 = clicked.Y;
                DrawRectColour = (e.Button == MouseButtons.Left) ? (byte)palettePicker1.SelectedFG : (byte)palettePicker1.SelectedBG;
                DrawRectPen = new Pen(Palettes[SelectedPal * PalSize + DrawRectColour], ZoomLevel);
                drawingBox.Invalidate();
            } else {
                if (DrawingRect) {
                    if (clicked.X != DrawRectX2 || clicked.Y != DrawRectY2) {
                        DrawRectX2 = clicked.X;
                        DrawRectY2 = clicked.Y;
                        drawingBox.Invalidate();
                    }
                }
            }
        }

        private class UndoState {
            public byte[] Before;
            public byte[] After;
            public bool ContainsChanges;
        }

        private Stack<UndoState> UndoBuffer = new Stack<UndoState>();
        private Stack<UndoState> RedoBuffer = new Stack<UndoState>();
        private UndoState NewUndo = null;

        private void StartUndo() {
            RedoBuffer.Clear();
            redoButton.Enabled = (RedoBuffer.Count > 0);
            if (NewUndo == null) {
                NewUndo = new UndoState();
                NewUndo.Before = (byte[])GFXData.Clone();
            }
        }

        private void CommitUndo() {
            if (NewUndo != null && NewUndo.ContainsChanges) {
                NewUndo.After = (byte[])GFXData.Clone();
                UndoBuffer.Push(NewUndo);
                NewUndo = null;
                // enforce a limit.. how?
                //if (UndoBuffer.Count > 36) ...
                undoButton.Enabled = (UndoBuffer.Count > 0);
            }
        }

        private void Undo() {
            UndoState ThisUndo = UndoBuffer.Pop();
            ApplyUndo(ThisUndo, false);
            RedoBuffer.Push(ThisUndo);
            undoButton.Enabled = (UndoBuffer.Count > 0);
            redoButton.Enabled = (RedoBuffer.Count > 0);
        }

        private void Redo() {
            UndoState ThisUndo = RedoBuffer.Pop();
            ApplyUndo(ThisUndo, true);
            UndoBuffer.Push(ThisUndo);
            undoButton.Enabled = (UndoBuffer.Count > 0);
            redoButton.Enabled = (RedoBuffer.Count > 0);
        }

        private void ApplyUndo(UndoState buffer, bool redo) {
            GFXData = redo ? buffer.After : buffer.Before;
            RenderBuffer();
            RenderZoomCache();
            drawingBox.Invalidate();
        }

        private void undoButton_Click(object sender, EventArgs e) {
            Undo();
        }

        private void redoButton_Click(object sender, EventArgs e) {
            Redo();
        }
    }
}
