using static PASM.Extended;

namespace PASM.Handlers
{
    public class set_ADR : Handler
    {
        public int set_register;
        public bool set_isMethodPtr;

        public uint address;

        public int size;

        public set_ADR(string[] args, Engine engine) : base (engine)
        {
            set_isMethodPtr = isMethodPointer(args[1], out set_register);
            address = PASM.Converter.ParseStringToUInt(args[3]);
            size = PASM.Converter.ParseStringToInt(args[4]);
        }

        public override void Execute()
        {
			Raster register = inst.GetRaster(set_isMethodPtr);
			if (register[set_register] == null) register[set_register] = new Raster.Register(address, (uint)size);
            else inst.GetRaster(set_isMethodPtr)[set_register].address = address;
        }
    }
}
