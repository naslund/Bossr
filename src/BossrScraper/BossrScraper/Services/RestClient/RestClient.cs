using BossrScraper.Factories.ConfigurationFactory;
using BossrScraper.Models.Authentication;
using BossrScraper.Models.Entities;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BossrScraper.Services.RestClient
{
    public class RestClient : IRestClient
    {
        private readonly IConfiguration configuration;
        private readonly HttpClient client;

        public RestClient(IConfigurationFactory configurationFactory)
        {
            configuration = configurationFactory.CreateConfiguration();
            client = new HttpClient();
            SetHttpClientBaseAdress(client);
        }

        private IAuthenticationToken Token { get; set; }

        public async Task<IEnumerable<IWorld>> GetWorldsAsync()
        {
            await ValidateToken();
            var response = await client.GetAsync(configuration["BossrApi:Resources:Worlds"]);
            
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<World[]>(await response.Content.ReadAsStringAsync());

            return null;
        }

        public async Task PostWorldAsync(IWorld world)
        {
            await ValidateToken();
            var worldJson = JsonConvert.SerializeObject(world);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Worlds"], new StringContent(worldJson, Encoding.UTF8, "application/json"));
        }

        private async Task PostTokenAsync()
        {
            var response = await client.PostAsync(configuration["BossrApi:Resources:Token"], new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", configuration["BossrApi:Username"]),
                new KeyValuePair<string, string>("password", configuration["BossrApi:Password"])
            }));

            if (response.IsSuccessStatusCode)
                Token = JsonConvert.DeserializeObject<AuthenticationToken>(await response.Content.ReadAsStringAsync());
        }

        private void SetHttpClientBaseAdress(HttpClient client)
        {
            client.BaseAddress = new Uri(configuration["BossrApi:BaseUrl"]);
        }

        private void SetHttpClientAuthentication(HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token.AccessToken);
        }

        private async Task ValidateToken()
        {
            if (Token == null || new JwtSecurityToken(Token.AccessToken).ValidTo > DateTime.UtcNow)
            {
                await PostTokenAsync();
                SetHttpClientAuthentication(client);
            }
        }
    }
}