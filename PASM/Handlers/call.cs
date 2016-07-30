using System.Linq;
using System.Collections.Generic;
namespace PASM.Handlers
{
    /// <summary>
    /// Moves the current line to the given point,
    /// Creates a new function instance for returning and method variables/registers
    /// When the currentline hits an "re" command, it moves the current line back to where it began (below the call command)
    /// </summary>
    public class call : Handler
    {
        string[] args;
        public call(string[] args, Engine inst) : base(inst)
        {
            this.args = args;
        }

        public override void Execute()
        {
			FunctionInstance func = new FunctionInstance(inst.rasterSize);
            func.doesReturnValue = false;
            func.returnLine = inst.currentLine;
            inst.currentLine = inst.points[Converter.ParseStringToInt(args[1])];

            if (args.Length > 1)
            {
                List<string> v = args.ToList();
                v.RemoveRange(0, 2);
                for (int g = 0; g < v.Count; g++)
                {
                    func.register[g] = inst.ResolvePointer(v[g]);
                }
            }
            inst.returns.Add(func);
        }
    }
}