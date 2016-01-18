using System;

namespace PashIDE
{
    public class Logger
    {
        public static void Log(string text)
        {
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor bprev = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("LOG: " + text);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = bprev;
        }

        public static void LogError(string text)
        {
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor bprev = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + text);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = bprev;
        }

        public static void LogInfo(string text)
        {
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor bprev = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("INFO: " + text);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = bprev;
        }
    }
}
