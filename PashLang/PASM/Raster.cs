namespace PASM
{
    public class Raster
    {
        private Register[] registers;
        public Register[] Stack => registers;

        public Raster(int size)
        {
            registers = new Register[size];
        }
        public Register this[int index] {
            get { return registers[index]; }
            set { registers[index] = value; }
        }

        public class Register
        {
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
}