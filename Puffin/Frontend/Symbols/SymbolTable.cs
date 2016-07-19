using System.Collections.Generic;

namespace Puffin.Frontend.Symbols
{
    public class SymbolTable<T>
    {
        private List<Symbol<Information>> symbols;

        public SymbolTable()
        {
            symbols = new List<Symbol<Information>>();
        }

        public SymbolTable(List<Symbol<Information>> symbols)
        {
            this.symbols = symbols;
        }

        public List<Symbol<Information>> Symbols
        {
            get { return symbols; }
        }
    }
}
