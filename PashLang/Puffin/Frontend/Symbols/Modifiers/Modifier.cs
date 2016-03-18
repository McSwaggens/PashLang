using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Symbols.Modifiers
{
    public class Modifier
    {
        public Modifier(EnumModifiers value)
        {
            this.value = value;
        }
        protected EnumModifiers value;
    }
}
