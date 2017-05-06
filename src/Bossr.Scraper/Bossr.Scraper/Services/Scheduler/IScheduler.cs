using System.Threading.Tasks;

namespace Bossr.Scraper.Services
{
    public interface IScheduler
    {
        Task Run();
    }
}