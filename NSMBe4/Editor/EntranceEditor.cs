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
            foreach (NSMBEntrance e in EdControl.Level.Entrances)
                entranceListBox.Items.Add(e.ToStringNormal());
            DataUpdateFlag = false;
        }

        private void entranceListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            DataUpdateFlag = true;
            List<LevelItem> ents = new List<LevelItem>();
            for (int l = 0; l < entranceListBox.SelectedIndices.Count; l++)
                ents.Add(EdControl.Level.Entrances[entranceListBox.SelectedIndices[l]]);
            if (ents.Count == 0)
                EdControl.SelectObject(null);
            else
            {
                EdControl.SelectObject(ents);
                EdControl.ScrollToObjects(ents);
            }
            DataUpdateFlag = false;
            EdControl.repaint();
        }

        private void entranceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceDataAction(SelectedObjects, 7, entranceTypeComboBox.SelectedIndex));
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

        private void entranceSetting128_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceBitAction(SelectedObjects, 0, exitOnly.Checked));
        }

        private void entranceSetting16_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceBitAction(SelectedObjects, 1, screenFade.Checked));
        }

        private void entranceSetting8_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceBitAction(SelectedObjects, 2, connectedPipe.Checked));
        }

        private void entranceSetting1_CheckedChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            EdControl.UndoManager.Do(new ChangeEntranceBitAction(SelectedObjects, 3, lowerScreen.Checked));
        }

        private void addEntranceButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableBlocks = EdControl.ViewableBlocks;

            NSMBEntrance ne = new NSMBEntrance();
            ne.X = (ViewableBlocks.X + ViewableBlocks.Width / 2) * 16;
            ne.Y = (ViewableBlocks.Y + ViewableBlocks.Height / 2) * 16;
            ne.Number = EdControl.Level.getFreeEntranceNumber();
            EdControl.UndoManager.Do(new AddLvlItemAction(UndoManager.ObjToList(ne)));
            EdControl.mode.SelectObject(ne);
        }


        private void deleteEntranceButton_Click(object sender, EventArgs e)
        {
            List<LevelItem> ents = new List<LevelItem>();
            foreach (LevelItem obj in SelectedObjects)
                if (obj is NSMBEntrance)
                    ents.Add(obj);
            foreach (LevelItem obj in ents)
                SelectedObjects.Remove(obj);
            EdControl.UndoManager.Do(new RemoveLvlItemAction(ents));
        }

        public void delete()
        {
            deleteEntranceButton.PerformClick();
        }

        public void UpdateInfo()
        {
            UpdateList();

            if (SelectedObjects == null || SelectedObjects.Count == 0)
            {
                tableLayoutPanel5.Visible = false;
                deleteEntranceButton.Enabled = false;
                return;
            }
            NSMBEntrance en = SelectedObjects[0] as NSMBEntrance;
            deleteEntranceButton.Enabled = true;
            tableLayoutPanel5.Visible = true;
            DataUpdateFlag = true;

            foreach (LevelItem obj in SelectedObjects)
                entranceListBox.SelectedIndices.Add(EdControl.Level.Entrances.IndexOf(obj as NSMBEntrance));
            entranceCameraXPosUpDown.Value = en.CameraX;
            entranceCameraYPosUpDown.Value = en.CameraY;
            entranceNumberUpDown.Value = en.Number;
            entranceDestAreaUpDown.Value = en.DestArea;
            entrancePipeIDUpDown.Value = en.ConnectedPipeID;
            entranceDestEntranceUpDown.Value = en.DestEntrance;
            entranceTypeComboBox.SelectedIndex = en.Type;
            exitOnly.Checked = (en.Settings & 128) != 0;
            screenFade.Checked = (en.Settings & 16) != 0;
            connectedPipe.Checked = (en.Settings & 8) != 0;
            lowerScreen.Checked = (en.Settings & 1) != 0;
            entranceViewUpDown.Value = en.EntryView;
            DataUpdateFlag = false;
        }
    }
}
