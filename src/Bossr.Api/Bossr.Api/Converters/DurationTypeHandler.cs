using Dapper;
using NodaTime;
using System.Data;

namespace Bossr.Api.Converters
{
    public class DurationTypeHandler : SqlMapper.TypeHandler<Duration>
    {
        public override Duration Parse(object value)
        {
            return Duration.FromHours((int)value);
        }

        public override void SetValue(IDbDataParameter parameter, Duration value)
        {
            parameter.Value = value.TotalHours;
        }
    }
}
