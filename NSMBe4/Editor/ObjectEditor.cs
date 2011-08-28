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
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ObjectEditor : UserControl
    {
        private NSMBObject o;
        public LevelEditorControl EdControl;
        private bool DataUpdateFlag = false;


        public ObjectEditor(NSMBObject o, LevelEditorControl EdControl)
        {
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "ObjectEditor");
            tileset0picker.Initialise(EdControl.GFX, 0);
            tileset1picker.Initialise(EdControl.GFX, 1);
            tileset2picker.Initialise(EdControl.GFX, 2);
            this.o = o;
            this.EdControl = EdControl;
            UpdateInfo(false);
        }

        public void SetObject(NSMBObject no)
        {
            bool keep = o == no;
            o = no;
            UpdateInfo(keep);
        }

        public void UpdateInfo(bool keep)
        {
            if (o == null) return;
            DataUpdateFlag = true;
            objXPosUpDown.Value = o.X;
            objYPosUpDown.Value = o.Y;
            objWidthUpDown.Value = o.Width;
            objHeightUpDown.Value = o.Height;

            if (!keep)
            {
                if (o.Tileset != 0) tileset0picker.selectObjectNumber(-1);
                if (o.Tileset != 1) tileset1picker.selectObjectNumber(-1);
                if (o.Tileset != 2) tileset2picker.selectObjectNumber(-1);

                if (o.Tileset == 0) tileset0picker.selectObjectNumber(o.ObjNum);
                if (o.Tileset == 1) tileset1picker.selectObjectNumber(o.ObjNum);
                if (o.Tileset == 2) tileset2picker.selectObjectNumber(o.ObjNum);

                tabControl1.SelectedIndex = o.Tileset;
            }
            DataUpdateFlag = false;
        }

        public void ReloadObjectPicker() {
            tileset0picker.reload();
            tileset1picker.reload();
            tileset2picker.reload();
        }

        private void objXPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //if ((int)objXPosUpDown.Value != o.X)
                //EdControl.UndoManager.Do(new MoveObjectAction(o, (int)objXPosUpDown.Value, o.Y));
        }

        private void objYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //if ((int)objYPosUpDown.Value != o.Y)
                //EdControl.UndoManager.Do(new MoveObjectAction(o, o.X, (int)objYPosUpDown.Value));
        }

        private void objWidthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //if ((int)objWidthUpDown.Value != o.Width)
            //    EdControl.UndoManager.Do(new SizeObjectAction(o, (int)objWidthUpDown.Value, o.Height));
        }

        private void objHeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //if ((int)objHeightUpDown.Value != o.Height)
            //    EdControl.UndoManager.Do(new SizeObjectAction(o, o.Width, (int)objHeightUpDown.Value));
        }

        private void addObjectButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;
            NSMBObject no = new NSMBObject(10, 0, ViewableArea.X, ViewableArea.Y, 1, 1, EdControl.GFX);
            //EdControl.UndoManager.Do(new AddObjectAction(no));
        }

        private void deleteObjectButton_Click(object sender, EventArgs e)
        {
            //EdControl.UndoManager.Do(new RemoveObjectAction(o));
        }

        private void setObjectType(int til, int obj)
        {
            if (til != 0) tileset0picker.selectObjectNumber(-1);
            if (til != 1) tileset1picker.selectObjectNumber(-1);
            if (til != 2) tileset2picker.selectObjectNumber(-1);

            //EdControl.UndoManager.Do(new ChangeObjectTypeAction(o, til, obj));
        }

        private void tileset0picker_ObjectSelected()
        {
            setObjectType(0, tileset0picker.SelectedObject);
        }

        private void tileset1picker_ObjectSelected()
        {
            setObjectType(1, tileset1picker.SelectedObject);
        }

        private void tileset2picker_ObjectSelected()
        {
            setObjectType(2, tileset2picker.SelectedObject);
        }

    }
}
