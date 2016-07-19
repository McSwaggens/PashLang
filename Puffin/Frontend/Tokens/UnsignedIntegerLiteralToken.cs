using System;
using static Puffin.Logger;

namespace Puffin.Frontend.Tokens
{
    public class UnsignedIntegerLiteralToken : Token
    {
        public UnsignedIntegerLiteralToken(string value)
        {
            this.value = value;
            if (this.value.EndsWith("UI") || this.value.EndsWith("ui"))
                this.value = this.value.Substring(0, this.value.Length - 2);
            this.type = ResolveType();
        }

        /// <summary>
        /// Property for the tokens type
        /// </summary>
        public override Enum Type
        {
            get { return type; }
        }

        /// <summary>
        /// Property for the tokens string value
        /// </summary>
        public override string Value
        {
            get { return value; }
        }

        /// <summary>
        /// This function resolves the type of the token
        /// </summary>
        /// <returns> the type of this token</returns>
        public override Enum ResolveType()
        {
            return EnumLiterals.UINT;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.value + " : " + this.type.ToString();
        }

        /// <summary>
        /// Custom additon operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values added together</returns>
        public static UnsignedIntegerLiteralToken operator +(UnsignedIntegerLiteralToken lhs, UnsignedIntegerLiteralToken rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) + uint.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom subtraction operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values subtracted together</returns>
        public static UnsignedIntegerLiteralToken operator -(UnsignedIntegerLiteralToken lhs, UnsignedIntegerLiteralToken rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) - uint.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom multiplication operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values multiplied together</returns>
        public static UnsignedIntegerLiteralToken operator *(UnsignedIntegerLiteralToken lhs, UnsignedIntegerLiteralToken rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) * uint.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom division operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values divided together</returns>
        public static UnsignedIntegerLiteralToken operator /(UnsignedIntegerLiteralToken lhs, UnsignedIntegerLiteralToken rhs)
        {
            try
            {
                UnsignedIntegerLiteralToken tok = new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) / uint.Parse(rhs.Value)).ToString());
                return tok;
            }
            catch (DivideByZeroException ex)
            {
                WriteError("You know you should never divide by zero!!!");
                return null;
            }
        }

        /// <summary>
        /// Custom left shift operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values left shifted together</returns>
        public static UnsignedIntegerLiteralToken operator <<(UnsignedIntegerLiteralToken lhs, int rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) << rhs).ToString());
        }

        /// <summary>
        /// Custom right shift operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values right shifted together</returns>
        public static UnsignedIntegerLiteralToken operator >>(UnsignedIntegerLiteralToken lhs, int rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) >> rhs).ToString());
        }

        /// <summary>
        /// Custom bitwise and operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values anded together</returns>
        public static UnsignedIntegerLiteralToken operator &(UnsignedIntegerLiteralToken lhs, UnsignedIntegerLiteralToken rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) & uint.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom bitwise or operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values ored together</returns>
        public static UnsignedIntegerLiteralToken operator |(UnsignedIntegerLiteralToken lhs, UnsignedIntegerLiteralToken rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) | uint.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom bitwise xor operator for uint literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values xored together</returns>
        public static UnsignedIntegerLiteralToken operator ^(UnsignedIntegerLiteralToken lhs, UnsignedIntegerLiteralToken rhs)
        {
            return new UnsignedIntegerLiteralToken((uint.Parse(lhs.Value) ^ uint.Parse(rhs.Value)).ToString());
        }
        /// <summary>
        /// Custom bitwise not for uint literal
        /// </summary>
        /// <param name="lhs">the uint literal</param>
        /// <returns>a new IntegerLiteral token with the opposite value of the input</returns>
        public static UnsignedIntegerLiteralToken operator ~(UnsignedIntegerLiteralToken lhs)
        {
            return new UnsignedIntegerLiteralToken((~uint.Parse(lhs.Value)).ToString());
        }
    }
}
