using NodaTime;

namespace Bossr.Scraper.Factories
{
    public interface IYesterdayLocalDateFactory
    {
        LocalDate GetYesterdaysDate();
    }
}
