using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Bitspco.Framework.Filters.Security.Models
{
    public class ClientInfo
    {
        public ClientAgentInfo AgentInfo { get; set; }
        public ClientNetInfo NetInfo { get; set; }
        public ClientGeoInfo GeoInfo { get; set; }

        public ClientInfo() {}
        public ClientInfo(HttpRequestBase request)
        {
            AgentInfo = new ClientAgentInfo(request);
            NetInfo = new ClientNetInfo(request);
            GeoInfo = new ClientGeoInfo(NetInfo.Ip);
        }
        public string GetKey(ClientInfo clientInfo)
        {
            return string.Format("{0}-{1}-{2}", NetInfo.Ip, GeoInfo.CountryCode, AgentInfo.DeviceFamily);
        }
    }
}
