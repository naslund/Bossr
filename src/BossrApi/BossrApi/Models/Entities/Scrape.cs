using BossrApi.Models.Interfaces;
using System;

namespace BossrApi.Models.Entities
{
    public class Scrape : IScrape
    {
        public int Id { get; set; }
        public DateTime TimeMinUtc { get; set; }
    }
}
