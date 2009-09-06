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

            l.Add(nv);
            EdControl.FireSetDirtyFlag();
            EdControl.SelectObject(nv);
            UpdateList();
        }

        private void deleteViewButton_Click(object sender, EventArgs e)
        {
            l.Remove(v);
            EdControl.SelectObject(null);
            EdControl.FireSetDirtyFlag();
            UpdateList();
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
            viewsList.SelectedItem = v;

            if (v != null)
            {
                xPos.Value = v.X;
                yPos.Value = v.Y;
                width.Value = v.Width;
                height.Value = v.Height;

                cameraID.Value = v.Camera;
                music.SelectedIndex = v.Music;
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

            v.X = (int)xPos.Value;
            v.Y = (int)yPos.Value;
            v.Width = (int)width.Value;
            v.Height = (int)height.Value;
            v.Number = (int)viewID.Value;
            EdControl.FireSetDirtyFlag();
            EdControl.repaint();

            UpdateItem();
        }

        private void viewSettings_ValueChanged(object sender, EventArgs e)
        {
            if (v == null || DataUpdateFlag)
                return;

            v.Camera = (int)cameraID.Value;
            v.Music = (int)music.SelectedIndex;
            v.Unknown1 = (int)unk1.Value;
            v.Unknown2 = (int)unk2.Value;
            v.Unknown3 = (int)unk3.Value;
            v.Lighting = (int)light.Value;
            v.FlagpoleID = (int)progressID.Value;
            EdControl.FireSetDirtyFlag();
        }

        private void viewsList_SelectedIndexChanged(object sender, EventArgs e) {
            if (DataUpdateFlag) return;

            SetView((NSMBView)viewsList.SelectedItem);
            EdControl.EnsurePosVisible(v.X / 16, v.Y / 16);
        }
    }
}
