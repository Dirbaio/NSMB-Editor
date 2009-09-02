using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    class ViewsEditionMode:EditionMode
    {
        private NSMBView v;
        private int DragStartX, DragStartY;
        private bool ResizeMode, CloneMode;

        public ViewsEditionMode(NSMBLevel Level, LevelEditorControl EdControl)
            : base(Level, EdControl)
        {

        }

        
        public override void MouseDown(int x, int y)
        {
            v = null;
            foreach(NSMBView vv in Level.Views)
                if (v.X <= x && v.X + v.Width >= x)
                    if (v.Y <= y && v.Y + v.Height >= y)
                        v = vv;

            EdControl.repaint();

            if (v == null)
                return;

            ResizeMode = Control.ModifierKeys == Keys.Shift;
            CloneMode = Control.ModifierKeys == Keys.Control;
            DragStartX = x;
            DragStartY = y;
            
        }

        public override void MouseDrag(int x, int y);
        public override void RenderSelection(Graphics g);
        public override void SelectObject(Object o);
        public override void Refresh();
    }
}
