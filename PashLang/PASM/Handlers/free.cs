using System.Linq;
using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Free's the memory at a given pointer
    /// </summary>
    public class free : Handler
    {
        public string tf;
        public free(string[] args, Engine inst) : base(inst)
        {
            tf = args[1];
        }

        public override void Execute()
        {
            int ptr;
            bool isMethodPtr = isMethodPointer(tf, out ptr);
			Raster.Register pointer = isMethodPtr ? inst.Returns.Last().register.Stack[ptr] : inst.raster.Stack[ptr];
			inst.ForceFreeRaster(pointer);
            pointer = null;
        }
    }
}