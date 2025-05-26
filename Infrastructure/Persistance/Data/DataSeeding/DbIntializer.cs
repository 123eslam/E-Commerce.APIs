using Domain.Entities.Identity;
using Domain.Entities.Order_Entitie;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Persistance.Data.DataSeeding
{
    public class DbIntializer : IDbIntializer
    {
        private readonly AppDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbIntializer(AppDbContext dbContext , RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task IntializAsync()
        {
            try
            {
                //if(_dbContext.Database.GetPendingMigrations().Any())
                //{
                    await _dbContext.Database.MigrateAsync();
                    if (!_dbContext.ProductTypes.Any())
                    {
                        //D:\E-Commerce.APIs\Infrastructure\Persistance\Data\DataSeeding\types.json
                        var typesData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\types.json");
                        //Convert from json file to c# object we use [desirlization]
                        var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                        if(types is not null && types.Any())
                        {
                            await _dbContext.AddRangeAsync(types);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.ProductBrands.Any())
                    {
                        //D:\E-Commerce.APIs\Infrastructure\Persistance\Data\DataSeeding\brands.json
                        var brandsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\brands.json");
                        //Convert from json file to c# object we use [desirlization]
                        var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                        if (brands is not null && brands.Any())
                        {
                            await _dbContext.AddRangeAsync(brands);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.Products.Any())
                    {
                        //D:\E-Commerce.APIs\Infrastructure\Persistance\Data\DataSeeding\products.json
                        var productsData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\products.json");
                        //Convert from json file to c# object we use [desirlization]
                        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                        if (products is not null && products.Any())
                        {
                            await _dbContext.AddRangeAsync(products);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    if (!_dbContext.DeliveryMethods.Any())
                    {
                        //D:\E-Commerce.APIs\Infrastructure\Persistance\Data\DataSeeding\delivery.json
                        var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistance\Data\DataSeeding\delivery.json");
                        //Convert from json file to c# object we use [desirlization]
                        var deliveries = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);
                        if (deliveries is not null && deliveries.Any())
                        {
                            await _dbContext.AddRangeAsync(deliveries);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                //}
            }
            catch(Exception ex)
            {

            }
        }

        public async Task IntializIdentityAsync()
        {
            //Add roles
            if (!_roleManager.Roles.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
            }
            //Add User and assign role 
            if (!_userManager.Users.Any())
            {
                var userAdmin = new User
                {
                    DisplayName = "Admin",
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    PhoneNumber = "01278637644"
                };
                var superAdminUser = new User
                {
                    DisplayName = "SuperAdmin",
                    UserName = "SuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    PhoneNumber = "01278637644"
                };
                await _userManager.CreateAsync(userAdmin, "P@ssw0rd");
                await _userManager.CreateAsync(superAdminUser, "Passw0rd@");
                await _userManager.AddToRoleAsync(userAdmin, "Admin");
                await _userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
            }
        }
    }
}
