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
        private Function[] Functions = new Function[101];
        public Memory memory;
        public Register register;
        private List<FunctionInstance> Returns = new List<FunctionInstance>();
		private List<Type> ReferencedLibraries = new List<Type> ();

		private Handler[] Code; 
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

        public void createSetMemory()
        {
            Memory memory = new Memory(1024);
            this.memory = memory;
        }

        public void createSetMemory(int sizemb)
        {
            short size = ((short)((sizemb / 1024) / 1024));
            memory = new Memory(size);
        }

        public void setMemory(Memory memory)
        {
            this.memory = memory;
        }

        public void malloc(int register, int size)
        {
            this.register[register].address = memory.Allocate(size);
            this.register[register].size = size;
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

        public void set(int ptr, bool isMethodPtr, short set) //INT16
        {
            if (register[ptr] == null)
            {
                malloc(ptr, 2);
                memory.write(Converter.int16(set), (isMethodPtr ? Returns.Last().register[ptr].address : register[ptr].address));
            }
            else
            {
                memory.write(Converter.int16(set), (isMethodPtr ? Returns.Last().register[ptr].address : register[ptr].address));
            }
        }

        public void set(int ptr, bool isMethodPtr, int set) //INT32
        {
            if (register[ptr] == null)
            {
                malloc(ptr, 4);
                memory.write(Converter.int32(set), (isMethodPtr ? Returns.Last().register[ptr].address : register[ptr].address));
            }
            else
            {
                memory.write(Converter.int32(set), (isMethodPtr ? Returns.Last().register[ptr].address : register[ptr].address));
            }
        }

        public void set(int ptr, bool isMethodPtr, long set) //INT64
        {
            if (register[ptr] == null)
            {
                malloc(ptr, 8);
                memory.write(Converter.int64(set), (isMethodPtr ? Returns.Last().register[ptr].address : register[ptr].address));
            }
            else
            {
                memory.write(Converter.int64(set), (isMethodPtr ? Returns.Last().register[ptr].address : register[ptr].address));
            }
        }

        public void set(int ptr, bool isMethodPtr, byte[] set)
        {
            
        }

        private static bool isMethodPointer(string sptr, out int ptr)
        {
            if (sptr.ToCharArray()[0] == ':')
            {
                ptr = int.Parse(sptr.Remove(0, 1));
                return true;
            }
            else
            {
                ptr = int.Parse(sptr);
                return false;
            }
        }

        private Dictionary<string, Type> StaticCache = new Dictionary<string, Type>();
        
        public void ReferenceLibrary(Type t)
        {
			ReferencedLibraries.Add (t);
        }

		public void ImportLibrary(Type t)
		{
			StaticCache.Add(t.Name, t);
		}

        private short ResolveINT16(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 2);
            return BitConverter.ToInt16(data, 0);
        }

        private short ResolveINT32(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt16(data, 0);
        }

        private short ResolveINT64(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt16(data, 0);
        }

        private object ResolveNumber (string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, register[reg].size);
            switch (register[reg].size)
            {
                case 2: return BitConverter.ToInt16(data, 0);
                case 4: return BitConverter.ToInt32(data, 0);
                case 8: return BitConverter.ToInt64(data, 0);
            }
            throw new Exception("Unknown number length of " + register[reg].size + " bytes, 16bits(2bytes), 32bits(4bytes), 64bits(8bytes)");
        }

        private PASM.Register.Pointer ResolvePointer(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            return isMethod ? Returns.Last().register[reg] : register[reg];
        }

        private byte[] ResolveData(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            return memory.read(register[reg].address, register[reg].size);
        }

        public byte[] CallStaticMethod(string @class, string method, object[] perams)
        {
            foreach (var i in StaticCache)
                if (i.Key == @class) Convert.ToString(i.Value.GetMethod(method, BindingFlags.Static | BindingFlags.Public).Invoke(i.Value, perams));
            return null;
        }

        private MathParser mathParser = new MathParser();

        public class Handler
        {

			public static List<Type> Handlers = new List<Type>() { typeof(mv), typeof(cl), typeof(re), typeof(ca), typeof(@if), typeof(im) };

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
            if (args[2] == "MATH") return new st_MATH(args, this);
            if (args[2] == "VOR") return new st_VOR(args, this);
            if (args[2] == "VOP") return new st_VOP(args, this);
            if (args[2] == "VORL") return new st_VORL(args, this);
            return null;
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
                int reg;
                bool isMethod = isMethodPointer(ptr, out reg);
                inst.set(reg, isMethod, set);
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
                    parts[i] = "" + inst.ResolveNumber(parts[i]);
                }
                
                equ = "";
                for (int i = 0; i < parts.Length; i++) { equ += parts[i]; if (i < ops.Length) equ += ops[i]; }

                int reg;
                bool isMethod = isMethodPointer(ptr, out reg);
                inst.set(reg, isMethod, (int)inst.mathParser.Parse(equ));
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

        public class MAR : Handler
        {
            string[] parts;
            public MAR(string[] args, Engine inst) : base(args, inst)
            {
                parts = args;
            }

            public override void Execute()
            {
                //if (parts[1] == "EXP")
                //{
                //    inst.Returns.Last().
                //}
                //else if (parts[2] == "SET")
                //{
                //    inst.Returns.Last().
                //}
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
                        func.register[g] = inst.ResolvePointer(v[g]);
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
            string sptr;
            string target;
            public st_VOP(string[] args, Engine inst) : base(args, inst)
            {
                sptr = args[1];
                target = args[3];
            }

            public override void Execute()
            {
                int Optr;
                int Tptr;
                bool OisMethod = isMethodPointer(sptr, out Optr);
                bool TisMethod = isMethodPointer(sptr, out Tptr);
                if (OisMethod) inst.register[Optr] = (TisMethod ? inst.Returns.Last().register[Tptr] : inst.register[Tptr]);
                else inst.Returns.Last().register[Optr] = (TisMethod ? inst.Returns.Last().register[Tptr] : inst.register[Tptr]);
            }
        }

        public class st_VORL : Handler
        {
            int ptr;
            bool isMethod;
            string Class, MethodName;
            string[] args;
            public st_VORL(string[] args, Engine inst) : base(args, inst)
            {
                isMethod = isMethodPointer(args[1], out ptr);
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
                    Params[i] = inst.ResolveData(v[i]);
                }
                inst.set(ptr, isMethod, inst.CallStaticMethod(args[3], args[4].Trim(':'), Params));
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
                byte[][] Params;
                List<string> b = args.ToList();
                b.RemoveRange(0, 3);
                string[] s = b.ToArray();
                Params = new byte[s.Length][];
                for (int i = 0; i < s.Length; i++)
                {
                    Params[i] = inst.ResolveData(s[i]);
                }
                inst.CallStaticMethod(Class, MethodName, Params);
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
                            inst.Returns[inst.Returns.Count - 2].register[func.ReturnVariablePos] = inst.ResolvePointer(args[1]);
                        else inst.register[func.ReturnVariablePos] = inst.ResolvePointer(args[1]);
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
                        func.register[g] = inst.ResolvePointer(v[g]);
                    }
                }
                inst.Returns.Add(func);
            }
        }

        public class @if : Handler
        {
            string arg1, arg2;
            string Operator;
            int jumpln;
            public @if(string[] args, Engine inst) : base(args, inst)
            {
                jumpln = inst.Functions[int.Parse(args[4])].Line;
                Operator = args[2];
                arg1 = args[1];
                arg2 = args[3];
            }

            public override void Execute()
            {
                //Compare the 2 arguements and jump to the next line if true...
                dynamic a1 = (dynamic)inst.ResolveNumber(arg1); //
                                                                //  Dynamic a1 and a2 will be resolved at runtime to be either 16, 32 or 64 bit integers.
                dynamic a2 = (dynamic)inst.ResolveNumber(arg2); //
                bool ReturnedValue = false;

                if (Operator == "=" && a1 == a2) ReturnedValue = true; else
                if (Operator == "!=" && a1 != a2) ReturnedValue = true; else
                if (Operator == ">" && a1 > a2) ReturnedValue = true; else
                if (Operator == ">=" && a1 >= a2) ReturnedValue = true; else
                if (Operator == "<" && a1 < a2) ReturnedValue = true; else
                if (Operator == "<=" && a1 <= a2) ReturnedValue = true;

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
        public bool MethodVariable = false; // Does the pointer have a : ?
        public int ReturnVariablePos; // Variable to set Location
        public Register register = new Register(1);
    }
}
