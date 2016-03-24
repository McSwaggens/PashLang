using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PASM.Extended;
namespace PASM.Handlers
{
    /// <summary>
    /// Sets a given pointer to an integer pointer
    /// </summary>
    public class set_IP : Handler
    {
        public int set_register;
        public bool set_isMethodPtr;

        public int get_register;
        public bool get_isMethodPtr;

        public set_IP(string[] args, Engine engine) : base (engine)
        {
            set_isMethodPtr = isMethodPointer(args[1], out set_register);
            get_isMethodPtr = isMethodPointer(args[3], out get_register); 
        }

        public override void Execute()
        {
            Raster set_raster = inst.GetRaster(set_isMethodPtr);

            byte[] get_data = inst.ResolveData(get_register, get_isMethodPtr);
            int size = get_data.Length;

            switch(size)
            {
                case 2:
                    set_raster[set_register].address = (uint)Convert.ToInt16(get_data);
                    break;
                case 4:
                    set_raster[set_register].address = (uint)Convert.ToInt32(get_data);
                    break;
                case 8:
                    set_raster[set_register].address = (uint)Convert.ToInt64(get_data);
                    break;
            }

        }
    }
}
