using System;
using System.Collections.Generic;
using System.IO;
using PASM;
using System.Diagnostics;
using static PashRuntime.UtilOut;
using static PashRuntime.OSInfo;
using System.Reflection;
using SnapScript;

namespace PashRuntime
{
    public class MainClass
    {
        private static Stopwatch sw = new Stopwatch();
        
        private static List<Type> StandardLibraries = new List<Type>()
        {
            typeof (stdlib.Standard), typeof(stdlib.Threading)
        };
        
        private static Dictionary<string, bool> Flags = new Dictionary<string, bool>()
        {
            {"d",   false}, //Debug run
            {"pc",  false}, //Show pasm code
            {"i",   false}, //Debug print information
            {"v",   false},
            {"t",   false},  // Record and print the execution time
            {"p",   false}, //Pause after execution
            {"it", false}, //Initialize time
            {"osi", false}, //OS Information
            {"nostdlib", false},
            {"compile-snap", false} //Compile with SnapScript flag
        };
        
        private static Dictionary<List<string>, string> TextArguments = new Dictionary<List<string>, string>() {
            {new List<string>{"h", "help"}, 
            @"
--------------------[PashRuntime Help]--------------------

[Usage]

PashRuntime [File] [[- | --] argument] ...

[Description]
PashRuntime is a collection of tools to, Compile and Execute Puffin or PASM code.
The first line should always be the location of the file you wish to execute or compile.

[Command Line Arguments]
Type these in for the desired effect.

-d (debug)
    run the engine in a debug mode.
    Defaults the file to ~/Documents/Scripts/PASM_test.p
    No need for file location argument.

-pc (print code)
    prints out the PASM code that is about to be executed.

-i (information)
    prints out process information as it is happening.
    This gives out Warnings, Errors, fixes, and other info that may be of some use.

-t (time)
    Will print out execution time of the PASM code.
    Order: (ms), (ticks), (raw time)
    
-nostdlib (no standard library)
    Disable the standard library from being added in by default.
    
--osi (os information)
    prints out the current OS information collected by PashRuntime.

[Github]
http://www.Github.com/McSwaggens/PashLang

--------------------[PashRuntime Help]--------------------
"},
            {new List<string>{"v", "version"}, 
$@"
Using;
PashRuntime version v1
PASM version Pre-Release v1
Puffin - Development phase
"}
        };
        
        private static bool DO_EXECUTE = true;
        
        #region Preferences
        ///These settings can be toggled off and on at will, will change how the program behaves, and cannot be changed. (readonly)
        private static readonly string DEBUG_FILE_NAME = "PASM_test.p";
        private static readonly bool FULL_WIDTH_WALLS = false;
        
        private static readonly uint DEFAULT_MEMORY_SIZE_BYTES = 1024;
        #endregion 
        
        private static uint AllocatedMemory = DEFAULT_MEMORY_SIZE_BYTES;
        
        public static void Main(string[] args)
        {
            //Initialize and load the prarameters from the args array, into the Flags dictionary.
            LoadParams(args);
            
            if (!DO_EXECUTE) return;
            
            // -osi (OS Information) argument
            if (Flags["osi"])
            {
                if (OS_UNIX)
                    Console.WriteLine("UNIX BASED √");
                if (OS_LINUX)
                    Console.WriteLine("LINUX √");
                if (OS_OSX)
                    Console.WriteLine("OSX √");
                if (OS_WINDOWS)
                    Console.WriteLine("WINDOWS √");
            }
            
            // -d (Debug) argument
            if (Flags["d"])
            {
				args = OS_LINUX ? new[] {"/home/" + Environment.UserName + $"/Documents/Scripts/{DEBUG_FILE_NAME}"} : new[] {"/Users/" + Environment.UserName + $"/Documents/Scripts/{DEBUG_FILE_NAME}"};
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
            
            //Check if the user wants to compile snapscript code
            if (Flags["compile-snap"])
            {
                string[] compiledPASM = SnapCompiler.Compile(code);
                foreach (string line in compiledPASM)
                {
                    Console.WriteLine("> " + line);
                }
                
                Execute(compiledPASM);
            }
            else
            {
                //Begin the process of executing the code.
                Execute(code);
            }
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
                        
                        string TextOut;
                        if (ContainsTextArgKey(flag, out TextOut)) {
                            Console.WriteLine(TextOut);
                            DO_EXECUTE = false;
                            return;
                        }
                        else if (flag == "m") {
                            i++;
                            uint memory;
                            bool worked = uint.TryParse(args[i], out memory);
                            if (worked) AllocatedMemory = memory;
                            else WriteError("Cannot parse memory flag: Please pass in a number, you entered " + args[i]);
                        }
                        else
                        if (Flags.ContainsKey(flag))
                            Flags[flag] = !Flags[flag];
                        else {
                            WriteColor("Unknown flag: " + flag, ConsoleColor.Red);
                            WriteColor("Aborting...", ConsoleColor.Yellow);
                            return;
                        }
                    }
                }
                
                foreach (KeyValuePair<string, bool> p in Flags)
                {
                    //Console.WriteLine(p);
                }
            }
        }
        
        private static bool ContainsTextArgKey(string key, out string value) {
            value = "";
            foreach (KeyValuePair<List<string>, string> pair in TextArguments) {
                foreach (string tKey in pair.Key) if (tKey == key) { value = pair.Value; return true; }
            }
            return false;
        }
        
        public static void StartRuntime(string[] code)
        {
            Engine engine = new Engine();
            engine.setMemory(AllocatedMemory);
            if (Flags["i"]) WriteInfo($"Allocated {AllocatedMemory} bytes of memory.");
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
