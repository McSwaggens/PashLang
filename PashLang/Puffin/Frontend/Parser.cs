using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.AST;
using Puffin.Frontend.Symbols;
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
            while (node != null)
            {
                int count = node.Value.StatementTokens.Count;
                if (node.Value.StatementTokens.Last().Value.Equals("(") &&
                    !(node.Value.StatementTokens.ElementAt(count - 2) is OperatorToken))
                    // we have found a function definition
                {
                    Symbol<Information> sym = ParseFunction(node);
                    if (sym == null)
                        return false;
                   symbolTable.Symbols.Add(sym);
                }
                node = node.Next;
            }   
            return true;
        }

        private Symbol<Information> ParseFunction(LinkedListNode<Statement> node)
        {
            MethodData data = new MethodData();
            data.name = node.Value.StatementTokens.ElementAt(node.Value.Modifiers.Count + 1).Value;
            data.returnType = node.Value.TypeInformation;
            data.parameters = ParseParameters(node);
            if (data.parameters == null)
            {
                Logger.WriteError("There was an error parsing parameters");
                return null;
            }
            MethodInformation fnInfo = new MethodInformation(data.name,data.returnType,null);
            currrentScope = new Scope(currrentScope,fnInfo);
            fnInfo.Parameters = data.parameters.ToArray();
            MethodSymbol<Information> fnSymbol = new MethodSymbol<Information>(fnInfo);
            fnSymbol.IdentifierType = EnumSymbolType.FUNCTION;
            currrentScope = currrentScope.ParentScope;
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
                    switch (ty)
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
                            typeInformation = new StructInformation(nameof(Int16), (short) 0, true, false);
                            break;
                        case EnumKeywords.BYTE:
                            typeInformation = new StructInformation(nameof(Byte), (byte) 0, true, false);
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
                            typeInformation = new StructInformation(nameof(UInt32), (uint) 0, true, false);
                            break;
                        case EnumKeywords.UBYTE:
                            typeInformation = new StructInformation(nameof(Byte), (byte) 0, true, false);
                            break;
                        case EnumKeywords.USHORT:
                            typeInformation = new StructInformation(nameof(UInt16), (ushort) 0, true, false);
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
                            typeInformation = new StructInformation(typeof (void).ToString(), null, true, true);
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
                    ArrayInformation arrayInfo = new ArrayInformation(name,typeInformation,false,(int)items,null);
                    ArraySymbol<Information> arraySymbol = new ArraySymbol<Information>(arrayInfo);
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
                    ParameterData data = new ParameterData();

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
                if (symbolTable.Symbols.Any(
                        x => x.ValueType.Name.Equals(name) && x.ValueType.DefinitionScope.Equals(temp)))
                    return true;
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
    }
}
