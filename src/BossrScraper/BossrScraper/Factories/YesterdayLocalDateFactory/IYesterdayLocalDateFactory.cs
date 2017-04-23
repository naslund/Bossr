using NodaTime;

namespace BossrScraper.Factories
{
    public interface IYesterdayLocalDateFactory
    {
        LocalDate GetYesterdaysDate();
    }
}
