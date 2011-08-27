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
        bool ResizeMode, CloneMode, SelectMode;
        public bool RefreshDataEd;
        int dx, dy; //MouseDown position
        int lx, ly; //last position

        int minBoundX, minBoundY; //the top left corner of the selected objects
        int maxBoundX, maxBoundY; //the top left corner of the selected objects
        int minSizeX, minSizeY; //the minimum size of all resizable objects.
        int selectionSnap; //The max snap in the selection :P

        Rectangle SelectionRectangle;

        List<LevelItem> SelectedObjects = new List<LevelItem>();

        ObjectEditor oe;
        SpriteEditor se;
        CreatePanel cp;

        public ObjectsEditionMode(NSMBLevel Level, LevelEditorControl EdControl)
            : base(Level, EdControl)
        {
            oe = new ObjectEditor(null, EdControl);
            se = new SpriteEditor(null, EdControl);
            cp = new CreatePanel(EdControl);
            SetPanel(cp);
        }

        public override void SelectObject(Object o)
        {
            if ((o is LevelItem ) && (!SelectedObjects.Contains(o as LevelItem) || SelectedObjects.Count != 1))
            {
                SelectedObjects.Clear();
                SelectedObjects.Add(o as LevelItem);
                RefreshDataEd = false;
            }
            if (o == null)
            {
                SelectedObjects.Clear();
            }
            UpdatePanel();
        }

        public void SelectObjects(object[] o)
        {
            SelectedObjects.Clear();
            foreach (object oo in o)
            {
                if (oo is LevelItem)
                    SelectedObjects.Add(oo as LevelItem);
            }
            UpdatePanel();
        }

        public override void RenderSelection(Graphics g)
        {
            if (SelectionRectangle != null && SelectMode)
                g.DrawRectangle(Pens.LightBlue, SelectionRectangle);

            foreach (LevelItem o in SelectedObjects)
                g.DrawRectangle(Pens.White, o.x, o.y, o.width, o.height);
        }

        public void ReloadObjectPicker()
        {
            oe.ReloadObjectPicker();
        }

        private void UpdateSelectionBounds()
        {
            minBoundX = Int32.MaxValue;
            minBoundY = Int32.MaxValue;
            maxBoundX = 0;
            maxBoundY = 0;
            minSizeX = Int32.MaxValue;
            minSizeY = Int32.MaxValue;
            selectionSnap = 1;
            foreach (LevelItem o in SelectedObjects)
            {
                if (o.rx < minBoundX) minBoundX = o.rx;
                if (o.ry < minBoundY) minBoundY = o.ry;
                if (o.rx + o.rwidth > maxBoundX) maxBoundX = o.rx + o.rwidth;
                if (o.ry + o.rheight > maxBoundY) maxBoundY = o.ry + o.rheight;
                if (o.snap > selectionSnap) selectionSnap = o.snap;

                if (o.isResizable)
                {
                    if (o.width < minSizeX) minSizeX = o.width;
                    if (o.height< minSizeY) minSizeY = o.height;
                }
            }
        }

        private void selectIfInside(LevelItem it, Rectangle r)
        {
            if (r.IntersectsWith(new Rectangle(it.x, it.y, it.width, it.height)))
                SelectedObjects.Add(it);
        }

        private void findSelectedObjects(int x1, int y1, int x2, int y2, bool firstOnly)
        {
            SelectedObjects.Clear();

            if (x1 > x2) { int aux = x1; x1 = x2; x2 = aux; }
            if (y1 > y2) { int aux = y1; y1 = y2; y2 = aux; }
            
            Rectangle r = new Rectangle(x1, y1, x2-x1, y2-y1);
            SelectionRectangle = r;
            foreach (NSMBObject o in Level.Objects) selectIfInside(o, r);
            foreach (NSMBSprite o in Level.Sprites) selectIfInside(o, r);

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
            foreach (LevelItem o in SelectedObjects)
            {
                if (x >= o.x && x < o.x + o.width)
                    if (y >= o.y && y < o.y + o.height)
                        return true;
            }

            return false;
        }

        public override void MouseDown(int x, int y)
        {
            lx = x;
            ly = y;
            dx = x;
            dy = y;

            if (!isInSelection(x, y) || SelectedObjects.Count == 1)
            {
                // Select an object
                findSelectedObjects(x, y, x, y, true);
                SelectMode = SelectedObjects.Count == 0;
            }

            if (!SelectMode)
            {
                //look if we are in resize mode...
                ResizeMode = Control.ModifierKeys == Keys.Shift;
                CloneMode = Control.ModifierKeys == Keys.Control;
                SelectMode = false;
                lx -= selectionSnap / 2;
                ly -= selectionSnap / 2;
            }

            EdControl.repaint();
            UpdatePanel();
        }


        public override void MouseDrag(int x, int y)
        {
            if(lx == x && ly == y) // don't clone objects if there is no visible movement
                return;

            if(SelectMode)
            {
                findSelectedObjects(x, y, dx, dy, false);
                lx = x;
                ly = y;
            }
            else
            {
                if (CloneMode)
                {
                    List<LevelItem> newObjects = CloneList(SelectedObjects);
                    EdControl.UndoManager.Do(new AddLvlItemAction(newObjects));

                    CloneMode = false;
                    ResizeMode = false;
                    SelectedObjects = newObjects;
                }
                if (ResizeMode)
                {
                    int xDelta = x-lx;
                    int yDelta = y-ly;
                    if (xDelta <= -minSizeX + selectionSnap) xDelta = -minSizeX + selectionSnap;
                    if (yDelta <= -minSizeY + selectionSnap) yDelta = -minSizeY + selectionSnap;
                    xDelta &= ~(selectionSnap-1);
                    yDelta &= ~(selectionSnap-1);
                    if (xDelta == 0 && yDelta == 0) return;
                    minSizeX += xDelta;
                    minSizeY += yDelta;
                    EdControl.UndoManager.Do(new ResizeLvlItemAction(SelectedObjects, xDelta, yDelta));
                    lx += xDelta;
                    ly += yDelta;
                }
                else
                {
                    int xDelta = x-lx;
                    int yDelta = y-ly;
                    if(xDelta < -minBoundX) xDelta = -minBoundX;
                    if(yDelta < -minBoundY) yDelta = -minBoundY;
                    xDelta &= ~(selectionSnap - 1);
                    yDelta &= ~(selectionSnap - 1);
                    if (xDelta == 0 && yDelta == 0) return;
                    minBoundX += xDelta;
                    minBoundY += yDelta;
                    Console.WriteLine(xDelta + " " + yDelta);
                    EdControl.UndoManager.Do(new MoveLvlItemAction(SelectedObjects, xDelta, yDelta));
                    lx += xDelta;
                    ly += yDelta;
                }
            }
        }

        public override void MouseUp()
        {
            SelectMode = false;
            EdControl.UndoManager.merge = false;
            EdControl.repaint();
        }

        public void UpdatePanel()
        {
            if (SelectedObjects.Count == 1)
            {
                object SelectedObject = SelectedObjects[0];
                if (SelectedObject is NSMBSprite)
                {
                    NSMBSprite s = SelectedObject as NSMBSprite;
                    NSMBSprite ps = se.s;
                    se.SetSprite(s);
                    SetPanel(se);
                    if (RefreshDataEd)
                        se.RefreshDataEditor();
                    else
                        se.UpdateDataEditor();
                }
                else if (SelectedObject is NSMBObject)
                {
                    oe.SetObject(SelectedObject as NSMBObject);
                    SetPanel(oe);
                }
            }
            else
            {
                SetPanel(cp);
            }

        }

        public override void Refresh()
        {
            UpdatePanel();
        }

        public void RefreshDataEditor()
        {
            se.RefreshDataEditor();
        }

        public override void DeleteObject()
        {
            if (SelectedObjects == null)
                return;
            if (SelectedObjects.Count == 0)
                return;

            if (SelectedObjects.Count == 1) {
                if (SelectedObjects[0] is NSMBObject)
                    EdControl.UndoManager.Do(new RemoveObjectAction(SelectedObjects[0] as NSMBObject));
                else
                    EdControl.UndoManager.Do(new RemoveSpriteAction(SelectedObjects[0] as NSMBSprite));
            } else
                EdControl.UndoManager.Do(new RemoveMultipleAction(SelectedObjects.ToArray()));

            SelectedObjects.Clear();
            EdControl.repaint();
        }
        public override string copy()
        {
            if (SelectedObjects == null)
                return "";
            if (SelectedObjects.Count == 0)
                return "";

            string str = "";
            foreach (object o in SelectedObjects) {
                str += o.ToString() + ":";
            }
            return str.Substring(0, str.Length - 1);
        }

        public override void paste(string contents)
        {
            List<object> objs = new List<object>();
            try {
                string[] data = contents.Split(':');
                int idx = 0;
                while (idx < data.Length) {
                    if (data[idx] == "OBJ") {
                        objs.Add(NSMBObject.FromString(data, ref idx, oe.EdControl.GFX));
                    } else if (data[idx] == "SPR") {
                        objs.Add(NSMBSprite.FromString(data, ref idx, oe.EdControl.Level));
                    } else
                        idx++;
                }
            } catch { }

            if (objs.Count == 0) return;
            
            //now place the new objects on the topleft corner
            int XMin = Int32.MaxValue; //position of the objects
            int YMin = Int32.MaxValue;

            foreach (object oo in objs)
            {
                if (oo is NSMBObject)
                {
                    NSMBObject o = oo as NSMBObject;

                    if (o.X < XMin) XMin = o.X;
                    if (o.Y < YMin) YMin = o.Y;
                }
                if (oo is NSMBSprite)
                {
                    NSMBSprite o = oo as NSMBSprite;

                    if (o.X < XMin) XMin = o.X;
                    if (o.Y < YMin) YMin = o.Y;
                }
            }

            Rectangle va = EdControl.ViewableArea;
            int XOffs = va.X - XMin; //Offset to move all the objects
            int YOffs = va.Y - YMin; //so they are on the topleft corner
            EdControl.UndoManager.Perform(new MoveMultipleAction(objs.ToArray(), XOffs, YOffs));

            if (objs.Count == 1) {
                if (objs[0] is NSMBObject)
                    EdControl.UndoManager.Do(new AddObjectAction(objs[0] as NSMBObject));
                else
                    EdControl.UndoManager.Do(new AddSpriteAction(objs[0] as NSMBSprite));
            } else
                EdControl.UndoManager.Do(new AddMultipleAction(objs.ToArray()));
            SelectObjects(objs.ToArray());
            UpdateSelectionBounds();
        }

        //creates a clone of a list

        private List<LevelItem> CloneList(List<LevelItem> Objects)
        {
            List<LevelItem> newObjects = new List<LevelItem>();
            foreach (LevelItem SelectedObject in Objects)
            {
                if (SelectedObject is NSMBObject)
                    newObjects.Add(new NSMBObject(SelectedObject as NSMBObject));
                else if (SelectedObject is NSMBSprite)
                    newObjects.Add(new NSMBSprite(SelectedObject as NSMBSprite));
            }

            return newObjects;
        }

        public string[] getSpriteNames()
        {
            return se.allSprites;
        }

        private int GetX(object obj)
        {
            if (obj is NSMBObject)
                return (obj as NSMBObject).X;
            if (obj is NSMBSprite)
                return (obj as NSMBSprite).X;
            return -1;
        }
        private int GetY(object obj)
        {
            if (obj is NSMBObject)
                return (obj as NSMBObject).Y;
            if (obj is NSMBSprite)
                return (obj as NSMBSprite).Y;
            return -1;
        }
    }
}
