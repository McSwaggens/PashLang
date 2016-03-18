using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.Modifiers;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class FieldInformation : Information
    {
        protected Information type;
        protected Information initialValue;
        protected bool isConstant;
        protected bool isInitialised;

        public FieldInformation(string name, Information type, bool isConstant, Information initialvalue = null)
        {
            this.name = name;
            this.type = type;
            this.isConstant = isConstant;
            this.initialValue = initialvalue;
            if (this.initialValue != null)
                this.isInitialised = true;
            else
                this.isInitialised = false;
            this.modifiers = new List<Modifier>();
        }
    }
}
