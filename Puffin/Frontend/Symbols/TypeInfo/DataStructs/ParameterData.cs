namespace Puffin.Frontend.Symbols.TypeInfo.DataStructs
{
    public struct ParameterData
    {
        public string name;
        public Information type;
        public bool isReference;
        public bool isPointer;
        public bool isOptional;
        public bool isOut;
        public object defaultValue;
    }
}