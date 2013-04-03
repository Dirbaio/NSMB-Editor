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
        List<NSMBView> lst;
        List<LevelItem> SelectedObjects;
        bool EditingViews, DataUpdateFlag;

        public ViewEditor(LevelEditorControl EdControl, List<NSMBView> l, bool EdVi)
        {
            InitializeComponent();
            this.EdControl = EdControl;
            this.lst = l;
            EditingViews = EdVi;
            LanguageManager.ApplyToContainer(this, "ViewEditor");
            music.Items.AddRange(ROM.UserInfo.getFullList("Music").ToArray());
            lightList.Items.AddRange(LanguageManager.GetList("3DLighting").ToArray());
            UpdateList();
        }

        public void SelectObjects(List<LevelItem> objs)
        {
            SelectedObjects = objs;
            UpdateInfo();
        }

        private void addViewButton_Click(object sender, EventArgs e)
        {
            NSMBView nv = new NSMBView();
            Rectangle va = EdControl.ViewableBlocks;
            nv.X = (va.X + (va.Width - 16) / 2) * 16;
            nv.Y = (va.Y + (va.Height - 12) / 2) * 16;
            nv.Height = 12 * 16;
            nv.Width = 16 * 16;
            nv.isZone = !EditingViews;
            nv.Number = EdControl.Level.getFreeViewNumber(lst);
            EdControl.UndoManager.Do(new AddLvlItemAction(UndoManager.ObjToList(nv)));
            EdControl.mode.SelectObject(nv);
        }

        private void deleteViewButton_Click(object sender, EventArgs e)
        {
            List<LevelItem> views = new List<LevelItem>();
            foreach (LevelItem obj in SelectedObjects)
                if (obj is NSMBView && (obj as NSMBView).isZone != EditingViews)
                    views.Add(obj);
            foreach (LevelItem obj in views)
                SelectedObjects.Remove(obj);
            EdControl.UndoManager.Do(new RemoveLvlItemAction(views));
        }

        public void delete()
        {
            deleteViewButton.PerformClick();
        }

        public void UpdateList()
        {
            DataUpdateFlag = true;
            viewsList.Items.Clear();
            foreach (NSMBView v in lst)
                viewsList.Items.Add(v.ToStringNormal());
            DataUpdateFlag = false;
        }

        public void UpdateInfo()
        {
            UpdateList();

            if (SelectedObjects == null || SelectedObjects.Count == 0)
            {
                panel2.Visible = false;
                tableLayoutPanel1.Visible = false;
                deleteViewButton.Enabled = false;
                return;
            }
            panel2.Visible = true;
            tableLayoutPanel1.Visible = EditingViews;
            deleteViewButton.Enabled = true;

            NSMBView v = SelectedObjects[0] as NSMBView;
            DataUpdateFlag = true;

            foreach (LevelItem obj in SelectedObjects)
                viewsList.SelectedIndices.Add(lst.IndexOf(obj as NSMBView));
            viewID.Value = v.Number;

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
            lightList.SelectedIndex = v.Lighting < lightList.Items.Count ? v.Lighting : -1;
            progressID.Value = v.FlagpoleID;

            camTop.Value = v.CameraTop;
            camTopSpecial.Value = v.CameraTopSpin;
            camBottom.Value = v.CameraBottom;
            camBottomSpecial.Value = v.CameraBottomSpin;
            scrollVertically.Checked = v.CameraBottomStick >= 15;
            DataUpdateFlag = false;
        }

        private void viewsList_SelectedIndexChanged(object sender, EventArgs e) {
            if (DataUpdateFlag) return;
            DataUpdateFlag = true;
            List<LevelItem> views = new List<LevelItem>();
            for (int l = 0; l < viewsList.SelectedIndices.Count; l++)
                views.Add(lst[viewsList.SelectedIndices[l]]);
            if (views.Count == 0)
                EdControl.SelectObject(null);
            else
            {
                EdControl.SelectObject(views);
                EdControl.ScrollToObjects(views);
            }
            DataUpdateFlag = false;
            EdControl.repaint();
        }

        private void viewID_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 1, (int)viewID.Value));
        }

        private void music_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            int newMusic = int.Parse((music.SelectedItem as string).Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 2, newMusic));
        }

        private void unk1_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 3, (int)unk1.Value));
        }

        private void unk2_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 4, (int)unk2.Value));
        }

        private void unk3_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 5, (int)unk3.Value));
        }

        private void lightList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 6, lightList.SelectedIndex));
        }

        private void progressID_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 7, (int)progressID.Value));
        }

        private void camTop_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 8, (int)camTop.Value));
        }

        private void camBottom_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 9, (int)camBottom.Value));
        }

        private void camTopSpecial_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 10, (int)camTopSpecial.Value));
        }

        private void camBottomSpecial_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 11, (int)camBottomSpecial.Value));
        }

        private void scrollVertically_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeViewDataAction(SelectedObjects, 12, scrollVertically.Checked ? 15 : 0));
        }

        private void selectContents_Click(object sender, EventArgs e)
        {
            List<LevelItem> selObjs2 = new List<LevelItem>();
            selObjs2.AddRange(SelectedObjects);
            foreach (LevelItem obj in selObjs2)
                if (obj is NSMBView && (obj as NSMBView).isZone != EditingViews) {
                    if (EdControl.mode is ObjectsEditionMode)
                        (EdControl.mode as ObjectsEditionMode).findSelectedObjects(obj.x, obj.y, obj.x + obj.width, obj.y + obj.height, false, false);
                }
        }
    }
}
