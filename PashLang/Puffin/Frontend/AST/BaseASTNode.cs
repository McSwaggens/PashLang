using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;

namespace Puffin.Frontend.AST
{
    public class BaseASTNode
    {
        private BaseASTNode left;
        private BaseASTNode right;
        private BaseASTNode parent;
    
        private Information data;
        private Symbol<Information> symbol;

        public virtual BaseASTNode Left
        {
            get { return left; }
            set { left = value; }
        }

        public virtual BaseASTNode Right
        {
            get { return right; }
            set { right = value; }
        }

        public virtual Information Data
        {
            get { return data; }
            set { data = value; }
        }

        public virtual Symbol<Information> Symbol
        {
            get { return symbol; }
            set { symbol = value; }
        }

        public BaseASTNode Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public virtual Object Evaluate()
        {
            Logger.WriteError("This method must be overridden");
            return null;
        }
        public void Visit(BaseASTNode node)
        {
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
