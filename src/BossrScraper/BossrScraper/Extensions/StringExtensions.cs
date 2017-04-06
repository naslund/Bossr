using System.Net;

namespace BossrScraper
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
