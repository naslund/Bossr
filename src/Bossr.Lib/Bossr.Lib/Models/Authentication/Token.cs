namespace Bossr.Scraper.Models.Authentication
{
    public interface IToken
    {
        string AccessToken { get; set; }
        int ExpiresIn { get; set; }
    }

    public class Token : IToken
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
