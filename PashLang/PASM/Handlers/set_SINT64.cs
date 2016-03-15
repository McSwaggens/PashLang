﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PASM.Extended;

namespace PASM.Handlers
{
    public class set_SINT64 : Handler
    {
        long set;
        string ptr;
        public set_SINT64(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Converter.ParseStringToLong_NEG_CHECK(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}
