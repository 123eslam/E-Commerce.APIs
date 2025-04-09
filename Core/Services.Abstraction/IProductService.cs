using Shared.Parameters;
using Shared.ProductDtos;
using Shared.Results;

namespace Services.Abstraction
{
    public interface IProductService
    {
        //Get all products
        Task<PaginatedResult<ProductResultDto>> GetAllProductAsync(ProductSpecificationsParameters parameters);
        //Get all brands
        Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();
        //Get all types
        Task<IEnumerable<TypeResultDto>> GetAllTypeAsync();
        //Get product by id
        Task<ProductResultDto> GetProductByIdAsync(int id);
    }
}
