using PASM;

namespace PASM
{
    public class Raster
    {
        private Register[] registers;
        public Register[] stack => registers;

        public Raster(int size)
        {
            registers = new Register[size];
        }
        public Register this[int index] {
            get { return registers[index]; }
            set { registers[index] = value; }
        }
    }
}