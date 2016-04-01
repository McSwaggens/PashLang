using System;
using System.Collections.Generic;
using System.Linq;
using Puffin.Frontend.AST;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Symbols.Modifiers;
using Puffin.Frontend.Symbols.TypeInfo;
using Puffin.Frontend.Symbols.TypeInfo.DataStructs;
using Puffin.Frontend.Tokens;
using static Puffin.Logger;

namespace Puffin.Frontend
{
    public class Parser
    {
        private Lexer lexical;
        private LinkedList<Statement> statements;
        private SymbolTable<Symbol<Information>> symbolTable; 
        private Scope currrentScope = Scope.GLOBAL_SCOPE;

        public Parser(Lexer lexical)
        {
            this.lexical = lexical;
            this.statements = new LinkedList<Statement>();
            this.symbolTable = new SymbolTable<Symbol<Information>>();
        }

        public bool Start()
        {
            if (!ParseFirstPass())
                return false;
            if (!ParseSecondPass())
                return false;
            return true;
        }

        private bool ParseFirstPass()
        {
            LinkedListNode<Token> node = lexical.Tokens.First;
            while (node != null)
            {
                LinkedList<Token> temp = new LinkedList<Token>();
                while (node != null && !(node.Value.Value.Equals(";") || node.Value.Value.Equals("(") || node.Value.Value.Equals(")") 
                    || node.Value.Value.Equals(",") || node.Value.Value.Equals("}") || node.Value.Value.Equals("{")))
                {
                    if (node.Value.Value.Equals("\r") || node.Value.Value.Equals("\n"))
                    {
                        node = node.Next;
                        continue;
                    }
                    temp.AddLast(node.Value);
                    node = node.Next;
                    
                }
                if(node == null)
                    break;
                temp.AddLast(node?.Value);
                Statement stm = new Statement(new List<Token>(temp.ToList()), true, true);
                stm.DefineSymbols();
                if (stm.StatementTokens.Count == 1 &&
                    stm.StatementTokens[0].Type.Equals((Enum) EnumControlTokens.NEW_LINE))
                {
                    node = node?.Next;
                    temp.Clear();
                    continue;
                }
                    
                statements.AddLast(stm);
                node = node?.Next;
                temp.Clear();
            }
            Write("Parsed " + statements.Count + " statements");
            return true;
        }

