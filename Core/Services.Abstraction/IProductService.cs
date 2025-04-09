using Shared.Parameters;
using Shared.ProductDtos;

namespace Services.Abstraction
{
    public interface IProductService
    {
        //Get all products
        Task<IEnumerable<ProductResultDto>> GetAllProductAsync(ProductSpecificationsParameters parameters);
        //Get all brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();
        //Get all types
        Task<IEnumerable<TypeResultDto>> GetAllTypeAsync();
        //Get product by id
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
