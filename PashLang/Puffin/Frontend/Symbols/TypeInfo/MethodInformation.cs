using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.Modifiers;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class MethodInformation : Information
    {
        Information returnType;
        Information[] parameters;

        public MethodInformation(string name, Information returnType, Information[] parameters)
        {
            this.name = name;
            this.parameters = parameters;
            this.returnType = returnType;
            this.modifiers = new List<Modifier>();
        }

        public bool ReturnsType(Information type)
        {
            return this.returnType.Equals(type);
        }

        public bool HasParameter(Information parameter)
        {
            return parameters.Contains(parameter);
        }

        public bool HasModifier(Modifier modifier)
        {
            return modifiers.Contains(modifier);
        }

        public Information ReturnType
        {
            get { return returnType; }
        }

        public Information[] Parameters
        {
            get { return parameters; }
            set { parameters = value;}
        }
    }
}
