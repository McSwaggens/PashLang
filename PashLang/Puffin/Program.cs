using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend;

namespace Puffin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Puffin Compiler");
            if (args.Length < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error No input file");
                Console.ResetColor();
                Console.ReadKey();
                return;
            }
            string inputString = File.ReadAllText(args[0]);
            Lexer lexical = new Lexer(inputString);
            if (!lexical.Start())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occurred during lexing");
                Console.ResetColor();
                Console.ReadKey();
            }
            for (int i = 0; i < lexical.TokenStrings.Length; i++)
            {
                Console.WriteLine(lexical.TokenStrings[i]);
            }
            Console.ReadKey();
        }
    }
}
