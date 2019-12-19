using Bossr.Scraper.Factories;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Bossr.Scraper.Services
{
    public interface IDataFetcher
    {
        Task<string> FetchHttpResponse(string url);
    }

    public class DriverDataFetcher : IDataFetcher
    {
        public async Task<string> FetchHttpResponse(string url)
        {
            using (IWebDriver driver = new ChromeDriver(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName))
            {
                driver.Url = url;
                return driver.PageSource;
            };
        }
    }
}