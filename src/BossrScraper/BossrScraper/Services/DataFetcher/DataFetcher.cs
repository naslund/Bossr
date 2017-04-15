﻿using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.DataFetcher
{
    public class DataFetcher : IDataFetcher
    {
        public async Task<HttpResponseMessage> FetchHttpResponse(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetAsync(url);
            }
        }
    }
}