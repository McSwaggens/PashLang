using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.StructInfo
{
    public abstract class Struct : StructInformation
    {
        /// <summary>
        /// Constructor for struct information
        /// </summary>
        /// <param name="name">the name of the struct </param>
        /// <param name="defaultValue">the default value of this structs object</param>
        /// <param name="isPrimitive"> whether this struct is a primitive type</param>
        /// <param name="isNullable"> whether this struct is nullable</param>
        protected Struct(string name, object defaultValue, bool isPrimitive, bool isNullable = false) : base(name, defaultValue, isPrimitive, isNullable)
        {
        }
    }
}
