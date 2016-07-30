using System.Linq;
using System.Collections.Generic;
namespace PASM.Handlers
{
    /// <summary>
    /// Value Of Return
    /// Sets the data at the given register to the return of a function call
    /// Captures the return of a call command
    /// </summary>
    public class set_VOR : Handler
    {
        string ptr;
        string[] args;
        public set_VOR(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            this.args = args;
        }

        public override void Execute()
        {
			FunctionInstance func = new FunctionInstance(inst.rasterSize);
            func.returnLine = inst.currentLine;
            if (args.Length > 3)
            {
                List<string> v = args.ToList();
                v.RemoveRange(0, 4);
                for (int g = 0; g < v.Count; g++)
                {
                    func.register[g] = inst.ResolvePointer(v[g]);
                }
            }

            int p;
            if (args[1].ToCharArray()[0] == ':')
            {
                p = Converter.ParseStringToInt(args[1].Substring(1));
                func.methodVariable = true;
            }
            else p = Converter.ParseStringToInt(args[1]);


            func.returnVariablePos = p;

            inst.returns.Add(func);
            inst.currentLine = inst.points[Converter.ParseStringToInt(args[3])];
        }
    }
}