using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PASM
{
    public class Engine
    {

        /*
            ideas:
                Memory Cleanup:
                  Make a boolean flag for manual and onsight memory cleanup
                  Manual meaning the host of the PASM engine will trigger the cleanup stack (Stack being the cached Register)
                  Onsight is what we have at the moment, where the engine does cleanup at the end of every method (re)
                
        */

        private int[] points;
        public Memory memory;
        public Register register = new Register(10);
        private List<FunctionInstance> Returns = new List<FunctionInstance>();
		private List<Type> ReferencedLibraries = new List<Type> ();
        
        public int CurrentLine = 0;
        
		private Handler[] Code;
        
        private static char[] MathCharacters = { '+', '-', '*', '/', '%' };
        
        public void Load(string[][] code)
        {
            foreach (string[] c in code) Load(c);
        }

        public void Load(string[] Code)
        {
            List<string> s = Code.ToList(); s.RemoveAll(str => string.IsNullOrEmpty(str));
            
            Code = s.ToArray();

            if (this.Code == null) this.Code = new Handler[s.Count];
            else
            {
                Handler[] prev = this.Code;
                this.Code = new Handler[prev.Length + s.Count];
                for (int i = 0; i < prev.Length; i++) this.Code[i] = prev[i];
            }

            Dictionary<int, int> points = new Dictionary<int, int>();
            int maxPointNum = 0;

            for (int i = 0; i < Code.Length; i++)
            {
                string st = Code[i];
                if (st.StartsWith("pt"))
                {
                    string[] args = Code[i].Split(' ');
                    int c = ParseStringToInt(args[1]);
                    int point = i;
                    points.Add(c, point);
                    if (c > maxPointNum) maxPointNum = c;
                }
            }

            this.points = new int[maxPointNum + 1];
            foreach (KeyValuePair<int, int> pair in points)
            {
                this.points[pair.Key] = pair.Value;
            }

            for (int i = 0; i < Code.Length; i++)
            {
                string st = Code[i];
                if (st.StartsWith("set"))
                    this.Code[i] = st_Parser(st.Split(' '), st);
                else {
                    string[] args = st.Split(' ');
                    string g = args[0];
                    foreach (Type t in Handler.Handlers) if (t.Name == g) this.Code[i] = (Handler)Activator.CreateInstance(t, args, this);
                }
            }
        }

        public bool Loaded => Code != null;

        public void setMemory() => memory = new Memory(1024);

        public void setMemory(int size) => memory = new Memory(size);

        public void setMemory(Memory memory) => this.memory = memory;

        public void malloc(Register register, int ptr, int size)
        {
            Register.Pointer pointer = new Register.Pointer(memory.Allocate(size), size);
            register[ptr] = pointer;
        }
        
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
        
        public void ExecuteDebug() {
            Console.WriteLine("Executing in debug mode, (May not get the best performance.)");
            while (CurrentLine < Code.Length)
            {
                try {
                    Code[CurrentLine].Execute();
                    CurrentLine++;
                }
                catch (PException pe) {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"(ERROR) PASM Exception at line {CurrentLine} while executing {Code[CurrentLine].ToString()} returned Exception Notice: {pe.Message}, Please check code and try again!");
                    Console.ForegroundColor = color;
                    break;
                }
            }
        }

        public void set(int ptr, bool isMethodPtr, short set) //INT16
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            if (register[ptr] == null)
            {
                register[ptr] = new Register.Pointer();
                malloc(register, ptr, 2);
                memory.write(Converter.int16(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.int16(set), register[ptr].address);
            }
        }

        public void set(int ptr, bool isMethodPtr, int set) //INT32
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            if (register[ptr] == null)
            {
                register[ptr] = new Register.Pointer();
                malloc(register, ptr, 4);
                memory.write(Converter.int32(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.int32(set), register[ptr].address);
            }
        }

        public void set(int ptr, bool isMethodPtr, long set) //INT64
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            if (register[ptr] == null)
            {
                register[ptr] = new Register.Pointer();
                malloc(register, ptr, 8);
                memory.write(Converter.int64(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.int64(set), register[ptr].address);
            }
        }

        public void set(int ptr, bool isMethodPtr, byte set)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            if (register[ptr] == null)
            {
                register[ptr] = new Register.Pointer();
                malloc(register, ptr, 1);
                memory.write(new byte[] { set }, register[ptr].address);
            }
            else
            {
                memory.write(new byte[] { set }, register[ptr].address);
            }
        }

        public void set(int ptr, bool isMethodPtr, byte[] set)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            if (register[ptr] == null)
            {
                register[ptr] = new Register.Pointer();
                malloc(register, ptr, set.Length);
                memory.write(set, register[ptr].address);
            }
            else
            {
                memory.write(set, register[ptr].address);
            }
        }

        private static bool isMethodPointer(string sptr, out int ptr)
        {
            if (sptr.ToCharArray()[0] == ':')
            {
                ptr = ParseStringToInt(sptr.Remove(0, 1));
                return true;
            }
            ptr = ParseStringToInt(sptr);
            return false;
        }

        //Free the address if it's not being used by another register.
        public void TryFreeRegister(Register.Pointer p)
        {
            Memory.Part part = memory.PartAddressStack[p.address];
            if (p.ReferenceCount == 0) memory.Free(part.Address);
        }

        //Will free the registers address even if the address is being used by another register, try not to use this tho...
        public void ForceFreeRegister(Register.Pointer p)
        {
            memory.Free(p.address);
        }

        public Register GetRegister(bool isMethod) => isMethod ? Returns.Last().register : register;
        
        

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

        private int ResolveINT32(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt32(data, 0);
        }

        private long ResolveINT64(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            Register register = isMethod ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt64(data, 0);
        }
        //Direct reference
        private short ResolveINT16(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 2);
            return BitConverter.ToInt16(data, 0);
        }

        private int ResolveINT32(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt32(data, 0);
        }

        private long ResolveINT64(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt64(data, 0);
        }

        private object ResolveNumber (string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            Register register = isMethod ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, register[reg].size);
            switch (register[reg].size)
            {
                case 2: return BitConverter.ToInt16(data, 0);
                case 4: return BitConverter.ToInt32(data, 0);
                case 8: return BitConverter.ToInt64(data, 0);
            }
            throw new PException("Unknown number length of " + register[reg].size + " bytes, 16bits(2bytes), 32bits(4bytes), 64bits(8bytes)");
        }

        private Register.Pointer ResolvePointer(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            return isMethod ? Returns.Last().register[reg] : register[reg];
        }

        private byte[] ResolveData(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            Register register = isMethod ? Returns.Last().register : this.register;
            return memory.read(register[reg].address, register[reg].size);
        }

        private static short ParseStringToShort(string s)
        {
            short value = 0;
            for (var i = 0; i < s.Length; i++)
            {
                value = (short) (value * 10 + (s[i] - '0'));
            }
            return value;
        }

        private static int ParseStringToInt(string s)
        {
            int value = 0;
            for (var i = 0; i < s.Length; i++)
            {
                value = value * 10 + (s[i] - '0');
            }
            return value;
        }

        private static long ParseStringToLong(string s)
        {
            long value = 0;
            for (var i = 0; i < s.Length; i++)
            {
                value = value * 10 + (s[i] - '0');
            }
            return value;
        }

        public byte[] CallStaticMethod(string @class, string method, byte[][] Params)
        {
            object[] Objects = new object[Params.Length + 1];
            Objects[0] = this;
            for (int i = 0; i < Params.Length; i++) Objects[i + 1] = Params[i];
            foreach (KeyValuePair<string, Type> i in StaticCache.Where(i => i.Key == @class))
                Convert.ToString(i.Value.GetMethod(method, BindingFlags.Static | BindingFlags.Public).Invoke(i.Value, Objects));
            return null;
        }

        private readonly MathParser mathParser = new MathParser();

        public class Handler
        {

			public static List<Type> Handlers = new List<Type>() { typeof(mov), typeof(free), typeof(calib), typeof(malloc_c), typeof(malloc_d), typeof(malloc_p), typeof(re), typeof(call), typeof(@if), typeof(im) };

            public Engine inst;
            public Handler(Engine inst)
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
            if (args[2] == "BYTE") return new st_INT64(args, this);
            if (args[2] == "INT16") return new st_INT16(args, this);
            if (args[2] == "INT32") return new st_INT32(args, this);
            if (args[2] == "INT64") return new st_INT64(args, this);
            if (args[2] == "MATH") return new st_MATH(args, this);
            if (args[2] == "QMATH") return new st_QMATH(args, this);
            if (args[2] == "VOR") return new st_VOR(args, this);
            if (args[2] == "VOP") return new st_VOP(args, this);
            if (args[2] == "PTR") return new st_PTR(args, this);
            if (args[2] == "VORL") return new st_VORL(args, this);
            throw new PException("Unknown set extension " + args[2]);
        }

        public class st_BYTE : Handler
        {
            byte set;
            string ptr;
            public st_BYTE(string[] args, Engine inst) : base(inst)
            {
                ptr = args[1];
                set = byte.Parse(args[3]);
            }

            public override void Execute()
            {
                int reg;
                bool isMethod = isMethodPointer(ptr, out reg);
                inst.set(reg, isMethod, set);
            }
        }

        public class st_INT16 : Handler
        {
            short set;
            string ptr;
            public st_INT16(string[] args, Engine inst) : base(inst)
            {
                ptr = args[1];
                set = ParseStringToShort(args[3]);
            }

            public override void Execute()
            {
                int reg;
                bool isMethod = isMethodPointer(ptr, out reg);
                inst.set(reg, isMethod, set);
            }
        }

        public class st_INT32 : Handler
        {
            int set;
            string ptr;
            public st_INT32(string[] args, Engine inst) : base(inst)
            {
                ptr = args[1];
                set = ParseStringToInt(args[3]);
            }

            public override void Execute()
            {
                int reg;
                bool isMethod = isMethodPointer(ptr, out reg);
                inst.set(reg, isMethod, set);
            }
        }

        public class st_INT64 : Handler
        {
            long set;
            string ptr;
            public st_INT64(string[] args, Engine inst) : base(inst)
            {
                ptr = args[1];
                set = ParseStringToLong(args[3]);
            }

            public override void Execute()
            {
                int reg;
                bool isMethod = isMethodPointer(ptr, out reg);
                inst.set(reg, isMethod, set);
            }
        }

        public class st_QMATH : Handler
        {
            private string ptr;
            private string Equasion;
            private int sizeSpace = 4;
            public st_QMATH(string[] args, Engine inst) : base(inst)
            {
                ptr = args[1];
                sizeSpace = ParseStringToInt(args[3]);
                for (int i = 4; i < args.Length; i++) Equasion += args[i];
            }

            public override void Execute()
            {
                object arg1;
                object arg2;
                char Operator;
                SeperateEquasion(Equasion, out arg1, out arg2, out Operator);

                int reg;
                bool isMethod = isMethodPointer(ptr, out reg);

                if (sizeSpace == 2)
                {
                    short result = 0;
                    short a1 = arg1 as short? ?? Convert.ToInt16(arg1);
                    short a2 = arg2 as short? ?? Convert.ToInt16(arg2);

                    if (Operator == '+') result = (short)(a1 + a2);
                    else
                    if (Operator == '-') result = (short)(a1 - a2);
                    else
                    if (Operator == '*') result = (short)(a1 * a2);
                    else
                    if (Operator == '/') result = (short)(a1 / a2);
                    else throw new PException("Unknown QMATH operator: " + Operator);

                    inst.set(reg, isMethod, result);
                }
                else if (sizeSpace == 4)
                {
                    int result = 0;
                    int a1 = arg1 as int? ?? Convert.ToInt32(arg1);
                    int a2 = arg2 as int? ?? Convert.ToInt32(arg2);

                    if (Operator == '+') result = a1 + a2;
                    else
                    if (Operator == '-') result = a1 - a2;
                    else
                    if (Operator == '*') result = a1 * a2;
                    else
                    if (Operator == '/') result = a1 / a2;
                    else throw new PException("Unknown QMATH operator: " + Operator);

                    inst.set(reg, isMethod, result);
                }
                else if (sizeSpace == 8)
                {
                    long result = 0;
                    //Convert arg1 or arg2 into the expected long type
                    long a1 = arg1 as long? ?? Convert.ToInt64(arg1);
                    long a2 = arg2 as long? ?? Convert.ToInt64(arg2);

                    if (Operator == '+') result = a1 + a2;
                    else
                    if (Operator == '-') result = a1 - a2;
                    else
                    if (Operator == '*') result = a1 * a2;
                    else
                    if (Operator == '/') result = a1 / a2;
                    else throw new PException("Unknown QMATH operator: " + Operator);

                    inst.set(reg, isMethod, result);
                }
            }

            public void SeperateEquasion(string equasion, out object arg1, out object arg2, out char Operator)
            {
                arg1 = null;
                Operator = '*';
                char[] carr = equasion.ToCharArray();
                string current = "";
                foreach (char c in carr)
                {
                    if (isMathCharacter(c))
                    {
                        arg1 = inst.ResolveNumber(current);
                        Operator = c;
                        current = "";
                    }
                    else current += c;
                }
                arg2 = inst.ResolveNumber(current);
            }

            //foreach is faster than LINQ sadly :(
            public bool isMathCharacter(char op)
            {
                foreach (char c in MathCharacters) if (op == c) return true;
                return false;
            }
        }

        public class st_MATH : Handler
        {
            private string Equasion;
            string ptr;
            char[] ops;
            string[] parts;
            public st_MATH(string[] args, Engine inst) : base(inst)
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
                foreach (char c in  MathCharacters) foreach (char e in equ) if (e == c) return true;
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
                    foreach (char o in MathCharacters.Where(o => o == c))
                    {
                        ret.Add(cbuild); ops.Add(o); cbuild = "";
                    }
                }
                ret.Add(cbuild);
                operators = ops.ToArray();
                return ret.ToArray();
            }

            public char[] MathCharacters = { '+', '-', '*', '/', '%' };
        }

        public class st_VOR : Handler
        {
            string ptr;
            int method;
            string[] args;
            public st_VOR(string[] args, Engine inst) : base(inst)
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
                    p = ParseStringToInt(args[1].Substring(1));
                    func.MethodVariable = true;
                }
                else p = ParseStringToInt(args[1]);


                func.ReturnVariablePos = p;

                inst.Returns.Add(func);
                inst.CurrentLine = inst.points[ParseStringToInt(args[3])];
            }
        }

        public class st_PTR : Handler
        {
            string working;
            string setter;
            public st_PTR(string[] args, Engine inst) : base(inst)
            {
                working = args[1];
                setter = args[3];
            }

            public override unsafe void Execute()
            {
                int workerPtr;
                Register workerRegister = inst.GetRegister(isMethodPointer(working, out workerPtr));

                int setterPtr;
                Register setterRegister = inst.GetRegister(isMethodPointer(setter, out setterPtr));
                
                workerRegister[workerPtr] = setterRegister[setterPtr];
                setterRegister[setterPtr].ReferenceCount++;
            }
        }

        public class st_VORL : Handler
        {
            int ptr;
            bool isMethod;
            string[] args;
            public st_VORL(string[] args, Engine inst) : base(inst)
            {
                isMethod = isMethodPointer(args[1], out ptr);
                this.args = args;
            }

            public override void Execute()
            {
                byte[][] Params;
                List<string> v = args.ToList();
                v.RemoveRange(0, 5);
                Params = new byte[v.Count][];
                for (int i = 0; i < v.Count; i++)
                {
                    Params[i] = inst.ResolveData(v[i]);
                }
                inst.set(ptr, isMethod, inst.CallStaticMethod(args[3], args[4].Trim(':'), Params));
            }
        }

        public class st_VOP : Handler
        {
            private string worker, setter;
            public st_VOP(string[] args, Engine engine) : base(engine)
            {
                worker = args[1];
                setter = args[3];
            }

            public override void Execute()
            {
                int setterPtr;
                bool isMethodPtr_Setter = isMethodPointer(worker, out setterPtr);

                inst.set(setterPtr, isMethodPtr_Setter, inst.ResolveData(setter));
            }
        }

        #endregion

        //malloc copy
        public class malloc_c : Handler
        {
            string workingPointer;
            string setterPointer;
            public malloc_c(string[] args, Engine inst) : base(inst)
            {
                workingPointer = args[1];
                setterPointer = args[2];
            }

            public override void Execute()
            {
                int workPtr;
                bool isMethodWorkingPtr = isMethodPointer(workingPointer, out workPtr);

                int setterPtr;
                bool isMethodSetterPtr = isMethodPointer(setterPointer, out setterPtr);

                inst.malloc(inst.GetRegister(isMethodWorkingPtr), workPtr, inst.ResolveData(setterPointer).Length);
            }
        }

        //malloc direct (set the amount of bytes static)
        public class malloc_d : Handler {
            string ts_ptr;
            int AllocationSize;
            public malloc_d(string[] args, Engine inst) : base (inst) {
                ts_ptr = args[1];
                AllocationSize = int.Parse(args[2]);
            }
            
            public override void Execute() {
                int ptr;
                bool isMethodPtr = isMethodPointer(ts_ptr, out ptr);
                inst.malloc(inst.GetRegister(isMethodPtr),ptr,AllocationSize);
            }
        }
        
        //malloc pointer
        public class malloc_p : Handler {
            string ts_ptr;
            int set_ptr;
            
            bool isMethodPtr;
            public malloc_p(string[] args, Engine inst) : base (inst) {
                ts_ptr = args[1];
                isMethodPtr = isMethodPointer(ts_ptr, out set_ptr);
            }
            
            public override void Execute() {
                int ptr;
                bool isMethodPtr = isMethodPointer(ts_ptr, out ptr);
                inst.malloc(inst.GetRegister(isMethodPtr),ptr,inst.ResolveINT32(isMethodPtr,ptr));
            }
        }

        public class calib : Handler
        {
            string Class, MethodName;
            string[] args;
            public calib(string[] args, Engine inst) : base(inst)
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
        
        public class free : Handler {
            public string tf;
            
            public free (string[] args, Engine inst) : base (inst) {
                tf = args[1];
            }
            
            public override void Execute() {
                int ptr;
                bool isMethodPtr = isMethodPointer(tf, out ptr);
                PASM.Register.Pointer pointer = isMethodPtr ? inst.Returns.Last().register.Stack[ptr] : inst.register.Stack[ptr];
                inst.ForceFreeRegister(pointer);
                pointer = null;
            }
        }

		public class im : Handler 
		{
			public string lib;
			public im(string[] args, Engine inst) : base (inst) {
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

        public class mov : Handler
        {
            public int Line;
            public mov(string[] args, Engine inst) : base(inst)
            {
                Line = inst.points[ParseStringToInt(args[1])];
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
            public re(string[] args, Engine inst) : base(inst)
            {
                this.args = args;
            }

            public override void Execute()
            {
                foreach (Register.Pointer p in inst.Returns.Last().register.Stack.Where(p => p != null))
                    inst.TryFreeRegister(p);
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

        public class call : Handler
        {
            string[] args;
            public call(string[] args, Engine inst) : base(inst)
            {
                this.args = args;
            }

            public override void Execute()
            {
                FunctionInstance func = new FunctionInstance();
                func.doesReturnValue = false;
                func.ReturnLine = inst.CurrentLine;
                inst.CurrentLine = inst.points[ParseStringToInt(args[1])];

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
            public @if(string[] args, Engine inst) : base(inst)
            {
                jumpln = inst.points[ParseStringToInt(args[4])];
                Operator = args[2];
                arg1 = args[1];
                arg2 = args[3];
            }

            public override void Execute()
            {

                //TODO: Need to move away from dynamic, slow af
                //Compare the 2 arguements and jump to the next line if true...
                dynamic a1 = inst.ResolveNumber(arg1); //
                                                       //  Dynamic a1 and a2 will be resolved at runtime to be either 16, 32 or 64 bit integers.
                dynamic a2 = inst.ResolveNumber(arg2); //
                bool ReturnedValue = false;
                //TODO: Make this faster
                if (Operator == "=" && a1 == a2) ReturnedValue = true; else
                if (Operator == "!=" && a1 != a2) ReturnedValue = true; else
                if (Operator == ">" && a1 > a2) ReturnedValue = true; else
                if (Operator == ">=" && a1 >= a2) ReturnedValue = true; else
                if (Operator == "<" && a1 < a2) ReturnedValue = true; else
                if (Operator == "<=" && a1 <= a2) ReturnedValue = true;
	            else throw new PException ($"Unknown comparison operator: {Operator}"); 
                if (!ReturnedValue) inst.CurrentLine = jumpln;
            }
        }

    }
    
    public class FunctionInstance
    {
        public bool doesReturnValue = true;
        public int ReturnLine;
        public bool MethodVariable = false; // Does the pointer have a : ?
        public int ReturnVariablePos; // Variable to set Location
        public Register register = new Register(10);
    }
    
    public class PException : Exception {
        public PException (string exception) : base (exception) {
            
        }
    }
}
