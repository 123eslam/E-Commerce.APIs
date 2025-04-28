using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithOrigins("http://localhost:4200");
                });
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponse;//Func => Return IActionResult ,Take ActionContext
            });
            return services;
        }
    }
}
