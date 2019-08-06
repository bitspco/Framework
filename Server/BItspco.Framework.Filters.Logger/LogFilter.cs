using System.Linq;
using System.Reflection;
using System.Text;

namespace Bitspco.Framework.Filters.Log
{
    public class LogFilter : IFilter
    {
        public LoggerPool Logger { get; set; }

        public LogFilter(ILogger logger)
        {
            Logger = new LoggerPool(logger);
        }
        public void BeginExecute(FilterContext context)
        {
            var logAttr = context.Method.GetCustomAttribute<LogAttribute>();
            if (logAttr != null)
            {
                var sb = new StringBuilder();
                sb.Append("Method : {MethodName}; ");
                sb.Append("Parameters : ");
                foreach (var item in context.Parameters) sb.Append("{" + item.Key + "}");
                var parameters = context.Parameters.Values.ToList();
                parameters.Insert(0, context.Method.Name);
                Logger.Enqueue(new LogPackage(sb.ToString(), parameters.ToArray()));
            }
        }

        public void EndExecute(FilterContext context)
        {

        }
    }
}
