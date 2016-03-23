using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.Modifiers;

namespace Puffin.Frontend.Symbols
{
    public abstract class Information
    {
        protected List<Modifier> modifiers;
        protected string name;
        protected Scope definitionScope;
        protected Information type;

        public virtual List<Modifier> Modifiers
        {
            get { return modifiers; }
        }

        public virtual string Name
        {
            get { return name; }
        }

        public virtual Scope DefinitionScope
        {
            get { return definitionScope; }
            set { definitionScope = value;}
        }

        public virtual Information IdentifierType
        {
            get { return type; }
            set { type = value; }
        }
    }
}
