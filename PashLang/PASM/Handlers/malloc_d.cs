using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Allocates a given amount of data to a register
    /// </summary>
    public class malloc_d : Handler
    {
        string ts_ptr;
        int AllocationSize;
        public malloc_d(string[] args, Engine inst) : base(inst)
        {
            ts_ptr = args[1];
            AllocationSize = int.Parse(args[2]);
        }

        public override void Execute()
        {
            int ptr;
            bool isMethodPtr = isMethodPointer(ts_ptr, out ptr);
			inst.malloc(inst.GetRaster(isMethodPtr), ptr, AllocationSize);
        }
    }
}