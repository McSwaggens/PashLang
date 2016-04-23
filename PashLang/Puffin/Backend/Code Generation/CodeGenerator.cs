using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Command_Line_Args;
using Puffin.Frontend;
using Puffin.Frontend.AST;
using Puffin.Frontend.AST.Nodes;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Tokens;

namespace Puffin.Backend.Code_Generation
{
    public class CodeGenerator
    {
        private Parser parser;
        private CommandLineParser args;
        private BaseASTNode astRoot;
        private long colonCount;
        private long globalCount;
        private List<Tuple<long, string, string>> vars = new List<Tuple<long, string, string>>();

        public CodeGenerator(Parser parser, CommandLineParser args)
        {
            this.parser = parser;
            this.args = args;
        }

        public CodeGenerator(BaseASTNode astRoot, CommandLineParser args)
        {
            this.astRoot = astRoot;
            this.args = args;
            Logger.WriteWarning("AST is not fully implemented yet we recommend that you use CodeGenerator(Parser, CommandLineParser) \n" +
                                "instead as some statements may not be generated correctly");
        }

        public StreamWriter CreateOutputFile()
        {
            StreamWriter writer;
            if (args != null && !args.OutputFile.Equals(""))
                writer = new StreamWriter(args.OutputFile,false);
            else
                writer = new StreamWriter("output.p",false);
            return writer;
        }
        public bool GenerateInstructions()
        {
            colonCount = 0;
            globalCount = 0;

            StreamWriter writer = CreateOutputFile();
            if (parser != null)
            {
                foreach (Statement smt in parser.Statements)
                {
                    List<Token> tokens = smt.StatementTokens.Where(x => (x is OperatorToken)).ToList();
                    foreach (Token tok in tokens)
                    {
                        OperatorToken op = (OperatorToken) tok;
                        switch ((EnumOperators) op.Type)
                        {
                            case EnumOperators.ASSIGNMENT:
                                if (!(smt.StatementTokens.ElementAt(smt.StatementTokens.IndexOf(tok) + 2) is OperatorToken))
                                {
                                    string ident = smt.StatementTokens.First(x => (x is IdentifierToken)).Value;
                                    if (!vars.Any(x => x.Item2.Equals(ident)))
                                    {
                                        vars.Add(new Tuple<long, string, string>(colonCount, ident, GetIdentiferType(ident)));
                                        writer.Write("set :" + colonCount + " ");
                                        colonCount++;
                                    }
                                    else
                                    {
                                        writer.Write("set :" + vars.First(x => x.Item2.Equals(ident)).Item1 + " ");
                                    }

                                    if (smt.TypeInformation != null)
                                        writer.Write(smt.TypeInformation.Name.ToUpper() + " ");
                                    else
                                        writer.Write(GetIdentiferType(vars.First(x => x.Item1 == colonCount - 1).Item2) + " ");
                                    if(vars.Any(x => x.Item2.Equals(smt.StatementTokens.ElementAt(smt.StatementTokens.IndexOf(tok) + 1).Value)))
                                        writer.Write(":" + vars.IndexOf(vars.First(x => x.Item2.Equals(smt.StatementTokens.ElementAt(smt.StatementTokens.IndexOf(tok) + 1).Value))));
                                    else
                                        writer.Write(smt.StatementTokens.ElementAt(smt.StatementTokens.IndexOf(tok) + 1).Value + " ");
                                }
                                break;
                            case EnumOperators.BINARY_PLUS:
                                writer.Write("set :" + colonCount + " ");
                                writer.Write("QMATH " + smt.StatementTokens.ElementAt(smt.StatementTokens.IndexOf(tok) - 1).Value + " + " 
                                    + smt.StatementTokens.ElementAt(smt.StatementTokens.IndexOf(tok) + 1).Value);
                                //writer.Write("QMATH " + );
                                writer.WriteLine();
                                break;
                            default:
                                Logger.WriteDebug("Operator " + op.ToString() + " is not handled yet");
                                break;
                        }
                    }
                    if (tokens.Count > 0) 
                        writer.WriteLine();
                }
            }
            writer.Flush();
            writer.Close();
            writer.Dispose();
            return true;
        }

        private string GetIdentiferName(string ident)
        {
            if (parser.SymbolTable.Symbols.First(x => x.IdentifierName.Equals(ident)).IdentifierType ==
                EnumSymbolType.VARIABLE)
            {
                VariableSymbol<Information> sym =
                    (VariableSymbol<Information>) parser.SymbolTable.Symbols.First(x => x.IdentifierName.Equals(ident));
                return sym.TypeInfo.Name;
            }
            return "UNKNOWN"; // TODO FIX ME
        }

        private string GetIdentiferType(string ident)
        {
            if (parser.SymbolTable.Symbols.First(x => x.IdentifierName.Equals(ident)).IdentifierType ==
                EnumSymbolType.VARIABLE)
            {
                VariableSymbol<Information> sym =
                    (VariableSymbol<Information>)parser.SymbolTable.Symbols.First(x => x.IdentifierName.Equals(ident));
                return sym.TypeInfo.IdentifierType.Name.ToUpper();
            }
            return "OBJECT"; // TODO FIX ME
        }
    }
}
