using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PASM.Extended;

namespace PASM.Handlers
{
    public class set_FLOAT : Handler
    {
        float set;
        string ptr;
        public set_FLOAT(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = (float)Convert.ToDouble(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}
