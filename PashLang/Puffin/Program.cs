﻿using System;
using Puffin;
using System.IO;
using System.Linq;
using System.Runtime.Hosting;
using Puffin.Command_Line_Args;
using Puffin.Frontend;
using Puffin.Frontend.AST;
using Puffin.Frontend.AST.Nodes;
using static Puffin.Logger;
namespace Puffin
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Puffin Compiler");
            Console.WriteLine(nameof(Single));
            CommandLineParser arguments = new CommandLineParser(args);
            if (!arguments.Start())
            {
                WriteError("Invalid Command line arguments");
                if (!OSInfo.OS_UNIX)
                    Console.ReadKey();
                return;
            }
            string inputString = File.ReadAllText(args[0]);
            Lexer lexical = new Lexer(inputString);
            if (!lexical.Start())
            {
                WriteError("Error occurred during lexing");
                if (!OSInfo.OS_UNIX)
                    Console.ReadKey();
                return;
            }
            Console.WriteLine("Begin source input ================");
            for (int i = 0; i < lexical.TokenStrings.Count; i++)
            {
                WriteColor(lexical.TokenStrings.ElementAt(i), ConsoleColor.White);
            }
            Console.WriteLine("End source input ==================");
           Console.WriteLine("Begin token output =================");
            lexical.PrintTokens();
            Console.WriteLine("End token output ==================");
            Parser parse = new Parser(lexical);
            if (!parse.Start())
            {
                WriteError("An error occurred while parsing");
                if (!OSInfo.OS_UNIX)
                    Console.ReadKey();
                return;
            }
            Console.WriteLine("Begin Statement output ===========");
            parse.PrintStatements();
            Console.WriteLine("End Statement output =============");
            Console.WriteLine("Begin Symbol output ==============");
            parse.PrintSymbols();
            Console.WriteLine("End Symbol output ================");
            Console.WriteLine("Begin Type Checker Output ========");
            TypeChecker tyChecker = new TypeChecker(parse.Statements.ToList(), parse.SymbolTable, arguments.Strictness);
            if (!tyChecker.Start())
            {
                WriteError("Type error occurred");
                if (!OSInfo.OS_UNIX)
                    Console.ReadKey();
                return;
            }
            Console.WriteLine("Type check succeeded");
            Console.WriteLine("End Type Checker Output ========");
            Console.WriteLine("Begin AST Output =================");
            ASTParser ast = new ASTParser(parse);
            BaseASTNode node = ast.ParseAST();
            if (node == null)
            {
                WriteError("null node encountered while parsing AST");
                if (!OSInfo.OS_UNIX)
                    Console.ReadKey();
                return;
            }
            Console.WriteLine(node.Evaluate(node));
            Console.WriteLine("End AST Output ===================");
            Console.WriteLine("Compilation Complete");
            if (!OSInfo.OS_UNIX)
                    Console.ReadKey();
        }
    }
}
