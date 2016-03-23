using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.Modifiers;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class ArrayInformation : Information
    {
        protected Information[] initialValue;
        protected bool isConstant;
        protected bool isInitialised;
        protected int numberOfItems;

        public ArrayInformation(string name, Information type, bool isConstant, int numberOfItems, params Information[] initialvalue)
        {
            this.name = name;
            this.type = type;
            this.isConstant = isConstant;
            this.numberOfItems = numberOfItems;
            this.initialValue = initialvalue;
            if (this.initialValue != null)
                this.isInitialised = true;
            else
                this.isInitialised = false;
            this.modifiers = new List<Modifier>();
        }

        public Information[] InitialValue
        {
            get { return initialValue; }
        }

        public bool IsConstant
        {
            get { return isConstant; }
        }

        public bool IsInitialised
        {
            get { return isInitialised; }
        }

        public int NumberOfItems
        {
            get { return numberOfItems; }
        }
    }
}
