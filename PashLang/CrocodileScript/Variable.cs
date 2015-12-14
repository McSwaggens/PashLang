using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrocodileScript
{
    public class Variable
    {
        public Function function;
        public bool inFunction { get { return function != null; } }
        public bool isPublic = false, isStatic = false;
        public VariableType type = VariableType.VOID;
        public string RawUnassignedValue;
        public bool WasAssignedValue { get { return RawUnassignedValue != null; } }

        public string Name;
        private static int _id = -1;
        public int ID = ++_id;

        public Variable(string Name, Function f)
        {
            this.Name = Name;
            function = f;
            ID = f.Variables.Count + 1;
            //The caller of this object needs to add this object to the functions variable stack...
        }

        public Variable(string Name, bool isPublic = false, bool isStatic = false)
        {
            this.Name = Name;
            this.isPublic = isPublic;
            this.isStatic = isStatic;
            //TODO: Set the ID to the statics top ID
        }

        public string Tag
        {
            get { return inFunction ? ":" + ID : ID + ""; }
        }
    }
}
