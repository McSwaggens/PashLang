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
    public class set_VORL : Handler
    {
        int ptr;
        bool isMethod;
        string[] args;
        public set_VORL(string[] args, Engine inst) : base(inst)
        {
            isMethod = isMethodPointer(args[1], out ptr);
            this.args = args;
        }

        public override void Execute()
        {
            byte[][] Params;
            List<string> b = args.ToList();
            b.RemoveRange(0, 5);
            string[] s = b.ToArray();
            Params = new byte[s.Length][];
            for (int i = 0; i < s.Length; i++)
            {
                Params[i] = inst.ResolveData(s[i]);
            }
            byte[] returnData = inst.CallStaticMethod(args[3], args[4], Params);
            if (returnData != null)
                inst.Set(ptr, isMethod, returnData);
        }
    }
}