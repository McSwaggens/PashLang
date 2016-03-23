using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Symbols
{
    public enum EnumSymbolType
    {
        NAMESPACE = 0x00,
        CLASS = 0x01,
        INTERFACE = 0x02,
        ENUM = 0x03,
        STRUCT = 0x04,
        FUNCTION = 0x05,
        VARIABLE = 0x06,
        ARRAY = 0x07,
        PARAMETER = 0x08,
    }
}
