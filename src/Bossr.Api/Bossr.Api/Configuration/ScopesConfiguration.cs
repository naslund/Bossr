using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Bossr.Api.Configuration
{
    public class ScopesConfiguration
    {
        public void ConfigureScopes(IServiceCollection services)
        {
            var policySets = new[]
            {
                new PolicySet { Scope = "categories.read", Policies = new [] { "ReadCategories" } },
                new PolicySet { Scope = "categories.write", Policies = new [] { "CreateCategories", "UpdateCategories", "DeleteCategories" } },
                new PolicySet { Scope = "creatures.read", Policies = new [] { "ReadCreatures" } },
                new PolicySet { Scope = "creatures.write", Policies = new [] { "CreateCreatures", "UpdateCreatures", "DeleteCreatures" } },
                new PolicySet { Scope = "positions.read", Policies = new [] { "ReadPositions" } },
                new PolicySet { Scope = "positions.write", Policies = new [] { "CreatePositions", "UpdatePositions", "DeletePositions" } },
                new PolicySet { Scope = "raids.read", Policies = new [] { "ReadRaids" } },
                new PolicySet { Scope = "raids.write", Policies = new [] { "CreateRaids", "UpdateRaids", "DeleteRaids" } },
                new PolicySet { Scope = "scrapes.read", Policies = new [] { "ReadScrapes" } },
                new PolicySet { Scope = "scrapes.write", Policies = new [] { "CreateScrapes", "UpdateScrapes", "DeleteScrapes" } },
                new PolicySet { Scope = "spawns.read", Policies = new [] { "ReadSpawns" } },
                new PolicySet { Scope = "spawns.write", Policies = new [] { "CreateSpawns", "UpdateSpawns", "DeleteSpawns" } },
                new PolicySet { Scope = "states.read", Policies = new [] { "ReadStates" } },
                new PolicySet { Scope = "statistics.read", Policies = new [] { "ReadStatistics" } },
                new PolicySet { Scope = "statistics.write", Policies = new [] { "CreateStatistics", "UpdateStatistics", "DeleteStatistics" } },
                new PolicySet { Scope = "tags.read", Policies = new [] { "ReadTags" } },
                new PolicySet { Scope = "tags.write", Policies = new [] { "CreateTags", "UpdateTags", "DeleteTags" } },
                new PolicySet { Scope = "users.read", Policies = new [] { "ReadUsers" } },
                new PolicySet { Scope = "users.write", Policies = new [] { "CreateUsers", "UpdateUsers", "DeleteUsers" } },
                new PolicySet { Scope = "worlds.read", Policies = new [] { "ReadWorlds" } },
                new PolicySet { Scope = "worlds.write", Policies = new [] { "CreateWorlds", "UpdateWorlds", "DeleteWorlds" } }
            };

            services.AddAuthorization(x =>
            {
                foreach (var set in policySets)
                {
                    foreach (var policy in set.Policies)
                    {
                        x.AddPolicy(policy, y => y.RequireClaim("scope", set.Scope));
                    }
                }
            });
        }

        private class PolicySet
        {
            public string Scope { get; set; }
            public string[] Policies { get; set; }
        }
    }
}
