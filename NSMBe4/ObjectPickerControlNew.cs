using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4
{
    public partial class ObjectPickerControlNew : UserControl
    {
        public delegate void ObjectSelectedDelegate();
        public event ObjectSelectedDelegate ObjectSelected;
        bool inited = false;
        NSMBGraphics gfx;
        int tileset;
        NSMBObject selected = null;
        List<NSMBObject> objects = new List<NSMBObject>();

        int tileWidth = -1;
        public int SelectedObject = 0;
        bool selecting = false;

        public ObjectPickerControlNew()
        {
            InitializeComponent();
        }

        public void Initialise(NSMBGraphics GFXd, int tileset)
        {
            if (inited) return;
            inited = true;
            gfx = GFXd;
            this.tileset = tileset;
            LoadObjects();
        }

        public void reload()
        {
            tileWidth = -1;
            LoadObjects();
        }
        private void LoadObjects()
        {
            int nw = (Width-40) / 16;
            if (nw < 5) nw = 5;

            if (tileWidth == nw) return;
            if (!inited) return;

            objects.Clear();

            tileWidth = nw;
            int x = 0;
            int y = 0;

            int rowheight = 1;
            for(int i = 0; i < 256; i++)
            {
                if (gfx.Tilesets[tileset].Objects[i] == null) continue;
                int ow = gfx.Tilesets[tileset].Objects[i].getWidth();
                int oh = gfx.Tilesets[tileset].Objects[i].getHeight();
                if (ow > tileWidth) ow = tileWidth;
                if (oh > 5) oh = 5;

                if (x + ow > tileWidth)
                {
                    //New line
                    x = 0;
                    y += rowheight + 1;
                    rowheight = 1;
                }

                NSMBObject o = new NSMBObject(i, tileset, x, y, ow, oh, gfx);
                if (i == SelectedObject) selected = o;

                x += ow + 1;
                if (oh > rowheight) rowheight = oh;

                if (!o.badObject)
                    objects.Add(o);
            }

            if(x != 0)
                y += rowheight + 1;
            /*
            MinimumSize = new Size(tileWidth * 16 + 16, y * 16);
            Size = new Size(tileWidth * 16 + 16, y * 16);
            MaximumSize = new Size(tileWidth * 16 + 16, y * 16);*/

            AutoScrollMinSize = new Size(tileWidth * 16 + 16, y * 16);
//            AutoScrollMinSize = new Size(2000, 2000);
            Invalidate();
        }

        private void ObjectPickerControlNew_Load(object sender, EventArgs e)
        {

        }

        private void ObjectPickerControlNew_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rec = e.ClipRectangle;

            e.Graphics.FillRectangle(Brushes.White, rec);
            rec.X -= AutoScrollPosition.X;
            rec.Y -= AutoScrollPosition.Y;
            System.Drawing.Drawing2D.Matrix tr = e.Graphics.Transform;
            e.Graphics.TranslateTransform(AutoScrollPosition.X, AutoScrollPosition.Y);

            foreach (NSMBObject obj in objects)
            {
                Rectangle or = new Rectangle(obj.X * 16 + 8, obj.Y * 16 + 8, obj.Width * 16, obj.Height * 16);
                Rectangle or2 = Rectangle.Inflate(or, 4, 4);
                if (or2.IntersectsWith(rec))
                {
                    e.Graphics.FillRectangle(obj == selected ? Brushes.OrangeRed : Brushes.LightBlue, or2);
                    e.Graphics.FillRectangle(Brushes.LightSlateGray, or);
                    obj.RenderPlain(e.Graphics, obj.X * 16 + 8, obj.Y * 16 + 8);
                }
            }
            e.Graphics.Transform = tr;
        }
        
        int xDown, yDown;
        int xScr, yScr;

        private void ObjectPickerControlNew_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                int x = e.X - AutoScrollPosition.X;
                int y = e.Y - AutoScrollPosition.Y;

                int oldSel = SelectedObject;

                foreach (NSMBObject obj in objects)
                {
                    Rectangle or = new Rectangle(obj.X * 16 + 8, obj.Y * 16 + 8, obj.Width * 16, obj.Height * 16);
                    or.Inflate(8, 8);
                    if (or.Contains(x, y))
                    {
                        selected = obj;
                        SelectedObject = obj.ObjNum;
                    }
                }

                if (oldSel != SelectedObject)
                {
                    Invalidate();
                    selecting = true;
                    if(ObjectSelected != null)
                        ObjectSelected();
                    selecting = false;
                }
            }
            else
            {
                xDown = e.X;
                yDown = e.Y;
                xScr = AutoScrollPosition.X;
                yScr = AutoScrollPosition.Y;
            }
        }

        private void ObjectPickerControlNew_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                AutoScrollPosition = new Point(-xScr + (xDown - e.X) * 2, -yScr + (yDown - e.Y)*2);
            }
        }

        private void ObjectPickerControlNew_Resize(object sender, EventArgs e)
        {
            LoadObjects();
        }

        public void selectObjectNumber(int objectNum)
        {
            SelectedObject = -1;
            selected = null;

            foreach(NSMBObject o in objects)
                if (o.ObjNum == objectNum)
                {
                    selected = o;
                    SelectedObject = objectNum;
                }

            if (selected != null && !selecting)
            {
                int ny = selected.Y * 16 - Height / 2;
                AutoScrollPosition = new Point(0, ny);
            }
            Invalidate();
        }
    }
}
