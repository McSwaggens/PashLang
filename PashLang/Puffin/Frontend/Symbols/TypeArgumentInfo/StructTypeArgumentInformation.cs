using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Puffin.Frontend.Symbols.TypeInfo;

namespace Puffin.Frontend.Symbols.TypeArgumentInfo
{
    public class StructTypeArgumentInformation<T> : TypeArgumentInformation<T> where T : StructInformation
    {
    }
}
