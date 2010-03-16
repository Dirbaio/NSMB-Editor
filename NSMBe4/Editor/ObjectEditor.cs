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
        private LevelEditorControl EdControl;
        private bool DataUpdateFlag = false;
        private ObjectPickerControl objectPickerControl1;

        public ObjectEditor(NSMBObject o, LevelEditorControl EdControl, ObjectPickerControl opc)
        {
            InitializeComponent();
            setOPC(opc);
            this.o = o;
            this.EdControl = EdControl;
            UpdateInfo();
            LanguageManager.ApplyToContainer(this, "ObjectEditor");
        }

        private void setOPC(ObjectPickerControl opc)
        {
            objectPickerControl1 = opc;

            this.objectPickerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectPickerControl1.Location = new System.Drawing.Point(3, 70);
            this.objectPickerControl1.Name = "objectPickerControl1";
            this.objectPickerControl1.Size = new System.Drawing.Size(266, 240);
            this.objectPickerControl1.TabIndex = 1;
            this.objectPickerControl1.ObjectSelected += new NSMBe4.ObjectPickerControl.ObjectSelectedDelegate(this.objectPickerControl1_ObjectSelected);
            this.objPickerBox.Controls.Add(this.objectPickerControl1);

        }

        private void SetTileset(int T)
        {
            o.Tileset = T;
            o.UpdateObjCache();
            objectPickerControl1.CurrentTileset = T;
            objectPickerControl1.Invalidate(true);
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }
        public void SetObject(NSMBObject no)
        {
            o = no;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            if (o == null) return;
            DataUpdateFlag = true;
            objXPosUpDown.Value = o.X;
            objYPosUpDown.Value = o.Y;
            objWidthUpDown.Value = o.Width;
            objHeightUpDown.Value = o.Height;

            objTileset0Button.Checked = (o.Tileset == 0);
            objTileset1Button.Checked = (o.Tileset == 1);
            objTileset2Button.Checked = (o.Tileset == 2);

            objTypeUpDown.Value = o.ObjNum;

            objectPickerControl1.CurrentTileset = o.Tileset;
            objectPickerControl1.SelectedObject = o.ObjNum;
            objectPickerControl1.EnsureObjVisible((int)objTypeUpDown.Value);
            objectPickerControl1.Invalidate(true);
            DataUpdateFlag = false;
        }

        public void ReloadObjectPicker() {
            objectPickerControl1.ReRenderAll(0);
            objectPickerControl1.ReRenderAll(1);
            objectPickerControl1.ReRenderAll(2);
        }

        private void objXPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.X = (int)objXPosUpDown.Value;
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.Y = (int)objYPosUpDown.Value;
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objWidthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.Width = (int)objWidthUpDown.Value;
            o.UpdateObjCache();
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objHeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.Height = (int)objHeightUpDown.Value;
            o.UpdateObjCache();
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objTileset0Button_Click(object sender, EventArgs e)
        {
            SetTileset(0);
        }

        private void objTileset1Button_Click(object sender, EventArgs e)
        {
            SetTileset(1);
        }

        private void objTileset2Button_Click(object sender, EventArgs e)
        {
            SetTileset(2);
        }

        private void objTypeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.ObjNum = (int)objTypeUpDown.Value;
            o.UpdateObjCache();
            objectPickerControl1.SelectedObject = (int)objTypeUpDown.Value;
            objectPickerControl1.EnsureObjVisible((int)objTypeUpDown.Value);
            objectPickerControl1.Invalidate(true);
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objectPickerControl1_ObjectSelected()
        {
            DataUpdateFlag = true;
            objTypeUpDown.Value = objectPickerControl1.SelectedObject;
            o.ObjNum = objectPickerControl1.SelectedObject;
            o.UpdateObjCache();
            DataUpdateFlag = false;

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void addObjectButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;
            NSMBObject no = new NSMBObject(10, 0, ViewableArea.X, ViewableArea.Y, 1, 1, EdControl.GFX);
            EdControl.Level.Objects.Add(no);
            EdControl.SelectObject(no);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void deleteObjectButton_Click(object sender, EventArgs e)
        {
            EdControl.Level.Objects.Remove(o);
            EdControl.SelectObject(null);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }
    }
}
