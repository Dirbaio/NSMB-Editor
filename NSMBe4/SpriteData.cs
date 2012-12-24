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
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Xml;

namespace NSMBe4
{
    public class SpriteData
    {
        public static Dictionary<int, SpriteData> datas = new Dictionary<int,SpriteData>();
        public static List<string> spriteNames = new List<string>();
        public static List<int> categoryIds = new List<int>();
        public static List<string> categories = new List<string>();
        public static Dictionary<int, List<int>> spritesInCategory = new Dictionary<int, List<int>>();
        public static string directory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NSMBe");
        public static string path = System.IO.Path.Combine(directory, "spritedata.xml");
        
        int spriteID;
        int categoryID;
        string name;
        string notes;
        public List<SpriteDataField> fields = new List<SpriteDataField>();

        public static string DownloadWebPage(string Url)
        {
            // Open a connection
            HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(Url);

            // You can also specify additional header values like 
            // the user agent or the referer:
            WebRequestObject.UserAgent = "NSMBe/5.2";
            WebRequestObject.Referer = "";

            // Request response:
            WebResponse Response = WebRequestObject.GetResponse();

            // Open data stream:
            Stream WebStream = Response.GetResponseStream();

            // Create reader object:
            StreamReader Reader = new StreamReader(WebStream);

            // Read the entire stream content:
            string PageContent = Reader.ReadToEnd();

            // Cleanup
            Reader.Close();
            WebStream.Close();
            Response.Close();

            return PageContent;
        }

        public static void update()
        {
            try
            {
                string data = DownloadWebPage("http://nsmbhd.net/spritexml.php");
                
                if (data.Trim() == "")
                    throw new Exception("Got empty data");

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(data);
                sw.Close();
                fs.Close();
                MessageBox.Show("Spritedata.xml has successfully been updated.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating the sprite data file: "+e.Message);
            }
        }

