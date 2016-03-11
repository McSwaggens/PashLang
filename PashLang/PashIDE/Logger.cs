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
            Console.WriteLine("Log: " + text);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = bprev;
        }

        public static void Error(string text)
        {
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor bprev = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + text);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = bprev;
        }

        public static void Warning(string text)
        {
            if (!Settings.CurrentSettings.showConsoleWarnings) return;
            ConsoleColor prev = Console.ForegroundColor;
            ConsoleColor bprev = Console.BackgroundColor;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Warning: " + text);
            Console.ForegroundColor = prev;
            Console.BackgroundColor = bprev;
        }
    }
}
