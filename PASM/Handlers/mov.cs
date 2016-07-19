namespace PASM.Handlers
{
    /// <summary>
    /// Move the current line to a pointer
    /// </summary>
    public class mov : Handler
    {
        public int Line;
        public mov(string[] args, Engine inst) : base(inst)
        {
            Line = inst.points[Converter.ParseStringToInt(args[1])];
        }

        public override void Execute()
        {
            inst.CurrentLine = Line;
        }
    }
}