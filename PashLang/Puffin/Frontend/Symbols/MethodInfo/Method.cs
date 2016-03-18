using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
