using Domain.Contracts;
using E_Commerce.Middlewares;

namespace E_Commerce.Extensions
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbIntializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
            await dbIntializer.IntializAsync();
            await dbIntializer.IntializIdentityAsync();
            return app;
        }
        public static WebApplication UseCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleware>();
            return app;
        }
    }
}
