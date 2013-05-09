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
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NSMBe4
{
    public class ObjectsEditionMode:EditionMode
    {
        public bool snapTo8Pixels = true;
        public bool resizeHandles;

        bool CloneMode, SelectMode;
        int dx, dy; //MouseDown position
        int lx, ly; //last position
        
        bool CreateObj;
        NSMBObject newObj;

        MouseAction mouseAct = new MouseAction();

        int minBoundX, minBoundY; //the top left corner of the selected objects
        int maxBoundX, maxBoundY; //the top left corner of the selected objects
        int minSizeX, minSizeY; //the minimum size of all resizable objects.
        int selectionSnap; //The max snap in the selection :P

        Rectangle SelectionRectangle;

        List<LevelItem> SelectedObjects = new List<LevelItem>();
        List<LevelItem> CurSelectedObjs = new List<LevelItem>();
        bool removing = false;
        public GoodTabsPanel tabs;

        public ObjectsEditionMode(NSMBLevel Level, LevelEditorControl EdControl)
            : base(Level, EdControl)
        {
            tabs = new GoodTabsPanel(EdControl);
            SetPanel(tabs);
            tabs.Dock = DockStyle.Fill;
            resizeHandles = Properties.Settings.Default.ShowResizeHandles;
        }

        public override void SelectObject(Object o)
        {
            SelectedObjects.Clear();
            CurSelectedObjs.Clear();
            if ((o is LevelItem) && (!SelectedObjects.Contains(o as LevelItem) || SelectedObjects.Count != 1))
                SelectedObjects.Add(o as LevelItem);
            if (o is List<LevelItem>)
                SelectedObjects.AddRange(o as List<LevelItem>);
            tabs.SelectObjects(SelectedObjects);
            UpdateSelectionBounds();
            UpdatePanel();
        }

        public override void SelectAll()
        {
            SelectedObjects.Clear();
            CurSelectedObjs.Clear();
            foreach (NSMBObject o in EdControl.Level.Objects) SelectedObjects.Add(o);
            foreach (NSMBSprite s in EdControl.Level.Sprites) SelectedObjects.Add(s);
            foreach (NSMBEntrance e in EdControl.Level.Entrances) SelectedObjects.Add(e);
            foreach (NSMBView v in EdControl.Level.Views) SelectedObjects.Add(v);
            foreach (NSMBView z in EdControl.Level.Zones) SelectedObjects.Add(z);
            foreach (NSMBPath p in EdControl.Level.Paths)
                foreach (NSMBPathPoint pp in p.points)
                    SelectedObjects.Add(pp);
            foreach (NSMBPath p in EdControl.Level.ProgressPaths)
                foreach (NSMBPathPoint pp in p.points)
                    SelectedObjects.Add(pp);
            tabs.SelectObjects(SelectedObjects);
            UpdateSelectionBounds();
            EdControl.repaint();
        }

        private void drawResizeKnob(Graphics g, int x, int y)
        {
            g.FillRectangle(Brushes.White, x - 3, y - 3, 6, 6);
            g.DrawRectangle(Pens.Black, x - 3, y - 3, 6, 6);
        }
        public override void RenderSelection(Graphics g)
        {
            if (SelectionRectangle != null && SelectMode)
                g.DrawRectangle(Pens.LightBlue, SelectionRectangle);

            foreach (LevelItem o in SelectedObjects)
                if (!CurSelectedObjs.Contains(o))
                    RenderSelectedObject(o, g);
            if (!removing)
                foreach (LevelItem o in CurSelectedObjs)
                    RenderSelectedObject(o, g);
        }

        private void RenderSelectedObject(LevelItem o, Graphics g)
        {
            if (o is NSMBView)
            {
                Color c;
                if ((o as NSMBView).isZone)
                    c = Color.LightGreen;
                else
                    c = Color.White;

                g.FillRectangle(new SolidBrush(Color.FromArgb(80, c)), o.x, o.y, o.width, o.height);
                Rectangle viewText = GetViewTextRect(o);
                if (viewText != Rectangle.Empty)
                {
                    SolidBrush fill = new SolidBrush(Color.FromArgb(80, c));
                    g.FillRectangle(fill, viewText);
                    g.DrawRectangle(Pens.White, viewText);
                    fill.Dispose();
                }
            }

            if (o is NSMBPathPoint)
            {
                Bitmap img = Properties.Resources.pathpoint_add;
                g.DrawImage(img, o.x + 16, o.y);
                img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                g.DrawImage(img, o.x - 16, o.y);
            }

            g.DrawRectangle(Pens.White, o.x, o.y, o.width, o.height);
            g.DrawRectangle(Pens.Black, o.x-1, o.y-1, o.width+2, o.height+2);
            if (o.isResizable && resizeHandles)
            {
                drawResizeKnob(g, o.x, o.y);
                drawResizeKnob(g, o.x, o.y + o.height);
                drawResizeKnob(g, o.x, o.y + o.height / 2);
                drawResizeKnob(g, o.x + o.width, o.y);
                drawResizeKnob(g, o.x + o.width, o.y + o.height);
                drawResizeKnob(g, o.x + o.width, o.y + o.height / 2);
                drawResizeKnob(g, o.x + o.width / 2, o.y);
                drawResizeKnob(g, o.x + o.width / 2, o.y + o.height);
            }
        }

        public void ReloadObjectPicker()
        {
            tabs.objects.tileset0picker.reload();
            tabs.objects.tileset1picker.reload();
            tabs.objects.tileset2picker.reload();
            //TODO: Fix.
            //This is called when changing tilesets and like.
        }

        public void UpdateSelectionBounds()
        {
            minBoundX = Int32.MaxValue;
            minBoundY = Int32.MaxValue;
            maxBoundX = 0;
            maxBoundY = 0;
            minSizeX = Int32.MaxValue;
            minSizeY = Int32.MaxValue;
            selectionSnap = snapTo8Pixels?8:1;
            foreach (LevelItem o in SelectedObjects)
            {
                if (o.rx < minBoundX) minBoundX = o.rx;
                if (o.ry < minBoundY) minBoundY = o.ry;
                if (o.rx + o.rwidth > maxBoundX) maxBoundX = o.rx + o.rwidth;
                if (o.ry + o.rheight > maxBoundY) maxBoundY = o.ry + o.rheight;
                if (o.snap > selectionSnap) selectionSnap = o.snap;

                if (o.isResizable)
                {
                    if (o is NSMBView && !(o as NSMBView).isZone) {
                        if (o.width - 256 < minSizeX) minSizeX = o.width - 256 + selectionSnap;
                        if (o.height - 192 < minSizeY) minSizeY = o.height - 192 + selectionSnap;
                    } else {
                        if (o.width < minSizeX) minSizeX = o.width;
                        if (o.height < minSizeY) minSizeY = o.height;
                    }
                }
            }
        }

        private void selectIfInside(LevelItem it, Rectangle r)
        {
            if (it is NSMBView)
            {
                Rectangle viewText = GetViewTextRect(it);
                if (r.IntersectsWith(viewText) && !SelectedObjects.Contains(it))
                    SelectedObjects.Add(it);
            }
            else if (r.IntersectsWith(new Rectangle(it.x, it.y, it.width, it.height)) && !SelectedObjects.Contains(it))
                    SelectedObjects.Add(it);
        }

        private Rectangle GetViewTextRect(LevelItem view)
        {
            Rectangle visible = EdControl.ViewablePixels;
            visible = new Rectangle(visible.X, visible.Y, visible.Width, visible.Height);
            if (!visible.IntersectsWith(new Rectangle(view.x, view.y, view.width, view.height)))
                return Rectangle.Empty;
            NSMBView v = view as NSMBView;
            Rectangle viewText = new Rectangle(new Point(Math.Max(v.x, visible.X), Math.Max(v.y, visible.Y) + (v.isZone ? 16 : 0)), TextRenderer.MeasureText(v.GetDisplayString(), NSMBGraphics.SmallInfoFont));
            return viewText;
        }

        public void findSelectedObjects(int x1, int y1, int x2, int y2, bool firstOnly, bool clearSelection)
        {
            if (clearSelection)
                SelectedObjects.Clear();

            if (x1 > x2) { int aux = x1; x1 = x2; x2 = aux; }
            if (y1 > y2) { int aux = y1; y1 = y2; y2 = aux; }
            
            Rectangle r = new Rectangle(x1, y1, x2-x1, y2-y1);
            SelectionRectangle = r;
            foreach (NSMBObject o in Level.Objects) selectIfInside(o, r);
            foreach (NSMBSprite o in Level.Sprites) selectIfInside(o, r);
            foreach (NSMBEntrance o in Level.Entrances) selectIfInside(o, r);
            foreach (NSMBView v in Level.Views) selectIfInside(v, r);
            foreach (NSMBView z in Level.Zones) selectIfInside(z, r);
            foreach (NSMBPath p in Level.Paths)
                foreach (NSMBPathPoint pp in p.points)
                    selectIfInside(pp, r);
            foreach (NSMBPath p in Level.ProgressPaths)
                foreach (NSMBPathPoint pp in p.points)
                    selectIfInside(pp, r);

            if (firstOnly && SelectedObjects.Count > 1)
            {
                LevelItem obj = SelectedObjects[SelectedObjects.Count - 1];
                SelectedObjects.Clear();
                SelectedObjects.Add(obj);
            }

            UpdateSelectionBounds();
            EdControl.repaint();
        }

        private bool isInSelection(int x, int y)
        {
            //Search in reverse order so that the top object is selected
            for (int l = SelectedObjects.Count - 1; l > -1; l--) {
                LevelItem o = SelectedObjects[l];
                if (x >= o.x && x < o.x + o.width)
                    if (y >= o.y && y < o.y + o.height)
                        return true;
            }
            return false;
        }

        public override void MouseDown(int x, int y, MouseButtons buttons)
        {
            //Right clicking creates a new object
            if (buttons == MouseButtons.Right) {
                dx = x / 16;
                dy = y / 16;
                lx = x;
                ly = y;
                CreateObj = true;
                if (tabs.SelectedTab == 2) //The sprite tab
                {
                    NSMBSprite newSprite = new NSMBSprite(Level);
                    newSprite.Type = tabs.sprites.getSelectedType();
                    if (newSprite.Type == -1)
                        return;
                    newSprite.Data = new byte[6];
                    newSprite.x = x;
                    newSprite.y = y;
                    EdControl.UndoManager.Do(new AddLvlItemAction(UndoManager.ObjToList(newSprite)));
                    SelectObject(newSprite);
                    return;
                }
                newObj = new NSMBObject(tabs.objects.getObjectType(), tabs.objects.getTilesetNum(), dx, dy, 1, 1, Level.GFX);
                EdControl.UndoManager.Do(new AddLvlItemAction(UndoManager.ObjToList(newObj)));
                SelectObject(newObj);
                return;
            }
            lx = x;
            ly = y;
            dx = x;
            dy = y;

            mouseAct = getActionAtPos(x, y);
            // Resize with the shift key
            if (mouseAct.nodeType != CreateNode.None)
            {
                NSMBPathPoint pp = new NSMBPathPoint(mouseAct.node);
                int zIndex = pp.parent.points.IndexOf(mouseAct.node);
                if (mouseAct.nodeType == CreateNode.After)
                {
                    pp.x += 16;
                    zIndex++;
                }
                else
                    pp.x -= 16;
                EdControl.UndoManager.Do(new AddPathNodeAction(UndoManager.ObjToList(pp), zIndex));
                SelectObject(pp);
            }
            else
            {
                if (Control.ModifierKeys == Keys.Shift && mouseAct.drag && mouseAct.vert == ResizeType.ResizeNone && mouseAct.hor == ResizeType.ResizeNone)
                {
                    mouseAct.vert = ResizeType.ResizeEnd;
                    mouseAct.hor = ResizeType.ResizeEnd;
                }
                if (!mouseAct.drag)
                {
                    // Select an object
                    findSelectedObjects(x, y, x, y, true, true);
                    SelectMode = SelectedObjects.Count == 0;
                }
                else if (mouseAct.vert == ResizeType.ResizeNone && mouseAct.hor == ResizeType.ResizeNone)
                {
                    List<LevelItem> selectedObjectsBack = new List<LevelItem>();
                    selectedObjectsBack.AddRange(SelectedObjects);

                    // Select an object
                    findSelectedObjects(x, y, x, y, true, true);

                    if (SelectedObjects.Count == 0)
                        SelectMode = true;
                    else
                    {
                        if (selectedObjectsBack.Contains(SelectedObjects[0]))
                            SelectedObjects = selectedObjectsBack;
                    }
                    UpdateSelectionBounds();
                    EdControl.repaint();
                }

                if (!SelectMode)
                {
                    CloneMode = Control.ModifierKeys == Keys.Control;
                    lx -= selectionSnap / 2;
                    ly -= selectionSnap / 2;
                }
            }
            EdControl.repaint();

            tabs.SelectObjects(SelectedObjects);
            UpdatePanel();
        }


        public override void MouseDrag(int x, int y)
        {
            //Resize the new object that was created by right-clicking.
            if (CreateObj && newObj != null)
            {
                Rectangle r = newObj.getRectangle();
                x = Math.Max(0, x / 16);
                y = Math.Max(0, y / 16);
                if (x == lx && y == ly) return;
                lx = x;
                ly = y;
                newObj.X = Math.Min(lx, dx);
                newObj.Y = Math.Min(ly, dy);
                newObj.Width = Math.Abs(lx - dx) + 1;
                newObj.Height = Math.Abs(ly - dy) + 1;
                newObj.UpdateObjCache();
                r = Rectangle.Union(r, newObj.getRectangle());
                Level.repaintTilemap(r.X, r.Y, r.Width, r.Height);
                EdControl.repaint();
                return;
            }

            if(lx == x && ly == y) // don't clone objects if there is no visible movement
                return;

            if(SelectMode)
            {
                findSelectedObjects(x, y, dx, dy, false, true);
                lx = x;
                ly = y;
            }
            else
            {
                UpdateSelectionBounds();
                if (CloneMode)
                {
                    List<LevelItem> newObjects = CloneList(SelectedObjects);
                    EdControl.UndoManager.Do(new AddLvlItemAction(newObjects));

                    CloneMode = false;
                    mouseAct.vert = ResizeType.ResizeNone;
                    mouseAct.hor = ResizeType.ResizeNone;

                    SelectedObjects = newObjects;
                }

                if (mouseAct.hor == ResizeType.ResizeNone && mouseAct.vert == ResizeType.ResizeNone)
                {
                    int xDelta = x-lx;
                    int yDelta = y-ly;
                    if (xDelta < -minBoundX) xDelta = -minBoundX;
                    if (yDelta < -minBoundY) yDelta = -minBoundY;
                    xDelta &= ~(selectionSnap - 1);
                    yDelta &= ~(selectionSnap - 1);
                    if (xDelta == 0 && yDelta == 0) return;
                    minBoundX += xDelta;
                    minBoundY += yDelta;
                    EdControl.UndoManager.Do(new MoveResizeLvlItemAction(SelectedObjects, xDelta, yDelta));
                    lx += xDelta;
                    ly += yDelta;
                
                    //Force align =D
                    //Only done when ONE object because you'll probably NOT want multiple objects
                    //moving relative to each other.
                    if(SelectedObjects.Count == 1)
                        foreach (LevelItem o in SelectedObjects)
                            if (o.rx % selectionSnap != 0 || o.ry % selectionSnap != 0 || o.rwidth % selectionSnap != 0 || o.rheight % selectionSnap != 0)
                                EdControl.UndoManager.Do(new MoveResizeLvlItemAction(UndoManager.ObjToList(o), -o.rx % selectionSnap, -o.ry % selectionSnap, -o.rwidth % selectionSnap, -o.rheight % selectionSnap));

                }
                else
                {
                    int xDelta = x-lx;
                    int yDelta = y-ly;

                    int xMoveDelta = 0;
                    int xResizeDelta = 0;
                    int yMoveDelta = 0;
                    int yResizeDelta = 0;

                    xDelta &= ~(selectionSnap - 1);
                    yDelta &= ~(selectionSnap - 1);
                    if (xDelta == 0 && yDelta == 0) return;

                    if (mouseAct.hor == ResizeType.ResizeBegin)
                    {
                        if (-xDelta <= -minSizeX + selectionSnap) xDelta = -(-minSizeX + selectionSnap);
                        if (xDelta < -minBoundX) xDelta = -minBoundX;
                        xMoveDelta = xDelta;
                        xResizeDelta = -xDelta;
                    }
                    if (mouseAct.vert == ResizeType.ResizeBegin)
                    {
                        if (-yDelta <= -minSizeY + selectionSnap) yDelta = -(-minSizeY + selectionSnap);
                        if (yDelta < -minBoundY) yDelta = -minBoundY;
                        yMoveDelta = yDelta;
                        yResizeDelta = -yDelta;
                    }
                    if (mouseAct.hor == ResizeType.ResizeEnd)
                    {
                        if (xDelta <= -minSizeX + selectionSnap) xDelta = -minSizeX + selectionSnap;
                        xResizeDelta = xDelta;
                    }
                    if (mouseAct.vert == ResizeType.ResizeEnd)
                    {
                        if (yDelta <= -minSizeY + selectionSnap) yDelta = -minSizeY + selectionSnap;
                        yResizeDelta = yDelta;
                    }
                    if (xMoveDelta == 0 && yMoveDelta == 0 && xResizeDelta == 0 && yResizeDelta == 0) return;

                    minBoundX += xMoveDelta;
                    minBoundY += yMoveDelta;
                    minSizeX += xResizeDelta;
                    minSizeY += yResizeDelta;
                    EdControl.UndoManager.Do(new MoveResizeLvlItemAction(SelectedObjects, xMoveDelta, yMoveDelta, xResizeDelta, yResizeDelta));
                    lx += xDelta;
                    ly += yDelta;
                }
            }
        }

        enum ResizeType
        {
            ResizeBegin,
            ResizeNone,
            ResizeEnd
        }

        enum CreateNode
        {
            None,
            Before,
            After
        }

        class MouseAction
        {
            public bool drag = false;
            public ResizeType vert = ResizeType.ResizeNone;
            public ResizeType hor = ResizeType.ResizeNone;
            public CreateNode nodeType = CreateNode.None;
            public NSMBPathPoint node = null;
        }

        private MouseAction getActionAtPos(int x, int y)
        {
            MouseAction act = new MouseAction();
            for (int l = SelectedObjects.Count - 1; l > -1; l--)
            {
                LevelItem o = SelectedObjects[l];
                // For clicking the plus buttons on selected nodes
                if (o is NSMBPathPoint)
                {
                    NSMBPathPoint pp = o as NSMBPathPoint;
                    if (x >= pp.x + 16 && x < pp.x + 32 && y >= pp.y && y < pp.y + 16)
                    {
                        act.nodeType = CreateNode.After;
                        act.node = pp;
                        return act;
                    }
                    if (x >= pp.x - 16 && x < pp.x && y >= pp.y && y < pp.y + 16)
                    {
                        act.nodeType = CreateNode.Before;
                        act.node = pp;
                        return act;
                    }
                }
                if (o.isResizable && resizeHandles)
                {
                    act.drag = true;

                    if (x >= o.x - 8 && x <= o.x + 4)
                        act.hor = ResizeType.ResizeBegin;
                    else if (x >= o.x + o.width - 4 && x <= o.x + o.width + 8)
                        act.hor = ResizeType.ResizeEnd;
                    else if (x >= o.x && x <= o.x + o.width)
                        act.hor = ResizeType.ResizeNone;
                    else act.drag = false;

                    if (y >= o.y - 8 && y <= o.y + 4)
                        act.vert = ResizeType.ResizeBegin;
                    else if (y >= o.y + o.height - 4 && y <= o.y + o.height + 8)
                        act.vert = ResizeType.ResizeEnd;
                    else if (y >= o.y && y <= o.y + o.height)
                        act.vert = ResizeType.ResizeNone;
                    else act.drag = false;
                    //Only display move cursor on views if cursor is over text
                    if (act.vert == ResizeType.ResizeNone && act.hor == ResizeType.ResizeNone && o is NSMBView && !GetViewTextRect(o).Contains(x, y))
                        act.drag = false;
                }
                else
                {
                    act.hor = ResizeType.ResizeNone;
                    act.vert = ResizeType.ResizeNone;

                    act.drag = false;

                    if (x >= o.x && x < o.x + o.width && y >= o.y && y < o.y + o.height)
                        act.drag = true;
                }

                if (act.drag) return act;
            }
            return act;
        }

        public override void MouseUp()
        {
            if (CreateObj)
                SelectObject(null);
            CreateObj = false;
            newObj = null;
            SelectMode = false;
            EdControl.UndoManager.merge = false;
            EdControl.repaint();
            tabs.SelectObjects(SelectedObjects);
        }

        private Cursor getCursorForPos(int x, int y)
        {
            MouseAction act = getActionAtPos(x, y);
            
            if (!act.drag) return Cursors.Default;

            if (act.vert == ResizeType.ResizeBegin && act.hor == ResizeType.ResizeBegin) return Cursors.SizeNWSE;
            if (act.vert == ResizeType.ResizeEnd && act.hor == ResizeType.ResizeEnd) return Cursors.SizeNWSE;

            if (act.vert == ResizeType.ResizeBegin && act.hor == ResizeType.ResizeEnd) return Cursors.SizeNESW;
            if (act.vert == ResizeType.ResizeEnd && act.hor == ResizeType.ResizeBegin) return Cursors.SizeNESW;

            if (act.vert == ResizeType.ResizeNone && act.hor == ResizeType.ResizeNone) return Cursors.SizeAll;
            if (act.vert == ResizeType.ResizeNone) return Cursors.SizeWE;
            if (act.hor == ResizeType.ResizeNone) return Cursors.SizeNS;

            return Cursors.Default;
        }

        public override void MouseMove(int x, int y)
        {
            EdControl.Cursor = getCursorForPos(x, y);
        }

        public void UpdatePanel()
        {
            tabs.UpdateInfo();
            if(SelectedObjects.Count == 1)
                EdControl.editor.coordinateViewer1.setLevelItem(SelectedObjects[0]);
            else
                EdControl.editor.coordinateViewer1.setLevelItem(null);
        }

        public override void Refresh()
        {
            UpdatePanel();
        }

        public override void DeleteObject()
        {
            if (SelectedObjects == null || SelectedObjects.Count == 0)
                return;
            
            EdControl.UndoManager.Do(new RemoveLvlItemAction(SelectedObjects));

            SelectedObjects.Clear();
            tabs.SelectObjects(new List<LevelItem>());
            EdControl.repaint();
        }

        public override string copy()
        {
            if (SelectedObjects == null || SelectedObjects.Count == 0)
                return "";

            string str = "";
            foreach (LevelItem obj in SelectedObjects) {
                str += obj.ToString() + ":";
            }
            return str.Substring(0, str.Length - 1);
        }

        public override void paste(string contents)
        {
            List<LevelItem> objs = new List<LevelItem>();
            try {
                string[] data = contents.Split(':');
                int idx = 0;
                while (idx < data.Length) {
                    LevelItem obj = FromString(data, ref idx);
                    if (obj != null)
                        objs.Add(obj);
                }
            } catch { }

            if (objs.Count == 0) return;

            //now center the objects
            Rectangle ViewableBlocks = EdControl.ViewableBlocks;
            SelectedObjects = objs;
            UpdateSelectionBounds();
            int XDelta = (ViewableBlocks.X - (minBoundX / 16)) * 16;
            int YDelta = (ViewableBlocks.Y - (minBoundY / 16)) * 16;
            foreach (LevelItem obj in SelectedObjects) {
                obj.x += XDelta;
                obj.y += YDelta;
            }
            minBoundX += XDelta;
            minBoundY += YDelta;
            
            EdControl.UndoManager.Do(new AddLvlItemAction(objs));
        }

        LevelItem FromString(String[] strs, ref int idx)
        {
            switch (strs[idx])
            {
                case "OBJ":
                    return NSMBObject.FromString(strs, ref idx, EdControl.Level.GFX);
                case "SPR":
                    return NSMBSprite.FromString(strs, ref idx, EdControl.Level);
                case "ENT":
                    return NSMBEntrance.FromString(strs, ref idx, EdControl.Level);
                case "VIW":
                case "ZON":
                    return NSMBView.FromString(strs, ref idx, EdControl.Level);
                // TODO: copy and paste with paths/path points
                //case "PTH":
                //    break;
                default:
                    idx++;
                    return null;
            }
        }

        public override void MoveObjects(int xDelta, int yDelta)
        {
            xDelta *= selectionSnap;
            yDelta *= selectionSnap;
            if (minBoundX >= -xDelta && minBoundY >= -yDelta)
            {
                EdControl.UndoManager.Do(new MoveResizeLvlItemAction(SelectedObjects, xDelta, yDelta));
                minBoundX += xDelta;
                minBoundY += yDelta;
            }
        }

        public override void lower()
        {
            EdControl.UndoManager.Do(new LowerLvlItemAction(SelectedObjects));
        }

        public override void raise()
        {
            EdControl.UndoManager.Do(new RaiseLvlItemAction(SelectedObjects));
        }

        //creates a clone of a list

        private List<LevelItem> CloneList(List<LevelItem> Objects)
        {
            List<LevelItem> newObjects = new List<LevelItem>();
            foreach (LevelItem SelectedObject in Objects)
            {
                if (SelectedObject is NSMBObject) newObjects.Add(new NSMBObject(SelectedObject as NSMBObject));
                if (SelectedObject is NSMBSprite) newObjects.Add(new NSMBSprite(SelectedObject as NSMBSprite));
                if (SelectedObject is NSMBEntrance) newObjects.Add(new NSMBEntrance(SelectedObject as NSMBEntrance));
                if (SelectedObject is NSMBView) newObjects.Add(new NSMBView(SelectedObject as NSMBView));
                if (SelectedObject is NSMBPathPoint) newObjects.Add(new NSMBPathPoint(SelectedObject as NSMBPathPoint));
            }

            return newObjects;
        }

    }
}
