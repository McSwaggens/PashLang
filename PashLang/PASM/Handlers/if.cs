using System;
using System.Linq;
using System.Collections.Generic;
namespace PASM.Handlers
{
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

            //TODO: Need to move away from dynamic, slow af
            //Compare the 2 arguements and jump to the next line if true...
            dynamic a1 = inst.ResolveNumber(arg1); //
                                                   //  Dynamic a1 and a2 will be resolved at runtime to be either 16, 32 or 64 bit integers.
            dynamic a2 = inst.ResolveNumber(arg2); //
            bool ReturnedValue = false;
            //TODO: Make this faster
            if (Operator == "=" && a1 == a2) ReturnedValue = true;
            else
            if (Operator == "!=" && a1 != a2) ReturnedValue = true;
            else
            if (Operator == ">" && a1 > a2) ReturnedValue = true;
            else
            if (Operator == ">=" && a1 >= a2) ReturnedValue = true;
            else
            if (Operator == "<" && a1 < a2) ReturnedValue = true;
            else
            if (Operator == "<=" && a1 <= a2) ReturnedValue = true;
            else throw new PException($"Unknown comparison operator: {Operator}");
            if (!ReturnedValue) inst.CurrentLine = jumpln;
        }
    }
}