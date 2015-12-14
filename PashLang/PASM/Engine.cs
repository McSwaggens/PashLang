using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PASM
{
    public class Engine
    {
        public object[] MEM = new object[100];
        public Function[] Functions = new Function[101];

        private List<FunctionInstance> Returns = new List<FunctionInstance>();
		private List<Type> ReferencedLibraries = new List<Type> ();

		public Handler[] Code;
        public void Load(string[] Code)
        {
            List<string> s = Code.ToList(); s.RemoveAll(str => String.IsNullOrEmpty(str));
            
            Code = s.ToArray();

            this.Code = new Handler[s.Count];

            for (int i = 0; i < Code.Length; i++)
            {
                string st = Code[i];
                if (st.StartsWith("me"))
                {
                    string[] args = Code[i].Split(' ');
                    int c = int.Parse(args[1]);
                    Function func = new Function();
                    func.Line = i;
                    Functions[c] = func;
                    this.Code[i] = new me(args, this);
                }
            }

            for (int i = 0; i < Code.Length; i++)
            {
                string st = Code[i];
                if (st.StartsWith("st"))
                {
                    this.Code[i] = st_Parser(st.Split(' '), st);
                }
                else if (st.StartsWith("if"))
                {
                    this.Code[i] = new If(st.Split(' '), this);
                }
                else {
                    string[] args = st.Split(' ');
                    string g = args[0];
                    foreach (Type t in Handler.Handlers)
                    {
                        if (t.Name == g)
                        {
                            this.Code[i] = (Handler)Activator.CreateInstance(t, args, this);
                        }
                    }
                }
            }
        }

        public bool Loaded
        {
            get { return Code != null; }
        }

        public int CurrentLine = 0;

        public void Execute()
        {
            CurrentLine = 0;
            while (CurrentLine < Code.Length)
            {
                if (Code[CurrentLine] != null)
                    Code[CurrentLine].Execute();
                CurrentLine++;
            }
        }

        public void Execute(int startingln)
        {
            CurrentLine = startingln;
            while (CurrentLine < Code.Length)
            {
                Code[CurrentLine].Execute();
                CurrentLine++;
            }
        }

        public bool isMethodVariable(string var)
        {
            return var.ToCharArray()[0] == ':';
        }

        public void set(int ptr, bool isMethodPtr, object set)
        {
            if (isMethodPtr)
            {
                Returns.Last().MEM[ptr] = set;
            }
            else MEM[ptr] = set;
        }

        public void set(string ptr, object set)
        {
            char c = ptr.ToCharArray()[0];
            bool isMethodPtr = c == ':';

            if (isMethodPtr)
            {
                Returns.Last().MEM[int.Parse(ptr.Substring(1))] = set;
            }
            else MEM[int.Parse(ptr)] = set;
        }

        private Dictionary<string, Type> DotNetCash = new Dictionary<string, Type>();
        
        public void ReferenceLibrary(Type t)
        {
			ReferencedLibraries.Add (t);
        }

		public void ImportLibrary(Type t)
		{
			DotNetCash.Add(t.Name, t);
		}

        private object ResolveValue(string val)
        {
            char c = val.ToCharArray()[0];
            if (c == ':')
            {
                return Returns.Last().MEM[int.Parse(val.Remove(0, 1))];
            }
            else return MEM[int.Parse(val)];
        }

        public object CallDotNet(string myclass1, string mymethod, object[] perams)
        {
            foreach (var i in DotNetCash)
            {
                if (i.Key == myclass1)
                {
                    object rett =
                        i.Value.GetMethod(
                            mymethod, BindingFlags.Static | BindingFlags.Public).Invoke(
                                i.Value, perams);
                    return Convert.ToString(rett);
                }
            }
            return null;
        }

        private MathParser mathParser = new MathParser();

        private object MATH_Parse(string expression)
        {
            return mathParser.Parse(expression);
        }

        public class Handler
        {

			public static List<Type> Handlers = new List<Type>() { typeof(mv), typeof(cl), typeof(re), typeof(ca), typeof(If), typeof(im) };

            public Engine inst;
            public Handler(string[] args, Engine inst)
            {
                this.inst = inst;
            }

            public virtual void Execute()
            {
            }
        }

        #region st

        private Handler st_Parser(string[] args, string ln)
        {
            if (args[2] == "INT") return new st_INT(args, this);
			if (args[2] == "BOOL") return new st_BOOL(args, this);
            if (args[2] == "STR") return new st_STR(args, this, ln);
            if (args[2] == "MATH") return new st_MATH(args, this);
            if (args[2] == "STRAD") return new st_STRAD(args, this);
            if (args[2] == "VOR") return new st_VOR(args, this);
            if (args[2] == "VOP") return new st_VOP(args, this);
            if (args[2] == "VORL") return new st_VORL(args, this);
            return null;
        }

        public class st_STR : Handler
        {
            public string set;
            public string ptr;
            public st_STR(string[] args, Engine inst, string ln) : base(args, inst)
            {
                ptr = args[1];
                set = ln.Split('"')[1];
            }

            public override void Execute()
            {
                inst.set(ptr, set);
            }
        }

		public class st_BOOL : Handler
		{
			public bool toset = false;
			public string ptr;
			public st_BOOL(string[] args, Engine inst) : base(args, inst)
			{
				ptr = args[1];
				if (args[3] ==  "true") toset = true;
				else if (args[3] == "false") toset = false;
				else throw new Exception("Unknown bool set " + args[3]);
			}

			public override void Execute()
			{
				inst.set(ptr, toset);
			}
		}

        public class st_INT : Handler
        {
            int set;
            string ptr;
            public st_INT(string[] args, Engine inst) : base(args, inst)
            {
                ptr = args[1];
                set = int.Parse(args[3]);
            }

            public override void Execute()
            {
                inst.set(ptr, set);
            }
        }

        public class st_MATH : Handler
        {
            private string Equasion;
            string ptr;
            char[] ops;
            string[] parts;
            public st_MATH(string[] args, Engine inst) : base(args, inst)
            {
                ptr = args[1];
                for (int i = 3; i < args.Length; i++) Equasion += args[i];
                parts = mSep(Equasion, out ops);
            }

            public override void Execute()
            {
                string equ = Equasion;
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = "" + inst.ResolveValue(parts[i]);
                }
                
                equ = "";
                for (int i = 0; i < parts.Length; i++) { equ += parts[i]; if (i < ops.Length) equ += ops[i]; }

                inst.set(ptr, inst.mathParser.Parse(equ));
            }

            public bool doesContainMathOperator(string equ)
            {
                foreach (char op in MathCharacters) if (equ.Contains(op)) return true;
                return false;
            }

            public string[] mSep(string sep, out char[] operators)
            {
                List<string> ret = new List<string>();
                List<char> ops = new List<char>();
                char[] s = sep.ToCharArray();
                string cbuild = "";
                for (int i = 0; i < s.Length; i++)
                {
                    char c = s[i];
                    int r;
                    if (int.TryParse("" + c, out r) || c == ':') cbuild += c;
                    foreach (char o in MathCharacters) if (o == c) { ret.Add(cbuild); ops.Add(o); cbuild = ""; }
                }
                ret.Add(cbuild);
                operators = ops.ToArray();
                return ret.ToArray();
            }

            public char[] MathCharacters = new char[] { '+', '-', '*', '/', '%' };
        }

        public class st_STRAD : Handler
        {
            string ptr;
            string[] parts;
            public st_STRAD(string[] args, Engine inst) : base(args, inst)
            {
                parts = new string[args.Length - 3];
                for (int i = 0; i < parts.Length; i++)
                {
                    parts[i] = args[3 + i];
                }

                ptr = args[1];
            }

            public override void Execute()
            {
                string Const = "";
                for (int i = 0; i < parts.Length; i++)
                {
                    Const += inst.ResolveValue(parts[i]);
                }
                inst.set(ptr, Const);
            }
        }

        public class st_VOR : Handler
        {
            string ptr;
            int method;
            string[] args;
            public st_VOR(string[] args, Engine inst) : base(args, inst)
            {
                ptr = args[1];
                this.args = args;
            }

            public override void Execute()
            {
                FunctionInstance func = new FunctionInstance();
                func.ReturnLine = inst.CurrentLine;
                if (args.Length > 3)
                {
                    List<string> v = args.ToList();
                    v.RemoveRange(0, 4);
                    for (int g = 0; g < v.Count; g++)
                    {
                        func.MEM[g] = inst.ResolveValue(v[g]);
                    }
                }

                int p;
                if (args[1].ToCharArray()[0] == ':')
                {
                    p = int.Parse(args[1].Substring(1));
                    func.MethodVariable = true;
                }
                else p = int.Parse(args[1]);


                func.ReturnVariablePos = p;

                inst.Returns.Add(func);
                inst.CurrentLine = inst.Functions[int.Parse(args[3])].Line;
            }
        }

        public class st_VOP : Handler
        {
            string ptr;
            string target;
            public st_VOP(string[] args, Engine inst) : base(args, inst)
            {
                ptr = args[1];
                target = args[3];
            }

            public override void Execute()
            {
                inst.set(ptr, inst.ResolveValue(target));
            }
        }

        public class st_VORL : Handler
        {
            string ptr;
            string Class, MethodName;
            string[] args;
            public st_VORL(string[] args, Engine inst) : base(args, inst)
            {
                ptr = args[1];
                this.args = args;
            }

            public override void Execute()
            {
                object[] Params;
                List<string> v = args.ToList();
                v.RemoveRange(0, 5);
                Params = new object[v.Count];
                for (int i = 0; i < v.Count; i++)
                {
                    Params[i] = inst.ResolveValue(v[i]);
                }
                inst.set(ptr, inst.CallDotNet(args[3], args[4].Trim(':'), Params));
                return;
            }
        }

        #endregion


        public class cl : Handler
        {
            string Class, MethodName;
            string[] args;
            public cl(string[] args, Engine inst) : base(args, inst)
            {
                this.args = args;
                Class = args[1];
                MethodName = args[2];
            }

            public override void Execute()
            {
                object[] Params;
                List<string> b = args.ToList();
                b.RemoveRange(0, 3);
                string[] s = b.ToArray();
                Params = new object[s.Length];
                for (int i = 0; i < s.Length; i++)
                {
                    Params[i] = inst.ResolveValue(s[i]);
                }
                inst.CallDotNet(Class, MethodName, Params);
            }
        }

		public class im : Handler 
		{
			public string lib;
			public im(string[] args, Engine inst) : base (args, inst) {
				lib = args[1];
			}

			public override void Execute() {
				foreach (Type t in inst.ReferencedLibraries)
					if (t.Name == lib) {
						inst.ImportLibrary (t);
						return;
					}
			}
		}

        public class mv : Handler
        {
            public int Line;
            public mv(string[] args, Engine inst) : base(args, inst)
            {
                Line = inst.Functions[int.Parse(args[1])].Line;
            }

            public override void Execute()
            {
                inst.CurrentLine = Line;
            }
        }

        public class re : Handler
        {
            string returnval;
            string[] args;
            public re(string[] args, Engine inst) : base(args, inst)
            {
                this.args = args;
            }

            public override void Execute()
            {
                if (args.Length > 0)
                {
                    FunctionInstance func = inst.Returns.Last();
                    if (func.doesReturnValue)
                    {
                        if (func.MethodVariable)
                            inst.Returns[inst.Returns.Count - 2].MEM[func.ReturnVariablePos] = inst.ResolveValue(args[1]);
                        else inst.MEM[func.ReturnVariablePos] = inst.ResolveValue(args[1]);
                    }
                }
                inst.CurrentLine = inst.Returns.Last().ReturnLine;
                inst.Returns.Remove(inst.Returns.Last());
            }
        }

        public class me : Handler
        {
            public me(string[] args, Engine inst) : base(args, inst)
            {
                //Do nothing...
            }
        }

        public class ca : Handler
        {
            string Func;
            string[] args;
            public ca(string[] args, Engine inst) : base(args, inst)
            {
                this.args = args;
            }

            public override void Execute()
            {
                FunctionInstance func = new FunctionInstance();
                func.doesReturnValue = false;
                func.ReturnLine = inst.CurrentLine;
                inst.CurrentLine = inst.Functions[int.Parse(args[1])].Line;

                if (args.Length > 1)
                {
                    List<string> v = args.ToList();
                    v.RemoveRange(0, 2);
                    for (int g = 0; g < v.Count; g++)
                    {
                        func.MEM[g] = inst.ResolveValue(v[g]);
                    }
                }
                inst.Returns.Add(func);
            }
        }

        public class If : Handler
        {
            string arg1, arg2;
            string Operator;
            int jumpln;
            public If(string[] args, Engine inst) : base(args, inst)
            {
                jumpln = inst.Functions[int.Parse(args[4])].Line;
                Operator = args[2];
                arg1 = args[1];
                arg2 = args[3];
            }

            public override void Execute()
            {
                bool ReturnedValue = false;
                if (Operator == "=")
                {
                    if (inst.ResolveValue(arg1).Equals(inst.ResolveValue(arg2))) ReturnedValue = true;
                }
                else
                if (Operator == "!=")
                {
                    if (inst.ResolveValue(arg1).Equals(inst.ResolveValue(arg2))) ReturnedValue = false;
                }
                else
                if (Operator == ">")
                {
                    if (Convert.ToInt32(inst.ResolveValue(arg1)) > (int)(inst.ResolveValue(arg2))) ReturnedValue = true;
                }
                else
                if (Operator == ">=")
                {
                    if (Convert.ToInt32(inst.ResolveValue(arg1)) >= (int)(inst.ResolveValue(arg2))) ReturnedValue = true;
                }
                else
                if (Operator == "<")
                {
                    if (Convert.ToInt32(inst.ResolveValue(arg1)) < (int)(inst.ResolveValue(arg2))) ReturnedValue = true;
                }
                else
                if (Operator == "<=")
                {
                    if (Convert.ToInt32(inst.ResolveValue(arg1)) <= (int)(inst.ResolveValue(arg2))) ReturnedValue = true;
                }

                if (!ReturnedValue) inst.CurrentLine = jumpln;
            }
        }

    }

    public class Function
    {
        public int Line;
    }
    
    public class FunctionInstance
    {
        public bool doesReturnValue = true;
        public int ReturnLine;
        public object[] MEM = new object[100];
        public bool MethodVariable = false; // Does the pointer have a : ?
        public int ReturnVariablePos; // Variable to set Location
    }
}