        public static void Load()
        {
            datas = new Dictionary<int, SpriteData>();

            if (!File.Exists(path))
            {
                if (MessageBox.Show("spritedata.xml not found. Do you want to download it?", "Hello!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    update();
                else
                    return;
            }
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                XmlReader xmlr = XmlReader.Create(fs);

                xmlr.ReadToFollowing("category");
                do
                {
                    int id = int.Parse(xmlr.GetAttribute("id"));
                    categoryIds.Add(id);
                    categories.Add(xmlr.ReadElementContentAsString());
                    spritesInCategory.Add(id, new List<int>());
                } while (xmlr.ReadToNextSibling("category"));

                while (xmlr.ReadToFollowing("sprite"))
                {
                    SpriteData d = readFromStream(xmlr);
                    if (d != null)
                        datas.Add(d.spriteID, d);
                }

                xmlr.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Error parsing spritedata.xml:\n" + e.Message + "\n"+e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                datas.Clear();
            }
        } 
        
        public static SpriteData readFromStream(XmlReader xmlr)
        {
            SpriteData sd = new SpriteData();

            sd.spriteID = int.Parse(xmlr.GetAttribute("id"));
            xmlr.ReadToFollowing("name");
            sd.name = xmlr.ReadElementContentAsString();
            spriteNames.Add(sd.name);
            xmlr.ReadToFollowing("category");
            sd.categoryID = int.Parse(xmlr.GetAttribute("id"));
            spritesInCategory[sd.categoryID].Add(sd.spriteID);
            xmlr.ReadToFollowing("notes");
            sd.notes = xmlr.ReadElementContentAsString();
            while (xmlr.ReadToNextSibling("field"))
            {
                SpriteDataField f = new SpriteDataField();
                f.display = xmlr.GetAttribute("type");
                f.name = xmlr.GetAttribute("name");
                f.notes = xmlr.GetAttribute("notes");
                string nybbles = xmlr.GetAttribute("id");
                if (nybbles.Contains("-"))
                {
                    string[] nybbles2 = nybbles.Split('-');
                    f.startNibble = int.Parse(nybbles2[0]);
                    f.endNibble = int.Parse(nybbles2[1]);
                }
                else
                    f.startNibble = f.endNibble = int.Parse(nybbles);
                string values = xmlr.GetAttribute("values");
                switch (f.display)
                {
                    case "list":
                        string[] items = values.Split(',');
                        f.values = new int[items.Length];
                        f.strings = new string[items.Length];

                        for (int j = 0; j < items.Length; j++)
                        {
                            string[] lulz = items[j].Split(new char[] { '=' });
                            f.values[j] = Int32.Parse(lulz[0]);
                            f.strings[j] = lulz[1];
                        }
                        break;
                    case "signedvalue":
                        if (values.Trim() == "")
                            f.data = 0;
                        else
                            f.data = Int32.Parse(values);
                        break;
                    case "value":
                        if (values.Trim() == "")
                            f.data = 0;
                        else
                            f.data = Int32.Parse(values);
                        break;
                    case "checkbox":
                        if (values.Trim() == "")
                            f.data = 1;
                        else
                            f.data = Int32.Parse(values);
                        break;
                }
                sd.fields.Add(f);
            }

            return sd;
        }

        public class SpriteDataField
        {
            public string name;
            public string notes;
            public string display;
            public int startNibble;
            public int endNibble;

            //For list
            public int[] values;
            public string[] strings;

            //For values and checkboxes
            public int data;

            public int getBitCount()
            {
                return (endNibble - startNibble + 1)*4;
            }

            public int getMin()
            {
                int value = 0;
                if (display == "signedvalue")
                    value = -((1 << (getBitCount()) - 1));
                if (display == "value" || display == "signedvalue")
                    value += this.data;
                return value;
            }

            public int getMax()
            {
                int value = (1 << (getBitCount())) - 1;
                if (display == "signedvalue")
                    value = (1 << (getBitCount() - 1)) - 1;
                if (display == "value" || display == "signedvalue")
                    value += this.data;
                return value;
            }

            public int getValue(byte[] data)
            {
                byte[] nibbles = new byte[12];
                for (int i = 0; i < 6; i++)
                {
                    nibbles[2 * i] = (byte)(data[i] >> 4);
                    nibbles[2 * i + 1] = (byte)(data[i] & 0xF);
                }

                int res = 0;
                for (int i = startNibble; i <= endNibble; i++)
                {
                    res = res << 4 | nibbles[i];
                }

                if (display == "signedvalue" && res > getMax())
                {
                    res -= (1 << getBitCount());
                }

                if (display == "value" || display == "signedvalue")
                    res += this.data;

                if (display == "checkbox")
                    res /= this.data;

                return res;
            }

            public void setValue(int b, byte[] data)
            {
                byte[] nibbles = new byte[12];
                for (int i = 0; i < 6; i++)
                {
                    nibbles[2 * i] = (byte)(data[i] >> 4);
                    nibbles[2 * i + 1] = (byte)(data[i] & 0xF);
                }

                if (display == "value" || display == "signedvalue")
                    b -= this.data;

                if (display == "checkbox")
                    b *= this.data;

                for (int i = endNibble; i >= startNibble; i--)
                {
                    nibbles[i] = (byte)(b & 0xF);
                    b = b >> 4;
                }

                for (int i = 0; i < 6; i++)
                {
                    data[i] = (byte)(nibbles[2*i] << 4 | nibbles[2*i+1]);
                }
            }
        }

        public class SpriteDataEditor : TableLayoutPanel
        {
            Dictionary<SpriteDataField, Control> controls = new Dictionary<SpriteDataField, Control>();

            List<LevelItem> sprites;
            NSMBSprite s;
            SpriteData sd;
            LevelEditorControl EdControl;
            public bool updating = false;

            public SpriteDataEditor(List<LevelItem> sprites, SpriteData sd, LevelEditorControl EdControl)
            {
                this.SizeChanged += new EventHandler(this_SizeChanged);
                updating = true;
                this.ColumnCount = 3;
                //Talbe layout panel doesn't automatically create row or column styles
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
                this.RowCount = sd.fields.Count;
                for (int l = 0; l < this.RowCount; l++)
                    this.RowStyles.Add(new RowStyle(SizeType.Absolute));
                this.AutoSize = true;
                this.AutoSizeMode = AutoSizeMode.GrowAndShrink;

                this.sprites = sprites;
                foreach (LevelItem obj in sprites)
                    if (obj is NSMBSprite) {
                        s = obj as NSMBSprite;
                        break;
                    }
                this.sd = sd;
                this.Dock = DockStyle.Fill;
                this.EdControl = EdControl;

                int row = 0;
                foreach (SpriteDataField v in sd.fields)
                {
                    Control c = CreateControlFor(v);
                    c.Anchor = AnchorStyles.Left | AnchorStyles.Right;
                    if (c is CheckBox || c is Label)
                    {
                        c.Font = new System.Drawing.Font(c.Font.FontFamily, c.Font.Size * 0.9F);
                        this.Controls.Add(c, 0, row);
                        this.RowStyles[row].Height = 25;
                        if (v.notes == "")
                            this.SetColumnSpan(c, 3);
                        else
                        {
                            NotesCtrl note = new NotesCtrl();
                            this.Controls.Add(note, 2, row);
                            note.Text = v.notes;
                        }
                    }
                    else {
                        this.Controls.Add(c, 1, row);
                        Label l = new Label();
                        l.Text = v.name;
                        l.Font = new System.Drawing.Font(l.Font.FontFamily, l.Font.Size * 0.9F);
                        l.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
                        l.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                        this.Controls.Add(l, 0, row);
                        this.RowStyles[row].Height = 25;
                        if (v.notes == "")
                            this.SetColumnSpan(c, 2);
                        else
                        {
                            NotesCtrl note = new NotesCtrl();
                            this.Controls.Add(note, 2, row);
                            note.Text = v.notes;
                        }
                        
                    }
                    row++;
                    controls.Add(v, c);
                }
                updating = false;
            }

            public void this_SizeChanged(object sender, EventArgs e)
            {
//                Console.Out.WriteLine(this.Width.ToString());
                if (this.Width != 200)
                    for (int l = 0; l < this.RowCount; l++)
                    {
                        Control ctrl = this.GetControlFromPosition(0, l);
                        if (ctrl is Label)
                        {
                            ctrl.MaximumSize = new System.Drawing.Size(this.Width / 2, 0);
                            this.RowStyles[l].Height = Math.Max(ctrl.PreferredSize.Height, this.GetControlFromPosition(1, l).Height) + 4;
                        }
                    }
            }

            public void UpdateData()
            {
                updating = true;
                foreach (SpriteDataField v in sd.fields)
                    updateValue(v);
                updating = false;
            }

            private Control CreateControlFor(SpriteDataField v)
            {
//                Console.WriteLine(v.display + " " + v.name);
                if (v.display == "checkbox")
                {
                    CheckBox c = new CheckBox();
                    c.Checked = v.getValue(s.Data) == 1;
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
                        c.SelectedIndex = Array.IndexOf(v.values, v.getValue(s.Data));
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
                    c.CheckBoxCount = v.getBitCount();
                    c.value = v.getValue(s.Data);
                    c.ValueChanged += new EventHandler(saveData);
                    return c;
                }
                else
                {
                    NumericUpDown c = new NumericUpDown();
                    c.Minimum = v.getMin();
                    c.Maximum = v.getMax();
                    c.Value = v.getValue(s.Data);
                    c.ValueChanged += new EventHandler(saveData);
                    return c;
                }
            }

            public void updateValue(SpriteDataField v)
            {
                Control c = controls[v];
                int value = v.getValue(s.Data);
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
                byte[] d = s.Data.Clone() as byte[];
                int index = 0;
                foreach(SpriteDataField sv in controls.Keys)
                {
                    int val = 0;
                    if (controls[sv] is NumericUpDown)
                        val = (int)(controls[sv] as NumericUpDown).Value;
                    else if (controls[sv] is ComboBox) {
                        int se = (controls[sv] as ComboBox).SelectedIndex;
                        if (se == -1)
                            val = 0;
                        else
                            val = sd.fields[index].values[(controls[sv] as ComboBox).SelectedIndex];
                    }
                    else if (controls[sv] is CheckBox)
                        val = (controls[sv] as CheckBox).Checked ? 1 : 0;
                    else if (controls[sv] is BinaryEdit)
                        val = (controls[sv] as BinaryEdit).value;
                    sv.setValue(val, d);
                    index++;
                }

                if (!updating && sender != null)
                {
                    EdControl.UndoManager.Do(new ChangeSpriteDataAction(sprites, d));
                }
            }
        }
    }
}
