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
        int DragStartX, DragStartY;
        int DragXOff, DragYOff;
        object CurObj;

        int minBoundX, minBoundY; //the top left corner of the selected objects

        Rectangle SelectionRectangle;

        List<object> SelectedObjects = new List<object>();

        ObjectEditor oe;
        SpriteEditor se;
        CreatePanel cp;

        public ObjectsEditionMode(NSMBLevel Level, LevelEditorControl EdControl, ObjectPickerControl opc)
            : base(Level, EdControl)
        {
            oe = new ObjectEditor(null, EdControl, opc);
            se = new SpriteEditor(null, EdControl);
            cp = new CreatePanel(EdControl);
            SetPanel(cp);
        }

        public override void SelectObject(Object o)
        {
            if ((o is NSMBSprite || o is NSMBObject) && (!SelectedObjects.Contains(o) || SelectedObjects.Count != 1))
            {
                SelectedObjects.Clear();
                SelectedObjects.Add(o);
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
            if (o != null)
                SelectedObjects.AddRange(o);
            UpdatePanel();
        }

        public override void RenderSelection(Graphics g)
        {
            if (SelectionRectangle != null && SelectMode)
            {
                g.DrawRectangle(Pens.LightBlue, SelectionRectangle.X*16, SelectionRectangle.Y*16, SelectionRectangle.Width*16, SelectionRectangle.Height*16);
            }
            foreach (object SelectedObject in SelectedObjects)
            {
                if (SelectedObject is NSMBObject)
                {
                    NSMBObject o = SelectedObject as NSMBObject;
                    g.DrawRectangle(Pens.White, o.X * 16, o.Y * 16, o.Width * 16, o.Height * 16);
                }
                if (SelectedObject is NSMBSprite)
                {
                    NSMBSprite o = SelectedObject as NSMBSprite;
                    g.DrawRectangle(Pens.White, o.getRect());
                }
            }
        }

        public void ReloadObjectPicker() {
            oe.ReloadObjectPicker();
        }

        private void UpdateSelectedBounds()
        {
            minBoundX = Int32.MaxValue;
            minBoundY = Int32.MaxValue;

            foreach (object oo in SelectedObjects)
            {
                if (oo is NSMBObject)
                {
                    NSMBObject o = oo as NSMBObject;
                    if (o.X < minBoundX)
                        minBoundX = o.X;
                    if (o.Y < minBoundY)
                        minBoundY = o.Y;
                }
                if (oo is NSMBSprite)
                {
                    NSMBSprite s = oo as NSMBSprite;
                    if (s.X < minBoundX)
                        minBoundX = s.X;
                    if (s.Y < minBoundY)
                        minBoundY = s.Y;
                }
            }
        }
        private void findSelectedObjects(int selXE, int selYE, bool firstOnly)
        {
            SelectedObjects.Clear();
            int xs = DragStartX < selXE ? DragStartX:selXE;
            int xb = DragStartX > selXE ? DragStartX:selXE;
            int ys = DragStartY < selYE ? DragStartY:selYE;
            int yb = DragStartY > selYE ? DragStartY:selYE;

            if (SelectionRectangle == null)
                SelectionRectangle = new Rectangle();

            SelectionRectangle.X = xs;
            SelectionRectangle.Y = ys;
            SelectionRectangle.Width = xb-xs+1;
            SelectionRectangle.Height = yb-ys+1;


            Rectangle r = new Rectangle();
            foreach (NSMBObject o in Level.Objects)
            {
                r.X = o.X;
                r.Y = o.Y;
                r.Height = o.Height;
                r.Width = o.Width;

                if (SelectionRectangle.IntersectsWith(r))
                    SelectedObjects.Add(o);
            }

            foreach (NSMBSprite s in Level.Sprites)
                if (SelectionRectangle.IntersectsWith(s.getRectB()))
                    SelectedObjects.Add(s);

            if (firstOnly && SelectedObjects.Count > 1)
            {
                object first = SelectedObjects[SelectedObjects.Count - 1];
                SelectedObjects.Clear();
                SelectedObjects.Add(first);
            }

            UpdateSelectedBounds();
            EdControl.repaint();
        }

        private object GetFirst(int x, int y)
        {
            for (int l = Level.Sprites.Count - 1; l >= 0; l--)
                if (Level.Sprites[l].getRectB().Contains(x, y))
                    return Level.Sprites[l];
            NSMBObject o;
            for (int l = Level.Objects.Count - 1; l >= 0; l--) {
                o = Level.Objects[l];
                if (o.X <= x && o.Y <= y && o.X + o.Width > x && o.Y + o.Height > y)
                    return o;
            }
            return null;
        }

        private bool isInSelection(int x, int y)
        {

            foreach (object so in SelectedObjects)
            {
                if (so is NSMBObject)
                {
                    NSMBObject o = so as NSMBObject;
                    if (x >= o.X && x < o.X + o.Width)
                        if (y >= o.Y && y < o.Y + o.Height)
                            return true;
                }
                if (so is NSMBSprite)
                {
                    NSMBSprite s = so as NSMBSprite;
                    Rectangle r = s.getRectB();
                    if (x >= r.X && y < s.Y + r.Width)
                        if (y >= r.Y && y < r.Y + r.Height)
                            return true;
                }
            }

            return false;
        }

        public override void MouseDown(int x, int y)
        {
            int xb = x / 16;
            int yb = y / 16;
            lx = xb;
            ly = yb;
            DragStartX = xb;
            DragStartY = yb;

            if (!isInSelection(xb, yb) || SelectedObjects.Count == 1)
            {
                // Select an object
                NSMBSprite pSelectedSprite = null;
                if (SelectedObjects.Count == 1 && SelectedObjects[0] is NSMBSprite)
                    pSelectedSprite = SelectedObjects[0] as NSMBSprite;
                findSelectedObjects(xb, yb, true);
                if (SelectedObjects.Count == 1 && SelectedObjects[0] is NSMBSprite)
                    RefreshDataEd = SelectedObjects[0] == pSelectedSprite;
                SelectMode = SelectedObjects.Count == 0;
            }
            if (!SelectMode)
            {
                //look if we are in resize mode...
                ResizeMode = Control.ModifierKeys == Keys.Shift;
                CloneMode = Control.ModifierKeys == Keys.Control;
                SelectMode = false;
            }
            CurObj = GetFirst(xb, yb);
            if (ResizeMode && SelectedObjects.Count == 1 && SelectedObjects[0] is NSMBObject) {
                NSMBObject o = SelectedObjects[0] as NSMBObject;
                DragXOff = (o.X + o.Width) * 16 - x;
                DragYOff = (o.Y + o.Height) * 16 - y;
            } else {
                DragXOff = x - GetX(CurObj) * 16 - 8;
                DragYOff = y - GetY(CurObj) * 16 - 8;
            }
            EdControl.repaint();
            UpdatePanel();
        }

        int lx, ly; //last position

        public override void MouseDrag(int x, int y)
        {
            int NewX = x / 16;
            int NewY = y / 16;

            if(lx == NewX && ly == NewY) // don't clone objects if there is no visible movement
                return;


            if(SelectMode)
            {
                findSelectedObjects(NewX, NewY, false);
            }
            else
            {
                if (CloneMode)
                {
                    List<object> newObjects = CloneList(SelectedObjects);
                    if (newObjects.Count == 1) {
                        if (newObjects[0] is NSMBObject)
                            EdControl.UndoManager.Do(new AddObjectAction(newObjects[0] as NSMBObject));
                        else
                            EdControl.UndoManager.Do(new AddSpriteAction(newObjects[0] as NSMBSprite));
                    } else
                        EdControl.UndoManager.Do(new AddMultipleAction(newObjects.ToArray()));
                    CloneMode = false;
                    ResizeMode = false;
                    SelectedObjects = newObjects;
                }
                if (ResizeMode)
                {
                    if (SelectedObjects.Count == 1)
                    {
                        object SelectedObject = SelectedObjects[0];
                        if (SelectedObject is NSMBObject)
                        {
                            NSMBObject o = SelectedObject as NSMBObject;
                            int nx = Math.Max(1, (x + DragXOff - o.X * 16) / 16);
                            int ny = Math.Max(1, (y + DragYOff - o.Y * 16) / 16);
                            EdControl.UndoManager.Do(new SizeObjectAction(o, nx, ny));
                        }
                    }
                }
                else
                {
                    int nx = Math.Max(-minBoundX, NewX - lx);
                    int ny = Math.Max(-minBoundY, NewY - ly);
                    minBoundX += nx;
                    minBoundY += ny;

                    if (SelectedObjects.Count == 1)
                    {
                        if (SelectedObjects[0] is NSMBObject) {
                            NSMBObject o = SelectedObjects[0] as NSMBObject;
                            EdControl.UndoManager.Do(new MoveObjectAction(o, o.X+nx, o.Y+ny));
                        } else {
                            NSMBSprite s = SelectedObjects[0] as NSMBSprite;
                            EdControl.UndoManager.Do(new MoveSpriteAction(s, s.X + nx, s.Y+ny));
                        }
                    } 
                    else
                        EdControl.UndoManager.Do(new MoveMultipleAction(SelectedObjects.ToArray(), nx, ny));
                }
            }
            lx = NewX;
            ly = NewY;
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
            SelectObject(null);
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
        }
        public override object copy()
        {
            if (SelectedObjects == null)
                return null;
            if (SelectedObjects.Count == 0)
                return null;

            List<object> copyList = new List<object>();
            copyList.AddRange(SelectedObjects);
            return copyList;
        }

        public override void paste(object contents)
        {
            if (contents is List<object>)
            {
                if ((contents as List<object>).Count == 0)
                    return;

                EdControl.UndoManager.Perform(new AddMultipleAction(CloneList(contents as List<object>).ToArray()));
                
                //now place the new objects on the topleft corner

                int XMin = Int32.MaxValue; //position of the objects
                int YMin = Int32.MaxValue;

                foreach (object oo in SelectedObjects)
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
                EdControl.UndoManager.Perform(new MoveMultipleAction(SelectedObjects.ToArray(), XOffs, YOffs));

                if (SelectedObjects.Count == 1) {
                    if (SelectedObjects[0] is NSMBObject)
                        EdControl.UndoManager.Do(new AddObjectAction(SelectedObjects[0] as NSMBObject));
                    else
                        EdControl.UndoManager.Do(new AddSpriteAction(SelectedObjects[0] as NSMBSprite));
                } else
                    EdControl.UndoManager.Do(new AddMultipleAction(SelectedObjects.ToArray()));
                UpdateSelectedBounds();
            }
        }

        //creates a clone of a list

        private List<object> CloneList(List<object> Objects)
        {
            List<object> newObjects = new List<object>();
            foreach (object SelectedObject in Objects)
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
