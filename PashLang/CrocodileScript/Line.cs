using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrocodileScript
{
    public class Line
    {
        public Block AttachedBlock;
        public Function ParentFunction;
        public bool inFunction { get { return ParentFunction != null; } }
        private CrocCompiler compiler;
        public Line(string rep, CrocCompiler compiler, Function func = null)
        {
            this.compiler = compiler;
            this.rep = rep.TrimStart(' ', '\t');
        }

        public Line(string rep, Block attachedBlock, CrocCompiler compiler, Function func = null)
        {
            this.compiler = compiler;
            this.rep = rep.TrimStart(' ', '\t');
            AttachedBlock = attachedBlock;
        }

        public void Compile()
        {
            if (!inFunction)
            {
                //Check for local syntax, such as public, static, private, local declorations (#), class, enum and function
                if (rep.StartsWith("#"))
                {
                    string[] split = rep.TrimStart('#').Split(' ');
                    if (split[0] == "declare")
                    {
                        //Declare clause
                        if (split[1] == "StartFunction")
                        {
                            //Set the function that will be used to start from when the process starts.
                            compiler.StartFunction = split[2];
                            ///Console.WriteLine ("Set Start Function to \"" + compiler.StartFunction + "\"");
                        }
                    }
                    else if (split[0] == "import")
                    {
                        compiler.Imports.Add(split[1]);
                        compiler.CompiledCode.Add("im " + split[1]);
                        ///Console.WriteLine ("Added import reference \"" + split [1] + "\"");
                    }
                }
                else
                {
                    string s = "";
                    bool isPublic = false, isStatic = false;
                    int i = 0;
                    char[] stack = rep.ToCharArray();
                    foreach (char c in stack)
                    {
                        if (c == ' ' || c == '\t')
                        {
                            if (s == "public")
                            {
                                if (isPublic) throw new Exception("Syntax error at: public");

                                isPublic = true;
                            }
                            else if (s == "static")
                            {
                                if (isStatic) throw new Exception("Syntax error at: static");
                                isStatic = true;
                            }
                            else
                            {
                                foreach (VariableType type in Enum.GetValues(typeof(VariableType)))
                                    if ((type.ToString().ToLower()) == s)
                                    {
                                        string Name = "";
                                        i++;
                                        for (;i < stack.Length;i++)
                                        {
                                            if (stack[i] == ' ' || stack[i] == '\t' || stack[i] == '(') break;
                                            else Name += stack[i];
                                        }
                                        char n = getNextChar(stack, i);
                                        if (n != '\0' && n == '(')
                                        {
                                            if (AttachedBlock != null)
                                            {
                                                Function function = new Function(Name, isPublic, isStatic);
                                                function.block = AttachedBlock;
                                                compiler.Functions.Add(function);
                                            }
                                            else throw new SyntaxException("Cannot compile function: " + Name + " no block/code attached.", SyntaxError.BlockMissing);
                                            return;
                                        }
                                        else
                                        {
                                            bool doesRegisterValue = n != '\0' && n == '=';

                                            string value = "";
                                            if (doesRegisterValue)
                                            {
                                                i += 2;
                                                for (; i < stack.Length; i++)
                                                {
                                                    value += stack[i];
                                                }
                                            }

                                            Variable v = new Variable(Name, isPublic, isStatic);
                                            v.type = type;
                                            v.RawUnassignedValue = doesRegisterValue ? value : null;
                                            compiler.Variables.Add(v);

                                            compiler.Calculate(value);

                                            return;
                                        }
                                    }
                                throw new Exception("Unknown variable type: " + s);
                            }
                            s = "";
                        }
                        else s += c;
                        i++;
                    }
                }
            }
        }

        private static char getNextChar(char[] arr, int index)
        {
            for(;index < arr.Length;index++)
            {
                char c = arr[index];
                if (c != ' ' && c != '\"') return c;
            }
            return '\0';
        }

        string rep;
    }
}
