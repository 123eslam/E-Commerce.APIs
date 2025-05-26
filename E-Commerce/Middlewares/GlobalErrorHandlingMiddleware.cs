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
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                    await HandleNotFoundApiAsync(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                _logger.LogError($"Something went wrong : {ex}");
                // Handle the exception and return a custom error response
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundApiAsync(HttpContext context)
        {
            //Set content type [application/json]
            context.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                ErrorMessage = $"The end point {context.Request.Path} not found"
            }.ToString();
            await context.Response.WriteAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //Set content type [application/json]
            context.Response.ContentType = "application/json";
            //Set status code 
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
            //return standard error response
            var response = new ErrorDetails //C# object
            {
                ErrorMessage = exception.Message
            };
            context.Response.StatusCode = exception switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound, //400
                UnAuthorizedException => (int)HttpStatusCode.Unauthorized, //401
                ValidationException validationException => HandleValidationException(validationException, response),
                _ => (int)HttpStatusCode.InternalServerError //500
            };
            response.StatusCode = context.Response.StatusCode;
            //Convert C# object to JSON in ErrorDetails class in to string
            await context.Response.WriteAsync(response.ToString());
        }

        private int HandleValidationException(ValidationException validationException, ErrorDetails response)
        {
            response.Errors = validationException.Errors;
            return (int)HttpStatusCode.BadRequest; //400
        }
    }
}
