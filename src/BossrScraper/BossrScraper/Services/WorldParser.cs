using BossrScraper.Models;
using BossrScraper.Services.Scraper;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services
{
    public class WorldParser : IWorldParser
    {
        private readonly IScraper scraper;
        public WorldParser(IScraper scraper)
        {
            this.scraper = scraper;
        }

        public async Task<IEnumerable<IWorld>> Parse()
        {
            var response = await scraper.Scrape("https://secure.tibia.com/community/?subtopic=worlds");
            var nodes = await ConvertToNodes(response);
            var worlds = nodes.Select(x => new World
            {
                Name = x.ChildNodes[0].InnerText.Trim(),
                PlayersOnline = x.ChildNodes[1].InnerText.Trim(),
                Location = x.ChildNodes[2].InnerText.Replace("&#160;", " ").Trim(),
                PvpType = x.ChildNodes[3].InnerText.Trim(),
                Tags = x.ChildNodes[4].InnerText.Trim()
            });
            return worlds;
        }

        public async Task<IEnumerable<HtmlNode>> ConvertToNodes(HttpResponseMessage response)
        {
            var document = new HtmlDocument();
            document.LoadHtml(await response.Content.ReadAsStringAsync());
            var nodes = document
                .DocumentNode
                .SelectSingleNode("//*[@class='InnerTableContainer']/table/tr[2]/td/div[2]/div/table")
                .ChildNodes
                .Skip(1)
                .Where(x => x.Name == "tr");
            return nodes;
        }
    }
}
