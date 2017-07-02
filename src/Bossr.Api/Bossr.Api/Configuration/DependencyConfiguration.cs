using Bossr.Api.Factories;
using Bossr.Api.Mappers;
using Bossr.Api.Repositories;
using Bossr.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Bossr.Api.Configuration
{
    public class DependencyConfiguration
    {
        public void ConfigureDependencies(IServiceCollection services)
        {
            services.AddTransient<JwtSecurityTokenHandler>();

            services.AddTransient<ITokenResponseFactory, TokenResponseFactory>();
            services.AddTransient<IJwtTokenFactory, JwtTokenFactory>();
            services.AddTransient<IClaimsFetcher, ClaimsFetcher>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWorldRepository, WorldRepository>();
            services.AddTransient<ICreatureRepository, CreatureRepository>();
            services.AddTransient<IScrapeRepository, ScrapeRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITagRepository, TagRepository>();
            services.AddTransient<IRaidRepository, RaidRepository>();
            services.AddTransient<ISpawnRepository, SpawnRepository>();
            services.AddTransient<IStatisticRepository, StatisticRepository>();
            services.AddTransient<IScopeRepository, ScopeRepository>();

            services.AddTransient<IRaidMapper, RaidMapper>();
            services.AddTransient<IScrapeMapper, ScrapeMapper>();
            services.AddTransient<IUserMapper, UserMapper>();

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IHashGenerator, HashGenerator>();
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<ISaltGenerator, SaltGenerator>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IResponseWriter, ResponseWriter>();
            services.AddTransient<IStateCalculator, StateCalculator>();
        }
    }
}
