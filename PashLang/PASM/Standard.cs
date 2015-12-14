using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASM
{
    public class Standard
    {
        public static void println(object m)
        {
            Console.WriteLine(m);
        }

        public static void print(object m)
        {
            Console.Write(m);
        }

        public static string readLine()
        {
            return Console.ReadLine();
        }

        public static string readKey()
        {
            return Console.ReadKey().KeyChar + "";
        }
    }
}
