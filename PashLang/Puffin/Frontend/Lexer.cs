using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Tokens;

namespace Puffin.Frontend
{
    public class Lexer
    {
        private string input;
        private LinkedList<string> tokenStrings;
        private LinkedList<Token> tokens; 

        public string Input
        {
            get { return input; }
        }

        public LinkedList<string> TokenStrings
        {
            get { return tokenStrings; }
        }

        public LinkedList<Token> Tokens
        {
            get { return tokens; }
        }

        public Lexer(string input)
        {
            this.input = input;
        }

        public bool Start()
        {
            tokenStrings = TokenizeInput();
            if (tokenStrings.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while tokenizing the input");
                Console.ResetColor();
                return false;
            }
            tokenStrings = new LinkedList<string>(tokenStrings.Where(str => !string.IsNullOrWhiteSpace(str)));
            tokens = GenerateTokens();
            if (tokens == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An error occurred while generating tokens");
                Console.ResetColor();
                return false;
            }
            PrintTokens();
            return true;
        }

        private void PrintTokens()
        {
            foreach (Token tok in tokens)
            {
                Console.WriteLine(tok.ToString());
            }
        }

        private LinkedList<Token> GenerateTokens()
        {
            LinkedList<Token> temp = new LinkedList<Token>();
            LinkedListNode<string> node = tokenStrings.First;
            while (node != null)
            {
                EnumKeywords outk;
                EnumOperators outo;
                EnumControlTokens outc;
                byte outb;
                ushort outus;
                short outs;
                uint outui;
                int outi;
                ulong outul;
                long outl;
                double outd;
                float outf;
                bool outbool;

                if (Enum.TryParse<EnumKeywords>(node.Value.ToUpper(), out outk))
                {
                    KeywordToken tok = new KeywordToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (Enum.TryParse<EnumOperators>(node.Value.ToUpper(), out outo))
                {
                    OperatorToken tok = new OperatorToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (Enum.TryParse<EnumControlTokens>(node.Value.ToUpper(), out outc))
                {
                    ControlToken tok = new ControlToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (byte.TryParse(node.Value, out outb))
                {
                    ByteLiteralToken tok = new ByteLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (ushort.TryParse(node.Value, out outus))
                {
                    UnsignedShortLiteralToken tok = new UnsignedShortLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (short.TryParse(node.Value, out outs))
                {
                    ShortLiteralToken tok = new ShortLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (uint.TryParse(node.Value, out outui))
                {
                    UnsignedIntegerLiteralToken tok = new UnsignedIntegerLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (int.TryParse(node.Value, out outi))
                {
                    IntegerLiteralToken tok = new IntegerLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (ulong.TryParse(node.Value, out outul))
                {
                    UnsignedLongLiteralToken tok = new UnsignedLongLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (long.TryParse(node.Value, out outl))
                {
                    LongLiteralToken tok = new LongLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (float.TryParse(node.Value, out outf))
                {
                    FloatLiteralToken tok = new FloatLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (double.TryParse(node.Value, out outd))
                {
                    DoubleLiteralToken tok = new DoubleLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (Boolean.TryParse(node.Value, out outbool))
                {
                    BooleanLiteralToken tok = new BooleanLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.StartsWith("'") && node.Value.EndsWith("'")))
                {
                    if (node.Value.Length == 3 && node.Value.Replace("'", string.Empty).Length == 1)
                    {
                        CharacterLiteralToken tok = new CharacterLiteralToken(node.Value.Replace("'",string.Empty));
                        temp.AddLast(tok);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR Found Invalid Character Literal " + node.Value);
                        Console.ResetColor();
                        return null;
                    }
                }
                else if (node.Value.StartsWith("\""))
                {
                    string outstr = String.Empty;
                    if (node.Value.Length <= 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR Found Stray \" in Program ");
                        Console.ResetColor();
                        return null;
                    }
                    while (!outstr.EndsWith("\""))
                    {
                        if (node == null || (node != null && node.Value == null))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("ERROR Found Unterminated String literal " + outstr);
                            Console.ResetColor();
                            return null;
                        }
                        outstr += node.Value;
                        node = node.Next;
                    }
                }
                else if (node.Value.Equals("{") || node.Value.Equals("}") || node.Value.Equals("[") ||
                         node.Value.Equals("]") || node.Value.Equals("(") || node.Value.Equals(")") ||
                         node.Value.Equals("\0") || node.Value.Equals("\t") || node.Value.Equals("\n"))
                {
                    ControlToken tok = new ControlToken(node.Value);
                    temp.AddLast(tok);
                }
                else
                {
                    Console.WriteLine("TODO Identifiers will be parsed here");
                    //StringLiteralToken tok = new StringLiteralToken("TODO Identifiers will be parsed here");
                    //temp.AddLast(tok);
                }
                if (node != null) node = node.Next;
            }
            return temp;
        }

        private LinkedList<string> TokenizeInput()
        {
            LinkedList<string> strings = new LinkedList<string>();
            StringBuilder sb = new StringBuilder();
            foreach (char ch in input.ToCharArray())
            {
                if (ch == '\t' || ch == '\0' || ch == '\n' || ch == '\r')
                {
                    //strings.AddLast(sb.ToString());
                    //sb.Clear();
                    continue;
                }
                
                
                if (ch == ' ' || ch == '(' || ch == ')' || ch == '[' || ch == ']' || ch == '{' ||
                    ch == '}' || ch == ',' || ch == ':' || ch == ';')
                {

                    strings.AddLast(sb.ToString());
                    sb.Clear();
                    sb.Append(ch);
                    strings.AddLast(sb.ToString());
                    sb.Clear();
                }
                else
                {
                    sb.Append(ch);
                }
            }
            strings.AddLast(sb.ToString());
            return strings;
        }
    }
}
