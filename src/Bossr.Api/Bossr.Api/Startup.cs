using Bossr.Api.Attributes;
using Bossr.Api.Converters;
using Bossr.Api.Factories;
using Bossr.Api.Mappers;
using Bossr.Api.Middleware;
using Bossr.Api.Repositories;
using Bossr.Api.Services;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Bossr.Api
{
    public class Startup
    {
        private readonly IConfigurationRoot configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(configuration["ConnectionStrings:Default"]));

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

            services.AddMvc(x => { x.Filters.Add(new ModelStateValidationFilterAttribute()); })
                .AddJsonOptions(x => x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            ConfigureScopes(services);
        }

        private void ConfigureScopes(IServiceCollection services)
        {
            services.AddAuthorization(x =>
            {
                x.AddPolicy("CreateCategories", y => y.RequireClaim("scope", "categories.write"));
                x.AddPolicy("ReadCategories", y => y.RequireClaim("scope", "categories.read"));
                x.AddPolicy("UpdateCategories", y => y.RequireClaim("scope", "categories.write"));
                x.AddPolicy("DeleteCategories", y => y.RequireClaim("scope", "categories.write"));
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            SqlMapper.AddTypeHandler(new LocalDateTypeHandler());
            SqlMapper.AddTypeHandler(new DurationTypeHandler());

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtToken:SecretKey"]));

            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(new TokenProviderOptions
            {
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                Path = configuration["JwtToken:AccessTokenPath"],
                Expiration = TimeSpan.FromMinutes(int.Parse(configuration["JwtToken:ExpirationTimeMinutes"]))
            }));

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }
            });

            app.UseMvc();
        }
    }
}