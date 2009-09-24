using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4 {
    public class NSMBPathPoint {
        public int X;
        public int Y;
        public ushort Unknown1;
        public ushort Unknown2;
        public ushort Unknown3;
        public ushort Unknown4;
        public ushort Unknown5;
        public ushort Unknown6;

        public NSMBPath parent;

        public NSMBPathPoint(NSMBPath p)
        {
            parent = p;
        }

        public NSMBPathPoint(NSMBPathPoint p)
        {
            X = p.X;
            Y = p.Y;
            parent = p.parent;
            Unknown1 = p.Unknown1;
            Unknown2 = p.Unknown2;
            Unknown3 = p.Unknown3;
            Unknown4 = p.Unknown4;
            Unknown5 = p.Unknown5;
            Unknown6 = p.Unknown6;
        }

        public void Render(Graphics g, Pen p, int num) {
            g.DrawImage(Properties.Resources.pathpoint, X + NSMBPath.XOffs, Y+NSMBPath.YOffs);
            if (p != null)
            {
                g.DrawRectangle(p, X, Y, 16, 16);
                g.DrawRectangle(p, X+1, Y+1, 14, 14);

            }
            g.DrawString(num.ToString(), NSMBGraphics.SmallInfoFont, Brushes.White, X, Y);
        }

        public static NSMBPathPoint read(ByteArrayInputStream inp, NSMBPath parent)
        {
            NSMBPathPoint p = new NSMBPathPoint(parent);
            p.X = inp.readUShort();
            p.Y = inp.readUShort();
            p.Unknown1 = inp.readUShort();
            p.Unknown2 = inp.readUShort();
            p.Unknown3 = inp.readUShort();
            p.Unknown4 = inp.readUShort();
            p.Unknown5 = inp.readUShort();
            p.Unknown6 = inp.readUShort();
            return p;
        }

        internal void write(ByteArrayOutputStream outn)
        {
            outn.writeUShort((ushort)X);
            outn.writeUShort((ushort)Y);
            outn.writeUShort(Unknown1);
            outn.writeUShort(Unknown2);
            outn.writeUShort(Unknown3);
            outn.writeUShort(Unknown4);
            outn.writeUShort(Unknown5);
            outn.writeUShort(Unknown6);
        }
    }
}
