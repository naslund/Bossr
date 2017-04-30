using BossrLib.Models.Entities;
using BossrScraper.Factories;
using BossrScraper.Models.Authentication;
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
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private IAuthenticationToken token;

        public RestClient(IConfigurationFactory configurationFactory)
        {
            configuration = configurationFactory.CreateConfiguration();
            client.BaseAddress = new Uri(configuration["BossrApi:BaseUrl"]);
        }

        public async Task<IEnumerable<ICreature>> GetCreaturesAsync()
        {
            await RefreshToken();
            var response = await client.GetAsync(configuration["BossrApi:Resources:Creatures"]);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<Creature[]>(await response.Content.ReadAsStringAsync());

            return null;
        }

        public async Task<ScrapeDto> GetLatestScrapeAsync()
        {
            await RefreshToken();
            var response = await client.GetAsync(configuration["BossrApi:Resources:Scrapes"] + "/latest");

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ScrapeDto>(await response.Content.ReadAsStringAsync());

            return null;
        }

        public async Task<IEnumerable<IWorld>> GetWorldsAsync()
        {
            await RefreshToken();
            var response = await client.GetAsync(configuration["BossrApi:Resources:Worlds"]);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<World[]>(await response.Content.ReadAsStringAsync());

            return null;
        }

        public async Task PostCreatureAsync(ICreature creature)
        {
            await RefreshToken();
            var creatureJson = JsonConvert.SerializeObject(creature);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Creatures"], new StringContent(creatureJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                creature.Id = JsonConvert.DeserializeObject<Creature>(await response.Content.ReadAsStringAsync()).Id;
        }

        public async Task PostScrapeAsync(ScrapeDto scrape)
        {
            await RefreshToken();
            var scrapeJson = JsonConvert.SerializeObject(scrape);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Scrapes"], new StringContent(scrapeJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                scrape.Id = JsonConvert.DeserializeObject<ScrapeDto>(await response.Content.ReadAsStringAsync()).Id;
        }

        public async Task PostSpawnAsync(ISpawn spawn)
        {
            await RefreshToken();
            var spawnJson = JsonConvert.SerializeObject(spawn);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Spawns"], new StringContent(spawnJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                spawn.Id = JsonConvert.DeserializeObject<Spawn>(await response.Content.ReadAsStringAsync()).Id;
        }

        public async Task PostWorldAsync(IWorld world)
        {
            await RefreshToken();
            var worldJson = JsonConvert.SerializeObject(world);
            var response = await client.PostAsync(configuration["BossrApi:Resources:Worlds"], new StringContent(worldJson, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
                world.Id = JsonConvert.DeserializeObject<World>(await response.Content.ReadAsStringAsync()).Id;
        }

        private async Task PostTokenAsync()
        {
            var response = await client.PostAsync(configuration["BossrApi:Resources:Token"], new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", configuration["BossrApi:Username"]),
                new KeyValuePair<string, string>("password", configuration["BossrApi:Password"])
            }));

            if (response.IsSuccessStatusCode)
                token = JsonConvert.DeserializeObject<AuthenticationToken>(await response.Content.ReadAsStringAsync());
        }

        private async Task RefreshToken()
        {
            if (token == null || new JwtSecurityToken(token.AccessToken).ValidTo < DateTime.UtcNow.AddMinutes(5))
            {
                await PostTokenAsync();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }
        }
    }
}