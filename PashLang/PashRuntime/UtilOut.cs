using System;

namespace PashRuntime
{
    public class UtilOut
    {
        public static void WriteColor(string text, ConsoleColor color)
        {
            ConsoleColor norm = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = norm;
        }
        
        public static string CharRepeat(char c, int times) {
            string ret = "";
            for (int i = 0; i < times; i++) ret += c;
            return ret;
        }

        public static void WriteError(string text)
        {
            WriteColor("(ERROR) " + text, ConsoleColor.Red);
        }

        public static void WriteWarning(string text)
        {
            WriteColor("(WARN) " + text, ConsoleColor.Yellow);
        }
    }
}