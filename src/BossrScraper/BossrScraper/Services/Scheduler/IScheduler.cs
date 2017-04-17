using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public interface IScheduler
    {
        Task Run();
    }
}