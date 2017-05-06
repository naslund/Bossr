namespace Bossr.Scraper.Models.Authentication
{
    public class AuthenticationToken : IAuthenticationToken
    {
        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
    }
}
