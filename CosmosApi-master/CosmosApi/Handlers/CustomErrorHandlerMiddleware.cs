using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CosmosApi.Handlers
{
    public class CustomErrorHandlerMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public CustomErrorHandlerMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _nextDelegate(context);
            }
            catch(Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("An unhandled error occured. Please contact support.");
            }
        }
    }
}
