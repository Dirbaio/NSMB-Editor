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
    public partial class SpriteEditor : UserControl
    {
        List<LevelItem> SelectedObjects;
        private LevelEditorControl EdControl;
        private byte[] SSTable;
        private bool updating = false;

        public string[] allSprites = new string[ROM.SpriteCount];
        private List<int> curSprites = new List<int>();

        public SpriteEditor(LevelEditorControl EdControl)
        {
            InitializeComponent();
            this.EdControl = EdControl;

            SSTable = ROM.GetInlineFile(ROM.Data.File_Modifiers);

            string[] spritelist = new string[ROM.SpriteCount];
            int i = 0;
            foreach (string sprite in SpriteData.spriteNames)
            {
                spritelist[i] = i + ": " + sprite;
                i++;
            }
            if (SpriteData.spriteNames.Count == 0)
            {
                for (int s = 0; s < ROM.SpriteCount; s++)
                    spritelist[s] = "Sprite " + s;
            }

            spriteListBox.Items.AddRange(spritelist);
            spriteListBox.Items.CopyTo(allSprites, 0);
            for (int l = 0; l < ROM.SpriteCount; l++)
                curSprites.Add(l);

            UpdateDataEditor();
            UpdateInfo();

            LanguageManager.ApplyToContainer(this, "SpriteEditor");
            spriteTypeUpDown.Maximum = ROM.SpriteCount - 1;
        }

        private SpriteData.SpriteDataEditor sed;

        public void SelectObjects(List<LevelItem> objs)
        {
            SelectedObjects = objs;
            UpdateInfo();
            UpdateDataEditor();
        }

        public void UpdateDataEditor()
        {
            if (sed != null)
            {
                sed.saveData(null, null);
                sed.Parent = null;
            }
            sed = null;
            if (SelectedObjects == null) return;

            int type = getSpriteType();

            if (type != -1 && SpriteData.datas.ContainsKey(type))
            {
                sed = new SpriteData.SpriteDataEditor(SelectedObjects, SpriteData.datas[type], EdControl);
                spriteDataPanel.Controls.Add(sed);
                sed.Dock = DockStyle.Fill;
                //sed.Parent = spriteDataPanel;
                spriteDataPanel.Visible = true;
            }
            else
            {
                spriteDataPanel.Visible = false;
            }
        }

        public void RefreshDataEditor()
        {
            if (sed != null)
                sed.UpdateData();
        }

        public void UpdateInfo()
        {
            if (SelectedObjects == null || SelectedObjects.Count == 0)
            {
                lblSelectSomething.Visible = true;
                panel3.Visible = false;
                spriteDataPanel.Visible = false;
                tableLayoutPanel1.Visible = false;
                spriteListBox.Visible = false;
                tableLayoutPanel2.Visible = false;
                return;
            }
            lblSelectSomething.Visible = false;
            panel3.Visible = true;
            spriteDataPanel.Visible = true;
            tableLayoutPanel1.Visible = true;
            spriteListBox.Visible = true;
            tableLayoutPanel2.Visible = true;
            updating = true;
            int type = getSpriteType();
            spriteTypeUpDown.Value = type > -1 ? type : 0;
            byte[] SpriteData = null;
            foreach (LevelItem obj in SelectedObjects)
                if (obj is NSMBSprite) {
                    SpriteData = (obj as NSMBSprite).Data;
                    break;
                }
            if (SpriteData != null) {
                spriteDataTextBox.Text = String.Format(
                    "{0:X2} {1:X2} {2:X2} {3:X2} {4:X2} {5:X2}",
                    SpriteData[0], SpriteData[1], SpriteData[2],
                    SpriteData[3], SpriteData[4], SpriteData[5]);
                spriteDataTextBox.BackColor = SystemColors.Window;
            }
            spriteListBox.SelectedIndex = curSprites.IndexOf(type);
            updating = false;
        }

        private void spriteTypeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (updating) return;
            EdControl.UndoManager.Do(new ChangeSpriteTypeAction(SelectedObjects, (int)spriteTypeUpDown.Value));
        }

        private void spriteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updating) return;
            if (spriteListBox.SelectedIndex > -1)
                spriteTypeUpDown.Value = curSprites[spriteListBox.SelectedIndex];
        }

        private void addSpriteButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableBlocks = EdControl.ViewableBlocks;
            NSMBSprite ns = new NSMBSprite(EdControl.Level);
            ns.X = ViewableBlocks.X + ViewableBlocks.Width / 2;
            ns.Y = ViewableBlocks.Y + ViewableBlocks.Height / 2;
            ns.Type = 0;
            ns.Data = new byte[6];
            EdControl.UndoManager.Do(new AddLvlItemAction(UndoManager.ObjToList(ns)));
            EdControl.mode.SelectObject(ns);
        }

        private void deleteSpriteButton_Click(object sender, EventArgs e)
        {
            List<LevelItem> sprites = new List<LevelItem>();
            foreach (LevelItem obj in SelectedObjects)
                if (obj is NSMBSprite)
                    sprites.Add(obj as NSMBSprite);
            foreach (LevelItem obj in sprites)
                SelectedObjects.Remove(obj);
            EdControl.UndoManager.Do(new RemoveLvlItemAction(sprites));
        }

        private void spriteListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Color TextColor, BackColor = e.BackColor;
            if (spriteListBox.Items.Count > 0 && e.Index > -1) {
                if (EdControl.Level.ValidSprites[curSprites[e.Index]]) {
                    TextColor = e.ForeColor;
                } else {
                    TextColor = Color.DarkRed;
                    if ((e.State & DrawItemState.Selected) != DrawItemState.None) {
                        TextColor = Color.White;
                        BackColor = Color.DarkRed;
                        SolidBrush b = new SolidBrush(BackColor);
                        e.Graphics.FillRectangle(b, e.Bounds);
                        b.Dispose();
                    }
                }

                TextRenderer.DrawText(e.Graphics, (string)spriteListBox.Items[e.Index], spriteListBox.Font, e.Bounds, TextColor, BackColor, TextFormatFlags.Left);

                int SSNumber = SSTable[curSprites[e.Index] << 1];
                int SSValue = SSTable[(curSprites[e.Index] << 1) + 1];
                string txt = (SSNumber + 1) + "-" + SSValue;
                if (SSValue == 0)
                    txt = "-";

                TextRenderer.DrawText(e.Graphics, txt, spriteListBox.Font, new Rectangle(e.Bounds.X +e.Bounds.Width - 30, e.Bounds.Y, 30, e.Bounds.Height), TextColor, BackColor, TextFormatFlags.Right);
            }
        }

        private void spriteDataTextBox_TextChanged(object sender, EventArgs e)
        {
            if (updating || !spriteDataTextBox.Visible)
                return;

            // validate
            if (System.Text.RegularExpressions.Regex.IsMatch(
                spriteDataTextBox.Text,
                "^[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                string parseit = spriteDataTextBox.Text.Replace(" ", "");
                byte[] data = new byte[6];
                for (int hexidx = 0; hexidx < 6; hexidx++)
                    data[hexidx] = byte.Parse(parseit.Substring(hexidx*2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                if (!updating)
                    EdControl.UndoManager.Do(new ChangeSpriteDataAction(SelectedObjects, data));
                spriteDataTextBox.BackColor = SystemColors.Window;
            }
            else
                spriteDataTextBox.BackColor = Color.Coral;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            curSprites.Clear();
            for (int l = 0; l < allSprites.Length; l++) {
                if (allSprites[l].ToLowerInvariant().Contains(searchBox.Text.ToLowerInvariant()))
                    curSprites.Add(l);
            }
            spriteListBox.Items.Clear();
            List<string> items = new List<string>();
            for (int l = 0; l < curSprites.Count; l++)
                items.Add(allSprites[curSprites[l]]);
            spriteListBox.Items.AddRange(items.ToArray());
            spriteListBox.SelectedIndex = curSprites.IndexOf(getSpriteType());
            if (curSprites.Count > 0)
                searchBox.BackColor = SystemColors.Window;
            else
                searchBox.BackColor = Color.Coral;
        }

        private int getSpriteType()
        {
            int type = -1;
            foreach (LevelItem obj in SelectedObjects)
                if (obj is NSMBSprite)
                {
                    NSMBSprite s = obj as NSMBSprite;
                    if (type == -1) type = s.Type;
                    if (type != s.Type) return -1;
                }
            return type;
        }

        private void clearSearch_Click(object sender, EventArgs e)
        {
            searchBox.Text = "";
        }

        private void clearSpriteData_Click(object sender, EventArgs e)
        {
            byte[] emptyData = new byte[6];
            EdControl.UndoManager.Do(new ChangeSpriteDataAction(SelectedObjects, emptyData));
        }
    }
}
