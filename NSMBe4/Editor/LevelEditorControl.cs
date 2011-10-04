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
using System.Drawing;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class LevelEditorControl : UserControl
    {
        public float zoom = 1;
        private bool drag = false;
        public LevelMinimap minimap;
        public MinimapControl minimapctrl;
        public UndoManager UndoManager;
        public Image bgImage;
        public int bgX, bgY;
        public int dsScreenX, dsScreenY;
        public bool showDSScreen = false;
        public bool ignoreMouse = false;

        public LevelEditorControl() {
            InitializeComponent();
            Ready = false;
            hScrollBar.Visible = false;
            vScrollBar.Visible = false;
            MouseWheel += new MouseEventHandler(DrawingArea_MouseWheel);
            DrawingArea.MouseWheel += new MouseEventHandler(DrawingArea_MouseWheel);
            //dragTimer.Start();
        }

        public void LoadUndoManager(ToolStripSplitButton Undo, ToolStripSplitButton Redo)
        {
            UndoManager = new UndoManager(Undo, Redo, this);
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

        #region Rendering

        public void repaint()
        {
            DrawingArea.Invalidate();
        }

        private void DrawingArea_Paint(object sender, PaintEventArgs e) {
            if (!Ready) return;
            minimap.Invalidate(true);
            minimapctrl.Invalidate(true);

            Rectangle ViewableBlocks = new Rectangle(hScrollBar.Value, vScrollBar.Value, ViewableWidth, ViewableHeight);
            Rectangle ViewablePixels = new Rectangle(hScrollBar.Value * 16, vScrollBar.Value * 16, ViewableWidth * 16, ViewableHeight * 16);
            //Viewable.X += 4; Viewable.Y += 4; Viewable.Width -= 8; Viewable.Height -= 8;

            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            e.Graphics.ScaleTransform(zoom, zoom);
            e.Graphics.TranslateTransform(-hScrollBar.Value * 16, -vScrollBar.Value * 16);

            if (bgImage != null)
                e.Graphics.DrawImage(bgImage, bgX, bgY);

            //RENDER PANNING BLOCKS GRID
            for(int x = ViewableBlocks.X / 16; x <= (ViewableBlocks.Width+ViewableBlocks.X)/16; x++)
                for (int y = ViewableBlocks.Y / 16; y <= (ViewableBlocks.Height + ViewableBlocks.Y) / 16; y++)
                    e.Graphics.DrawRectangle(Pens.LightGray, x * 256, y * 256, 256, 256);


            //RENDER OBJECTS
            for (int ObjIdx = 0; ObjIdx < Level.Objects.Count; ObjIdx++) {
                Rectangle ObjRect = new Rectangle(Level.Objects[ObjIdx].X, Level.Objects[ObjIdx].Y, Level.Objects[ObjIdx].Width, Level.Objects[ObjIdx].Height);
                if (ObjRect.IntersectsWith(ViewableArea)) {
                    Level.Objects[ObjIdx].render(e.Graphics, this);
                }
            }

            foreach(NSMBSprite s in Level.Sprites)
                if(ViewablePixels.IntersectsWith(s.getRect()) || s.AlwaysDraw())
                    s.render(e.Graphics, this);

            foreach (NSMBView v in Level.Views)
                v.render(e.Graphics, this);
            foreach (NSMBView v in Level.Zones)
                v.render(e.Graphics, this);

            foreach(NSMBEntrance n in Level.Entrances)
                if(ViewablePixels.Contains(n.X, n.Y))
                    n.render(e.Graphics, this);

            foreach (NSMBPath p in Level.Paths)
                p.render(e.Graphics, this, false);
            foreach (NSMBPath p in Level.ProgressPaths)
                p.render(e.Graphics, this, false);

            if (mode != null)
                mode.RenderSelection(e.Graphics);

            // DS Screen preview
            if (showDSScreen) {
                e.Graphics.DrawRectangle(Pens.BlueViolet, dsScreenX, dsScreenY, 256, 192);
                e.Graphics.DrawLine(Pens.BlueViolet, dsScreenX + 128, dsScreenY, dsScreenX + 128, dsScreenY + 192);
                e.Graphics.DrawLine(Pens.BlueViolet, dsScreenX, dsScreenY + 96, dsScreenX + 256, dsScreenY + 96);
            }

            e.Graphics.TranslateTransform(hScrollBar.Value * 16, vScrollBar.Value * 16);
        }
        #endregion

        public bool ProcessCmdKeyHack(ref Message msg, Keys keyData)
        {
            return ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Console.Out.WriteLine(keyData);
            if (keyData == (Keys.Control | Keys.X))
            {
                cut();
                return true;
            }
            if (keyData == (Keys.Control | Keys.C))
            {
                copy();
                return true;
            }
            if (keyData == (Keys.Control | Keys.V))
            {
                paste();
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                Level.Save();
                return true;
            }
            if (keyData == (Keys.Control | Keys.Z))
            {
                UndoManager.onUndoLast(null, null);
                return true;
            }
            if (keyData == (Keys.Control | Keys.Y))
            {
                UndoManager.onRedoLast(null, null);
                return true;
            }
            if (keyData == (Keys.Delete))
            {
                delete();
                return true;
            }
            int xDelta = 0, yDelta = 0;
            if (keyData == Keys.Up)
                yDelta -= 1;
            if (keyData == Keys.Down)
                yDelta += 1;
            if (keyData == Keys.Left)
                xDelta -= 1;
            if (keyData == Keys.Right)
                xDelta += 1;
            if (xDelta != 0 || yDelta != 0) {
                mode.MoveObjects(xDelta, yDelta);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        bool scrollingDrag = false;

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e) {
            if (Ready)
            {
                if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Alt)
                {
                    DragStartX = e.X;
                    DragStartY = e.Y;
                    scrollingDrag = true;
                    return;
                }
                if (e.Button == MouseButtons.Left) {
                    drag = true;
                    this.Focus();
                }

                if (mode != null)
                    mode.MouseDown((int)(e.X / zoom) + hScrollBar.Value * 16, (int)(e.Y/zoom) + vScrollBar.Value * 16);
            }
        }

        private void DrawingArea_SizeChanged(object sender, EventArgs e)
        {
            ignoreMouse = true;
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e) {
            if (ignoreMouse)
            {
                ignoreMouse = false;
                return;
            }
            int DragSpeed = (int)Math.Ceiling(16 * zoom);

            if (scrollingDrag)
            {
                int NewX = e.X;
                int NewY = e.Y;
                int XDelta = (NewX - DragStartX) / DragSpeed;
                int YDelta = (NewY - DragStartY) / DragSpeed;
                if (XDelta != 0 || YDelta != 0)
                {
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
            else if (e.Button == MouseButtons.Left && Ready && mode != null) {
                mode.MouseDrag((int)(e.X / zoom) + hScrollBar.Value * 16, (int)(e.Y / zoom) + vScrollBar.Value * 16);
            }


        }

        public void EnsurePosVisible(int X, int Y) {
            Point NewPosition = new Point(ViewableArea.X, ViewableArea.Y);
            if (X < ViewableArea.X)
                NewPosition.X = Math.Max(0, X - (ViewableWidth / 2));
            if (X >= ViewableArea.Right)
                NewPosition.X = Math.Min(hScrollBar.Maximum, X - (ViewableWidth / 2));
            if (Y < ViewableArea.Y)
                NewPosition.Y = Math.Max(0, Y - (ViewableHeight / 2));
            if (Y >= ViewableArea.Bottom)
                NewPosition.Y = Math.Min(vScrollBar.Maximum, Y - (ViewableHeight / 2));

            ScrollEditor(NewPosition);
        }

        public void ScrollToObjects(List<LevelItem> objs)
        {
            foreach (LevelItem obj in objs)
                if (ViewableArea.IntersectsWith(new Rectangle(obj.x / 16, obj.y / 16, obj.width / 16, obj.height / 16)))
                    return;
            if (objs.Count > 0)
                EnsurePosVisible(objs[0].x / 16, objs[0].y / 16);
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

        public void cut()
        {
            copy();
            mode.DeleteObject();
        }

        public void copy()
        {
            string str = mode.copy();
            if (str.Length > 0)
                Clipboard.SetText("NSMBeClip|" + str + "|");
        }

        public void paste()
        {
            string str = Clipboard.GetText().Trim();
            if (str.Length > 0 && str.StartsWith("NSMBeClip|") && str.EndsWith("|")) {
                mode.paste(str.Substring(10, str.Length - 11));
                mode.Refresh();
            }
        }

        public void delete()
        {
            mode.DeleteObject();
        }

        private void DrawingArea_MouseUp(object sender, MouseEventArgs e)
        {
            scrollingDrag = false;
            drag = false;
            mode.MouseUp();
        }

        private void dragTimer_Tick(object sender, EventArgs e)
        {
            Point mousePos = this.PointToClient(MousePosition);
            if ((MouseButtons == MouseButtons.Left) && drag)
            {
                if (mousePos.X < 0 && hScrollBar.Value > 0)
                    hScrollBar.Value -= 1;
                if (mousePos.X > DrawingArea.Width && hScrollBar.Value < hScrollBar.Maximum)
                    hScrollBar.Value += 1;
                if (mousePos.Y < 0 && vScrollBar.Value > 0)
                    vScrollBar.Value -= 1;
                if (mousePos.Y > DrawingArea.Height && vScrollBar.Value < vScrollBar.Maximum)
                    vScrollBar.Value += 1;
                mode.MouseDrag((int)(mousePos.X / zoom) + hScrollBar.Value * 16, (int)(mousePos.Y / zoom) + vScrollBar.Value * 16);
            }
        }

    }
}
