using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Common
{
    public class ServiceCallException : OperationException
    {
        public ServiceCallException(string message) : base(message) {}
    }
}
