using System.Threading.Tasks;
using System.Net.Http;

namespace DotNet.Ranking.Bacen
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }
        
        public virtual async Task<HttpResponseMessage> GetAsync()
        {            
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new System.Uri(Constants.URL_BACEN)
            };
            
            var result = await _client.SendAsync(req).ConfigureAwait(false);

            return result;

        }
    }
}