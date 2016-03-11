using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    //malloc copy
    public class malloc_c : Handler
    {
        string workingPointer;
        string setterPointer;
        public malloc_c(string[] args, Engine inst) : base(inst)
        {
            workingPointer = args[1];
            setterPointer = args[2];
        }

        public override void Execute()
        {
            int workPtr;
            bool isMethodWorkingPtr = isMethodPointer(workingPointer, out workPtr);

            int setterPtr;
            bool isMethodSetterPtr = isMethodPointer(setterPointer, out setterPtr);

            inst.malloc(inst.GetRegister(isMethodWorkingPtr), workPtr, inst.ResolveData(setterPointer).Length);
        }
    }
}