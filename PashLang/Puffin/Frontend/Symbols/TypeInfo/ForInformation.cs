using Puffin.Frontend.AST;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class ForInformation : Information
    {
        protected VariableInformation counter;
        protected Statement condition;
        protected Statement iterator;

        public ForInformation(VariableInformation counter, Statement condition, Statement iterator)
        {
            
        }
    }
}
