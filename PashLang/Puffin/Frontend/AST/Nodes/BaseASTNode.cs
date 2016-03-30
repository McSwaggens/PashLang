using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;

namespace Puffin.Frontend.AST.Nodes
{
    public class BaseASTNode
    {
        private BaseASTNode left;
        private BaseASTNode right;
        private BaseASTNode parent;
    
        private Information data;
        private Symbol<Information> symbol;

        public BaseASTNode Left
        {
            get { return left; }
            set { left = value; }
        }

        public BaseASTNode Right
        {
            get { return right; }
            set { right = value; }
        }

        public Information Data
        {
            get { return data; }
            set { data = value; }
        }

        public Symbol<Information> Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public BaseASTNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public virtual Object Evaluate(BaseASTNode node)
        {
            Logger.WriteCritical("BaseASTNode.Evaluate must be overridden");
            return null;
        }
        public virtual void Visit(BaseASTNode node)
        {
            Evaluate(node);
            if (node.Left != null)
            {
                Visit(node.Left);
            }
            else if (node.Right != null)
            {
                Visit(node.Right);
            }
        }

    }
}
