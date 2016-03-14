﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public class UnsignedIntegerLiteralToken : Token
    {
        private Enum type;
        private string value;

        public UnsignedIntegerLiteralToken(string value)
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
            return EnumLiterals.UINT;
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
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: You know you should never divide by zero!!!");
                Console.ResetColor();
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
