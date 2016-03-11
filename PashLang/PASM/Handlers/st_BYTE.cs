using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    public class st_BYTE : Handler
    {
        byte set;
        string ptr;
        public st_BYTE(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = byte.Parse(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}