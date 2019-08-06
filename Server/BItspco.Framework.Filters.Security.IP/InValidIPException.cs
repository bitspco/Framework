using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.IP
{
    public class InValidIPException : SecurityException
    {
        public InValidIPException()
        {

        }
        public InValidIPException(string message) : base(message)
        {

        }
    }
}
