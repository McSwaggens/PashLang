using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Pointer Address Range
    /// Copies the data from another block of data, with where is starts and where it ends
    /// This operator enables OOP and arrays
    /// TODO: Explain this better
    /// </summary>
    public class set_PARC : Handler
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

        public set_PARC(string[] args, Engine inst) : base(inst)
        {
            ts_isMethodPtr = isMethodPointer(args[1], out ts_ptr);

            cpy_isMethodPtr = isMethodPointer(args[3], out cpy_ptr);

            startingRange = (uint)Converter.ParseStringToInt(args[4]);
            endingRange = (uint)Converter.ParseStringToInt(args[5]);

        }

        public override void Execute()
        {
			inst.set(ts_ptr, ts_isMethodPtr, inst.memory.read(inst.GetRaster(cpy_isMethodPtr)[cpy_ptr].address + startingRange, endingRange));
        }
    }
}
