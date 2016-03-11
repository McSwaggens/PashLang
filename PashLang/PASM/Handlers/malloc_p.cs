using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    //malloc pointer
    public class malloc_p : Handler
    {
        string ts_ptr;
        int set_ptr;

        bool isMethodPtr;
        public malloc_p(string[] args, Engine inst) : base(inst)
        {
            ts_ptr = args[1];
            isMethodPtr = isMethodPointer(ts_ptr, out set_ptr);
        }

        public override void Execute()
        {
            int ptr;
            bool isMethodPtr = isMethodPointer(ts_ptr, out ptr);
            inst.malloc(inst.GetRegister(isMethodPtr), ptr, inst.ResolveINT32(isMethodPtr, ptr));
        }
    }
}