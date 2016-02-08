using System.Collections.Generic;

namespace CrocodileScript
{
    public class Variable
    {
        public Function function;
        public bool inFunctionSpace => function == null;
        public bool Public, Static;

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

        public Variable(string name, bool @public = false, bool @static = false)
        {
            Name = name;
            Public = @public;
            Static = @static;
        }

        public string[] Compile()
        {
            List<string> code = new List<string>();

            CrocCompiler.ComputeResult result = CrocCompiler.instance.Calculate(RawUnassignedValue);
            code = result.ComputeCode;
            
            return code.ToArray();
        }

        public string Tag => !inFunctionSpace ? ":" + ID : ID + "";
    }
}
