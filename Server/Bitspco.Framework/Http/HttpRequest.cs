using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Bitspco.Framework.Extension;
using System.Web;

namespace Bitspco.Framework.Http
{
    public class HttpRequest
    {
        public string Url { get; set; }
        public WebHeaderCollection Headers { get; set; } = new WebHeaderCollection();
        public HttpRequest(string url)
        {
            Url = url;
        }
        public HttpRequest AddHeader(string name, string value)
        {
            Headers.Add(name, value);
            return this;
        }
        public string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
        public T Send<T>(HttpMethod method, object parameter = null)
        {
            if (!(WebRequest.Create(Url) is HttpWebRequest req)) return default(T);
            req.Method = method.GetDescription();
            req.Headers = Headers;
            req.ContentType = "application/json";
            if (parameter != null)
            {
                switch (method)
                {
                    case HttpMethod.Get:
                    case HttpMethod.Delete:
                    case HttpMethod.Options:
                        Url += (Url.Contains("?") ? "&" : "?") + GetQueryString(parameter);
                        break;
                    case HttpMethod.Post:
                    case HttpMethod.Put:
                    case HttpMethod.Patch:
                        using (var writer = new StreamWriter(req.GetRequestStream(), Encoding.Unicode))
                        {
                            writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(parameter));
                        }
                        break;
                }
            }
            T obj;
            var response = req.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
            {
                var result = reader.ReadToEnd();
                obj = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
            }
            return obj;
        }
    }
}
