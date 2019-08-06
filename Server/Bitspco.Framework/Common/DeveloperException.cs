using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Common
{
    public class DeveloperException : OperationException
    {
        public DeveloperException(string message) : base(message) {}
    }
}
