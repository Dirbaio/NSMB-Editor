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
        NSMBEntrance en;
        LevelEditorControl EdControl;
        bool DataUpdateFlag = false;

        public EntranceEditor(NSMBEntrance e, LevelEditorControl ec)
        {
            InitializeComponent();
            en = e;
            EdControl = ec;
            UpdateList();
            if (entranceTypeComboBox.Items.Count == 0)
                entranceTypeComboBox.Items.AddRange(LanguageManager.GetList("EntranceTypes").ToArray());

            deleteEntranceButton.Enabled = false;
            LanguageManager.ApplyToContainer(this, "EntranceEditor");
        }

        public void UpdateList()
        {
            DataUpdateFlag = true;
            entranceListBox.Items.Clear();
            entranceListBox.Items.AddRange(EdControl.Level.Entrances.ToArray());
            entranceListBox.SelectedItem = en;
            DataUpdateFlag = false;
        }

        public void UpdateItem()
        {
            entranceListBox.SelectedItem = en;
            if (en == null)
                return;
            if (entranceListBox.Items.Contains(en))
                entranceListBox.Items[entranceListBox.Items.IndexOf(en)] = en;
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
            if ((int)entranceXPosUpDown.Value != en.X)
                EdControl.UndoManager.PerformAction(UndoType.MoveEntrance, en, new Rectangle(en.X, en.Y, (int)entranceXPosUpDown.Value, en.Y));

            en.X = (int)entranceXPosUpDown.Value;

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
            UpdateItem();
        }

        private void entranceYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            if ((int)entranceYPosUpDown.Value != en.Y)
                EdControl.UndoManager.PerformAction(UndoType.MoveEntrance, en, new Rectangle(en.X, en.Y, en.X, (int)entranceYPosUpDown.Value));
            en.Y = (int)entranceYPosUpDown.Value;

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
            UpdateItem();
        }

        private void entranceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.Type, entranceTypeComboBox.SelectedIndex, 7, 0));
            en.Type = entranceTypeComboBox.SelectedIndex;

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
            UpdateItem();
        }

        private void entranceCameraXPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.CameraX, (int)entranceCameraXPosUpDown.Value, 0, 0));
            en.CameraX = (int)entranceCameraXPosUpDown.Value;

            EdControl.FireSetDirtyFlag();
        }

        private void entranceCameraYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.CameraY, (int)entranceCameraYPosUpDown.Value, 1, 0));
            en.CameraY = (int)entranceCameraYPosUpDown.Value;
            EdControl.FireSetDirtyFlag();
        }

        private void entranceNumberUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.Number, (int)entranceNumberUpDown.Value, 2, 0)); 
            en.Number = (int)entranceNumberUpDown.Value;
            UpdateItem();

            EdControl.FireSetDirtyFlag();
        }

        private void entranceDestAreaUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.DestArea, (int)entranceDestAreaUpDown.Value, 3, 0));
            en.DestArea = (int)entranceDestAreaUpDown.Value;
            EdControl.FireSetDirtyFlag();
        }

        private void entranceDestEntranceUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.DestEntrance, (int)entranceDestEntranceUpDown.Value, 4, 0));
            en.DestEntrance = (int)entranceDestEntranceUpDown.Value;
            EdControl.FireSetDirtyFlag();
        }

        private void entrancePipeIDUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.ConnectedPipeID, (int)entrancePipeIDUpDown.Value, 5, 0));
            en.ConnectedPipeID = (int)entrancePipeIDUpDown.Value;
            EdControl.FireSetDirtyFlag();
        }

        private void entranceViewUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(en.EntryView, (int)entranceViewUpDown.Value, 6, 0));
            en.EntryView = (int)entranceViewUpDown.Value;
            EdControl.FireSetDirtyFlag();
        }

        private void entranceSetting128_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            int pval = en.Settings;
            if (entranceSetting128.Checked)
                en.Settings |= 128;
            else
                en.Settings &= 127;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(pval, en.Settings, 8, 0));
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void entranceSetting16_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            int pval = en.Settings;
            if (entranceSetting16.Checked)
                en.Settings |= 16;
            else
                en.Settings &= 239;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(pval, en.Settings, 8, 1));
            EdControl.FireSetDirtyFlag();
        }

        private void entranceSetting8_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            int pval = en.Settings;
            if (entranceSetting8.Checked)
                en.Settings |= 8;
            else
                en.Settings &= 247;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(pval, en.Settings, 8, 2));
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void entranceSetting1_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            int pval = en.Settings;
            if (entranceSetting1.Checked)
                en.Settings |= 1;
            else
                en.Settings &= 254;
            EdControl.UndoManager.PerformAction(UndoType.ChangeEntranceData, en, new Rectangle(pval, en.Settings, 8, 3));
            EdControl.FireSetDirtyFlag();
        }

        private void addEntranceButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;

            NSMBEntrance ne = new NSMBEntrance();
            EdControl.UndoManager.PerformAction(UndoType.AddEntrance, ne, null);
            ne.X = ViewableArea.X * 16;
            ne.Y = ViewableArea.Y * 16;
            ne.Number = EdControl.Level.getFreeEntranceNumber();
            EdControl.Level.Entrances.Add(ne);
            entranceListBox.Items.Add(ne);

            UpdateList();
            EdControl.SelectObject(ne);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }


        private void deleteEntranceButton_Click(object sender, EventArgs e)
        {
            EdControl.UndoManager.PerformAction(UndoType.RemoveEntrance, en, EdControl.Level.Entrances.IndexOf(en));
            EdControl.Level.Entrances.Remove(en);
            entranceListBox.Items.Remove(en);
            entranceListBox.SelectedIndex = -1;
            UpdateList();
            EdControl.SelectObject(null);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        public void SetEntrance(NSMBEntrance ne)
        {
            this.en = ne;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            DataUpdateFlag = true;
            groupBox2.Visible = en != null;
            entranceListBox.SelectedItem = en;
            deleteEntranceButton.Enabled = en != null;
            DataUpdateFlag = false;

            if (en == null) return;

            DataUpdateFlag = true;
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
