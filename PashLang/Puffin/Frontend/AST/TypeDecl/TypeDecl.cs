using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols;

namespace Puffin.Frontend.AST.TypeDecl
{
    public class TypeDecl
    {
        Information info;
        EnumDeclType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        public TypeDecl(Information info, EnumDeclType type)
        {
            this.info = info;
            this.type = type;
        }

        public Information Info
        {
            get { return info; }
            set { info = value; }
        }

        public EnumDeclType Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
