using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace E_Commerce.Extensions
{
    public static class PresentitionServicesExtensions
    {
        public static IServiceCollection AddPresentitionServices(this IServiceCollection services)
        {
            //Presentation Services
            services.AddControllers()
                    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly)
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;//Func => Return IActionResult ,Take ActionContext
            });
            return services;
        }
    }
}
