using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using PASM.Handlers;
using static PASM.Extended;
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

        public int[] points;
        public Memory memory;
        public Register register = new Register(10);
        public  List<FunctionInstance> Returns = new List<FunctionInstance>();
		public List<Type> ReferencedLibraries = new List<Type> ();
        private Dictionary<string, Type> StaticCache = new Dictionary<string, Type>();

        public int CurrentLine = 0;
        
		private Handler[] Code;
        
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
                    int c = Converter.ParseStringToInt(args[1]);
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
                    this.Code[i] = set_Parser(st.Split(' '), st);
                else {
                    string[] args = st.Split(' ');
                    string g = args[0];
                    foreach (Type t in Handler.Handlers) if (t.Name == g) this.Code[i] = (Handler)Activator.CreateInstance(t, args, this);
                }
            }
        }

        private Handler set_Parser(string[] args, string ln)
        {
            if (args[2] == "QMATH") return new st_QMATH(args, this);
            if (args[2] == "INT32") return new st_INT32(args, this);
            if (args[2] == "BYTE") return new st_INT64(args, this);
            if (args[2] == "INT16") return new st_INT16(args, this);
            if (args[2] == "INT64") return new st_INT64(args, this);
            if (args[2] == "PAR") return new set_PAR(args, this);
            if (args[2] == "PARC") return new set_PAR(args, this);
            if (args[2] == "VOR") return new st_VOR(args, this);
            if (args[2] == "VOP") return new st_VOP(args, this);
            if (args[2] == "PTR") return new st_PTR(args, this);
            if (args[2] == "VORL") return new st_VORL(args, this);
            throw new PException("Unknown set extension " + args[2]);
        }

        public void ReferenceLibrary(params Type[] t)
        {
            foreach (Type type in t)
                ReferencedLibraries.Add(type);
        }

        public void ImportLibrary(Type t)
        {
            StaticCache.Add(t.Name, t);
        }

        public bool Loaded => Code != null;

        public void setMemory() => memory = new Memory(1024);

        public void setMemory(int size) => memory = new Memory(size);

        public void setMemory(Memory memory) => this.memory = memory;
        
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

        public void malloc(Register register, int ptr, int size)
        {
            Register.Pointer pointer = new Register.Pointer(memory.Allocate(size), size);
            register[ptr] = pointer;
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

        public short ResolveINT16(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 2);
            return BitConverter.ToInt16(data, 0);
        }

        public int ResolveINT32(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt32(data, 0);
        }

        public long ResolveINT64(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            Register register = isMethod ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt64(data, 0);
        }

        //Direct reference
        public short ResolveINT16(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 2);
            return BitConverter.ToInt16(data, 0);
        }

        public int ResolveINT32(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt32(data, 0);
        }

        public long ResolveINT64(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt64(data, 0);
        }

        public object ResolveNumber (string sptr)
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

        public Register.Pointer ResolvePointer(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            return isMethod ? Returns.Last().register[reg] : register[reg];
        }

        public byte[] ResolveData(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            Register register = isMethod ? Returns.Last().register : this.register;
            return memory.read(register[reg].address, register[reg].size);
        }

        public byte[] ResolveData(int ptr, bool isMethodPtr)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            return memory.read(register[ptr].address, register[ptr].size);
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
    }
    
}
