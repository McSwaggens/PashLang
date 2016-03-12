using System;

namespace SnapScript
{
    public class Logger
    {

        public static void Log(string text) => System.Console.WriteLine(text);

        public static void LogColor(string text, ConsoleColor color)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = prevColor;
        }

        public static void LogError(string text)
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = prevColor;
        }

        //No need for a log information method, every information log is treated as normal output...
    }
}
