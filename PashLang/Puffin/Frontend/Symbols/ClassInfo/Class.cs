using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.ClassInfo
{
    public abstract class Class : ClassInformation
    {
        /// <summary>
        /// Constructor for class information
        /// </summary>
        /// <param name="name">the name of the class </param>
        /// <param name="defaultValue">the default value of this classes object</param>
        /// <param name="isPrimitive"> whether this class is a primitive type</param>
        /// <param name="isNullable"> whether this class is nullable</param>
        protected Class(string name, object defaultValue, bool isPrimitive, bool isNullable = true) : base(name, defaultValue, isPrimitive, isNullable)
        {

        }
    }
}
