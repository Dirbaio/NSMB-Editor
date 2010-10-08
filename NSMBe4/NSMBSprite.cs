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
// 153/203

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NSMBe4
{
    public class NSMBSprite {
        public int X;
        public int Y;
        public int Type;
        public byte[] Data;
        private NSMBLevel Level;

        public NSMBSprite(NSMBLevel Level)
        {
            this.Level = Level;
        }

        public NSMBSprite(NSMBSprite s)
        {
            this.X = s.X;
            this.Y = s.Y;
            this.Type = s.Type;
            this.Level = s.Level;

            this.Data = new byte[6];
            Array.Copy(s.Data, Data, 6);
        }

        public Rectangle getRect() {
            int x = this.X * 16;
            int y = this.Y * 16;
            int width = 16;
            int height = 16;
            switch (this.Type) {
                case 23:
                    y -= 28;
                    width = 32; height = 28;
                    break;
                case 24:
                    y += 32;
                    width = 32; height = 28;
                    break;
                case 25:
                    x += 32;
                    width = 28; height = 32;
                    break;
                case 26:
                    x -= 28;
                    width = 28; height = 32;
                    break;
                case 27:
                    height = (Data[2] & 0xF0) + 32;
                    y -= height - 16;
                    break;
                case 28:
                    height = 18;
                    y -= 2;
                    break;
                case 31:
                    width = 21;
                    break;
                case 32:
                    x -= 29; y -= 119;
                    width = 27; height = 24;
                    break;
                case 33:
                    if (Data[2] % 0x10 > 0) {
                        x -= 8; y -= 16;
                        width = 32; height = 32;
                    }
                    if (Data[2] / 0x10 > 0)
                        x += 8;
                    break;
                case 34:
                    x -= 7; y -= 26;
                    width = 30; height = 42;
                    break;
                case 36:
                    width = 32; height = 36;
                    break;
                case 37:
                    width = 20;
                    break;
                case 38:
                    width = 24; height = 20;
                    break;
                case 42:
                    y -= 48; height = 48;
                    if (Data[2] % 0x10 > 0) {
                        x -= 29; width = 45;
                    }
                    break;
                case 48:
                    y -= 90;
                    width = 32; height = 74;
                    break;
                case 49:
                    y += 32;
                    width = 32; height = 74;
                    break;
                case 50:
                    x += 32;
                    width = 74; height = 32;
                    break;
                case 51:
                    x -= 90;
                    width = 74; height = 32;
                    break;
                //case 52:
                //    Buzzy Beetles don't need to be modified
                case 53:
                    y -= 10;
                    width = 17; height = 26;
                    break;
                case 54:
                    height = 29;
                    break;
                case 55:
                    width = 21;
                    break;
                case 56:
                    width = (Data[2] % 0x10) * 10;
                    y -= width;
                    if (Data[2] / 0x10 > 0) {
                        x -= width;
                        width *= 2;
                    }
                    width += 12;
                    height = width;
                    break;
                case 57:
                    if (Data[2] % 0x10 == 1) {
                        x -= 4; y -= 4;
                        width = 24; height = 24;
                    } else if (Data[2] % 0x10 == 2) {
                        x -= 4; y -= 20;
                        width = 72; height = 40;
                    }
                    if (Data[2] % 0x10 != 1 && Data[2] % 0x10 != 2) {
                        if (Data[4] / 0x10 == 1)
                            y += 8;
                        if (Data[5] % 0x10 == 1)
                            x += 8;
                    }
                    break;
                case 59:
                    y -= 18;
                    width = 18; height = 34;
                    break;
                case 64:
                    if (Data[2] % 0x10 == 1) {
                        x -= 38; y -= 74;
                        width = 96; height = 90;
                    } else {
                        x -= 29; y -= 57;
                        width = 78; height = 73;
                    }
                    break;
                case 66:
                    if (Data[2] % 0x10 != 1)
                        y -= 2;
                    height = 18;
                    break;
                case 68:
                case 69:
                    width = Math.Max(16 * (Data[2] + 1), 32);
                    break;
                case 73:
                    width = 192; height = 32;
                    break;
                case 74:
                    y -= 8;
                    width = 64; height = 40;
                    break;
                case 75:
                    width = 256;
                    break;
                case 76:
                    x -= 24; y -= 10;
                    width = 16 * (Data[2] % 0x10) + 48;
                    height = 16 * Math.Max(Data[2] / 0x10, Data[3] % 0x10) + 18;
                    break;
                case 77:
                case 78:
                    width = Data[2]; int s = 2;
                    if (this.Type == 78) { width = Math.Max(0, width - 1); s = 1; x -= 8; }
                    x -= (width * s + 1) * 8;
                    width = (width * s + 2) * 16;
                    break;
                case 79:
                    x -= 28; y -= 34;
                    width = 92; height = 85;
                    break;
                case 82:
                    x -= 24; y -= 8;
                    width = 64; height = 32;
                    break;
                case 86:
                    x -= 14; y -= 6;
                    width = 44; height = 39;
                    break;
                case 89:
                    y -= 16;
                    width = 30; height = 32;
                    break;
                case 90:
                    if (Data[2] == 1) {
                        y -= 44;
                        width = 92; height = 60;
                    } else {
                        y -= 14;
                        width = 52; height = 30;
                    }
                    break;
                case 91:
                    y += 8;
                    width = 64;
                    break;
                case 92:
                    if (Data[2] % 0x10 == 1) {
                        x += 3; y += 7;
                        width = 25; height = 23;
                    } else if (Data[2] / 0x10 == 1) {
                        x += 45; y += 3;
                        width = 104; height = 28;
                    } else {
                        x += 12; y += 3;
                        width = 104; height = 28;
                    }
                    break;
                case 93:
                    width = 48; height = 48;
                    if (Data[4] % 0x10 == 1) {
                        width = 24; height = 24; }
                    if (Data[2] / 0x10 % 2 == 1) {
                        x += (width / 9) * BtoI(Data[2] / 0x10 > 4);
                        y += (width / 9) * BtoI(Data[2] / 0x10 == 3 || Data[2] / 0x10 == 5);
                    }
                    x -= (width - 16) / 2;
                    y -= (height - 16);
                    break;
                case 94:
                    if (Data[2] > 0) {
                        x -= 6; y -= 16;
                        width = 26; height = 37;
                    } else {
                        height = 18;
                    }
                    break;
                case 95:
                    x -= 6;
                    width = 26; height = 26;
                    break;
                case 96:
                    x -= 32; y -= 127;
                    width = 90; height = 147;
                    break;
                case 99:
                    x -= 38; y -= 29;
                    width = 91; height = 75;
                    break;
                case 102:
                    width = 30; height = 32;
                    break;
                case 103:
                    x -= 51; y -= 59;
                    width = 115; height = 76;
                    break;
                case 104:
                    x -= 88; y -= 130;
                    width = 191; height = 150;
                    break;
                case 105:
                    x -= 78; y -= 164;
                    width = 165; height = 182;
                    break;
                //case 106:
                //    Red Coins don't need to be modified
                case 107:
                    if (Data[2] != 1)
                        y -= 2;
                    height = 18;
                    break;
                case 108:
                    if (Data[2] != 1)
                        y -= 2;
                    height = 18;
                    break;
                case 109:
                    x -= 6; y -= 6;
                    int shift = Data[2] / 0x10;
                    if (shift == 1 || shift == 3)
                        x += 8;
                    if (shift == 2 || shift == 3)
                        y += 8;
                    height = 28; width = 27;
                    break;
                case 111:
                    x -= 128; y -= 32;
                    width = 256; height = 64;
                    break;
                case 113:
                    width = 60; height = 47;
                    break;
                case 114:
                case 118:
                    int w = 64;
                    if (this.Type == 118)
                        w = 128;
                    switch (Data[2] % 0x40) {
                        case 0:
                            width = w; height = 16; break;
                        case 1:
                            x -= w - 16; width = w; height = 16; break;
                        case 2:
                            y -= w - 16; width = 16; height = w; break;
                        case 3:
                            width = 16; height = w; break;
                    }
                    break;
                case 115:
                    y -= 42;
                    width = 63; height = 58;
                    break;
                case 116:
                    x -= 16; y -= 5;
                    width = 41; height = 21;
                    break;
                case 117:
                    x -= 12; y -= 8;
                    width = 44; height = 23;
                    break;
                case 119:
                    int sz = Data[5] / 0x10;
                    if (sz == 0) sz = 1;
                    width = sz * 64; height = sz * 68;
                    x -= sz * 32; y -= sz * 6;
                    break;
                case 120:
                    y += 3;
                    width = 33; height = 29;
                    break;
                case 122:
                    x -= 37; y -= 48;
                    width = 64; height = 64;
                    break;
                case 123:
                    x -= 8; y += 2;
                    width = 40; height = 30;
                    break;
                case 126:
                    width = (Data[5] & 0xF0) * 2;
                    x -= width / 2;
                    if (width == 0)
                        width = 16;
                    break;
                case 127:
                    x -= 144; y -= 104;
                    width = 288; height = 208;
                    break;
                case 128:
                    x -= 9; y -= 26;
                    width = 66; height = 90;
                    break;
                case 130:
                    width = 21;
                    break;
                case 131:
                case 132:
                    width = 22; height = 33;
                    break;
                case 136:
                    width = 18; height = 16 * (Data[2] + 2) + 7;
                    y -= height - 16;
                    break;
                case 144:
                    x -= 2; y -= 7;
                    width = 19; height = 26;
                    break;
                case 146:
                    width = 32; height = 8;
                    break;
                case 147:
                    x -= Data[2] * 8 + 6;
                    width = 16 * (Data[2] - 1) + 28;
                    height = 10;
                    break;
                case 148:
                    y -= 2;
                    height = 18;
                    break;
                case 149:
                    y -= 13;
                    height = 29;
                    break;
                case 150:
                    y -= 16;
                    width = 19; height = 32;
                    break;
                //case 152:
                //    Switch blocks don't need to be modified
                case 155:
                    width = 16 * (Data[3] % 0x10 + 1); height = 16 * (Data[2] / 0x10 + 1);
                    break;
                case 157:
                    y -= 15; width = 18;
                    height = 31;
                    break;
                case 158:
                    y -= 18;
                    width = 18; height = 34;
                    break;
                case 162:
                    x -= 16 * (Data[3] / 0x10 + 1);
                    width = 32 * (Data[3] / 0x10 + 2);
                    height = 16 * (Data[4] % 0x10 + 4);
                    break;
                case 173:
                    x -= 3; width = 8;
                    height = 16 * (Data[2] % 0x10 + 4);
                    break;
                case 180:
                    width = 18; height = 26;
                    break;
                case 183:
                    y -= 23;
                    width = 26; height = 39;
                    break;
                case 186:
                    y -= 12;
                    width = 24; height = 28;
                    break;
                case 187:
                    width = 112; height = 16;
                    break;
                case 189:
                    y -= 48;
                    width = 32; height = 64;
                    break;
                case 191:
                    y -= 80;
                    height = Data[4] / 0x10;
                    if (height == 0) height = 8;
                    height = (height + 1) * 16;
                    break;
                case 193:
                    y -= 28;
                    width = 25; height = 44;
                    break;
                case 194:
                    width = 64; height = 74;
                    break;
                case 197:
                    width = 16 * Math.Max(1, Data[2] / 0x10);
                    height = 16 * Math.Max(1, Data[2] % 0x10);
                    break;
                case 204:
                    y -= 7;
                    width = 27; height = 23;
                    break;
                case 205:
                    y -= 27;
                    width = 43; height = 33;
                    break;
                case 206:
                    y -= 32;
                    width = 256; height = 48;
                    break;
                case 207:
                    x -= 20; y -= 32;
                    width = 63; height = 52;
                    break;
                case 209:
                    y -= 15; width = 26;
                    height = 31;
                    break;
                case 211:
                    y -= 16;
                    width = 26; height = 32;
                    break;
                case 219:
                    switch (Data[2] / 0x10) {
                        case 1:
                            x -= 4; width = 20; break;
                        case 2:
                            height = 20; break;
                        case 3:
                            width = 20; break;
                        default:
                            y -= 4; height = 20; break;
                    }
                    break;
                case 220:
                    y -= 25;
                    width = 31; height = 41;
                    break;
                case 222:
                    if (Data[2] / 0x10 > 0)
                        x += 8;
                    if (Data[2] % 0x10 > 0)
                        y += 8;
                    width = 8; height = 8;
                    break;
                case 223:
                    width = 32; height = 32;
                    break;
                case 224:
                    width = 192; height = 64;
                    break;
                case 226:
                    x -= 14; y += 80;
                    width = 44; height = 37;
                    break;
                case 227:
                    y -= 5;
                    width = 23; height = 21;
                    break;
                case 228:
                    width = 20; height = 20;
                    break;
                case 233:
                    x -= 11; y -= 11;
                    width = 22; height = (Data[2] % 0x10) * 16 + 75;
                    break;
                case 235:
                    switch (Data[2] / 0x10) {
                        case 1:
                            x += 8; break;
                        case 2:
                            x -= 8; break;
                    }
                    switch (Data[2] % 0x10) {
                        case 1:
                            y += 8; break;
                        case 2:
                            y -= 8; break;
                    }
                    width = 32; height = 32;
                    break;
                case 236:
                    x -= 16; y -= 16;
                    if (Data[5] / 0x10 > 0)
                        y += 8;
                    width = 48; height = 48;
                    break;
                case 237:
                    y -= 15;
                    width = 45; height = 31;
                    break;
                case 238:
                    x -= 16 * (Data[2] / 0x10 + 1);
                    width = 32 * (Data[2] / 0x10 + 2);
                    height = 112;
                    break;
                case 239:
                    x -= 8 * (Data[2] % 0x10 + 1);
                    width = 16 * (Data[2] % 0x10 + 2);
                    height = 16 * (Math.Max(Data[2] / 0x10, Data[3] % 0x10) + 1) + 32;
                    break;
                case 241:
                    width = 23;
                    height = Math.Min(8, Data[4] + 1) * 16;
                    y -= height - 16; x -= 3;
                    break;
                case 242:
                    if (Data[2] / 0x10 == 1) {
                        if (Data[3] % 0x10 == 1) {
                            x -= 32; width = 80;
                        } else {
                            x -= 72; width = 160;
                        }
                    } else {
                        x -= 8; width = 32;
                    }
                    height = 32 + 16 * (Data[2] % 0x10);
                    break;
                //case 243:
                //    Roof Spinys don't need to be modified
                case 244:
                    switch (Data[2] / 0x10) {
                        case 1:
                            x -= 8; break;
                        case 2:
                            x += 8; break;
                    }
                    x -= 40;
                    width = 96; height = 16 * (Data[2] % 0x10 + 2);
                    break;
                case 246:
                    x -= 16; y -= 8;
                    width = 48; height = 48;
                    break;
                case 248:
                    x -= 53; y -= 106;
                    width = 109; height = 108;
                    break;
                case 249:
                    x -= Data[2] % 0x10 * 8 + 24; y += 5;
                    width = Data[2] % 0x10 * 16 + 64;
                    height = Data[3] / 0x10 * 16 + 83;
                    break;
                case 250:
                    width = 22; height = 14;
                    break;
                case 252:
                    x -= 32; y -= 64;
                    width = 64; height = 80;
                    break;
                case 254:
                    y -= 3;
                    width = 16; height = 19;
                    break;
                case 256:
                    width = 96; height = 16;
                    if (Data[4] / 0x10 == 0)
                        x -= 80;
                    break;
                case 265:
                    x += 8;
                    width = 16; height = 20;
                    break;
                case 268:
                    x -= 26; y -= 26;
                    width = 53; height = 53;
                    break;
                case 272:
                    if (Data[2] % 0x10 == 1) {
                        x -= 31; y -= 12;
                        width = 34; height = 23;
                    } else {
                        x += 15; y -= 12;
                        width = 30; height = 23;
                    }
                    break;
                case 273:
                    width = 32; height = 32;
                    break;
                case 274:
                    x -= 16;
                    height = 35; width = (Data[2] % 0x10 + 3) * 16;
                    break;
                case 277:
                    width = 32; height = 32;
                    break;
                case 278:
                    width = (Data[2] % 0x10 + 1) * 16;
                    height = (Data[2] / 0x10 + 1) * 16;
                    break;
                case 279:
                    if (Data[2] == 0 || Data[2] == 1) {
                        y -= 39;
                        width = 16; height = 39;
                    } else if (Data[2] == 2 || Data[2] == 3) {
                        y += 16;
                        width = 16; height = 39;
                    } else if (Data[2] == 4 || Data[2] == 5) {
                        x += 16;
                        width = 39; height = 16;
                    } else if (Data[2] == 6 || Data[2] == 7) {
                        x -= 39;
                        width = 39; height = 16;
                    }
                    break;
                case 281:
                    if (Data[2] % 0x10 > 0)
                        y += 16;
                    else
                        y -= 15;
                    x += 10;
                    width = 13; height = 15;
                    break;
                case 282:
                    x -= 16;
                    width = 32; height = (Data[2] % 0x10 + 4) * 16;
                    break;
                case 283:
                    y -= 32;
                    width = 54; height = 55;
                    break;
                case 284:
                    y -= 2;
                    width = 18; height = 18;
                    break;
                case 285:
                    y -= 8;
                    width = 46; height = 24;
                    break;
                //case 289:
                //  Expandable Blocks don't need to be modified
                case 290:
                    x -= 9; y -= 13;
                    width = 38; height = 28;
                    break;
                case 292:
                    y -= 32;
                    width = 32; height = 48;
                    break;
                case 298:
                    width = Math.Max(16 * (Data[2] % 0x10 + 1), 32);
                    height = 16 * (Data[2] / 0x10 + 1);
                    int spikes = Data[5] % 0x10;
                    if (width == 32 && (spikes == 1 || spikes == 2))
                        width = 48;
                    if (width == 32 && spikes == 3)
                        width = 64;
                    if (height == 16 && (spikes == 4 || spikes == 5))
                        height = 32;
                    if (height == 16 && spikes == 6)
                        height = 48;
                    break;
                case 300:
                    y += -8 + Data[2] % 0x10; x-= 64;
                    width = 128; height = 16;
                    break;
                case 303:
                    x += 4; y -= 61;
                    width = 72; height = 74;
                    break;
                case 260:
                case 304:
                    y -= 128;
                    width = 64; height = 144;
                    break;
                case 261:
                case 307:
                    width = 64; height = 144;
                    break;
                case 262:
                case 308:
                    width = 144; height = 64;
                    break;
                case 263:
                case 309:
                    x -= 128;
                    width = 144; height = 64;
                    break;
                case 323:
                    height = 32;
                    width = 32 * (Data[2] + 2);
                    break;
            }

            return new Rectangle(x, y, width, height);
        }

        public Rectangle getRectB() {
            Rectangle rect = getRect();
            int t = rect.X;
            rect.X = (int)Math.Floor((float)rect.X / 16);
            rect.Width += t - 16 * rect.X;
            t = rect.Y;
            rect.Y = (int)Math.Floor((float)rect.Y / 16);
            rect.Height += t - 16 * rect.Y;
            rect.Width = (int)Math.Ceiling((float)rect.Width / 16);
            rect.Height = (int)Math.Ceiling((float)rect.Height / 16);
            return rect;
        }

        public bool Render(Graphics g) {
            int RenderX = X * 16, RenderX2 = RenderX;
            int RenderY = Y * 16, RenderY2 = RenderY;
            Bitmap img = null;

            bool customRendered = true;

            switch (this.Type)
            {
                case 23:
                    g.DrawImage(Properties.Resources.PiranhaplantTube, RenderX, RenderY - 28, 32, 28);
                    break;
                case 24:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY + 32, 32, 28);
                    break;
                case 25:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX + 32, RenderY, 28, 32);
                    break;
                case 26:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 28, RenderY, 28, 32);
                    break;
                case 27:
                    for (int l = 0; l <= Data[2] / 0x10; l++) {
                        if (l == Data[2] / 0x10) {
                            g.DrawImage(Properties.Resources.BulletBillCannonTop, RenderX, RenderY - 16, 16, 32);
                        } else {
                            g.DrawImage(Properties.Resources.BulletBillCannon, RenderX, RenderY, 16, 16);
                            RenderY -= 16;
                        }
                    }
                    break;
                case 28:
                    g.DrawImage(Properties.Resources.BomOmb, RenderX, RenderY - 2, 16, 18);
                    break;
                case 31:
                    g.DrawImage(Properties.Resources.CheepCheep, RenderX, RenderY, 21, 16);
                    break;
                case 32:
                    if (Data[5] == 0x10)
                        img = Properties.Resources.EndingFlagRed;
                    else
                        img = Properties.Resources.EndingFlag;
                    g.DrawImage(img, RenderX - 29, RenderY - 119, 27, 24);
                    break;
                case 33:
                    if (Data[2] % 0x10 > 0) {
                        img = Properties.Resources.SpringGiant;
                        RenderY -= 16;
                        RenderX -= 8;
                    } else
                        img = Properties.Resources.Spring;
                    if (Data[2] / 0x10 > 0)
                        RenderX += 8;
                    g.DrawImage(img, RenderX, RenderY, img.Width, img.Height);
                    break;
                case 34:
                    g.DrawImage(Properties.Resources.RedCoinRing, RenderX - 7, RenderY - 26, 30, 42);
                    break;
                case 36:
                    g.DrawImage(Properties.Resources.Thwomp, RenderX, RenderY, 32, 36);
                    break;
                case 37:
                    g.DrawImage(Properties.Resources.Spiny, RenderX, RenderY, 20, 16);
                    break;
                case 38:
                    g.DrawImage(Properties.Resources.Boo, RenderX, RenderY, 24, 20);
                    break;
                case 42:
                    g.DrawImage(Properties.Resources.ChainChompLog, RenderX, RenderY - 48, 16, 48);
                    if (Data[2] % 0x10 > 0)
                        g.DrawImage(Properties.Resources.ChainChomp, RenderX - 29, RenderY - 28, 44, 28);
                    break;
                case 48:
                    g.DrawImage(Properties.Resources.TubeBubbles, RenderX, RenderY - 90, 32, 74);
                    break;
                case 49:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY + 32, 32, 74);
                    break;
                case 50:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX + 32, RenderY, 74, 32);
                    break;
                case 51:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 90, RenderY, 74, 32);
                    break;
                case 52:
                    if (Data[2] > 0)
                        img = Properties.Resources.BuzzyBeetleU;
                    else
                        img = Properties.Resources.BuzzyBeetle;
                    g.DrawImage(img, RenderX, RenderY, 16, 16);
                    break;
                case 53:
                    g.DrawImage(Properties.Resources.DryBones, RenderX, RenderY - 10, 17, 26);
                    break;
                case 54:
                    g.DrawImage(Properties.Resources.FireBall, RenderX, RenderY, 16, 29);
                    break;
                case 55:
                    g.DrawImage(Properties.Resources.BulletBill, RenderX, RenderY, 21, 16);
                    break;
                case 56:
                    RenderY2 = (Data[2] % 0x10) * 10;
                    if (Data[2] / 0x10 > 0) {
                        RenderX -= RenderY2;
                        RenderY += RenderY2;
                    }
                    while (!(RenderX == RenderX2 + RenderY2 + 10)) {
                        if (RenderX == RenderX2)
                            g.DrawImage(Properties.Resources.FireBarMiddle, RenderX + 4, RenderY + 4, 8, 8);
                        else
                            g.DrawImage(Properties.Resources.FireBarBall, RenderX, RenderY, 15, 15);
                        RenderX += 10; RenderY -= 10;
                    }
                    break;
                case 57:
                    if (Data[2] % 0x10 != 1 && Data[2] % 0x10 != 2) {
                        if (Data[4] / 0x10 == 1)
                            RenderY += 8;
                        if (Data[5] % 0x10 == 1)
                            RenderX += 8;
                    }
                    if (Data[2] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX - 4, RenderY - 4, 24, 24);
                    else if (Data[2] % 0x10 == 2) {
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX - 4, RenderY - 4, 24, 24);
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX + 20, RenderY - 20, 24, 24);
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX + 44, RenderY - 4, 24, 24);
                    } else {
                        g.DrawImage(Properties.Resources.Coin, RenderX, RenderY, 16, 16);
                    }
                    break;
                case 59:
                    g.DrawImage(Properties.Resources.HammerBro, RenderX, RenderY - 18, 18, 34);
                    break;
                case 64:
                    if (Data[2] % 0x10 == 1) {
                        img = Properties.Resources.WhompL;
                        RenderX -= 38; RenderY -= 74;
                    } else {
                        img = Properties.Resources.Whomp;
                        RenderX -= 29; RenderY -= 57;
                    }
                    g.DrawImage(img, RenderX, RenderY, img.Width, img.Height);
                    break;
                case 66:
                    img = Properties.Resources.PSwitch;
                    if (Data[2] % 0x10 == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    g.DrawImage(img, RenderX, RenderY, 16, 18);
                    break;
                case 68:
                case 69:
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY, 16, 16);
                    for (int l = 0; l < Data[2] - 1; l++) {
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX, RenderY, 16, 16);
                    }
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 16, RenderY, 16, 16);
                    break;
                case 73:
                    g.DrawImage(Properties.Resources.HangingPlatform, RenderX, RenderY, 192, 32);
                    RenderX += 92;
                    for (int l = 0; l < 6; l++) {
                        RenderY -= 32;
                        g.DrawImage(Properties.Resources.HangingPlatformChain, RenderX, RenderY, 8, 32);
                    }
                    break;
                case 74:
                    g.DrawImage(Properties.Resources.TiltingRock, RenderX, RenderY - 8, 64, 40);
                    break;
                case 75:
                    g.DrawImage(Properties.Resources.SeeSaw, RenderX, RenderY, 256, 16);
                    break;
                case 76:
                    RenderX2 = RenderX + 16 * (Data[2] % 0x10) - 10;
                    g.DrawImage(Properties.Resources.ScalePlatformBolt, RenderX - 2, RenderY - 10, 13, 13);
                    g.DrawImage(Properties.Resources.ScalePlatformBolt, RenderX2, RenderY - 10, 13, 13);
                    Pen rope = new Pen(Color.FromArgb(49, 24, 74));
                    g.DrawLine(Pens.White, RenderX + 8, RenderY - 9, RenderX2 + 2, RenderY - 9);
                    g.DrawLine(rope, RenderX + 9, RenderY - 8, RenderX2 + 1, RenderY - 8);
                    RenderY2 = RenderY + 16 * (Data[2] / 0x10) - 8;
                    int RenderY3 = RenderY + 16 * (Data[3] % 0x10) - 8;
                    g.DrawLine(Pens.White, RenderX - 1, RenderY, RenderX - 1, RenderY2 - 1);
                    g.DrawLine(rope, RenderX, RenderY + 1, RenderX, RenderY2 - 1);
                    g.DrawLine(Pens.White, RenderX2 + 9, RenderY + 3, RenderX2 + 9, RenderY3 - 1);
                    g.DrawLine(rope, RenderX2 + 10, RenderY + 1, RenderX2 + 10, RenderY3 - 1);
                    g.DrawImage(Properties.Resources.ScalePlatformEnd, RenderX2 - 14, RenderY3, 48, 16);
                    g.DrawImage(Properties.Resources.ScalePlatformEnd, RenderX - 24, RenderY2, 48, 16);
                    break;
                case 77:
                case 78:
                    int w = Data[2], s = 2;
                    if (this.Type == 78) { w = Math.Max(0, w - 1); s = 1; RenderX -= 8; }
                    RenderX -= (w * s + 1) * 8;
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY, 16, 16);
                    for (int l = 0; l < w * s; l++) {
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX, RenderY, 16, 16);
                    }
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 16, RenderY, 16, 16);
                    break;
                case 79:
                    g.DrawImage(Properties.Resources.Spinning3PointedPlatform, RenderX - 28, RenderY - 34, 92, 85);
                    break;
                case 82:
                    g.DrawImage(Properties.Resources.SpinningRectanglePlatform, RenderX - 24, RenderY - 8, 64, 32);
                    break;
                case 86:
                    g.DrawImage(Properties.Resources.SpinningTrianglePlatform, RenderX - 14, RenderY - 6, 44, 39);
                    break;
                case 89:
                    g.DrawImage(Properties.Resources.Snailicorn, RenderX, RenderY - 16, 30, 32);
                    break;
                case 90:
                    if (Data[2] == 1)
                        g.DrawImage(Properties.Resources.WigglerL, RenderX, RenderY - 44, 92, 60);
                    else
                        g.DrawImage(Properties.Resources.Wiggler, RenderX, RenderY - 14, 52, 30);
                    break;
                case 91:
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY + 8, 16, 16);
                    g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX + 16, RenderY + 8, 16, 16);
                    g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX + 32, RenderY + 8, 16, 16);
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 48, RenderY + 8, 16, 16);
                    break;
                case 92:
                    if (Data[2] % 0x10 == 1) {
                        g.DrawImage(Properties.Resources.EelNonMoving, RenderX + 3, RenderY + 7, 25, 23);
                    } else if (Data[2] / 0x10 == 1) {
                        img = Properties.Resources.Eel;
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        g.DrawImage(img, RenderX + 45, RenderY + 3, 104, 28);
                    } else if (Data[4] % 0x10 == 1) {
                        g.DrawImage(Properties.Resources.Eel, RenderX + 12, RenderY + 3, 104, 28);
                    } else {
                        Rectangle r = new Rectangle(RenderX + 12, RenderY + 3, 104, 28);
                        g.FillRectangle(Brushes.Black, r);
                        g.DrawString(LanguageManager.Get("Sprites", "Eel"), NSMBGraphics.SmallInfoFont, Brushes.White, r);
                    }
                    break;
                case 93:
                    if (Data[2] / 0x10 % 2 == 1) {
                        img = Properties.Resources.ArrowSignRotate45;
                        if (Data[2] % 0x10 == 1)
                            img = Properties.Resources.ArrowSignRotate45F;
                    } else {
                        img = Properties.Resources.ArrowSign;
                        if (Data[2] % 0x10 == 1)
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    img = RotateBitmap(img, -Data[2] / 0x20 * 90);
                    if (Data[4] % 0x10 == 1)
                        img = new Bitmap(img, 24, 24);
                    if (Data[2] / 0x10 % 2 == 1) {
                        RenderX += (img.Width / 9) * BtoI(Data[2] / 0x10 > 4);
                        RenderY += (img.Width / 9) * BtoI(Data[2] / 0x10 == 3 || Data[2] / 0x10 == 5);
                    }
                    g.DrawImage(img, RenderX - (img.Width - 16) / 2, RenderY - (img.Height - 16), img.Width, img.Height);
                    break;
                case 94:
                    if (Data[2] > 0)
                        g.DrawImage(Properties.Resources.SwooperLarge, RenderX - 6, RenderY - 16, 26, 37);
                    else
                        g.DrawImage(Properties.Resources.Swooper, RenderX, RenderY, 16, 18);
                    break;
                case 95:
                    g.DrawImage(Properties.Resources.SpinBoard, RenderX - 6, RenderY, 26, 26);
                    break;
                case 96:
                    g.DrawImage(Properties.Resources.SeaWeed, RenderX - 32, RenderY - 127, 90, 147);
                    break;
                case 99:
                    g.DrawImage(Properties.Resources.Spinning4PointedPlatform, RenderX - 38, RenderY - 29, 91, 75);
                    break;
                case 102:
                    g.DrawImage(Properties.Resources.SpikeBallSmall, RenderX, RenderY, 30, 32);
                    break;
                case 103:
                    g.DrawImage(Properties.Resources.Dorrie, RenderX - 51, RenderY - 59, 115, 76);
                    break;
                case 104:
                    g.DrawImage(Properties.Resources.Tornado, RenderX - 88, RenderY - 130, 191, 150);
                    break;
                case 105:
                    g.DrawImage(Properties.Resources.WhirlPool, RenderX - 78, RenderY - 164, 165, 182);
                    break;
                case 106:
                    g.DrawImage(Properties.Resources.RedCoin, RenderX, RenderY, 16, 16);
                    break;
                case 107:
                    img = Properties.Resources.QSwitch;
                    if (Data[2] == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    g.DrawImage(img, RenderX, RenderY, 16, 18);
                    break;
                case 108:
                    img = Properties.Resources.RedSwitch;
                    if (Data[2] == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    g.DrawImage(img, RenderX, RenderY, 16, 18);
                    break;
                case 109:
                    int shift = Data[2] / 0x10;
                    if (shift == 1 || shift == 3)
                        RenderX += 8;
                    if (shift == 2 || shift == 3)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.ElectricBall, RenderX - 6, RenderY - 6, 28, 27);
                    break;
                case 111:
                    g.DrawImage(Properties.Resources.Log, RenderX - 128, RenderY - 32, 256, 64);
                    break;
                case 113:
                    g.DrawImage(Properties.Resources.CheepChomp, RenderX, RenderY, 60, 47);
                    break;
                case 115:
                    g.DrawImage(Properties.Resources.SpikeBallLarge, RenderX, RenderY - 42, 63, 58);
                    break;
                case 114:
                case 118:
                    if (this.Type == 114)
                        img = Properties.Resources.SmallFlamethrower;
                    else
                        img = Properties.Resources.Flamethrower;
                    switch (Data[2] % 0x40) {
                        case 0:
                            g.DrawImage(img, RenderX, RenderY, img.Width, 16); break;
                        case 1:
                            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            g.DrawImage(img, RenderX - (img.Width - 16), RenderY, img.Width, 16); break;
                        case 2:
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            g.DrawImage(img, RenderX, RenderY - (img.Height - 16), 16, img.Height); break;
                        case 3:
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            g.DrawImage(img, RenderX, RenderY, 16, img.Height); break;
                    }
                    break;
                case 116:
                    g.DrawImage(Properties.Resources.WaterBug, RenderX - 16, RenderY - 5, 41, 21);
                    break;
                case 117:
                    g.DrawImage(Properties.Resources.FlyingBlock, RenderX - 12, RenderY - 8, 44, 23);
                    break;
                case 119:
                    int sz = Data[5] / 0x10;
                    if (sz == 0) sz = 1;
                    g.DrawImage(ScaleBitmap(Properties.Resources.Pendulum, sz * 64, sz * 68), RenderX - sz * 32, RenderY - sz * 6);
                    break;
                case 120:
                    g.DrawImage(Properties.Resources.PiranhaplantGround, RenderX, RenderY + 3, 33, 29);
                    break;
                case 122:
                    g.DrawImage(Properties.Resources.GiantPiranhaplant, RenderX - 37, RenderY - 48, 64, 64);
                    break;
                case 123:
                    g.DrawImage(Properties.Resources.FirePiranhaplant, RenderX - 8, RenderY + 2, 40, 30);
                    break;
                case 126:
                    Bitmap img2 = null;
                    RenderX2 = RenderX - 16;
                    for (int l = 0; l < Data[5] / 0x10; l++ ) {
                        if (l == 0) {
                            img = Properties.Resources.DrawBridgeEnd;
                            img2 = img.Clone() as Bitmap; img2.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        } else if (l == Data[5] / 0x10 - 1) {
                            img = Properties.Resources.DrawBridgeHinge;
                            img2 = img.Clone() as Bitmap; img2.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        } else if (l == 1) {
                            img = Properties.Resources.DrawBridgeSection;
                            img2 = img.Clone() as Bitmap; img2.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        g.DrawImage(img2, RenderX, RenderY, 16, 16);
                        g.DrawImage(img, RenderX2, RenderY, 16, 16);
                        RenderX += 16; RenderX2 -= 16;
                    }
                    if (Data[5] / 0x10 == 0)
                        g.DrawImage(Properties.Resources.DrawBridgeHinge, RenderX, RenderY, 16, 16);
                    break;
                case 127:
                    g.DrawImage(Properties.Resources.GiantSpinningPlatform, RenderX - 144, RenderY - 104, 288, 208);
                    break;
                case 128:
                    g.DrawImage(Properties.Resources.WarpCannon, RenderX - 9, RenderY - 26, 66, 90);
                    int world = Data[5] / 0x10;
                    if (world >= 5 && world <= 8)
                        world -= 5;
                    else
                        world = 0;
                    img = Properties.Resources.WarpWorlds;
                    img = img.Clone(new Rectangle(world * 18, 0, 18, 19), img.PixelFormat);
                    g.DrawImage(img, RenderX + 26, RenderY + 23, 18, 19);
                    break;
                case 130:
                    g.DrawImage(Properties.Resources.CheepCheep, RenderX, RenderY, 21, 16);
                    break;
                case 131:
                case 132:
                    g.DrawImage(Properties.Resources.MidpointFlag, RenderX, RenderY, 22, 33);
                    break;
                case 136:
                    for (int l = 0; l <= Data[2]; l++) {
                        g.DrawImage(Properties.Resources.PokeySection, RenderX, RenderY, 17, 16);
                        RenderY -= 16;
                    }
                    g.DrawImage(Properties.Resources.PokeyHead, RenderX, RenderY - 9, 18, 25);
                    break;
                case 144:
                    g.DrawImage(Properties.Resources.SpikedBlock, RenderX - 2, RenderY - 7, 19, 26);
                    RenderY2 = Data[2] % 0x10;
                    if (RenderY2 == 2) RenderY2 = 3;
                    if (RenderY2 <= 3)
                        g.DrawImage(Properties.Resources.FlyingQBlockOverrides.Clone(new Rectangle(RenderY2 * 16,
                        0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare), RenderX, RenderY + 1, 16, 16);
                    break;
                case 146:
                    g.DrawImage(Properties.Resources.StarGate, RenderX, RenderY, 32, 8);
                    break;
                case 147:
                    RenderX -= Data[2] * 8 + 6;
                    g.DrawImage(Properties.Resources.BumpPlatformLeft, RenderX, RenderY, 14, 10);
                    RenderX += 14;
                    for (int l = 0; l < Data[2] - 1; l++) {
                        g.DrawImage(Properties.Resources.BumpPlatformSection, RenderX, RenderY, 16, 10);
                        RenderX += 16;
                    }
                    if (Data[2] > 0)
                        g.DrawImage(Properties.Resources.BumpPlatformRight, RenderX, RenderY, 14, 10);
                    break;
                case 148:
                    g.DrawImage(Properties.Resources.Goomba, RenderX, RenderY - 2, 16, 18);
                    break;
                case 149:
                    switch (Data[2]) {
                        case 1:
                            img = Properties.Resources.KoopaRed;
                            break;
                        case 2:
                        case 3:
                            img = Properties.Resources.KoopaBlue;
                            break;
                        default:
                            img = Properties.Resources.KoopaGreen;
                            break;
                    }
                    g.DrawImage(img, RenderX, RenderY - 13, 16, 29);
                    break;
                case 150:
                    switch (Data[2] % 0x10) {
                        case 1:
                            img = Properties.Resources.ParakoopaRed;
                            break;
                        case 2:
                            img = Properties.Resources.ParakoopaBlue;
                            break;
                        default:
                            img = Properties.Resources.ParakoopaGreen;
                            break;
                    }
                    g.DrawImage(img, RenderX, RenderY - 16, 19, 32);
                    break;
                case 152:
                    g.DrawImage(Properties.Resources.SwitchBlock, RenderX, RenderY, 16, 16);
                    break;
                case 155:
                    Rectangle rect = this.getRect();
                    rect.Offset(1, 1);
                    g.DrawRectangle(Pens.Black, rect);
                    rect.Offset(-1, -1);
                    g.DrawRectangle(Pens.White, rect);
                    g.DrawString(LanguageManager.Get("Sprites", "Warp"), NSMBGraphics.SmallInfoFont, Brushes.Black, RenderX + 1, RenderY + 1);
                    g.DrawString(LanguageManager.Get("Sprites", "Warp"), NSMBGraphics.SmallInfoFont, Brushes.White, RenderX, RenderY);
                    break;
                case 157:
                    g.DrawImage(Properties.Resources.FireBro, RenderX, RenderY - 15, 18, 31);
                    break;
                case 158:
                    g.DrawImage(Properties.Resources.BoomerangBro, RenderX, RenderY - 18, 18, 34);
                    break;
                case 162:
                    RenderY += 24;
                    RenderX += 6;
                    g.DrawImage(Properties.Resources.MushroomStalkTop, RenderX, RenderY, 20, 8);
                    RenderY += 8;
                    g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY, 20, 16);
                    for (int l = 0; l <= Data[4] % 0x10; l++) {
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY, 20, 16);
                    }
                    RenderX = X * 16 - 16 * (Data[3] / 0x10 + 1);
                    RenderY = Y * 16;
                    img = Properties.Resources.MushroomEdge;
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    RenderX += 32;
                    for (int l = 0; l < Data[3] / 0x10; l++) {
                        g.DrawImage(Properties.Resources.MushroomSection, RenderX, RenderY, 32, 24);
                        RenderX += 32;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    break;
                case 173:
                    for (int l = 0; l < Data[2] % 0x10 + 4; l++) {
                        g.DrawImage(Properties.Resources.Rope, RenderX - 3, RenderY, 8, 16);
                        RenderY += 16;
                    }
                    break;
                case 180:
                    if (Data[2] % 0x10 == 1)
                        img = Properties.Resources.FenceKoopaRed;
                    else
                        img = Properties.Resources.FenceKoopaGreen;
                    g.DrawImage(img, RenderX, RenderY, 18, 26);
                    break;
                case 183:
                    g.DrawImage(Properties.Resources.Lakitu, RenderX, RenderY - 23, 26, 39);
                    break;
                case 186:
                    g.DrawImage(Properties.Resources.Paragoomba, RenderX, RenderY - 12, 24, 28);
                    break;
                case 187:
                    g.DrawImage(Properties.Resources.ManualPlatform, RenderX, RenderY, 112, 16);
                    if (Data[2] % 0x10 == 0 || Data[2] % 0x10 == 2)
                        g.DrawImage(Properties.Resources.UpArrow, RenderX + 51, RenderY + 4, 10, 9);
                    else if (Data[2] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.DownArrow, RenderX + 51, RenderY + 4, 10, 9);
                    break;
                case 189:
                    g.DrawImage(Properties.Resources.Pipe, RenderX, RenderY - 48, 32, 64);
                    break;
                case 191:
                    RenderY2 = Data[4] / 0x10;
                    if (RenderY2 == 0) RenderY2 = 8;
                    g.DrawImage(Properties.Resources.QBlock, RenderX, RenderY + (RenderY2 - 5) * 16, 16, 16);
                    if (Data[2] % 0x10 <= 8)
                        g.DrawImage(Properties.Resources.HangingBlockOverrides.Clone(new Rectangle((Data[2] % 0x10) * 16, 
                        0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare), RenderX, RenderY + (RenderY2 - 5) * 16, 16, 16);
                    g.DrawLine(Pens.White, RenderX + 8, RenderY + (RenderY2 - 5) * 16, RenderX + 8, RenderY - 80);
                    g.DrawLine(Pens.White, RenderX + 9, RenderY + (RenderY2 - 5) * 16, RenderX + 9, RenderY - 80);
                    break;
                case 193:
                    g.DrawImage(Properties.Resources.DryBonesLarge, RenderX, RenderY - 28, 25, 44);
                    break;
                case 194:
                    g.DrawImage(Properties.Resources.GiantThwomp, RenderX, RenderY, 64, 74);
                    break;
                case 197:
                    for (int l = 1; l <= Math.Max(1, Data[2] % 0x10); l++) {
                        for (int m = 1; m <= Math.Max(1, Data[2] / 0x10); m++) {
                            g.DrawImage(Properties.Resources.SwitchBlock2, RenderX, RenderY, 16, 16);
                            RenderX += 16;
                        }
                        RenderY += 16;
                        RenderX = RenderX2;
                    }
                    break;
                case 204:
                    g.DrawImage(Properties.Resources.JumpingFlame, RenderX, RenderY - 7, 27, 23);
                    break;
                case 205:
                    g.DrawImage(Properties.Resources.FlameChomp, RenderX, RenderY - 27, 43, 33);
                    break;
                case 206:
                    g.DrawImage(Properties.Resources.GhostGoo, RenderX, RenderY - 32, 256, 48);
                    break;
                case 207:
                    if (Data[2] == 0)
                        img = Properties.Resources.CheepCheepGiant;
                    else
                        img = Properties.Resources.CheepCheepGiantGreen;
                    g.DrawImage(img, RenderX - 20, RenderY - 32, 63, 52);
                    break;
                case 209:
                    g.DrawImage(Properties.Resources.GiantHammerBro, RenderX, RenderY - 15, 26, 31);
                    break;
                case 211:
                    g.DrawImage(Properties.Resources.Blooper, RenderX, RenderY - 16, 26, 32);
                    break;
                case 219:
                    switch (Data[2] / 0x10) {
                        case 1:
                            g.DrawImage(Properties.Resources.SpinyBeetleLeft, RenderX - 4, RenderY, 20, 16); break;
                        case 2:
                            g.DrawImage(Properties.Resources.SpinyBeetleDown, RenderX, RenderY, 16, 20); break;
                        case 3:
                            g.DrawImage(Properties.Resources.SpinyBeetleRight, RenderX, RenderY, 20, 16); break;
                        default:
                            g.DrawImage(Properties.Resources.SpinyBeetleUp, RenderX, RenderY - 4, 16, 20); break;
                    }
                    break;
                case 220:
                    g.DrawImage(Properties.Resources.BowserJr, RenderX, RenderY - 25, 31, 41);
                    break;
                case 222:
                    if (Data[2] / 0x10 > 0)
                        RenderX += 8;
                    if (Data[2] % 0x10 > 0)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.MiniGoomba, RenderX, RenderY, 9, 9);
                    break;
                case 223:
                    g.DrawImage(Properties.Resources.FlipGateSmall, RenderX, RenderY, 32, 32);
                    break;
                case 224:
                    g.DrawImage(Properties.Resources.FlipGateLarge, RenderX, RenderY, 192, 64);
                    break;
                case 226:
                    g.DrawImage(Properties.Resources.HangingScuttleBug, RenderX - 14, RenderY + 80, 44, 37);
                    g.DrawLine(new Pen(Color.White, 2), RenderX + 8, RenderY, RenderX + 8, RenderY + 82);
                    break;
                case 227:
                    g.DrawImage(Properties.Resources.MoneyBag, RenderX, RenderY - 5, 23, 21);
                    break;
                case 228:
                    g.DrawImage(Properties.Resources.RouletteBlock, RenderX, RenderY, 20, 20);
                    break;
                case 233:
                    for (int l = 0; l < Data[2] % 0x10 + 4; l++) {
                        g.DrawImage(Properties.Resources.SwingingPole, RenderX - 3, RenderY, 7, 16);
                        RenderY += 16;
                    }
                    g.DrawImage(Properties.Resources.LineAttachment, RenderX2 - 11, RenderY2 - 11, 22, 22);
                    break;
                case 235:
                    switch (Data[2] / 0x10) {
                        case 1:
                            RenderX += 8; break;
                        case 2:
                            RenderX -= 8; break;
                    }
                    switch (Data[2] % 0x10) {
                        case 1:
                            RenderY += 8; break;
                        case 2:
                            RenderY -= 8; break;
                    }
                    g.DrawImage(Properties.Resources.StarCoin, RenderX, RenderY, 32, 32);
                    break;
                case 236:
                    if (Data[5] / 0x10 > 0)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.SpinningSquarePlatform, RenderX - 16, RenderY - 16, 48, 48);
                    break;
                case 237:
                    g.DrawImage(Properties.Resources.Broozer, RenderX, RenderY - 15, 45, 31);
                    break;
                case 238:
                    g.DrawImage(Properties.Resources.MushroomStalkTop, RenderX + 6, RenderY + 24, 20, 8);
                    RenderY += 32;
                    for (int l = 0; l < 5; l++){
                        g.DrawImage(Properties.Resources.MushroomStalk, RenderX + 6, RenderY, 20, 16);
                        RenderY += 16;
                    }
                    RenderX = X * 16 - 16 * (Data[2] / 0x10 + 1);
                    RenderY = Y * 16;
                    img = Properties.Resources.PurpleMushroomEdge;
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    RenderX += 32;
                    for (int l = 0; l < Data[2] / 0x10; l++) {
                        g.DrawImage(Properties.Resources.PurpleMushroomSection, RenderX, RenderY, 32, 24);
                        RenderX += 32;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY, 32, 24);
                    break;
                case 239:
                    g.DrawImage(Properties.Resources.RiseLowerMushroomStalkTop, RenderX, RenderY + 24, 16, 8);
                    RenderY += 32;
                    for (int l = 0; l <= Math.Max(Data[2] / 0x10, Data[3] % 0x10); l++) {
                        g.DrawImage(Properties.Resources.RiseLowerMushroomStalk, RenderX, RenderY, 16, 16);
                        RenderY += 16;
                    }
                    RenderX -= (Data[2] % 0x10 + 1) * 8;
                    if (Data[2] / 0x10 > Data[3] % 0x10) {
                        img = Properties.Resources.LoweringMushroomEdge;
                        img2 = Properties.Resources.LoweringMushroomMiddle;
                    } else {
                        img = Properties.Resources.RisingMushroomEdge;
                        img2 = Properties.Resources.RisingMushroomMiddle;
                    }
                    g.DrawImage(img, RenderX, RenderY2, 16, 24);
                    for (int l = 0; l < Data[2] % 0x10; l++) {
                        RenderX += 16;
                        g.DrawImage(img2, RenderX, RenderY2, 16, 24);
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX + 16, RenderY2, 16, 24);
                    break;
                case 241:
                    img = Properties.Resources.RotatingCannon;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    for (int l = 0; l <= Math.Min(7, (int)Data[4]); l++) {
                        if ((Data[3] & (1 << l)) > 0)
                            g.DrawImage(Properties.Resources.RotatingCannonEmpty, RenderX + 2, RenderY, 13, 16);
                        else if ((Data[2] & (1 << l)) > 0)
                            g.DrawImage(img, RenderX - 3, RenderY, 19, 16);
                        else
                            g.DrawImage(Properties.Resources.RotatingCannon, RenderX + 1, RenderY, 19, 16);
                        RenderY -= 16;
                    }
                    break;
                case 242:
                    RenderX2 = Data[2] % 0x10;
                    if (Data[2] / 0x10 == 1) {
                        if (Data[3] % 0x10 == 1)
                            g.DrawImage(Properties.Resources.ExpandedMushroomS, RenderX - 32, RenderY, 80, 16);
                        else
                            g.DrawImage(Properties.Resources.ExpandedMushroomL, RenderX - 72, RenderY, 160, 16);
                        RenderX2++;
                        RenderY2 -= 16;
                    }
                    else
                        g.DrawImage(Properties.Resources.ContractedMushroom, RenderX - 8, RenderY, 32, 32);
                    RenderY2 += 32;
                    for (int l = 0; l < RenderX2; l++) {
                        if (l == 0)
                            g.DrawImage(Properties.Resources.ExpandMushroomStalkTop, RenderX, RenderY2, 16, 16);
                        else
                            g.DrawImage(Properties.Resources.ExpandMushroomStalk, RenderX, RenderY2, 16, 16);
                        RenderY2 += 16;
                    }
                    break;
                case 243:
                    g.DrawImage(Properties.Resources.RoofSpiny, RenderX, RenderY, 17, 16);
                    break;
                case 244:
                    switch (Data[2] / 0x10) {
                        case 1:
                            RenderX -= 8; break;
                        case 2:
                            RenderX += 8; break;
                    }
                    g.DrawImage(Properties.Resources.BouncingMushroom, RenderX - 40, RenderY, 96, 32);
                    RenderY += 32;
                    RenderX += 3;
                    for (int l = 0; l < Data[2] % 0x10; l++) {
                        g.DrawImage(Properties.Resources.BouncingMushroomStalk, RenderX, RenderY, 10, 16);
                        RenderY += 16;
                    }
                    break;
                case 246:
                    g.DrawImage(Properties.Resources.Barrel, RenderX - 16, RenderY - 8, 48, 49);
                    break;
                case 248:
                    g.DrawImage(Properties.Resources.BalloonBoo, RenderX - 53, RenderY - 106, 109, 108);
                    break;
                case 249:
                    RenderX -= Data[2] % 0x10 * 8 + 24;
                    RenderY += Data[3] / 0x10 * 16 + 88;
                    int XOff = Data[2] % 0x10 * 16 + 48;
                    for (int l = 0; l < Data[3] / 0x10 + 4; l++) {
                        RenderY -= 16;
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX, RenderY, 16, 16);
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX + XOff, RenderY, 16, 16);
                    }
                    for (int l = 0; l < Data[2] % 0x10 + 2; l++){
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX, RenderY, 16, 16);
                    }
                    g.DrawImage(Properties.Resources.LineAttachment, X * 16 - 3, Y * 16 + 5, 22, 22);
                    break;
                case 250:
                    g.DrawImage(Properties.Resources.Crow, RenderX, RenderY, 22, 14);
                    break;
                case 252:
                    g.DrawImage(Properties.Resources.BanzaiBillCannon, RenderX - 32, RenderY - 64, 64, 80);
                    break;
                case 254:
                    g.DrawImage(Properties.Resources.Kabomb, RenderX, RenderY - 3, 16, 19);
                    break;
                case 256:
                    img = Properties.Resources.ThruWallPlatform;
                    if (Data[4] / 0x10 == 0)
                        g.DrawImage(img, RenderX - 80, RenderY, 96, 16);
                    else {
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        g.DrawImage(img, RenderX, RenderY, 96, 16);
                    }
                    break;
                case 265:
                    g.DrawImage(Properties.Resources.Hand, RenderX + 8, RenderY, 16, 20);
                    break;
                case 268:
                    g.DrawImage(Properties.Resources.UnderwaterBounceBall, RenderX - 26, RenderY - 26, 53, 53);
                    break;
                case 272:
                    if (Data[2] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.SnowBranchLeft, RenderX - 31, RenderY - 12, 34, 23);
                    else
                        g.DrawImage(Properties.Resources.SnowBranchRight, RenderX + 15, RenderY - 12, 30, 23);
                    break;
                case 273:
                    g.DrawImage(Properties.Resources.SnowballThrower, RenderX, RenderY, 32, 32);
                    break;
                case 274:
                    img = Properties.Resources.SinkingSnowEdge;
                    g.DrawImage(img, RenderX - 16, RenderY, 16, 35);
                    for (int l = 0; l <= Data[2] % 0x10; l++) {
                        g.DrawImage(Properties.Resources.SinkingSnow, RenderX, RenderY, 16, 35);
                        RenderX += 16;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY, 16, 35);
                    break;
                case 277:
                    int direction = Data[5] % 8;
                    if (direction == 0 || direction == 2 || direction == 4 || direction == 6)
                        img = Properties.Resources.Arrow;
                    else
                        img = Properties.Resources.ArrowRotate45;
                    if (direction == 2 || direction == 3)
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    if (direction == 4 || direction == 5)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    if (direction == 6 || direction == 7)
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX, RenderY, 32, 32);
                    break;
                case 278:
                    g.DrawImage(ScaleBitmap(Properties.Resources.GroundpoundGoo, (Data[2] % 0x10 + 1) * 16,
                        (Data[2] / 0x10 + 1) * 16), RenderX, RenderY);
                    break;
                case 279:
                    img = Properties.Resources.OneWayDoor;
                    if (Data[2] == 0 || Data[2] == 1) {
                        img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        RenderY -= 39;
                    } else if (Data[2] == 2 || Data[2] == 3) {
                        RenderY += 16;
                    } else if (Data[2] == 4 || Data[2] == 5) {
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        RenderX += 16;
                    } else if (Data[2] == 6 || Data[2] == 7) {
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        RenderX -= 39;
                    }
                    if (Data[2] == 0 || Data[2] == 2)
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    else if (Data[2] == 4 || Data[2] == 6)
                        img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY, img.Width, img.Height);
                    break;
                case 281:
                    img = Properties.Resources.PipeCaterpiller;
                    if (Data[2] % 0x10 > 0) {
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        g.DrawImage(img, RenderX + 10, RenderY + 16, 13, 15);
                    } else
                        g.DrawImage(img, RenderX + 10, RenderY - 15, 13, 15);
                    break;
                case 282:
                    RenderX -= 16;
                    g.DrawImage(Properties.Resources.VineTop, RenderX, RenderY, 32, 16);
                    for (int l = 0; l < Data[2] % 0x10 + 2; l++ ) {
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.Vine, RenderX, RenderY, 32, 16);
                    }
                    g.DrawImage(Properties.Resources.VineBottom, RenderX, RenderY + 16, 32, 16);
                    break;
                case 283:
                    g.DrawImage(Properties.Resources.SpikeBass, RenderX, RenderY - 32, 54, 55);
                    break;
                case 284:
                    g.DrawImage(Properties.Resources.Pumpkin, RenderX, RenderY - 2, 18, 18);
                    break;
                case 285:
                    g.DrawImage(Properties.Resources.ScuttleBug, RenderX, RenderY - 8, 46, 24);
                    break;
                case 289:
                    g.DrawImage(Properties.Resources.ExpandableBlock, RenderX, RenderY, 16, 16);
                    break;
                case 290:
                    g.DrawImage(Properties.Resources.FlyingQBlock, RenderX - 9, RenderY - 13, 38, 28);
                    if (Data[2] % 0x10 < 8)
                        g.DrawImage(Properties.Resources.FlyingQBlockOverrides.Clone(new Rectangle((Data[2] % 0x10)
                        * 16, 0, 16, 16), System.Drawing.Imaging.PixelFormat.DontCare), RenderX, RenderY, 16, 16);
                    break;
                case 292:
                    g.DrawImage(Properties.Resources.Door2, RenderX, RenderY - 32, 32, 48);
                    break;
                case 298:
                    int width = Data[2] % 0x10;
                    int height = Data[2] / 0x10;
                    int spikes = Data[5] % 0x10;
                    if (spikes == 1 || spikes == 3) {
                        height -= 1;
                        RenderY += 16;
                    }
                    if (spikes == 2 || spikes == 3) height -= 1;
                    if (spikes == 4 || spikes == 6) width -= 1;
                    if (spikes == 5 || spikes == 6) {
                        width -= 1;
                        RenderX += 16;
                    }
                    if (width < 1) width = 1;
                    if (height < 0) height = 0;
                    int xp = RenderX;
                    int yp = RenderY;
                    if (height == 0) {
                        g.DrawImage(Properties.Resources.StoneBlockFlatHorizLeft, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX += 16;
                            g.DrawImage(Properties.Resources.StoneBlockFlatHorizMiddle, RenderX, RenderY, 16, 16);
                        }
                        g.DrawImage(Properties.Resources.StoneBlockFlatHorizRight, RenderX + 16, RenderY, 16, 16);
                    }
                    else {
                        g.DrawImage(Properties.Resources.StoneBlockTopLeft, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX += 16;
                            g.DrawImage(Properties.Resources.StoneBlockTop, RenderX, RenderY, 16, 16);
                        }
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.StoneBlockTopRight, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < height - 1; l++) {
                            RenderY += 16;
                            g.DrawImage(Properties.Resources.StoneBlockRight, RenderX, RenderY, 16, 16);
                        }
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.StoneBlockBottomRight, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX -= 16;
                            g.DrawImage(Properties.Resources.StoneBlockBottom, RenderX, RenderY, 16, 16);
                        }
                        RenderX -= 16;
                        g.DrawImage(Properties.Resources.StoneBlockBottomLeft, RenderX, RenderY, 16, 16);
                        for (int l = 0; l < height - 1; l++) {
                            RenderY -= 16;
                            g.DrawImage(Properties.Resources.StoneBlockLeft, RenderX, RenderY, 16, 16);
                        }
                        RenderX = xp;
                        RenderY = yp;
                        int xStart = RenderX;
                        for (int l = 0; l < height - 1; l++) {
                            RenderY += 16;
                            for (int m = 0; m < width - 1; m++) {
                                RenderX += 16;
                                g.DrawImage(Properties.Resources.StoneBlockMiddle, RenderX, RenderY, 16, 16);
                            }
                            RenderX = xStart;
                        }
                    }
                    if (spikes == 1 || spikes == 3) {
                        RenderX = xp;
                        RenderY = yp - 16;
                        for (int l = 0; l <= width; l++)  {
                            g.DrawImage(Properties.Resources.StoneBlockSpikes, RenderX, RenderY, 16, 16);
                            RenderX += 16;
                        }
                    }
                    if (spikes == 2 || spikes == 3) {
                        RenderX = xp;
                        RenderY = (Y + height + 1) * 16;
                        if (spikes == 3)
                            RenderY += 16;
                        Bitmap spikes2 = Properties.Resources.StoneBlockSpikes;
                        spikes2.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        for (int l = 0; l <= Data[2] % 0x10; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY, 16, 16);
                            RenderX += 16;
                        }
                    }
                    if (spikes == 4 || spikes == 6) {
                        RenderX = (X + width + 1) * 16;
                        RenderY = yp;
                        if (spikes == 6)
                            RenderX += 16;
                        Bitmap spikes2 = Properties.Resources.StoneBlockSpikes;
                        spikes2.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        for (int l = 0; l <= Data[2] / 0x10; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY, 16, 16);
                            RenderY += 16;
                        }
                    }
                    if (spikes == 5 || spikes == 6) {
                        RenderX = xp - 16;
                        RenderY = yp;
                        Bitmap spikes2 = Properties.Resources.StoneBlockSpikes;
                        spikes2.RotateFlip(RotateFlipType.Rotate90FlipX);
                        for (int l = 0; l <= height; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY, 16, 16);
                            RenderY += 16;
                        }
                    }
                    break;
                case 300:
                    RenderY += -8 + Data[2] % 0x10;
                    g.DrawImage(Properties.Resources.GhostPlatform, RenderX - 64, RenderY, 128, 16);
                    break;
                case 303:
                    g.DrawImage(Properties.Resources.SpinningSpikeBall, RenderX + 4, RenderY - 61, 72, 74);
                    break;
                case 260:
                case 304:
                    g.DrawImage(Properties.Resources.GiantSpike, RenderX, RenderY - 128, 64, 144);
                    break;
                case 261:
                case 307:
                    img = Properties.Resources.GiantSpike;
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    g.DrawImage(img, RenderX, RenderY, 64, 144);
                    break;
                case 262:
                case 308:
                    img = Properties.Resources.GiantSpike;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX, RenderY, 144, 64);
                    break;
                case 263:
                case 309:
                    img = Properties.Resources.GiantSpike;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 128, RenderY, 144, 64);
                    break;
                case 323:
                    g.DrawImage(Properties.Resources.CloudLeftEdge, RenderX, RenderY, 16, 26);
                    RenderX += 16;
                    for (int l = 0; l <= Data[2]; l++) {
                        g.DrawImage(Properties.Resources.CloudSection, RenderX, RenderY, 32, 32);
                        RenderX += 32;
                    }
                    g.DrawImage(Properties.Resources.CloudRightEdge, RenderX, RenderY, 16, 25);
                    break;
                default:
                    customRendered = false;
                    if (Level.ValidSprites[this.Type])
                        img = Properties.Resources.sprite;
                    else
                        img = Properties.Resources.sprite_invalid;
                    g.DrawImage(img, RenderX, RenderY, 16, 16);
                    g.DrawString(Type.ToString(), NSMBGraphics.SmallInfoFont, Brushes.White, (float)RenderX, (float)RenderY);
                    break;
            }

            if (!Level.ValidSprites[this.Type] && customRendered) {
                SolidBrush sb = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
                g.FillRectangle(sb, this.getRect());
                sb.Dispose();
            }

            return customRendered;
        }

        private static Bitmap RotateBitmap(Bitmap b, float angle)
        {
            Bitmap newBitmap = new Bitmap(b.Width, b.Height);
            using (Graphics g = Graphics.FromImage(newBitmap)) {
                g.TranslateTransform(b.Width / 2, b.Height / 2);
                g.RotateTransform(angle);
                g.TranslateTransform(-b.Width / 2, -b.Height / 2);
                g.DrawImage(b, 0, 0, b.Width, b.Height);
            }
            return newBitmap;
        }

        private static Bitmap ScaleBitmap(Bitmap b, int width, int height) {
            Bitmap newImg = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(newImg)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(b, 0, 0, width + width / b.Width / 2, height + height / b.Height / 2);
            }
            return newImg;
        }

        private static int BtoI(bool value)
        {
            return value ? 1 : -1;
        }
    }
}
