using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace NSMBe4 {
    public partial class ObjectPickerControl : UserControl {

        private NSMBObject[][] TilesetObjects;
        private bool inited = false;
        private NSMBGraphics GFX;
        private bool Ready = false;

        public ObjectPickerControl()
        {
            InitializeComponent();
            vScrollBar.Visible = true;
            SelectedObject = 0;
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        public void Initialise(NSMBGraphics GFXd) {
            if (inited) return;
            inited = true;
            GFX = GFXd;
            Ready = true;
            LoadObjects();
        }

        private void LoadObjects() {
            TilesetObjects = new NSMBObject[][] { null, null, null };
            TilesetObjects[0] = new NSMBObject[256];
            TilesetObjects[1] = new NSMBObject[256];
            TilesetObjects[2] = new NSMBObject[256];
            for (int TSIdx = 0; TSIdx < 3; TSIdx++) {
                for (int ObjIdx = 0; ObjIdx < 256; ObjIdx++) {
                    TilesetObjects[TSIdx][ObjIdx] = new NSMBObject(ObjIdx, TSIdx, 0, 0, 5, 3, GFX);
                }
            }
        }

        public void ReRenderAll(int Tileset) {
            for (int ObjIdx = 0; ObjIdx < 256; ObjIdx++) {
                TilesetObjects[Tileset][ObjIdx].UpdateObjCache();
            }
        }

        #region Scrolling
        private void UpdateScrollbars() {
            ViewableHeight = (int)Math.Ceiling((float)DrawingArea.Height / 54);

            vScrollBar.Maximum = ((int)Math.Ceiling((float)(256 - ViewableHeight) / 4) * 4) + 1;
        }

        private void ObjectPickerControl_Resize(object sender, EventArgs e) {
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        private void vScrollBar_ValueChanged(object sender, ScrollEventArgs e) {
            UpdateScrollbars();
            DrawingArea.Invalidate();
        }

        public void EnsureObjVisible(int ObjNum) {
            if (ObjNum < vScrollBar.Value) {
                vScrollBar.Value = ObjNum;
            } else if (ObjNum > (vScrollBar.Value + ViewableHeight - 2)) {
                vScrollBar.Value = ObjNum - ViewableHeight + 2;
            }
        }

        private int ViewableHeight;
        #endregion


        public int SelectedObject;
        public int CurrentTileset;


        public delegate void ObjectSelectedDelegate();
        public event ObjectSelectedDelegate ObjectSelected;

        private void DrawingArea_Paint(object sender, PaintEventArgs e) {
            if (!Ready) return;

            // This works sort of weirdly since I render one half in GDI+ and the rest in GDI
            // GDI+ part
            e.Graphics.Clear(Color.Silver);

            int CurrentDrawY = 2;
            int RealObjIdx = vScrollBar.Value;

            for (int ObjIdx = 0; ObjIdx < ViewableHeight; ObjIdx++) {
                e.Graphics.FillRectangle((RealObjIdx == SelectedObject) ? Brushes.WhiteSmoke : Brushes.Gainsboro, 2, CurrentDrawY, DrawingArea.Width - 4, 52);
                e.Graphics.DrawString("Object " + RealObjIdx.ToString(), NSMBGraphics.SmallInfoFont, Brushes.Black, 86, (float)CurrentDrawY);
                if (RealObjIdx >= GFX.Tilesets[CurrentTileset].Objects.Length) {
                    // Invalid object
                    e.Graphics.DrawImage(NSMBe4.Properties.Resources.warning, DrawingArea.Width - 22, CurrentDrawY + 2);
                    e.Graphics.DrawString("This object does not exist\nin the selected tileset.", NSMBGraphics.SmallInfoFont, Brushes.Black, 86, (float)CurrentDrawY + 14);
                }
                if (GFX.Tilesets[CurrentTileset].UseNotes && RealObjIdx < GFX.Tilesets[CurrentTileset].ObjNotes.Length) {
                    e.Graphics.DrawString(GFX.Tilesets[CurrentTileset].ObjNotes[RealObjIdx], NSMBGraphics.SmallInfoFont, Brushes.Black, 86, (float)CurrentDrawY + 14);
                }
                CurrentDrawY += 54;
                RealObjIdx++;
                if (RealObjIdx == 256) break;
            }

            // GDI part
            CurrentDrawY = 4;
            RealObjIdx = vScrollBar.Value;

#if USE_GDIPLUS
            for (int ObjIdx = 0; ObjIdx < ViewableHeight; ObjIdx++) {
                TilesetObjects[CurrentTileset][RealObjIdx].RenderPlain(e.Graphics, 4, CurrentDrawY);
                CurrentDrawY += 54;
                RealObjIdx++;
                if (RealObjIdx == 256) break;
            }
#else
            IntPtr pTarget = e.Graphics.GetHdc();

            for (int ObjIdx = 0; ObjIdx < ViewableHeight; ObjIdx++) {
                TilesetObjects[CurrentTileset][RealObjIdx].RenderPlain(pTarget, 4, CurrentDrawY);
                CurrentDrawY += 54;
                RealObjIdx++;
                if (RealObjIdx == 256) break;
            }

            e.Graphics.ReleaseHdc(pTarget);
#endif
        }

        private void DrawingArea_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                int OldSelection = SelectedObject;

                SelectedObject = (int)Math.Floor((double)(e.Y - 2) / 54) + vScrollBar.Value;
                if (SelectedObject < 0) SelectedObject = 0;
                if (SelectedObject > 255) SelectedObject = 255;

                if (SelectedObject != OldSelection) {
                    Invalidate(true);
                    ObjectSelected();
                }
            }
        }

        private void DrawingArea_MouseMove(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                if (e.Y < 16 && vScrollBar.Value > 0) {
                    vScrollBar.Value -= 1;
                    Invalidate(true);
                }
                if (e.Y > (DrawingArea.Height - 16) && vScrollBar.Value < vScrollBar.Maximum) {
                    vScrollBar.Value += 1;
                    Invalidate(true);
                }
            }
            DrawingArea_MouseDown(sender, e);
        }
    }
}
