using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.StructInfo
{
    public class Struct : StructInformation
    {
        public interface IStruct
        {
            StructInformation information { get; }

            /// <summary>
            /// Creates the information for this struct
            /// </summary>
            /// <returns> the struct information </returns>
            StructInformation CreateInformation();
        }

        /// <summary>
        /// Constructor for struct information
        /// </summary>
        /// <param name="name">the name of the struct </param>
        /// <param name="defaultValue">the default value of this structs object</param>
        /// <param name="isPrimitive"> whether this struct is a primitive type</param>
        /// <param name="isNullable"> whether this struct is nullable</param>
        public Struct(string name, object defaultValue, bool isPrimitive, bool isNullable = false) : base(name, defaultValue, isPrimitive, isNullable)
        {
        }
    }
}
