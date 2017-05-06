using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services
{
    public interface IDataFetcher
    {
        Task<HttpResponseMessage> FetchHttpResponse(string url);
    }
}