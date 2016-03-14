using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public abstract class Token
    {
        private Enum type;
        private string value;

        /// <summary>
        /// Property for the tokens type
        /// </summary>
        public abstract Enum Type { get; }
        /// <summary>
        /// Property for the tokens string value
        /// </summary>
        public abstract string Value { get; }
        /// <summary>
        /// This function resolves the type of the token
        /// </summary>
        /// <returns> the type of this token</returns>
        public abstract Enum ResolveType();
        /// <summary>
        /// Custom equality operator for tokens
        /// </summary>
        /// <param name="lhs"> the first token</param>
        /// <param name="rhs"> the second token</param>
        /// <returns> whether the tokens are equal</returns>
        public static bool operator ==(Token lhs, Token rhs)
        {
            if ((object)lhs == null || (object)rhs == null)
                return false;
            if (Object.ReferenceEquals(lhs, rhs))
                return true;
            if ((lhs.type.Equals(rhs.type)) && (lhs.value.Equals(rhs.value)))
                return true;
            return false;
        }
        /// <summary>
        /// Custom not equality operator for tokens
        /// </summary>
        /// <param name="lhs"> the first token</param>
        /// <param name="rhs"> the second token</param>
        /// <returns>whether the tokens are not equal</returns>
        public static bool operator !=(Token lhs, Token rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (!(obj is Token))
                return false;
            Token tok = (Token) obj;
            return this == tok;
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
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode()  * 31;
        }
    }
}
