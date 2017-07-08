using System;

namespace Bossr.Scraper.Factories
{
    public interface IDateTimeFactory
    {
        DateTime GetYesterdaysDate();
    }

    public class DateTimeFactory : IDateTimeFactory
    {
        public DateTime GetYesterdaysDate()
        {
            return DateTime.UtcNow.Date.AddDays(-1);
        }
    }
}
