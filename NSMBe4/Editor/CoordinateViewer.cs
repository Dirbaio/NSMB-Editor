using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4.Editor
{
    public partial class CoordinateViewer : UserControl
    {
        public CoordinateViewer()
        {
            InitializeComponent();
            xUpDown.Maximum = 512 * 16;
            yUpDown.Maximum = 256 * 16;
            widthUpDown.Maximum = 512 * 16;
            heightUpDown.Maximum = 256 * 16;
        }

        public LevelEditorControl EdControl;
        public LevelItem it = null;

        bool updating = false;

        public void setLevelItem(LevelItem it)
        {
            this.it = it;
            updating = true;

            if (it == null)
            {
                Enabled = false;
            }
            else
            {
                Enabled = true;

                xUpDown.Value = it.rx / it.snap;
                yUpDown.Value = it.ry / it.snap;

                widthUpDown.Enabled = it.isResizable;
                heightUpDown.Enabled = it.isResizable;
                widthUpDown.Value = it.rwidth / it.snap;
                heightUpDown.Value = it.rheight / it.snap;
            }

            updating = false;
        }

        private void anyUpDownValueChanged(object sender, EventArgs e)
        {
            if (updating) return;

            EdControl.UndoManager.Do(new MoveResizeLvlItemAction(UndoManager.ObjToList(it),
                (int)(xUpDown.Value * it.snap - it.rx),
                (int)(yUpDown.Value * it.snap - it.ry),
                (int)(widthUpDown.Value * it.snap - it.rwidth),
                (int)(heightUpDown.Value * it.snap - it.rheight)));
        }
    }
}
