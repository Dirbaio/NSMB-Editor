using System;
using System.Collections.Generic;
using System.Text;

namespace NSMBe4
{
    public class Version
    {
        private static string rev = "$Rev: 147 $";

        public static string getRevision()
        {
            return rev;
        }
    }
}
