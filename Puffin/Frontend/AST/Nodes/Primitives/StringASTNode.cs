﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.AST.Nodes.Primitives
{
    public class StringASTNode : BaseASTNode
    {
        public StringASTNode(VariableSymbol<Information> owner, BaseASTNode parent)
        {
            if (owner == null || !owner.TypeInfo.IdentifierType.Name.Equals(nameof(String)))
            {
                Logger.WriteError("Cannot construct an String node without a valid symbol");
                return;
            }
            this.Symbol = owner;
            this.Data = owner.TypeInfo;
            this.Left = null;
            this.Right = null;
            this.Parent = parent;

        }

        public StringASTNode(VariableSymbol<Information> owner, BaseASTNode left, BaseASTNode right, BaseASTNode parent)
        {
            if (owner == null || !owner.TypeInfo.IdentifierType.Name.Equals(nameof(String)))
            {
                Logger.WriteError("Cannot construct an String node without a valid symbol");
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