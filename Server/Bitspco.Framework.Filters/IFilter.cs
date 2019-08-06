using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters
{
    public interface IFilter
    {
        void BeginExecute(FilterContext context);
        void EndExecute(FilterContext context);
    }
}
