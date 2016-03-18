using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.AST;
using Puffin.Frontend.Tokens;
using static Puffin.Logger;

namespace Puffin.Frontend
{
    public class Parser
    {
        private Lexer lexical;
        private LinkedList<Statement> statements; 

        public Parser(Lexer lexical)
        {
            this.lexical = lexical;
            this.statements = new LinkedList<Statement>();
        }

        public bool Start()
        {
            if (!ParseFirstPass())
                return false;
            return true;
        }

        private bool ParseFirstPass()
        {
            LinkedListNode<Token> node = lexical.Tokens.First;
            while (node != null)
            {
                LinkedList<Token> temp = new LinkedList<Token>();
                while (node != null && !(node.Value.Value.Equals(";") || node.Value.Value.Equals("}")))
                {
                    temp.AddLast(node.Value);
                    node = node.Next;
                    
                }
                Statement stm = new Statement(new List<Token>(temp.ToList()), true, true);
                statements.AddLast(stm);
                node = node?.Next;
                temp.Clear();
            }
            Write("Parsed " + statements.Count + " statements");
            return true;
        }
    }
}
