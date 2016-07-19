using System;
namespace PASM
{
    public class PException : Exception
    {
        public PException(string exception) : base(exception)
        {

        }
    }
}