using System.Linq;
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
			foreach (Register p in inst.returns.Last().register.stack.Where(p => p != null))
				inst.TryFreeRegister(p);
            if (args.Length > 0)
            {
                FunctionInstance func = inst.returns.Last();
                if (func.doesReturnValue)
                {
                    if (func.methodVariable)
                        inst.returns[inst.returns.Count - 2].register[func.returnVariablePos] = inst.ResolvePointer(args[1]);
					else inst.raster[func.returnVariablePos] = inst.ResolvePointer(args[1]);
                }
            }
            inst.currentLine = inst.returns.Last().returnLine;
            inst.returns.Remove(inst.returns.Last());
        }
    }
}