using System;
using System.Linq;
using System.Collections.Generic;
using PASM;
namespace PASM.Handlers
{
    /// <summary>
    /// Calls a library
    /// The library must be imported with the "im" command before being used with calib
    /// </summary>
    public class calib : Handler
    {
        string Class, MethodName;
        string[] args;
        public calib(string[] args, Engine inst) : base(inst)
        {
            this.args = args;
            Class = args[1];
            MethodName = args[2];
        }

        public override void Execute()
        {
            byte[][] Params;
            List<string> b = args.ToList();
            b.RemoveRange(0, 3);
            string[] s = b.ToArray();
            Params = new byte[s.Length][];
            for (int i = 0; i < s.Length; i++)
            {
                Params[i] = inst.ResolveData(s[i]);
            }
            inst.CallStaticMethod(Class, MethodName, Params);
        }
    }
}