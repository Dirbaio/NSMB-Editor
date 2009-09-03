using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NSMBe4
{
    public class ObjectsEditionMode:EditionMode
    {
        bool ResizeMode, CloneMode;
        int DragStartX, DragStartY;
        Object SelectedObject = null;

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
            if (o is NSMBSprite || o is NSMBObject || o == null)
                SelectedObject = o;
            UpdatePanel();
        }

        public override void RenderSelection(Graphics g)
        {
            if (SelectedObject is NSMBObject)
            {
                NSMBObject o = SelectedObject as NSMBObject;
                g.DrawRectangle(Pens.White, o.X * 16, o.Y * 16, o.Width * 16, o.Height * 16);
            }
            if (SelectedObject is NSMBSprite)
            {
                NSMBSprite o = SelectedObject as NSMBSprite;
                g.DrawRectangle(Pens.White, o.X * 16, o.Y * 16, 16, 16);
            }
        }

        public override void MouseDown(int x, int y)
        {
            int xb = x / 16;
            int yb = y / 16;

            // Select an object
            SelectedObject = null;
            foreach(NSMBObject o in Level.Objects)
            {
                if (xb >= o.X && xb < (o.X + o.Width) && yb >= o.Y && yb < (o.Y + o.Height))
                    SelectedObject = o;
            }

            foreach(NSMBSprite s in Level.Sprites)
            {
                if (xb == s.X &&
                    yb == s.Y)
                    SelectedObject = s;
            }

            //look if we are in resize mode...
            ResizeMode = Control.ModifierKeys == Keys.Shift;
            CloneMode = Control.ModifierKeys == Keys.Control;


            UpdatePanel();
            EdControl.repaint();
            DragStartX = xb;
            DragStartY = yb;
            
        }

        public override void MouseDrag(int x, int y)
        {
            if(SelectedObject == null)
                return;

            if (CloneMode)
            {
                if (SelectedObject is NSMBObject)
                {
                    NSMBObject o = SelectedObject as NSMBObject;
                    o = new NSMBObject(o);
                    Level.Objects.Add(o);
                    SelectedObject = o;
                }
                else if (SelectedObject is NSMBSprite)
                {
                    NSMBSprite s = SelectedObject as NSMBSprite;
                    s = new NSMBSprite(s);
                    Level.Sprites.Add(s);
                    SelectedObject = s;
                }
                CloneMode = false;
                ResizeMode = false;
            }


            int NewX = x / 16;
            int NewY = y / 16;

            if (ResizeMode && SelectedObject is NSMBObject) {
                // Resize Object
                NSMBObject o = SelectedObject as NSMBObject;
                if (DragStartX != NewX) {
                    int NewWidth = o.Width + (NewX - DragStartX);
                    if (NewWidth > 0) {
                        o.Width = NewWidth;
                        o.UpdateObjCache();
                        DragStartX = NewX;
                    }
                    EdControl.repaint();
                    UpdatePanel();
                    SetDirtyFlag();
                }
                if (DragStartY != NewY) {
                    int NewHeight = o.Height + (NewY - DragStartY);
                    if (NewHeight > 0) {
                        o.Height = NewHeight;
                        o.UpdateObjCache();
                        DragStartY = NewY;
                    }
                    EdControl.repaint();
                    UpdatePanel();
                    SetDirtyFlag();
                }
            } else {
                // Move Object
                if (DragStartX != NewX) {
                    if (SelectedObject is NSMBSprite) {
                        NSMBSprite s = SelectedObject as NSMBSprite;
                        s.X += (NewX - DragStartX);
                        if (s.X < 0) {
                            s.X = 0;
                        }
                    } else {
                        NSMBObject o = SelectedObject as NSMBObject;
                        o.X += (NewX - DragStartX);
                        if (o.X < 0) {
                            o.X = 0;
                        }
                    }
                    DragStartX = NewX;
                    EdControl.repaint();
                    UpdatePanel();
                    SetDirtyFlag();
                }
                if (DragStartY != NewY) {
                    if (SelectedObject is NSMBSprite) {
                        NSMBSprite s = SelectedObject as NSMBSprite;
                        s.Y += (NewY - DragStartY);
                        if (s.Y < 0) {
                            s.Y = 0;
                        }
                    } else
                    {
                        NSMBObject o = SelectedObject as NSMBObject;
                        o.Y += (NewY - DragStartY);
                        if (o.Y < 0) {
                            o.Y = 0;
                        }
                    }
                    DragStartY = NewY;
                    EdControl.repaint();
                    UpdatePanel();
                    SetDirtyFlag();
                }
            }
        }

        private void UpdatePanel()
        {
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
            else
            {
                SetPanel(cp);
            }

        }

        public override void Refresh()
        {
            if (!EdControl.Level.Objects.Contains(SelectedObject as NSMBObject))
                if (!EdControl.Level.Sprites.Contains(SelectedObject as NSMBSprite))
                    SelectObject(null);
            UpdatePanel();
        }
    }
}
