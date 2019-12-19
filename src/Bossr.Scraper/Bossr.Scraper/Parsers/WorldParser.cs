using Bossr.Lib.Models.Entities;
using Bossr.Scraper.Extensions;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bossr.Scraper.Parsers
{
    public interface IWorldParser
    {
        Task<IEnumerable<IWorld>> Parse(string response);
    }

    public class WorldParser : IWorldParser
    {
        public async Task<IEnumerable<IWorld>> Parse(string response)
        {
            var nodes = await ConvertToNodesAsync(response);
            return ConvertToEntity(nodes);
        }

        private async Task<IEnumerable<HtmlNode>> ConvertToNodesAsync(string response)
        {
            var document = new HtmlDocument();
            document.LoadHtml(response);
            var nodes = document
                .DocumentNode
                .SelectNodes("(//*[@class='TableContent'])[3]/tbody/tr")
                .Skip(1);
            
            return nodes;
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