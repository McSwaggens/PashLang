using PASM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashRuntime
{
    public class Threading
    {
        public static void SLEEP(Engine engine, byte[] m)
        {
            int sleepTime = BitConverter.ToInt32(m,0);
            System.Threading.Thread.Sleep(sleepTime);
        }
    }
}
