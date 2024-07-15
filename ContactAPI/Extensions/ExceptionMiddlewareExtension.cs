using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using ContactAPI.Middleware;

namespace ContactAPI.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

        }
    }
}