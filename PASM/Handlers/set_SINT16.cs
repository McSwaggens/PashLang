using static PASM.Extended;

namespace PASM.Handlers
{
    public class set_SINT16 : Handler
    {
        short set;
        string ptr;
        public set_SINT16(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Converter.ParseStringToShort_NEG_CHECK(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.Set(reg, isMethod, set);
        }
    }
}
