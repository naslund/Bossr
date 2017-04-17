using NodaTime;
using NodaTime.Text;
using System.Globalization;

namespace BossrApi.Converters
{
    public static class LocalDateStringConverter
    {
        public static LocalDate ToLocalDate(string date)
        {
            var pattern = LocalDatePattern.CreateWithInvariantCulture("yyyy-MM-dd");
            return pattern.Parse(date).Value;
        }

        public static string ToString(LocalDate date)
        {
            return date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
