using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class TileBehavior
    {
        byte[] tb;
        String name;

        public TileBehavior(String tb, String name)
        {
            String[] tb_string = tb.Split(' ');
            byte[] tb_byte = new byte[4];
            for (int count = 0; count < 4; count++)
            {
                tb_byte[count] = byte.Parse(tb_string[count], System.Globalization.NumberStyles.HexNumber);
            }
            this.tb = tb_byte;
            this.name = name;
        }

        public byte[] getTb()
        {
            return tb;
        }

        public override String ToString()
        {
            return name;
        }
    }
}
