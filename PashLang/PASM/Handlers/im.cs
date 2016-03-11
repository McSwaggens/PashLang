using System;
using System.Linq;
using System.Collections.Generic;
namespace PASM.Handlers
{
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