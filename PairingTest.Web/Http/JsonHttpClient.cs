using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PairingTest.Web.Http
{
    public class JsonHttpClient  :IHttpClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<IApiResponse<TResponse>> GetAsync<TResponse>(string url) where TResponse : class
        {
            var message = BuildRequestMessageWithHeaders(HttpMethod.Get, url);

            var response = await HttpClient.SendAsync(message);

            return new JsonHttpResponse<TResponse>(response);
        }

        private HttpRequestMessage BuildRequestMessageWithHeaders(HttpMethod get, string url)
        {
            var message = new HttpRequestMessage(get, url);
            message.Headers.Accept.Clear();
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return message;
        }
    }
}