using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend
{
    public class Lexer
    {
        private string input;
        private string[] tokenStrings;

        public string Input
        {
            get { return input; }
        }

        public string[] TokenStrings
        {
            get { return tokenStrings; }
        }

        public Lexer(string input)
        {
            this.input = input;
        }

        public bool Start()
        {
            tokenStrings = TokenizeInput();
            if (tokenStrings.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while tokenizing the input");
                Console.ResetColor();
                return false;
            }
            tokenStrings = tokenStrings.Where(str => !string.IsNullOrWhiteSpace(str)).ToArray();
            return true;
        }

        private string[] TokenizeInput()
        {
            List<string> strings = new List<string>();
            StringBuilder sb = new StringBuilder();
            foreach (char ch in input.ToCharArray())
            {
                if (ch == '\t' || ch == '\0' || ch == '\n' || ch == '\r')
                {
                    //strings.Add(sb.ToString());
                    //sb.Clear();
                    continue;
                }
                
                
                if (ch == ' ' || ch == '(' || ch == ')' || ch == '[' || ch == ']' || ch == '{' ||
                    ch == '}' || ch == ',' || ch == ':' || ch == ';')
                {

                    strings.Add(sb.ToString());
                    sb.Clear();
                    sb.Append(ch);
                    strings.Add(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(ch);
                }
            }
            strings.Add(sb.ToString());
            return strings.ToArray();
        }
    }
}
