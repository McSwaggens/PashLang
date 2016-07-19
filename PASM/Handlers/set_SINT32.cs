using static PASM.Extended;

namespace PASM.Handlers
{
    public class set_SINT32 : Handler
    {
        int set;
        string ptr;
        public set_SINT32(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = Converter.ParseStringToInt_NEG_CHECK(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}
