using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public class OperatorToken : Token
    {
        private Enum type;
        private string value;

        public OperatorToken(string value)
        {
            this.value = value;
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
    }
}
