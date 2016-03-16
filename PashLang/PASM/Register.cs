using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASM
{
    public class Register
    {
        private Pointer[] pointers;
        public Pointer[] Stack => pointers;

        public Register(int size)
        {
            pointers = new Pointer[size];
        }
        public Pointer this[int index] {
            get { return pointers[index]; }
            set { pointers[index] = value; }
        }

        public class Pointer
        {
            public int ReferenceCount = 1;
            public uint address;
            public uint size;
            public Pointer(uint address, uint size)
            {
                this.address = address;
                this.size = size;
            }
            public Pointer()
            {

            }
        }
    }
}