using System;
using CrocodileScript;
using System.IO;
using PASM;

namespace CrocRuntime
{
	class MainClass
	{
		private static bool DevelopmentBuild = true;
        public static void Main (string[] args)
		{
            if (DevelopmentBuild)
				args = new string[] { "C:/Users/xxdjo/Documents/Code/croc.c" };

			Console.WriteLine ("Compiling Croc Code...");

			CrocCompiler Compiler = new CrocCompiler (File.ReadAllLines(args[0]));

			CrocResult result = Compiler.Compile ();

            if (result.WasSuccessfull)
            {

                File.WriteAllLines(Path.GetDirectoryName(args[0]) + "/" + Path.GetFileNameWithoutExtension(args[0]) + ".p", result.PASM);
                Console.WriteLine("Compilation was successfull... " + Path.GetDirectoryName(args[0]) + "/" + Path.GetFileNameWithoutExtension(args[0]) + ".p");
            }
            else
            {
                Console.WriteLine("Compilation wasn't successfull...");
            }

			string CrocCode = result.BatteredCode;

			Console.WriteLine ("Compilation finished...\nCompiled Code is as follows: \n");
            printarray(result.PASM);

            Console.WriteLine("Executing PASM code with 1024MB of memory...");
            Engine engine = new Engine();
            engine.Load(result.PASM);
            engine.setMemory(1024);
            engine.ReferenceLibrary(typeof(Standard));
            engine.Execute();
            Console.WriteLine("PASM code has finished executing...");
            Console.ReadLine();
		}
        private static void printarray(string[] array)
        {
            foreach (string c in array) Console.WriteLine(c);
        }
	}
}
