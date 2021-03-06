﻿using System;
using static PASM.Extended;

namespace PASM.Handlers
{
    public class set_DOUBLE : Handler
    {
        double set;
        string ptr;
        public set_DOUBLE(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Convert.ToDouble(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.Set(reg, isMethod, set);
        }
    }
}
