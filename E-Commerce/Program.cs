using E_Commerce.Extensions;

namespace E_Commerce
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Services
            //Presentation Services
            builder.Services.AddPresentitionServices();
            //Core Services
            builder.Services.AddCoreServices(builder.Configuration);
            //Infrastructure Services
            builder.Services.AddInfrastructureServices(builder.Configuration); 
            #endregion
            
            var app = builder.Build();

            #region Middleware
            app.UseCustomMiddleware();
            await app.SeedDbAsync();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}