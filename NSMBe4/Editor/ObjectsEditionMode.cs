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
        int DragStartX, DragStartY;

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
            if (o is NSMBSprite || o is NSMBObject)
            {
                SelectedObjects.Clear();
                SelectedObjects.Add(o);
            }
            if (o == null)
                SelectedObjects.Clear();

            UpdatePanel();
        }

        public void SelectObjects(object[] o)
        {
            SelectedObjects.Clear();
            if (!(o == null))
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
                findSelectedObjects(xb, yb, true);
                SelectMode = SelectedObjects.Count == 0;
            }

            if (!SelectMode)
            {
                //look if we are in resize mode...
                ResizeMode = Control.ModifierKeys == Keys.Shift;
                CloneMode = Control.ModifierKeys == Keys.Control;
                SelectMode = false;
            }
            EdControl.repaint();
            UpdatePanel();
        }

        int lx, ly; //last position

        public override void MouseDrag(int x, int y)
        {
            int NewX = x / 16;
            int NewY = y / 16;

            if(x == NewX && y == NewY) // don't clone objects if there is no visible movement
                return;

            lx = NewX;
            ly = NewY;

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
                            EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddObject, newObjects[0], null);
                        else
                            EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddSprite, newObjects[0], null);
                    } else {
                        EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddMultiple, newObjects.ToArray(), null);
                    }
                    CloneMode = false;
                    ResizeMode = false;
                    SelectedObjects = newObjects;
                }

                int XDelta = 0;
                int YDelta = 0;

                if (DragStartX != NewX)
                {
                    XDelta = NewX - DragStartX;
                    DragStartX = NewX;
                }
                if (DragStartY != NewY)
                {
                    YDelta = NewY - DragStartY;
                    DragStartY = NewY;
                }

                if (XDelta == 0 && YDelta == 0)
                    return;

                if (ResizeMode)
                {
                    if (SelectedObjects.Count == 1)
                    {
                        object SelectedObject = SelectedObjects[0];
                        if (SelectedObject is NSMBObject)
                        {
                            NSMBObject o = SelectedObject as NSMBObject;
                            EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.SizeObject, o, new Rectangle(o.Width, o.Height, o.Width + XDelta, o.Height + XDelta));
                            o.Width += XDelta;
                            o.Height += YDelta;
                            if(o.Width < 1)
                                o.Width = 1;
                            if(o.Height < 1)
                                o.Height = 1;
                            o.UpdateObjCache();
                        }
                    }
                }
                else
                {

                    if (minBoundX + XDelta < 0)
                        XDelta = -minBoundX;
                    if (minBoundY + YDelta < 0)
                        YDelta = -minBoundY;

                    minBoundX += XDelta;
                    minBoundY += YDelta;
                    // Move Objects
                    if (SelectedObjects.Count == 1) {
                        if (SelectedObjects[0] is NSMBObject) {
                            NSMBObject o = SelectedObjects[0] as NSMBObject;
                            EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.MoveObject, o, new Rectangle(o.X, o.Y, o.X + XDelta, o.Y + YDelta));
                        } else {
                            NSMBSprite s = SelectedObjects[0] as NSMBSprite;
                            EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.MoveSprite, s, new Rectangle(s.X, s.Y, s.X + XDelta, s.Y + YDelta));
                        } 
                    } else {
                        EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.MoveMultiple, SelectedObjects.ToArray(), new Point(XDelta, YDelta));
                    }
                    foreach(object SelectedObject in SelectedObjects)
                    {
                        if (SelectedObject is NSMBSprite)
                        {
                            NSMBSprite s = SelectedObject as NSMBSprite;
                            s.X += XDelta;
                            s.Y += YDelta;
                            if (s.X < 0)
                                s.X = 0;
                            if (s.Y < 0)
                                s.Y = 0;
                        }
                        else
                        {
                            NSMBObject o = SelectedObject as NSMBObject;
                            o.X += XDelta;
                            o.Y += YDelta;
                            if (o.X < 0)
                                o.X = 0;
                            if (o.Y < 0)
                                o.Y = 0;     
                        }
                    }
                }

                EdControl.repaint();
                UpdatePanel();
                if (XDelta != 0 || YDelta != 0)
                    EdControl.FireSetDirtyFlag();
            }
        }

        public override void MouseUp()
        {
            SelectMode = false;
            EdControl.editor.undoMngr.merge = false;
            EdControl.repaint();
        }

        public void UpdatePanel()
        {
            if (SelectedObjects.Count == 1)
            {
                object SelectedObject = SelectedObjects[0];
                if (SelectedObject is NSMBSprite)
                {
                    se.SetSprite(SelectedObject as NSMBSprite);
                    SetPanel(se);
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
            UpdatePanel();
        }

        public override void DeleteObject()
        {
            if (SelectedObjects == null)
                return;
            if (SelectedObjects.Count == 0)
                return;

            if (SelectedObjects.Count == 1) {
                if (SelectedObjects[0] is NSMBObject)
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.RemoveObject, SelectedObjects[0], Level.Objects.IndexOf(SelectedObjects[0] as NSMBObject));
                else
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.RemoveSprite, SelectedObjects[0], Level.Sprites.IndexOf(SelectedObjects[0] as NSMBSprite));
            } else {
                int[] zIndex = new int[SelectedObjects.Count];
                object item;
                for (int l = 0; l < SelectedObjects.Count; l++) {
                    item = SelectedObjects[l];
                    if (item is NSMBObject)
                        zIndex[l] = Level.Objects.IndexOf(item as NSMBObject);
                    else
                        zIndex[l] = Level.Sprites.IndexOf(item as NSMBSprite);
                }
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.RemoveMultiple, SelectedObjects.ToArray(), zIndex);
            }

            foreach (object oo in SelectedObjects)
            {
                if (oo is NSMBObject)
                {
                    NSMBObject o = oo as NSMBObject;
                    Level.Objects.Remove(o);
                }
                if (oo is NSMBSprite)
                {
                    NSMBSprite o = oo as NSMBSprite;
                    Level.Sprites.Remove(o);
                }
            }

            SelectedObjects.Clear();

            EdControl.repaint();
            EdControl.FireSetDirtyFlag();

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

                SelectedObjects = CloneList(contents as List<object>);
                
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

                foreach (object oo in SelectedObjects)
                {
                    if (oo is NSMBObject)
                    {
                        NSMBObject o = oo as NSMBObject;
                        o.X += XOffs;
                        o.Y += YOffs;
                    }
                    if (oo is NSMBSprite)
                    {
                        NSMBSprite o = oo as NSMBSprite;
                        o.X += XOffs;
                        o.Y += YOffs;
                    }
                }

                if (SelectedObjects.Count == 1) {
                    if (SelectedObjects[0] is NSMBObject)
                        EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddObject, SelectedObjects[0], null);
                    else
                        EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddSprite, SelectedObjects[0], null);
                } else {
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddMultiple, SelectedObjects.ToArray(), null);
                }

                UpdateSelectedBounds();
                EdControl.repaint();
                EdControl.FireSetDirtyFlag();
            }
        }

        //creates a clone of a list and adds it to the level

        private List<object> CloneList(List<object> Objects)
        {
            List<object> newObjects = new List<object>();
            foreach (object SelectedObject in Objects)
            {
                if (SelectedObject is NSMBObject)
                {
                    NSMBObject o = SelectedObject as NSMBObject;
                    o = new NSMBObject(o);
                    Level.Objects.Add(o);
                    newObjects.Add(o);
                }
                else if (SelectedObject is NSMBSprite)
                {
                    NSMBSprite s = SelectedObject as NSMBSprite;
                    s = new NSMBSprite(s);
                    Level.Sprites.Add(s);
                    newObjects.Add(s);
                }
            }

            return newObjects;
        }
    }
}
