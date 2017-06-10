using AutoMapper;
using Bossr.Api.Repositories;
using Bossr.Lib.Models.Entities;
using NodaTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bossr.Api.Services
{
    public interface IStateCalculator
    {
        Task<IEnumerable<StateDto>> GetStatesByWorldId(int worldId);
    }

    public class StateCalculator : IStateCalculator
    {
        private readonly IStatisticRepository statisticRepository;
        private readonly IScrapeRepository scrapeRepository;
        private readonly IRaidRepository raidRepository;

        public StateCalculator(
            IStatisticRepository statisticRepository,
            IScrapeRepository scrapeRepository,
            IRaidRepository raidRepository)
        {
            this.statisticRepository = statisticRepository;
            this.scrapeRepository = scrapeRepository;
            this.raidRepository = raidRepository;
        }

        public async Task<IEnumerable<StateDto>> GetStatesByWorldId(int worldId)
        {
            var raids = await raidRepository.ReadAllAsync();

            var statistics = await statisticRepository.ReadAllByWorldIdAsync(worldId); // Todo: Only get latest X stats (X = amount of spawnpoints)
            var scrapes = await scrapeRepository.ReadAllAsync();
            scrapes = scrapes.OrderByDescending(x => x.Date);

            foreach (var scrape in scrapes) // Todo: Get combined from repo
                scrape.Statistics = statistics.Where(x => x.ScrapeId == scrape.Id);

            var states = new List<StateDto>();
            foreach (var raid in raids)
            {
                var spawn = raid.Spawns.First();
                var totalAmount = GetAmountOfSpawnsByCreature(raids, spawn.CreatureId);

                var allOccurances = GetAllOccurances(scrapes, spawn);
                if (!allOccurances.Any())
                    continue;

                var latestOccurances = allOccurances
                    .Take(totalAmount)
                    .OrderByDescending(x => x.Date);
                
                var expectedMin = GetMin(latestOccurances.First().Date).Plus(raid.FrequencyMin);
                var expectedMax = GetMax(latestOccurances.Last().Date).Plus(raid.FrequencyMax);

                var missedRaids = 0;
                while (Instant.FromDateTimeUtc(DateTime.UtcNow) > expectedMax)
                {
                    expectedMin = expectedMin.Plus(raid.FrequencyMin);
                    expectedMax = expectedMax.Plus(raid.FrequencyMax);
                    missedRaids++;
                }

                var state = new StateDto
                {
                    Raid = Mapper.Map<RaidDto>(raid),
                    ExpectedMin = expectedMin.ToDateTimeUtc(),
                    ExpectedMax = expectedMax.ToDateTimeUtc(),
                    MissedRaids = missedRaids
                };

                states.Add(state);
            }

            return states;
        }

        private int GetAmountOfSpawnsByCreature(IEnumerable<IRaid> raids, int creatureId)
        {
            return raids
                .Where(x => x.Spawns.Any(y => y.CreatureId == creatureId))
                .Sum(x => x.Spawns.Sum(y => y.Amount));
        }

        private IEnumerable<IScrape> GetAllOccurances(IEnumerable<IScrape> scrapes, ISpawn spawn)
        {
            return scrapes.Where(x => x.Statistics.Any(y => y.CreatureId == spawn.CreatureId));
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
    }
}
