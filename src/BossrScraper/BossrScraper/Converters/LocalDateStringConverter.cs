using NodaTime;
using NodaTime.Text;
using System.Globalization;

namespace BossrScraper.Converters
{
    public static class LocalDateStringConverter
    {
        public static LocalDate ToLocalDate(this string date)
        {
            var pattern = LocalDatePattern.CreateWithInvariantCulture("yyyy-MM-dd");
            return pattern.Parse(date).Value;
        }

        public static string ToIsoString(this LocalDate date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
