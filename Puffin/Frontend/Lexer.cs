﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Symbols.TypeInfo;
using Puffin.Frontend.Tokens;
using static Puffin.Logger;
namespace Puffin.Frontend
{
    public class Lexer
    {
        private string input;
        private LinkedList<string> tokenStrings;
        private LinkedList<Token> tokens;
        public static readonly List<string> operators = new List<string>(new string[] { "=", "!=", "==", "+", "-", "*", "/", "++#", "++", "--#", "--", ">", "<", ">=", "<=", "&&", "&", "||", "|", "!", "~", "^", "+=", "-=", "*=", "/=", "<<", ">>", "%=", "&=", "|=", "^=", "<<=", ">>=", "?:", ".", "," });
        //private SymbolTable symbolTable;

        //public Lexer()
        //{
        //    symbolTable = new SymbolTable();
        //}

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
                WriteError("An error occurred while tokenizing the input");
                return false;
            }
            tokenStrings = new LinkedList<string>(tokenStrings.Where(str => !Regex.IsMatch(str, @"[^\S\r\n]+") && !string.IsNullOrEmpty(str)));
            //tokenStrings = new LinkedList<string>(tokenStrings.Where(str => !str.Equals("\n") || !str.Equals("\t")));
            tokens = GenerateTokens();
            if (tokens == null)
            {
                WriteError("An error occured while generating tokens");
                return false;
            }
            return true;
        }

        public void PrintTokens()
        {
            foreach (Token tok in tokens)
            {
                if(tok.Value.Equals("\n"))
                    WriteColor(tok.Type.ToString(),ConsoleColor.Magenta);
                else
                    WriteColor(tok.ToString(),ConsoleColor.Magenta);
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

                if ((node.Value.EndsWith("b") || node.Value.EndsWith("B")) 
                    && byte.TryParse(node.Value.Substring(0,node.Value.Length - 1), out outb))
                {
                    ByteLiteralToken tok = new ByteLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.EndsWith("us") || node.Value.EndsWith("US"))
                    && ushort.TryParse(node.Value.Substring(0, node.Value.Length - 2), out outus))
                {
                    UnsignedShortLiteralToken tok = new UnsignedShortLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.EndsWith("s") || node.Value.EndsWith("S"))
                    && short.TryParse(node.Value.Substring(0, node.Value.Length - 1), out outs))
                {
                    ShortLiteralToken tok = new ShortLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.EndsWith("ui") || node.Value.EndsWith("UI"))
                    && uint.TryParse(node.Value.Substring(0, node.Value.Length - 2), out outui))
                {
                    UnsignedIntegerLiteralToken tok = new UnsignedIntegerLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (int.TryParse(node.Value, out outi))
                {
                    IntegerLiteralToken tok = new IntegerLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.EndsWith("ul") || node.Value.EndsWith("UL"))
                    && ulong.TryParse(node.Value.Substring(0, node.Value.Length - 2), out outul))
                {
                    UnsignedLongLiteralToken tok = new UnsignedLongLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.EndsWith("l") || node.Value.EndsWith("L"))
                    && long.TryParse(node.Value.Substring(0, node.Value.Length - 1), out outl))
                {
                    LongLiteralToken tok = new LongLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.EndsWith("F") || node.Value.EndsWith("f"))
                    && float.TryParse(node.Value.Substring(0, node.Value.Length - 1), out outf))
                {
                    FloatLiteralToken tok = new FloatLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if ((node.Value.EndsWith("D") || node.Value.EndsWith("d"))
                    && double.TryParse(node.Value.Substring(0, node.Value.Length - 1), out outd))
                {
                    DoubleLiteralToken tok = new DoubleLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (bool.TryParse(node.Value, out outbool))
                {
                    BooleanLiteralToken tok = new BooleanLiteralToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (Enum.TryParse<EnumKeywords>(node.Value.ToUpper(), out outk))
                {
                    //Logger.Write(Enum.Parse(typeof(EnumKeywords),node.Value.ToUpper()).ToString());
                    KeywordToken tok = new KeywordToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (Enum.TryParse<EnumOperators>(node.Value.ToUpper(), out outo))
                {
                    OperatorToken tok = new OperatorToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (ResolveOperator(node.Value.ToUpper()) != EnumOperators.NO_OPERATOR)
                {
                    OperatorToken tok = new OperatorToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (Enum.TryParse<EnumControlTokens>(node.Value.ToUpper(), out outc))
                {
                    ControlToken tok = new ControlToken(node.Value);
                    temp.AddLast(tok);
                }
                else if (node.Value.StartsWith("//"))
                {
                    while (node != null && !node.Value.Equals("\n"))
                    {
                        node = node.Next;
                    }
                }
                else if (node.Value.StartsWith("/*"))
                {
                    while (node != null && !node.Value.EndsWith("*/"))
                    {
                        node = node.Next;
                    }
                    if (node == null)
                    {
                        WriteError("Unterminated block comment");
                        return null;
                    }
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
                        WriteError("Found Invalid Character Literal " + node.Value);
                        return null;
                    }
                }
                else if (node.Value.StartsWith("\""))
                {
                    int times = 0;
                    string outstr = String.Empty;
                    if (node.Value.Length <= 1)
                    {
                        WriteError("Found Stray \" in Program");
                        return null;
                    }
                    while (!outstr.EndsWith("\""))
                    {
                        if (node == null || (node != null && node.Value == null))
                        {
                            WriteError("ERROR Found Unterminated String literal " + outstr);
                            return null;
                        }
                        outstr += " ";
                        outstr += node.Value;
                        node = node.Next;
                        times++;
                    }
                    //outstr = outstr.Substring(0, outstr.LastIndexOf('\"') + 1);
                    //Logger.WriteWarning(outstr);
                    //string nextTok = outstr.Substring(outstr.LastIndexOf('\"'), outstr.Length - 1);
                    //Logger.WriteWarning(nextTok);
                    StringLiteralToken tok = new StringLiteralToken(outstr);
                    temp.AddLast(tok);
                    if (times >= 1)
                        node = node.Previous; // if we have advanced the lexer move it back one so no tokens are skipped

                }
                else if (node.Value.Equals("{") || node.Value.Equals("}") || node.Value.Equals("[") ||
                         node.Value.Equals("]") || node.Value.Equals("(") || node.Value.Equals(")") ||
                         node.Value.Equals("\0")|| node.Value.Equals(";")|| node.Value.Equals("\t") || 
                         node.Value.Equals("\n"))
                {
                    ControlToken tok = new ControlToken(node.Value);
                    temp.AddLast(tok);
                }
                else
                {
                    if (node.Next != null && node.Next.Value.Equals("("))
                    {
                        IdentifierToken tok = new IdentifierToken(node.Value);
                        temp.AddLast(tok);
                        if (node.Previous != null)
                        {
                            MethodSymbol<MethodInformation> function = new MethodSymbol<MethodInformation>(
                                new MethodInformation(node.Value,
                                new ClassInformation(node.Previous.Value,null,true,true), 
                                new ParameterInformation[0]));
                            Console.WriteLine(function.IdentifierName + " : " + function.ValueType.Name);
                        }
                        else
                        {
                            WriteError("Invalid return type");
                            return null;
                        }
                    }
                    else
                    {
                        IdentifierToken tok = new IdentifierToken(node.Value);
                        temp.AddLast(tok);
                    }
                }
                if (node != null) node = node.Next;
            }
            return temp;
        }

        private LinkedList<string> TokenizeInput()
        {
            LinkedList<string> strings = new LinkedList<string>();
            StringBuilder sb = new StringBuilder();
           
            foreach (char ch in input)
            {
                if (ch == '\t' || ch == '\0' || ch == '\r')
                {
                    //strings.AddLast(sb.ToString());
                    //sb.Clear();
                    continue;
                }
                
                
                if (ch == ' ' || ch == '(' || ch == ')' || ch == '[' || ch == ']' || ch == '{' ||
                    ch == '}' || ch == ',' || ch == ':' || ch == ';' || ch == '\n')
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

        private EnumOperators ResolveOperator(string value)
        {
            for (int i = 0; i < operators.Count; i++)
            {
                if (value.Equals(operators[i]))
                {
                    return (EnumOperators) i;
                }
            }
            return EnumOperators.NO_OPERATOR;
        }
    }
}
