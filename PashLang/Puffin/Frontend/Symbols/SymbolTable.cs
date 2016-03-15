using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Symbols
{
    public class SymbolTable
    {
        private List<Symbol<dynamic>> symbols;

        public List<Symbol<dynamic>> Symbols
        {
            get { return symbols; }
        }

        public SymbolTable()
        {
            symbols = new List<Symbol<dynamic>>();
        }

        public SymbolTable(List<Symbol<dynamic>> symbols)
        {
            this.symbols = symbols;
        }
    }
}
