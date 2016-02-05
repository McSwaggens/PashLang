namespace CrocodileScript
{
    public class Variable
    {
        public Function function;
        public bool inFunction => function != null;
        public bool isPublic, isStatic;

        public VariableType type = VariableType.VOID;

        public string RawUnassignedValue;
        public bool WasAssignedValue => RawUnassignedValue != null;

        public string Name;
        private static int _id = -1;
        public int ID = ++_id;

        public Variable()
        {
        }

        public Variable(string name, Function f)
        {
            Name = name;
            function = f;
            ID = f.Variables.Count + 1;
        }

        public Variable(string name, bool isPublic = false, bool isStatic = false)
        {
            Name = name;
            this.isPublic = isPublic;
            this.isStatic = isStatic;
        }

        public string Tag => inFunction ? ":" + ID : ID + "";
    }
}
