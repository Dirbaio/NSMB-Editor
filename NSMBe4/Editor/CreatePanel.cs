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
    public partial class CreatePanel : UserControl
    {
        LevelEditorControl EdControl;
        public CreatePanel(LevelEditorControl EdControl)
        {
            InitializeComponent();
            this.EdControl = EdControl;

            LanguageManager.ApplyToContainer(this, "CreatePanel");
        }

        private void CreateObject_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;
            NSMBObject no = new NSMBObject(10, 0, ViewableArea.X, ViewableArea.Y, 1, 1, EdControl.GFX);
            EdControl.UndoManager.Do(new AddObjectAction(no));
        }

        private void CreateSprite_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;
            NSMBSprite ns = new NSMBSprite(EdControl.Level);
            ns.X = ViewableArea.X;
            ns.Y = ViewableArea.Y;
            ns.Type = 0;
            ns.Data = new byte[6];
            EdControl.UndoManager.Do(new AddSpriteAction(ns));
        }
    }
}
