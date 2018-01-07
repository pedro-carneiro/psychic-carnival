namespace ToggleApi
{
    using Microsoft.AspNetCore.Http;
    using ToggleApi.Exceptions;
    using System.Net;
    using System.Threading.Tasks;

    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ResourceNotFoundException e)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                await context.Response.WriteAsync("");
            }
        }
    }
}
