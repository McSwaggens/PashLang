using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.MethodInfo
{
    public abstract class Method : MethodInformation
    {
        public Method(string name, Information returnType, ParameterInformation[] parameters) : base(name, returnType, parameters)
        {
        }
    }
}
