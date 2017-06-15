using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace Bossr.Scraper.Factories
{
    public class YesterdayLocalDateFactory : IYesterdayLocalDateFactory
    {
        public LocalDate GetYesterdaysDate()
        {
            return LocalDate.FromDateTime(DateTime.UtcNow.Date).Minus(Period.FromDays(1));
        }
    }
}
