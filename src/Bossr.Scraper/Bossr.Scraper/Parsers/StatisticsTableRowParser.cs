using Bossr.Scraper.Extensions;
using Bossr.Scraper.Models.Entities;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Parsers
{
    public interface IStatisticsTableRowParser
    {
        Task<IEnumerable<IStatisticsTableRow>> Parse(string response, int worldId);
    }

    public class StatisticsTableRowParser : IStatisticsTableRowParser
    {
        public async Task<IEnumerable<IStatisticsTableRow>> Parse(string response, int worldId)
        {
            var nodes = await ConvertToNodesAsync(response);
            return ConvertToEntities(nodes, worldId);
        }

        private async Task<IEnumerable<HtmlNode>> ConvertToNodesAsync(string response)
        {
            var document = new HtmlDocument();
            document.LoadHtml(response);

            return document
                .DocumentNode
                .SelectNodes("//form[@action='?subtopic=killstatistics']/../table/tbody/tr")
                .Skip(2)
                .SkipLast(1);
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
