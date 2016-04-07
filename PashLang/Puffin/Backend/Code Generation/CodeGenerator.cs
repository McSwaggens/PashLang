using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend;

namespace Puffin.Backend.Code_Generation
{
    public class CodeGenerator
    {
        private Parser parser;
        public CodeGenerator(Parser parser)
        {
            this.parser = parser;
        }
    }
}
