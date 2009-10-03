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
                res.values = ReadStringList(sr);
            
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

        private static string[] ReadStringList(StreamReader sr)
        {
            List<string> res = new List<string>();
            String s = ReadLine(sr);
            int start = LineNum;
            while (s != "end list")
            {
                if (s == null)
                    throw new Exception("Error: List not terminated. Started at line: "+start);
                res.Add(s);
                s = ReadLine(sr);
            }
            return res.ToArray();
        }

        public class SpriteDataValue
        {
            public string name;
            public string display;
            public string[] values;
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
                else
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
                else
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

            byte[] sdata;
            SpriteData sd;
            LevelEditorControl EdControl;

            public SpriteDataEditor(byte[] sdata, SpriteData sd, LevelEditorControl EdControl)
            {
                this.ColumnCount = 2;
                this.RowCount = sd.values.Count;
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                foreach (ColumnStyle cs in this.ColumnStyles)
                    cs.SizeType = SizeType.AutoSize;
                foreach (RowStyle cs in this.RowStyles)
                    cs.SizeType = SizeType.AutoSize;

                this.sdata = sdata;
                this.sd = sd;
                this.Dock = DockStyle.Fill;
                this.EdControl = EdControl;

                int row = 0;
                foreach (SpriteDataValue v in sd.values)
                {
                    Control c = CreateControlFor(v);
                    c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    if (c is CheckBox)
                    {
                        this.Controls.Add(c, 0, row);
                        this.SetColumnSpan(c, 2);
                    }
                    else 
                        this.Controls.Add(c, 1, row);

                    if (v.display != "checkbox")
                    {
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
            }

            private Control CreateControlFor(SpriteDataValue v)
            {
                if (v.display == "checkbox")
                {
                    CheckBox c = new CheckBox();
                    c.Checked = v.vs.getValue(sdata) == 1;
                    c.Text = v.name;
                    c.CheckedChanged += new EventHandler(saveData);
                    return c;
                }
                else if (v.display == "list")
                {
                    ComboBox c = new ComboBox();
                    c.DropDownStyle = ComboBoxStyle.DropDownList;
                    c.Items.AddRange(v.values);
                    try
                    {
                        c.SelectedIndex = v.vs.getValue(sdata);
                    }
                    catch (ArgumentOutOfRangeException) { } //just in case
                    c.SelectedIndexChanged += new EventHandler(saveData);
                    c.SelectionChangeCommitted += new EventHandler(saveData);
                    c.DropDownClosed += new EventHandler(saveData);
                    
                    return c;
                }
                else
                {
                    NumericUpDown c = new NumericUpDown();
                    c.Minimum = v.vs.getMin();
                    c.Maximum = v.vs.getMax();
                    c.ValueChanged += new EventHandler(saveData);
                    c.Value = v.vs.getValue(sdata);
                    return c;
                }
            }
            public void saveData(object sender, EventArgs e)
            {
                foreach(SpriteDataValueSource s in controls.Keys)
                {
                    int val = 0;
                    if (controls[s] is NumericUpDown)
                        val = (int)(controls[s] as NumericUpDown).Value;
                    else if (controls[s] is ComboBox)
                        val = (int)(controls[s] as ComboBox).SelectedIndex;
                    else if (controls[s] is CheckBox)
                        val = (controls[s] as CheckBox).Checked ? 1 : 0;

                    s.setValue(val, sdata);
                }

                EdControl.FireSetDirtyFlag();
            }
        }
    }
}
