using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.IP
{
    [AttributeUsage(AttributeTargets.Method)]
    public class IPAttribute : Attribute
    {
        public string[] Expressions { get; set; }
        
        public IPAttribute(params string[] expressions)
        {
            Expressions = expressions;
        }
    }
}
