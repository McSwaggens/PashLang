using System;
using System.Collections.Generic;
using System.Linq;
using static SnapScript.Logger;

namespace SnapScript
{
    public class SnapCompiler
    {
        //TODO Add support for comments using #
        //TODO return
        //TODO use
        //TODO goto
        //TODO function
        //TODO code:
        /*
        Standard::cout_num(Short);
        Short->Int;
        */

        private static Dictionary<string, VariableType> DeclaredTypes = new Dictionary<string, VariableType>{
            { "short",  VariableType.INT16 },
            { "int",    VariableType.INT32 },
            { "long",   VariableType.INT64 },
            { "byte",   VariableType.BYTE },
            { "bool",   VariableType.BOOL }
        };
        private static Dictionary<string, OperatorType> DeclaredOperators = new Dictionary<string, OperatorType>{
            { "->", OperatorType.Pointer },
            { "+",  OperatorType.Plus },
            { "-",  OperatorType.Dash },
            { "*",  OperatorType.Star },
            { "/",  OperatorType.Slash },
            { "=",  OperatorType.Equals },
            { "==", OperatorType.Comparison },
            { "!=", OperatorType.ReverseComparison },
            { "+=", OperatorType.SelfAddition },
            { "-=", OperatorType.SelfRemove },
            { ">",  OperatorType.Comparison_Bigger },
            { ">=", OperatorType.Comparison_Bigger_Equal },
            { "<",  OperatorType.Comparison_Smaller },
            { "<=", OperatorType.Comparison_Smaller_Equal },
            { "::", OperatorType.StaticReference },
            { "(", OperatorType.OpenBracket },
            { ")", OperatorType.CloseBrakcet },
        };

        private static VariableType GetTypeFromRaw(string raw)
        {
            foreach (KeyValuePair<string, VariableType> pair in DeclaredTypes) if (pair.Key == raw) return pair.Value;
            return VariableType.UNKNOWN;
        }

        private static string[] Statements = { "return", "goto", "funcion", "import" };
        private static char[] SpecialCharacters = "+-!@#$%^&*():;\"',.?/~`\\|=<>{}[]".ToCharArray();

        private List<Variable> Variables = new List<Variable>();
        private List<Function> Functions = new List<Function>();
        private List<Point> Points = new List<Point>();


        private int CurrentLine = 0;

        public static string[] Compile(string[] code)
        {
            SnapCompiler compiler = new SnapCompiler(code);
            return compiler.CompileToPASM();
        }

        public string[] Code;
        public string[] PasmCode;

        public SnapCompiler(string[] code)
        {
            Code = code;
        }


