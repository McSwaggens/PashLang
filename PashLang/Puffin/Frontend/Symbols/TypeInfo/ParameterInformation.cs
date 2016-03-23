﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public class ParameterInformation : Information
    {
        bool isOptional;
        bool isReference;
        bool isPointer;
        bool isOut;
        object defaultValue;

        public ParameterInformation(string name, Information type, bool isReference, bool isPointer, bool isOptional, bool isOut, object defaultValue = null)
        {
            this.name = name;
            this.type = type;
            this.isReference = isReference;
            this.isPointer = isPointer;
            this.isOptional = isOptional;
            this.isOut = isOut;
            this.defaultValue = defaultValue;
        }


    }
}
