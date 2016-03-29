using System;

namespace Puffin
{
    public class Logger
    {
        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void Write(string text)
        {
            Console.WriteLine(text);
        }

        public static void WriteError(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("(Error) " + text);
            Console.ResetColor();
        }

        public static void WriteWarning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("(Warning) " + text);
            Console.ResetColor();
        }

        public static void WriteCritical(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("(Critical) " + text);
            Console.ResetColor();
        }
    }
}
