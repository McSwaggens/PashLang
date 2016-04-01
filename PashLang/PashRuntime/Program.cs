using System;
using System.Collections.Generic;
using System.IO;
using PASM;
using System.Diagnostics;
using static PashRuntime.UtilOut;
using static PashRuntime.OSInfo;

namespace PashRuntime
{
    public class MainClass
    {
        private static Stopwatch sw = new Stopwatch();
        
        public static Dictionary<string, bool> Flags = new Dictionary<string, bool>()
        {
            {"d",   false}, //Debug run
            {"pc",  false}, //Show pasm code
            {"i",   false}, //Debug print information
            {"v",   false},
            {"t",   false},  // Record and print the execution time
            {"p",   false}, //Pause after execution
            {"it", false}, //Initialize time
            {"nostdlib", false} 
        };
        
        #region Preferences
        ///These settings can be toggled off and on at will, will change how the program behaves, and cannot be changed. (readonly)
        private static readonly string DEBUG_FILE_NAME = "PASM_test.p";
        private static readonly bool FULL_WIDTH_WALLS = false;
        
        private static readonly uint DEFAULT_MEMORY_SIZE_BYTES = 1024;
        #endregion 
        
        public static void Main(string[] args)
        {
            //Initialize and load the prarameters from the args array, into the Flags dictionary.
            LoadParams(args);
            
            // -i (Information) argument
            if (Flags["i"])
            {
                Console.WriteLine($"OS_UNIX = {OS_UNIX}");
                Console.WriteLine($"OS_OSX = {OS_OSX}");
                Console.WriteLine($"OS_LINUX = {OS_LINUX}");
                Console.WriteLine($"OS_WINDOWS = {OS_WINDOWS}");
            }
            
            // -d (Debug) argument
            if (Flags["d"])
            {
				args = OS_LINUX ? new[] {"/home/" + Environment.UserName + $"/Documents/Scripts/{DEBUG_FILE_NAME}"} : new[] {"/User/" + Environment.UserName + $"/Documents/Scripts/{DEBUG_FILE_NAME}"};
				WriteWarning("Using debug mode");
			}
            
            //Check if there is a file to executed.
            if (args.Length == 0)
            {
                WriteError("Please parse in a file to be executed...");
                WriteWarning("Aborting...");
                return;
            }
            
            //Check if the file exists
            if (File.Exists(args[0]))
                Console.WriteLine("Executing pash code file: " + Path.GetFileName(args[0]) + "...");
            else
            {
                //Print an error if the file does not exist
                WriteError("Unknown file: " + args[0]);
                WriteWarning("Aborting...");
				return;
            }
            string[] code = File.ReadAllLines(args[0]);
            
            // Print all the code we have to execute.
            if (Flags["pc"]) 
            {
                //When switched on, will fill the width of the console with a single line, otherwise, we just use 15(x2) as a width.
                string wall;
                if (FULL_WIDTH_WALLS) wall = CharRepeat('-', (Console.BufferWidth/2)-3); else wall = CharRepeat('-', 15);
                Console.WriteLine($"{wall}[Code]{wall}");
                for (int i = 0; i < code.Length; i++) Console.WriteLine($"{i}\t{code[i]}");
                Console.WriteLine($"{wall}[Code]{wall}");
            }
            
            //Begin the process of executing the code.
            Execute(code);
        }
        
        public static void Entry(string[] code, string[] args)
        {
            LoadParams(args);
            Execute(code);
        }
        
        private static void Execute(string[] code) {
            StartRuntime(code);
            //If the "t" flag is active, print out the execution time.
            if (Flags["t"])
                Console.WriteLine($"Execution finished in {sw.ElapsedMilliseconds} ms, {sw.ElapsedTicks} ticks {sw.Elapsed}.");
            else
                Console.WriteLine("Execution finished.");
            
            //Pause the program after execution, and wait for a keystroke.
            if (Flags["p"]) {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        
        private static void LoadParams(string[] args) {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    
                    if (args[i].StartsWith("-"))
                    {
                        ///TODO: make difference between - and --
                        string flag = args[i].StartsWith("--")
                            ? args[i].TrimStart("--".ToCharArray())
                            : args[i].TrimStart("-".ToCharArray());
                        if (string.IsNullOrWhiteSpace(flag))
                            continue;
                        if (!Flags.ContainsKey(flag))
                        {
                            WriteColor("Unknown flag: " + flag, ConsoleColor.Red);
                            WriteColor("Aborting...", ConsoleColor.Yellow);
                            return;
                        }
                        Flags[flag] = !Flags[flag];
                    }
                }
                
                foreach (KeyValuePair<string, bool> p in Flags)
                {
                    //Console.WriteLine(p);
                }
            }
        }

        public static List<Type> StandardLibraries = new List<Type>()
        {
            typeof (stdlib.Standard), typeof(stdlib.Threading)
        };


        public static void StartRuntime(string[] code)
        {
            Engine engine = new Engine();
            engine.setMemory(DEFAULT_MEMORY_SIZE_BYTES);
            sw.Reset();
            sw.Start();
            engine.Load(code);
            sw.Stop();
            
            if (Flags["it"]) {
                Console.WriteLine($"Initialization took {sw.ElapsedMilliseconds} ms, {sw.ElapsedTicks} ticks {sw.Elapsed}.");
            }
            
            if (!Flags["nostdlib"])
            {
                //Reference the Standard library...
                StandardLibraries.ForEach(t => engine.ReferenceLibrary(t));
            }
            sw.Reset();
            sw.Start();
            engine.Execute();
            sw.Stop();
        }
    }
}
