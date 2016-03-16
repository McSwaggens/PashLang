using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        protected EnumOperators[] validOperators;
        protected ClassInformation superClass;
        protected InterfaceInformation[] implementedInterfaces;
        protected MethodInformation[] implementedMethods;
        protected MethodInformation[] overridedMethods;
        protected MethodInformation[] inheritedMethods;
        protected FieldInformation[] definedFields;
        protected FieldInformation[] inheritedFields;
        protected EnumInformation[] innerEnums;
        protected ClassInformation[] innerClasses;
        protected StructInformation[] innerStructs;
        protected InterfaceInformation[] innerInterfaces;
        protected ClassTypeArgumentInformation<ClassInformation>[] classTypeArguments;
        protected InterfaceTypeArgumentInformation<InterfaceInformation>[] interfaceTypeArguments;
        protected StructTypeArgumentInformation<StructInformation>[] structTypeArguments;
        protected EnumTypeArgumentInformation<EnumInformation>[] enumTypeArguments;

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
        public bool DefinesField(FieldInformation fieldInfo)
        {
            return DefinedFields.Contains(fieldInfo);
        }

        /// <summary>
        /// Returns whether this class inherits the specified field
        /// </summary>
        /// <param name="fieldInfo"> the field</param>
        /// <returns>true if this class inherits the field false if not</returns>
        public bool InheritsField(FieldInformation fieldInfo)
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
        public virtual EnumOperators[] ValidOperators
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
        public virtual InterfaceInformation[] ImplementedInterfaces
        {
            get { return implementedInterfaces; }
        }

        /// <summary>
        /// Property for the methods this class implements
        /// </summary>
        public virtual MethodInformation[] ImplementedMethods
        {
            get { return implementedMethods; }
        }

        /// <summary>
        /// Property for the methods that this class overrides
        /// </summary>
        public virtual MethodInformation[] OverridedMethods
        {
            get { return overridedMethods; }
        }

        /// <summary>
        /// Property for the class that this method inherits
        /// </summary>
        public virtual MethodInformation[] InheritedMethods
        {
            get { return inheritedMethods; }
        }

        /// <summary>
        /// Property for the fields that this class defines
        /// </summary>
        public virtual FieldInformation[] DefinedFields
        {
            get { return definedFields; }
        }

        /// <summary>
        /// Property for the methods that this method inherits
        /// </summary>
        public virtual FieldInformation[] InheritedFields
        {
            get { return inheritedFields; }
        }

        /// <summary>
        /// Property for the inner enums that this class defines
        /// </summary>
        public virtual EnumInformation[] InnerEnums
        {
            get { return innerEnums; }
        }

        /// <summary>
        /// Property for the inner classes that this class defines
        /// </summary>
        public virtual ClassInformation[] InnerClasses
        {
            get { return innerClasses; }
        }

        /// <summary>
        /// Property for the inner structs that this class defines
        /// </summary>
        public virtual StructInformation[] InnerStructs
        {
            get { return innerStructs; }
        }

        /// <summary>
        /// Property for the inner interfaces this class defines
        /// </summary>
        protected InterfaceInformation[] InnerInterfaces
        {
            get { return innerInterfaces; }
            set { innerInterfaces = value; }
        }
        /// <summary>
        /// Property for the class type arguments that this class takes
        /// </summary>
        public virtual ClassTypeArgumentInformation<ClassInformation>[] ClassTypeArguments
        {
            get { return classTypeArguments; }
        }

        /// <summary>
        /// Property for the interface type arguments that this class takes
        /// </summary>
        public virtual InterfaceTypeArgumentInformation<InterfaceInformation>[] InterfaceTypeArguments
        {
            get { return interfaceTypeArguments; }
        }

        /// <summary>
        /// Property for the struct type arguments that this class takes
        /// </summary>
        public virtual StructTypeArgumentInformation<StructInformation>[] StructTypeArguments
        {
            get { return structTypeArguments; }
        }

        /// <summary>
        /// Property for the enum type arguments that this class takes
        /// </summary>
        public virtual EnumTypeArgumentInformation<EnumInformation>[] EnumTypeArguments
        {
            get { return enumTypeArguments; }
        }

        /// <summary>
        /// Property for the access modifiers that this class has
        /// </summary>
        public virtual Modifier[] AccessModifiers
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
