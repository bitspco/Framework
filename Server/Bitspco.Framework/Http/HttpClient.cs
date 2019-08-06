using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Bitspco.Framework.Extension;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Collections.Generic;

namespace Bitspco.Framework.Http
{
    public class HttpClient
    {
        public string Url { get; set; }
        private System.Net.Http.HttpClient _client { get; } = new System.Net.Http.HttpClient();
        private Dictionary<string, string> Headers { get; } = new Dictionary<string, string>();
        public HttpClient(string url)
        {
            Url = url;
        }
        public HttpClient AddHeader(string name, string value)
        {
            Headers.Add(name, value);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
            return this;
        }
        public HttpClient Clear()
        {
            Headers.Clear();
            return this;
        }
        public string GetQueryString(object obj)
        {
            if (obj == null) return null;
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }
        public T Send<T>(HttpMethod method, object parameter = null)
        {
            var url = Url;
            StringContent content = null;
            switch (method)
            {
                case HttpMethod.Get:
                case HttpMethod.Delete:
                case HttpMethod.Options:
                    url += (Url.Contains("?") ? "&" : "?") + GetQueryString(parameter);
                    break;
                case HttpMethod.Post:
                case HttpMethod.Put:
                case HttpMethod.Patch:
                    content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(parameter), Encoding.UTF8, "application/json");
                    break;
            }
            var request = new HttpRequestMessage(new System.Net.Http.HttpMethod(method.GetDescription()), url) { Content = content };
            foreach (var item in Headers) request.Headers.Add(item.Key, item.Value);
            var response = _client.SendAsync(request).Result;
            var responseString = response.Content.ReadAsStringAsync().Result;
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
        }
        public T Get<T>(object parameter = null) => Send<T>(HttpMethod.Get, parameter);
        public T Post<T>(object parameter = null) => Send<T>(HttpMethod.Post, parameter);
        public T Put<T>(object parameter = null) => Send<T>(HttpMethod.Put, parameter);
        public T Patch<T>(object parameter = null) => Send<T>(HttpMethod.Patch, parameter);
        public T Delete<T>(object parameter = null) => Send<T>(HttpMethod.Delete, parameter);
        public T Options<T>(object parameter = null) => Send<T>(HttpMethod.Options, parameter);
    }
}
