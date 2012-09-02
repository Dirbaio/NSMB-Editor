using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class TileBehavior
    {
        public byte[] tb;
        String name;
        public bool isHeader;

        public TileBehavior(String tb, String name)
        {
            this.name = name;
            if (tb == string.Empty) {
                isHeader = true;
                return;
            }
            String[] tb_string = tb.Split(' ');
            byte[] tb_byte = new byte[4];
            for (int count = 0; count < 4; count++)
            {
                tb_byte[count] = byte.Parse(tb_string[count], System.Globalization.NumberStyles.HexNumber);
            }
            this.tb = tb_byte;
        }

        public override String ToString()
        {
            return name;
        }

        public static List<TileBehavior> readFromFile(string fileName)
        {
            List<TileBehavior> behaviors = new List<TileBehavior>();
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(fileName);
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (line != string.Empty) {
                        if (line.StartsWith("+"))
                            behaviors.Add(new TileBehavior("", line.Substring(1)));
                        else {
                            int equalpos = line.IndexOf('=');
                            behaviors.Add(new TileBehavior(line.Substring(0, equalpos - 1).Trim(), line.Substring(equalpos + 1).Trim()));
                        }
                    }
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error reading tile behavior file. The error is: \n" + ex.Message);
            }
            return behaviors;
        }
    }
}
