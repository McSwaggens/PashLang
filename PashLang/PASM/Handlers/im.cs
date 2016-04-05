using System;
namespace PASM.Handlers
{
    /// <summary>
    /// Enables the use of referenced libraries in code
    /// </summary>
    public class im : Handler
    {
        public string lib;
        public im(string[] args, Engine inst) : base(inst)
        {
            lib = args[1];
        }

        public override void Execute()
        {
            foreach (Type t in inst.ReferencedLibraries)
                if (t.Name == lib)
                {
                    inst.ImportLibrary(t);
                    return;
                }
        }
    }
}