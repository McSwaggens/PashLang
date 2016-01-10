using System;

namespace PashRuntime
{
	public class Standard
	{
		public static void printnum_ln(byte[] m)
		{
            object num = null;
            if (m.Length == 2) num = BitConverter.ToInt16(m, 0);
            else if (m.Length == 4) num = BitConverter.ToInt32(m, 0);
            else if (m.Length == 8) num = BitConverter.ToInt64(m, 0);
            Console.WriteLine(num);
		}
	}
}

