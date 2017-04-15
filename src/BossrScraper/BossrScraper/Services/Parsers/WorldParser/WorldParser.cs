using BossrScraper.Models.Entities;
using BossrScraper.Models.ScrapeItems;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BossrScraper.Services.Parsers.WorldParser
{
    public class WorldParser : IWorldParser
    {
        public async Task<IEnumerable<IWorld>> Parse(HttpResponseMessage response)
        {
            var nodes = await ConvertToNodesAsync(response);
            return ConvertToEntity(nodes);
        }

        private async Task<IEnumerable<HtmlNode>> ConvertToNodesAsync(HttpResponseMessage response)
        {
            var document = new HtmlDocument();
            document.LoadHtml(await response.Content.ReadAsStringAsync());
            return document
                .DocumentNode
                .SelectSingleNode("//*[@class='InnerTableContainer']/table/tr[2]/td/div[2]/div/table")
                .ChildNodes
                .Skip(1)
                .Where(x => x.Name == "tr");
        }

        private IEnumerable<IWorld> ConvertToEntity(IEnumerable<HtmlNode> nodes)
        {
            return nodes.Select(x => new World
            {
                Name = x.ChildNodes[0].InnerText.HtmlDecodeAndTrim()
            });
        }
    }
}