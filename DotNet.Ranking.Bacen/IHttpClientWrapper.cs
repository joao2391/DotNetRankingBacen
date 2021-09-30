using System.Net.Http;
using System.Threading.Tasks;

namespace DotNet.Ranking.Bacen
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> GetAsync();
    }
}
