using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols
{
    public class MethodSymbol<T> : Symbol<T> where T : Information
    {
        private MethodInformation info;

        public MethodSymbol(MethodInformation info)
        {
            this.info = info;
            this.identifierName = info.Name;
            this.ValueType = info.ReturnType;
        }

        public MethodInformation Info
        {
            get { return info; }
            set { info = value; }
        }

        /// <summary>
        ///  Returns whether the symbol is a function
        /// </summary>
        /// <returns>whether the symbol is a function</returns>
        public override bool IsFunction()
        {
            return true;
        }

        /// <summary>
        ///  Returns whether the symbol is a class
        /// </summary>
        /// <returns>whether the symbol is a class</returns>
        public override bool IsClass()
        {
            return false;
        }

        /// <summary>
        ///  Returns whether the symbol is a struct
        /// </summary>
        /// <returns>whether the symbol is a struct</returns>
        public override bool IsStruct()
        {
            return false;
        }

        /// <summary>
        ///  Returns whether the symbol is a interface
        /// </summary>
        /// <returns>whether the symbol is a interface</returns>
        public override bool IsInterface()
        {
            return false;
        }

        /// <summary>
        ///  Returns whether the symbol is a enum
        /// </summary>
        /// <returns>whether the symbol is a enum</returns>
        public override bool IsEnum()
        {
            return false;
        }

        /// <summary>
        ///  Returns whether the symbol is a variable
        /// </summary>
        /// <returns>whether the symbol is a variable</returns>
        public override bool IsVariable()
        {
            return false;
        }

        /// <summary>
        ///  Returns whether the symbol is a array
        /// </summary>
        /// <returns>whether the symbol is a array</returns>
        public override bool IsArray()
        {
            return false;
        }

        /// <summary>
        ///  Returns whether the symbol is a namespace
        /// </summary>
        /// <returns>whether the symbol is a namespace</returns>
        public override bool isNamespace()
        {
            return false;
        }
    }
}
