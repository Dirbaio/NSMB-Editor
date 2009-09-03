using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ObjectEditor : UserControl
    {
        private NSMBObject o;
        private LevelEditorControl EdControl;
        private bool DataUpdateFlag = false;
        private ObjectPickerControl objectPickerControl1;

        public ObjectEditor(NSMBObject o, LevelEditorControl EdControl, ObjectPickerControl opc)
        {
            InitializeComponent();
            setOPC(opc);
            this.o = o;
            this.EdControl = EdControl;
            UpdateInfo();
            if (Properties.Settings.Default.Language == 1)
            {
                {
                    addObjectButton.Text = "Añadir Objeto";
                    deleteObjectButton.Text = "Borrar Objeto";
                    objPositionBox.Text = "Posicion de Objeto";
                    objPickerBox.Text = "Elegir Objeto";
                    label2.Text = "Anchura:";
                    label4.Text = "Altura:";
                    label5.Text = "Serie de Objetos:";
                    label6.Text = "Tipo de Objeto:";
                }
            }
        }

        private void setOPC(ObjectPickerControl opc)
        {
            objectPickerControl1 = opc;

            this.objectPickerControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectPickerControl1.Location = new System.Drawing.Point(3, 70);
            this.objectPickerControl1.Name = "objectPickerControl1";
            this.objectPickerControl1.Size = new System.Drawing.Size(266, 240);
            this.objectPickerControl1.TabIndex = 1;
            this.objectPickerControl1.ObjectSelected += new NSMBe4.ObjectPickerControl.ObjectSelectedDelegate(this.objectPickerControl1_ObjectSelected);
            this.objPickerBox.Controls.Add(this.objectPickerControl1);

        }

        private void SetTileset(int T)
        {
            o.Tileset = T;
            o.UpdateObjCache();
            objectPickerControl1.CurrentTileset = T;
            objectPickerControl1.Invalidate(true);
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }
        public void SetObject(NSMBObject no)
        {
            o = no;
            UpdateInfo();
        }

        public void UpdateInfo()
        {
            if (o == null) return;
            DataUpdateFlag = true;
            objXPosUpDown.Value = o.X;
            objYPosUpDown.Value = o.Y;
            objWidthUpDown.Value = o.Width;
            objHeightUpDown.Value = o.Height;

            objTileset0Button.Checked = (o.Tileset == 0);
            objTileset1Button.Checked = (o.Tileset == 1);
            objTileset2Button.Checked = (o.Tileset == 2);

            objTypeUpDown.Value = o.ObjNum;

            objectPickerControl1.CurrentTileset = o.Tileset;
            objectPickerControl1.SelectedObject = o.ObjNum;
            objectPickerControl1.EnsureObjVisible((int)objTypeUpDown.Value);
            objectPickerControl1.Invalidate(true);
            DataUpdateFlag = false;
        }

        private void objXPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.X = (int)objXPosUpDown.Value;
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objYPosUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.Y = (int)objYPosUpDown.Value;
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objWidthUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.Width = (int)objWidthUpDown.Value;
            o.UpdateObjCache();
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objHeightUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.Height = (int)objHeightUpDown.Value;
            o.UpdateObjCache();
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objTileset0Button_Click(object sender, EventArgs e)
        {
            SetTileset(0);
        }

        private void objTileset1Button_Click(object sender, EventArgs e)
        {
            SetTileset(1);
        }

        private void objTileset2Button_Click(object sender, EventArgs e)
        {
            SetTileset(2);
        }

        private void objTypeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (DataUpdateFlag) return;
            o.ObjNum = (int)objTypeUpDown.Value;
            o.UpdateObjCache();
            objectPickerControl1.SelectedObject = (int)objTypeUpDown.Value;
            objectPickerControl1.EnsureObjVisible((int)objTypeUpDown.Value);
            objectPickerControl1.Invalidate(true);
            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void objectPickerControl1_ObjectSelected()
        {
            DataUpdateFlag = true;
            objTypeUpDown.Value = objectPickerControl1.SelectedObject;
            o.ObjNum = objectPickerControl1.SelectedObject;
            o.UpdateObjCache();
            DataUpdateFlag = false;

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void addObjectButton_Click(object sender, EventArgs e)
        {
            Rectangle ViewableArea = EdControl.ViewableArea;
            NSMBObject no = new NSMBObject(10, 0, ViewableArea.X, ViewableArea.Y, 1, 1, EdControl.GFX);
            EdControl.Level.Objects.Add(no);
            EdControl.SelectObject(no);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }

        private void deleteObjectButton_Click(object sender, EventArgs e)
        {
            EdControl.Level.Objects.Remove(o);
            EdControl.SelectObject(null);

            EdControl.Invalidate(true);
            EdControl.FireSetDirtyFlag();
        }
    }
}
