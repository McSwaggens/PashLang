using PASM;
using System;

namespace PashRuntime
{
	public class Standard
	{
		public static void cout_num(Engine engine, byte[] m)
		{
            object num = null;
            if (m.Length == 2) num = BitConverter.ToInt16(m, 0);
            else if (m.Length == 4) num = BitConverter.ToInt32(m, 0);
            else if (m.Length == 8) num = BitConverter.ToInt64(m, 0);
            Console.WriteLine(num);
		}

        public static void PRINT_MEMORY_DUMP(Engine engine)
        {
            Memory memory = engine.memory;
            foreach (PASM.Memory.Part part in memory.PartAddressStack)
            {
                if (part != null)
                {
                    bool used = part.Used;
                    Console.WriteLine((used ? "USED" : "FREE") + " A: 0x" + part.Address.ToString("X") + " S :0x" + part.Size.ToString("X"));
                }
            }
        }

        public static void WRITE_MEMORY_SIZE(Engine engine, byte[] m)
        {
            Console.WriteLine(m.Length);
        }
	}
}

