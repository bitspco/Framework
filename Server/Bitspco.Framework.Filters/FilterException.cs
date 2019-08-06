using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters
{
    public class FilterException : Exception
    {
        public FilterException()
        {

        }
        public FilterException(string message) : base(message)
        {

        }
    }
}
