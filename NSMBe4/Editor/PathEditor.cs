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
    public partial class PathEditor : UserControl
    {
        private LevelEditorControl EdControl;
        private NSMBPath p;
        private NSMBPathPoint n;
        private bool DataUpdateFlag = false;
        List<NSMBPath> l;

        public PathEditor(LevelEditorControl EdControl, List<NSMBPath> l)
        {
            this.l = l;
            InitializeComponent();
            LanguageManager.ApplyToContainer(this, "PathEditor");
            this.EdControl = EdControl;
            UpdateList();
        }

        public void UpdateList()
        {
            DataUpdateFlag = true;
            pathsList.Items.Clear();
            pathsList.Items.AddRange(l.ToArray());
            pathsList.SelectedItem = p;
            DataUpdateFlag = false;
        }

        public void UpdateItem()
        {
            pathsList.SelectedItem = p;
            if (p == null)
                return;
            if(pathsList.Items.Contains(p))
                pathsList.Items[pathsList.Items.IndexOf(p)] = p;
        }

        public void UpdateInfo()
        {
            DataUpdateFlag = true;
            UpdateItem();
            deletePath.Enabled = p != null;
            clonePath.Enabled = deletePath.Enabled;

            groupBox1.Visible = p != null;
            if (p != null)
                pathID.Value = p.id;

            groupBox2.Visible = n != null;
            if (n != null)
            {
                nodeX.Value = n.X;
                nodeY.Value = n.Y;
                unk1.Value = n.Unknown1;
                unk2.Value = n.Unknown2;
                unk3.Value = n.Unknown3;
                unk4.Value = n.Unknown4;
                unk5.Value = n.Unknown5;
                unk6.Value = n.Unknown6;
            }
            DataUpdateFlag = false;
        }

        private void data_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || n == null) return;
            if (n.Unknown1 != (int)unk1.Value)
                EdControl.UndoManager.Do(new ChangePathNodeData(n, 0, (int)unk1.Value));
            if (n.Unknown2 != (int)unk2.Value)
                EdControl.UndoManager.Do(new ChangePathNodeData(n, 1, (int)unk2.Value));
            if (n.Unknown3 != (int)unk3.Value)
                EdControl.UndoManager.Do(new ChangePathNodeData(n, 2, (int)unk3.Value));
            if (n.Unknown4 != (int)unk4.Value)
                EdControl.UndoManager.Do(new ChangePathNodeData(n, 3, (int)unk4.Value));
            if (n.Unknown5 != (int)unk5.Value)
                EdControl.UndoManager.Do(new ChangePathNodeData(n, 4, (int)unk5.Value));
            if (n.Unknown6 != (int)unk6.Value)
                EdControl.UndoManager.Do(new ChangePathNodeData(n, 5, (int)unk6.Value));
        }

        private void position_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || n == null) return;
            EdControl.UndoManager.Do(new MovePathNodeAction(n, (int)nodeX.Value, (int)nodeY.Value));
        }

        private void pathID_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || n == null) return;
            EdControl.UndoManager.Do(new ChangePathIDAction(p, (int)pathID.Value));
            UpdateItem();
        }

        private void pathsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            if(pathsList.SelectedItem != null)
                setNode(pathsList.SelectedItem as NSMBPath, ((NSMBPath)pathsList.SelectedItem).points[0]);
        }

        public void setNode(NSMBPath np, NSMBPathPoint nn)
        {
            this.p = np;
            this.n = nn;
            if (nn != null) EdControl.EnsurePosVisible(nn.X / 16, nn.Y / 16);

            UpdateInfo();
        }

        private void addPath_Click(object sender, EventArgs e)
        {
            Rectangle va = EdControl.ViewableArea;
            NSMBPath np = new NSMBPath();
            NSMBPathPoint npp = new NSMBPathPoint(np);
            npp.X = va.X * 16;
            npp.Y = va.Y * 16;
            np.points.Add(npp);
            EdControl.UndoManager.Do(new AddPathAction(np));
        }

        private void deletePath_Click(object sender, EventArgs e)
        {
            if (p == null) return;
            EdControl.UndoManager.Do(new RemovePathAction(p));
        }

        private void clonePath_Click(object sender, EventArgs e)
        {
            NSMBPath np = new NSMBPath(p);
            EdControl.UndoManager.Do(new AddPathAction(np));
        }
    }
}
