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
            if (nv.isZone)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddZone, nv, null);
            else
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.AddView, nv, null);

            l.Add(nv);
            EdControl.FireSetDirtyFlag();
            EdControl.SelectObject(nv);
            UpdateList();
        }

        private void deleteViewButton_Click(object sender, EventArgs e)
        {
            if (v.isZone)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.RemoveZone, v, EdControl.Level.Zones.IndexOf(v));
            else
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.RemoveView, v, EdControl.Level.Views.IndexOf(v));
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
            if ((int)xPos.Value != v.X || (int)yPos.Value != v.Y) {
                if (v.isZone)
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.MoveZone, v, new Rectangle(v.X, v.Y, (int)xPos.Value, (int)yPos.Value));
                else
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.MoveView, v, new Rectangle(v.X, v.Y, (int)xPos.Value, (int)yPos.Value));
            } else if ((int)width.Value != v.Width || (int)height.Value != v.Height) {
                if (v.isZone)
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.SizeZone, v, new Rectangle(v.Width, v.Height, (int)width.Value, (int)height.Value));
                else
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.SizeView, v, new Rectangle(v.Width, v.Height, (int)width.Value, (int)height.Value));
            } else if ((int)viewID.Value != v.Number) {
                if (v.isZone)
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeZoneID, v, new Point(v.Number, (int)viewID.Value));
                else
                    EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.Number, (int)viewID.Value, 0, 0));
            }


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
            if (v.Camera != (int)cameraID.Value)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.Camera, (int)cameraID.Value, 1, 0));
            v.Camera = (int)cameraID.Value;
            int newMusic = int.Parse((music.SelectedItem as string).Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
            if (v.Music != newMusic)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.Music, newMusic, 2, 0));
            v.Music = newMusic;
            if (v.Unknown1 != (int)unk1.Value)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.Unknown1, (int)unk1.Value, 3, 0));
            v.Unknown1 = (int)unk1.Value;
            if (v.Unknown2 != (int)unk2.Value)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.Unknown2, (int)unk2.Value, 4, 0));
            v.Unknown2 = (int)unk2.Value;
            if (v.Unknown3 != (int)unk3.Value)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.Unknown3, (int)unk3.Value, 5, 0));
            v.Unknown3 = (int)unk3.Value;
            if (v.Lighting != (int)light.Value)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.Lighting, (int)light.Value, 6, 0));
            v.Lighting = (int)light.Value;
            if (v.FlagpoleID != (int)progressID.Value)
                EdControl.editor.undoMngr.PerformAction(NSMBe4.Editor.UndoType.ChangeViewData, v, new Rectangle(v.FlagpoleID, (int)progressID.Value, 7, 0));
            v.FlagpoleID = (int)progressID.Value;
            EdControl.FireSetDirtyFlag();
        }

        private void viewsList_SelectedIndexChanged(object sender, EventArgs e) {
            if (DataUpdateFlag) return;

            EdControl.SelectObject(viewsList.SelectedItem);
            if(v != null)
                EdControl.EnsurePosVisible(v.X / 16, v.Y / 16);
        }
    }
}
