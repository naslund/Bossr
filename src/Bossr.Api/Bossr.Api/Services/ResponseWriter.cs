using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Threading.Tasks;

namespace Bossr.Api.Services
{
    public interface IResponseWriter
    {
        Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, object content);
    }

    public class ResponseWriter : IResponseWriter
    {
        public async Task WriteResponseAsync(HttpContext context, HttpStatusCode statusCode, object content)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json; charset=utf-8";
            var serializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var response = JsonConvert.SerializeObject(content, serializerSettings);
            await context.Response.WriteAsync(response);
        }
    }
}