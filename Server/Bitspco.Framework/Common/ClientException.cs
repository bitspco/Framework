using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Common
{
    public class ClientException : OperationException
    {
        public ClientException(string message) : base(message) {}
    }
}
