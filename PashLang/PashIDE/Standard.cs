using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PashIDE
{
    public class Standard
    {
        public static void cout_num(byte[] m)
        {
            object num = null;
            if (m.Length == 2) num = BitConverter.ToInt16(m, 0);
            else if (m.Length == 4) num = BitConverter.ToInt32(m, 0);
            else if (m.Length == 8) num = BitConverter.ToInt64(m, 0);
            Console.WriteLine(num);
        }
    }
}
