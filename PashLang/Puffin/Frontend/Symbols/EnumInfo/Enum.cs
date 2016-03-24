using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.EnumInfo
{
    public abstract class Enum : EnumInformation
    {
        /// <summary>
        /// Constructor for enum information
        /// </summary>
        /// <param name="name">the name of the enum </param>
        /// <param name="defaultValue">the default value of this enum object</param>
        /// <param name="isPrimitive"> whether this enum is a primitive type</param>
        /// <param name="isNullable"> whether this enum is nullable</param>
        public Enum(string name, object defaultValue, bool isPrimitive, bool isNullable = false) : base(name, defaultValue, isPrimitive, isNullable)
        {
        }
    }
}
