using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Sets the given register to a 8 byte unsigned integer
    /// </summary>
    public class set_INT64 : Handler
    {
        ulong set;
        string ptr;
        public set_INT64(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Converter.ParseStringToULong(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.Set(reg, isMethod, set);
        }
    }
}