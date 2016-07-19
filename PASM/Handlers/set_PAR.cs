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
        bool startingRange_isMethodPtr;
        
        int startingRange_Ptr;
        
        bool endingRange_isMethodPtr;
        
        int endingRange_Ptr;
        
        public set_PAR(string[] args, Engine inst) : base(inst)
        {
            ts_isMethodPtr = isMethodPointer(args[1], out ts_ptr);

            cpy_isMethodPtr = isMethodPointer(args[3], out cpy_ptr);

            startingRange_isMethodPtr = isMethodPointer(args[4], out startingRange_Ptr);
            endingRange_isMethodPtr = isMethodPointer(args[5], out endingRange_Ptr);

        }

        public override void Execute()
        {
            uint startingRange = (uint)inst.ResolveINT32(startingRange_isMethodPtr, startingRange_Ptr);
            
            uint endingRange = (uint)inst.ResolveINT32(endingRange_isMethodPtr, endingRange_Ptr);
            
			Register cpy_pointer = inst.GetRaster(cpy_isMethodPtr)[cpy_ptr];
			inst.GetRaster(ts_isMethodPtr)[ts_ptr] = new Register(cpy_pointer.address + startingRange, endingRange);
        }
    }
}
