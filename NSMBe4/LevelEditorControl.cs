using System;
using System.Drawing;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class LevelEditorControl : UserControl {

        private float zoom = 1;

        public LevelEditorControl() {
            InitializeComponent();
            Ready = false;
            hScrollBar.Visible = false;
            vScrollBar.Visible = false;
            MouseWheel += new MouseEventHandler(DrawingArea_MouseWheel);
            DrawingArea.MouseWheel += new MouseEventHandler(DrawingArea_MouseWheel);
        }

        public void SetZoom(float nZoom)
        {
            this.zoom = nZoom;
            UpdateScrollbars();
            repaint();
        }

        public LevelEditor editor;

        public void Initialise(NSMBGraphics GFX, NSMBLevel Level, LevelEditor editor) {
            this.GFX = GFX;
            this.Level = Level;
            this.editor = editor;
            Ready = true;
            hScrollBar.Visible = true;
            vScrollBar.Visible = true;
            ViewableArea = new Rectangle();
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        public void SetEditionMode(EditionMode nm)
        {
            if (nm == mode)
                return;

            mode = nm;
            if(mode != null)
                mode.Refresh();

            DrawingArea.Invalidate();
        }

        #region Scrolling
        private void UpdateScrollbars() {
            ViewableWidth = (int)Math.Ceiling((float)DrawingArea.Width / 16 / zoom);
            ViewableHeight = (int)Math.Ceiling((float)DrawingArea.Height / 16 / zoom);

            int xMax = 512 - ViewableWidth;
            int yMax = 256 - ViewableHeight;
            if (yMax < 0) yMax = 0;
            if (xMax < 0) xMax = 0;

            vScrollBar.Maximum = yMax;
            hScrollBar.Maximum = xMax;

            ViewableArea.X = hScrollBar.Value;
            ViewableArea.Y = vScrollBar.Value;
            ViewableArea.Width = ViewableWidth;
            ViewableArea.Height = ViewableHeight;
        }

        private void LevelEditorControl_Resize(object sender, EventArgs e) {
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        private void hScrollBar_ValueChanged(object sender, EventArgs e) {
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        private void vScrollBar_ValueChanged(object sender, EventArgs e) {
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        private void DrawingArea_MouseWheel(object sender, MouseEventArgs e) {
            if (e.Delta == 120) {
                if (vScrollBar.Value == 1) {
                    vScrollBar.Value = 0;
                } else if (vScrollBar.Value > 1) {
                    vScrollBar.Value -= 2;
                }
            }
            if (e.Delta == -120) {
                if (vScrollBar.Value == (vScrollBar.Maximum - 1)) {
                    vScrollBar.Value = vScrollBar.Maximum;
                } else if (vScrollBar.Value < (vScrollBar.Maximum - 1)) {
                    vScrollBar.Value += 2;
                }
            }
        }

        private int ViewableWidth;
        private int ViewableHeight;
        public Rectangle ViewableArea;
        #endregion

        public NSMBGraphics GFX;
        public NSMBLevel Level;
        private bool Ready;

        public enum ObjectType {
            Object,
            Sprite,
            Entrance,
            Path
        }

        public EditionMode mode = null;

        private int DragStartX;
        private int DragStartY;

        public delegate void UpdateViewableAreaDelegate();
        public event UpdateViewableAreaDelegate UpdateViewableArea;

        public delegate void SetDirtyFlagDelegate();
        public event SetDirtyFlagDelegate SetDirtyFlag;
        public void FireSetDirtyFlag()
        {
            SetDirtyFlag();
        }

        #region Rendering

        public void repaint()
        {
            DrawingArea.Invalidate();
        }

        private void DrawingArea_Paint(object sender, PaintEventArgs e) {
            if (!Ready) return;

            Rectangle ViewableBlocks = new Rectangle(hScrollBar.Value, vScrollBar.Value, ViewableWidth, ViewableHeight);
            Rectangle ViewablePixels = new Rectangle(hScrollBar.Value * 16, vScrollBar.Value * 16, ViewableWidth * 16, ViewableHeight * 16);
            //Viewable.X += 4; Viewable.Y += 4; Viewable.Width -= 8; Viewable.Height -= 8;

            e.Graphics.ScaleTransform(zoom, zoom);
            e.Graphics.TranslateTransform(-hScrollBar.Value * 16, -vScrollBar.Value * 16);
            
            //RENDER PANNING BLOCKS GRID
            for(int x = ViewableBlocks.X / 16; x <= (ViewableBlocks.Width+ViewableBlocks.X)/16; x++)
                for (int y = ViewableBlocks.Y / 16; y <= (ViewableBlocks.Height + ViewableBlocks.Y) / 16; y++)
                    e.Graphics.DrawRectangle(Pens.LightGray, x * 256, y * 256, 256, 256);


            //RENDER OBJECTS
#if USE_GDIPLUS
            for (int ObjIdx = 0; ObjIdx < Level.Objects.Count; ObjIdx++) {
                Rectangle ObjRect = new Rectangle(Level.Objects[ObjIdx].X, Level.Objects[ObjIdx].Y, Level.Objects[ObjIdx].Width, Level.Objects[ObjIdx].Height);
                if (ObjRect.IntersectsWith(ViewableArea)) {
                    Level.Objects[ObjIdx].Render(e.Graphics, ViewableArea.X, ViewableArea.Y, ViewableArea);
                }
            }
#else
            IntPtr pTarget = e.Graphics.GetHdc();

            foreach(NSMBObject o in Level.Objects)
            {
                Rectangle ObjRect = new Rectangle(o.X, o.Y, o.Width, o.Height);
                if (ObjRect.IntersectsWith(ViewableBlocks))
                    o.Render(pTarget, ViewableBlocks.X, ViewableBlocks.Y, ViewableBlocks, zoom);
            }

            e.Graphics.ReleaseHdc(pTarget);
#endif

            foreach(NSMBView v in Level.Views)
                v.render(e.Graphics, ViewableArea.X, ViewableArea.Y);
            
            foreach(NSMBSprite s in Level.Sprites)
                if(ViewableBlocks.Contains(s.X, s.Y))
                    s.Render(e.Graphics);

            foreach(NSMBEntrance n in Level.Entrances)
                if(ViewablePixels.Contains(n.X, n.Y))
                    n.Render(e.Graphics);

            foreach (NSMBPath p in Level.Paths)
                p.Render(e.Graphics, false);

            if (mode != null)
                mode.RenderSelection(e.Graphics);

            e.Graphics.TranslateTransform(hScrollBar.Value * 16, vScrollBar.Value * 16);
        }
        #endregion

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            /*
            if (keyData == Keys.Left || keyData == Keys.Up || keyData == Keys.Right || keyData == Keys.Down) {
                int XDelta = 0;
                int YDelta = 0;
                if (keyData == Keys.Left) XDelta -= 1;
                if (keyData == Keys.Right) XDelta += 1;
                if (keyData == Keys.Up) YDelta -= 1;
                if (keyData == Keys.Down) YDelta += 1;
                if (SelectedObject != -1) {
                    if (SelectedObjectType == ObjectType.Object) {
                        Level.Objects[SelectedObject].X += XDelta;
                        Level.Objects[SelectedObject].Y += YDelta;
                    }
                    if (SelectedObjectType == ObjectType.Sprite) {
                        Level.Sprites[SelectedObject].X += XDelta;
                        Level.Sprites[SelectedObject].Y += YDelta;
                    }
                    if (SelectedObjectType == ObjectType.Entrance) {
                        Level.Entrances[SelectedObject].X += XDelta;
                        Level.Entrances[SelectedObject].Y += YDelta;
                    }
                    DrawingArea.Invalidate();
                    UpdateSelectedObjInfo();
                    SetDirtyFlag();
                    return true;
                }
            }*/
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e) {
            if (Ready)
            {
                if (e.Button == MouseButtons.Right)
                {
                    DragStartX = e.X;
                    DragStartY = e.Y;
                    return;
                }

                if (mode != null)
                    mode.MouseDown((int)(e.X / zoom) + hScrollBar.Value * 16, (int)(e.Y/zoom) + vScrollBar.Value * 16);
            }
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left && Ready && mode != null)
                mode.MouseDrag((int)(e.X / zoom) + hScrollBar.Value * 16, (int)(e.Y / zoom) + vScrollBar.Value * 16);

            int DragSpeed = (int)Math.Ceiling(8*zoom);

            if (e.Button == MouseButtons.Right) {
                int NewX = e.X;
                int NewY = e.Y;
                int XDelta = (NewX - DragStartX) / DragSpeed;
                int YDelta = (NewY - DragStartY) / DragSpeed;
                if (XDelta != 0 || YDelta != 0) {
                    Point NewPosition = new Point(ViewableArea.X - XDelta, ViewableArea.Y - YDelta);
                    if (NewPosition.X < 0) NewPosition.X = 0;
                    if (NewPosition.X > hScrollBar.Maximum) NewPosition.X = hScrollBar.Maximum;
                    if (NewPosition.Y < 0) NewPosition.Y = 0;
                    if (NewPosition.Y > vScrollBar.Maximum) NewPosition.Y = vScrollBar.Maximum;
                    DragStartX += (XDelta * DragSpeed);
                    DragStartY += (YDelta * DragSpeed);
                    ScrollEditor(NewPosition);
                }
            }
        }

        public void EnsurePosVisible(int X, int Y) {
            Point NewPosition = new Point(ViewableArea.X, ViewableArea.Y);
            if (X < ViewableArea.X) {
                NewPosition.X = Math.Max(0, X - (ViewableWidth / 2));
            }
            if (X >= ViewableArea.Right) {
                NewPosition.X = Math.Min(hScrollBar.Maximum, X - (ViewableWidth / 2));
            }

            if (Y < ViewableArea.Y) {
                NewPosition.Y = Math.Max(0, Y - (ViewableHeight / 2));
            }
            if (Y >= ViewableArea.Bottom) {
                NewPosition.Y = Math.Min(vScrollBar.Maximum, Y - (ViewableHeight / 2));
            }

            ScrollEditor(NewPosition);
        }

        public void ScrollEditor(Point NewPosition) {
            ViewableArea.Location = NewPosition;
            hScrollBar.Value = NewPosition.X;
            vScrollBar.Value = NewPosition.Y;
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        public void SelectObject(Object no)
        {
            if(mode != null)
                mode.SelectObject(no);
        }

        private object Clipboard = null;
        private EditionMode ClipboardOriginMode;

        public void cut()
        {
            object nc = mode.copy();
            if (nc != null)
                Clipboard = nc;

            ClipboardOriginMode = mode;
            mode.DeleteObject();
        }

        public void copy()
        {
            object nc = mode.copy();
            if (nc != null)
                Clipboard = nc;
            ClipboardOriginMode = mode;
        }

        public void paste()
        {
            SetEditionMode(ClipboardOriginMode);
            mode.paste(Clipboard);
        }

        public void delete()
        {
            mode.DeleteObject();
        }

        private void DrawingArea_MouseUp(object sender, MouseEventArgs e)
        {
            mode.MouseUp();
        }
    }
}
