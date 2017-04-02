using BossrApi.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BossrApi.Attributes
{
    public class SqlExceptionFilterAttribute : ExceptionFilterAttribute
    {
        int number;
        string message;

        public SqlExceptionFilterAttribute(int number, string message)
        {
            this.number = number;
            this.message = message;
        }

        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception as SqlException;

            if (exception?.Number == number)
                context.Result = new BadRequestObjectResult(new MessageResponse { Message = message });
        }
    }
}
