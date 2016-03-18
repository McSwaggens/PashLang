using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.Modifiers;
using Puffin.Frontend.Tokens;

namespace Puffin.Frontend.AST
{
    public class Statement
    {
        protected List<Token> statementTokens;
        protected List<Modifier> modifiers; 
        protected bool valueStatement;
        protected bool isTerminated;

        public Statement(List<Token> statemenrTokens, bool valueStatement, bool isTerminated)
        {
            this.statementTokens = statemenrTokens;
            this.valueStatement = valueStatement;
            this.isTerminated = isTerminated;
        }

        public void DefineSymbols()
        {
            foreach (EnumKeywords ty in statementTokens.OfType<KeywordToken>().Select(tok => (EnumKeywords)tok.Type))
            {
                if ((int)ty <= 0x03 || (int)ty == 0x25 || (int)ty == 0x26)
                {
                    modifiers.Add(new Modifier((EnumModifiers) Enum.Parse(typeof(EnumModifiers),ty.ToString().ToUpper())));
                }
                else if (((int)ty >= 0x04 && (int)ty <= 0x0F) || ((int)ty >= 0x30 && (int)ty <= 0x33))
                {
                    //TODO deal with Types
                }
            }

            //LinkedListNode<Token> node = statementTokens.First;
            //while (node != null)
            //{

            //    node = node.Next;
            //}
        }
    }
}
