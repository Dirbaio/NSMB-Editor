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
        public bool isProgressPath;

        public NSMBPath()
        {

        }

        public NSMBPath(NSMBPath path)
        {
            this.id = path.id;
            NSMBPathPoint newpt;
            foreach (NSMBPathPoint pt in path.points) {
                newpt = new NSMBPathPoint(pt);
                newpt.parent = this;
                this.points.Add(newpt);
            }
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

        public static NSMBPath read(ByteArrayInputStream inp, ByteArrayInputStream nodes, bool isProgressPath)
        {
            NSMBPath p = new NSMBPath();
            p.isProgressPath = isProgressPath;

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

        public int getMinX()
        {
            int min = int.MaxValue;
            foreach (NSMBPathPoint n in points)
                min = Math.Min(min, n.X);
            return min;
        }

        public int getMinY()
        {
            int min = int.MaxValue;
            foreach (NSMBPathPoint n in points)
                min = Math.Min(min, n.Y);
            return min;
        }

        public override string ToString()
        {
            return string.Format(LanguageManager.Get("NSMBPath", "ToString"), id, points.Count);
        }
    }
}
