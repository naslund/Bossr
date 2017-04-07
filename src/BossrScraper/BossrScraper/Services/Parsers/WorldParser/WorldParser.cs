using BossrScraper.Models;
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
            return ConvertToModel(nodes);
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

        private IEnumerable<IWorld> ConvertToModel(IEnumerable<HtmlNode> nodes)
        {
            return nodes.Select(x => new World
            {
                Name = x.ChildNodes[0].InnerText.HtmlDecodeAndTrim(),
                PlayersOnline = x.ChildNodes[1].InnerText.HtmlDecodeAndTrim(),
                Location = x.ChildNodes[2].InnerText.HtmlDecodeAndTrim(),
                PvpType = x.ChildNodes[3].InnerText.HtmlDecodeAndTrim(),
                Tags = x.ChildNodes[4].InnerText.HtmlDecodeAndTrim()
            });
        }
    }
}
