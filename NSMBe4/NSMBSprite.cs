﻿/*
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
using System.Windows.Forms;

namespace NSMBe4
{
    public class NSMBSprite : LevelItem
    {
        public int X;
        public int Y;
        public int Type;
        public byte[] Data;
        private NSMBLevel Level;

        private static SolidBrush invalidBrush = new SolidBrush(Color.FromArgb(100, 255, 0, 0));
        public int x { get { return getRect().X; } set { X += (value - getRect().X) / snap; } }
        public int y { get { return getRect().Y; } set { Y += (value - getRect().Y) / snap; } }
        public int width { get { return getRect().Width; } set { } }
        public int height { get { return getRect().Height; } set { } }

        public int rx { get { return X * snap; } }
        public int ry { get { return Y * snap; } }
        public int rwidth { get { return 16; } }
        public int rheight { get { return 16; } }

        public bool isResizable { get { return false; } }
        public int snap { get { return 16; } }

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

        private static int[] AlwaysDrawNums = { 68, 69, 73, 141, 226, 305, 306 };
        private static int[] TileCreateNums = { 0x67, 0x50, -0x870, 0x1B, 0x19, 0x67, -0x430, -0x240, 0x67, 0x67, 0x67, 0x67, 0x67, 0x67, 0x67, 0x18 };

        public bool AlwaysDraw() {
            return Array.IndexOf(AlwaysDrawNums, this.Type) > -1;
        }

        public Rectangle getRect() {
            int x = this.X * 16;
            int y = this.Y * 16;
            int width = 16;
            int height = 16;
            switch (this.Type) {
                case 21:
                    x += 73; y -= 8;
                    height = 19;
                    break;
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
                    height = (Data[5] & 0xF0) + 32;
                    y -= height - 16;
                    break;
                case 28:
                    height = 18;
                    y -= 2;
                    break;
                case 29:
                    y -= 22;
                    width = 29; height = 38;
                    break;
                case 31:
                    width = 21;
                    break;
                case 32:
                    x -= 29; y -= 119;
                    width = 27; height = 24;
                    break;
                case 33:
                    if (Data[5] % 0x10 == 1) {
                        x -= 8; y -= 16;
                        width = 32; height = 32;
                    }
                    if (Data[5] / 0x10 == 1)
                        x += 8;
                    break;
                case 34:
                    x -= 7; y -= 26;
                    width = 30; height = 42;
                    if (Data[5] / 0x10 == 1)
                        x += 8;
                    break;
                case 35:
                    x -= 125; y -= 28;
                    width = 162; height = 43;
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
                //case 40: see 183
                case 41:
                    x += 4; y -= 7;
                    width = 24; height = 23;
                    break;
                case 42:
                    x -= 8; y -= 48;
                    height = 48;
                    if (Data[5] % 0x10 == 1) {
                        x -= 29; width = 45;
                    }
                    break;
                case 43:
                    y -= 28; height = 28;
                    x -= 29; width = 44;
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
                    width = (Data[5] % 0x10) * 10;
                    y -= width;
                    if (Data[5] / 0x10 == 1) {
                        x -= width;
                        width *= 2;
                    }
                    width += 12;
                    height = width;
                    break;
                case 57:
                    if (Data[5] % 0x10 == 1) {
                        x -= 4; y -= 4;
                        width = 24; height = 24;
                    } else if (Data[5] % 0x10 == 2) {
                        x -= 4; y -= 20;
                        width = 72; height = 40;
                    }
                    if (Data[5] % 0x10 != 1 && Data[5] % 0x10 != 2) {
                        if (Data[3] / 0x10 == 1)
                            y += 8;
                        if (Data[2] % 0x10 == 1)
                            x += 8;
                    }
                    break;
                case 58:
                    x -= 41; y -= 38;
                    width = 76; height = 55;
                    break;
                case 59:
                    y -= 18;
                    width = 18; height = 34;
                    break;
                case 63:
                    x -= 42; y -= 40;
                    width = 80; height = 57;
                    break;
                case 64:
                    if (Data[5] % 0x10 == 1) {
                        x -= 38; y -= 74;
                        width = 96; height = 90;
                    } else {
                        x -= 29; y -= 57;
                        width = 78; height = 73;
                    }
                    break;
                case 65:
                    x += 22; y -= 29;
                    width = 59; height = 48;
                    break;
                case 66:
                    if (Data[5] % 0x10 != 1)
                        y -= 2;
                    if (Data[5] / 0x10 == 1)
                        x += 8;
                    height = 18;
                    break;
                case 67:
                    x -= 27; y -= 8;
                    width = 71; height = 26;
                    break;
                case 68:
                    width = Math.Max(16 * (Data[5] % 0x8 + 1), 32);
                    break;
                case 69:
                case 296:
                    width = Math.Max(16 * (Data[5] % 0x10 + 1), 32);
                    break;
                case 70:
                    if (Data[5] % 0x10 == 1) {
                        x -= 56;
                        width = 128;
                    } else {
                        x -= 24;
                        width = 64;
                    }
                    break;
                case 71:
                    width = Math.Max(16 * (Data[5] % 0x8 + 1), 32);
                    height = 16 * (int)Math.Pow(2, ((Data[4] & 0x30) / 0x10));
                    break;
                case 72:
                case 297:
                    width = Math.Max(16 * (Data[5] % 0x10 + 1), 32);
                    height = 16 * (int)Math.Pow(2, ((Data[4] & 0x30) / 0x10));
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
                    width = 16 * (Data[5] % 0x10) + 48;
                    height = 16 * Math.Max(Data[5] / 0x10, Data[4] % 0x10) + 18;
                    break;
                case 77:
                case 78:
                    width = Data[5] % 0x10;
                    int s = 2;
                    if (this.Type == 78) {
                        width = Math.Max(0, width - 1);
                        s = 1;
                        x -= 8;
                    }
                    x -= (width * s + 1) * 8;
                    width = (width * s + 2) * 16;
                    break;
                case 79:
                    x -= 28; y -= 34;
                    width = 92; height = 85;
                    break;
                case 80:
                    if (Data[3] % 0x10 == 1)
                        y -= 16;
                    else
                        y += 16;
                    width = 64;
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
                    if (Data[5] % 0x10 == 1) {
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
                    if (Data[5] % 0x10 == 1) {
                        x += 3; y += 7;
                        width = 25; height = 23;
                    } else if (Data[5] / 0x10 == 1) {
                        x += 45; y += 3;
                        width = 104; height = 28;
                    } else {
                        x += 12; y += 3;
                        width = 104; height = 28;
                    }
                    break;
                case 93:
                    width = 48; height = 48;
                    if (Data[3] % 0x10 == 1) {
                        width = 24; height = 24; }
                    if (Data[5] / 0x10 % 2 == 1) {
                        x += (width / 9) * ((Data[5] / 0x10 > 4) ? 1 : -1);
                        y += (width / 9) * ((Data[5] / 0x10 == 3 || Data[5] / 0x10 == 5) ? 1 : -1);
                    }
                    x -= (width - 16) / 2;
                    y -= (height - 16);
                    if (Data[4] / 0x10 == 1)
                        x += 8;
                    if (Data[4] % 0x10 == 1)
                        y += 8;
                    break;
                case 94:
                    if (Data[5] % 0x10 == 1) {
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
                    switch (Data[2] % 0x10) {
                        case 2:
                            x -= 40; y -= 59;
                            width = 82; height = 75;
                            break;
                        case 3:
                            x -= 40; y -= 59;
                            width = 83; height = 75;
                            break;
                        default:
                            x -= 51; y -= 59;
                            width = 115; height = 76;
                            break;
                    }
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
                case 108:
                    if (Data[5] % 0x10 != 1)
                        y -= 2;
                    if (Data[5] / 0x10 == 1)
                        x += 8;
                    height = 18;
                    break;
                case 109:
                    x -= 6; y -= 6;
                    int shift = Data[5] / 0x10;
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
                    switch (Data[5] % 0x40) {
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
                    int sz = Data[2] / 0x10;
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
                case 124:
                    x -= 38; y -= 45;
                    width = 79; height = 61;
                    break;
                case 126:
                    width = (Data[2] & 0xF0) * 2;
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
                //case 129: see 140
                case 130:
                    width = 21;
                    break;
                case 131:
                case 132:
                    width = 22; height = 33;
                    break;
                case 136:
                    width = 18; height = 16 * (2 + Data[5] % 0x8) + 9;
                    y -= height - 16;
                    break;
                case 129:
                case 140:
                    x -= 20; y -= 15;
                    width = 25; height = 31;
                    break;
                case 141:
                    x -= 128;
                    width = 276; height = 128;
                    break;
                case 142:
                    x -= 8; width = (Data[2] / 0x10 + 1) * 16;
                    height = (Data[2] % 0x10);
                    if (height >= 8)
                        height -= 16;
                    if (height >= 0) {
                        y -= height * 16;
                        height += 1;
                    } else {
                        height -= 1;
                    }
                    height *= 16;
                    height = Math.Abs(height);
                    break;
                case 144:
                    x -= 2; y -= 7;
                    width = 19; height = 26;
                    break;
                case 146:
                    width = 32; height = 8;
                    break;
                case 147:
                    x -= Data[5] * 8 + 6;
                    width = 16 * (Data[5] - 1) + 28;
                    height = 10;
                    break;
                case 148:
                    y -= 2;
                    height = 18;
                    break;
                case 149:
                    if (Data[5] / 0x10 != 1){
                        y -= 13;
                        height = 29;
                    }
                    break;
                case 150:
                    y -= 16;
                    width = 19; height = 32;
                    break;
                //case 152:
                //    Switch blocks don't need to be modified
                case 155:
                    width = 16 * (Data[4] % 0x10 + 1);
                    height = 16 * (Data[5] / 0x10 + 1);
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
                    x -= 16 * (Data[4] / 0x10 + 1);
                    width = 32 * (Data[4] / 0x10 + 2);
                    height = 16 * (Data[3] % 0x10 + 4);
                    break;
                case 173:
                    x -= 3; width = 8;
                    height = 16 * (Data[5] % 0x10 + 4);
                    break;
                case 174:
                    x -= 16 * (Data[4] / 0x10 + 1);
                    width = 32 * (Data[4] / 0x10 + 2);
                    height = 64;
                    break;
                case 175:
                    x -= 40; y -= 48;
                    width = 96; height = 64;
                    break;
                case 180:
                    width = 18; height = 26;
                    break;
                case 40:
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
                    height = Data[3] / 0x10;
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
                    width = 16 * Math.Max(1, Data[5] / 0x10);
                    height = 16 * Math.Max(1, Data[5] % 0x10);
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
                case 210:
                    width = 32; height = 32;
                    break;
                case 211:
                    y -= 16;
                    width = 26; height = 32;
                    break;
                case 212:
                    y -= 16;
                    width = 59; height = 32;
                    break;
                case 213:
                    y -= 32; x -= 8;
                    width = 42; height = 64;
                    break;
                case 219:
                    switch (Data[5] / 0x10) {
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
                    width = 32; height = 41;
                    break;
                case 222:
                    if (Data[5] / 0x10 == 1)
                        x += 8;
                    if (Data[5] % 0x10 == 1)
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
                    y += (Data[5] & 0xF0) + 64;
                    x -= 14;
                    width = 44; height = 37;
                    break;
                case 227:
                    y -= 5;
                    width = 23; height = 21;
                    break;
                case 228:
                    width = 20; height = 20;
                    break;
                case 229:
                    x -= 16; y -= 56;
                    width = 108; height = 75;
                    break;
                case 232:
                    y -= 128;
                    height += 128;
                    break;
                case 233:
                    x -= 11; y -= 11;
                    width = 22; height = (Data[5] % 0x10) * 16 + 75;
                    break;
                case 235:
                    if (Data[5] / 0x10 == 1)
                        x += 8;
                    if (Data[5] % 0x10 == 1)
                        y += 8;
                    width = 32; height = 32;
                    break;
                case 236:
                    x -= 16; y -= 16;
                    if (Data[2] / 0x10 == 1)
                        y += 8;
                    width = 48; height = 48;
                    break;
                case 237:
                    y -= 15;
                    width = 45; height = 31;
                    break;
                case 238:
                    x -= 16 * (Data[5] / 0x10 + 1);
                    width = 32 * (Data[5] / 0x10 + 2);
                    height = 112;
                    break;
                case 239:
                    x -= 8 * (Data[5] % 0x10 + 1);
                    width = 16 * (Data[5] % 0x10 + 2);
                    height = 16 * (Math.Max(Data[5] / 0x10, Data[4] % 0x10) + 1) + 32;
                    break;
                case 241:
                    width = 23;
                    height = Math.Min(8, Data[3] + 1) * 16;
                    y -= height - 16; x -= 3;
                    break;
                case 242:
                    if (Data[5] / 0x10 == 1) {
                        if (Data[4] % 0x10 == 1) {
                            x -= 32; width = 80;
                        } else {
                            x -= 72; width = 160;
                        }
                    } else {
                        x -= 8; width = 32;
                    }
                    height = 32 + 16 * (Data[5] % 0x10);
                    break;
                //case 243:
                //    Roof Spinys don't need to be modified
                case 244:
                    switch (Data[5] / 0x10) {
                        case 1:
                            x -= 8; break;
                        case 2:
                            x += 8; break;
                    }
                    x -= 40;
                    width = 96; height = 16 * (Data[5] % 0x10 + 2);
                    break;
                case 245:
                    y -= 56;
                    width = 32; height = 72;
                    break;
                case 246:
                    x -= 16; y -= 8;
                    width = 48; height = 48;
                    break;
                case 247:
                    x -= 27; y -= 8;
                    width = 71; height = 26;
                    break;
                case 248:
                    x -= 53; y -= 106;
                    width = 109; height = 108;
                    break;
                case 249:
                    x -= Data[5] % 0x10 * 8 + 24; y += 5;
                    width = Data[5] % 0x10 * 16 + 64;
                    height = Data[4] / 0x10 * 16 + 83;
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
                case 255:
                    width = 256; height = 64;
                    break;
                case 256:
                    width = 96; height = 16;
                    if (Data[3] / 0x10 == 0)
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
                    if (Data[5] % 0x10 == 1) {
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
                    height = 35; width = (Data[5] % 0x10 + 3) * 16;
                    break;
                case 275:
                    height = (Data[5] % 0x10) * 16 + 30;
                    y -= height - 16;
                    break;
                case 277:
                    width = 32; height = 32;
                    break;
                case 278:
                    width = (Data[5] % 0x10 + 1) * 16;
                    height = (Data[5] / 0x10 + 1) * 16;
                    break;
                case 279:
                    int nvalue = Data[5] % 0x10;
                    if (nvalue == 0 || nvalue == 1) {
                        y -= 39;
                        width = 16; height = 39;
                    } else if (nvalue == 2 || nvalue == 3) {
                        y += 16;
                        width = 16; height = 39;
                    } else if (nvalue == 4 || nvalue == 5) {
                        x += 16;
                        width = 39; height = 16;
                    } else if (nvalue == 6 || nvalue == 7) {
                        x -= 39;
                        width = 39; height = 16;
                    }
                    break;
                case 281:
                    if (Data[5] % 0x10 == 1)
                        y += 16;
                    else
                        y -= 15;
                    x += 10;
                    width = 13; height = 15;
                    break;
                case 282:
                    x -= 16;
                    width = 32; height = (Data[5] % 0x10 + 4) * 16;
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
                case 295:
                    x -= 256; y += 16;
                    width = 512; height = 95;
                    break;
                case 298:
                    width = Math.Max(16 * (Data[5] % 0x10 + 1), 32);
                    height = 16 * (Data[5] / 0x10 + 1);
                    int spikes = Data[2] % 0x10;
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
                    y += -8 + Data[5] % 0x10; x-= 64;
                    width = 128; height = 16;
                    break;
                case 303:
                    width = 96; height = 39;
                    if ((Data[5] % 0x4) == 0){
                        x += 4; y -= 11;
                    }
                    if ((Data[5] % 0x4) == 1){
                        x -= 11; y -= 83;
                        width = 39; height = 96;
                    }
                    if ((Data[5] % 0x4) == 2){
                        x -= 83; y -= 11;
                    }
                    if ((Data[5] % 0x4) == 3){
                        x -= 11; y += 4;
                        width = 39; height = 96;
                    }
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
                //case 305:
                //case 306:
                //    These are still selected with the original sprite icon
                case 312:
                    width = (Data[5] % 0x10 + 2) * 16;
                    x -= (width - 16) / 2;
                    height = (Data[5] & 0xF0) + 49;
                    break;
                case 323:
                    height = 32;
                    width = 32 * (Data[5] + 2);
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

        public void render(Graphics g, LevelEditorControl ed)
        {
            int RenderX = X * 16, RenderX2 = RenderX;
            int RenderY = Y * 16, RenderY2 = RenderY;
            int width, height;
            Bitmap img = null;

            bool customRendered = true;

            switch (this.Type)
            {
                case 21:
                    g.DrawImage(Properties.Resources.MegaGoomba, RenderX + 73, RenderY - 8);
                    break;
                case 23:
                    g.DrawImage(Properties.Resources.PiranhaplantTube, RenderX, RenderY - 28);
                    break;
                case 24:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY + 32);
                    break;
                case 25:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX + 32, RenderY);
                    break;
                case 26:
                    img = Properties.Resources.PiranhaplantTube;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 28, RenderY);
                    break;
                case 27:
                    for (int l = 0; l <= Data[5] / 0x10; l++) {
                        if (l == Data[5] / 0x10) {
                            g.DrawImage(Properties.Resources.BulletBillCannonTop, RenderX, RenderY - 16);
                        } else {
                            g.DrawImage(Properties.Resources.BulletBillCannon, RenderX, RenderY);
                            RenderY -= 16;
                        }
                    }
                    break;
                case 28:
                    g.DrawImage(Properties.Resources.BomOmb, RenderX, RenderY - 2);
                    break;
                case 29:
                    g.DrawImage(Properties.Resources.PrincessPeach, RenderX, RenderY - 22);
                    break;
                case 31:
                    g.DrawImage(Properties.Resources.CheepCheep, RenderX, RenderY);
                    break;
                case 32:
                    if (Data[2] / 0x10 == 1)
                        img = Properties.Resources.EndingFlagRed;
                    else
                        img = Properties.Resources.EndingFlag;
                    g.DrawImage(img, RenderX - 29, RenderY - 119);
                    break;
                case 33:
                    if (Data[5] % 0x10 == 1) {
                        img = Properties.Resources.SpringGiant;
                        RenderY -= 16;
                        RenderX -= 8;
                    } else
                        img = Properties.Resources.Spring;
                    if (Data[5] / 0x10 == 1)
                        RenderX += 8;
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 34:
                    if (Data[5] / 0x10 == 1)
                        RenderX += 8;
                    g.DrawImage(Properties.Resources.RedCoinRing, RenderX - 7, RenderY - 26);
                    break;
                case 35:
                    g.DrawImage(Properties.Resources.FinalBoss, RenderX - 125, RenderY - 28);
                    break;
                case 36:
                    g.DrawImage(Properties.Resources.Thwomp, RenderX, RenderY);
                    break;
                case 37:
                    g.DrawImage(Properties.Resources.Spiny, RenderX, RenderY);
                    break;
                case 38:
                    g.DrawImage(Properties.Resources.Boo, RenderX, RenderY);
                    break;
                //case 40: see 183
                case 41:
                    g.DrawImage(Properties.Resources.BowserSwitch, RenderX + 4, RenderY - 7);
                    break;
                case 42:
                    g.DrawImage(Properties.Resources.ChainChompLog, RenderX - 8, RenderY - 48);
                    if (Data[5] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.ChainChomp, RenderX - 37, RenderY - 28);
                    break;
                case 43:
                    g.DrawImage(Properties.Resources.ChainChomp, RenderX - 29, RenderY - 28);
                    break;
                case 48:
                    g.DrawImage(Properties.Resources.TubeBubbles, RenderX, RenderY - 90);
                    break;
                case 49:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY + 32);
                    break;
                case 50:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX + 32, RenderY);
                    break;
                case 51:
                    img = Properties.Resources.TubeBubbles;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 90, RenderY);
                    break;
                case 52:
                    if (Data[5] % 0x10 == 1)
                        img = Properties.Resources.BuzzyBeetleU;
                    else
                        img = Properties.Resources.BuzzyBeetle;
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 53:
                    g.DrawImage(Properties.Resources.DryBones, RenderX, RenderY - 10);
                    break;
                case 54:
                    g.DrawImage(Properties.Resources.FireBall, RenderX, RenderY);
                    break;
                case 55:
                    g.DrawImage(Properties.Resources.BulletBill, RenderX, RenderY);
                    break;
                case 56:
                    RenderY2 = (Data[5] % 0x10) * 10;
                    if (Data[5] / 0x10 == 1) {
                        RenderX -= RenderY2;
                        RenderY += RenderY2;
                    }
                    while (!(RenderX == RenderX2 + RenderY2 + 10)) {
                        if (RenderX == RenderX2)
                            g.DrawImage(Properties.Resources.FireBarMiddle, RenderX + 4, RenderY + 4);
                        else
                            g.DrawImage(Properties.Resources.FireBarBall, RenderX, RenderY);
                        RenderX += 10; RenderY -= 10;
                    }
                    break;
                case 57:
                    if (Data[5] % 0x10 != 1 && Data[5] % 0x10 != 2) {
                        if (Data[3] / 0x10 == 1)
                            RenderY += 8;
                        if (Data[2] % 0x10 == 1)
                            RenderX += 8;
                    }
                    if (Data[5] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX - 4, RenderY - 4);
                    else if (Data[5] % 0x10 == 2) {
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX - 4, RenderY - 4);
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX + 20, RenderY - 20);
                        g.DrawImage(Properties.Resources.CoinInBubble, RenderX + 44, RenderY - 4);
                    } else {
                        g.DrawImage(Properties.Resources.Coin, RenderX, RenderY);
                    }
                    break;
                case 58:
                    g.DrawImage(Properties.Resources.Bowser, RenderX - 41, RenderY - 38);
                    break;
                case 59:
                    g.DrawImage(Properties.Resources.HammerBro, RenderX, RenderY - 18);
                    break;
                case 63:
                    g.DrawImage(Properties.Resources.DryBowser, RenderX - 42, RenderY - 40);
                    break;
                case 64:
                    if (Data[5] % 0x10 == 1) {
                        img = Properties.Resources.WhompL;
                        RenderX -= 38; RenderY -= 74;
                    } else {
                        img = Properties.Resources.Whomp;
                        RenderX -= 29; RenderY -= 57;
                    }
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 65:
                    g.DrawImage(Properties.Resources.CheepSkipper, RenderX + 22, RenderY - 29);
                    break;
                case 66:
                    img = Properties.Resources.PSwitch;
                    if (Data[5] % 0x10 == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    if (Data[5] / 0x10 == 1)
                        RenderX += 8;
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 67:
                    g.DrawImage(Properties.Resources.Shark, RenderX - 27, RenderY - 8);
                    break;
                case 68:
                case 69:
                case 296:
                    int dist = (Data[4] % 0x10) * 16;
                    int LiftLength = (Data[5] % 0x10);
                    if (Data[3] % 0x10 == 1)
                        dist = -dist;
                    if (this.Type == 68) {
                        RenderY2 -= dist;
                        LiftLength = (Data[5] % 0x8);
                    } else {
                        RenderX2 += dist;
                    }
                    width = Math.Max(16 * (LiftLength + 1), 32) / 2;
                    g.DrawLine(Pens.White, RenderX + width, RenderY + 8, RenderX2 + width, RenderY2 + 8);
                    g.FillEllipse(Brushes.White, RenderX2 + width - 4, RenderY2 + 4, 7, 7);
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY);
                    for (int l = 0; l < LiftLength - 1; l++){
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX, RenderY, 16, 16);
                    }
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 16, RenderY);
                    RenderX = X * 16; RenderY = Y * 16;
                    if (RenderX != RenderX2 || RenderY != RenderY2) {
                        g.DrawImage(Properties.Resources.MovingPlatformLeftE, RenderX2, RenderY2);
                        for (int l = 0; l < LiftLength - 1; l++){
                            RenderX2 += 16;
                            g.DrawImage(Properties.Resources.MovingPlatformSectionE, RenderX2, RenderY2);
                        }
                        g.DrawImage(Properties.Resources.MovingPlatformRightE, RenderX2 + 16, RenderY2);
                    }
                    break;
                case 70:
                    if (Data[5] % 0x10 == 1) {
                        g.DrawImage(Properties.Resources.SpinningLogL, RenderX - 56, RenderY);
                    } else {
                        g.DrawImage(Properties.Resources.SpinningLog, RenderX - 24, RenderY);
                    }
                    break;
                case 73:
                    g.DrawImage(Properties.Resources.HangingPlatform, RenderX, RenderY);
                    RenderX += 92;
                    for (int l = 0; l < 6; l++) {
                        RenderY -= 32;
                        g.DrawImage(Properties.Resources.HangingPlatformChain, RenderX, RenderY);
                    }
                    break;
                case 74:
                    g.DrawImage(Properties.Resources.TiltingRock, RenderX, RenderY - 8);
                    break;
                case 75:
                    g.DrawImage(Properties.Resources.SeeSaw, RenderX, RenderY);
                    break;
                case 76:
                    RenderX2 = RenderX + 16 * (Data[5] % 0x10) - 10;
                    g.DrawImage(Properties.Resources.ScalePlatformBolt, RenderX - 2, RenderY - 10);
                    g.DrawImage(Properties.Resources.ScalePlatformBolt, RenderX2, RenderY - 10);
                    Pen rope = new Pen(Color.FromArgb(49, 24, 74));
                    g.DrawLine(Pens.White, RenderX + 8, RenderY - 9, RenderX2 + 2, RenderY - 9);
                    g.DrawLine(rope, RenderX + 9, RenderY - 8, RenderX2 + 1, RenderY - 8);
                    RenderY2 = RenderY + 16 * (Data[4] % 0x10) - 8;
                    int RenderY3 = RenderY + 16 * (Data[5] / 0x10) - 8;
                    g.DrawLine(Pens.White, RenderX - 1, RenderY, RenderX - 1, RenderY2 - 1);
                    g.DrawLine(rope, RenderX, RenderY + 1, RenderX, RenderY2 - 1);
                    g.DrawLine(Pens.White, RenderX2 + 9, RenderY + 3, RenderX2 + 9, RenderY3 - 1);
                    g.DrawLine(rope, RenderX2 + 10, RenderY + 1, RenderX2 + 10, RenderY3 - 1);
                    g.DrawImage(Properties.Resources.ScalePlatformEnd, RenderX2 - 14, RenderY3);
                    g.DrawImage(Properties.Resources.ScalePlatformEnd, RenderX - 24, RenderY2);
                    rope.Dispose();
                    break;
                case 77:
                case 78:
                    int w = Data[5] % 0x10, s = 2;
                    if (this.Type == 78) { w = Math.Max(0, w - 1); s = 1; RenderX -= 8; }
                    RenderX -= (w * s + 1) * 8;
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY);
                    for (int l = 0; l < w * s; l++) {
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX, RenderY);
                    }
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 16, RenderY);
                    break;
                case 79:
                    g.DrawImage(Properties.Resources.Spinning3PointedPlatform, RenderX - 28, RenderY - 34);
                    break;
                case 80:
                    img = Properties.Resources.arrow_down;
                    if (Data[3] % 0x10 == 1)
                        RenderY -= 16;
                    else {
                        RenderY += 16;
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    }
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY);
                    g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX + 16, RenderY);
                    g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX + 32, RenderY);
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 48, RenderY);
                    g.DrawImage(img, RenderX + 24, RenderY2, 16, 16);
                    break;
                case 82:
                    g.DrawImage(Properties.Resources.SpinningRectanglePlatform, RenderX - 24, RenderY - 8);
                    break;
                case 86:
                    g.DrawImage(Properties.Resources.SpinningTrianglePlatform, RenderX - 14, RenderY - 6);
                    break;
                case 88:
                    g.DrawImage(Level.GFX.Tilesets[0].Map16Buffer, RenderX, RenderY, new Rectangle(0, 0x50, 16, 16), GraphicsUnit.Pixel);
                    g.DrawImage(Properties.Resources.PSwitch, RenderX + 2, RenderY + 1, 12, 14);
                    break;
                case 89:
                    g.DrawImage(Properties.Resources.Snailicorn, RenderX, RenderY - 16);
                    break;
                case 90:
                    if (Data[5] == 1)
                        g.DrawImage(Properties.Resources.WigglerL, RenderX, RenderY - 44);
                    else
                        g.DrawImage(Properties.Resources.Wiggler, RenderX, RenderY - 14);
                    break;
                case 91:
                    g.DrawImage(Properties.Resources.MovingPlatformLeft, RenderX, RenderY + 8);
                    g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX + 16, RenderY + 8);
                    g.DrawImage(Properties.Resources.MovingPlatformSection, RenderX + 32, RenderY + 8);
                    g.DrawImage(Properties.Resources.MovingPlatformRight, RenderX + 48, RenderY + 8);
                    break;
                case 92:
                    if (Data[5] % 0x10 == 1) {
                        g.DrawImage(Properties.Resources.EelNonMoving, RenderX + 3, RenderY + 7);
                    } else if (Data[5] / 0x10 == 1) {
                        img = Properties.Resources.Eel;
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        g.DrawImage(img, RenderX + 45, RenderY + 3);
                    } else if (Data[3] % 0x10 == 1) {
                        g.DrawImage(Properties.Resources.Eel, RenderX + 12, RenderY + 3);
                    } else {
                        Rectangle r = new Rectangle(RenderX + 12, RenderY + 3, 104, 28);
                        g.FillRectangle(Brushes.Black, r);
                        g.DrawString(LanguageManager.Get("Sprites", "Eel"), NSMBGraphics.SmallInfoFont, Brushes.White, r);
                    }
                    break;
                case 93:
                    if (Data[5] / 0x10 % 2 == 1) {
                        img = Properties.Resources.ArrowSignRotate45;
                        if (Data[5] % 0x10 == 1)
                            img = Properties.Resources.ArrowSignRotate45F;
                    } else {
                        img = Properties.Resources.ArrowSign;
                        if (Data[5] % 0x10 == 1)
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    }
                    img = RotateBitmap(img, -Data[5] / 0x20 * 90);
                    if (Data[3] % 0x10 == 1)
                        img = new Bitmap(img, 24, 24);
                    if (Data[5] / 0x10 % 2 == 1) {
                        RenderX += (img.Width / 9) * ((Data[5] / 0x10 > 4) ? 1 : -1);
                        RenderY += (img.Width / 9) * ((Data[5] / 0x10 == 3 || Data[5] / 0x10 == 5) ? 1 : -1);
                    }
                    if (Data[4] / 0x10 == 1)
                        RenderX += 8;
                    if (Data[4] % 0x10 == 1)
                        RenderY += 8;
                    g.DrawImage(img, RenderX - (img.Width - 16) / 2, RenderY - (img.Height - 16));
                    break;
                case 94:
                    if (Data[5] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.SwooperLarge, RenderX - 6, RenderY - 16);
                    else
                        g.DrawImage(Properties.Resources.Swooper, RenderX, RenderY);
                    break;
                case 95:
                    g.DrawImage(Properties.Resources.SpinBoard, RenderX - 6, RenderY);
                    break;
                case 96:
                    g.DrawImage(Properties.Resources.SeaWeed, RenderX - 32, RenderY - 127);
                    break;
                case 99:
                    g.DrawImage(Properties.Resources.Spinning4PointedPlatform, RenderX - 38, RenderY - 29);
                    break;
                case 102:
                    g.DrawImage(Properties.Resources.SpikeBallSmall, RenderX, RenderY);
                    break;
                case 103:
                    switch (Data[2] % 0x10) {
                        case 1:
                            img = Properties.Resources.Dorrie;
                            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            g.DrawImage(img, RenderX - 51, RenderY - 59);
                            break;
                        case 2:
                            g.DrawImage(Properties.Resources.DorrieAway, RenderX - 40, RenderY - 59);
                            break;
                        case 3:
                            g.DrawImage(Properties.Resources.DorrieTowards, RenderX - 40, RenderY - 59);
                            break;
                        default:
                            g.DrawImage(Properties.Resources.Dorrie, RenderX - 51, RenderY - 59);
                            break;
                    }
                    break;
                case 104:
                    g.DrawImage(Properties.Resources.Tornado, RenderX - 88, RenderY - 130);
                    break;
                case 105:
                    g.DrawImage(Properties.Resources.WhirlPool, RenderX - 78, RenderY - 164);
                    break;
                case 106:
                    g.DrawImage(Properties.Resources.RedCoin, RenderX, RenderY);
                    break;
                case 107:
                    img = Properties.Resources.QSwitch;
                    if (Data[5] % 0x10 == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    if (Data[5] / 0x10 == 1)
                        RenderX += 8;
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 108:
                    img = Properties.Resources.RedSwitch;
                    if (Data[5] % 0x10 == 1)
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    else
                        RenderY -= 2;
                    if (Data[5] / 0x10 == 1)
                        RenderX += 8;
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 109:
                    int shift = Data[5] / 0x10;
                    if (shift == 1 || shift == 3)
                        RenderX += 8;
                    if (shift == 2 || shift == 3)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.ElectricBall, RenderX - 6, RenderY - 6);
                    break;
                case 110:
                    g.DrawImage(Level.GFX.Tilesets[0].Map16Buffer, RenderX, RenderY, new Rectangle(0, 0x50, 16, 16), GraphicsUnit.Pixel);
                    g.DrawImage(Properties.Resources.RedSwitch, RenderX + 2, RenderY + 1, 12, 14);
                    break;
                case 111:
                    g.DrawImage(Properties.Resources.Log, RenderX - 128, RenderY - 32);
                    break;
                case 113:
                    g.DrawImage(Properties.Resources.CheepChomp, RenderX, RenderY);
                    break;
                case 115:
                    g.DrawImage(Properties.Resources.SpikeBallLarge, RenderX, RenderY - 42);
                    break;
                case 114:
                case 118:
                    if (this.Type == 114)
                        img = Properties.Resources.SmallFlamethrower;
                    else
                        img = Properties.Resources.Flamethrower;
                    switch (Data[5] % 0x40) {
                        case 0:
                            g.DrawImage(img, RenderX, RenderY); break;
                        case 1:
                            img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                            g.DrawImage(img, RenderX - (img.Width - 16), RenderY); break;
                        case 2:
                            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                            g.DrawImage(img, RenderX, RenderY - (img.Height - 16)); break;
                        case 3:
                            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            g.DrawImage(img, RenderX, RenderY); break;
                    }
                    break;
                case 116:
                    g.DrawImage(Properties.Resources.WaterBug, RenderX - 16, RenderY - 5);
                    break;
                case 117:
                    g.DrawImage(Properties.Resources.FlyingBlock, RenderX - 12, RenderY - 8);
                    break;
                case 119:
                    int sz = Data[2] / 0x10;
                    if (sz == 0) sz = 1;
                    g.DrawImage(ScaleBitmap(Properties.Resources.Pendulum, sz * 64, sz * 68), RenderX - sz * 32, RenderY - sz * 6);
                    break;
                case 120:
                    g.DrawImage(Properties.Resources.PiranhaplantGround, RenderX, RenderY + 3);
                    break;
                case 122:
                    g.DrawImage(Properties.Resources.GiantPiranhaplant, RenderX - 37, RenderY - 48);
                    break;
                case 123:
                    g.DrawImage(Properties.Resources.FirePiranhaplant, RenderX - 8, RenderY + 2);
                    break;
                case 124:
                    g.DrawImage(Properties.Resources.GiantFirePiranhaplant, RenderX - 38, RenderY - 45);
                    break;
                case 126:
                    Bitmap img2 = null;
                    RenderX2 = RenderX - 16;
                    for (int l = 0; l < Data[2] / 0x10; l++ ) {
                        if (l == 0) {
                            img = Properties.Resources.DrawBridgeEnd;
                            img2 = Properties.Resources.DrawBridgeEnd; img2.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        } else if (l == Data[2] / 0x10 - 1) {
                            img = Properties.Resources.DrawBridgeHinge;
                            img2 = Properties.Resources.DrawBridgeHinge; img2.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        } else if (l == 1) {
                            img = Properties.Resources.DrawBridgeSection;
                            img2 = Properties.Resources.DrawBridgeSection; img2.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        g.DrawImage(img2, RenderX, RenderY);
                        g.DrawImage(img, RenderX2, RenderY);
                        RenderX += 16; RenderX2 -= 16;
                    }
                    if (Data[2] / 0x10 == 0)
                        g.DrawImage(Properties.Resources.DrawBridgeHinge, RenderX, RenderY);
                    break;
                case 127:
                    g.DrawImage(Properties.Resources.GiantSpinningPlatform, RenderX - 144, RenderY - 104);
                    break;
                case 128:
                    g.DrawImage(Properties.Resources.WarpCannon, RenderX - 9, RenderY - 26);
                    int world = Data[2] / 0x10;
                    if (world >= 5 && world <= 8)
                        world -= 5;
                    else
                        world = 0;
                    g.DrawImage(Properties.Resources.WarpWorlds, RenderX + 26, RenderY + 23, new Rectangle(world * 18, 0, 18, 19), GraphicsUnit.Pixel);
                    break;
                //case 129: see 140
                case 130:
                    g.DrawImage(Properties.Resources.CheepCheep, RenderX, RenderY);
                    break;
                case 131:
                case 132:
                    g.DrawImage(Properties.Resources.MidpointFlag, RenderX, RenderY);
                    break;
                case 136:
                    int PokeyMax = Data[5] & 0x7;
                    for (int l = 0; l <= PokeyMax; l++) {
                        g.DrawImage(Properties.Resources.PokeySection, RenderX, RenderY);
                        RenderY -= 16;
                    }
                    g.DrawImage(Properties.Resources.PokeyHead, RenderX, RenderY - 9);
                    break;
                case 129:
                case 140:
                    g.DrawImage(Properties.Resources.BossKey, RenderX - 20, RenderY - 15);
                    break;
                case 141:
                    g.DrawImage(Properties.Resources.SwellingGround, RenderX - 128, RenderY);
                    if ((Data[2] & 0xF) == 0) {
                        g.DrawImage(Properties.Resources.SwellingGroundOut, RenderX - 128, RenderY - 112);
                    } else if ((Data[2] & 0xF) == 1) {
                        g.DrawImage(Properties.Resources.SwellingGroundIn, RenderX - 128, RenderY);
                    }
                    break;
                case 142:
                    RenderX -= 8;
                    RenderX2 = RenderX + (Data[2] / 0x10) * 16;
                    RenderY2 = Data[2] % 0x10;
                    if (RenderY2 >= 8)
                        RenderY2 -= 16;
                    RenderY2 = RenderY - RenderY2 * 16;
                    g.DrawLine(Pens.White, RenderX + 8, RenderY + 8, RenderX2 + 8, RenderY2 + 8);
                    g.DrawImage(Properties.Resources.TightRopeEnd, RenderX, RenderY);
                    g.DrawImage(Properties.Resources.TightRopeEnd, RenderX2, RenderY2);
                    break;
                case 144:
                    g.DrawImage(Properties.Resources.SpikedBlock, RenderX - 2, RenderY - 7);
                    RenderY2 = Data[5] % 0x10;
                    if (RenderY2 == 2) RenderY2 = 3;
                    if (RenderY2 <= 3)
                        g.DrawImage(Properties.Resources.FlyingQBlockOverrides, RenderX, RenderY + 1, new Rectangle(RenderY2 * 16, 0, 16, 16), GraphicsUnit.Pixel);
                    break;
                case 146:
                    g.DrawImage(Properties.Resources.StarGate, RenderX, RenderY);
                    break;
                case 147:
                    RenderX -= Data[5] * 8 + 6;
                    g.DrawImage(Properties.Resources.BumpPlatformLeft, RenderX, RenderY);
                    RenderX += 14;
                    for (int l = 0; l < Data[5] - 1; l++) {
                        g.DrawImage(Properties.Resources.BumpPlatformSection, RenderX, RenderY);
                        RenderX += 16;
                    }
                    if (Data[5] > 0)
                        g.DrawImage(Properties.Resources.BumpPlatformRight, RenderX, RenderY);
                    break;
                case 148:
                    g.DrawImage(Properties.Resources.Goomba, RenderX, RenderY - 2);
                    break;
                case 149:
                    bool inShell = Data[5] / 0x10 == 1;
                    switch (Data[5] % 0x10) {
                        case 1:
                            img = inShell ? Properties.Resources.RedKoopaShell : Properties.Resources.KoopaRed; break;
                        case 2:
                        case 3:
                            img = inShell ? Properties.Resources.BlueKoopaShell : Properties.Resources.KoopaBlue; break;
                        default:
                            img = inShell ? Properties.Resources.GreenKoopaShell : Properties.Resources.KoopaGreen; break;
                    }
                    if (inShell)
                        g.DrawImage(img, RenderX, RenderY);
                    else
                        g.DrawImage(img, RenderX, RenderY - 13);
                    break;
                case 150:
                    switch (Data[5] % 0x10) {
                        case 1:
                            img = Properties.Resources.ParakoopaRed; break;
                        case 2:
                            img = Properties.Resources.ParakoopaBlue; break;
                        default:
                            img = Properties.Resources.ParakoopaGreen; break;
                    }
                    g.DrawImage(img, RenderX, RenderY - 16);
                    break;
                case 152:
                    g.DrawImage(Properties.Resources.SwitchBlock, RenderX, RenderY);
                    break;
                case 155:
                    Rectangle rect = this.getRect();
                    rect.Offset(1, 1);
                    g.DrawRectangle(Pens.Black, rect);
                    rect.Offset(-1, -1);
                    g.DrawRectangle(Pens.Lime, rect);
                    g.DrawString(LanguageManager.Get("Sprites", "Warp"), NSMBGraphics.SmallInfoFont, Brushes.Black, RenderX + 1, RenderY + 1);
                    g.DrawString(LanguageManager.Get("Sprites", "Warp"), NSMBGraphics.SmallInfoFont, Brushes.Lime, RenderX, RenderY);
                    break;
                case 157:
                    g.DrawImage(Properties.Resources.FireBro, RenderX, RenderY - 15);
                    break;
                case 158:
                    g.DrawImage(Properties.Resources.BoomerangBro, RenderX, RenderY - 18);
                    break;
                case 162:
                    RenderY += 24;
                    RenderX += 6;
                    g.DrawImage(Properties.Resources.MushroomStalkTop, RenderX, RenderY);
                    RenderY += 8;
                    g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY);
                    for (int l = 0; l <= Data[3] % 0x10; l++) {
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY);
                    }
                    RenderX = X * 16 - 16 * (Data[4] / 0x10 + 1);
                    RenderY = Y * 16;
                    img = Properties.Resources.MushroomEdge;
                    img2 = Properties.Resources.MushroomSection;
                    if (((Data[3] & 0x80) / 0x80) == 1){
                        img = Properties.Resources.MushroomEdgeGreen;
                        img2 = Properties.Resources.MushroomSectionGreen;
                    }
                    g.DrawImage(img, RenderX, RenderY);
                    RenderX += 32;
                    for (int l = 0; l < Data[4] / 0x10; l++) {
                        g.DrawImage(img2, RenderX, RenderY);
                        RenderX += 32;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 173:
                    for (int l = 0; l < Data[5] % 0x10 + 4; l++) {
                        g.DrawImage(Properties.Resources.Rope, RenderX - 3, RenderY);
                        RenderY += 16;
                    }
                    break;
                case 174:
                    RenderY += 24;
                    RenderX += 6;
                    g.DrawImage(Properties.Resources.MushroomStalkTop, RenderX, RenderY);
                    RenderY += 8;
                    g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY);
                    g.DrawImage(Properties.Resources.MushroomStalk, RenderX, RenderY + 16);
                    RenderX = X * 16 - 16 * (Data[4] / 0x10 + 1);
                    RenderY = RenderY2;
                    img = Properties.Resources.MushroomEdge;
                    img2 = Properties.Resources.MushroomSection;
                    if (((Data[3] & 0x10) / 0x10) == 1){
                        img = Properties.Resources.MushroomEdgeGreen;
                        img2 = Properties.Resources.MushroomSectionGreen;
                    }
                    g.DrawImage(img, RenderX, RenderY);
                    RenderX += 32;
                    for (int l = 0; l < Data[4] / 0x10; l++) {
                        g.DrawImage(img2, RenderX, RenderY);
                        RenderX += 32;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 175:
                    g.DrawImage(Properties.Resources.BouncyBricks, RenderX - 40, RenderY - 48);
                    break;
                case 180:
                    if (Data[5] % 0x10 == 1)
                        img = Properties.Resources.FenceKoopaRed;
                    else
                        img = Properties.Resources.FenceKoopaGreen;
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 40:
                case 183:
                    g.DrawImage(Properties.Resources.Lakitu, RenderX, RenderY - 23);
                    break;
                case 186:
                    g.DrawImage(Properties.Resources.Paragoomba, RenderX, RenderY - 12);
                    break;
                case 187:
                    g.DrawImage(Properties.Resources.ManualPlatform, RenderX, RenderY);
                    if (Data[5] % 0x10 == 0 || Data[5] % 0x10 == 2)
                        g.DrawImage(Properties.Resources.UpArrow, RenderX + 51, RenderY + 4);
                    else if (Data[5] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.DownArrow, RenderX + 51, RenderY + 4);
                    break;
                case 189:
                    g.DrawImage(Properties.Resources.Pipe, RenderX, RenderY - 48);
                    break;
                case 191:
                    RenderY2 = Data[3] / 0x10;
                    if (RenderY2 == 0) RenderY2 = 8;
                    g.DrawImage(Properties.Resources.QBlock, RenderX, RenderY + (RenderY2 - 5) * 16);
                    if (Data[5] % 0x10 <= 8)
                        g.DrawImage(Properties.Resources.HangingBlockOverrides, RenderX, RenderY + (RenderY2 - 5) * 16, new Rectangle((Data[5] % 0x10) * 16, 0, 16, 16), GraphicsUnit.Pixel);
                    g.DrawLine(Pens.White, RenderX + 7, RenderY + (RenderY2 - 5) * 16, RenderX + 7, RenderY - 80);
                    g.DrawLine(Pens.White, RenderX + 8, RenderY + (RenderY2 - 5) * 16, RenderX + 8, RenderY - 80);
                    break;
                case 193:
                    g.DrawImage(Properties.Resources.DryBonesLarge, RenderX, RenderY - 28);
                    break;
                case 194:
                    g.DrawImage(Properties.Resources.GiantThwomp, RenderX, RenderY);
                    break;
                case 197:
                    RenderY2 = Data[4] / 0x10; //Tile number
                    RenderY2 = RenderY2 < TileCreateNums.Length ? TileCreateNums[RenderY2] : 0x67; //Load actual number from hardcoded list
                    width = Data[3] % 0x10; //Checkerboard pattern
                    height = Data[4] % 0x10; //Create or destroy
                    for (int l = 1; l <= Math.Max(1, Data[5] % 0x10); l++) {
                        for (int m = 1; m <= Math.Max(1, Data[5] / 0x10); m++) {
                            if (width == 0 || width == 1 && l % 2 == m % 2 || width == 2 && l % 2 != m % 2) //Checkerboard pattern
                            {
                                if (height == 0)
                                    g.DrawImage(Properties.Resources.DestroyTile, RenderX, RenderY);
                                else
                                    if (RenderY2 < 0) //Negative number indicates an override tile
                                        g.DrawImage(Properties.Resources.tileoverrides, RenderX, RenderY, new Rectangle(-RenderY2, 0, 16, 16), GraphicsUnit.Pixel);
                                    else
                                        g.DrawImage(Level.GFX.Tilesets[0].Map16Buffer, RenderX, RenderY, new Rectangle(RenderY2 % 0x10 * 0x10, RenderY2 & 0xF0, 16, 16), GraphicsUnit.Pixel);
                            }
                            RenderX += 16;
                        }
                        RenderY += 16;
                        RenderX = RenderX2;
                    }
                    if (height == 0)
                        g.DrawRectangle(Pens.Black, this.getRect());
                    break;
                case 204:
                    g.DrawImage(Properties.Resources.JumpingFlame, RenderX, RenderY - 7);
                    break;
                case 205:
                    g.DrawImage(Properties.Resources.FlameChomp, RenderX, RenderY - 27);
                    break;
                case 206:
                    g.DrawImage(Properties.Resources.GhostGoo, RenderX, RenderY - 32);
                    break;
                case 207:
                    if (Data[5] % 0x10 == 1)
                        img = Properties.Resources.CheepCheepGiantGreen;
                    else
                        img = Properties.Resources.CheepCheepGiant;
                    g.DrawImage(img, RenderX - 20, RenderY - 32);
                    break;
                case 209:
                    g.DrawImage(Properties.Resources.GiantHammerBro, RenderX, RenderY - 15);
                    break;
                case 210:
                    g.DrawImage(Properties.Resources.VSBattleStar, RenderX, RenderY);
                    break;
                case 211:
                    g.DrawImage(Properties.Resources.Blooper, RenderX, RenderY - 16);
                    break;
                case 212:
                    g.DrawImage(Properties.Resources.BlooperNanny, RenderX, RenderY - 16);
                    break;
                case 213:
                    g.DrawImage(Properties.Resources.BlooperWithMini, RenderX - 8, RenderY - 32);
                    break;
                case 219:
                    switch (Data[5] / 0x10) {
                        case 1:
                            g.DrawImage(Properties.Resources.SpinyBeetleLeft, RenderX - 4, RenderY); break;
                        case 2:
                            g.DrawImage(Properties.Resources.SpinyBeetleDown, RenderX, RenderY); break;
                        case 3:
                            g.DrawImage(Properties.Resources.SpinyBeetleRight, RenderX, RenderY); break;
                        default:
                            g.DrawImage(Properties.Resources.SpinyBeetleUp, RenderX, RenderY - 4); break;
                    }
                    break;
                case 220:
                    if ((Data[5] & 0xF0) == 0 || (Data[5] & 0xF0) == 0x30)
                        g.DrawImage(Properties.Resources.BowserJrMasked, RenderX, RenderY - 25);
                    else
                        g.DrawImage(Properties.Resources.BowserJr, RenderX, RenderY - 25);
                    break;
                case 222:
                    if (Data[5] / 0x10 == 1)
                        RenderX += 8;
                    if (Data[5] % 0x10 == 1)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.MiniGoomba, RenderX, RenderY);
                    break;
                case 223:
                    g.DrawImage(Properties.Resources.FlipGateSmall, RenderX, RenderY);
                    break;
                case 224:
                    g.DrawImage(Properties.Resources.FlipGateLarge, RenderX, RenderY);
                    break;
                case 226:
                    RenderY += (Data[5] & 0xF0) + 64;
                    g.DrawLine(Pens.White, RenderX + 7, RenderY2, RenderX + 7, RenderY);
                    g.DrawLine(Pens.White, RenderX + 8, RenderY2, RenderX + 8, RenderY);
                    g.DrawImage(Properties.Resources.HangingScuttleBug, RenderX - 14, RenderY);
                    break;
                case 227:
                    g.DrawImage(Properties.Resources.MoneyBag, RenderX, RenderY - 5);
                    break;
                case 228:
                    g.DrawImage(Properties.Resources.RouletteBlock, RenderX, RenderY);
                    break;
                case 229:
                    g.DrawImage(Properties.Resources.PeteyPiranha, RenderX - 16, RenderY - 56);
                    break;
                case 232:
                    g.DrawImage(Properties.Resources.QBlock, RenderX, RenderY);
                    if (Data[5] % 0x10 < 10)
                        g.DrawImage(Properties.Resources.HangingBlockOverrides, RenderX, RenderY, new Rectangle((Data[5] % 0x10) * 16, 0, 16, 16), GraphicsUnit.Pixel);
                    g.DrawLine(Pens.White, RenderX + 7, RenderY - 128, RenderX + 7, RenderY);
                    g.DrawLine(Pens.White, RenderX + 8, RenderY - 128, RenderX + 8, RenderY);
                    break;
                case 233:
                    for (int l = 0; l < Data[5] % 0x10 + 4; l++) {
                        g.DrawImage(Properties.Resources.SwingingPole, RenderX - 3, RenderY);
                        RenderY += 16;
                    }
                    g.DrawImage(Properties.Resources.LineAttachment, RenderX2 - 11, RenderY2 - 11);
                    break;
                case 235:
                    if (Data[5] / 0x10 == 1)
                        RenderX += 8;
                    if (Data[5] % 0x10 == 1)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.StarCoin, RenderX, RenderY);
                    break;
                case 236:
                    if (Data[2] / 0x10 == 1)
                        RenderY += 8;
                    g.DrawImage(Properties.Resources.SpinningSquarePlatform, RenderX - 16, RenderY - 16);
                    break;
                case 237:
                    g.DrawImage(Properties.Resources.Broozer, RenderX, RenderY - 15);
                    break;
                case 238:
                    g.DrawImage(Properties.Resources.MushroomStalkTop, RenderX + 6, RenderY + 24);
                    RenderY += 32;
                    for (int l = 0; l < 5; l++){
                        g.DrawImage(Properties.Resources.MushroomStalk, RenderX + 6, RenderY);
                        RenderY += 16;
                    }
                    RenderX = X * 16 - 16 * (Data[5] / 0x10 + 1);
                    RenderY = Y * 16;
                    img = Properties.Resources.PurpleMushroomEdge;
                    g.DrawImage(img, RenderX, RenderY);
                    RenderX += 32;
                    for (int l = 0; l < Data[5] / 0x10; l++) {
                        g.DrawImage(Properties.Resources.PurpleMushroomSection, RenderX, RenderY);
                        RenderX += 32;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 239:
                    g.DrawImage(Properties.Resources.RiseLowerMushroomStalkTop, RenderX, RenderY + 24);
                    RenderY += 32;
                    for (int l = 0; l <= Math.Max(Data[5] / 0x10, Data[4] % 0x10); l++) {
                        g.DrawImage(Properties.Resources.RiseLowerMushroomStalk, RenderX, RenderY);
                        RenderY += 16;
                    }
                    RenderX -= (Data[5] % 0x10 + 1) * 8;
                    if (Data[5] / 0x10 > Data[4] % 0x10) {
                        img = Properties.Resources.LoweringMushroomEdge;
                        img2 = Properties.Resources.LoweringMushroomMiddle;
                    } else {
                        img = Properties.Resources.RisingMushroomEdge;
                        img2 = Properties.Resources.RisingMushroomMiddle;
                    }
                    g.DrawImage(img, RenderX, RenderY2);
                    for (int l = 0; l < Data[5] % 0x10; l++) {
                        RenderX += 16;
                        g.DrawImage(img2, RenderX, RenderY2);
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX + 16, RenderY2);
                    break;
                case 241:
                    img = Properties.Resources.RotatingCannon;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    for (int l = 0; l <= Math.Min(7, (int)Data[3]); l++) {
                        if ((Data[4] & (1 << l)) > 0)
                            g.DrawImage(Properties.Resources.RotatingCannonEmpty, RenderX + 2, RenderY);
                        else if ((Data[5] & (1 << l)) > 0)
                            g.DrawImage(img, RenderX - 3, RenderY);
                        else
                            g.DrawImage(Properties.Resources.RotatingCannon, RenderX + 1, RenderY);
                        RenderY -= 16;
                    }
                    break;
                case 242:
                    RenderX2 = Data[5] % 0x10;
                    if (Data[5] / 0x10 == 1) {
                        if (Data[4] % 0x10 == 1)
                            g.DrawImage(Properties.Resources.ExpandedMushroomS, RenderX - 32, RenderY);
                        else
                            g.DrawImage(Properties.Resources.ExpandedMushroomL, RenderX - 72, RenderY);
                        RenderX2++;
                        RenderY2 -= 16;
                    }
                    else
                        g.DrawImage(Properties.Resources.ContractedMushroom, RenderX - 8, RenderY);
                    RenderY2 += 32;
                    for (int l = 0; l < RenderX2; l++) {
                        if (l == 0)
                            g.DrawImage(Properties.Resources.ExpandMushroomStalkTop, RenderX, RenderY2);
                        else
                            g.DrawImage(Properties.Resources.ExpandMushroomStalk, RenderX, RenderY2);
                        RenderY2 += 16;
                    }
                    break;
                case 243:
                    g.DrawImage(Properties.Resources.RoofSpiny, RenderX, RenderY);
                    break;
                case 244:
                    switch (Data[5] / 0x10) {
                        case 1:
                            RenderX -= 8; break;
                        case 2:
                            RenderX += 8; break;
                    }
                    g.DrawImage(Properties.Resources.BouncingMushroom, RenderX - 40, RenderY);
                    RenderY += 32;
                    RenderX += 3;
                    for (int l = 0; l < Data[5] % 0x10; l++) {
                        g.DrawImage(Properties.Resources.BouncingMushroomStalk, RenderX, RenderY);
                        RenderY += 16;
                    }
                    break;
                case 245:
                    int PumpX = RenderX + (Data[5] & 0xF0);
                    int PumpY = RenderY - (Data[4] & 0xF0);
                    int PumpShiftX = (Data[4] % 0x2);
                    int PumpShiftY = (Data[3] % 0x2);
                    if (PumpShiftX == 1)
                        PumpX -= 336;
                    if (PumpShiftY == 1)
                        PumpY += 256;
                    g.DrawImage(Properties.Resources.SwollenPipe, RenderX, RenderY - 56);
                    g.DrawImage(Properties.Resources.Pump, PumpX + 48, PumpY - 15);
                    break;
                case 246:
                    g.DrawImage(Properties.Resources.Barrel, RenderX - 16, RenderY - 8);
                    break;
                case 247:
                    g.DrawImage(Properties.Resources.Shark, RenderX - 27, RenderY - 8);
                    g.DrawImage(Properties.Resources.Infinity, RenderX - 4, RenderY + 1);
                    break;
                case 248:
                    g.DrawImage(Properties.Resources.BalloonBoo, RenderX - 53, RenderY - 106);
                    break;
                case 249:
                    RenderX -= Data[5] % 0x10 * 8 + 24;
                    RenderY += Data[4] / 0x10 * 16 + 88;
                    int XOff = Data[5] % 0x10 * 16 + 48;
                    for (int l = 0; l < Data[4] / 0x10 + 4; l++) {
                        RenderY -= 16;
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX, RenderY);
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX + XOff, RenderY);
                    }
                    for (int l = 0; l < Data[5] % 0x10 + 2; l++){
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.WallJumpPlatformBlock, RenderX, RenderY);
                    }
                    g.DrawImage(Properties.Resources.LineAttachment, X * 16 - 3, Y * 16 + 5);
                    break;
                case 250:
                    g.DrawImage(Properties.Resources.Crow, RenderX, RenderY);
                    break;
                case 252:
                    g.DrawImage(Properties.Resources.BanzaiBillCannon, RenderX - 32, RenderY - 64);
                    break;
                case 254:
                    g.DrawImage(Properties.Resources.Kabomb, RenderX, RenderY - 3);
                    break;
                case 255:
                    g.DrawImage(Properties.Resources.Jungle, RenderX, RenderY);
                    break;
                case 256:
                    img = Properties.Resources.ThruWallPlatform;
                    if (Data[3] / 0x10 == 0)
                        g.DrawImage(img, RenderX - 80, RenderY);
                    else {
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        g.DrawImage(img, RenderX, RenderY);
                    }
                    break;
                case 265:
                    g.DrawImage(Properties.Resources.Hand, RenderX + 8, RenderY);
                    break;
                case 268:
                    g.DrawImage(Properties.Resources.UnderwaterBounceBall, RenderX - 26, RenderY - 26);
                    break;
                case 272:
                    if (Data[5] % 0x10 == 1)
                        g.DrawImage(Properties.Resources.SnowBranchLeft, RenderX - 31, RenderY - 12);
                    else
                        g.DrawImage(Properties.Resources.SnowBranchRight, RenderX + 15, RenderY - 12);
                    break;
                case 273:
                    g.DrawImage(Properties.Resources.SnowballThrower, RenderX, RenderY);
                    break;
                case 274:
                    img = Properties.Resources.SinkingSnowEdge;
                    g.DrawImage(img, RenderX - 16, RenderY);
                    for (int l = 0; l <= Data[5] % 0x10; l++) {
                        g.DrawImage(Properties.Resources.SinkingSnow, RenderX, RenderY);
                        RenderX += 16;
                    }
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 275:
                    RenderY -= 14;
                    g.DrawImage(Properties.Resources.JumpingQBlock, RenderX, RenderY);
                    if ((Data[4] % 0x10) <= 3)
                        g.DrawImage(Properties.Resources.JumpingQBlockOverrides, RenderX, RenderY + 3, new Rectangle((Data[4] % 0x10) * 16, 0, 16, 16), GraphicsUnit.Pixel);
                    for (int l = 0; l < Data[5] % 0x10; l++) {
                        RenderY -= 16;
                        g.DrawImage(Properties.Resources.Brick, RenderX, RenderY);
                    }
                    break;
                case 277:
                    int direction = Data[2] % 8;
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
                    g.DrawImage(ScaleBitmap(Properties.Resources.GroundpoundGoo, (Data[5] % 0x10 + 1) * 16,
                        (Data[5] / 0x10 + 1) * 16), RenderX, RenderY);
                    break;
                case 279:
                    img = Properties.Resources.OneWayDoor;
                    if (Data[5] == 0 || Data[5] == 1) {
                        img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        RenderY -= 39;
                    } else if (Data[5] == 2 || Data[5] == 3) {
                        RenderY += 16;
                    } else if (Data[5] == 4 || Data[5] == 5) {
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        RenderX += 16;
                    } else if (Data[5] == 6 || Data[5] == 7) {
                        img.RotateFlip(RotateFlipType.Rotate270FlipX);
                        RenderX -= 39;
                    }
                    if (Data[5] == 0 || Data[5] == 2)
                        img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    else if (Data[5] == 4 || Data[5] == 6)
                        img.RotateFlip(RotateFlipType.RotateNoneFlipY);
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 281:
                    img = Properties.Resources.PipeCaterpiller;
                    if (Data[5] % 0x10 == 1) {
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        g.DrawImage(img, RenderX + 10, RenderY + 16);
                    } else
                        g.DrawImage(img, RenderX + 10, RenderY - 15);
                    break;
                case 282:
                    RenderX -= 16;
                    g.DrawImage(Properties.Resources.VineTop, RenderX, RenderY);
                    for (int l = 0; l < Data[5] % 0x10 + 2; l++ ) {
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.Vine, RenderX, RenderY);
                    }
                    g.DrawImage(Properties.Resources.VineBottom, RenderX, RenderY + 16);
                    break;
                case 283:
                    g.DrawImage(Properties.Resources.SpikeBass, RenderX, RenderY - 32);
                    break;
                case 284:
                    g.DrawImage(Properties.Resources.Pumpkin, RenderX, RenderY - 2);
                    break;
                case 285:
                    g.DrawImage(Properties.Resources.ScuttleBug, RenderX, RenderY - 8);
                    break;
                case 289:
                    g.DrawImage(Properties.Resources.ExpandableBlock, RenderX, RenderY);
                    break;
                case 290:
                    g.DrawImage(Properties.Resources.FlyingQBlock, RenderX - 9, RenderY - 13);
                    if (Data[5] % 0x10 < 8)
                        g.DrawImage(Properties.Resources.FlyingQBlockOverrides, RenderX, RenderY, new Rectangle((Data[5] % 0x10) * 16, 0, 16, 16), GraphicsUnit.Pixel);
                    break;
                case 291:
                    g.DrawImage(Level.GFX.Tilesets[0].Map16Buffer, RenderX, RenderY, new Rectangle(0, 0x50, 16, 16), GraphicsUnit.Pixel);
                    g.DrawImage(Properties.Resources.QSwitch, RenderX + 2, RenderY + 1, 12, 14);
                    break;
                case 292:
                    g.DrawImage(Properties.Resources.Door2, RenderX, RenderY - 32);
                    break;
                case 295:
                    g.DrawImage(Properties.Resources.MummiPokey, RenderX - 256, RenderY + 16);
                    break;
                case 71:
                case 72:
                case 297:
                case 298:
                    width = Data[5] % 0x10;
                    if (this.Type == 71)
                        width = Data[5] % 0x8;
                    int spikes = 0;
                    if (this.Type == 298) {
                        height = Data[5] / 0x10;
                        spikes = Data[2] % 0x10;
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
                    }
                    else
                        height = (int) (Math.Pow(2,((Data[4] & 0x30) / 0x10)) - 1);
                    if (width < 1) width = 1;
                    if (height < 0) height = 0;
                    int xp = RenderX;
                    int yp = RenderY;
                    if (height == 0) {
                        g.DrawImage(Properties.Resources.StoneBlockFlatHorizLeft, RenderX, RenderY);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX += 16;
                            g.DrawImage(Properties.Resources.StoneBlockFlatHorizMiddle, RenderX, RenderY);
                        }
                        g.DrawImage(Properties.Resources.StoneBlockFlatHorizRight, RenderX + 16, RenderY);
                    }
                    else {
                        g.DrawImage(Properties.Resources.StoneBlockTopLeft, RenderX, RenderY);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX += 16;
                            g.DrawImage(Properties.Resources.StoneBlockTop, RenderX, RenderY);
                        }
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.StoneBlockTopRight, RenderX, RenderY);
                        for (int l = 0; l < height - 1; l++) {
                            RenderY += 16;
                            g.DrawImage(Properties.Resources.StoneBlockRight, RenderX, RenderY);
                        }
                        RenderY += 16;
                        g.DrawImage(Properties.Resources.StoneBlockBottomRight, RenderX, RenderY);
                        for (int l = 0; l < width - 1; l++) {
                            RenderX -= 16;
                            g.DrawImage(Properties.Resources.StoneBlockBottom, RenderX, RenderY);
                        }
                        RenderX -= 16;
                        g.DrawImage(Properties.Resources.StoneBlockBottomLeft, RenderX, RenderY);
                        for (int l = 0; l < height - 1; l++) {
                            RenderY -= 16;
                            g.DrawImage(Properties.Resources.StoneBlockLeft, RenderX, RenderY);
                        }
                        RenderX = xp;
                        RenderY = yp;
                        int xStart = RenderX;
                        for (int l = 0; l < height - 1; l++) {
                            RenderY += 16;
                            for (int m = 0; m < width - 1; m++) {
                                RenderX += 16;
                                g.DrawImage(Properties.Resources.StoneBlockMiddle, RenderX, RenderY);
                            }
                            RenderX = xStart;
                        }
                    }
                    if (spikes == 1 || spikes == 3) {
                        RenderX = xp;
                        RenderY = yp - 16;
                        for (int l = 0; l <= width; l++)  {
                            g.DrawImage(Properties.Resources.StoneBlockSpikes, RenderX, RenderY);
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
                        for (int l = 0; l <= Data[5] % 0x10; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY);
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
                        for (int l = 0; l <= Data[5] / 0x10; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY);
                            RenderY += 16;
                        }
                    }
                    if (spikes == 5 || spikes == 6) {
                        RenderX = xp - 16;
                        RenderY = yp;
                        Bitmap spikes2 = Properties.Resources.StoneBlockSpikes;
                        spikes2.RotateFlip(RotateFlipType.Rotate90FlipX);
                        for (int l = 0; l <= height; l++) {
                            g.DrawImage(spikes2, RenderX, RenderY);
                            RenderY += 16;
                        }
                    }
                    break;
                case 300:
                    RenderY += -8 + Data[5] % 0x10;
                    g.DrawImage(Properties.Resources.GhostPlatform, RenderX - 64, RenderY);
                    break;
                case 303:
                    img = Properties.Resources.SpinningSpikeBall;
                    int BallChainX = RenderX;
                    int BallChainY = RenderY;
                    if ((Data[5] % 0x4) == 0){
                        BallChainX += 4;
                        BallChainY -= 11;
                    }
                    if ((Data[5] % 0x4) == 1){
                        img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        BallChainX -= 11;
                        BallChainY -= 83;
                    }
                    if ((Data[5] % 0x4) == 2){
                        img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        BallChainX -= 83;
                        BallChainY -= 11;
                    }
                    if ((Data[5] % 0x4) == 3){
                        img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        BallChainX -= 11;
                        BallChainY += 4;
                    }
                    g.DrawImage(img, BallChainX, BallChainY);
                    break;
                case 260:
                case 304:
                    g.DrawImage(Properties.Resources.GiantSpike, RenderX, RenderY - 128);
                    break;
                case 305:
                    customRendered = false;
                    RenderX2 = Math.Max(0, ((Data[4] >> 4) + ((Data[4] & 0xF) << 4) - 15) * 16);
                    if (RenderX2 != 0) {
                        g.FillRectangle(Brushes.Black, RenderX, RenderY + 6, RenderX2, 4);
                        g.FillRectangle(Brushes.Black, RenderX + RenderX2 - 4, RenderY, 4, 10);
                        g.FillRectangle(Brushes.White, RenderX + 1, RenderY + 7, RenderX2 - 2, 2);
                        g.FillRectangle(Brushes.White, RenderX + RenderX2 - 3, RenderY + 1, 2, 8);
                    }
                    renderDefaultImg(g, RenderX, RenderY);
                    break;
                case 306:
                    customRendered = false;
                    RenderX2 = (Data[5] & 0xF0) - 16;
                    if (RenderX2 != 0) {
                        g.FillRectangle(Brushes.Black, RenderX + 6, RenderY - RenderX2, 4, RenderX2 + 16);
                        g.FillRectangle(Brushes.Black, RenderX, RenderY - RenderX2, 16, 4);
                        g.FillRectangle(Brushes.White, RenderX + 7, RenderY - RenderX2 + 1, 2, RenderX2 + 14);
                        g.FillRectangle(Brushes.White, RenderX + 1, RenderY - RenderX2 + 1, 14, 2);
                    }
                    renderDefaultImg(g, RenderX, RenderY);
                    break;
                case 261:
                case 307:
                    img = Properties.Resources.GiantSpike;
                    img.RotateFlip(RotateFlipType.Rotate180FlipNone);
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 262:
                case 308:
                    img = Properties.Resources.GiantSpike;
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    g.DrawImage(img, RenderX, RenderY);
                    break;
                case 263:
                case 309:
                    img = Properties.Resources.GiantSpike;
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                    g.DrawImage(img, RenderX - 128, RenderY);
                    break;
                case 312:
                    img = Properties.Resources.GreenMushroomEdge;
                    img.RotateFlip(RotateFlipType.RotateNoneFlipX);
                    width = Data[5] % 0x10 + 1;
                    RenderX -= width * 8;
                    g.DrawImage(Properties.Resources.GreenMushroomEdge, RenderX, RenderY);
                    for (int l = 0; l <= width - 2; l++) {
                        RenderX += 16;
                        g.DrawImage(Properties.Resources.GreenMushroomMiddle, RenderX, RenderY);
                    }
                    g.DrawImage(img, RenderX + 16, RenderY);
                    g.DrawImage(Properties.Resources.GreenMushroomStalkTop, RenderX2, RenderY + 24);
                    for (int l = 0; l < Data[5] / 0x10; l++) {
                        g.DrawImage(Properties.Resources.GreenMushroomStalk, RenderX2, RenderY + 49);
                        RenderY += 16;
                    }
                    break;
                case 323:
                    g.DrawImage(Properties.Resources.CloudLeftEdge, RenderX, RenderY);
                    RenderX += 16;
                    for (int l = 0; l <= Data[5]; l++) {
                        g.DrawImage(Properties.Resources.CloudSection, RenderX, RenderY);
                        RenderX += 32;
                    }
                    g.DrawImage(Properties.Resources.CloudRightEdge, RenderX, RenderY);
                    break;
                default:
                    customRendered = false;
                    renderDefaultImg(g, RenderX, RenderY);
                    break;
            }

            if (!Level.ValidSprites[this.Type] && customRendered) {
                g.FillRectangle(invalidBrush, this.getRect());
            }

            //I dunno what's this user for. ~Dirbaio
//            return customRendered;
        }

        private void renderDefaultImg(Graphics g, int RenderX, int RenderY)
        {
            Bitmap img;
            if (Level.ValidSprites[this.Type])
                img = Properties.Resources.sprite;
            else
                img = Properties.Resources.sprite_invalid;
            g.DrawImage(img, RenderX, RenderY);
            g.DrawString(Type.ToString(), NSMBGraphics.SmallInfoFont, Brushes.White, (float)RenderX, (float)RenderY);
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

        public override string ToString()
        {
            return String.Format("SPR:{0}:{1}:{2}:{3:X2}{4:X2}{5:X2}{6:X2}{7:X2}{8:X2}", X, Y, Type,
                Data[0], Data[1], Data[2], Data[3], Data[4], Data[5]);
        }

        public static NSMBSprite FromString(string[] strs, ref int idx, NSMBLevel lvl)
        {
            NSMBSprite s = new NSMBSprite(lvl);
            s.X = int.Parse(strs[1 + idx]);
            s.Y = int.Parse(strs[2 + idx]);
            s.Type = int.Parse(strs[3 + idx]);
            s.Data = new byte[6];
            for (int l = 0; l < 6; l++) {
                s.Data[l] = byte.Parse(strs[4 + idx].Substring(l * 2, 2), System.Globalization.NumberStyles.HexNumber);
            }
            idx += 5;
            return s;
        }
    }
}
