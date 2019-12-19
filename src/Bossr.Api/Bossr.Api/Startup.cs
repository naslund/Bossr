using Bossr.Api.Attributes;
using Bossr.Api.Configuration;
using Bossr.Api.Converters;
using Bossr.Api.Factories;
using Bossr.Api.Middleware;
using Dapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
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

            new DependencyConfiguration().ConfigureDependencies(services);

            services.AddMvc(x => { x.Filters.Add(new ModelStateValidationFilterAttribute()); })
                .AddJsonOptions(x => x.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore);

            new ScopesConfiguration().ConfigureScopes(services);

            services.AddSwaggerGen(x => x.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Bossr API", Version = "v1" }));
        }

        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            SqlMapper.AddTypeHandler(new DateTimeTypeHandler());

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

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

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

            app.UseSwagger();
            
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Bossr API v1"); });
        }
    }
}