using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.Modifiers;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class ArrayParameterInformation : ParameterInformation
    {
        protected new object[] defaultValue;

        public ArrayParameterInformation(string name, Information type, bool isReference, bool isPointer, bool isOptional, bool isOut, object[] defaultValue = null) : base(name, type, isReference, isPointer, isOptional, isOut, defaultValue)
        {
            this.name = name;
            this.type = type;
            this.isReference = isReference;
            this.isPointer = isPointer;
            this.isOptional = isOptional;
            this.isOut = isOut;
            this.defaultValue = defaultValue;
        }
    }
}
