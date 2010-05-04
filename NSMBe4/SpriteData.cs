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
using System.IO;
using System.Windows.Forms;

namespace NSMBe4
{
    public class SpriteData
    {
        public static int LineNum = 0;
        public static Dictionary<int, SpriteData> datas;

        public List<int> numbers = new List<int>();
        public List<SpriteDataValue> values = new List<SpriteDataValue>();

        public static void Load()
        {
            //datas = new Dictionary<int, SpriteData>();
            //return;

            FileStream fs = new FileStream("./spritedata.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fs);

            try
            {

                datas = new Dictionary<int, SpriteData>();
                SpriteData d = readFromStream(sr);

                while (d != null && !sr.EndOfStream)
                {
                    foreach (int i in d.numbers)
                        datas.Add(i, d);
                    d = readFromStream(sr);
                }

                sr.Close();
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Exception at line " + LineNum);
                Console.Out.WriteLine(e.Message);
                Console.Out.WriteLine(e.StackTrace);
                MessageBox.Show("Error parsing spritedata.txt:\n" + e.Message + "\nAt line: " + LineNum, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                datas.Clear();
            }
        }

        public static SpriteData readFromStream(StreamReader sr)
        {
            string header = ReadLine(sr);
            if (header == "end file")
                return null;

            string[] headerA = header.Split(new char[]{' '});
            if (headerA[0] != "sprite")
                return null;

            SpriteData sd = new SpriteData();
            for(int i = 1; i < headerA.Length; i++)
                sd.numbers.Add(Int32.Parse(headerA[i]));

            while (true)
            {
                String s = ReadLine(sr);
                if (s == "end sprite")
                    break;

                string com = s.Substring(0, s.IndexOf(" ")).Trim();
                string val = s.Substring(s.IndexOf(" ")+1).Trim();

                if (com == "value")
                    sd.values.Add(ReadValueFromStream(sr, val));
            }

            return sd;
        }

        private static SpriteDataValue ReadValueFromStream(StreamReader sr, string s)
        {
            SpriteDataValue res = new SpriteDataValue();
            string com = s.Substring(0, s.IndexOf(" ")).Trim();
            string val = s.Substring(s.IndexOf(" ") + 1).Trim();
            res.name = val;
            res.display = com;
            if (res.display == "list")
                setList(sr, res);

            if (res.display == "label") {
                res.vs = new SpriteDataValueSource();
                res.vs.byteNum = -1;
            } else
                res.vs = ReadValueSourceFromStream(sr);
            return res;
        }

        private static SpriteDataValueSource ReadValueSourceFromStream(StreamReader sr)
        {
            SpriteDataValueSource source = new SpriteDataValueSource();
            string[] ss = ReadLine(sr).Split(new char[] { ' ' });
            if (ss[0] == "split")
            {
                source.type = ValueSourceType.SPLIT;
                source.split1 = ReadValueSourceFromStream(sr);
                source.split2 = ReadValueSourceFromStream(sr);
            }
            else
            {
                string loc = ss[0];
                if (loc.Contains("."))
                {
                    source.type = ValueSourceType.NIBBLE;
                    source.secondNibble = loc.EndsWith(".2");
                    loc = loc.Substring(0, loc.IndexOf('.'));
                }
                source.byteNum = int.Parse(loc)-1;

                for (int i = 1; i < ss.Length; i++)
                {
                    if (ss[i] == "plus")
                        source.plus += int.Parse(ss[++i]);
                    if (ss[i] == "signed")
                        source.signed = true;
                }
            }
            return source;
        }

        private static string ReadLine(StreamReader sr)
        {
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine().Trim();
                LineNum++;

                if (s.StartsWith("//")) //it's a comment (like this one :P)
                    continue;
                if (s.Length == 0)
                    continue;
                return s;
            }
            return null;
        }

        private static void setList(StreamReader sr, SpriteDataValue sdv)
        {
            List<int> ints = new List<int>();
            List<string> strings = new List<string>();
            int start = LineNum;
            int count = 0;
            String s = ReadLine(sr);
            String[] s2 = s.Split(' ');
            while (s != "end list")
            {
                if (s == null)
                    throw new Exception("Error: List not terminated. Started at line: " + start);
                if (s2.Length >= 3 && s2[s2.Length - 2] == "=") {
                    ints.Add(int.Parse(s2[s2.Length - 1]));
                    strings.Add(s.Substring(0, s.IndexOf(" = ")));
                } else {
                    ints.Add(count);
                    strings.Add(s);
                }

                s = ReadLine(sr);
                s2 = s.Split(' ');
                count++;
            }
            sdv.values = ints.ToArray();
            sdv.strings = strings.ToArray();
        }

        public class SpriteDataValue
        {
            public string name;
            public string display;
            public int[] values;
            public string[] strings;
            public SpriteDataValueSource vs;
        }

        public class SpriteDataValueSource
        {
            public ValueSourceType type = ValueSourceType.BYTE;

            public bool signed = false;
            public int plus = 0;
            public int byteNum;
            public bool secondNibble;

            public SpriteDataValueSource split1, split2;

            public int getValue(byte[] data)
            {
                if (type == ValueSourceType.SPLIT)
                {
                    return split1.getValue(data) * 16 + split2.getValue(data);
                }
                else if (byteNum > -1)
                {
                    int b = data[byteNum];
                    if (type == ValueSourceType.NIBBLE)
                        if (secondNibble)
                            b = b % 16;
                        else
                            b = b / 16;

                    if (signed) // Two's complement
                    {
                        if (type == ValueSourceType.NIBBLE)
                        {
                            if (b >= 8) b -= 16;
                        }
                        else
                            if (b >= 128) b -= 256;
                    }

                    b+=plus;
                    return b;
                }
                return 0;
            }

            public void setValue(int b, byte[] data)
            {
                b-=plus;

                if (signed)
                    if (type == ValueSourceType.NIBBLE)
                    {
                        if (b < 0) b += 16;
                    }
                    else
                        if (b < 0) b += 256;

                if (type == ValueSourceType.NIBBLE)
                {
                    b %= 16;
                    byte mask = 0xF;
                    if (secondNibble)
                        mask *= 16;
                    else
                        b *= 16;

                    data[byteNum] &= mask;
                    data[byteNum] |= (byte) b;
                }
                else if(byteNum >= 0 && byteNum <= 5)
                    data[byteNum] = (byte) b;
            }

            public int getMin()
            {

                if (!signed)
                    return 0 + plus;

                if (type == ValueSourceType.NIBBLE)
                    return -8 + plus;
                else
                    return -128 + plus;
            }

            public int getMax()
            {
                if (type == ValueSourceType.NIBBLE)
                    if (signed)
                        return 7 + plus;
                    else
                        return 15 + plus;
                else
                    if (signed)
                        return 127 + plus;
                    else
                        return 255 + plus;
            }
        }

        public enum ValueSourceType:int
        {
            BYTE = 0,
            NIBBLE = 1,
            SPLIT = 2
        }

        public class SpriteDataEditor : TableLayoutPanel
        {
            Dictionary<SpriteDataValueSource, Control> controls = new Dictionary<SpriteDataValueSource, Control>();

            NSMBSprite s;
            SpriteData sd;
            LevelEditorControl EdControl;
            public bool updating = false;

            public SpriteDataEditor(NSMBSprite s, SpriteData sd, LevelEditorControl EdControl)
            {
                updating = true;
                this.ColumnCount = 2;
                this.RowCount = sd.values.Count;
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                foreach (ColumnStyle cs in this.ColumnStyles)
                    cs.SizeType = SizeType.AutoSize;
                foreach (RowStyle cs in this.RowStyles)
                    cs.SizeType = SizeType.AutoSize;

                this.s = s;
                this.sd = sd;
                this.Dock = DockStyle.Fill;
                this.EdControl = EdControl;

                int row = 0;
                foreach (SpriteDataValue v in sd.values)
                {
                    Control c = CreateControlFor(v);
                    c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    if (c is CheckBox || c is Label)
                    {
                        this.Controls.Add(c, 0, row);
                        this.SetColumnSpan(c, 2);
                    }
                    else {
                        this.Controls.Add(c, 1, row);
                        Label l = new Label();
                        l.Text = v.name;
                        l.Anchor = AnchorStyles.Left;
                        l.AutoSize = false;
                        l.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        this.Controls.Add(l, 0, row);
                    }
                    row++;
                    controls.Add(v.vs, c);
                }
                updating = false;
            }

            public void UpdateData()
            {
                updating = true;
                foreach (SpriteDataValue v in sd.values)
                    updateValue(v);
                updating = false;
            }

            private Control CreateControlFor(SpriteDataValue v)
            {
                if (v.display == "checkbox")
                {
                    CheckBox c = new CheckBox();
                    c.Checked = v.vs.getValue(s.Data) == 1;
                    c.Text = v.name;
                    c.CheckedChanged += new EventHandler(saveData);
                    return c;
                }
                else if (v.display == "list")
                {
                    ComboBox c = new ComboBox();
                    c.DropDownStyle = ComboBoxStyle.DropDownList;
                    c.Items.AddRange(v.strings);
                    try
                    {
                        c.SelectedIndex = Array.IndexOf(v.values, v.vs.getValue(s.Data));
                    }
                    catch (ArgumentOutOfRangeException) { } //just in case
                    //c.SelectedIndexChanged += new EventHandler(saveData);
                    c.SelectionChangeCommitted += new EventHandler(saveData);
                    //c.DropDownClosed += new EventHandler(saveData);
                    
                    return c;
                }
                else if (v.display == "label")
                {
                    Label c = new Label();
                    c.Text = v.name;
                    return c;
                }
                else if (v.display == "binary")
                {
                    BinaryEdit c = new BinaryEdit();
                    if (v.vs.type == ValueSourceType.NIBBLE)
                        c.CheckBoxCount = 4;
                    else
                        c.CheckBoxCount = 8;
                    c.value = v.vs.getValue(s.Data);
                    c.ValueChanged += new EventHandler(saveData);
                    return c;
                }
                else
                {
                    NumericUpDown c = new NumericUpDown();
                    c.Minimum = v.vs.getMin();
                    c.Maximum = v.vs.getMax();
                    c.Value = v.vs.getValue(s.Data);
                    c.ValueChanged += new EventHandler(saveData);
                    return c;
                }
            }

            public void updateValue(SpriteDataValue v)
            {
                Control c = controls[v.vs];
                int value = v.vs.getValue(s.Data);
                if (c is CheckBox)
                    (c as CheckBox).Checked = value == 1;
                if (c is ComboBox)
                    (c as ComboBox).SelectedIndex = Array.IndexOf(v.values, value);
                if (c is BinaryEdit)
                    (c as BinaryEdit).value = value;
                if (c is NumericUpDown)
                    (c as NumericUpDown).Value = value;
            }
            public void saveData(object sender, EventArgs e)
            {
                byte[] orig = s.Data.Clone() as byte[];
                int index = 0;
                foreach(SpriteDataValueSource sv in controls.Keys)
                {
                    int val = 0;
                    if (controls[sv] is NumericUpDown)
                        val = (int)(controls[sv] as NumericUpDown).Value;
                    else if (controls[sv] is ComboBox) {
                        int se = (controls[sv] as ComboBox).SelectedIndex;
                        if (se == -1)
                            val = 0;
                        else
                            val = sd.values[index].values[(controls[sv] as ComboBox).SelectedIndex];
                    }
                    else if (controls[sv] is CheckBox)
                        val = (controls[sv] as CheckBox).Checked ? 1 : 0;
                    else if (controls[sv] is BinaryEdit)
                        val = (controls[sv] as BinaryEdit).value;
                    sv.setValue(val, s.Data);
                    index++;
                }
                if (!updating && sender != null) {
                    byte[][] datas = new byte[2][];
                    datas[0] = new byte[6];
                    datas[1] = new byte[6];
                    Array.Copy(orig, datas[0], 6);
                    Array.Copy(s.Data.Clone() as byte[], datas[1], 6);
                    EdControl.UndoManager.PerformAction(UndoType.ChangeSpriteData, s, datas);
                }
                EdControl.FireSetDirtyFlag();
                EdControl.Invalidate(true);
            }
        }
    }
}
