using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASM
{
    public class Extended
    {
        public static bool isMethodPointer(string sptr, out int ptr)
        {
            if (sptr.ToCharArray()[0] == ':')
            {
                ptr = Converter.ParseStringToInt(sptr.Remove(0, 1));
                return true;
            }
            ptr = Converter.ParseStringToInt(sptr);
            return false;
        }
    }
}
