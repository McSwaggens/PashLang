using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.AST
{
    public class ByteASTNode : BaseASTNode
    {
        public ByteASTNode(VariableSymbol<Information> owner, BaseASTNode parent)
        {
            if (owner == null || !owner.TypeInfo.IdentifierType.Name.Equals(nameof(Byte)))
            {
                Logger.WriteError("Cannot construct an Byte node without a valid symbol");
                return;
            }
            this.Symbol = owner;
            this.Data = owner.TypeInfo;
            this.Left = null;
            this.Right = null;
            this.Parent = parent;

        }

        public ByteASTNode(VariableSymbol<Information> owner, BaseASTNode left, BaseASTNode right, BaseASTNode parent)
        {
            if (owner == null || !owner.TypeInfo.IdentifierType.Name.Equals(nameof(Byte)))
            {
                Logger.WriteError("Cannot construct an Byte node without a valid symbol");
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

        public override object Evaluate()
        {
            VariableSymbol<Information> info = (VariableSymbol<Information>)Symbol;
            return ((StructInformation)info.ValueType).DefaultValue;
        }
    }
}
