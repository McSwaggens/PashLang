using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Value Of Retern Library
    /// Gets the data returned by static library method
    /// Can accept params
    /// </summary>
    public class st_VORL : Handler
    {
        int ptr;
        bool isMethod;
        string[] args;
        public st_VORL(string[] args, Engine inst) : base(inst)
        {
            isMethod = isMethodPointer(args[1], out ptr);
            this.args = args;
        }

        public override void Execute()
        {
            byte[][] Params;
            List<string> v = args.ToList();
            v.RemoveRange(0, 5);
            Params = new byte[v.Count][];
            for (int i = 0; i < v.Count; i++)
            {
                Params[i] = inst.ResolveData(v[i]);
            }
            inst.set(ptr, isMethod, inst.CallStaticMethod(args[3], args[4].Trim(':'), Params));
        }
    }
}