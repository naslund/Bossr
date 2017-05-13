using NodaTime;

namespace Bossr.Api.Converters
{
    public static class DurationHoursConverter
    {
        public static Duration ToDuration(int hours)
        {
            return Duration.FromHours(hours);
        }

        public static int ToHours(Duration duration)
        {
            return (int)duration.TotalHours;
        }
    }
}
