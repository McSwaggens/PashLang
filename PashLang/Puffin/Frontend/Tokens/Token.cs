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

        public abstract Enum Type { get; }

        public abstract string Value { get; }

        public abstract Enum ResolveType();
        
    }
}
