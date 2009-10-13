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
            EdControl.Level.Objects.Add(no);
            EdControl.SelectObject(no);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void CreateSprite_Click(object sender, EventArgs e)
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
    }
}
