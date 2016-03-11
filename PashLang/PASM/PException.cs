using System;
using System.Linq;
using System.Collections.Generic;
namespace PASM
{
    public class PException : Exception
    {
        public PException(string exception) : base(exception)
        {

        }
    }
}