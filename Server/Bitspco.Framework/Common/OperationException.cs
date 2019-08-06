using Bitspco.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Common
{
    public abstract class OperationException : System.Exception
    {
        public OperationResult OperationResult { get; set; }
        public OperationException(string message) : base(message)
        {
            OperationResult = new OperationResult()
            {
                Success = false,
                Message = InnerException?.Message ?? Message
            };
        }
    }
}