        public string[] CompileToPASM()
        {
            List<Token[]> tokenCode = new List<Token[]>();
            List<string> compiledCode = new List<string>();
            for (; CurrentLine < Code.Length; CurrentLine++)
            {
                string str_line = Code[CurrentLine];
                List<Token> generatedTokens = null;
                if (str_line != "")
                    generatedTokens = GenerateTokens(str_line).ToList();
                tokenCode.Add(generatedTokens.ToArray());
                //VariableType, WORD, Operator, Number
            }
            foreach (Token[] token in tokenCode)
            {
                if (token[0].type == TokenType.WORD && ResolveVariable(token[0].Raw) != null && token[1]?.type == TokenType.WORD && token[2]?.type == TokenType.Operator && token[1].Raw == "(")
                {
                    List<string> Params = new List<string>();
                    char c = '\0';
                    for (int i = 3; c != ')'; i++)
                    {
                        if (token[i].type == TokenType.WORD) Params.Add(token[i].Raw);
                        i++;
                        if (token[i].type != TokenType.Operator || token[i].Raw != ",") throw new SnapException($"Expected comma (,) when parsing parameters for method {token[1].Raw}");
                    }
                    Function function = new Function(token[1].Raw, Params);
                    Functions.Add(function);
                }
            }
            int Indentation = 0;
            bool inMethod = false;
            int line = 0;
            foreach (Token[] token in tokenCode)
            {
                if (token[0].type == TokenType.Operator && token[0].Raw == "{")
                {
                    inMethod = true;
                    Indentation++;
                }
                else if (token[0].type == TokenType.Operator && token[0].Raw == "}")
                {
                    Indentation--;
                    if (Indentation == 0) { inMethod = false; }
                }
                else if (token[0].type == TokenType.Type && token[1].type == TokenType.WORD)
                {
                    //Create a blank variable for us to use... (null)
                    bool isPublic = !inMethod;
                    Variable v = new Variable(this, isPublic);
                    VariableType type = ResolveDeclaredVariableType(token[0].Raw);
                    v.type = type;
                    v.Name = token[1].Raw;
                    Variables.Add(v);
                    if (token.Length > 1) //We're now setting a variable.
                    {
                        if (token[2].type == TokenType.Operator && token[2].Raw == "=")
                        {
                            string cpl = $"set {v.Tag}";
                            if (token[3].type == TokenType.WORD)
                            {

                                Variable resolvedSetTo = ResolveVariable(token[3].Raw);
                                if (resolvedSetTo == null) throw new SnapException("Unable to find variable " + token[3].Raw);
                                cpl += $" VOP {resolvedSetTo.Tag}";
                                compiledCode.Add("malloc_c " + v.Tag + " " + resolvedSetTo.Tag);
                            }
                            else if (token[3].type == TokenType.Number)
                            {
                                if (token[0].Raw == "byte") cpl += $" BYTE {token[3].Raw}";
                                else if (token[0].Raw == "short") cpl += $" INT16 {token[3].Raw}";
                                else if (token[0].Raw == "int") cpl += $" INT32 {token[3].Raw}";
                                else if (token[0].Raw == "long") cpl += $" INT64 {token[3].Raw}";
                                else throw new Exception("Unknown number variable set type for " + token[0].Raw);
                            }
                            compiledCode.Add(cpl);
                        }
                    }
                    else //No variable is set...
                    {

                    }
                }
                else if (token[0].type == TokenType.WORD && ResolveVariable(token[0].Raw) != null && token[1]?.type == TokenType.WORD && token[2]?.type == TokenType.Operator && token[1].Raw == "(")
                {
                    Function function = ResolveFunction(token[1].Raw);
                    compiledCode.Add($"pt {function.ID}");
                }
                else if (token[0].type == TokenType.WORD && ResolveFunction(token[0].Raw) != null && token[1].type == TokenType.Operator && token[1].Raw == "(")
                {
                    Function function = ResolveFunction(token[0].Raw);
                    string cpl = $"call {function.ID}";
                    List<string> Params = new List<string>();
                    Token t = token[2];
                    for (int i = 2; t.Raw != ")"; i++, t = token[i])
                    {
                        if (t.type == TokenType.WORD) Params.Add(t.Raw);
                        i++;
                        if (t.type != TokenType.Operator || t.Raw != ",") throw new SnapException($"Expected comma (,) when parsing parameters for method {t.Raw}");
                    }
                }
                else if (token[0].type == TokenType.WORD && ResolveVariable(token[0].Raw) != null && token[1]?.type == TokenType.Operator && token[1].Raw == "=" && token[2]?.type == TokenType.Number)
                {
                    Variable workingVariable = ResolveVariable(token[0].Raw);
                    compiledCode.Add($"set {workingVariable.Tag} {workingVariable.type} {token[2].Raw}");
                }
                else if (token[0].type == TokenType.WORD && ResolveVariable(token[0].Raw) != null && token[1]?.type == TokenType.Operator && token[1].Raw == "=" && token[2]?.type == TokenType.WORD && ResolveVariable(token[2].Raw) != null)
                {
                    Variable workingVariable = ResolveVariable(token[0].Raw);
                    Variable setterVariable = ResolveVariable(token[2].Raw);

                    compiledCode.Add($"set {workingVariable.Tag} VOP {setterVariable.Tag}");
                }
                else if (token[0].type == TokenType.WORD && ResolveVariable(token[0].Raw) != null && token[1]?.type == TokenType.Operator && token[1].Raw == "->" && token[2]?.type == TokenType.WORD && ResolveVariable(token[2].Raw) != null)
                {
                    Variable workingVariable = ResolveVariable(token[0].Raw);
                    Variable setterVariable = ResolveVariable(token[2].Raw);

                    compiledCode.Add($"set {workingVariable.Tag} PTR {setterVariable.Tag}");
                }
                else if (token[0].type == TokenType.WORD && token.Length > 0 && token[1].type == TokenType.Operator && ResolveDefinedOperatorType(token[1].Raw) == OperatorType.StaticReference && token[2].type == TokenType.WORD)
                {
                    List<string> paramGroup = new List<string>();
                    string Library = token[0].Raw;
                    string Method = token[2].Raw;
                    string cpl = "calib " + Library + " " + Method;
                    for (int i = 4; i < token.Length; i++)
                    {
                        Token t = token[i];
                        if (t.type == TokenType.Operator && t.Raw == ")") break;
                        cpl += " " + ResolveVariable(t.Raw).Tag;
                    }

                    compiledCode.Add(cpl);
                }
                else if (token[0].type == TokenType.Statement)
                {
                    if (token[0].Raw == "import")
                    {
                        if (token[1].type == TokenType.WORD) compiledCode.Add("im " + token[1].Raw);
                    }
                    else if (token[0].Raw == "return")
                    {
                        string cpl = "re";
                        if (token.Length > 1)
                        {
                            Variable variable = ResolveVariable(token[1].Raw);
                            if (variable == null) throw new SnapException("Unknown variable " + token[1].Raw);
                            cpl += " " + variable.ID;
                        }
                        compiledCode.Add(cpl);
                    }
                    else if (token[0].Raw == "goto")
                    {
                        if (token.Length == 1) throw new SnapException("Missing point parse at command goto");
                        Point point = ResolvePoint(token[1].Raw);
                        compiledCode.Add("mov " + point.ID);
                    }
                }
                else
                {
                    //throw new SnapException($"Unknwon syntax error at line {line}");
                }
                line++;
            }
            return compiledCode.ToArray();
        }

