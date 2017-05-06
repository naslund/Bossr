using System.Net;

namespace Bossr.Scraper.Extensions
{
    public static class StringExtensions
    {
        public static string HtmlDecodeAndTrim(this string source)
        {
            return WebUtility
                .HtmlDecode(source)
                .Replace("\u00A0", " ")
                .Trim();
        }
    }
}