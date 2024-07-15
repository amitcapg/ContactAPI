using System.Net;

namespace ContactAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionMiddleware> _logger;

        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _environment = env;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                APIError response;
                string? message;
                var exceptionType = ex.GetType();
                int statusCode;

                if (exceptionType == typeof(UnauthorizedAccessException))
                {
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "You are not authorized";
                }
                else
                {
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = "Something went wrong";
                }

                if (_environment.IsDevelopment())
                {
                    response = new APIError(statusCode, ex.Message, errorDetails: ex.StackTrace ?? ex.StackTrace.ToString());
                }
                else
                {
                    response = new APIError(statusCode, message);
                }

                _logger.LogError(ex, "An error occurred: {errorMessage}", ex.Message);

                context.Response.ContentType = "application/json";

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(response.ToString());
            }
        }
    }
}