using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.VariableInfo
{
    public abstract class Variable : VariableInformation
    {
        public Variable(string name, Information type, bool isConstant, bool isPointer, Information initialvalue = null) : base(name, type, isConstant, isPointer, initialvalue)
        {
        }
    }
}
