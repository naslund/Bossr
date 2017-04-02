using AutoMapper;
using BossrApi.Attributes;
using BossrApi.Factories;
using BossrApi.Interfaces;
using BossrApi.Middleware.TokenProvider;
using BossrApi.Models.Dtos;
using BossrApi.Models.Pocos;
using BossrApi.Repositories.UserRepository;
using BossrApi.Services.HashGenerator;
using BossrApi.Services.PasswordValidator;
using BossrApi.Services.ResponseWriter;
using BossrApi.Services.SaltGenerator;
using BossrApi.Services.TokenGenerator;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BossrApi
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

            services.AddTransient<IHashGenerator, HashGenerator>();
            services.AddTransient<IPasswordValidator, PasswordValidator>();
            services.AddTransient<ISaltGenerator, SaltGenerator>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IResponseWriter, ResponseWriter>();

            services.AddMvc(x => { x.Filters.Add(new ModelStateValidationFilterAttribute()); });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Mapper.Initialize(x => { x.CreateMap<User, UserDto>(); });

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
