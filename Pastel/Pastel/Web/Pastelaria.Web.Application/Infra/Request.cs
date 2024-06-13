using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Routing;

namespace Pastelaria.Web.Application.Infra
{
    public class Request
    {
        private readonly HttpClient _httpClient;

        public Request()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public Request(string uri) : this()
        {
            SetUri(uri);
        }

        public Request(string uri, int timeout) : this(uri)
        {
            _httpClient.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
        }

        public Request SetUri(string uri)
        {
            _httpClient.BaseAddress = new Uri(uri);
            return this;
        }

        /// <summary>
        /// Definara o timeout de todas requisicoes desta instancia do HttpCliente.
        /// Utilizar apenas uma vez por chamada da tela para a ActionResult em questao.
        /// </summary>
        /// <param name="timeout">(segundos multiplicado por 1000</param>
        public void SetTimeout(int timeout)
        {
            _httpClient.Timeout = new TimeSpan(0, 0, 0, 0, timeout);
        }

        public Request AddHeader(string name, object value)
        {
            _httpClient.DefaultRequestHeaders.Add(name, value.ToString());
            return this;
        }

        public HttpResponseMessage Get(string route = null) => Get(route, null);

        public HttpResponseMessage Get(int route) => Get(route.ToString(), null);

        public HttpResponseMessage Get(object parameters) => Get(string.Empty, parameters);

        public HttpResponseMessage Get(string route, object parameters)
        {
            try
            {
                return _httpClient.GetAsync(Route(route, parameters)).Result;
            }
            catch (AggregateException)
            {
                return Offline();
            }
        }

        public HttpResponseMessage Post<T>(T obj) where T : class => Post(string.Empty, obj);

        public HttpResponseMessage Post<T>(string route, T obj) where T : class
        {
            try
            {
                return _httpClient.PostAsync(Route(route, null), Content(obj)).Result;
            }
            catch (AggregateException)
            {
                return Offline();
            }
        }

        public HttpResponseMessage Put(string route) => Put(route, (object)null);

        public HttpResponseMessage Put<T>(T obj) where T : class => Put(string.Empty, obj);

        public HttpResponseMessage Put<T>(int route, T obj) where T : class => Put(route.ToString(), obj);

        public HttpResponseMessage Put<T>(string route, T obj) where T : class
        {
            try
            {
                return _httpClient.PutAsync(Route(route, null), Content(obj)).Result;
            }
            catch (AggregateException)
            {
                return Offline();
            }
        }

        public HttpResponseMessage Delete(string route) => Delete(route, null);

        public HttpResponseMessage Delete(int route) => Delete(route, null);

        public HttpResponseMessage Delete(object parameters) => Delete(null, parameters);

        public HttpResponseMessage Delete(object route, object parameters)
        {
            try
            {
                return _httpClient.DeleteAsync(Route(route, parameters)).Result;
            }
            catch (AggregateException)
            {
                return Offline();
            }
        }

        private static string Route(object route, object parameters)
        {
            var r = route?.ToString() ?? string.Empty;
            if (parameters == null)
                return r;

            var queryString = new List<string>();
            foreach (var prop in new RouteValueDictionary(parameters))
            {
                var qs = $"{prop.Key}=";
                var @decimal = prop.Value as decimal?;
                var dateTime = prop.Value as DateTime?;
                if (@decimal != null)
                    qs += Regex.Replace(@decimal.ToString(), @"(?<=\d)\,(?=\d)", ".", RegexOptions.Compiled);
                else if (dateTime != null)
                    qs += dateTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                else
                    qs += prop.Value;
                queryString.Add(qs);
            }

            return r + (queryString.Any() ? "?" + string.Join("&", queryString) : string.Empty);
        }

        private static HttpContent Content<T>(T obj)
        {
            if (obj == null)
                return new StringContent(string.Empty);

            return new ObjectContent<T>(obj, new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    ContractResolver = new DefaultContractResolver
                    {
                        IgnoreSerializableAttribute = true
                    }
                }
            });
        }

        private HttpResponseMessage Offline()
        {
            return new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"Uri {_httpClient.BaseAddress.AbsoluteUri} OFFLINE")
            };
        }
    }
}