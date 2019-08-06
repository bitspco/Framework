using Bitspco.Framework.Filters.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.IP
{
    public class IPFilter : ISecurityFilter
    {
        public void BeginExecute(FilterContext context)
        {
            var attr = context.Method.GetCustomAttribute<IPAttribute>();
            if (attr == null) return;
            var clientInfo = context.Items["ClientInfo"] as ClientInfo;
            if (clientInfo == null) throw new SecurityException("ClientInfo most be set");

            var flag = false;
            foreach (var item in attr.Expressions)
            {
                if (new Regex(item).IsMatch(clientInfo.NetInfo.Ip)) { flag = true; break; }
            }
            if(!flag) throw new InValidIPException();
        }

        public void EndExecute(FilterContext context)
        {
        }
    }
}
