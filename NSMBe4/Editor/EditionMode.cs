using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NSMBe4
{
    public abstract class EditionMode
    {
        public NSMBLevel Level;
        public LevelEditorControl EdControl;

        public EditionMode(NSMBLevel Level, LevelEditorControl EdControl)
        {
            this.Level = Level;
            this.EdControl = EdControl;
        }


        public void SetDirtyFlag()
        {
            EdControl.FireSetDirtyFlag();
        }

        public void SetPanel(UserControl p)
        {
            EdControl.editor.SetPanel(p);
        }

        public abstract void MouseDown(int x, int y);
        public abstract void MouseDrag(int x, int y);
        public virtual void MouseUp() { }
        public abstract void RenderSelection(Graphics g);
        public abstract void SelectObject(Object o);
        public abstract void Refresh();

        public virtual void DeleteObject()
        {
        }

        public virtual object copy()
        {
            return null;
        }

        public virtual void paste(object contents)
        {
        }
    }
}
