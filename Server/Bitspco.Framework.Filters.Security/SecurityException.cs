using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security
{
    public class SecurityException : FilterException
    {

        public SecurityException()
        {

        }
        public SecurityException(string message) : base(message)
        {

        }
    }
}
