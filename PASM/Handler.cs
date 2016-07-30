using System;
using PASM.Handlers;

namespace PASM
{
    public class Handler
    {
        public static Type[] handlers = new Type[] { typeof(mov), typeof(free), typeof(calib), typeof(malloc_c), typeof(malloc_d), typeof(malloc_p), typeof(re), typeof(call), typeof(@if), typeof(im) };

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