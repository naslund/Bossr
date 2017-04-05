using System.Threading.Tasks;

namespace BossrScraper.Services.Scheduler
{
    public interface IScheduler
    {
        Task Run();
    }
}