        public bool ParseSecondPass()
        {
            LinkedListNode<Statement> node = statements.First;
            List<Information> pars = new List<Information>();
            string name = "";
            while (node != null)
            {
                int count = node.Value.StatementTokens.Count;
                try
                {
                    if (node.Value.StatementTokens.Last().Value.Equals(";") && GetTypes(node).Count == 1)
                        // we have found a variable local or global
                    {
                        Symbol<Information> sym = ParseVariable(node);
                        if (sym == null)
                        {
                            Logger.WriteError("Invalid variable declaration");
                            return false;
                        }
                        symbolTable.Symbols.Add(sym);
                    }
                    else if (node.Value.StatementTokens.Last().Value.Equals("(") &&
                             node.Value.StatementTokens.ElementAt(count - 2).Type.Equals((Enum) EnumKeywords.FOR))
                        // we have found a for loop
                    {
                        node = node.Next;
                        LinkedListNode<Statement> counter = node;
                        if (counter == null)
                        {
                            Logger.WriteError("For loop missing counter, condition and iterator");
                            return false;
                        }
                        node = node.Next;

                        LinkedListNode<Statement> condition = node;
                        if (condition == null)
                        {
                            Logger.WriteError("For loop missing condition and iterator");
                            return false;
                        }

                        node = node.Next;
                        LinkedListNode<Statement> iterator = node;
                        if (iterator == null)
                        {
                            Logger.WriteError("For loop missing iterator");
                            return false;
                        }
                        if (!ParseFor(counter, condition, iterator))
                        {
                            Logger.WriteError("invalid for statement");
                            return false;
                        }
                    }
                    else if (node.Value.StatementTokens.Last().Value.Equals("(") &&
                             node.Value.StatementTokens.ElementAt(count - 2).Type.Equals((Enum) EnumKeywords.IF))
                    {
                        node = node.Next;
                        if (!ParseIf(node))
                        {
                            Logger.WriteError("Invalid if statement");
                            return false;
                        }
                    }
                    else if (node.Value.StatementTokens.Last().Value.Equals("(") &&
                             (node.Value.StatementTokens.Count == 2 ||
                              node.Value.StatementTokens.ElementAt(count - 3) is OperatorToken || 
                              node.Value.StatementTokens.ElementAt(count - 4) is IdentifierToken)) // We have found a function call
                    {
                        Logger.WriteWarning("Found Function call");
                        name = node.Value.StatementTokens.ElementAt(
                            node.Value.StatementTokens.Count - 2).Value;
                        if (!isDefinedInScope(name))
                        {
                            Logger.WriteError("Function " + node.Value.StatementTokens.ElementAt(
                                node.Value.StatementTokens.Count - 2).Value
                                + "Does not exist");
                            return false;
                        }
                            //node = node.Next;
                        if (node.Value.StatementTokens.Count > 1 &&
                            !node.Value.StatementTokens.First().Value.Equals(")"))
                        {
                            pars = ParseCallingParameters(node);
                            if (pars != null)
                            {
                                if (pars.Count == 0)
                                {
                                    if (!FunctionWithParametersDefined(name, pars))
                                    {
                                        Logger.WriteError("Function with no parameters" + name +
                                                          " is not defined in this scope");
                                        return false;
                                    }
                                }
                                if (!FunctionWithParametersDefined(name, pars))
                                {
                                    string parameterSigniture = "";
                                    foreach (Information par in pars)
                                    {
                                        if (par is ParameterInformation)
                                            parameterSigniture += ((ParameterInformation) par).IdentifierType.Name + ",";
                                    }
                                    Logger.WriteError("Function " + name + " with parameters " + parameterSigniture +
                                                      " not defined in this scope");
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (node.Value.StatementTokens.Last().Value.Equals("(") &&
                             !(node.Value.StatementTokens.ElementAt(count - 2) is OperatorToken))
                        // we have found a function definition
                    {
                        Symbol<Information> sym = ParseFunction(node);
                        if (sym == null)
                            return false;
                        symbolTable.Symbols.Add(sym);
                    }
                    else if (node.Value.StatementTokens.Count == 1 &&
                             node.Value.StatementTokens.First().Value.Equals("}")) // We have found end of a scope
                    {
                        currrentScope = currrentScope.ParentScope;
                    }
                    node = node.Next;
                }
                catch (NullReferenceException ex)
                {
                    Logger.WriteCritical(ex.StackTrace);
                    node = node.Next;
                    continue;
                }
                catch (IndexOutOfRangeException ex2)
                {
                    Logger.WriteCritical(ex2.StackTrace);
                    node = node.Next;
                    continue;
                }
            }
            if (currrentScope != Scope.GLOBAL_SCOPE)
            {
                Logger.WriteError("Missing }");
                return false;
            }
            return true;
        }

        private List<Information> ParseCallingParameters(LinkedListNode<Statement> node)
        {
            ParameterInformation parameterInformation;
            ParameterData data = new ParameterData();
            Information typeInformation = null;
            EnumKeywords ty;
            string name;
            bool isOut = false;
            bool isRef = false;
            bool isPointer = false;
            int index = 0;
            bool isArray = false;
            List<Information> pars = new List<Information>();

            if (node.Value.StatementTokens.First() is KeywordToken &&
                node.Value.StatementTokens.Last().Value.Equals("("))
                node = node.Next;
            do
            {
                if (node != null)
                {
                    if (node.Value.StatementTokens[index].Value.Equals(")"))
                        return new List<Information>();

                    if (node.Value.StatementTokens[index].Value.Equals("out"))
                    {
                        isOut = true;
                        index++;
                    }
                    if (node.Value.StatementTokens[index].Value.Equals("ref"))
                    {
                        if (isOut)
                        {
                            Logger.WriteError("Parameters can not be defined out and ref");
                            return null;
                        }
                        isRef = true;
                        index++;
                    }
                    if (node.Value.StatementTokens[index].Value.EndsWith("*"))
                    {
                        if (isOut || isRef)
                        {
                            Logger.WriteError("Out or ref parameters can not be pointers");
                            return null;
                        }
                        isPointer = true;
                    }
                    if (node.Value.StatementTokens[index] is IdentifierToken)
                    {
                        if (isDefinedInScope(node.Value.StatementTokens[index].Value))
                        {
                            var symbol = symbolTable.Symbols.FirstOrDefault(
                                x => x.IdentifierName.Equals(node.Value.StatementTokens[index].Value));
                            if (symbol != null)
                            {
                                Information inf = symbol.ValueType;
                                pars.Add(inf);
                            }
                        }
                        else
                        {
                            Logger.WriteError("Parameter " + node.Value.StatementTokens[index].Value + " is not defined in this scope");
                            return null;
                        }
                    }
                    node = node.Next;
                }
            } while (node != null && node.Value.StatementTokens.Last().Value.Equals(","));
            return pars;
        }

        private bool FunctionWithParametersDefined(string name, List<Information> pars)
        {
            List<Symbol<Information>> functions =
                symbolTable.Symbols.Where(x => x.IdentifierType == EnumSymbolType.FUNCTION).ToList();
            foreach (Symbol<Information> symbol in functions)
            {
                if (symbol is MethodSymbol<Information>)
                {
                    MethodSymbol<Information> temp = (MethodSymbol<Information>) symbol;
                    if (temp.Info.Name.Equals(name))
                    {
                        if (pars.Count != temp.Info.Parameters.Length)
                        {
                            Logger.WriteError("Function " + name + " requires " + temp.Info.Parameters.Length + " Parameters found " + pars.Count);
                            return false;
                        }
                        
                        foreach (Information par in pars)
                        {
                            if (!temp.Info.HasParameter(par))
                                return false;
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ParseIf(LinkedListNode<Statement> node)
        {
            Symbol<Information> counterSymbol = new VariableSymbol<Information>(new StructInformation("if",null,true,true));
            Scope ifScope = new Scope(currrentScope, counterSymbol);
            currrentScope = ifScope;
            if (!ParseBoolean(node))
            {
                Logger.WriteError("Invalid if condition");
                return false;
            }
            return true;
        }

        private List<Token> GetTypes(LinkedListNode<Statement> node)
        {
            return node.Value.StatementTokens.Where(x => ParseType(x.Type)).ToList();
        }

        private Symbol<Information> ParseVariable(LinkedListNode<Statement> node)
        {
            VariableData data = new VariableData();
            data.name = node.Value.StatementTokens.ElementAt(node.Value.Modifiers.Count + 1).Value;
            data.type = node.Value.TypeInformation;
            data.isConstant = node.Value.Modifiers.Any(x => x.Value.Equals(EnumModifiers.CONST));
            data.isPointer = node.Value.StatementTokens.ElementAt(node.Value.Modifiers.Count).Value.EndsWith("*");
            if (node.Value.StatementTokens.Count >= node.Value.Modifiers.Count + 3)
            {
                if (!node.Value.StatementTokens.ElementAt(node.Value.Modifiers.Count + 2)
                        .Type.Equals((Enum) EnumOperators.ASSIGNMENT))
                {
                    Logger.WriteError("Invalid Variable initialiser");
                    return null;
                }
                data.initialvalue = (object) node.Value.StatementTokens.ElementAt(node.Value.Modifiers.Count + 3);
            }
            if (isDefinedInScope(data.name))
            {
                Logger.WriteError("Variable " + data.name + " is already defined in this scope");
                return null;
            }
            VariableInformation info = new VariableInformation(data.name, data.type, data.isConstant, data.isPointer, data.initialvalue);
            info.DefinitionScope = currrentScope;
            VariableSymbol<Information> sym = new VariableSymbol<Information>(info);
            sym.IdentifierType = EnumSymbolType.VARIABLE;
            return sym; //temp
        }

        private bool ParseType(Enum ty)
        {
            if(ty is EnumKeywords) { 
            Information info;
                switch ((EnumKeywords)ty)
                {
                    case EnumKeywords.INT:
                        info = new StructInformation(nameof(Int32), 0, true, false);
                        return true;
                    case EnumKeywords.BOOLEAN:
                        info = new StructInformation(nameof(Boolean), false, true, false);
                        return true;
                    case EnumKeywords.LONG:
                        info = new StructInformation(nameof(Int64), 0L, true, false);
                        return true;
                    case EnumKeywords.SHORT:
                        info = new StructInformation(nameof(Int16), (short) 0, true, false);
                        return true;
                    case EnumKeywords.BYTE:
                        info = new StructInformation(nameof(Byte), (byte) 0, true, false);
                        return true;
                    case EnumKeywords.CHAR:
                        info = new StructInformation(nameof(Char), '\0', true, false);
                        return true;
                    case EnumKeywords.FLOAT:
                        info = new StructInformation(nameof(Single), 0.0f, true, false);
                        return true;
                    case EnumKeywords.DOUBLE:
                        info = new StructInformation(nameof(Double), 0.0, true, false);
                        return true;
                    case EnumKeywords.DATASET:
                        Logger.WriteWarning("Datasets are not dealt with yet");
                        return true;
                    case EnumKeywords.UINT:
                        info = new StructInformation(nameof(UInt32), (uint) 0, true, false);
                        return true;
                    case EnumKeywords.UBYTE:
                        info = new StructInformation(nameof(Byte), (byte) 0, true, false);
                        return true;
                    case EnumKeywords.USHORT:
                        info = new StructInformation(nameof(UInt16), (ushort) 0, true, false);
                        return true;
                    case EnumKeywords.ULONG:
                        info = new StructInformation(nameof(UInt64), 0UL, true, false);
                        return true;
                    case EnumKeywords.OBJECT:
                        info = new ClassInformation(nameof(Object), null, true, true);
                        return true;
                    case EnumKeywords.STRING:
                        info = new ClassInformation(nameof(String), "", true, true);
                        return true;
                    case EnumKeywords.VOID:
                        info = new StructInformation(typeof (void).ToString(), null, true, true);
                        return true;
                    default:
                        Logger.WriteWarning("User Defined types are not dealt with yet");
                        return false;
                }
            }
            return false;
        }

        private bool ParseFor(LinkedListNode<Statement> counter, LinkedListNode<Statement> condition, LinkedListNode<Statement> iterator)
        {
            Symbol<Information> counterSymbol = new VariableSymbol<Information>(new StructInformation("if", null, true, true));
            Scope forScope = new Scope(currrentScope,counterSymbol);
            currrentScope = forScope;
            if (ParseType(counter.Value.StatementTokens.First().Type))
            {
                 counterSymbol = ParseVariable(counter);
                if (counterSymbol == null)
                {
                    Logger.WriteError("Invalid counter variable declaration: " + counter.Value.ToString());
                    return false;
                }
            }
            else
            {
                if (!isDefinedInScope(counter.Value.StatementTokens.First().Value))
                {
                    Logger.WriteError("Variable " + counter.Value.StatementTokens.First().Value + " is undefined in this scope");
                    return false;
                }
            }
            if (!ParseBoolean(condition))
            {
                Logger.WriteError("For loop condition " + condition.Value.ToString() + " Does not evaluate to bool");
                return false;
            }
            if (!ParseIterator(iterator))
            {
                Logger.WriteError("Invalid for loop iterator " + iterator.Value.ToString());
                return false;
            }
            return true;
        }

        private bool ParseIterator(LinkedListNode<Statement> iterator)
        {
            if (!isDefinedInScope(iterator.Value.StatementTokens.First().Value))
            {
                Logger.WriteError("Variable " + iterator.Value.StatementTokens.First().Value + " is undefined in this scope");
                return false;
            }
            if (!ParseAssignment(iterator))
            {
                Logger.WriteError("Iterator must assign to a variable");
                return false;
            }
            return true;
        }

        private bool ParseAssignment(LinkedListNode<Statement> iterator)
        {
            if (!iterator.Value.StatementTokens.Any(x => x is OperatorToken))
            {
                Logger.WriteError("Assignment operations must have at least 1 operator");
                return false;
            }
            List<Token> ops = new List<Token>(iterator.Value.StatementTokens.Where(x => x is OperatorToken));
            foreach (Token op in ops)
            {
                switch ((EnumOperators) op.Type)
                {
                    case EnumOperators.ASSIGNMENT:
                        return true;
                    case EnumOperators.BITWISE_AND_ASSIGNMENT:
                        return true;
                    case EnumOperators.BITWISE_LEFT_SHIFT_ASSIGNMENT:
                        return true;
                    case EnumOperators.BITWISE_OR_ASSIGNMENT:
                        return true;
                    case EnumOperators.BITWISE_RIGHT_SHIFT_ASSIGNMENT:
                        return true;
                    case EnumOperators.BITWISE_XOR_ASSIGNMENT:
                        return true;
                    case EnumOperators.ADDITION_ASSIGNMENT:
                        return true;
                    case EnumOperators.MULTIPLICATION_ASSIGNMENT:
                        return true;
                    case EnumOperators.DIVISION_ASSIGNMENT:
                        return true;
                    case EnumOperators.SUBTRACTION_ASSIGNMENT:
                        return true;
                    case EnumOperators.MODULO_ASSIGNMENT:
                        return true;
                    case EnumOperators.INCREMENT_POSTFIX:
                        return true;
                    case EnumOperators.INCREMENT_PREFIX:
                        return true;
                    case EnumOperators.DECREMENT_POSTFIX:
                        return true;
                    case EnumOperators.DECREMENT_PREFIX:
                        return true;
                    default:
                        continue;
                }
            }
            return false;
        }

        private bool ParseBoolean(LinkedListNode<Statement> condition)
        {
            if (condition.Value.StatementTokens.Any(x => x is OperatorToken) &&
                condition.Value.StatementTokens.Count >= 3)
            {
                List<Token> ops = new List<Token>(condition.Value.StatementTokens.Where(x => x is OperatorToken));
                foreach (Token op in ops)
                {
                    switch ((EnumOperators) op.Type)
                    {
                        case EnumOperators.EQUALITY:
                            return true;
                        case EnumOperators.NOT_EQUAL:
                            return true;
                        case EnumOperators.LESS_THAN:
                            return true;
                        case EnumOperators.LESS_THAN_AND_EQUAL:
                            return true;
                        case EnumOperators.GREATER_THAN:
                            return true;
                        case EnumOperators.GREATER_THAN_AND_EQUAL:
                            return true;
                        case EnumOperators.LOGICAL_AND:
                            return true;
                        case EnumOperators.LOGICAL_NOT:
                            return true;
                        case EnumOperators.LOGICAL_OR:
                            return true;
                        default:
                            continue;
                    }  
                }
            }
            else if (condition.Value.StatementTokens.Last() is BooleanLiteralToken)
                return true;
            return false;
        }

        private Symbol<Information> ParseFunction(LinkedListNode<Statement> node)
        {
            MethodData data = new MethodData();
            data.name = node.Value.StatementTokens.ElementAt(node.Value.Modifiers.Count + 1).Value;
            data.returnType = node.Value.TypeInformation;
            data.parameters = ParseParameters(node);
            if (data.parameters == null)
            {
                //Logger.WriteWarning("function has no parameters");
                MethodInformation functionInfo = new MethodInformation(data.name, data.returnType,new Information[0]);
                functionInfo.DefinitionScope = currrentScope;
                currrentScope = new Scope(currrentScope, functionInfo);
                functionInfo.Modifiers = node.Value.Modifiers;
                MethodSymbol<Information> functionSymbol = new MethodSymbol<Information>(functionInfo);
                functionSymbol.IdentifierType = EnumSymbolType.FUNCTION;
                return functionSymbol;

            }
            MethodInformation fnInfo = new MethodInformation(data.name,data.returnType,null);
            fnInfo.DefinitionScope = currrentScope;
            currrentScope = new Scope(currrentScope,fnInfo);
            fnInfo.Parameters = data.parameters.ToArray();
            fnInfo.Modifiers = node.Value.Modifiers;
            MethodSymbol<Information> fnSymbol = new MethodSymbol<Information>(fnInfo);
            fnSymbol.IdentifierType = EnumSymbolType.FUNCTION;
            //currrentScope = currrentScope.ParentScope; // TODO Move this to where we find a statement containing only '}'
            return fnSymbol;
        }

        private List<Information> ParseParameters(LinkedListNode<Statement> node)
        {
            List<Information> pars = new List<Information>();
            if (node.Value.StatementTokens.First() is KeywordToken &&
                node.Value.StatementTokens.Last().Value.Equals("("))
                node = node.Next; // Advance the parser to get to the parameter list
            do
            {
                ParameterInformation parameterInformation;
                ParameterData data = new ParameterData();
                Information typeInformation = null;
                EnumKeywords ty;
                string name;
                bool isOut = false;
                bool isRef = false;
                bool isPointer = false;
                int index = 0;
                bool isArray = false;
                if (node.Value.StatementTokens[index].Value.Equals("out"))
                {
                    isOut = true;
                    index++;
                }
                if (node.Value.StatementTokens[index].Value.Equals("ref"))
                {
                    if (isOut)
                    {
                        Logger.WriteError("Parameters can not be defined out and ref");
                        return null;
                    }
                    isRef = true;
                    index++;
                }
                if (node.Value.StatementTokens[index].Value.EndsWith("*"))
                {
                    if (isOut || isRef)
                    {
                        Logger.WriteError("Out or ref parameters can not be pointers");
                        return null;
                    }
                    isPointer = true;
                }
                if (!Enum.TryParse(node.Value.StatementTokens[index].Value.Replace("*", "").ToUpper(), out ty))
                {
                    Logger.WriteWarning("User defined type " + node.Value.StatementTokens[index].Value +
                                        " are not dealt with yet");
                    return null;
                }

                if (((int) ty >= 0x04 && (int) ty <= 0x0F) || ((int) ty >= 0x30 && (int) ty <= 0x33) || (int) ty == 0x45 ||
                    (int) ty == 0x46)
                {
                    switch ((EnumKeywords)ty)
                    {
                        case EnumKeywords.INT:
                            typeInformation = new StructInformation(nameof(Int32), 0, true, false);
                            break;
                        case EnumKeywords.BOOLEAN:
                            typeInformation = new StructInformation(nameof(Boolean), false, true, false);
                            break;
                        case EnumKeywords.LONG:
                            typeInformation = new StructInformation(nameof(Int64), 0L, true, false);
                            break;
                        case EnumKeywords.SHORT:
                            typeInformation = new StructInformation(nameof(Int16), (short)0, true, false);
                            break;
                        case EnumKeywords.BYTE:
                            typeInformation = new StructInformation(nameof(Byte), (byte)0, true, false);
                            break;
                        case EnumKeywords.CHAR:
                            typeInformation = new StructInformation(nameof(Char), '\0', true, false);
                            break;
                        case EnumKeywords.FLOAT:
                            typeInformation = new StructInformation(nameof(Single), 0.0f, true, false);
                            break;
                        case EnumKeywords.DOUBLE:
                            typeInformation = new StructInformation(nameof(Double), 0.0, true, false);
                            break;
                        case EnumKeywords.DATASET:
                            Logger.WriteWarning("Datasets are not dealt with yet");
                            break;
                        case EnumKeywords.UINT:
                            typeInformation = new StructInformation(nameof(UInt32), (uint)0, true, false);
                            break;
                        case EnumKeywords.UBYTE:
                            typeInformation = new StructInformation(nameof(Byte), (byte)0, true, false);
                            break;
                        case EnumKeywords.USHORT:
                            typeInformation = new StructInformation(nameof(UInt16), (ushort)0, true, false);
                            break;
                        case EnumKeywords.ULONG:
                            typeInformation = new StructInformation(nameof(UInt64), 0UL, true, false);
                            break;
                        case EnumKeywords.OBJECT:
                            typeInformation = new ClassInformation(nameof(Object), null, true, true);
                            break;
                        case EnumKeywords.STRING:
                            typeInformation = new ClassInformation(nameof(String), "", true, true);
                            break;
                        case EnumKeywords.VOID:
                            typeInformation = new StructInformation(typeof(void).ToString(), null, true, true);
                            break;
                        default:
                            Logger.WriteWarning("User Defined types are not dealt with yet");
                            break;
                    }
                    index++;
                }
                if (node.Value.StatementTokens[index].Value.Equals("["))
                {
                    uint items;
                    isArray = true;
                    index++;
                    if (node.Value.StatementTokens[index].Value.Equals("]"))
                    {
                        items = 0;
                        index--;
                        Logger.WriteWarning("No array size specified for array: " + node.Value.StatementTokens[index + 2].Value);
                    }
                    else if (!uint.TryParse(node.Value.StatementTokens[index].Value, out items))
                    {
                        Logger.WriteError("Invalid array Size " + node.Value.StatementTokens[index].Value);
                        return null;
                    }

                    index++;

                    if (!node.Value.StatementTokens[index].Value.Equals("]"))
                    {
                        Logger.WriteError("Unterminated array declaration");
                        return null;
                    }
                    index++;
                    if (node.Value.StatementTokens.Count - 3 > index)
                    {
                        index++;
                        if (node.Value.StatementTokens[index].Type.Equals((Enum) EnumOperators.ASSIGNMENT))
                        {
                            name = node.Value.StatementTokens[index - 1].Value;
                            index += 2;
                            Logger.WriteWarning("TODO Implement optional values for arrays");
                        }
                        else
                        {
                            Logger.WriteError("Invalid optional array parameter declaration");
                            return null;
                        }
                    }
                    else
                    {
                        name = node.Value.StatementTokens[index].Value;
                    }
                    data.isOut = isOut;
                    data.isReference = isRef;
                    data.isPointer = isPointer;
                    data.name = name;
                    data.type = typeInformation;
                    ArrayParameterInformation arrayInfo = new ArrayParameterInformation(data.name,typeInformation,data.isReference,data.isPointer,data.isOptional,data.isOut,null);
                    ArrayParameterSymbol<Information> arraySymbol = new ArrayParameterSymbol<Information>(arrayInfo);
                    arraySymbol.IdentifierType = EnumSymbolType.ARRAY;
                    arrayInfo.DefinitionScope = currrentScope;
                    symbolTable.Symbols.Add(arraySymbol);
                    pars.Add(arrayInfo);
                    node = node.Next;
                    continue;

                    //Logger.WriteWarning("TODO Parse arrays");
                    //index += 2;
                }
                else
                {
                    isArray = false;
                }
                if (node.Value.StatementTokens.Count - 1 < index ||
                    !(node.Value.StatementTokens[index] is IdentifierToken))
                {
                    Logger.WriteError("Invalid parameter definition");
                    return null;
                }
                name = node.Value.StatementTokens[index].Value;
                index++;
                if (isDefinedInScope(name))
                {
                    Logger.WriteError("a symbol named: " + name + " Is already defined in this scope");
                    return null;
                }
                    
                if (node.Value.StatementTokens.Count - 1 == index)
                {
                    data.isOptional = false;
                }
                else
                {
                    if (!node.Value.StatementTokens[index].Type.Equals((Enum) EnumOperators.ASSIGNMENT))
                    {
                        Logger.WriteError("Invalid Optional Parameter declaration");
                        return null;
                    }
                    data.isOptional = true;
                    data.defaultValue = (object) node.Value.StatementTokens[index + 1].Value;
                }
                data.isOut = isOut;
                data.isReference = isRef;
                data.isPointer = isPointer;
                data.name = name;
                data.type = typeInformation;
                ParameterInformation info = new ParameterInformation(data.name, data.type, data.isReference,
                    data.isPointer, data.isOptional, data.isOut,data.defaultValue);
                ParameterSymbol<Information> sym = new ParameterSymbol<Information>(info);
                sym.IdentifierType = EnumSymbolType.PARAMETER;
                info.DefinitionScope = currrentScope;
                symbolTable.Symbols.Add(sym);
                pars.Add(info);
                node = node.Next;
            } while (node.Previous.Value.StatementTokens.Last().Value.Equals(","));
            return pars;
        }

        public bool isDefinedInScope(string name)
        {
            Scope temp = currrentScope;
            while (temp != null)
            {
                foreach (Symbol<Information> sym in symbolTable.Symbols)
                {
                    if (sym is MethodSymbol<Information>)
                    {
                        MethodSymbol<Information> tempsym = (MethodSymbol<Information>) sym;
                        if (tempsym.Info.Name.Equals(name) && tempsym.Info.DefinitionScope.Equals(temp))
                            return true;
                    }
                    else
                    {
                        if (sym.IdentifierName.Equals(name) && sym.ValueType.DefinitionScope.Equals(temp))
                            return true;
                    }
                }
                temp = temp.ParentScope;
            }
            return false;
        }

        public void PrintSymbols()
        {
            foreach (Symbol<Information> sym in symbolTable.Symbols)
            {
                Logger.WriteColor(sym.ToString(),ConsoleColor.Green);
            }
        }

        public void PrintStatements()
        {
            foreach (Statement stm in statements)
            {
                Logger.WriteColor(stm.ToString(),ConsoleColor.Cyan);
            }
        }

        public LinkedList<Statement> Statements
        {
            get { return statements; }
        }

        public SymbolTable<Symbol<Information>> SymbolTable
        {
            get { return symbolTable; }
        }
    }
}
