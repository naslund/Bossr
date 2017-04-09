using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace BossrApi.Services.ResponseWriter
{
    public interface IResponseWriter
    {
        Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, object content);
    }
}