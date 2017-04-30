using BossrLib.Models.Entities;
using BossrScraper.Factories;
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

namespace BossrScraper.Services
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

        public async Task<IEnumerable<ICreature>> GetCreaturesAsync()
        {
            await ValidateToken();
            var response = await client.GetAsync(configuration["BossrApi:Resources:Creatures"]);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Creature[]>(await response.Content.ReadAsStringAsync());

            return null;
        }

        public async Task<ScrapeDto> GetLatestScrapeAsync()
        {
            await ValidateToken();
            var response = await client.GetAsync(configuration["BossrApi:Resources:Scrapes"] + "/latest");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ScrapeDto>(await response.Content.ReadAsStringAsync());

            return null;
        }

        public async Task PostWorldAsync(IWorld world)
        {
            await ValidateToken();
            var worldJson = JsonConvert.SerializeObject(world);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Worlds"], new StringContent(worldJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                world.Id = JsonConvert.DeserializeObject<World>(await response.Content.ReadAsStringAsync()).Id;
        }

        public async Task PostCreatureAsync(ICreature creature)
        {
            await ValidateToken();
            var creatureJson = JsonConvert.SerializeObject(creature);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Creatures"], new StringContent(creatureJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                creature.Id = JsonConvert.DeserializeObject<Creature>(await response.Content.ReadAsStringAsync()).Id;
        }

        public async Task PostScrapeAsync(ScrapeDto scrape)
        {
            await ValidateToken();
            var scrapeJson = JsonConvert.SerializeObject(scrape);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Scrapes"], new StringContent(scrapeJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                scrape.Id = JsonConvert.DeserializeObject<ScrapeDto>(await response.Content.ReadAsStringAsync()).Id;
        }

        public async Task PostSpawnAsync(ISpawn spawn)
        {
            await ValidateToken();
            var spawnJson = JsonConvert.SerializeObject(spawn);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Spawns"], new StringContent(spawnJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                spawn.Id = JsonConvert.DeserializeObject<Spawn>(await response.Content.ReadAsStringAsync()).Id;
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
            if (Token == null || new JwtSecurityToken(Token.AccessToken).ValidTo < DateTime.UtcNow.AddMinutes(5))
            {
                await PostTokenAsync();
                SetHttpClientAuthentication(client);
            }
        }
    }
}