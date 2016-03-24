using System;
using static Puffin.Logger;
namespace Puffin.Frontend.Tokens
{
    public class DoubleLiteralToken : Token
    {
        
        

        public DoubleLiteralToken(string value)
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
            return EnumLiterals.DOUBLE;
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
        /// Custom additon operator for double literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values added together</returns>
        public static DoubleLiteralToken operator +(DoubleLiteralToken lhs, DoubleLiteralToken rhs)
        {
            return new DoubleLiteralToken((double.Parse(lhs.Value) + double.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom subtraction operator for double literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values subtracted together</returns>
        public static DoubleLiteralToken operator -(DoubleLiteralToken lhs, DoubleLiteralToken rhs)
        {
            return new DoubleLiteralToken((double.Parse(lhs.Value) - double.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom multiplication operator for double literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values multiplied together</returns>
        public static DoubleLiteralToken operator *(DoubleLiteralToken lhs, DoubleLiteralToken rhs)
        {
            return new DoubleLiteralToken((double.Parse(lhs.Value) * double.Parse(rhs.Value)).ToString());
        }

        /// <summary>
        /// Custom division operator for double literal
        /// </summary>
        /// <param name="lhs">the first literal</param>
        /// <param name="rhs">the second literal</param>
        /// <returns>a new IntegerLiteral token with the values divided together</returns>
        public static DoubleLiteralToken operator /(DoubleLiteralToken lhs, DoubleLiteralToken rhs)
        {
            try
            {
                DoubleLiteralToken tok = new DoubleLiteralToken((double.Parse(lhs.Value) / double.Parse(rhs.Value)).ToString());
                return tok;
            }
            catch (DivideByZeroException ex)
            {
                WriteError("You know you should never divide by zero!!!");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR: You know you should never divide by zero!!!");
                Console.ResetColor();
                return null;
            }
        }
    }
}
