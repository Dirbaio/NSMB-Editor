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
    public partial class ViewEditor : UserControl
    {
        LevelEditorControl EdControl;
        List<NSMBView> l;
        NSMBView v;
        bool EditingViews, DataUpdateFlag;

        public ViewEditor(LevelEditorControl EdControl, List<NSMBView> l, bool EdVi)
        {
            InitializeComponent();
            this.EdControl = EdControl;
            this.l = l;
            EditingViews = EdVi;
            LanguageManager.ApplyToContainer(this, "ViewEditor");
            music.Items.AddRange(LanguageManager.GetList("Music").ToArray());
            UpdateList();
        }

        private void addViewButton_Click(object sender, EventArgs e)
        {
            NSMBView nv = new NSMBView();
            Rectangle va = EdControl.ViewableArea;
            nv.X = va.X * 16;
            nv.Y = va.Y * 16;
            nv.Height = 12 * 16;
            nv.Width = 16 * 16;
            nv.isZone = !EditingViews;
            EdControl.UndoManager.Do(new AddViewAction(nv));
        }

        private void deleteViewButton_Click(object sender, EventArgs e)
        {
            EdControl.UndoManager.Do(new RemoveViewAction(v));
        }

        public void delete()
        {
            deleteViewButton.PerformClick();
        }

        public void UpdateList()
        {
            viewsList.Items.Clear();
            viewsList.Items.AddRange(l.ToArray());
            viewsList.SelectedItem = v;
        }

        public void UpdateItem()
        {
            DataUpdateFlag = true;
            viewsList.SelectedItem = v;
            DataUpdateFlag = false;
            if (v == null)
                return;
            if (viewsList.Items.Contains(v))
                viewsList.Items[viewsList.Items.IndexOf(v)] = v;
        }

        public void UpdateInfo()
        {
            DataUpdateFlag = true;
            UpdateList();
            viewsList.SelectedItem = v;

            if (v != null)
            {
                xPos.Value = v.X;
                yPos.Value = v.Y;
                width.Value = v.Width;
                height.Value = v.Height;
                viewID.Value = v.Number;

                cameraID.Value = v.Camera;
                music.SelectedIndex = 0;
                for (int findmusic = 0; findmusic < music.Items.Count; findmusic++) {
                    int check = int.Parse((music.Items[findmusic] as string).Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                    if (check == v.Music) {
                        music.SelectedIndex = findmusic;
                        break;
                    }
                }
                unk1.Value = v.Unknown1;
                unk2.Value = v.Unknown2;
                unk3.Value = v.Unknown3;
                light.Value = v.Lighting;
                progressID.Value = v.FlagpoleID;
            }
            DataUpdateFlag = false;
        }

        public void SetView(NSMBView v)
        {
            this.v = v;
            UpdateInfo();
            tableLayoutPanel2.Visible = v != null;
            groupBox1.Visible = v != null && EditingViews;
        }

        private void position_ValueChanged(object sender, EventArgs e)
        {
            if (v == null || DataUpdateFlag)
                return;
            if ((int)xPos.Value != v.X || (int)yPos.Value != v.Y)
                EdControl.UndoManager.Do(new MoveViewAction(v, (int)xPos.Value, (int)yPos.Value));
            else if ((int)width.Value != v.Width || (int)height.Value != v.Height)
                EdControl.UndoManager.Do(new SizeViewAction(v, (int)width.Value, (int)height.Value));
            else if ((int)viewID.Value != v.Number)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 0, (int)viewID.Value));
            UpdateItem();
        }

        private void viewSettings_ValueChanged(object sender, EventArgs e)
        {
            if (v == null || DataUpdateFlag)
                return;
            if (v.Camera != (int)cameraID.Value)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 1, (int)cameraID.Value));
            v.Camera = (int)cameraID.Value;
            int newMusic = int.Parse((music.SelectedItem as string).Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            if (v.Music != newMusic)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 2, newMusic));
            if (v.Unknown1 != (int)unk1.Value)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 3, (int)unk1.Value));
            if (v.Unknown2 != (int)unk2.Value)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 4, (int)unk2.Value));
            if (v.Unknown3 != (int)unk3.Value)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 5, (int)unk3.Value));
            if (v.Lighting != (int)light.Value)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 6, (int)light.Value));
            if (v.FlagpoleID != (int)progressID.Value)
                EdControl.UndoManager.Do(new ChangeViewDataAction(v, 7, (int)progressID.Value));
        }

        private void viewsList_SelectedIndexChanged(object sender, EventArgs e) {
            if (DataUpdateFlag) return;

            EdControl.SelectObject(viewsList.SelectedItem);
            if(v != null)
                EdControl.EnsurePosVisible(v.X / 16, v.Y / 16);
        }
    }
}
