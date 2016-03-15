using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Symbols
{
    public class Scope
    {
        public static readonly Scope GLOBAL_SCOPE = new Scope();
        private Scope parentScope;
        private LinkedList<Scope> childScopes;
        private Symbol<dynamic> ownerSymbol; 

        private Scope()
        {
            this.parentScope = null;
            this.ownerSymbol = null;
        }

        public Scope(Scope parent, Symbol<dynamic> owner)
        {
            if (parent == null)
            {
                this.parentScope = GLOBAL_SCOPE;
                this.ownerSymbol = null;
            }
            this.parentScope = parent;
            this.ownerSymbol = owner;
            childScopes = new LinkedList<Scope>();
        }


    }
}
