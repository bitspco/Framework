using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters
{
    public class FilterContext
    {
        public MethodBase Method { get; set; }
        public Dictionary<string, object> Parameters { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> Items { get; } = new Dictionary<string, object>();
    }
}
