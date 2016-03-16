using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.Modifiers;
using Puffin.Frontend.Symbols.TypeArgumentInfo;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public abstract class EnumInformation : Information
    {
        protected StructInformation valueType;
        protected object defaultValue;
        protected bool isPrimitive;
        protected bool isNullable;
        protected List<StructInformation> definedValues;

        /// <summary>
        /// Constructor for enum information
        /// </summary>
        /// <param name="name">the name of the enum </param>
        /// <param name="defaultValue">the default value of this enum object</param>
        /// <param name="isPrimitive"> whether this enum is a primitive type</param>
        /// <param name="isNullable"> whether this enum is nullable</param>
        public EnumInformation(string name, object defaultValue, bool isPrimitive, bool isNullable = false)
        {
            this.name = name;
            this.defaultValue = defaultValue;
            this.isPrimitive = isPrimitive;
            this.isNullable = isNullable;

            this.modifiers = new List<Modifier>();
            this.definedValues = new List<StructInformation>();
        }

        /// <summary>
        /// Returns whether this enum has the specified modifier
        /// </summary>
        /// <param name="modifier"> the modifier</param>
        /// <returns> true if this enum  has the modifier false if not </returns>
        public bool HasModifier(Modifier modifier)
        {
            return modifiers.Contains(modifier);
        }
        
        /// <summary>
        /// Returns whether the enum defines this value
        /// </summary>
        /// <param name="value"> the value </param>
        /// <returns>true if the enum defines this value false if not</returns>
        public bool DefinesValue(StructInformation value)
        {
            return definedValues.Contains(value);
        }

        public virtual StructInformation ValueType
        {
            get { return valueType; }
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
        /// Property for this enum's defined values
        /// </summary>
        public virtual List<StructInformation> DefinedValues
        {
            get { return definedValues; }
        }

        /// <summary>
        /// Property for this enum's modifiers
        /// </summary>
        public virtual List<Modifier> EnumModifiers
        {
            get { return modifiers; }
        }

        /// <summary>
        /// Property for this emum's name
        /// </summary>
        public virtual string EnumName
        {
            get { return name; }
        }

        /// <summary>
        /// Property for this enum's definition scope
        /// </summary>
        public virtual Scope EnumDefinitionScope
        {
            get { return definitionScope; }
        }
    }
}
