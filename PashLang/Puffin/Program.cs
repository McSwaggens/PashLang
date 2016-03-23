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
                Logger.WriteColor(lexical.TokenStrings.ElementAt(i), ConsoleColor.White);
            }
            Console.WriteLine("End source input ==================");
           Console.WriteLine("Begin token output =================");
            lexical.PrintTokens();
            Console.WriteLine("End token output ==================");
            Parser parse = new Parser(lexical);
            if (!parse.Start())
            {
                Logger.WriteError("An error occured while parsing");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Begin Statement output ===========");
            parse.PrintStatements();
            Console.WriteLine("End Statement output =============");
            Console.WriteLine("Begin Symbol output ==============");
            parse.PrintSymbols();
            Console.WriteLine("End Symbol output ================");
            Console.WriteLine("Compilation Complete");
            Console.ReadKey();
        }
    }
}
