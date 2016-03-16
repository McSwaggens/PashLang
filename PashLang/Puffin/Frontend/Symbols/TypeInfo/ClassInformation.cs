using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Tokens;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public abstract class ClassInformation : Information
    {
        protected object defaultValue;
        protected bool isPrimitive;
        protected bool isNullable;
        protected EnumOperators[] validOperators;
        protected ClassInformation superType;
        protected InterfaceInformation[] implementedInterfaces;
        protected MethodInformation[] implementedMethods;
        protected MethodInformation[] overridedMethods;
        protected MethodInformation[] inheritedMethods;
        protected FieldInformation[] implementedFields;
        protected FieldInformation[] inheritedFields;
        protected EnumInformation[] implementedEnums;
        protected ClassInformation[] implementedClasses;
        protected StructInformation[] implementedStructs;

        public abstract bool ExtendsType(ClassInformation typeInformation);
        public abstract bool ImplementsInterface(InterfaceInformation interfaceinInformation);
        public abstract bool ImplementsOperator(EnumOperators operatorInfo);
        
        public virtual object DefaultValue
        {
            get { return defaultValue; }
        }

        public virtual bool IsPrimitive
        {
            get { return isPrimitive; }
        }

        public virtual EnumOperators[] ValidOperators
        {
            get { return validOperators; }
        }

        public virtual ClassInformation SuperType
        {
            get { return superType; }
        }

        public virtual InterfaceInformation[] ImplementedInterfaces
        {
            get { return implementedInterfaces; }
        }

        public bool IsNullable
        {
            get { return isNullable; }
        }
    }
}
