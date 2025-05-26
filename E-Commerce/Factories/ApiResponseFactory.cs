using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;
using System.Net;

namespace E_Commerce.Factories
{
    public class ApiResponseFactory
    {
        public static IActionResult CustomValidationErrorResponse(ActionContext context)
        {
            //context ==> ModelState ==> Dictionary<string, ModelStateEntry>
            //string ==> Key , name of parameter
            //ModelStateEntry ==> object ==> Errors 
            //1] get all errors in modelstate entry
            var errors = context.ModelState.Where(error => error.Value.Errors.Any()).Select(error =>
            new ValidationError
            {
                Field = error.Key,
                Errors = error.Value.Errors.Select(e => e.ErrorMessage)
            });
            //2] create custom error response
            var response = new ValidationErrorResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                ErrorMessage = "Validation error",
                Errors = errors
            };
            //3] return 
            return new BadRequestObjectResult(response);
        }
    }
}
