using System.Collections.Generic;

namespace CrocodileScript
{
    public class Function
    {
        public List<Variable> Variables = new List<Variable>();
        public Block attachedBlock;

        public VariableType[] RequiredVariableTypes;
        public bool RequiresParameters => RequiresParameters == null;

        private static int _id = -1;
        public int ID = ++_id;
        public string Name;
        public bool Public, Static;

        public Function(string Name, bool Public = false, bool Static = false)
        {
            this.Name = Name;
            this.Public = Public;
            this.Static = Static;
        }

        public string[] Compile()
        {
            List<string> CompileResult = new List<string>();
            foreach (Line line in attachedBlock.Lines)
            {
                line.CompileInLine();
            }
            return CompileResult.ToArray();
        }
    }
}
