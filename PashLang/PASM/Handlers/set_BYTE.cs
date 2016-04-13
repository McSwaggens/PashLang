using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Sets the given register to a 1 byte unsigned integer
    /// </summary>
    public class set_BYTE : Handler
    {
        byte set;
        string ptr;
        public set_BYTE(string[] args, Engine inst) : base(inst)
        {
            ptr = args[1];
            set = byte.Parse(args[3]);
        }

        public override void Execute()
        {
            int reg;
            bool isMethod = isMethodPointer(ptr, out reg);
            inst.set(reg, isMethod, set);
        }
    }
}