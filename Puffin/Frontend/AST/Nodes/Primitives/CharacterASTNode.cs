using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.AST.Nodes.Primitives
{
    public class CharacterASTNode : BaseASTNode
    {
        public CharacterASTNode(VariableSymbol<Information> owner, BaseASTNode parent)
        {
            if (owner == null || !owner.TypeInfo.IdentifierType.Name.Equals(nameof(Char)))
            {
                Logger.WriteError("Cannot construct an Char node without a valid symbol");
                return;
            }
            this.Symbol = owner;
            this.Data = owner.TypeInfo;
            this.Left = null;
            this.Right = null;
            this.Parent = parent;

        }

        public CharacterASTNode(VariableSymbol<Information> owner, BaseASTNode left, BaseASTNode right, BaseASTNode parent)
        {
            if (owner == null || !owner.TypeInfo.IdentifierType.Name.Equals(nameof(Char)))
            {
                Logger.WriteError("Cannot construct an Char node without a valid symbol");
                return;
            }
            this.Symbol = owner;
            this.Data = owner.TypeInfo;

            this.Left = left;
            Left.Parent = this;
            this.Right = right;
            Right.Parent = this;
            this.Parent = parent;
        }

        public override object Evaluate(BaseASTNode node)
        {
            VariableSymbol<Information> info = (VariableSymbol<Information>)Symbol;
            return ((StructInformation)info.ValueType).DefaultValue;
        }
    }
}
