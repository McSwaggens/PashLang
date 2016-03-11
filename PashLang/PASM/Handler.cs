using System;
using System.Linq;
using System.Collections.Generic;
using PASM.Handlers;

namespace PASM
{
    public class Handler
    {

        public static List<Type> Handlers = new List<Type>() { typeof(mov), typeof(free), typeof(calib), typeof(malloc_c), typeof(malloc_d), typeof(malloc_p), typeof(re), typeof(call), typeof(@if), typeof(im) };

        public Engine inst;
        public Handler(Engine inst)
        {
            this.inst = inst;
        }

        public virtual void Execute()
        {
        }
    }
}