using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace NSMBe4
{
    public class NSMBPath
    {
        public const int XOffs = 0;
        public const int YOffs = 0;

        public int id;
        public List<NSMBPathPoint> points = new List<NSMBPathPoint>();

        public NSMBPath()
        {

        }

        public void write(ByteArrayOutputStream outp, ByteArrayOutputStream outn)
        {
            outp.writeUShort((ushort)id);
            outp.writeUShort((ushort)(outn.getPos()/16));
            outp.writeUShort((ushort)(points.Count));
            outp.writeUShort(0); //Unused values

            foreach (NSMBPathPoint p in points)
                p.write(outn);
        }

        public static NSMBPath read(ByteArrayInputStream inp, ByteArrayInputStream nodes)
        {
            NSMBPath p = new NSMBPath();

            p.id = inp.readUShort();
            int row = inp.readUShort();
            int len = inp.readUShort();
            inp.skip(2); //unused values

            nodes.seek(row*16);
            for (int i = 0; i < len; i++)
                p.points.Add(NSMBPathPoint.read(nodes, p));
            return p;
        }

        public void Render(Graphics g, bool selected)
        {
            if (points.Count <= 0)
                return;

            bool first = true;
            int lx = 0;
            int ly = 0;
            foreach (NSMBPathPoint p in points)
            {
                if (!first)
                    g.DrawLine(NSMBGraphics.PathPen, lx, ly, p.X+8+XOffs, p.Y+8+YOffs);
                    
                lx = p.X+8+XOffs;
                ly = p.Y+8+YOffs;
                first = false;
            }

            NSMBPathPoint fp = points[0];
            NSMBPathPoint lp = points[points.Count - 1];
            int num = 0;
            foreach (NSMBPathPoint p in points)
            {
                Pen pe = null;
                if (p == fp)
                    pe = Pens.Green;
                if (p == lp)
                    pe = Pens.Red;

                p.Render(g, pe, num);
                num++;
            }
        }

        public override string ToString()
        {
            return string.Format(LanguageManager.Get("NSMBPath", "ToString"), id, points.Count);
        }
    }
}
