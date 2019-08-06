using Bitspco.Framework.Filters.Security.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security
{
    public class SecurityFilter : IFilter
    {
        public ClientInfo ClientInfo { get; set; }
        public List<ISecurityFilter> Filters { get; } = new List<ISecurityFilter>();

        public SecurityFilter() {}
        public SecurityFilter(ClientInfo clientInfo)
        {
            ClientInfo = clientInfo;
        }

        public void BeginExecute(FilterContext context)
        {
            context.Items["ClientInfo"] = ClientInfo;
            foreach (var item in Filters) item.BeginExecute(context);
        }

        public void EndExecute(FilterContext context)
        {
            foreach(var item in Filters) item.EndExecute(context);
        }
    }
}
