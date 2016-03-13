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
        }

        public override Enum Type => type;

        public override string Value => value;

        public override Enum ResolveType()
        {
            var values = Enum.GetValues(typeof(EnumControlTokens)).Cast<EnumControlTokens>();
            foreach (EnumControlTokens op in values)
            {
                if (value.Equals(op.ToString().ToLower()))
                    return op;
            }
            return null;
        }
    }
}
