using System;

namespace BossrApi.Models.Interfaces
{
    public interface IScrape
    {
        int Id { get; set; }
        DateTime TimeMinUtc { get; set; }
    }
}
