using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public interface IDataFetcher
    {
        Task<HttpResponseMessage> FetchHttpResponse(string url);
    }
}