using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Symbols
{
    public abstract class SymbolTable<T>
    {
        private List<Symbol<Information>> symbols;

        public List<Symbol<Information>> Symbols
        {
            get { return symbols; }
        }

        public SymbolTable()
        {
            symbols = new List<Symbol<Information>>();
        }

        public SymbolTable(List<Symbol<Information>> symbols)
        {
            this.symbols = symbols;
        }
    }
}
