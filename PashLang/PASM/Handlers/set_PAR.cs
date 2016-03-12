using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASM;
using static PASM.Extended;

namespace PASM.Handlers
{
    public class set_PAR : Handler
    {
        //to set value pair
        bool ts_isMethodPtr;
        int ts_ptr;

        //copy PTR
        bool cpy_isMethodPtr;
        int cpy_ptr;

        //Ranges we want to copy with
        int startingRange;
        int endingRange;

        public set_PAR(string[] args, Engine inst) : base(inst)
        {
            ts_isMethodPtr = isMethodPointer(args[1], out ts_ptr);

            cpy_isMethodPtr = isMethodPointer(args[3], out cpy_ptr);

            startingRange = Converter.ParseStringToInt(args[4]);
            endingRange = Converter.ParseStringToInt(args[5]);

        }

        public override void Execute()
        {
            Register.Pointer cpy_pointer = inst.GetRegister(cpy_isMethodPtr)[cpy_ptr];
            inst.GetRegister(ts_isMethodPtr)[ts_ptr] = new Register.Pointer(cpy_pointer.address + startingRange, endingRange);
        }
    }
}
