using Dapper;
using NodaTime;
using System;
using System.Data;

namespace BossrApi.Converters
{
    public class LocalDateTypeHandler : SqlMapper.TypeHandler<LocalDate>
    {
        public override LocalDate Parse(object value)
        {
            return LocalDate.FromDateTime((DateTime)value);
        }

        public override void SetValue(IDbDataParameter parameter, LocalDate value)
        {
            parameter.Value = value.ToDateTimeUnspecified();
        }
    }
}