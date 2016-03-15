using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Tokens
{
    public class KeywordToken : Token
    {
        
        

        public KeywordToken(string value)
        {
            this.value = value;
            this.type = ResolveType();
        }

        public override Enum Type => type;

        public override string Value => value;

        public override Enum ResolveType()
        {
            var values = Enum.GetValues(typeof(EnumKeywords)).Cast<EnumKeywords>();
            foreach (EnumKeywords ty in values)
            {
                if (value.Equals(ty.ToString().ToLower()))
                    return ty;
            }
            return null;
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return this.value + " : " + this.type.ToString();
        }
    }
}
