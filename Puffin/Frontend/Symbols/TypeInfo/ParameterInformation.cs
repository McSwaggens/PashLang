namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class ParameterInformation : Information
    {
        protected bool isOptional;
        protected bool isReference;
        protected bool isPointer;
        protected bool isOut;
        protected object defaultValue;

        public ParameterInformation(string name, Information type, bool isReference, bool isPointer, bool isOptional, bool isOut, object defaultValue = null)
        {
            this.name = name;
            this.type = type;
            this.isReference = isReference;
            this.isPointer = isPointer;
            this.isOptional = isOptional;
            this.isOut = isOut;
            this.defaultValue = defaultValue;
        }


    }
}
