using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public class ControlToken : Token
    {
        private Enum type;
        private string value;

        public ControlToken(string value)
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
            IEnumerable<EnumControlTokens> values = Enum.GetValues(typeof(EnumControlTokens)).Cast<EnumControlTokens>();
            foreach (EnumControlTokens op in values)
            {
                if (value.Equals(op.ToString().ToLower()))
                    return op;
            }
            switch (value)
            {
                case "{":
                    return EnumControlTokens.OPEN_CURLY_BRACKET;
                case "}":
                    return EnumControlTokens.CLOSE_CURLY_BRACKET;
                case "[":
                    return EnumControlTokens.OPEN_SQUARE_BRACKET;
                case "]":
                    return EnumControlTokens.CLOSE_SQUARE_BRACKET;
                case "(":
                    return EnumControlTokens.OPEN_BRACKET;
                case ")":
                    return EnumControlTokens.CLOSE_BRACKET;
                case "\n":
                    return EnumControlTokens.NEW_LINE;
                case "\t":
                    return EnumControlTokens.TAB;
                case "\0":
                    return EnumControlTokens.NULL_CHARACTER;
                default:
                    return null;
            }
            return null;
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
    }
}
