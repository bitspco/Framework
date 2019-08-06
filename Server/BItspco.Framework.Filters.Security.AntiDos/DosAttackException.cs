using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.AntiDos
{
    public class DosAttackException : SecurityException
    {
        public DosAttackException()
        {

        }
        public DosAttackException(string message) : base(message)
        {

        }
    }
}
