using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;

namespace PASM.Handlers
{
    /// <summary>
    /// Sets the given register to a 2 byte unsigned integer
    /// </summary>
    public class st_INT16 : Handler
    {
        short set;
        string ptr;
        public st_INT16(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Converter.ParseStringToShort(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}