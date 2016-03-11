using System;
using System.Linq;
using System.Collections.Generic;
namespace PASM.Handlers
{
    public class mov : Handler
    {
        public int Line;
        public mov(string[] args, Engine inst) : base(inst)
        {
            Line = inst.points[Converter.ParseStringToInt(args[1])];
        }

        public override void Execute()
        {
            inst.CurrentLine = Line;
        }
    }
}