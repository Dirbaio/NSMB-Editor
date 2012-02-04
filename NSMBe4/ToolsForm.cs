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
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ToolsForm : Form
    {

        private LevelEditorControl EdControl;
        private NSMBSprite foundSprite;
        private NSMBObject foundObj;

        public ToolsForm(LevelEditorControl edc)
        {
            InitializeComponent();
            if (Properties.Settings.Default.mdi)
                this.MdiParent = MdiParentForm.instance;
            LanguageManager.ApplyToContainer(this, "ToolsForm");
            this.EdControl = edc;
        }

        private void spriteFindNext_Click(object sender, EventArgs e)
        {
            if (EdControl.Level.Sprites.Count == 0)
            {
                //MessageBox.Show(LanguageManager.Get("ToolsForm", "NoSprites"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int ind = -1;
            if (foundSprite != null && EdControl.Level.Sprites.Contains(foundSprite))
                ind = EdControl.Level.Sprites.IndexOf(foundSprite);


            int startInd = ind;
            ind++;
            ind %= EdControl.Level.Sprites.Count;
            bool found = false;

            while (ind != startInd && !found)
            {
                if (EdControl.Level.Sprites[ind].Type == SpriteNumber.Value)
                {
                    foundSprite = EdControl.Level.Sprites[ind];
                    found = true;
                }
                ind++;
                ind %= EdControl.Level.Sprites.Count;
            }

            if (found)
            {
                EdControl.SelectObject(foundSprite);
                EdControl.EnsureBlockVisible(foundSprite.X, foundSprite.Y);
            }
            //else
                //MessageBox.Show(LanguageManager.Get("ToolsForm", "NotFound"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void sprSelectAll_Click(object sender, EventArgs e)
        {
            List<LevelItem> sprites = new List<LevelItem>();
            foreach (NSMBSprite s in EdControl.Level.Sprites)
                if (s.Type == SpriteNumber.Value)
                    sprites.Add(s);
            EdControl.SelectObject(sprites);
            EdControl.ScrollToObjects(sprites);
            EdControl.repaint();
        }

        private void objFindNext_Click(object sender, EventArgs e)
        {
            if (EdControl.Level.Objects.Count != 0) {
                int startInd = -1;
                if (foundObj != null && EdControl.Level.Objects.Contains(foundObj))
                    startInd = EdControl.Level.Objects.IndexOf(foundObj);
                int ind = (startInd + 1) % EdControl.Level.Objects.Count;

                bool found = false;
                while (ind != startInd && !found) {
                    if (EdControl.Level.Objects[ind].Tileset == nudFindTileset.Value &&
                        EdControl.Level.Objects[ind].ObjNum == nudFindObjNum.Value) {
                        foundObj = EdControl.Level.Objects[ind];
                        found = true;
                    }
                    ind = (ind + 1) % EdControl.Level.Objects.Count;
                }

                if (found) {
                    EdControl.SelectObject(foundObj);
                    EdControl.EnsureBlockVisible(foundObj.X, foundObj.Y);
                }
                //else
                    //MessageBox.Show(LanguageManager.Get("ToolsForm", "NotFound"), LanguageManager.Get("General", "Warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void objSelectAll_Click(object sender, EventArgs e)
        {
            List<LevelItem> objs = new List<LevelItem>();
            foreach (NSMBObject o in EdControl.Level.Objects)
                if (o.Tileset == nudFindTileset.Value && o.ObjNum == nudFindObjNum.Value)
                    objs.Add(o);
            EdControl.SelectObject(objs);
            EdControl.ScrollToObjects(objs);
            EdControl.repaint();
        }

        private void ToolsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}
