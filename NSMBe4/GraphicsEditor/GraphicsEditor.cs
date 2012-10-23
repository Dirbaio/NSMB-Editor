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
            LanguageManager.ApplyToContainer(this, "GraphicsEditor", toolTip1);
            ZoomLevel = 1;
        }

        public Bitmap DrawBuffer;
        private Bitmap ZoomCache;

        private PalettedImage img;
        private PixelPalettedImage imgEdit;
        private Palette pal;

        private int ViewWidth;
        private int ViewHeight;

        public int ZoomLevel;
        public bool GridEnabled;

        private int HoverX = -1;
        private int HoverY = -1;

        private int LastBrushX = -1;
        private int LastBrushY = -1;

        private int LastEraserX = -1;
        private int LastEraserY = -1;

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

        private ColourPicker _cp = null;

        public enum ToolType {
            Brush,
            Eraser,
            Picker,
            Fill,
            Line,
            Rectangle
        }

        public ToolType Tool;

        public void setImage(PalettedImage img)
        {
            this.imgEdit = img as PixelPalettedImage;
            this.img = img;

            DrawBuffer = new Bitmap(img.getWidth(), img.getHeight());
            ZoomCache = new Bitmap(img.getWidth() * 8, img.getHeight() * 8);
            RenderBuffer();
            SetZoomLevel(ZoomLevel);
            RenderZoomCache();
            drawingBox.Invalidate();
        }

        public void setPalette(Palette pal)
        {
            this.pal = pal;
            palettePicker1.SetPalette(pal);
            RenderBuffer();
            RenderZoomCache();
            drawingBox.Invalidate();
        }

        public void load()
        {
            SelectTool(ToolType.Brush);

            hoverStatus.Text = "";
            imageStatus.Text = string.Format(LanguageManager.Get("GraphicsEditor", "imageStatus"),  8, img.getWidth(), img.getHeight());

        }

        public delegate void SomethingSavedD();
        public event SomethingSavedD SomethingSaved;
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (img == null || pal == null) return;
            img.save();
            pal.save();

            if (SomethingSaved != null)
                SomethingSaved();
        }


        private void palettePicker1_EditColour(int ind)
        {
            Color change = pal.pal[ind];

            if (_cp == null || _cp.IsDisposed) {
                _cp = new ColourPicker();
            }

            _cp.R = change.R >> 3;
            _cp.G = change.G >> 3;
            _cp.B = change.B >> 3;

            if (_cp.ShowDialog() == DialogResult.OK) {
                pal.pal[ind] = Color.FromArgb(_cp.R << 3, _cp.G << 3, _cp.B << 3);
                palettePicker1.SetPalette(pal);

                RenderBuffer();
                RenderZoomCache();
                drawingBox.Invalidate();
            }
        }

        private byte GetPixel(int x, int y) {
            return (byte)imgEdit.getPixel(x, y);
        }

        private void SetPixel(int x, int y, byte value)
        {
            if (x < 0 || x >= img.getWidth()) return;
            if (y < 0 || y >= img.getHeight()) return;
            imgEdit.setPixel(x, y, value);
            value = (byte)imgEdit.getPixel(x, y);

            Color PalValue = pal.getColorSafe(value);

            DrawBuffer.SetPixel(x, y, PalValue);

            int px = x * ZoomLevel;
            int py = y * ZoomLevel;
            for (int zy = 0; zy < ZoomLevel; zy++) {
                for (int zx = 0; zx < ZoomLevel; zx++) {
                    ZoomCache.SetPixel(px + zx, py, PalValue);
                }
                py += 1;
            }

            if(NewUndo != null)
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

                byte val = GetPixel(x, y);

                if (val == source)
                {
                    SetPixel(x, y, dest);

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

        private void RenderBuffer()
        {
            if (img == null || pal == null) return;

            DrawBuffer = img.render(pal);
            /*
            for (int x = 0; x < img.getWidth(); x++)
                for (int y = 0; y < img.getHeight(); y++)
                    DrawBuffer.SetPixel(x, y, pal.getColorSafe(imgEdit.getPixel(x, y)));
             */
        }

        private unsafe void RenderZoomCache()
        {
            if (img == null || pal == null) return;
            if (ZoomLevel == 0) return;

            System.Drawing.Imaging.BitmapData source = DrawBuffer.LockBits(new Rectangle(0, 0, img.getWidth(), img.getHeight()), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            System.Drawing.Imaging.BitmapData dest = ZoomCache.LockBits(new Rectangle(0, 0, img.getWidth() * ZoomLevel, img.getHeight() * ZoomLevel), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            byte* SourceLine = (byte*)source.Scan0;
            byte* DestLine = (byte*)dest.Scan0;

            int h = img.getHeight();
            int w = img.getWidth();
            for (int y = 0; y < h; y++) {
                for (int zy = 0; zy < ZoomLevel; zy++) {
                    byte* SourcePtr = SourceLine;
                    byte* DestPtr = DestLine;

                    for (int x = 0; x < w; x++) {
                        byte a = *SourcePtr++;
                        byte r = *SourcePtr++;
                        byte g = *SourcePtr++;
                        byte b = *SourcePtr++;
                        for (int dx = 0; dx < ZoomLevel; dx++) {
                            *DestPtr++ = a;
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

        private void drawingBox_Paint(object sender, PaintEventArgs e)
        {
            if (img == null || pal == null) return;
            if (DrawBuffer != null)
            {
                // Render the transparency grid
                for (int x = 0; x < drawingBox.Width; x += 8)
                    for (int y = 0; y < drawingBox.Height; y += 8)
                        if (x % 16 == y % 16)
                            e.Graphics.FillRectangle(Brushes.Gray, x, y, 8, 8);

                e.Graphics.DrawImage(ZoomCache, 0, 0);

                if (DrawingLine) {
                    e.Graphics.DrawLine(DrawLinePen,
                        DrawLineX1 * ZoomLevel + (ZoomLevel / 2), DrawLineY1 * ZoomLevel + (ZoomLevel / 2),
                        DrawLineX2 * ZoomLevel + (ZoomLevel / 2), DrawLineY2 * ZoomLevel + (ZoomLevel / 2));
                }

                if (DrawingRect) {
                    int x1 = DrawRectX1 * ZoomLevel + (ZoomLevel / 2);
                    int x2 = DrawRectX2 * ZoomLevel + (ZoomLevel / 2);
                    int y1 = DrawRectY1 * ZoomLevel + (ZoomLevel / 2);
                    int y2 = DrawRectY2 * ZoomLevel + (ZoomLevel / 2);
                    e.Graphics.DrawLine(DrawRectPen, x1, y1, x2, y1); // top
                    e.Graphics.DrawLine(DrawRectPen, x1, y1, x1, y2); // left
                    e.Graphics.DrawLine(DrawRectPen, x2, y1, x2, y2); // right
                    e.Graphics.DrawLine(DrawRectPen, x1, y2, x2, y2); // bottom
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

        private void SetZoomLevel(int zoom)
        {
            if (img == null || pal == null) return;
            ZoomLevel = zoom;
            drawingBox.Size = new Size(img.getWidth() * zoom, img.getHeight() * zoom);
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
            if (img == null || pal == null) return;
            HandleDraw(true, e);
        }

        private void drawingBox_MouseMove(object sender, MouseEventArgs e) {
            if (img == null || pal == null) return;
            HandleDraw(false, e);
        }

        private void drawingBox_MouseUp(object sender, MouseEventArgs e) {
            if (img == null || pal == null) return;
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
            Point pos = new Point(e.X / ZoomLevel, e.Y / ZoomLevel);
            if (pos.X != HoverX || pos.Y != HoverY)
            {
                hoverStatus.Text = string.Format(HoverStatusString, pos.X, pos.Y);
            }
            HoverX = pos.X; HoverY = pos.Y;

            if (imgEdit == null || pal == null) return;

            if (Control.ModifierKeys == Keys.Control) {
                if (newclick) {
                    // auto pick
                    DoPickTool(e, pos);
                }
            } else
            {

                if (newclick)
                    StartUndo();

                switch (Tool) {
                    case ToolType.Brush:
                        DoBrushTool(e, pos, newclick);
                        break;
                    case ToolType.Eraser:
                        DoEraserTool(e, pos, newclick);
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

        private void DoBrushTool(MouseEventArgs e, Point clicked, bool newclick) {
            byte colour = 0;
            if (e.Button == MouseButtons.Left) {
                colour = (byte)palettePicker1.SelectedFG;
            } else if (e.Button == MouseButtons.Right) {
                colour = (byte)palettePicker1.SelectedBG;
            } else {
                return;
            }

            byte old = GetPixel(clicked.X, clicked.Y);

            if (newclick || LastBrushX == -1 || LastBrushY == -1) {
                SetPixel(clicked.X, clicked.Y, colour);
                drawingBox.Invalidate();
            } else {
                if (clicked.X != LastBrushX || clicked.Y != LastBrushY) {
                    DrawLine(LastBrushX, LastBrushY, clicked.X, clicked.Y, colour);
                    drawingBox.Invalidate();
                }
            }

            LastBrushX = clicked.X;
            LastBrushY = clicked.Y;
        }

        private void DoEraserTool(MouseEventArgs e, Point clicked, bool newclick)
        {
            if (e.Button == MouseButtons.Left) {
                byte old = GetPixel(clicked.X, clicked.Y);

                if (newclick || LastEraserX == -1 || LastEraserY == -1) {
                    SetPixel(clicked.X, clicked.Y, 0);
                    drawingBox.Invalidate();
                } else {
                    if (clicked.X != LastEraserX || clicked.Y != LastEraserY) {
                        DrawLine(LastEraserX, LastEraserY, clicked.X, clicked.Y, 0);
                        drawingBox.Invalidate();
                    }
                }

                LastEraserX = clicked.X;
                LastEraserY = clicked.Y;
            }
        }

        private void DoPickTool(MouseEventArgs e, Point clicked)
        {
            byte picked = GetPixel(clicked.X, clicked.Y);

            if (e.Button == MouseButtons.Left) {
                palettePicker1.SelectedFG = picked;
                palettePicker1.Invalidate(true);
            }

            if (e.Button == MouseButtons.Right) {
                palettePicker1.SelectedBG = picked;
                palettePicker1.Invalidate(true);
            }
        }

        private void DoFillTool(MouseEventArgs e, Point clicked) 
        {
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
                DrawLinePen = new Pen(pal.pal[DrawLineColour], ZoomLevel);
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
                DrawRectPen = new Pen(pal.pal[DrawRectColour], ZoomLevel);
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
                NewUndo.Before = (byte[])img.getRawData().Clone();
//                Console.WriteLine("Created undo state");
            }
        }

        private void CommitUndo() {
            if (NewUndo != null && NewUndo.ContainsChanges) {
                NewUndo.After = (byte[])img.getRawData().Clone();
                UndoBuffer.Push(NewUndo);
                NewUndo = null;
//                Console.WriteLine("Committed undo state");
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
            img.setRawData(redo ? buffer.After : buffer.Before);
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
