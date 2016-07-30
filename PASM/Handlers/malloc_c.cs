using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Allocates memory to a given register with the size being copied from another address
    /// </summary>
    public class malloc_c : Handler
    {
        string workingPointer;
        string setterPointer;
        public malloc_c(string[] args, Engine inst) : base(inst)
        {
            workingPointer = args[1];
            setterPointer = args[2];
        }

        public override void Execute()
        {
            int workPtr;
            bool isMethodWorkingPtr = isMethodPointer(workingPointer, out workPtr);

            int setterPtr;
            bool isMethodSetterPtr = isMethodPointer(setterPointer, out setterPtr);

			inst.Malloc(inst.GetRaster(isMethodWorkingPtr), workPtr, inst.ResolveData(setterPointer).Length);
        }
    }
}