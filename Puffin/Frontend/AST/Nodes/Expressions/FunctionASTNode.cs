using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.AST.Nodes.Expressions
{
    public class FunctionASTNode : BaseExpressionASTNode
    {
        private BaseASTNode scopeNode;
        private BaseASTNode returnNode;
        private List<BaseASTNode> parameterNodes;
         
        public FunctionASTNode(BaseASTNode scopeNode, BaseASTNode returnNode, List<BaseASTNode> parameterNodes)
        {
            this.scopeNode = scopeNode;
            this.returnNode = returnNode;
            if(parameterNodes != null)
                this.parameterNodes = parameterNodes;
            else
                parameterNodes = new List<BaseASTNode>();
        }

        public override object Evaluate(BaseASTNode node)
        {
            return base.Evaluate(node);
        }

        public override void Visit(BaseASTNode node)
        {
            base.Visit(node);
        }
    }
}
