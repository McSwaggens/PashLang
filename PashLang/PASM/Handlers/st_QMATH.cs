using System;
using System.Linq;
using System.Collections.Generic;
using static PASM.Extended;
namespace PASM.Handlers
{
    public class st_QMATH : Handler
    {
        private string ptr;
        private string Equasion;
        private int sizeSpace = 4;
        public st_QMATH(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            sizeSpace = Converter.ParseStringToInt(args[3]);
            for (int i = 4; i < args.Length; i++) Equasion += args[i];
        }

        public override void Execute()
        {
            object arg1;
            object arg2;
            char Operator;
            SeperateEquasion(Equasion, out arg1, out arg2, out Operator);

            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);

            if (sizeSpace == 2)
            {
                short result = 0;
                short a1 = arg1 as short? ?? Convert.ToInt16(arg1);
                short a2 = arg2 as short? ?? Convert.ToInt16(arg2);

                if (Operator == '+') result = (short)(a1 + a2);
                else
                if (Operator == '-') result = (short)(a1 - a2);
                else
                if (Operator == '*') result = (short)(a1 * a2);
                else
                if (Operator == '/') result = (short)(a1 / a2);
                else throw new PException("Unknown QMATH operator: " + Operator);

                inst.set(reg, isMethod, result);
            }
            else if (sizeSpace == 4)
            {
                int result = 0;
                int a1 = arg1 as int? ?? Convert.ToInt32(arg1);
                int a2 = arg2 as int? ?? Convert.ToInt32(arg2);

                if (Operator == '+') result = a1 + a2;
                else
                if (Operator == '-') result = a1 - a2;
                else
                if (Operator == '*') result = a1 * a2;
                else
                if (Operator == '/') result = a1 / a2;
                else throw new PException("Unknown QMATH operator: " + Operator);

                inst.set(reg, isMethod, result);
            }
            else if (sizeSpace == 8)
            {
                long result = 0;
                //Convert arg1 or arg2 into the expected long type
                long a1 = arg1 as long? ?? Convert.ToInt64(arg1);
                long a2 = arg2 as long? ?? Convert.ToInt64(arg2);

                if (Operator == '+') result = a1 + a2;
                else
                if (Operator == '-') result = a1 - a2;
                else
                if (Operator == '*') result = a1 * a2;
                else
                if (Operator == '/') result = a1 / a2;
                else throw new PException("Unknown QMATH operator: " + Operator);

                inst.set(reg, isMethod, result);
            }
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
        public static char[] MathCharacters = { '+', '-', '*', '/', '%' };
    }
}