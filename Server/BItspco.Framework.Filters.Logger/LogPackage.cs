using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Log
{
    public class LogPackage
    {
        public string Template { get; set; }
        public object[] Parameters { get; set; }

        public LogPackage()
        {

        }
        public LogPackage(string template, params object[] parameters)
        {
            Template = template;
            Parameters = parameters;
        }
    }
}
