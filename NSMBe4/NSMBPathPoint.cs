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
