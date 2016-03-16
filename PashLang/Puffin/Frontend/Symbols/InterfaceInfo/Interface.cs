using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.InterfaceInfo
{
    public abstract class Interface : InterfaceInformation
    {
        /// <summary>
        /// Constructor for interface information
        /// </summary>
        /// <param name="name">the name of the interface </param>
        /// <param name="defaultValue">the default value of this interfaces object</param>
        /// <param name="isPrimitive"> whether this interface is a primitive type</param>
        /// <param name="isNullable"> whether this interface is nullable</param>
        public Interface(string name, object defaultValue, bool isPrimitive, bool isNullable = true) : base(name, defaultValue, isPrimitive, isNullable)
        {
        }
    }
}
