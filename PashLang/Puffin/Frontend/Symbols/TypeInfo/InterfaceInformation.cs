using System.Collections.Generic;
using Puffin.Frontend.Symbols.Modifiers;
using Puffin.Frontend.Symbols.TypeArgumentInfo;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class InterfaceInformation : Information
    {
        protected object defaultValue;
        protected bool isPrimitive;
        protected bool isNullable;
        protected List<InterfaceInformation> extendedInterfaces;
        protected List<VariableInformation> definedFields;
        protected List<VariableInformation> inheritedFields;
        protected List<MethodInformation> definedMethods;
        protected List<MethodInformation> inheritedMethods;
        protected List<ClassTypeArgumentInformation<ClassInformation>> classTypeArguments;
        protected List<InterfaceTypeArgumentInformation<InterfaceInformation>> interfaceTypeArguments;
        protected List<StructTypeArgumentInformation<StructInformation>> structTypeArguments;
        protected List<EnumTypeArgumentInformation<EnumInformation>> enumTypeArguments;

        /// <summary>
        /// Constructor for interface information
        /// </summary>
        /// <param name="name">the name of the interface </param>
        /// <param name="defaultValue">the default value of this interfaces object</param>
        /// <param name="isPrimitive"> whether this interface is a primitive type</param>
        /// <param name="isNullable"> whether this interface is nullable</param>
        public InterfaceInformation(string name, object defaultValue, bool isPrimitive, bool isNullable = true)
        {
            this.name = name;
            this.defaultValue = defaultValue;
            this.isPrimitive = isPrimitive;
            this.isNullable = isNullable;

            this.modifiers = new List<Modifier>();
            this.inheritedMethods = new List<MethodInformation>();
            this.definedFields = new List<VariableInformation>();
            this.inheritedFields = new List<VariableInformation>();
            this.classTypeArguments = new List<ClassTypeArgumentInformation<ClassInformation>>();
            this.interfaceTypeArguments = new List<InterfaceTypeArgumentInformation<InterfaceInformation>>();
            this.structTypeArguments = new List<StructTypeArgumentInformation<StructInformation>>();
            this.enumTypeArguments = new List<EnumTypeArgumentInformation<EnumInformation>>();
        }

        /// <summary>
        /// Returns whether this class has the specified modifier
        /// </summary>
        /// <param name="modifier"> the modifier</param>
        /// <returns> true if this class has the modifier false if not </returns>
        public bool HasModifier(Modifier modifier)
        {
            return Modifiers.Contains(modifier);
        }

        /// <summary>
        /// Returns whether this interface inherits the specified method from its super interface
        /// </summary>
        /// <param name="methodInfo"> the method</param>
        /// <returns>true if this interface inherits the method false if not</returns>
        public bool InheritsMethod(MethodInformation methodInfo)
        {
            return InheritedMethods.Contains(methodInfo);
        }

        /// <summary>
        /// Returns whether this interface defines the specified field
        /// </summary>
        /// <param name="fieldInfo"> the field</param>
        /// <returns>true if this interface defines the field false if not</returns>
        public bool DefinesField(VariableInformation fieldInfo)
        {
            return DefinedFields.Contains(fieldInfo);
        }

        /// <summary>
        /// Returns whether this interface inherits the specified field
        /// </summary>
        /// <param name="fieldInfo"> the field</param>
        /// <returns>true if this interface inherits the field false if not</returns>
        public bool InheritsField(VariableInformation fieldInfo)
        {
            return InheritedFields.Contains(fieldInfo);
        }

        /// <summary>
        /// Returns whether this interface has the specified interface type argument
        /// </summary>
        /// <param name="classTypeArgument"> the class type argument</param>
        /// <returns>true if this interface has the interface type argument false if not</returns>
        public bool HasTypeArgument(ClassTypeArgumentInformation<ClassInformation> classTypeArgument)
        {
            return ClassTypeArguments.Contains(classTypeArgument);
        }

        /// <summary>
        /// Returns whether this interface has the specified interface type argument
        /// </summary>
        /// <param name="interfaceTypeArgument"> the interface type argument</param>
        /// <returns>true if this interface has the interface type argument false if not</returns>
        public bool HasTypeArgument(InterfaceTypeArgumentInformation<InterfaceInformation> interfaceTypeArgument)
        {
            return InterfaceTypeArguments.Contains(interfaceTypeArgument);
        }

        /// <summary>
        /// Returns whether this interface has the specified enum type argument
        /// </summary>
        /// <param name="enumTypeArgument"> the enum type argument</param>
        /// <returns>true if this interface has the enum type argument false if not</returns>
        public bool HasTypeArgument(EnumTypeArgumentInformation<EnumInformation> enumTypeArgument)
        {
            return EnumTypeArguments.Contains(enumTypeArgument);
        }

        /// <summary>
        /// Returns whether this interface has the specified struct type argument
        /// </summary>
        /// <param name="structTypeArgument"> the struct type argument</param>
        /// <returns>true if this interface has the struct type argument false if not</returns>
        public bool HasTypeArgument(StructTypeArgumentInformation<StructInformation> structTypeArgument)
        {
            return StructTypeArguments.Contains(structTypeArgument);
        }

        /// <summary>
        /// Property for this interface's default value
        /// </summary>
        public virtual object DefaultValue
        {
            get { return defaultValue; }
        }

        /// <summary>
        /// Property for whether this interface is a primitive type
        /// </summary>
        public virtual bool IsPrimitive
        {
            get { return isPrimitive; }
        }

        /// <summary>
        /// Property for whether this interface is nullable 
        /// </summary>
        public virtual bool IsNullable
        {
            get { return isNullable; }
        }
        /// <summary>
        /// Property for the interface that this method inherits
        /// </summary>
        public virtual List<MethodInformation> InheritedMethods
        {
            get { return inheritedMethods; }
        }

        /// <summary>
        /// Property for the fields that this interface defines
        /// </summary>
        public virtual List<VariableInformation> DefinedFields
        {
            get { return definedFields; }
        }

        /// <summary>
        /// Property for the methods that this method inherits
        /// </summary>
        public virtual List<VariableInformation> InheritedFields
        {
            get { return inheritedFields; }
        }
        /// <summary>
        /// Property for the interface type arguments that this interface takes
        /// </summary>
        public virtual List<ClassTypeArgumentInformation<ClassInformation>> ClassTypeArguments
        {
            get { return classTypeArguments; }
        }

        /// <summary>
        /// Property for the interface type arguments that this interface takes
        /// </summary>
        public virtual List<InterfaceTypeArgumentInformation<InterfaceInformation>> InterfaceTypeArguments
        {
            get { return interfaceTypeArguments; }
        }

        /// <summary>
        /// Property for the struct type arguments that this interface takes
        /// </summary>
        public virtual List<StructTypeArgumentInformation<StructInformation>> StructTypeArguments
        {
            get { return structTypeArguments; }
        }

        /// <summary>
        /// Property for the enum type arguments that this interface takes
        /// </summary>
        public virtual List<EnumTypeArgumentInformation<EnumInformation>> EnumTypeArguments
        {
            get { return enumTypeArguments; }
        }

        /// <summary>
        /// Property for the access modifiers that this interface has
        /// </summary>
        public virtual List<Modifier> AccessModifiers
        {
            get { return modifiers; }
        }

        /// <summary>
        /// Property for the interface name
        /// </summary>
        public virtual string ClassName
        {
            get { return name; }
        }

        /// <summary>
        /// property for the interface definition scope
        /// </summary>
        public virtual Scope ClassDefinitionScope
        {
            get { return definitionScope; }
        }
    }
}
