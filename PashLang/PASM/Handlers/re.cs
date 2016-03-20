using System;
using System.Linq;
using System.Collections.Generic;
namespace PASM.Handlers
{

    /// <summary>
    /// Returns from a method
    /// Can also return data
    /// </summary>
    public class re : Handler
    {
        string[] args;
        public re(string[] args, Engine inst) : base(inst)
        {
            this.args = args;
        }

        public override void Execute()
        {
			foreach (Raster.Register p in inst.Returns.Last().register.Stack.Where(p => p != null))
				inst.TryFreeRaster(p);
            if (args.Length > 0)
            {
                FunctionInstance func = inst.Returns.Last();
                if (func.doesReturnValue)
                {
                    if (func.MethodVariable)
                        inst.Returns[inst.Returns.Count - 2].register[func.ReturnVariablePos] = inst.ResolvePointer(args[1]);
					else inst.raster[func.ReturnVariablePos] = inst.ResolvePointer(args[1]);
                }
            }
            inst.CurrentLine = inst.Returns.Last().ReturnLine;
            inst.Returns.Remove(inst.Returns.Last());
        }
    }
}