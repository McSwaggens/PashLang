namespace Puffin.Frontend.Symbols.TypeInfo.DataStructs
{
    public struct VariableData
    {
        public string name;
        public Information type;
        public bool isConstant;
        public bool isPointer;
        public object initialvalue;
    }
}