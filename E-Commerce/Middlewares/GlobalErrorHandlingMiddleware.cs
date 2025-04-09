using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"Something went wrong : {ex}");
                // Handle the exception and return a custom error response
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Set content type [application/json]
            context.Response.ContentType = "application/json";
            //Set status code 
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
            context.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound, //400
                _ => (int)HttpStatusCode.InternalServerError //500
            };
            //return standard error response
            var response = new ErrorDetails //C# object
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = exception.Message
            }.ToString();
            //Convert C# object to JSON in ErrorDetails class in to string
            await context.Response.WriteAsync(response);
        }
    }
}
