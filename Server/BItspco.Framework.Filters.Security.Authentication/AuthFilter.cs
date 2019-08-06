using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Bitspco.Framework.Filters.Security.Authenticate
{
    public class AuthFilter : ISecurityFilter
    {
        private IAuthenticator authenticator;

        public AuthFilter()
        {

        }
        public AuthFilter(IAuthenticator authenticator)
        {
            SetAuthenticator(authenticator);
        }
        public void SetAuthenticator(IAuthenticator authenticator)
        {
            this.authenticator = authenticator;
        }
        private Exception Process(FilterContext context, AuthAttribute attr)
        {
            var policy = attr.Policy ?? "";
            if (!authenticator.HasPermission(policy)) return new UnauthorizedAccessException("Access denied!!!");
            return null;
        }
        public void BeginExecute(FilterContext context)
        {
            if (context.Method.GetCustomAttribute<AuthIgnoreAttribute>() != null) return;
            var list = new List<AuthAttribute>();
            var attrs = context.Method.ReflectedType.GetCustomAttributes<AuthAttribute>().ToList();
            foreach (var attr in attrs)
            {
                attr.Exception = Process(context, attr);
                list.Add(attr);
                if (attr.MethodNamePolicyEnable) {
                    var methodAttr = new AuthAttribute() { Policy = context.Method.Name };
                    methodAttr.Exception = Process(context, attr);
                    list.Add(methodAttr);
                }
            }
            attrs = context.Method.GetCustomAttributes<AuthAttribute>().ToList();
            foreach (var attr in attrs)
            {
                attr.Exception = Process(context, attr);
                list.Add(attr);
                if (attr.MethodNamePolicyEnable)
                {
                    var methodAttr = new AuthAttribute() { Policy = context.Method.Name };
                    methodAttr.Exception = Process(context, attr);
                    list.Add(methodAttr);
                }
            }
            list = list.OrderByDescending(x => x.Priority).ToList();
            if(list.Count > 0)
            {
                if (authenticator == null) throw new Exception("Authenticator has not been set");
                if (string.IsNullOrEmpty(authenticator.GetToken())) throw new UnauthorizedAccessException("Token doesn't set!!!");
                if (!authenticator.IsTokenValid()) throw new UnauthorizedAccessException("Token doesn't valid!!!");
            }
            for (int i = 1; i < list.Count; i++)
            {
                var first = list[i - 1];
                var second = list[i];
                switch (first.Operation)
                {
                    case OperationType.And:
                        if (first.Exception != null || second.Exception != null) throw second.Exception;
                        break;
                    case OperationType.Or:
                        if (first.Exception != null && second.Exception != null) throw second.Exception;
                        break;
                }
            }
        }

        public void EndExecute(FilterContext context)
        {

        }
    }
}
