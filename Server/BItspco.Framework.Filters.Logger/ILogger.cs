using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Log
{
    public interface ILogger
    {
        void Log(LogPackage obj);
    }
}
