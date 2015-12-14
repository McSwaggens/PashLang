using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrocodileScript
{
    public class SyntaxException : Exception
    {
        public SyntaxError Error;
        public SyntaxException(string mes, SyntaxError error) : base(mes)
        {
            Error = error;
        }
        //TODO: Format message

    }

    public enum SyntaxError
    {
        SyntaxError, BlockMissing
    }
}
