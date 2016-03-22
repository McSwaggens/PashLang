using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public class OperatorToken : Token
    {
        
        

        public OperatorToken(string value)
        {
            this.value = value;
            if ((this.type = ResolveType()) == null)
            {
                this.type = ResolveOperator();
                if(this.type.Equals((Enum)EnumOperators.NO_OPERATOR)) 
                    Logger.WriteError("This token is not an operator: " + this.value);
            }
        }

        public override Enum Type => type;

        public override string Value => value;

        public override Enum ResolveType()
        {
            var values = Enum.GetValues(typeof(EnumOperators)).Cast<EnumOperators>();
            foreach (EnumOperators op in values)
            {
                if (value.Equals(op.ToString().ToLower()))
                    return op;
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

        private EnumOperators ResolveOperator()
        {
            for (int i = 0; i < Lexer.operators.Count; i++)
            {
                if (value.Equals(Lexer.operators[i]))
                {
                    return (EnumOperators)i;
                }
            }
            return EnumOperators.NO_OPERATOR;
        }
    }
}
