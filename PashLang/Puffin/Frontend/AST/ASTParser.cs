using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.AST.Nodes;
using Puffin.Frontend.AST.Nodes.Primitives;
using Puffin.Frontend.Symbols;

namespace Puffin.Frontend.AST
{
    public class ASTParser
    {
        private Parser parse;
        private LinkedList<BaseASTNode> nodes; 
        public ASTParser(Parser parse)
        {
            if (parse == null)
            {
                Logger.WriteError("Can not parse AST with no parsed statements");
                return;
            }
            this.parse = parse;
            this.nodes = new LinkedList<BaseASTNode>();
        }

        public BaseASTNode ParseAST()
        {
            BaseASTNode root = new BaseASTNode();
            foreach (Statement smt in parse.Statements)
            {
                Symbol<Information> sym =
                    parse.SymbolTable.Symbols.FirstOrDefault(x => x.IdentifierName.Equals(smt.TypeInformation.Name));
                if (sym is VariableSymbol<Information>)
                {
                    BaseASTNode node = ParseVariableNode(smt, sym, root);
                    if (node == null)
                    {
                        Logger.WriteCritical("null node found when parsing variables in AST");
                        return null;
                    }
                    nodes.AddLast(node);
                }
                else // This node must be an expression such as a function definition / call or other construct
                {
                    BaseASTNode node = ParseExpressionNode(smt, sym, root);
                    if (node == null)
                    {
                        #if DEBUG
                            Logger.WriteDebug("AST Can not handle " + smt.ToMultilineString() + "\n" + "yet this functionality will be added in a future Release");
                            return BaseASTNode.EMPTY; // TODO implement all node types
                        #else
                            Logger.WriteCritical("null node found when parsing expressions in AST");
                            return null;
                        #endif
                    }
                    nodes.AddLast(node);
                }


            }
            return null;
        }

        private BaseASTNode ParseVariableNode(Statement smt, Symbol<Information> sym, BaseASTNode parent)
        {
            string nameofVoid = typeof (void).ToString();
            switch (smt.TypeInformation.Name)
            {
                case nameof(Byte):
                    return new ByteASTNode((VariableSymbol<Information>)sym, parent);
                case "S" + nameof(Int16):
                    return new ShortASTNode((VariableSymbol<Information>)sym, parent);
                case "S" + nameof(Int32):
                    return new IntegerASTNode((VariableSymbol<Information>)sym, parent);
                case "S" + nameof(Int64):
                    return new LongASTNode((VariableSymbol<Information>)sym, parent);
                case nameof(UInt16):
                    return new UnsignedShortASTNode((VariableSymbol<Information>)sym, parent);
                case nameof(UInt32):
                    return new UnsignedIntegerASTNode((VariableSymbol<Information>)sym, parent);
                case nameof(UInt64):
                    return new UnsignedLongASTNode((VariableSymbol<Information>)sym, parent);
                case "Float":
                    return new FloatASTNode((VariableSymbol<Information>)sym, parent);
                case nameof(Double):
                    return new DoubleASTNode((VariableSymbol<Information>)sym, parent);
                case nameof(Object):
                    return new ObjectASTNode((VariableSymbol<Information>)sym, parent);
                case nameof(String):
                    return new StringASTNode((VariableSymbol<Information>)sym, parent);
                case "System.Void":
                    if (!sym.IsPointer)
                    {
                        Logger.WriteError("Cannot initialise a variable of type void did you mean void*?");
                        return null;
                    }
                    return new VoidASTNode((VariableSymbol<Information>)sym, parent);
                default:
                    Logger.WriteWarning("Type " + smt.TypeInformation.Name + " Does not have an AST node associated with it yet");
                    return null;
            }
        }

        private BaseASTNode ParseExpressionNode(Statement smt, Symbol<Information> sym, BaseASTNode parent)
        {
            Logger.WriteWarning("Expression nodes can not be parsed yet");
            return null;
        }
    }
}
