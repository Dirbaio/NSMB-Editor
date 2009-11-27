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

            n.Unknown1 = (ushort)unk1.Value;
            n.Unknown2 = (ushort)unk2.Value;
            n.Unknown3 = (ushort)unk3.Value;
            n.Unknown4 = (ushort)unk4.Value;
            n.Unknown5 = (ushort)unk5.Value;
            n.Unknown6 = (ushort)unk6.Value;

            EdControl.FireSetDirtyFlag();
        }

        private void position_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            if (n == null) return;

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

            l.Add(np);
            UpdateList();
            EdControl.SelectObject(npp);
        }

        private void deletePath_Click(object sender, EventArgs e)
        {
            if (p == null)
                return;

            l.Remove(p);
            UpdateList();
            EdControl.SelectObject(null);
            EdControl.repaint();
        }
    }
}
