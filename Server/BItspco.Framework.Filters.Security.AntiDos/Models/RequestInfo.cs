using Bitspco.Framework.Filters.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.AntiDos.Models
{
    public class RequestInfo
    {
        public ClientInfo ClientInfo { get; private set; }
        public List<DateTime> RequestTimes { get; private set; } = new List<DateTime>();

        public RequestInfo(ClientInfo clientInfo)
        {
            ClientInfo = clientInfo;
        }
    }
}
