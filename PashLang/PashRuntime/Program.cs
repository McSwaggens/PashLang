﻿using System;
using System.Collections.Generic;
using System.IO;
using PASM;
using System.Diagnostics;

namespace PashRuntime
{
    class MainClass
    {
        public static Dictionary<string, bool> Flags = new Dictionary<string, bool>()
        {
            {"d", false},
            {"s", false},
            {"v", false},
            {"t", true}, //Record and print the execution time...
            {"dr", false}, // Double run
            {"nostdlib", false}
        };

        public static void Main(string[] args)
        {
            args = new[] {"/Users/xxdjo/Documents/Pash Projects/test/main.p"};
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
            }
            if (args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
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
                    Console.WriteLine(p);
                }
            }
            StartRuntime(args[0]);
            //try {
            //    StartRuntime(args[0]);
            //}
            //catch (Exception e)
            //{
            //    WriteError("Failed to execute PASH code, ERROR: " + e.GetType() + ", " + e.Message);
            //}
            Console.WriteLine("Finished" +
                              (Flags["t"]
                                  ? " in " + sw.ElapsedMilliseconds + "ms, " + sw.ElapsedTicks + "ticks (" + sw.Elapsed +
                                    ")."
                                  : "..."));
            Console.ReadLine();
        }

        public static List<Type> StandardLibraries = new List<Type>()
        {
            typeof (Standard)
        };

        private static Stopwatch sw = new Stopwatch();

        public static void StartRuntime(string file)
        {
            Engine engine = new Engine();
            engine.setMemory(1024);
            engine.Load(File.ReadAllLines(file));
            if (!Flags["nostdlib"])
            {
                //Reference the Standard library...
                StandardLibraries.ForEach(t => engine.ReferenceLibrary(t));
            }
            int runtimes = 1;
            if (Flags["dr"]) runtimes = 2;
            for (int i = 0; i < runtimes; i++)
            {
                sw.Reset();
                sw.Start();
                engine.Execute();
                sw.Stop();
            }
        }

        public static void WriteColor(string text, ConsoleColor color)
        {
            ConsoleColor norm = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = norm;
        }

        public static void WriteError(string text)
        {
            WriteColor("Error: " + text, ConsoleColor.Red);
        }

        public static void WriteWarning(string text)
        {
            WriteColor("Warn!: " + text, ConsoleColor.Yellow);
        }
    }
}