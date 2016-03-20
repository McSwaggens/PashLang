using System;
using System.Linq;
using System.Collections.Generic;
namespace PASM
{
    /// <summary>
    /// An instance of a function
    /// includes a new set of registers
    /// </summary>
    public class FunctionInstance
    {
        public bool doesReturnValue = true;
        public int ReturnLine;
        public bool MethodVariable = false; // Does the pointer have a : ?
        public int ReturnVariablePos; // Variable to set Location
		public Raster register;
        public FunctionInstance(int size)
        {
			register = new Raster(size);
        }
    }
}