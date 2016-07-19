using System.Collections.Generic;
using Puffin.Frontend.Symbols.Modifiers;
using Puffin.Frontend.Symbols.TypeArgumentInfo;
using Puffin.Frontend.Tokens;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class ClassInformation : Information
    {
        protected object defaultValue;
        protected bool isPrimitive;
        protected bool isNullable;
        protected List<EnumOperators> validOperators;
        protected ClassInformation superClass;
        protected List<InterfaceInformation> implementedInterfaces;
        protected List<MethodInformation> implementedMethods;
        protected List<MethodInformation> overridedMethods;
        protected List<MethodInformation> inheritedMethods;
        protected List<VariableInformation> definedFields;
        protected List<VariableInformation> inheritedFields;
        protected List<EnumInformation> innerEnums;
        protected List<ClassInformation> innerClasses;
        protected List<StructInformation> innerStructs;
        protected List<InterfaceInformation> innerInterfaces;
        protected List<ClassTypeArgumentInformation<ClassInformation>> classTypeArguments;
        protected List<InterfaceTypeArgumentInformation<InterfaceInformation>> interfaceTypeArguments;
        protected List<StructTypeArgumentInformation<StructInformation>> structTypeArguments;
        protected List<EnumTypeArgumentInformation<EnumInformation>> enumTypeArguments;

        /// <summary>
        /// Constructor for class information
        /// </summary>
        /// <param name="name">the name of the class </param>
        /// <param name="defaultValue">the default value of this classes object</param>
        /// <param name="isPrimitive"> whether this class is a primitive type</param>
        /// <param name="isNullable"> whether this class is nullable</param>
        public ClassInformation(string name, object defaultValue, bool isPrimitive, bool isNullable = true)
        {
            this.name = name;
            this.defaultValue = defaultValue;
            this.isPrimitive = isPrimitive;
            this.isNullable = isNullable;

            this.modifiers = new List<Modifier>();
            this.validOperators = new List<EnumOperators>();
            this.superClass = null;
            this.implementedInterfaces = new List<InterfaceInformation>();
            this.implementedMethods = new List<MethodInformation>();
            this.overridedMethods = new List<MethodInformation>();
            this.inheritedMethods = new List<MethodInformation>();
            this.definedFields = new List<VariableInformation>();
            this.inheritedFields = new List<VariableInformation>();
            this.innerEnums = new List<EnumInformation>();
            this.innerClasses = new List<ClassInformation>();
            this.innerStructs = new List<StructInformation>();
            this.innerInterfaces = new List<InterfaceInformation>();
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
        /// Returns whether this class extends the specified type
        /// </summary>
        /// <param name="typeInformation"> the type</param>
        /// <returns> true if this class extends the type false if not </returns>
        public bool ExtendsType(ClassInformation typeInformation)
        {
            return superClass.Equals(typeInformation);
        }

        /// <summary>
        /// Returns whether this class implements the specified interface
        /// </summary>
        /// <param name="interfaceinInformation">the interface</param>
        /// <returns>true if this class implements the interface false if not</returns>
        public bool ImplementsInterface(InterfaceInformation interfaceinInformation)
        {
            return ImplementedInterfaces.Contains(interfaceinInformation);
        }

        /// <summary>
        /// Returns whether this class supports the specified operator
        /// </summary>
        /// <param name="operatorInfo"> the operator</param>
        /// <returns>true if this class supports the operator false if not</returns>
        public bool ImplementsOperator(EnumOperators operatorInfo)
        {
            return ValidOperators.Contains(operatorInfo);
        }

        /// <summary>
        /// Returns whether this class implements the specified method
        /// </summary>
        /// <param name="methodInfo"> the method</param>
        /// <returns>true if this class implements the method false if not</returns>
        public bool ImplementsMethod(MethodInformation methodInfo)
        {
            return ImplementedMethods.Contains(methodInfo);
        }

        /// <summary>
        /// Returns whether this class inherits the specified method from its super class
        /// </summary>
        /// <param name="methodInfo"> the method</param>
        /// <returns>true if this class inherits the method false if not</returns>
        public bool InheritsMethod(MethodInformation methodInfo)
        {
            return InheritedMethods.Contains(methodInfo);
        }

        /// <summary>
        /// Returns whether this class overrides the specified method
        /// </summary>
        /// <param name="methodInfo"> the method</param>
        /// <returns>true if this class supports the method false if not</returns>
        public bool OverridesMethod(MethodInformation methodInfo)
        {
            return OverridedMethods.Contains(methodInfo);
        }

        /// <summary>
        /// Returns whether this class defines the specified field
        /// </summary>
        /// <param name="fieldInfo"> the field</param>
        /// <returns>true if this class defines the field false if not</returns>
        public bool DefinesField(VariableInformation fieldInfo)
        {
            return DefinedFields.Contains(fieldInfo);
        }

        /// <summary>
        /// Returns whether this class inherits the specified field
        /// </summary>
        /// <param name="fieldInfo"> the field</param>
        /// <returns>true if this class inherits the field false if not</returns>
        public bool InheritsField(VariableInformation fieldInfo)
        {
            return InheritedFields.Contains(fieldInfo);
        }

        /// <summary>
        /// Returns whether this class defines the specified inner class
        /// </summary>
        /// <param name="classInfo"> the class</param>
        /// <returns>true if this class defines the inner class false if not</returns>
        public bool HasInnerClass(ClassInformation classInfo)
        {
            return InnerClasses.Contains(classInfo);
        }

        /// <summary>
        /// Returns whether this class defines the specified inner interface
        /// </summary>
        /// <param name="interfaceInfo"> the interface</param>
        /// <returns>true if this class defines the inner interface false if not</returns>
        public bool HasInnerInterface(InterfaceInformation interfaceInfo)
        {
            return InnerInterfaces.Contains(interfaceInfo);
        }

        /// <summary>
        /// Returns whether this class defines the specified inner struct
        /// </summary>
        /// <param name="structInfo"> the struct</param>
        /// <returns>true if this class defines the inner struct false if not</returns>
        public bool HasInnerStruct(StructInformation structInfo)
        {
            return InnerStructs.Contains(structInfo);
        }

        /// <summary>
        /// Returns whether this class defines the specified inner enum
        /// </summary>
        /// <param name="enumInfo"> the enum</param>
        /// <returns>true if this class defines the inner enum false if not</returns>
        public bool HasInnerEnum(EnumInformation enumInfo)
        {
            return InnerEnums.Contains(enumInfo);
        }

        /// <summary>
        /// Returns whether this class has the specified class type argument
        /// </summary>
        /// <param name="classTypeArgument"> the class type argument</param>
        /// <returns>true if this class has the class type argument false if not</returns>
        public bool HasTypeArgument(ClassTypeArgumentInformation<ClassInformation> classTypeArgument)
        {
            return ClassTypeArguments.Contains(classTypeArgument);
        }

        /// <summary>
        /// Returns whether this class has the specified interface type argument
        /// </summary>
        /// <param name="interfaceTypeArgument"> the interface type argument</param>
        /// <returns>true if this class has the interface type argument false if not</returns>
        public bool HasTypeArgument(InterfaceTypeArgumentInformation<InterfaceInformation> interfaceTypeArgument)
        {
            return InterfaceTypeArguments.Contains(interfaceTypeArgument);
        }

        /// <summary>
        /// Returns whether this class has the specified enum type argument
        /// </summary>
        /// <param name="enumTypeArgument"> the enum type argument</param>
        /// <returns>true if this class has the enum type argument false if not</returns>
        public bool HasTypeArgument(EnumTypeArgumentInformation<EnumInformation> enumTypeArgument)
        {
            return EnumTypeArguments.Contains(enumTypeArgument);
        }

        /// <summary>
        /// Returns whether this class has the specified struct type argument
        /// </summary>
        /// <param name="structTypeArgument"> the struct type argument</param>
        /// <returns>true if this class has the struct type argument false if not</returns>
        public bool HasTypeArgument(StructTypeArgumentInformation<StructInformation> structTypeArgument)
        {
            return StructTypeArguments.Contains(structTypeArgument);
        }

        /// <summary>
        /// Property for this classe's default value
        /// </summary>
        public virtual object DefaultValue
        {
            get { return defaultValue; }
        }

        /// <summary>
        /// Property for whether this class is a primitive type
        /// </summary>
        public virtual bool IsPrimitive
        {
            get { return isPrimitive; }
        }

        /// <summary>
        /// Property for whether this class is nullable 
        /// </summary>
        public virtual bool IsNullable
        {
            get { return isNullable; }
        }

        /// <summary>
        /// Property for the operators which are valid for this type
        /// </summary>
        public virtual List<EnumOperators> ValidOperators
        {
            get { return validOperators; }
        }

        /// <summary>
        /// Property for this classe's superclass
        /// </summary>
        public virtual ClassInformation SuperClass
        {
            get { return superClass; }
        }

        /// <summary>
        /// Property for the interfaces that this class implements
        /// </summary>
        public virtual List<InterfaceInformation> ImplementedInterfaces
        {
            get { return implementedInterfaces; }
        }

        /// <summary>
        /// Property for the methods this class implements
        /// </summary>
        public virtual List<MethodInformation> ImplementedMethods
        {
            get { return implementedMethods; }
        }

        /// <summary>
        /// Property for the methods that this class overrides
        /// </summary>
        public virtual List<MethodInformation> OverridedMethods
        {
            get { return overridedMethods; }
        }

        /// <summary>
        /// Property for the class that this class inherits
        /// </summary>
        public virtual List<MethodInformation> InheritedMethods
        {
            get { return inheritedMethods; }
        }

        /// <summary>
        /// Property for the fields that this class defines
        /// </summary>
        public virtual List<VariableInformation> DefinedFields
        {
            get { return definedFields; }
        }

        /// <summary>
        /// Property for the methods that this class inherits
        /// </summary>
        public virtual List<VariableInformation> InheritedFields
        {
            get { return inheritedFields; }
        }

        /// <summary>
        /// Property for the inner enums that this class defines
        /// </summary>
        public virtual List<EnumInformation> InnerEnums
        {
            get { return innerEnums; }
        }

        /// <summary>
        /// Property for the inner classes that this class defines
        /// </summary>
        public virtual List<ClassInformation> InnerClasses
        {
            get { return innerClasses; }
        }

        /// <summary>
        /// Property for the inner structs that this class defines
        /// </summary>
        public virtual List<StructInformation> InnerStructs
        {
            get { return innerStructs; }
        }

        /// <summary>
        /// Property for the inner interfaces this class defines
        /// </summary>
        protected List<InterfaceInformation> InnerInterfaces
        {
            get { return innerInterfaces; }
            set { innerInterfaces = value; }
        }
        /// <summary>
        /// Property for the class type arguments that this class takes
        /// </summary>
        public virtual List<ClassTypeArgumentInformation<ClassInformation>> ClassTypeArguments
        {
            get { return classTypeArguments; }
        }

        /// <summary>
        /// Property for the interface type arguments that this class takes
        /// </summary>
        public virtual List<InterfaceTypeArgumentInformation<InterfaceInformation>> InterfaceTypeArguments
        {
            get { return interfaceTypeArguments; }
        }

        /// <summary>
        /// Property for the struct type arguments that this class takes
        /// </summary>
        public virtual List<StructTypeArgumentInformation<StructInformation>> StructTypeArguments
        {
            get { return structTypeArguments; }
        }

        /// <summary>
        /// Property for the enum type arguments that this class takes
        /// </summary>
        public virtual List<EnumTypeArgumentInformation<EnumInformation>> EnumTypeArguments
        {
            get { return enumTypeArguments; }
        }

        /// <summary>
        /// Property for the access modifiers that this class has
        /// </summary>
        public virtual List<Modifier> AccessModifiers
        {
            get { return modifiers; }
        }

        /// <summary>
        /// Property for the class name
        /// </summary>
        public virtual string ClassName
        {
            get { return name; }
        }

        /// <summary>
        /// property for the class definition scope
        /// </summary>
        public virtual Scope ClassDefinitionScope
        {
            get { return definitionScope; }
        }
    }
}
