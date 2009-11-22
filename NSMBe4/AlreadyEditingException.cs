using System;
using System.Collections.Generic;
using System.Text;
using NSMBe4.DSFileSystem;

namespace NSMBe4
{
    public class AlreadyEditingException : Exception
    {
        public File f;
        public AlreadyEditingException(File f)
        {
            this.f = f;
        }

        public override string Message
        {
            get
            {
                return "Already editing file: " + f.name;
            }
        }
    }
}
