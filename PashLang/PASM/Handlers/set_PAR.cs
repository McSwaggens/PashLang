using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PASM;
using static PASM.Extended;

namespace PASM.Handlers
{
    /// <summary>
    /// Sets the pointer and size to another pointer + size
    /// This operator enables the use of OOP and Arrays
    /// TODO: Explain this better
    /// 
    /// </summary>
    public class set_PAR : Handler
    {
        //to set value pair
        bool ts_isMethodPtr;
        int ts_ptr;

        //copy PTR
        bool cpy_isMethodPtr;
        int cpy_ptr;

        //Ranges we want to copy with
        uint startingRange;
        uint endingRange;

        public set_PAR(string[] args, Engine inst) : base(inst)
        {
            ts_isMethodPtr = isMethodPointer(args[1], out ts_ptr);

            cpy_isMethodPtr = isMethodPointer(args[3], out cpy_ptr);

            startingRange = (uint)Converter.ParseStringToInt(args[4]);
            endingRange = (uint)Converter.ParseStringToInt(args[5]);

        }

        public override void Execute()
        {
			Raster.Register cpy_pointer = inst.GetRaster(cpy_isMethodPtr)[cpy_ptr];
			inst.GetRaster(ts_isMethodPtr)[ts_ptr] = new Raster.Register(cpy_pointer.address + startingRange, endingRange);
        }
    }
}
