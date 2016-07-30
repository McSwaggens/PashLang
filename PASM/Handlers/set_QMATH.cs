using System;
using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Sets the data at the current address to the result of a mathematical calculation
    /// This command requires the byte context to be disclosed
    /// </summary>
    public class set_QMATH : Handler
    {
        private string ptr;
        private string Equasion;
        public set_QMATH(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            for (int i = 3; i < args.Length; i++) Equasion += args[i];
        }

        public override void Execute()
        {
            object arg1;
            object arg2;
            char Operator;
            SeperateEquasion(Equasion, out arg1, out arg2, out Operator);

            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);

            long result = 0;
            long a1 = Convert.ToInt64(arg1);
            long a2 = Convert.ToInt64(arg2);

            
            if (Operator == '*') result = (a1 * a2);
            else
            if (Operator == '/') result = (a1 / a2);
            else
            if (Operator == '+') result = (a1 + a2);
            else
            if (Operator == '-') result = (a1 - a2);
            else
            if (Operator == '%') result = (a1 % a2);
            else throw new PException("Unknown QMATH operator: " + Operator);

            if (arg1 is long || arg2 is long) inst.Set(reg, isMethod, result);
            else if (arg1 is int || arg2 is long) inst.Set(reg, isMethod, (int)result);
            else if (arg1 is short || arg2 is short) inst.Set(reg, isMethod, (short)result);
        }

        public void SeperateEquasion(string equasion, out object arg1, out object arg2, out char Operator)
        {
            arg1 = null;
            Operator = '*';
            char[] carr = equasion.ToCharArray();
            string current = "";
            foreach (char c in carr)
            {
                if (isMathCharacter(c))
                {
                    arg1 = inst.ResolveNumber(current);
                    Operator = c;
                    current = "";
                }
                else current += c;
            }
            arg2 = inst.ResolveNumber(current);
        }

        //foreach is faster than LINQ sadly :(
        public bool isMathCharacter(char op)
        {
            foreach (char c in MathCharacters) if (op == c) return true;
            return false;
        }
        public static char[] MathCharacters = { '*', '+', '/', '-', '%' };
    }
}