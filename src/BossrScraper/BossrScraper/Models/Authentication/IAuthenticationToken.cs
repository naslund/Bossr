namespace BossrScraper.Models.Authentication
{
    public interface IAuthenticationToken
    {
        string AccessToken { get; set; }
        int ExpiresIn { get; set; }
    }
}
