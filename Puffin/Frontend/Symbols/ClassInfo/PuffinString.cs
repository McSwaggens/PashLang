using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.ClassInfo
{
    public class PuffinString : Class
    {
        
        /// <summary>
        /// Constructor for class information
        /// </summary>
        /// <param name="name">the name of the class </param>
        /// <param name="defaultValue">the default value of this classes object</param>
        /// <param name="isPrimitive"> whether this class is a primitive type</param>
        /// <param name="isNullable"> whether this class is nullable</param>
        public PuffinString(string name, object defaultValue, bool isPrimitive, bool isNullable = true) : base(name, defaultValue, isPrimitive, isNullable)
        {
        }


        public override ClassInformation CreateInformation()
        {
            throw new NotImplementedException();
        }
    }
}
