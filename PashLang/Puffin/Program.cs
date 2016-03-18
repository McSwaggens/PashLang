using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend;
using static Puffin.Logger;

namespace Puffin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Puffin Compiler");
            if (args.Length < 1)
            {
                WriteError("No input file");
                Console.ReadKey();
                return;
            }
            string inputString = File.ReadAllText(args[0]);
            Lexer lexical = new Lexer(inputString);
            if (!lexical.Start())
            {
                WriteError("Error occurred during lexing");
                Console.ReadKey();
            }
            Console.WriteLine("Begin source input ================");
            for (int i = 0; i < lexical.TokenStrings.Count; i++)
            {
                Console.WriteLine(lexical.TokenStrings.ElementAt(i));
            }
            Console.WriteLine("End source input ==================");
           Console.WriteLine("Begin token output ================");
            lexical.PrintTokens();
            Console.WriteLine("End token output ==================");

            Console.WriteLine("Compilation Complete");
            Console.ReadKey();
        }
    }
}
