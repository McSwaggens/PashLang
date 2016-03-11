using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    public class st_MATH : Handler
    {
        private string Equasion;
        string ptr;
        char[] ops;
        string[] parts;
        public st_MATH(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            for (int i = 3; i < args.Length; i++) Equasion += args[i];
            parts = mSep(Equasion, out ops);
        }

        public override void Execute()
        {
            string equ = Equasion;
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = "" + inst.ResolveNumber(parts[i]);
            }

            equ = "";
            for (int i = 0; i < parts.Length; i++) { equ += parts[i]; if (i < ops.Length) equ += ops[i]; }

            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, (int)inst.mathParser.Parse(equ));
        }

        public bool doesContainMathOperator(string equ)
        {
            foreach (char c in MathCharacters) foreach (char e in equ) if (e == c) return true;
            return false;
        }

        public string[] mSep(string sep, out char[] operators)
        {
            List<string> ret = new List<string>();
            List<char> ops = new List<char>();
            char[] s = sep.ToCharArray();
            string cbuild = "";
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                int r;
                if (int.TryParse("" + c, out r) || c == ':') cbuild += c;
                foreach (char o in MathCharacters.Where(o => o == c))
                {
                    ret.Add(cbuild); ops.Add(o); cbuild = "";
                }
            }
            ret.Add(cbuild);
            operators = ops.ToArray();
            return ret.ToArray();
        }

        public char[] MathCharacters = { '+', '-', '*', '/', '%' };
    }
}