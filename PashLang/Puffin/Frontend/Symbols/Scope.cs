using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols
{
    public class Scope
    {
        public static readonly Scope GLOBAL_SCOPE = new Scope();
        private Scope parentScope;
        private LinkedList<Scope> childScopes;
        private Symbol<Information> ownerSymbol;
        private Scope currrentScope;
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
            this.parentScope = parent;
            this.ownerSymbol = owner;
            childScopes = new LinkedList<Scope>();
        }

        public Scope(Scope currrentScope, MethodInformation fnInfo)
        {
            this.currrentScope = currrentScope;
            this.fnInfo = fnInfo;
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

        public Scope CurrrentScope
        {
            get { return currrentScope; }
        }

        public MethodInformation FnInfo
        {
            get { return fnInfo; }
        }
    }
}
