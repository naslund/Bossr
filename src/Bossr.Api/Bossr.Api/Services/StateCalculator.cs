using Bossr.Api.Mappers;
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
        private readonly ISpawnRepository spawnRepository;
        private readonly ICreatureRepository creatureRepository;
        private readonly IPositionRepository positionRepository;
        private readonly IRaidMapper raidMapper;
        private readonly IScrapeMapper scrapeMapper;

        public StateCalculator(
            IStatisticRepository statisticRepository,
            IScrapeRepository scrapeRepository,
            IRaidRepository raidRepository,
            ISpawnRepository spawnRepository,
            ICreatureRepository creatureRepository,
            IPositionRepository positionRepository,
            IRaidMapper raidMapper,
            IScrapeMapper scrapeMapper)
        {
            this.statisticRepository = statisticRepository;
            this.scrapeRepository = scrapeRepository;
            this.raidRepository = raidRepository;
            this.spawnRepository = spawnRepository;
            this.creatureRepository = creatureRepository;
            this.positionRepository = positionRepository;
            this.raidMapper = raidMapper;
            this.scrapeMapper = scrapeMapper;
        }

        public async Task<IEnumerable<StateDto>> GetStatesByWorldId(int worldId)
        {
            var raids = await raidRepository.ReadAllAsync();
            var spawns = await spawnRepository.ReadAllAsync();
            var creatures = await creatureRepository.ReadAllAsync();
            var positions = await positionRepository.ReadAllAsync();

            raidMapper.MapRelations(raids, spawns, creatures, positions);

            var statistics = (await statisticRepository.ReadAllByWorldIdAsync(worldId)) // Todo: Only get latest X stats (X = amount of spawnpoints)
                .ToList();

            var scrapes = (await scrapeRepository.ReadAllAsync())
                .OrderByDescending(x => x.Date)
                .ToList();

            scrapeMapper.MapRelations(scrapes, statistics);

            var currentTimeUtc = DateTime.UtcNow;
            var states = new List<StateDto>();
            foreach (var raid in raids)
            {
                var spawn = raid.Spawns.First();
                var totalAmount = GetTotalAmountOfRaidsByCreature(raids, spawn);

                var latestOccurances = GetAllOccurances(scrapes, spawn)
                    .Take(totalAmount)
                    .ToList();

                if (!latestOccurances.Any())
                    continue;

                var expectedMin = GetExpectedMin(latestOccurances.Min(x => x.Date)).Plus(raid.FrequencyMin);
                var expectedMax = GetExpectedMax(latestOccurances.Max(x => x.Date)).Plus(raid.FrequencyMax);

                var missedRaids = 0;
                var currentInstant = Instant.FromDateTimeUtc(currentTimeUtc);
                while (currentInstant > expectedMax)
                {
                    expectedMin = expectedMin.Plus(raid.FrequencyMin);
                    expectedMax = expectedMax.Plus(raid.FrequencyMax);
                    missedRaids++;
                }

                var state = CreateState(raid, expectedMin, expectedMax, missedRaids);
                states.Add(state);
            }

            return states;
        }

        private StateDto CreateState(IRaid raid, Instant expectedMin, Instant expectedMax, int missedRaids)
        {
            return new StateDto
            {
                Raid = raidMapper.MapToRaidDto(raid),
                ExpectedMin = expectedMin.ToDateTimeUtc(),
                ExpectedMax = expectedMax.ToDateTimeUtc(),
                MissedRaids = missedRaids
            };
        }

        private int GetTotalAmountOfRaidsByCreature(IEnumerable<IRaid> raids, ISpawn spawn)
        {
            return raids
                .Where(x => x.Spawns.Any(y => y.CreatureId == spawn.CreatureId))
                .Count();
        }

        private IEnumerable<IScrape> GetAllOccurances(IEnumerable<IScrape> scrapes, ISpawn spawn)
        {
            return scrapes.Where(x => x.Statistics.Any(y => y.CreatureId == spawn.CreatureId));
        }

        private Instant GetExpectedMin(LocalDate date)
        {
            return Instant
                .FromDateTimeUtc(DateTime.SpecifyKind(date.ToDateTimeUnspecified(), DateTimeKind.Utc))
                .Plus(Duration.FromHours(2));
        }

        private Instant GetExpectedMax(LocalDate date)
        {
            return Instant
                .FromDateTimeUtc(DateTime.SpecifyKind(date.ToDateTimeUnspecified(), DateTimeKind.Utc))
                .Plus(Duration.FromDays(1))
                .Plus(Duration.FromHours(2));
        }
    }
}
