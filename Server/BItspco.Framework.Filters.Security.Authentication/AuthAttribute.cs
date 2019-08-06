using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.Authenticate
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AuthAttribute : Attribute
    {
        public string Policy { get; set; }
        public bool MethodNamePolicyEnable { get; set; } = false;
        public short Priority { get; set; }
        public Exception Exception { get; set; }
        public OperationType Operation { get; set; }
        public AuthAttribute()
        {

        }
        public AuthAttribute(string policy)
        {
            Policy = policy;
        }
    }
}
