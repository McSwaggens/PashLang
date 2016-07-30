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
                  Manual meaning the host of the PASM engine will trigger the cleanup stack (Stack being the cached Raster)
                  Onsight is what we have at the moment, where the engine does cleanup at the end of every method (re)
                
        */

        public int[] points;
        public Memory memory;
        public Raster raster = new Raster(50);
        public  List<FunctionInstance> returns = new List<FunctionInstance>();
		public List<Type> referencedLibraries = new List<Type> ();
        private Dictionary<string, Type> staticCache = new Dictionary<string, Type>();
        public int rasterSize = 60;
        public int currentLine = 0;
        
		private Handler[] Code;

        /// <summary>
        /// Blank initializer
        /// You will need to manually set the memory, code and starting line(if needed).
        /// </summary>
        public Engine()
        {

        }

        /// <summary>
        /// Initializer
        /// </summary>
        /// <param name="PASM Code"></param>
        /// <param name="Memory Size"></param>
        /// <param name="Starting Line"></param>
        public Engine(string[] code, uint memory = 1024, int startingLine = 0)
        {
            Load(code);
            setMemory(memory);
            currentLine = startingLine;
            
        }

        /// <summary>
        /// Intitializer
        /// </summary>
        /// <param name="PASM Code"></param>
        /// <param name="Memory size"></param>
        /// <param name="Starting Line"></param>
        public Engine(string[][] code, uint memory = 1024, int startingLine = 0)
        {
            foreach (string[] cf in code) Load(cf);
            setMemory(memory);
            currentLine = startingLine;
        }

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
                this.Code[i] = ParseLine(st);
            }
        }

        /// <summary>
        /// Parses the line string into a Handler
        /// </summary>
        /// <param name="Line"></param>
        /// <returns></returns>
        private Handler ParseLine(string line)
        {
            if (line.StartsWith("set"))
                return set_Parser(line.Split(' '), line);
            else {
                string[] args = line.Split(' ');
                string g = args[0];
                foreach (Type t in Handler.handlers) if (t.Name == g) return (Handler)Activator.CreateInstance(t, args, this);
            }
            return null;
        }

        /// <summary>
        /// Separate the set commands to the correct class instances
        /// </summary>
        /// <param name="args"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private Handler set_Parser(string[] args, string ln)
        {
            if (args[2] == "SINT16" )   return new set_SINT16   (args, this);
            if (args[2] == "SINT32" )   return new set_SINT32   (args, this);
            if (args[2] == "SINT64" )   return new set_SINT64   (args, this);
            if (args[2] == "QMATH"  )   return new set_QMATH     (args, this);
            if (args[2] == "INT32"  )   return new set_INT32     (args, this);
            if (args[2] == "BYTE"   )   return new set_INT64     (args, this);
            if (args[2] == "FLOAT"  )   return new set_FLOAT    (args, this);
            if (args[2] == "DOUBLE" )   return new set_DOUBLE   (args, this);
            if (args[2] == "INT64"  )   return new set_INT64     (args, this);
            if (args[2] == "INT16"  )   return new set_INT16     (args, this);
            if (args[2] == "PARD"   )   return new set_PARD     (args, this);
            if (args[2] == "IP"     )   return new set_IP       (args, this);
            if (args[2] == "ADR"    )   return new set_ADR      (args, this);
            if (args[2] == "VORL"   )   return new set_VORL      (args, this);
            if (args[2] == "PAR"    )   return new set_PAR      (args, this);
            if (args[2] == "PARC"   )   return new set_PAR      (args, this);
            if (args[2] == "SIZE"   )   return new set_SIZE      (args, this);
            if (args[2] == "VOR"    )   return new set_VOR       (args, this);
            if (args[2] == "VOP"    )   return new set_VOP       (args, this);
            if (args[2] == "PTR"    )   return new set_PTR       (args, this);
            if (args[2] == "VORL"   )   return new set_VORL      (args, this);
            throw new PException("Unknown set extension " + args[2]);
        }

        /// <summary>
        /// Add the static library type to a pool, awaiting to be imported by the script
        /// </summary>
        /// <param name="t"></param>
        public void ReferenceLibrary(params Type[] t)
        {
            foreach (Type type in t)
                referencedLibraries.Add(type);
        }

        /// <summary>
        /// Imports the library, making its functions available to the code.
        /// </summary>
        /// <param name="t"></param>
        public void ImportLibrary(Type t)
        {
            staticCache.Add(t.Name, t);
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
        public void setMemory(uint size) => memory = new Memory(size);

        /// <summary>
        /// Sets the amount of memory the engine is allowed to use
        /// Also known as memory allocation
        /// </summary>
        public void setMemory(Memory memory) => this.memory = memory;
        

        /// <summary>
        /// Wipes the current static register and changes it's size.
        /// </summary>
        /// <param name="Size"></param>
        public void setStaticRasterSize(int Size)
        {
			raster = new Raster(Size);
        }

        /// <summary>
        /// Sets the size for new registers that are made.
        /// </summary>
        /// <param name="Size"></param>
        public void setNewRasterSize(int Size)
        {
            
        }


        /// <summary>
        /// Executes the PASM script that is loaded into the engine instance.
        /// </summary>
        public void Execute()
        {
            currentLine = 0;
            while (currentLine < Code.Length)
            {
                if (Code[currentLine] != null)
                    Code[currentLine].Execute();
                currentLine++;
            }
        }

        /// <summary>
        /// Executes the PASM script that is loaded into the engine and starts the 
        /// </summary>
        /// <param name="Starting line"></param>
        public void Execute(int startingln)
        {
            currentLine = startingln;
            while (currentLine < Code.Length)
            {
                Code[currentLine].Execute();
                currentLine++;
            }
        }

        /// <summary>
        /// Parses the currentline before it's executed.
        /// </summary>
        /// <param name="Code"></param>
        public void ExecuteNoPreParse(string[] code)
        {
            Dictionary<int, int> points = new Dictionary<int, int>();
            int maxPointNum = 0;

            for (int i = 0; i < Code.Length; i++)
            {
                string st = code[i];
                if (st.StartsWith("pt"))
                {
                    string[] args = code[i].Split(' ');
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
            Code = new Handler[code.Length];
            while (currentLine < Code.Length)
            {
                if (Code[currentLine] == null) Code[currentLine] = ParseLine(code[currentLine]);
                Code[currentLine].Execute();
                currentLine++;
            }
        }

        /// <summary>
        /// Executes the current code loaded into the engine in a debug mode
        /// This "Debug mode" will throw more detailed information and allowes for break points (not currently implemented)
        /// </summary>
        public void ExecuteDebug() {
            Console.WriteLine("Executing in debug mode, (May not get the best performance.)");
            while (currentLine < Code.Length)
            {
                try {
                    Code[currentLine].Execute();
                    currentLine++;
                }
                catch (PException pe) {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"(ERROR) PASM Exception at line {currentLine} while executing {Code[currentLine].ToString()} returned Exception Notice: {pe.Message}, Please check code and try again!");
                    Console.ForegroundColor = color;
                    break;
                }
            }
        }


        /// <summary>
        /// Allocate memory at an address location
        /// </summary>
        /// <param name="Raster"></param>
        /// <param name="Pointer"></param>
        /// <param name="Size"></param>
        public void malloc(Raster register, int ptr, uint size)
        {
            Register pointer = new Register(memory.Allocate(size), size);
            register[ptr] = pointer;
        }

        /// <summary>
        /// Allocate memory at an address location
        /// </summary>
        /// <param name="Raster"></param>
        /// <param name="Pointer"></param>
        /// <param name="Size"></param>
        public void malloc(Raster register, int ptr, int size)
        {
            Register pointer = new Register(memory.Allocate((uint)size), (uint)size);
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
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
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
            Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
                malloc(register, ptr, 4);
                memory.write(Converter.int32(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.int32(set), register[ptr].address);
            }
        }

        /// <summary>
        /// Write a 4 byte integer to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
        public void set(int ptr, bool isMethodPtr, uint set) //INT32
        {
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
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
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
                malloc(register, ptr, 4);
                memory.write(Converter.float4(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.float4(set), register[ptr].address);
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
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
                malloc(register, ptr, 8);
                memory.write(Converter.int64(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.int64(set), register[ptr].address);
            }
        }

        /// <summary>
        /// Write a 8 byte integer to an address
        /// </summary>
        /// <param name="Pointer"></param>
        /// <param name="isMethodPointer"></param>
        /// <param name="DataSet"></param>
        public void set(int ptr, bool isMethodPtr, ulong set) //INT64
        {
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
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
        public void set(int ptr, bool isMethodPtr, ushort set) //INT16
        {
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
                malloc(register, ptr, 2);
                memory.write(Converter.int16(set), register[ptr].address);
            }
            else
            {
                memory.write(Converter.int16(set), register[ptr].address);
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
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
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
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
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
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            if (register[ptr] == null)
            {
                register[ptr] = new Register();
                malloc(register, ptr, (uint)set.Length);
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
        public void TryFreeRegister(Register p)
        {
			if (p.referenceCount == 0)
				memory.Free (p.address);
        }

        /// <summary>
        /// Will free the registers address even if the address is being used by another register, try not to use this tho...
        /// </summary>
        /// <param name="p"></param>
        public void ForceFreeRegister(Register p)
        {
            memory.Free(p.address);
        }

        /// <summary>
        /// Returns the right register
        /// </summary>
        /// <param name="isMethod"></param>
        /// <returns></returns>
		public Raster GetRaster(bool isMethod) => isMethod ? returns.Last().register : raster;

        /// <summary>
        /// Returns a 2 byte integer from a string
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public short ResolveINT16(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
			byte[] data = memory.read(raster[reg].address, 2);
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
			byte[] data = memory.read(raster[reg].address, 4);
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
			Raster register = isMethod ? returns.Last().register : this.raster;
            byte[] data = memory.read(register[reg].address, 8);
            return BitConverter.ToInt64(data, 0);
        }

        /// <summary>
        /// Returns a 2 byte integer from a given address
        /// </summary>
        /// <param name="isMethodPtr"></param>
        /// <param name="Raster"></param>
        /// <returns></returns>
        public short ResolveINT16(bool isMethodPtr, int reg)
        {
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            byte[] data = memory.read(register[reg].address, 2);
            return BitConverter.ToInt16(data, 0);
        }

        /// <summary>
        /// Returns a 4 byte integer from a given address
        /// </summary>
        /// <param name="isMethodPtr"></param>
        /// <param name="Raster"></param>
        /// <returns></returns>
        public int ResolveINT32(bool isMethodPtr, int reg)
        {
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            byte[] data = memory.read(register[reg].address, 4);
            return BitConverter.ToInt32(data, 0);
        }

        /// <summary>
        /// Returns a 8 byte integer from a given address
        /// </summary>
        /// <param name="isMethodPtr"></param>
        /// <param name="Raster"></param>
        /// <returns></returns>
        public long ResolveINT64(bool isMethodPtr, int reg)
        {
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
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
			Raster register = isMethod ? returns.Last().register : this.raster;
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
        /// Resolves an integer with an unknown size
        /// </summary>
        /// <param name="sptr"></param>
        /// <returns></returns>
        public object ResolveNumber (int register, bool isMethodPtr)
        {
			Raster raster = isMethodPtr ? returns.Last().register : this.raster;
            byte[] data = memory.read(raster[register].address, raster[register].size);
            switch (raster[register].size)
            {
                case 2: return BitConverter.ToInt16(data, 0);
                case 4: return BitConverter.ToInt32(data, 0);
                case 8: return BitConverter.ToInt64(data, 0);
            }
            throw new PException("Unknown number length of " + raster[register].size + " bytes, 16bits(2bytes), 32bits(4bytes), 64bits(8bytes)");
        }

        /// <summary>
        /// Returns a Pointer from a string 
        /// example: :0 or 0
        /// </summary>
        /// <param name="string"></param>
        /// <returns></returns>
        public Register ResolvePointer(string sptr)
        {
            int reg;
            bool isMethod = isMethodPointer(sptr, out reg);
			return isMethod ? returns.Last().register[reg] : raster[reg];
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
			Raster register = isMethod ? returns.Last().register : this.raster;
            return memory.read(register[reg].address, register[reg].size);
        }

        /// <summary>
        /// Returns data from an address
        /// </summary>
        /// <param name="String pointer"></param>
        /// <returns></returns>
        public byte[] ResolveData(int ptr, bool isMethodPtr)
        {
			Raster register = isMethodPtr ? returns.Last().register : this.raster;
            return memory.read(register[ptr].address, register[ptr].size);
        }

        /// <summary>
        /// Calls a method from inside a static library
        /// </summary>
        /// <param name="Class"></param>
        /// <param name="Method"></param>
        /// <param name="params"></param>
        /// <returns></returns>
        public byte[] CallStaticMethod(string @class, string method, byte[][] @params)
        {
            object[] Objects = new object[@params.Length + 1];
            Objects[0] = this;
            for (int i = 0; i < @params.Length; i++) Objects[i + 1] = @params[i];
            foreach (KeyValuePair<string, Type> i in staticCache.Where(i => i.Key == @class))
                return (byte[])(i.Value.GetMethod(method, BindingFlags.Static | BindingFlags.Public).Invoke(i.Value, Objects));
            return null;
        }
    }
    
}
