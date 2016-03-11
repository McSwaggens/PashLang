using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    public class st_INT64 : Handler
    {
        long set;
        string ptr;
        public st_INT64(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Converter.ParseStringToLong(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}