using AutoMapper;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bossr.Api.Controllers
{
    [Route("api/states")]
    [Authorize(Roles = "admin")]
    public class StatesController : Controller
    {
        private readonly IStatisticRepository statisticRepository;
        private readonly IScrapeRepository scrapeRepository;
        private readonly IRaidRepository raidRepository;

        public StatesController(
            IStatisticRepository statisticRepository,
            IScrapeRepository scrapeRepository,
            IRaidRepository raidRepository)
        {
            this.statisticRepository = statisticRepository;
            this.scrapeRepository = scrapeRepository;
            this.raidRepository = raidRepository;
        }

        [HttpGet("{worldId}")]
        public async Task<IActionResult> Get(int worldId)
        {
            var raids = await raidRepository.ReadAllAsync();

            var statistics = await statisticRepository.ReadAllByWorldIdAsync(worldId); // Todo: Only get latest X stats (X = amount of spawnpoints)
            var scrapes = await scrapeRepository.ReadAllAsync();
            scrapes = scrapes.OrderByDescending(x => x.Date);

            foreach (var scrape in scrapes) // Todo: Get combined from repo
                scrape.Statistics = statistics.Where(x => x.ScrapeId == scrape.Id);

            var states = new List<State>();
            foreach (var raid in raids)
            {
                var spawn = raid.Spawns.First();

                var latestOccurance = GetLatestOccurance(scrapes, spawn);
                if (latestOccurance == null)
                    continue;

                var expectedMin = GetMin(latestOccurance.Value).Plus(raid.FrequencyMin);
                var expectedMax = GetMax(latestOccurance.Value).Plus(raid.FrequencyMax);

                var missedRaids = 0;
                while (Instant.FromDateTimeUtc(DateTime.UtcNow) > expectedMax)
                {
                    expectedMin = expectedMin.Plus(raid.FrequencyMin);
                    expectedMax = expectedMax.Plus(raid.FrequencyMax);
                    missedRaids++;
                }

                var state = new State
                {
                    Raid = Mapper.Map<RaidDto>(raid),
                    ExpectedMin = expectedMin.ToDateTimeUtc(),
                    ExpectedMax = expectedMax.ToDateTimeUtc(),
                    MissedRaids = missedRaids
                };

                states.Add(state);
            }

            return Ok(states);
        }

        private LocalDate? GetLatestOccurance(IEnumerable<IScrape> scrapes, ISpawn spawn)
        {
            return scrapes.FirstOrDefault(x => x.Statistics.Any(y => y.CreatureId == spawn.CreatureId))?.Date;
        }

        private Instant GetMin(LocalDate date)
        {
            return Instant
                .FromDateTimeUtc(DateTime.SpecifyKind(date.ToDateTimeUnspecified(), DateTimeKind.Utc))
                .Plus(Duration.FromHours(2));
        }

        private Instant GetMax(LocalDate date)
        {
            return Instant
                .FromDateTimeUtc(DateTime.SpecifyKind(date.ToDateTimeUnspecified(), DateTimeKind.Utc))
                .Plus(Duration.FromDays(1))
                .Plus(Duration.FromHours(2));
        }

        private class State
        {
            public RaidDto Raid { get; set; }
            public DateTime ExpectedMin { get; set; }
            public DateTime ExpectedMax { get; set; }
            public int MissedRaids { get; set; }
        }
    }
}
