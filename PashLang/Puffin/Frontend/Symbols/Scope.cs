using System.Collections.Generic;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols
{
    public class Scope
    {
        public static readonly Scope GLOBAL_SCOPE = new Scope();
        private Scope parentScope;
        private LinkedList<Scope> childScopes;
        private Symbol<Information> ownerSymbol;
        private MethodInformation fnInfo;

        private Scope()
        {
            this.parentScope = null;
            this.ownerSymbol = null;
        }

        public Scope(Scope parent, Symbol<Information> owner)
        {
            if (parent == null)
            {
                this.parentScope = GLOBAL_SCOPE;
                this.ownerSymbol = null;
            }
            else
            {
                this.parentScope = parent;
                this.ownerSymbol = owner;
            }
            childScopes = new LinkedList<Scope>();
        }

        public Scope(Scope currrentScope, MethodInformation fnInfo)
        {
            if (currrentScope == null)
            {
                this.parentScope = GLOBAL_SCOPE;
                this.ownerSymbol = null;
            }
            else
            {
                this.parentScope = currrentScope;
                this.fnInfo = fnInfo;
            }
        }

        public Scope ParentScope
        {
            get { return parentScope; }
        }

        public LinkedList<Scope> ChildScopes
        {
            get { return childScopes; }
        }

        public Symbol<Information> OwnerSymbol
        {
            get { return ownerSymbol; }
        }

        public MethodInformation FnInfo
        {
            get { return fnInfo; }
        }
    }
}
