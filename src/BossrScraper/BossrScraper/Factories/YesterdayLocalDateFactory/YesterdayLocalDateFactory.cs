using System;
using System.Collections.Generic;
using System.Text;
using NodaTime;

namespace BossrScraper.Factories
{
    public class YesterdayLocalDateFactory : IYesterdayLocalDateFactory
    {
        public LocalDate GetYesterdaysDate()
        {
            return LocalDate.FromDateTime(DateTime.Today).Minus(Period.FromDays(1));
        }
    }
}
