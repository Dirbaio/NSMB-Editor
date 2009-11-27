using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NSMBe4.DSFileSystem;


namespace NSMBe4 {
    public partial class DataFinder : Form {
        public DataFinder() {
            InitializeComponent();
            List<string> LevelNames = LanguageManager.GetList("LevelNames");

            Levels = new List<string>();
            LevelFiles = new List<string>();

            string WorldID = null;
            for (int NameIdx = 0; NameIdx < LevelNames.Count; NameIdx++) {
                LevelNames[NameIdx] = LevelNames[NameIdx].Trim();
                if (LevelNames[NameIdx] == "") continue;
                if (LevelNames[NameIdx][0] == '-') {
                    string[] ParseWorld = LevelNames[NameIdx].Substring(1).Split('|');
                    WorldID = ParseWorld[1];
                } else {
                    string[] ParseLevel = LevelNames[NameIdx].Split('|');
                    if (ParseLevel[2] == "1") {
                        Levels.Add(ParseLevel[0]);
                        LevelFiles.Add(WorldID + ParseLevel[1] + "_1");
                    } else {
                        int AreaCount = int.Parse(ParseLevel[2]);
                        for (int AreaIdx = 1; AreaIdx <= AreaCount; AreaIdx++) {
                            Levels.Add(ParseLevel[0] + " area " + AreaIdx.ToString());
                            LevelFiles.Add(WorldID + ParseLevel[1] + "_" + AreaIdx.ToString());
                        }
                    }
                }
            }

            //now pad the level names to the same size to get the data aligned !!!
            int longestName = 0;
            foreach (string s in Levels)
            {
                if (s.Length > longestName)
                    longestName = s.Length;
            }
            for (int i = 0; i < Levels.Count; i++)
            {
                int pad = longestName - Levels[i].Length;
                for (int j = 0; j < pad; j++)
                    Levels[i] += " ";
            }
        }
        private List<string> Levels;
        private List<string> LevelFiles;

        private void DataFinder_Load(object sender, EventArgs e) {
            LanguageManager.ApplyToContainer(this, "DataFinder");
        }

        private void processButton_Click(object sender, EventArgs e) {
            if (!findBlockRadioButton.Checked && !findSpriteRadioButton.Checked) {
                MessageBox.Show(LanguageManager.Get("DataFinder", "ChooseMode"));
                return;
            }

            StringBuilder output = new StringBuilder();
            if (findBlockRadioButton.Checked)
                output.AppendLine(string.Format(LanguageManager.Get("DataFinder", "BlockInstances"), blockNumberUpDown.Value.ToString()));
            else
                output.AppendLine(string.Format(LanguageManager.Get("DataFinder", "SpriteInstances"), spriteUpDown.Value.ToString()));

            if (findBlockRadioButton.Checked)
                initDataComparer((int)(splitCountUpDown.Value));
            else
                initDataComparer(6);

            for (int i = 0; i < Levels.Count; i++)
            {
                File levelFile = ROM.FS.getFileByName(LevelFiles[i] + ".bin");
                File bgFile = ROM.FS.getFileByName(LevelFiles[i] + "_bgdat.bin");
                NSMBLevel l = new NSMBLevel(levelFile, bgFile, null);

                string n = Levels[i];
                if (labellingTypeCheckBox.Checked)
                    n = LevelFiles[i];
                n += ": ";

                if (findBlockRadioButton.Checked)
                {
                    output.Append(n);
                    int b = (int)(blockNumberUpDown.Value - 1);
                    int s = (int)(splitCountUpDown.Value);

                    if (s == 0)
                        PrintByteArray(output, l.Blocks[b], 0, l.Blocks[b].Length, n);
                    else
                    {
                        for (int j = 0; j < l.Blocks[b].Length; j += s)
                        {
                            if (j != 0)
                                for (int k = 0; k < n.Length; k++)
                                    output.Append(" ");
                                
                            if(j+s < l.Blocks[b].Length)
                                PrintByteArray(output, l.Blocks[b], j, j+s, n);
                            else
                                PrintByteArray(output, l.Blocks[b], j, l.Blocks[b].Length, n);
                        }
                    }
                }
                else
                {
                    bool printSpace = false;
                    foreach (NSMBSprite s in l.Sprites)
                    {
                        if (s.Type == spriteUpDown.Value)
                        {
                            if (printSpace)
                                for (int k = 0; k < n.Length; k++)
                                    output.Append(" ");
                            else
                                output.Append(n);

                            printSpace = true;
                            PrintByteArray(output, s.Data, 0, 6, n);
                        }
                    }
                }

                l.close();
            }

            if (data != null)
            {
                output.Append("\r\n");
                output.Append("\r\n");

                for (int i = 0; i < data.Length; i++)
                    output.Append(String.Format("{0:00} ", i));
                output.Append("\r\n");

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Keys.Count == 1)
                        output.Append(String.Format("{0:X2}", data[i].Keys.GetEnumerator().Current));
                    else
                        output.Append("__");
                    output.Append(" ");
                }
                output.Append("\r\n");
                output.Append("\r\n");

                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].Keys.Count != 1)
                    {
                        output.Append("========================= " + i + ":\r\n");

                        int bestCount = -1;
                        int bestVal = -1;
                        foreach (byte b in data[i].Keys)
                        {
                            if (data[i][b].Count > bestCount)
                            {
                                bestCount = data[i][b].Count;
                                bestVal = b;
                            }
                        }

                        bool hideMostUsed = false;
                        int mostUsed = bestVal;
                        int mostUsedCount = bestCount;
                        bestCount = -1;
                        bestVal = -1;
                        foreach (byte b in data[i].Keys)
                        {
                            if (data[i][b].Count > bestCount && b != mostUsed)
                            {
                                bestCount = data[i][b].Count;
                                bestVal = b;
                            }
                        }
                        if (mostUsedCount > bestCount * 2)
                            hideMostUsed = true;

                        foreach (byte b in data[i].Keys)
                        {
                            output.Append(String.Format("{0:X2}", b));
                            output.Append(": ");
                            if (hideMostUsed && b == mostUsed)
                                output.Append(" All Other Levels\r\n");
                            else
                            {
                                bool writeSpace = false;
                                foreach (string s in data[i][b])
                                {
                                    if (writeSpace)
                                        output.Append("    ");

                                    writeSpace = true;
                                    output.Append(s + "\r\n");
                                }
                            }
                        }
                    }
                }
            }

            outputTextBox.Text = output.ToString();
        }

        private void PrintByteArray(StringBuilder sb, byte[] array, int start, int end, string name)
        {
            bool space = false;
            for (int i = start; i < end; i++) {
                sb.Append(String.Format("{0:X2}", array[i]));
                if (space) sb.Append(' ');
                space = !space;
                registerByte(i - start, name, array[i]);
            }
            sb.Append("\r\n");
        }


        private Dictionary<byte, List<string>>[] data;

        private void registerByte(int pos, string name, byte val)
        {
            if (data == null) return;
            if (pos < 0) return;
            if (pos >= data.Length) return;

            if (!data[pos].ContainsKey(val))
                data[pos][val] = new List<string>();

            data[pos][val].Add(name);
        }

        private void initDataComparer(int size)
        {
            if(size == 0)
            {
                data = null;
                return;
            }

            data = new Dictionary<byte, List<string>>[size];
            for (int i = 0; i < size; i++)
                data[i] = new Dictionary<byte, List<string>>();
        }
    }
}
