using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class BackgroundDragEditionMode : EditionMode
    {
        public BackgroundDragEditionMode(NSMBLevel l, LevelEditorControl edc) : base(l, edc) { }

        int dx;
        int dy;

        public override void MouseDown(int x, int y)
        {
            dx = EdControl.bgX - x;
            dy = EdControl.bgY - y;
        }

        public override void MouseDrag(int x, int y)
        {
            EdControl.bgX = dx + x;
            EdControl.bgY = dy + y;
            EdControl.repaint();
        }

        public override void RenderSelection(System.Drawing.Graphics g)
        {
        }

        public override void SelectObject(object o)
        {
        }

        public override void Refresh()
        {
        }
    }
}
