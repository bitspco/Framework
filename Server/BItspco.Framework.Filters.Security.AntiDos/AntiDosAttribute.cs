using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitspco.Framework.Filters.Security.AntiDos
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AntiDosAttribute : Attribute
    {
        public string Name { get; set; }
        public int MaxRequestPerMinutes { get; set; } = 50;

        public AntiDosAttribute()
        {

        }
        public AntiDosAttribute(int maxRequestPerMinutes)
        {
            MaxRequestPerMinutes = maxRequestPerMinutes;
        }
    }
}
