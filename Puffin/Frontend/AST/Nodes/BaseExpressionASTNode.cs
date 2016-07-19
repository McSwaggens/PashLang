using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;

namespace Puffin.Frontend.AST.Nodes
{
    public class BaseExpressionASTNode : BaseASTNode
    {
        protected List<Statement> expressionStatements;
        protected Information returnValue;

        public List<Statement> ExpressionStatements
        {
            get { return expressionStatements; }
            set { expressionStatements = value; }
        }

        public Information ReturnValue
        {
            get { return returnValue; }
            set { returnValue = value; }
        }

        public override object Evaluate(BaseASTNode node)
        {
            Logger.WriteCritical("BaseExpressionASTNode.Evaluate Must be overridden");
            return null;
        }

        public override void Visit(BaseASTNode node)
        {
            if(!(node is BaseExpressionASTNode))
                base.Visit(node);
            else
                Logger.WriteCritical("BaseExpressionASTNode.Visit Must be overridden");
        }
    }
}
