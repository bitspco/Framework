using Bitspco.Framework.Filters.Security.AntiDos.Models;
using Bitspco.Framework.Filters.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bitspco.Framework.Filters.Security.AntiDos
{
    public class AntiDosFilter : ISecurityFilter
    {
        private int maxRequestPerMinutes = 50;
        private static Dictionary<string, RequestInfo> requests = new Dictionary<string, RequestInfo>();

        public bool AttributeEnable { get; set; } = true;
        public int MaxRequestPerMinutes { get { return maxRequestPerMinutes; } set { if (maxRequestPerMinutes < 10) throw new Exception("MaxRequestPerMinutes can't set less than 10"); maxRequestPerMinutes = value; } }

        public AntiDosFilter() {}
        public AntiDosFilter(int maxRequestPerMinutes)
        {
            MaxRequestPerMinutes = maxRequestPerMinutes;
        }
        public void BeginExecute(FilterContext context)
        {
            var attr = context.Method.GetCustomAttribute<AntiDosAttribute>();
            var name = "";
            var maxRequestPerMinutes = MaxRequestPerMinutes;
            if(attr != null)
            {
                name = attr.Name;
                maxRequestPerMinutes = attr.MaxRequestPerMinutes;
            }
            else if (AttributeEnable) return;
            var clientInfo = context.Items["ClientInfo"] as ClientInfo;
            if (clientInfo == null) throw new SecurityException("ClientInfo most be set");
            RequestInfo request;
            var key = name + clientInfo.GetKey(clientInfo);
            lock (requests) request = requests.ContainsKey(key) ? requests[key] : null;
            if (request == null)
            {
                lock (requests) if (requests.Count > 1000) requests.Remove(requests.First().Key);
                requests[key] = request = new RequestInfo(clientInfo);
            }
            var oneMinAgo = DateTime.Now.AddMinutes(-1);
            request.RequestTimes.Add(DateTime.Now);
            if (request.RequestTimes.Count > maxRequestPerMinutes + 100) request.RequestTimes.RemoveRange(0, 100);
            if (request.RequestTimes.Where(x => x > oneMinAgo).Count() > maxRequestPerMinutes) throw new DosAttackException("You request too much try again later");
        }

        public void EndExecute(FilterContext context)
        {
        }
    }
}
