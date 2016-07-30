using System;

namespace PASM.Handlers
{
    /// <summary>
    /// Conditional checker
    /// TODO: Finish this comment...
    /// </summary>
    public class @if : Handler
    {
        string arg1, arg2;
        string Operator;
        int jumpln;
        public @if(string[] args, Engine inst) : base(inst)
        {
            jumpln = inst.points[Converter.ParseStringToInt(args[4])];
            Operator = args[2];
            arg1 = args[1];
            arg2 = args[3];
        }

        public override void Execute()
        {
            object oNumber1 = inst.ResolveNumber(arg1);
            object oNumber2 = inst.ResolveNumber(arg2);
            bool ReturnedValue = false;
            
            if (isIntBased (oNumber1) && isIntBased(oNumber2)){
                long a1, a2;
                
                if (oNumber1 is long || oNumber1 is ulong)
                    a1 = (long)oNumber1;
                else
                    a1 = Convert.ToInt64(oNumber1);
                    
                if (oNumber2 is long || oNumber2 is ulong)
                    a2 = (long)oNumber2;
                else
                    a2 = Convert.ToInt64(oNumber2);
                
                if (Operator == "=") ReturnedValue = a1 == a2;
                else
                if (Operator == "!=") ReturnedValue = a1 != a2;
                else
                if (Operator == ">") ReturnedValue = a1 > a2;
                else
                if (Operator == ">=") ReturnedValue = a1 >= a2;
                else
                if (Operator == "<") ReturnedValue =  a1 < a2;
                else
                if (Operator == "<=") ReturnedValue = a1 <= a2;
                else
                throw new PException($"Unknown comparison operator: {Operator}");
            }
            else if (isFloatBased(oNumber1) && isFloatBased(oNumber2)) {
                double a1, a2;
                
                if (oNumber1 is double)
                    a1 = (double)oNumber1;
                else
                    a1 = Convert.ToDouble(oNumber1);
                    
                if (oNumber2 is double)
                    a2 = (double)oNumber2;
                else
                    a2 = Convert.ToDouble(oNumber2);
                    
                    
                if (Operator == "=") ReturnedValue = a1 == a2;
                else
                if (Operator == "!=") ReturnedValue = a1 != a2;
                else
                if (Operator == ">") ReturnedValue = a1 > a2;
                else
                if (Operator == ">=") ReturnedValue = a1 >= a2;
                else
                if (Operator == "<") ReturnedValue =  a1 < a2;
                else
                if (Operator == "<=") ReturnedValue = a1 <= a2;
                else
                throw new PException($"Unknown comparison operator: {Operator}");
            }
            if (!ReturnedValue) inst.currentLine = jumpln;
        }
        
        bool isIntBased(object obj) => obj is byte || obj is sbyte || obj is short || obj is ushort || obj is int || obj is uint || obj is long || obj is ulong;
        
        bool isFloatBased(object obj) => obj is float || obj is double;
    }
}