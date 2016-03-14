﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public class LongLiteralToken : Token
    {
        private Enum type;
        private string value;

        public LongLiteralToken(string value)
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
            return EnumLiterals.LONG;
        }
        /// <summary>
        /// Custom additon operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values added together</returns>
        public static LongLiteralToken operator +(LongLiteralToken lhs, LongLiteralToken rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) + long.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom subtraction operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values subtracted together</returns>
        public static LongLiteralToken operator -(LongLiteralToken lhs, LongLiteralToken rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) - long.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom multiplication operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values multiplied together</returns>
        public static LongLiteralToken operator *(LongLiteralToken lhs, LongLiteralToken rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) * long.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom division operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values divided together</returns>
        public static LongLiteralToken operator /(LongLiteralToken lhs, LongLiteralToken rhs)
        {
            try
            {
                LongLiteralToken tok = new LongLiteralToken((long.Parse(lhs.Value) / long.Parse(rhs.Value)).ToString());
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
        /// Custom left shift operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values left shifted together</returns>
        public static LongLiteralToken operator <<(LongLiteralToken lhs, int rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) << rhs).ToString());
        }

        /// <summary>
        /// Custom right shift operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values right shifted together</returns>
        public static LongLiteralToken operator >>(LongLiteralToken lhs, int rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) >> rhs).ToString());
        }

        /// <summary>
        /// Custom bitwise and operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values anded together</returns>
        public static LongLiteralToken operator &(LongLiteralToken lhs, LongLiteralToken rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) & long.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom bitwise or operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values ored together</returns>
        public static LongLiteralToken operator |(LongLiteralToken lhs, LongLiteralToken rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) | long.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom bitwise xor operator for long literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values xored together</returns>
        public static LongLiteralToken operator ^(LongLiteralToken lhs, LongLiteralToken rhs)
        {
            return new LongLiteralToken((long.Parse(lhs.Value) ^ long.Parse(rhs.Value)).ToString());
        }
        /// <summary>
        /// Custom bitwise not for long literal
        /// </summary>
        /// <param name="lhs">the long literal</param>
        /// <returns>a new IntegerLiteral token with the opposite value of the input</returns>
        public static LongLiteralToken operator ~(LongLiteralToken lhs)
        {
            return new LongLiteralToken((~long.Parse(lhs.Value)).ToString());
        }
    }
}