        public Function ResolveFunction(string functionName)
        {
            foreach (Function function in Functions) if (function.Name == functionName) return function;
            return null;
        }

        public Point ResolvePoint(string name)
        {
            foreach (Point p in Points) if (p.Name == name) return p; return null;
        }

        public Variable ResolveVariable(string variableName)
        {
            foreach (Variable variable in Variables)
            {
                if (variable.Name == variableName) return variable;
            }
            //TODO Maybe throw an error???
            return null;
        }


        public bool DoesVariableExist(string variableName)
        {
            foreach (Variable variable in Variables)
            {
                if (variable.Name == variableName) return true;
            }
            //TODO Maybe throw an error???
            return false;
        }

        public OperatorType ResolveDefinedOperatorType(string text)
        {
            foreach (KeyValuePair<string, OperatorType> pair in DeclaredOperators)
            {
                if (pair.Key == text) return pair.Value;
            }
            return DeclaredOperators.First().Value;
        }

        public Token[] GenerateTokens(string line)
        {
            List<Part> parts = new List<Part>();
            {
                char[] carr = line.ToCharArray();
                string current = "";
                for (int i = 0; i < carr.Length; i++)
                {
                    char c = carr[i];
                    if (c == '{' || c == '}')
                    {
                        if (current != "" && current != " ")
                            parts.Add(new Part(current));
                        parts.Add(new Part("" + c));
                        continue;
                    }
                    bool _isOperator = isOperator(c);
                    //Checks
                    if ((c == ' ' || c == '\t') || (_isOperator) || i == carr.Length - 1)
                    {
                        if (i == carr.Length - 1 && !_isOperator && current != " ")
                        {
                            current += c;
                        }
                        if (_isOperator)
                        {
                            if (current != "")
                                parts.Add(new Part(current));
                            if (c != ' ' && c != '\t')
                                current = "" + c;
                            else
                                current = "";
                            i++;
                            int startLength = i;
                            for (; i < carr.Length; i++)
                            {
                                c = carr[i];
                                if (!isOperator(c))
                                {
                                    if (i != carr.Length)
                                        i--;
                                    break;
                                }
                                current += c;
                            }
                            for (; i > startLength - 1; i--)
                            {
                                if (DoesOperatorTypeExist(current)) break;
                                else current = current.Remove(current.Length - 1);
                            }
                        }
                        if (current != "")
                            parts.Add(new Part(current));
                        current = "";
                        continue;
                    }
                    if (c != ' ' && c != '\t')
                        current += c;
                }
                if (current != "") parts.Add(new Part(current));
            }
            List<Token> tokens = new List<Token>();
            foreach (Part part in parts)
            {
                string current = part.Raw;
                VariableType type = ResolveDeclaredVariableType(current);
                if (type != VariableType.UNKNOWN) tokens.Add(new Token(TokenType.Type, current));
                else if (isStatement(current)) tokens.Add(new Token(TokenType.Statement, current));
                else if (isAllOperator(current)) tokens.Add(new Token(TokenType.Operator, current));
                else {
                    int num_out;
                    if (int.TryParse(current, out num_out)) tokens.Add(new Token(TokenType.Number, current));
                    else {
                        if (isWordLike(current))
                            tokens.Add(new Token(TokenType.WORD, current));
                    }
                }
            }
            return tokens.ToArray();
        }

