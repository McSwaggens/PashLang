using System;
using Puffin.Frontend.Tokens;

namespace Puffin.Frontend.Symbols
{
    public abstract class Symbol<T> : Token where T : Information
    {
        protected string identifierName;
        protected T identifierValue;
        protected EnumSymbolType identifierType;
        protected bool isConstant;
        protected bool isPointer;
        protected new Information type;
        protected new string value;

        /// <summary>
        ///  Returns whether the symbol is a function
        /// </summary>
        /// <returns>whether the symbol is a function</returns>
        public abstract bool IsFunction();
        /// <summary>
        ///  Returns whether the symbol is a class
        /// </summary>
        /// <returns>whether the symbol is a class</returns>
        public abstract bool IsClass();
        /// <summary>
        ///  Returns whether the symbol is a struct
        /// </summary>
        /// <returns>whether the symbol is a struct</returns>
        public abstract bool IsStruct();
        /// <summary>
        ///  Returns whether the symbol is a interface
        /// </summary>
        /// <returns>whether the symbol is a interface</returns>
        public abstract bool IsInterface();
        /// <summary>
        ///  Returns whether the symbol is a enum
        /// </summary>
        /// <returns>whether the symbol is a enum</returns>
        public abstract bool IsEnum();
        /// <summary>
        ///  Returns whether the symbol is a variable
        /// </summary>
        /// <returns>whether the symbol is a variable</returns>
        public abstract bool IsVariable();
        /// <summary>
        ///  Returns whether the symbol is a array
        /// </summary>
        /// <returns>whether the symbol is a array</returns>
        public abstract bool IsArray();
        /// <summary>
        ///  Returns whether the symbol is a namespace
        /// </summary>
        /// <returns>whether the symbol is a namespace</returns>
        public abstract bool isNamespace();
        /// <summary>
        /// Custom Equality operator for symbol
        /// </summary>
        /// <param name="lhs">the first symbol</param>
        /// <param name="rhs">the second symbol</param>
        /// <returns>whether the two symbols are equal</returns>
        public static bool operator ==(Symbol<T> lhs, Symbol<T> rhs)
        {
            if ((object) lhs == null && (object) rhs == null)
                return true;
            if ((object) lhs == null || (object) rhs == null)
                return false;
            if (lhs.Value == rhs.Value)
                return true;
            return false;
        }
        /// <summary>
        /// Custom non Equality operator for symbol
        /// </summary>
        /// <param name="lhs">the first symbol</param>
        /// <param name="rhs">the second symbol</param>
        /// <returns>whether the two symbols are not equal</returns>
        public static bool operator !=(Symbol<T> lhs, Symbol<T> rhs)
        {
            return !(lhs == rhs);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <returns>
        /// true if the specified object  is equal to the current object; otherwise, false.
        /// </returns>
        /// <param name="obj">The object to compare with the current object. </param>
        public override bool Equals(object obj)
        {
            if (!(obj is Symbol<Information>))
                return false;
            Symbol<Information> sym = obj as Symbol<Information>;
            if (this == sym)
                return true;
            return false;
        }

        /// <summary>
        /// Serves as the default hash function. 
        /// </summary>
        /// <returns>
        /// A hash code for the current object.
        /// </returns>
        public override int GetHashCode()
        {
            return this.IdentifierName.GetHashCode() + this.IdentifierValue.GetHashCode()*31;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.IdentifierName + " : " + this.IdentifierType + " : " + this.IdentifierValue;
        }

        /// <summary>
        /// Property for the tokens type
        /// </summary>
        public override Enum Type
        {
            get { return null; }
        }

        /// <summary>
        /// Property for the tokens string value
        /// </summary>
        public override string Value
        {
            get { return ""; }
        }

        /// <summary>
        /// This function resolves the type of the token
        /// </summary>
        /// <returns> the type of this token</returns>
        public override Enum ResolveType()
        {
            return EnumSymbolType.NAMESPACE;
        }
        /// <summary>
        /// Property for the symbol name
        /// </summary>
        public virtual string IdentifierName
        {
            get { return identifierName; }
        }
        /// <summary>
        /// Property for the symbols value
        /// </summary>
        public virtual T IdentifierValue
        {
            get { return identifierValue; }
            set
            {
                if (!isConstant) 
                    identifierValue = value;
                else
                    Console.WriteLine("ERROR Tried To write to a constant variable");
            }
        }
        /// <summary>
        /// Property for the Symbols type
        /// </summary>
        public virtual EnumSymbolType IdentifierType
        {
            get { return identifierType; }
            set { identifierType = value;}
        }
        /// <summary>
        /// Property for whether the symbols value is constant
        /// </summary>
        public virtual bool IsConstant
        {
            get { return isConstant; }
        }
        /// <summary>
        /// Property for whether the value is a pointer
        /// </summary>
        public bool IsPointer
        {
            get { return isPointer; }
        }

        public Information ValueType
        {
            get { return type; }
            set { type = value; }
        }
    }
}
