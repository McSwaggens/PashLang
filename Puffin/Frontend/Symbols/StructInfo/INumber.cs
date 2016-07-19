using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.ClassInfo;

namespace Puffin.Frontend.Symbols.StructInfo
{
    public interface INumber
    {
        object Value { get; set; }
        /// <summary>
        /// Returns whether this object is convertible
        /// </summary>
        /// <param name="o"> the object</param>
        /// <returns></returns>
        bool Convertable(object o);

        /// <summary>
        /// Converts this number to a byte
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a byte</returns>
        PuffinByte ToByte(INumber inNumber);
        /// <summary>
        /// Converts this number to a int16
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a int16</returns>
        PuffinInt16 ToInt16(INumber inNumber);
        /// <summary>
        /// Converts this number to a int32
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a int32</returns>
        PuffinInt32 ToInt32(INumber inNumber);
        /// <summary>
        /// Converts this number to a int64
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a int64</returns>
        PuffinInt64 ToInt64(INumber inNumber);
        /// <summary>
        /// Converts this number to a Uint16
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a Uint16</returns>
        PuffinUInt16 ToUint16(INumber inNumber);
        /// <summary>
        /// Converts this number to a Uint32
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a Uint32</returns>
        PuffinUInt32 ToUint32(INumber inNumber);
        /// <summary>
        /// Converts this number to a Uint64
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a Uint64</returns>
        PuffinUInt64 ToUint64(INumber inNumber);
        /// <summary>
        /// Converts this number to a String
        /// </summary>
        /// <param name="inNumber"> the number </param>
        /// <returns>this number represented as a String</returns>
        PuffinString ToString(INumber inNumber);
        /// <summary>
        /// Converts this number to the specified type
        /// </summary>
        /// <typeparam name="T"> the type to return</typeparam>
        /// <param name="inNumber">the number</param>
        /// <param name="typeInformation">the type info</param>
        /// <returns></returns>
        T ToType<T>(INumber inNumber, Information typeInformation);
    }
}