        class Part
        {
            public Part(string raw)
            {
                Raw = raw;
            }
            public string Raw;
        }

        public bool isVariable(string text)
        {
            foreach (Variable v in Variables) if (v.Name == text) return true;
            return false;
        }

        public bool isWordLike(string word)
        {
            foreach (char c in word.ToCharArray())
            {
                int n_o;
                if (isOperator(c) || int.TryParse(c + "", out n_o)) return false;
            }
            return true;
        }

        public bool isStatement(string text)
        {
            foreach (string statement in Statements) if (statement == text) return true;
            return false;
        }

        public bool DoesOperatorTypeExist(string text)
        {
            foreach (KeyValuePair<string, OperatorType> pair in DeclaredOperators)
            {
                if (pair.Key == text) return true;
            }
            return false;
        }

        public OperatorType GetOperatorType(string text)
        {
            foreach (KeyValuePair<string, OperatorType> pair in DeclaredOperators)
            {
                if (pair.Key == text) return pair.Value;
            }
            return OperatorType.Pointer;
        }

        public VariableType ResolveVariableTypeRaw(string text)
        {
            foreach (KeyValuePair<string, VariableType> pair in DeclaredTypes) if (pair.Key == text) return pair.Value; return VariableType.UNKNOWN;
        }

        public VariableType ResolveDeclaredVariableType(string text)
        {
            foreach (KeyValuePair<string, VariableType> pair in DeclaredTypes) if (pair.Key == text) return pair.Value; return VariableType.UNKNOWN;
        }

        public Variable GetVariable(string name)
        {
            foreach (Variable variable in Variables) if (variable.Name == name) return variable; return null;
        }

        public bool isOperator(char character)
        {
            foreach (char c in SpecialCharacters)
                if (c == character)
                    return true;
            return false;
        }

        public bool isAllOperator(string text)
        {
            foreach (char c in text.ToCharArray())
                if (!isOperator(c))
                    return false;
            return true;
        }

        public class Token
        {
            public TokenType type;
            public string Raw = null;
            public bool hasRaw => Raw != null;
            public bool Equals(TokenType t) => type == t;
            public static bool operator ==(Token type, TokenType t) => type == t;
            public static bool operator !=(Token type, TokenType t) => type != t;
            public override string ToString() => $"RAW:({Raw}), TYPE:({type.ToString()})";

            public Token(TokenType type, string raw)
            {
                this.type = type;
                Raw = raw;
            }
        }

        public class Variable
        {
            private SnapCompiler compiler;

            public VariableType type = VariableType.UNKNOWN;

            public static int static_id = 0;

            public string Name;
            public bool Public = true;
            public int ID = static_id++;

            public string Tag => Public ? "" + ID : ":" + ID;

            public Variable(SnapCompiler compiler_, bool _Public)
            {
                compiler = compiler_;
                Public = _Public;
            }
        }

        public enum OperatorType
        {
            Pointer,//->
            Plus, Dash, Star, Slash, Equals, Comparison,
            ReverseComparison,//!=
            SelfAddition,//+=
            SelfRemove,//-=
            Comparison_Bigger,//>
            Comparison_Bigger_Equal,//>=
            Comparison_Smaller,//<
            Comparison_Smaller_Equal,//<=
            StaticReference,//::
            OpenBracket,
            CloseBrakcet,
        }

        public enum TokenType
        {
            Type, Operator, Number, Bool, Variable, Statement, WORD
        }

        public enum VariableType
        {
            BYTE, INT16, INT32, INT64, BOOL, UNKNOWN
        }

        private static int point_static_id = 0;

        public class Point
        {
            public int ID = point_static_id++;
            public string Name;
        }

        public class Function
        {
            public int ID = point_static_id++;
            public Point NoExecuteJumpPoint = new Point();
            public string Name;
            public List<string> parameters;
            public Function(string name, List<string> Params)
            {
                parameters = Params;
            }
        }

        public class SnapException : Exception
        {
            public SnapException(string message) : base(message)
            {

            }
        }
    }
}