using System;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace Bitspco.Framework.Filters.Security.Models
{
    public class ClientNetInfo
    {
        public string Ip { get; private set; }
        public string Url { get; private set; }
        public string Method { get; private set; }
        public string Host { get; private set; }
        public string Origin { get; private set; }
        public string Referer { get; private set; }
        public string CacheControl { get; private set; }
        public DateTime RequestTime { get; private set; } = DateTime.Now;


        public ClientNetInfo()
        {

        }
        public ClientNetInfo(HttpRequestBase request)
        {
            try { Ip = GetClientIp(request); } catch (Exception) { }
            try { Method = request.HttpMethod; } catch (Exception) { }
            try { Url = request.Url.ToString(); } catch (Exception) { }
            try { Host = request.Headers.GetValues("Host").FirstOrDefault(); } catch (Exception) { }
            try { Origin = request.Headers.GetValues("Origin").FirstOrDefault(); } catch (Exception) { }
            try { Referer = request.Headers.GetValues("Referer").FirstOrDefault(); } catch (Exception) { }
            try { CacheControl = request.Headers.GetValues("Cache-Control").FirstOrDefault(); } catch (Exception) { }
        }
        public string GetClientIp(HttpRequestBase request)
        {
            return request.ServerVariables.Get("REMOTE_ADDR");
        }
        public string GetClientIp(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return null;
            }
        }
    }
}
