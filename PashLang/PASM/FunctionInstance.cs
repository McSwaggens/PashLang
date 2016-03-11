using System;
using System.Linq;
using System.Collections.Generic;
namespace PASM
{
    public class FunctionInstance
    {
        public bool doesReturnValue = true;
        public int ReturnLine;
        public bool MethodVariable = false; // Does the pointer have a : ?
        public int ReturnVariablePos; // Variable to set Location
        public Register register = new Register(10);
    }
}