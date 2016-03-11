using System;
using System.Linq;
using System.Collections.Generic;
using PASM;
using static PASM.Extended;
namespace PASM.Handlers
{
    public class free : Handler
    {
        public string tf;

        public free(string[] args, Engine inst) : base(inst)
        {
            tf = args[1];
        }

        public override void Execute()
        {
            int ptr;
            bool isMethodPtr = isMethodPointer(tf, out ptr);
            PASM.Register.Pointer pointer = isMethodPtr ? inst.Returns.Last().register.Stack[ptr] : inst.register.Stack[ptr];
            inst.ForceFreeRegister(pointer);
            pointer = null;
        }
    }
}