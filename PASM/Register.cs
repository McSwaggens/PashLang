using System;

namespace PASM {
    public class Register {
        public int ReferenceCount = 1;
        public uint address;
        public uint size;
        public Register(uint address, uint size)
        {
            this.address = address;
            this.size = size;
        }
        
        public Register()
        {
            
        }
    }
}