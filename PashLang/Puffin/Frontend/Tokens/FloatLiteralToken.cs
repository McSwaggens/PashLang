using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public class FloatLiteralToken : Token
    {
        private Enum type;
        private string value;

        public FloatLiteralToken(string value)
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
            return EnumLiterals.FLOAT;
        }
        /// <summary>
        /// Custom additon operator for float literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values added together</returns>
        public static FloatLiteralToken operator +(FloatLiteralToken lhs, FloatLiteralToken rhs)
        {
            return new FloatLiteralToken((float.Parse(lhs.Value) + float.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom subtraction operator for float literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values subtracted together</returns>
        public static FloatLiteralToken operator -(FloatLiteralToken lhs, FloatLiteralToken rhs)
        {
            return new FloatLiteralToken((float.Parse(lhs.Value) - float.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom multiplication operator for float literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values multiplied together</returns>
        public static FloatLiteralToken operator *(FloatLiteralToken lhs, FloatLiteralToken rhs)
        {
            return new FloatLiteralToken((float.Parse(lhs.Value) * float.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom division operator for float literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values divided together</returns>
        public static FloatLiteralToken operator /(FloatLiteralToken lhs, FloatLiteralToken rhs)
        {
            try
            {
                FloatLiteralToken tok = new FloatLiteralToken((float.Parse(lhs.Value) / float.Parse(rhs.Value)).ToString());
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
    }
}
