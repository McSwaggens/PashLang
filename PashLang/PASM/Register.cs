using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASM
{
    public class Register
    {
        private Pointer[] registers;
        public Register(int size)
        {
            registers = new Pointer[size];
        }
        public Pointer this[int index] {
            get { return registers[index]; }
            set { registers[index] = value; }
        }
        public class Pointer
        {
            public int address, size;
            public Pointer(int address, int size)
            {
                this.address = address;
                this.size = size;
            }
        }
    }
}