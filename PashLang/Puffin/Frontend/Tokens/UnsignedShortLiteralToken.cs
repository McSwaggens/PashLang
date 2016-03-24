using System;

namespace Puffin.Frontend.Tokens
{
    public class UnsignedShortLiteralToken : Token
    {
        
        

        public UnsignedShortLiteralToken(string value)
        {
            this.value = value;
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
            return EnumLiterals.USHORT;
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
        /// Custom additon operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values added together</returns>
        public static UnsignedShortLiteralToken operator +(UnsignedShortLiteralToken lhs, UnsignedShortLiteralToken rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) + ushort.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom subtraction operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values subtracted together</returns>
        public static UnsignedShortLiteralToken operator -(UnsignedShortLiteralToken lhs, UnsignedShortLiteralToken rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) - ushort.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom multiplication operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values multiplied together</returns>
        public static UnsignedShortLiteralToken operator *(UnsignedShortLiteralToken lhs, UnsignedShortLiteralToken rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) * ushort.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom division operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values divided together</returns>
        public static UnsignedShortLiteralToken operator /(UnsignedShortLiteralToken lhs, UnsignedShortLiteralToken rhs)
        {
            try
            {
                UnsignedShortLiteralToken tok = new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) / ushort.Parse(rhs.Value)).ToString());
                return tok;
            }
            catch (DivideByZeroException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: You know you should never divide by zero!!!");
                Console.ResetColor();
                return null;
            }
        }

        /// <summary>
        /// Custom left shift operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values left shifted together</returns>
        public static UnsignedShortLiteralToken operator <<(UnsignedShortLiteralToken lhs, int rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) << rhs).ToString());
        }

        /// <summary>
        /// Custom right shift operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values right shifted together</returns>
        public static UnsignedShortLiteralToken operator >>(UnsignedShortLiteralToken lhs, int rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) >> rhs).ToString());
        }

        /// <summary>
        /// Custom bitwise and operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values anded together</returns>
        public static UnsignedShortLiteralToken operator &(UnsignedShortLiteralToken lhs, UnsignedShortLiteralToken rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) & ushort.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom bitwise or operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values ored together</returns>
        public static UnsignedShortLiteralToken operator |(UnsignedShortLiteralToken lhs, UnsignedShortLiteralToken rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) | ushort.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom bitwise xor operator for ushort literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values xored together</returns>
        public static UnsignedShortLiteralToken operator ^(UnsignedShortLiteralToken lhs, UnsignedShortLiteralToken rhs)
        {
            return new UnsignedShortLiteralToken((ushort.Parse(lhs.Value) ^ ushort.Parse(rhs.Value)).ToString());
        }
        /// <summary>
        /// Custom bitwise not for ushort literal
        /// </summary>
        /// <param name="lhs">the ushort literal</param>
        /// <returns>a new IntegerLiteral token with the opposite value of the input</returns>
        public static UnsignedShortLiteralToken operator ~(UnsignedShortLiteralToken lhs)
        {
            return new UnsignedShortLiteralToken((~ushort.Parse(lhs.Value)).ToString());
        }
    }
}

