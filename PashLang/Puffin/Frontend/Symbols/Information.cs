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
        protected Modifier[] modifiers;
        protected string name;
        protected Scope definitionScope;

        public virtual Modifier[] Modifiers
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
        }
    }
}
