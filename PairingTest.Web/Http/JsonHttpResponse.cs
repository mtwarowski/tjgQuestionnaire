using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace PairingTest.Web.Http
{
    public class JsonHttpResponse<T> : IApiResponse<T> where T : class
    {
        private readonly HttpResponseMessage response;

        public HttpStatusCode Status => response.StatusCode;
        public bool IsSuccessful => response.StatusCode == HttpStatusCode.OK;
        
        public JsonHttpResponse(HttpResponseMessage response)
        {
            this.response = response;
        }

        public async Task<T> GetPayloadAsync()
        {
            var body = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(body);
        }
    }
}