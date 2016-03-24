using System.Collections.Generic;
using Puffin.Frontend.Symbols.Modifiers;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class StructInformation : Information
    {
        protected object defaultValue;
        protected bool isPrimitive;
        protected bool isNullable;
        protected List<VariableInformation> definedFields;

        /// <summary>
        /// Constructor for struct information
        /// </summary>
        /// <param name="name">the name of the struct </param>
        /// <param name="defaultValue">the default value of this structs object</param>
        /// <param name="isPrimitive"> whether this struct is a primitive type</param>
        /// <param name="isNullable"> whether this struct is nullable</param>
        public StructInformation(string name, object defaultValue, bool isPrimitive, bool isNullable = false)
        {
            this.name = name;
            this.defaultValue = defaultValue;
            this.isPrimitive = isPrimitive;
            this.isNullable = isNullable;

            this.modifiers = new List<Modifier>();
            this.definedFields = new List<VariableInformation>();
        }

        /// <summary>
        /// Property for this struct's default value
        /// </summary>
        public virtual object DefaultValue
        {
            get { return defaultValue; }
        }

        /// <summary>
        /// Property for whether this struct is a primitive type
        /// </summary>
        public virtual bool IsPrimitive
        {
            get { return isPrimitive; }
        }

        /// <summary>
        /// Property for whether this struct is nullable
        /// </summary>
        public virtual bool IsNullable
        {
            get { return isNullable; }
        }

        /// <summary>
        /// Property for this structs defined fields
        /// </summary>
        public virtual List<VariableInformation> DefinedFields
        {
            get { return definedFields; }
        }

        /// <summary>
        /// Property for this struct's modifiers
        /// </summary>
        public virtual List<Modifier> StructModifiers
        {
            get { return modifiers; }
        }

        /// <summary>
        /// Property for this struct's name
        /// </summary>
        public virtual string StructName
        {
            get { return name; }
        }

        /// <summary>
        /// Property for this struct's definition scope
        /// </summary>
        public virtual Scope StructDefinitionScope
        {
            get { return definitionScope; }
        }
    }
}
