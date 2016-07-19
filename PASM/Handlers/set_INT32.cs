using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Sets the given register to a 4 byte unsigned integer
    /// </summary>
    public class set_INT32 : Handler
    {
        uint set;
        string ptr;
        public set_INT32(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Converter.ParseStringToUInt(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}