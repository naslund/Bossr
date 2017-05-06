using AutoMapper;
using Bossr.Api.Attributes;
using Bossr.Api.Converters;
using Bossr.Api.Factories;
using Bossr.Api.Middleware;
using Bossr.Api.Repositories;
using Bossr.Api.Services;
using Bossr.Lib.Models.Entities;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Bossr.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDbConnectionFactory>(new SqlConnectionFactory(Configuration["ConnectionString"]));

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IWorldRepository, WorldRepository>();
            services.AddTransient<ICreatureRepository, CreatureRepository>();
            services.AddTransient<ISpawnRepository, SpawnRepository>();
            services.AddTransient<IScrapeRepository, ScrapeRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ITagRepository, TagRepository>();

            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IHashGenerator, HashGenerator>();
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<ISaltGenerator, SaltGenerator>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IResponseWriter, ResponseWriter>();

            services.AddMvc(x => { x.Filters.Add(new ModelStateValidationFilterAttribute()); });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<User, UserDto>();
                x.CreateMap<Scrape, ScrapeDto>().ForMember(dest => dest.Date, opts => opts.MapFrom(y => LocalDateStringConverter.ToString(y.Date)));
                x.CreateMap<ScrapeDto, Scrape>().ForMember(dest => dest.Date, opts => opts.MapFrom(y => LocalDateStringConverter.ToLocalDate(y.Date)));
            });

            SqlMapper.AddTypeHandler(new LocalDateTypeHandler());

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["SecretKey"]));

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMiddleware<TokenProviderMiddleware>(Options.Create(new TokenProviderOptions
            {
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
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