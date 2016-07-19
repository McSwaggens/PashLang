using System.Collections.Generic;

namespace Puffin.Frontend.Symbols.TypeInfo.DataStructs
{
    public struct MethodData
    {
        public string name;
        public Information returnType;
        public List<Information> parameters;
    }
}