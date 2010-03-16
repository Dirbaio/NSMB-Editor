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
        private NSMBSprite s;
        private LevelEditorControl EdControl;
        private bool DataUpdateFlag = false;
        private byte[] SSTable;

        private string[] allSprites = new string[324];
        private List<int> curSprites = new List<int>();

        public SpriteEditor(NSMBSprite s, LevelEditorControl EdControl)
        {
            InitializeComponent();
            this.s = s;
            this.EdControl = EdControl;

            SSTable = ROM.GetInlineFile(ROM.Data.File_Modifiers);

            string[] spritelist = new string[324];
            foreach (string sprite in LanguageManager.GetList("Sprites"))
            {
                string trimmedsprite = sprite.Trim();
                if (trimmedsprite == "") continue;
                int equalPos = trimmedsprite.IndexOf('=');
                spritelist[int.Parse(trimmedsprite.Substring(0, equalPos))] = trimmedsprite.Substring(0, equalPos) + ": " + trimmedsprite.Substring(equalPos + 1);
            }

            spriteListBox.Items.AddRange(spritelist);
            spriteListBox.Items.CopyTo(allSprites, 0);
            for (int l = 0; l <= 323; l++)
                curSprites.Add(l);

            UpdateInfo();

            LanguageManager.ApplyToContainer(this, "SpriteEditor");
        }

        private SpriteData.SpriteDataEditor sed;

        public void SetSprite(NSMBSprite ns)
        {
            if (ns == s)
            {
                UpdateInfo();
                return;
            }

            this.s = ns;

            UpdateDataEditor();
            UpdateInfo();
        }

        private void UpdateDataEditor()
        {
            if (sed != null)
            {
                sed.saveData(null, null);
                spriteDataTextBox.Focus();
                sed.Parent = null;
            }
            sed = null;
            if (SpriteData.datas.ContainsKey(s.Type))
            {
                sed = new SpriteData.SpriteDataEditor(s.Data, SpriteData.datas[s.Type], EdControl);
                sed.Parent = spriteDataPanel;
                spriteDataPanel.Visible = true;
                rawSpriteData.Visible = false;
            }
            else
            {
                spriteDataPanel.Visible = false;
                rawSpriteData.Visible = true;
            }
        }

        public void UpdateInfo()
        {
            if (s == null) return;

            spriteXPosUpDown.Value = s.X;
            spriteYPosUpDown.Value = s.Y;
            spriteTypeUpDown.Value = s.Type;

            byte[] SpriteData = s.Data;
            spriteDataTextBox.Text = String.Format(
                "{0:X2} {1:X2} {2:X2} {3:X2} {4:X2} {5:X2}",
                SpriteData[0], SpriteData[1], SpriteData[2],
                SpriteData[3], SpriteData[4], SpriteData[5]);
            spriteDataTextBox.BackColor = SystemColors.Window;

            spriteListBox.SelectedIndex = curSprites.IndexOf(s.Type);

        }
        private void spriteXPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            s.X = (int)spriteXPosUpDown.Value;
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void spriteYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            s.Y = (int)spriteYPosUpDown.Value;
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void spriteTypeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            s.Type = (int)spriteTypeUpDown.Value;
            DataUpdateFlag = true;
            spriteListBox.SelectedIndex = curSprites.IndexOf(s.Type);
            DataUpdateFlag = false;
            UpdateDataEditor();
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void spriteListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            if (spriteListBox.SelectedIndex > -1 && curSprites[spriteListBox.SelectedIndex] != s.Type)
            {
                s.Type = curSprites[spriteListBox.SelectedIndex];
                DataUpdateFlag = true;
                spriteTypeUpDown.Value = s.Type;
                DataUpdateFlag = false;
                UpdateDataEditor();
                EdControl.Invalidate(true);
                EdControl.FireSetDirtyFlag();
            }
        }

        private void addSpriteButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;
            NSMBSprite ns = new NSMBSprite(EdControl.Level);
            ns.X = ViewableArea.X;
            ns.Y = ViewableArea.Y;
            ns.Type = 0;
            ns.Data = new byte[6];

            EdControl.Level.Sprites.Add(ns);
            EdControl.SelectObject(ns);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();

        }

        private void deleteSpriteButton_Click(object sender, EventArgs e)
        {
            EdControl.Level.Sprites.Remove(s);
            EdControl.SelectObject(null);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void spriteListBox_DrawItem(object sender, DrawItemEventArgs e)
        {

            e.DrawBackground();
            //Brush UseBrush;
            Color UseColour;
            if (spriteListBox.Items.Count > 0) {
                if (EdControl.Level.ValidSprites[curSprites[e.Index]])
                {
                    //UseBrush = Brushes.Black;
                    UseColour = e.ForeColor;
                }
                else
                {
                    //UseBrush = Brushes.DarkRed;
                    UseColour = Color.DarkRed;
                }

                //e.Graphics.DrawString((string)spriteListBox.Items[e.Index], spriteListBox.Font, UseBrush, e.Bounds);
                TextRenderer.DrawText(e.Graphics, (string)spriteListBox.Items[e.Index], spriteListBox.Font, e.Bounds, UseColour, e.BackColor, TextFormatFlags.Left);

                int SSNumber = SSTable[curSprites[e.Index] << 1];
                int SSValue = SSTable[(curSprites[e.Index] << 1) + 1];
                string txt = (SSNumber + 1) + "-" + SSValue;
                if (SSValue == 0)
                    txt = "-";

                TextRenderer.DrawText(e.Graphics, txt, spriteListBox.Font, new Rectangle(e.Bounds.X +e.Bounds.Width - 30, e.Bounds.Y, 30, e.Bounds.Height), UseColour, e.BackColor, TextFormatFlags.Right);
            }
            e.DrawFocusRectangle();
        }

        private void spriteDataTextBox_TextChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag || !spriteDataTextBox.Visible)
                return;

            // validate
            if (System.Text.RegularExpressions.Regex.IsMatch(
                spriteDataTextBox.Text,
                "^[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *[0-9a-f] *$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                string parseit = spriteDataTextBox.Text.Replace(" ", "");
                byte[] data = new byte[6];
                for (int hexidx = 0; hexidx < 6; hexidx++)
                {
                    data[hexidx] = byte.Parse(parseit.Substring(hexidx*2, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                }
                s.Data = data;
                spriteDataTextBox.BackColor = SystemColors.Window;
            }
            else
            {
                spriteDataTextBox.BackColor = Color.Coral;
            }
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
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
            spriteListBox.SelectedIndex = curSprites.IndexOf(s.Type);
            if (curSprites.Count > 0)
                searchBox.BackColor = SystemColors.Window;
            else
                searchBox.BackColor = Color.Coral;
        }
    }
}
