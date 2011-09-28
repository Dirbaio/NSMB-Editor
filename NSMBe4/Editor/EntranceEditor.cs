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
    public partial class EntranceEditor : UserControl
    {
        List<LevelItem> SelectedObjects;
        LevelEditorControl EdControl;
        bool DataUpdateFlag = false;

        public EntranceEditor(LevelEditorControl ec)
        {
            InitializeComponent();
            EdControl = ec;
            UpdateList();
            if (entranceTypeComboBox.Items.Count == 0)
                entranceTypeComboBox.Items.AddRange(LanguageManager.GetList("EntranceTypes").ToArray());

            deleteEntranceButton.Enabled = false;
            LanguageManager.ApplyToContainer(this, "EntranceEditor");
        }

        public void SelectObjects(List<LevelItem> objs)
        {
            SelectedObjects = objs;
            UpdateInfo();
        }

        public void UpdateList()
        {
            DataUpdateFlag = true;
            entranceListBox.Items.Clear();
            entranceListBox.Items.AddRange(EdControl.Level.Entrances.ToArray());
            DataUpdateFlag = false;
        }

        public void UpdateItem()
        {
            //entranceListBox.SelectedItem = en;
            //if (en == null)
            //    return;
            //if (entranceListBox.Items.Contains(en))
            //    entranceListBox.Items[entranceListBox.Items.IndexOf(en)] = en;
        }

        private void entranceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            DataUpdateFlag = true;
            int ind = entranceListBox.SelectedIndex;
            if (ind == -1)
                EdControl.SelectObject(null);
            else
            {
                NSMBEntrance ent = EdControl.Level.Entrances[ind];
                EdControl.SelectObject(ent);
                EdControl.EnsurePosVisible(ent.X/16, ent.Y/16);
            }
            DataUpdateFlag = false;
        }

        private void entranceXPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //if ((int)entranceXPosUpDown.Value != en.X)
                //EdControl.UndoManager.Do(new MoveLvlItemAction(UndoManager.ObjToList(en), (int)entranceXPosUpDown.Value - en.X, 0));
            UpdateItem();
        }

        private void entranceYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //if ((int)entranceYPosUpDown.Value != en.Y)
                //EdControl.UndoManager.Do(new MoveLvlItemAction(UndoManager.ObjToList(en), 0, (int)entranceYPosUpDown.Value - en.Y));
            UpdateItem();
        }

        private void entranceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 7, entranceTypeComboBox.SelectedIndex));

            UpdateItem();
        }

        private void entranceCameraXPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 0, (int)entranceCameraXPosUpDown.Value));
        }

        private void entranceCameraYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 1, (int)entranceCameraYPosUpDown.Value));
        }

        private void entranceNumberUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 2, (int)entranceNumberUpDown.Value));
            UpdateItem();
        }

        private void entranceDestAreaUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 3, (int)entranceDestAreaUpDown.Value));
        }

        private void entranceDestEntranceUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 4, (int)entranceDestEntranceUpDown.Value));
        }

        private void entrancePipeIDUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 5, (int)entrancePipeIDUpDown.Value));
        }

        private void entranceViewUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 6, (int)entranceViewUpDown.Value));
        }

        // I have no clue how to handle these -Piranhaplant
        private void entranceSetting128_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //int v = en.Settings;
            //if (entranceSetting128.Checked)
            //    v |= 128;
            //else
            //    v &= 127;
            //EdControl.UndoManager.Do(new ChangeEntranceDataAction(en, 8, v));
        }

        private void entranceSetting16_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //int v = en.Settings;
            //if (entranceSetting16.Checked)
            //    v |= 16;
            //else
            //    v &= 239;
            //EdControl.UndoManager.Do(new ChangeEntranceDataAction(en, 9, v));
        }

        private void entranceSetting8_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //int v = en.Settings;
            //if (entranceSetting8.Checked)
            //    v |= 8;
            //else
            //    v &= 247;
            //EdControl.UndoManager.Do(new ChangeEntranceDataAction(en, 10, v));
        }

        private void entranceSetting1_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            //int v = en.Settings;
            //if (entranceSetting1.Checked)
            //    v |= 1;
            //else
            //    v &= 254;
            //EdControl.UndoManager.Do(new ChangeEntranceDataAction(en, 11, v));
        }

        private void addEntranceButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;

            NSMBEntrance ne = new NSMBEntrance();
            ne.X = ViewableArea.X * 16;
            ne.Y = ViewableArea.Y * 16;
            ne.Number = EdControl.Level.getFreeEntranceNumber();
            EdControl.UndoManager.Do(new AddLvlItemAction(UndoManager.ObjToList(ne)));
        }


        private void deleteEntranceButton_Click(object sender, EventArgs e)
        {
            // This button will probably be removed.

            //int selIdx = entranceListBox.SelectedIndex;
            EdControl.UndoManager.Do(new RemoveLvlItemAction(SelectedObjects));
            //entranceListBox.SelectedIndex = Math.Min(selIdx, entranceListBox.Items.Count - 1);
        }

        public void delete()
        {
            deleteEntranceButton.PerformClick();
        }

        public void UpdateInfo()
        {
            DataUpdateFlag = true;
            NSMBEntrance en = null;
            groupBox2.Visible = SelectedObjects != null;
            deleteEntranceButton.Enabled = SelectedObjects != null;
            UpdateList();
            DataUpdateFlag = false;

            if (SelectedObjects == null) return;
            foreach (LevelItem obj in SelectedObjects)
                if (obj is NSMBEntrance) {
                    en = obj as NSMBEntrance;
                    break;
                }
            deleteEntranceButton.Enabled = en != null;
            if (en == null) return;

            DataUpdateFlag = true;
            entranceListBox.SelectedItem = en;
            entranceXPosUpDown.Value = en.X;
            entranceYPosUpDown.Value = en.Y;
            entranceCameraXPosUpDown.Value = en.CameraX;
            entranceCameraYPosUpDown.Value = en.CameraY;
            entranceNumberUpDown.Value = en.Number;
            entranceDestAreaUpDown.Value = en.DestArea;
            entrancePipeIDUpDown.Value = en.ConnectedPipeID;
            entranceDestEntranceUpDown.Value = en.DestEntrance;
            entranceTypeComboBox.SelectedIndex = en.Type;
            entranceSetting128.Checked = (bool)((en.Settings & 128) != 0);
            entranceSetting16.Checked = (bool)((en.Settings & 16) != 0);
            entranceSetting8.Checked = (bool)((en.Settings & 8) != 0);
            entranceSetting1.Checked = (bool)((en.Settings & 1) != 0);
            entranceViewUpDown.Value = en.EntryView;
            DataUpdateFlag = false;
        }
    }
}
