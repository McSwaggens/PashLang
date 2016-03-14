using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Value Of Pointer
    /// Sets the data at the given register to another registers data
    /// Copies the data of another registers data
    /// </summary>
    public class st_VOP : Handler
    {
        private string worker, setter;
        public st_VOP(string[] args, Engine engine) : base(engine)
        {
            worker = args[1];
            setter = args[3];
        }

        public override void Execute()
        {
            int setterPtr;
            bool isMethodPtr_Setter = isMethodPointer(worker, out setterPtr);

            inst.set(setterPtr, isMethodPtr_Setter, inst.ResolveData(setter));
        }
    }
}