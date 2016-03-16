using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puffin.Frontend.Symbols.TypeInfo
{
    public abstract class TypeArgumentInformation<T> where T : Information
    {
        protected T argumentInformation;
        protected bool isAllowed;
        protected string typeName;

        public virtual T ArgumentInformation
        {
            get { return argumentInformation; }
        }

        public virtual bool IsAllowed
        {
            get { return isAllowed; }
        }

        public virtual string TypeName
        {
            get { return typeName; }
        }
    }
}
