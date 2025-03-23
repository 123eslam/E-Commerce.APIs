using System.Text.Json;

namespace Persistance.Data.DataSeeding
{
    public class DbIntializer : IDbIntializer
    {
        private readonly AppDbContext _dbContext;

        public DbIntializer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task IntializAsync()
        {
            try
            {
                if(_dbContext.Database.GetPendingMigrations().Any())
                {
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
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
