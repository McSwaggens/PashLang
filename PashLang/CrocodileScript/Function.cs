using System.Collections.Generic;

namespace CrocodileScript
{
    public class Function
    {
        private static int _id = -1;
        public int ID = ++_id;
        public string Name;
        public bool isStatic = false;
        public bool isPublic = false;

        public Function(string Name, bool Public = false, bool Static = false)
        {
            this.Name = Name;
            isPublic = Public;
            isStatic = Static;
        }

        //TODO: Params with types.

        public List<Variable> Variables = new List<Variable>();
        public Block block;
    }
}
