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
            if (DataUpdateFlag) return;
            if (n == null)
                return;
            ushort[] values = { (ushort)unk1.Value, (ushort)unk2.Value, (ushort)unk3.Value, (ushort)unk4.Value, (ushort)unk5.Value, (ushort)unk6.Value };
            if (n.Unknown1 != values[0])
                EdControl.UndoManager.PerformAction(UndoType.ChangePathNodeData, n, new Rectangle(n.Unknown1, values[0], 0, 0));
            if (n.Unknown2 != values[1])
                EdControl.UndoManager.PerformAction(UndoType.ChangePathNodeData, n, new Rectangle(n.Unknown2, values[1], 1, 0));
            if (n.Unknown3 != values[2])
                EdControl.UndoManager.PerformAction(UndoType.ChangePathNodeData, n, new Rectangle(n.Unknown3, values[2], 2, 0));
            if (n.Unknown4 != values[3])
                EdControl.UndoManager.PerformAction(UndoType.ChangePathNodeData, n, new Rectangle(n.Unknown4, values[3], 3, 0));
            if (n.Unknown5 != values[4])
                EdControl.UndoManager.PerformAction(UndoType.ChangePathNodeData, n, new Rectangle(n.Unknown5, values[4], 4, 0));
            if (n.Unknown6 != values[5])
                EdControl.UndoManager.PerformAction(UndoType.ChangePathNodeData, n, new Rectangle(n.Unknown6, values[5], 5, 0));
            n.Unknown1 = values[0];
            n.Unknown2 = values[1];
            n.Unknown3 = values[2];
            n.Unknown4 = values[3];
            n.Unknown5 = values[4];
            n.Unknown6 = values[5];

            EdControl.FireSetDirtyFlag();
        }

        private void position_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            if (n == null) return;

            EdControl.UndoManager.PerformAction(UndoType.MovePathNode, n, new Rectangle(n.X, n.Y, (ushort)nodeX.Value, (ushort)nodeY.Value));

            n.X = (ushort)nodeX.Value;
            n.Y = (ushort)nodeY.Value;

            EdControl.repaint();
            EdControl.FireSetDirtyFlag();
        }

        private void pathID_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            if (p == null)
                return;

            EdControl.UndoManager.PerformAction(UndoType.ChangePathID, p, new Point(p.id, (ushort)pathID.Value));

            p.id = (ushort) pathID.Value;

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
            EdControl.UndoManager.PerformAction(UndoType.AddPath, np, null);

            l.Add(np);
            UpdateList();
            EdControl.SelectObject(npp);
        }

        private void deletePath_Click(object sender, EventArgs e)
        {
            if (p == null)
                return;
            EdControl.UndoManager.PerformAction(UndoType.RemovePath, p, null);

            l.Remove(p);
            UpdateList();
            EdControl.SelectObject(null);
            EdControl.repaint();
        }

        private void clonePath_Click(object sender, EventArgs e)
        {
            NSMBPath np = new NSMBPath(p);
            l.Add(np);
            setNode(np, np.points[0]);
            EdControl.UndoManager.PerformAction(UndoType.AddPath, np, null);
            UpdateList();
        }
    }
}
