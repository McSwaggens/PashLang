using PASM;
using System;
namespace stdlib
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
