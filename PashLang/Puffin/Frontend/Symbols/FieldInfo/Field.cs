using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.FieldInfo
{
    public abstract class Field : FieldInformation
    {
        public Field(string name, Information type, bool isConstant, Information initialvalue = null) : base(name, type, isConstant, initialvalue)
        {
        }
    }
}
