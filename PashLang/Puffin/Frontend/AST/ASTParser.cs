using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.AST.Nodes;
using Puffin.Frontend.Symbols;

namespace Puffin.Frontend.AST
{
    public class ASTParser
    {
        private Parser parse;
        public ASTParser(Parser parse)
        {
            if (parse == null)
            {
                Logger.WriteError("Can not parse AST with no parsed statements");
                return;
            }
            this.parse = parse;
        }

        public BaseASTNode ParseAST()
        {
            BaseASTNode node = new BaseASTNode();
            foreach (Statement smt in parse.Statements)
            {
                Symbol<Information> sym =
                    parse.SymbolTable.Symbols.FirstOrDefault(x => x.IdentifierName.Equals(smt.TypeInformation.Name));
                switch (smt.TypeInformation.Name)
                {
                    case nameof(Int16):

                    case nameof(Int32):
                        return new IntegerASTNode((VariableSymbol<Information>) sym, null);
                    case nameof(Int64):
                        return new IntegerASTNode((VariableSymbol<Information>)sym, null);
                    default:
                        Logger.WriteWarning("Type " + smt.TypeInformation.Name + " Does not have an AST node associated with it yet");
                        return new BaseASTNode();
                }
            }
            return null;
        }
    }
}
