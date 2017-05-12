using Bossr.Scraper.Extensions;
using Bossr.Scraper.Models.Entities;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Parsers
{
    public class StatisticsTableRowParser : IStatisticsTableRowParser
    {
        public async Task<IEnumerable<IStatisticsTableRow>> Parse(HttpResponseMessage response, int worldId)
        {
            var nodes = await ConvertToNodesAsync(response);
            return ConvertToEntities(nodes, worldId);
        }

        private async Task<IEnumerable<HtmlNode>> ConvertToNodesAsync(HttpResponseMessage response)
        {
            var document = new HtmlDocument();
            document.LoadHtml(await response.Content.ReadAsStringAsync());
            return document
                .DocumentNode
                .SelectSingleNode("//*[@id='killstatistics']/div[5]/div[1]/div[1]/table")
                .SelectNodes("tr[position() > 2 and position() < last()]");
        }

        private IEnumerable<IStatisticsTableRow> ConvertToEntities(IEnumerable<HtmlNode> nodes, int worldId)
        {
            return nodes.Select(x => new StatisticsTableRow
            {
                WorldId = worldId,
                CreatureName = x.ChildNodes[0].InnerText.HtmlDecodeAndTrim(),
                PlayersKilled = int.Parse(x.ChildNodes[1].InnerText.HtmlDecodeAndTrim()),
                CreaturesKilled = int.Parse(x.ChildNodes[2].InnerText.HtmlDecodeAndTrim())
            });
        }
    }
}
