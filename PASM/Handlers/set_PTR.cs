using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Sets the given registers address to another registers address
    /// </summary>
    public class set_PTR : Handler
    {
        string working;
        string setter;
        public set_PTR(string[] args, Engine inst) : base(inst)
        {
            working = args[1];
            setter = args[3];
        }

        public override unsafe void Execute()
        {
            int workerPtr;
			Raster workerRegister = inst.GetRaster(isMethodPointer(working, out workerPtr));

            int setterPtr;
			Raster setterRegister = inst.GetRaster(isMethodPointer(setter, out setterPtr));

            workerRegister[workerPtr] = setterRegister[setterPtr];
            setterRegister[setterPtr].referenceCount++;
        }
    }
}