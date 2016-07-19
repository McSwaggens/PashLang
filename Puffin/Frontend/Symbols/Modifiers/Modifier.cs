namespace Puffin.Frontend.Symbols.Modifiers
{
    public class Modifier
    {
        public Modifier(EnumModifiers value)
        {
            this.value = value;
        }
        protected EnumModifiers value;

        public EnumModifiers Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }
}
