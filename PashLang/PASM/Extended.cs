using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PASM
{
    /// <summary>
    /// Extra methods not required to be in the Engine class
    /// </summary>
    public class Extended
    {
        /// <summary>
        /// Checks if the string is a method pointer or not
        /// </summary>
        /// <param name="string"></param>
        /// <param name="returned number"></param>
        /// <returns></returns>
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
