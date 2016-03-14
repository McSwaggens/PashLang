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

        /// <summary>
        /// Load code into handler heap
        /// </summary>
        /// <param name="Code"></param>
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

        /// <summary>
        /// Separate the set commands to the correct class instances
        /// </summary>
        /// <param name="args"></param>
        /// <param name="ln"></param>
        /// <returns></returns>
        private Handler set_Parser(string[] args, string ln)
        {
            if (args[2] == "QMATH")     return new st_QMATH(args, this);
            if (args[2] == "INT32")     return new st_INT32(args, this);
            if (args[2] == "BYTE")      return new st_INT64(args, this);
            if (args[2] == "FLOAT")     return new set_FLOAT(args, this);
            if (args[2] == "DOUBLE")    return new set_DOUBLE(args, this);
            if (args[2] == "INT64")     return new st_INT64(args, this);
            if (args[2] == "INT16")     return new st_INT16(args, this);
            if (args[2] == "PAR")       return new set_PAR(args, this);
            if (args[2] == "PARC")      return new set_PAR(args, this);
            if (args[2] == "VOR")       return new st_VOR(args, this);
            if (args[2] == "VOP")       return new st_VOP(args, this);
            if (args[2] == "PTR")       return new st_PTR(args, this);
            if (args[2] == "VORL")      return new st_VORL(args, this);
            throw new PException("Unknown set extension " + args[2]);
        }

        /// <summary>
        /// Add the static library type to a pool, awaiting to be imported by the script
        /// </summary>
        /// <param name="t"></param>
        public void ReferenceLibrary(params Type[] t)
        {
            foreach (Type type in t)
                ReferencedLibraries.Add(type);
        }

        /// <summary>
        /// Imports the library, making its functions available to the code.
        /// </summary>
        /// <param name="t"></param>
        public void ImportLibrary(Type t)
        {
            StaticCache.Add(t.Name, t);
        }


        /// <summary>
        /// Check if there is any code to be loaded
        /// </summary>
        public bool Loaded => Code != null;

        /// <summary>
        /// Sets the amount of memory the engine is allowed to use
        /// Also known as memory allocation
        /// </summary>
        public void setMemory() => memory = new Memory(1024);

        /// <summary>
        /// Sets the amount of memory the engine is allowed to use
        /// Also known as memory allocation
        /// </summary>
        public void setMemory(int size) => memory = new Memory(size);

        /// <summary>
        /// Sets the amount of memory the engine is allowed to use
        /// Also known as memory allocation
        /// </summary>
        public void setMemory(Memory memory) => this.memory = memory;
        

        /// <summary>
        /// Executes the PASM script that is loaded into the engine instance.
        /// </summary>
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

        /// <summary>
        /// Executes the PASM script that is loaded into the engine and starts the 
        /// </summary>
        /// <param name="startingln"></param>
        public void Execute(int startingln)
        {
            CurrentLine = startingln;
            while (CurrentLine < Code.Length)
            {
                Code[CurrentLine].Execute();
                CurrentLine++;
            }
        }
        
        /// <summary>
        /// Executes the current code loaded into the engine in a debug mode
        /// This "Debug mode" will throw more detailed information and allowes for break points (not currently implemented)
        /// </summary>
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


        /// <summary>
        /// Allocate memory at an address location
        /// </summary>
        /// <param name="Register"></param>
        /// <param name="Pointer"></param>
        /// <param name="Size"></param>
        public void malloc(Register register, int ptr, int size)
        {
            Register.Pointer pointer = new Register.Pointer(memory.Allocate(size), size);
            register[ptr] = pointer;
        }

        /// <summary>
        /// Write a 8 byte double to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
        public void set(int ptr, bool isMethodPtr, double set) //FLOAT
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            if (register[ptr] == null)
            {
                register[ptr] = new Register.Pointer();
                malloc(register, ptr, 8);
                memory.write(Converter.double8(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.double8(set), register[ptr].address);
            }
        }

        

        /// <summary>
        /// Write a 4 byte integer to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
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

        /// <summary>
        /// Write a 4 byte float to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
        public void set(int ptr, bool isMethodPtr, float set) //FLOAT
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            if (register[ptr] == null)
            {
                register[ptr] = new Register.Pointer();
                malloc(register, ptr, 8);
                memory.write(Converter.double8(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.double8(set), register[ptr].address);
            }
        }

        /// <summary>
        /// Write a 8 byte integer to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
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

        /// <summary>
        /// Write a 2 byte integer to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
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

        /// <summary>
        /// Write a 1 byte integer to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
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


        /// <summary>
        /// Writes a set of data to a given address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
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

        /// <summary>
        /// Free an address if it's not being used by another register
        /// </summary>
        /// <param name="p"></param>
        public void TryFreeRegister(Register.Pointer p)
        {
            Memory.Part part = memory.PartAddressStack[p.address];
            if (p.ReferenceCount == 0) memory.Free(part.Address);
        }

        /// <summary>
        /// Will free the registers address even if the address is being used by another register, try not to use this tho...
        /// </summary>
        /// <param name="p"></param>
        public void ForceFreeRegister(Register.Pointer p)
        {
            memory.Free(p.address);
        }

        /// <summary>
        /// Returns the right register
        /// </summary>
        /// <param name="isMethod"></param>
        /// <returns></returns>
        public Register GetRegister(bool isMethod) => isMethod ? Returns.Last().register : register;

        /// <summary>
        /// Returns a 2 byte integer from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public short ResolveINT16(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 2);
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// Returns a 4 byte integer from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public int ResolveINT32(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Returns a 8 byte integer from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public long ResolveINT64(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            Register register = isMethod ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt64(data, 0);
        }

        /// <summary>
        /// Returns a 2 byte integer from a given address
        /// </summary>
        /// <param name="isMethodPtr"></param>
        /// <param name="Register"></param>
        /// <returns></returns>
        public short ResolveINT16(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 2);
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// Returns a 4 byte integer from a given address
        /// </summary>
        /// <param name="isMethodPtr"></param>
        /// <param name="Register"></param>
        /// <returns></returns>
        public int ResolveINT32(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Returns a 8 byte integer from a given address
        /// </summary>
        /// <param name="isMethodPtr"></param>
        /// <param name="Register"></param>
        /// <returns></returns>
        public long ResolveINT64(bool isMethodPtr, int reg)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt64(data, 0);
        }

        /// <summary>
        /// Resolves an integer with an unknown size
        /// </summary>
        /// <param name="sptr"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns a Pointer from a string 
        /// example: :0 or 0
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public Register.Pointer ResolvePointer(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            return isMethod ? Returns.Last().register[reg] : register[reg];
        }

        /// <summary>
        /// Returns data from an address
        /// </summary>
        /// <param name="String pointer"></param>
        /// <returns></returns>
        public byte[] ResolveData(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
            Register register = isMethod ? Returns.Last().register : this.register;
            return memory.read(register[reg].address, register[reg].size);
        }

        /// <summary>
        /// Returns data from an address
        /// </summary>
        /// <param name="String pointer"></param>
        /// <returns></returns>
        public byte[] ResolveData(int ptr, bool isMethodPtr)
        {
            Register register = isMethodPtr ? Returns.Last().register : this.register;
            return memory.read(register[ptr].address, register[ptr].size);
        }

        /// <summary>
        /// Calls a method from inside a static library
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="Method"></param>
        /// <param name="Params"></param>
        /// <returns></returns>
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
