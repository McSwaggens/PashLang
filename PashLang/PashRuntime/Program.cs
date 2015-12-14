using System;
using System.Collections.Generic;
using System.IO;
using PASM;

namespace PashRuntime
{
	class MainClass
	{
		public static Dictionary<string, bool> Flags = new Dictionary<string, bool>() {
			{"d", false},
			{"s", false},
			{"v", false},
			{"nostdlib", false}
		};
		public static void Main (string[] args) {
            int calc = 5 * 5 + 7 / 6 + 3 - 9 + 12 * 4 * 5 + 23;
            Console.WriteLine(calc);
            args = new string[] { "/Users/xxdjo/Documents/Code/croc.p" };
			if (args.Length == 0) {
				WriteError ("Please parse in a file to be executed...");
				WriteWarning ("Aborting...");
				return;
			}
			if (File.Exists (args [0]))
				Console.WriteLine ("Executing pash code file: " + Path.GetFileName (args [0]) + "...");
			else {
				WriteError ("Unknown file: " + args[0]);
				WriteWarning ("Aborting...");
			}
			if (args.Length > 1) {
				for (int i = 1; i < args.Length; i++) {
					///TODO: make difference between - and --
					string flag = args [i].StartsWith ("--") ? args [i].TrimStart ("--".ToCharArray ()) : args [i].TrimStart ("-".ToCharArray ());
					if (String.IsNullOrWhiteSpace (flag))
						continue;
					if (!Flags.ContainsKey(flag)) {
						WriteColor ("Unknown flag: " + flag, ConsoleColor.Red);
						WriteColor ("Aborting...", ConsoleColor.Yellow);
						return;
					}
					Flags [flag] = !Flags [flag];
				}
				foreach (KeyValuePair<string, bool> p in Flags) {
					Console.WriteLine (p);
				}
			}
			StartRuntime (args[0]);
            Console.WriteLine("Finished...");
            Console.ReadLine();
		}

		public static List<Type> StandardLibraries = new List<Type> () {
			typeof(Standard)
		};

		public static void StartRuntime (string file) {
			Engine engine = new Engine ();
			engine.Load (File.ReadAllLines (file));
			if (!Flags ["nostdlib"]) {
				//Reference the Standard library...
				StandardLibraries.ForEach (t => engine.ReferenceLibrary (t));
			}
			engine.Execute ();
		}

		public static void WriteColor(string text, ConsoleColor color) {
			ConsoleColor norm = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.WriteLine (text);
			Console.ForegroundColor = norm;
		}

		public static void WriteError(string text) {
			WriteColor ("Error: " + text, ConsoleColor.Red);
		}

		public static void WriteWarning(string text) {
			WriteColor ("Warn!: " + text, ConsoleColor.Yellow);
		}
	}
}
