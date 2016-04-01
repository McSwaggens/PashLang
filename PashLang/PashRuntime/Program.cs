using System;
using System.Collections.Generic;
using System.IO;
using PASM;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PashRuntime
{
    public class MainClass
    {
        public static Dictionary<string, bool> Flags = new Dictionary<string, bool>()
        {
            {"d",   false}, //Debug run
            {"pc",  false}, //Show pasm code
            {"i",   false}, //Debug print information
            {"v",   false},
            {"t",   true},  // Record and print the execution time...
            {"nostdlib", false}
        };
        
        [DllImport ("libc")]
		static extern int uname (IntPtr buf);
        
        private static bool IsRunningMac ()
		{
			IntPtr buf = IntPtr.Zero;
			try {
				buf = Marshal.AllocHGlobal (8192);
				// This is a hacktastic way of getting sysname from uname ()
				if (uname (buf) == 0) {
					string os = Marshal.PtrToStringAnsi (buf);
					if (os == "Darwin")
						return true;
				}
			} catch {
			} finally {
				if (buf != IntPtr.Zero)
					Marshal.FreeHGlobal (buf);
			}
			return false;
		}
        
        private static bool IsRunningUnix() => Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.MacOSX;
        
        private static readonly bool OS_UNIX = IsRunningUnix();
        
        private static bool IsRunningLinux() => OS_UNIX && !IsRunningMac();
        
        private static readonly bool OS_LINUX = IsRunningLinux();
        
        private static readonly bool OS_OSX = OS_UNIX && IsRunningMac();
        
        private static readonly bool OS_WINDOWS = Environment.OSVersion.Platform == PlatformID.Win32Windows;
        
        
        private static readonly string DEBUG_FILE_NAME = "PASM_test.p";
        public static void Main(string[] args)
        {
            LoadParams(args);
            
            if (Flags["i"])
            {
                Console.WriteLine($"OS_UNIX = {OS_UNIX}");
                Console.WriteLine($"OS_OSX = {OS_OSX}");
                Console.WriteLine($"OS_LINUX = {OS_LINUX}");
                Console.WriteLine($"OS_WINDOWS = {OS_WINDOWS}");
            }
            if (Flags["d"])
            {
				args = OS_LINUX ? new[] {"/home/" + Environment.UserName + $"/Documents/Scripts/{DEBUG_FILE_NAME}"} : new[] {"/User/" + Environment.UserName + $"/Documents/Scripts/{DEBUG_FILE_NAME}"};
				WriteWarning("Using debug mode");
			}
            if (args.Length == 0)
            {
                WriteError("Please parse in a file to be executed...");
                WriteWarning("Aborting...");
                return;
            }
            if (File.Exists(args[0]))
                Console.WriteLine("Executing pash code file: " + Path.GetFileName(args[0]) + "...");
            else
            {
                WriteError("Unknown file: " + args[0]);
                WriteWarning("Aborting...");
				return;
            }
            string[] code = File.ReadAllLines(args[0]);
            
            if (Flags["pc"]) 
            {
                //When switched on, will fill the width of the console with a single line, otherwise, we just use 15(x2) as a width.
                bool fullWidth = false;
                string wall;
                if (fullWidth) wall = CharRepeat('-', (Console.BufferWidth/2)-3); else wall = CharRepeat('-', 15);
                Console.WriteLine($"{wall}[Code]{wall}");
                for (int i = 0; i < code.Length; i++) Console.WriteLine($"{i}\t{code[i]}");
                Console.WriteLine($"{wall}[Code]{wall}");
            }
            
            Execute(code);
        }

        public static void Entry(string[] code, string[] args)
        {
            LoadParams(args);
            Execute(code);
        }
        
        private static void Execute(string[] code) {
            StartRuntime(code);
            Console.WriteLine("Finished" +
                              (Flags["t"]
                                  ? " in " + sw.ElapsedMilliseconds + "ms, " + sw.ElapsedTicks + "ticks (" + sw.Elapsed +
                                    ")."
                                  : "..."));
        }
        
        private static void LoadParams(string[] args) {
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
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

        private static Stopwatch sw = new Stopwatch();

        public static void StartRuntime(string[] code)
        {
            Engine engine = new Engine();
            engine.setMemory(1024);
            engine.Load(code);
            if (!Flags["nostdlib"])
            {
                //Reference the Standard library...
                StandardLibraries.ForEach(t => engine.ReferenceLibrary(t));
            }
            sw.Start();
            engine.Execute();
            sw.Stop();
        } 

        public static void WriteColor(string text, ConsoleColor color)
        {
            ConsoleColor norm = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = norm;
        }
        
        private static string CharRepeat(char c, int times) {
            string ret = "";
            for (int i = 0; i < times; i++) ret += c;
            return ret;
        }

        public static void WriteError(string text)
        {
            WriteColor("(ERROR) " + text, ConsoleColor.Red);
        }

        public static void WriteWarning(string text)
        {
            WriteColor("(WARN) " + text, ConsoleColor.Yellow);
        }
    }
}
