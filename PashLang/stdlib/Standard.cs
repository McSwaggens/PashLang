using PASM;
using System;

namespace stdlib
{
	public class Standard
	{
        public static byte[] test(Engine engine)
        {
            return Converter.int32(123);
        }

		public static void cout_num(Engine engine, byte[] m)
		{
            object num = null;
            if (m.Length == 2) num = BitConverter.ToInt16(m, 0);
            else if (m.Length == 4) num = BitConverter.ToInt32(m, 0);
            else if (m.Length == 8) num = BitConverter.ToInt64(m, 0);
            else if (m.Length == 1) num = m[0];
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
                    Console.WriteLine((used ? "USED" : "FREE") + " ADR: " + part.Address + " SIZE: " + part.Size);
                }
            }
        }
        
        public static void PRINT_MEMORY_DUMP_DATA(Engine engine)
        {
            Memory memory = engine.memory;
            foreach (PASM.Memory.Part part in memory.PartAddressStack)
            {
                if (part != null)
                {
                    bool used = part.Used;
                    Console.Write((used ? "USED" : "FREE") + " ADR: " + part.Address + " SIZE: " + part.Size + " DATA: ");
                    int i = 0;
                    foreach (byte dat in memory.read(part.Address, part.Size))
                    {
                        Console.Write($"[{i}] {dat},");
                        i++;
                    }
                    Console.WriteLine();
                }
            }
        }

		public static void PRINT_MEMORY_DUMP_COMPLEX(Engine engine)
		{
			Memory memory = engine.memory;
			foreach (PASM.Memory.Part part in memory.PartAddressStack)
			{
				if (part != null)
				{
					bool used = part.Used;
					Console.WriteLine((used ? "USED" : "FREE") + " ADR: NUM (" + part.Address + ") HEX (0x" + part.Address.ToString("X") + ") SIZE: NUM (" + part.Size + ") HEX (0x" + part.Size.ToString("X") + ")");
				}
			}
		}

        public static void WRITE_MEMORY_SIZE(Engine engine, byte[] m)
        {
            Console.WriteLine(m.Length);
        }
	}
}

