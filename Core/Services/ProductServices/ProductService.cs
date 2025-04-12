using AutoMapper;
using Domain.Contracts;
using Domain.Entities.Products;
using Domain.Exceptions;
using Services.Abstraction;
using Services.Specifications;
using Shared.Parameters;
using Shared.ProductDtos;
using Shared.Results;

namespace Services.ProductServices
{
    public class ProductService(IUnitOfWork UnitOfWork ,IMapper Mapper) : IProductService
    {
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandAsync()
        {
            //1. Retrieve all brands => UnitOfWork
            var brands = await UnitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            //2. Map to BrandResultDto => IMapper
            var brandsResult = Mapper.Map<IEnumerable<BrandResultDto>>(brands);
            //3. Return IEnumerable<BrandResultDto>
            return brandsResult;
        }

        public async Task<IEnumerable<TypeResultDto>> GetAllTypeAsync()
        {
            //1. Retrieve all types => UnitOfWork
            var types = await UnitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            //2. Map to TypeResultDto => IMapper
            var typesResult = Mapper.Map<IEnumerable<TypeResultDto>>(types);
            //3. Return IEnumerable<TypeResultDto>
            return typesResult;
        }

        public async Task<PaginatedResult<ProductResultDto>> GetAllProductAsync(ProductSpecificationsParameters parameters)
        {
            //1. Retrieve all products => UnitOfWork
            var products = await UnitOfWork.GetRepository<Product, int>().GetAllAsync(new ProductWithBrandAndTypeSpecifications(parameters));
            var totalCount = await UnitOfWork.GetRepository<Product, int>().CountAsync(new ProductCountSpecifications(parameters));
            //2. Map to ProductResultDto => IMapper
            var productsResult = Mapper.Map<IEnumerable<ProductResultDto>>(products);
            //3. Return PaginatedResult<ProductResultDto>
            //return productsResult;
            var result = new PaginatedResult<ProductResultDto>(
                productsResult.Count(),
                parameters.PageIndex,
                totalCount,
                productsResult
                );
            return result;
        }

        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
            //1. Retrieve product by id => UnitOfWork
            var product = await UnitOfWork.GetRepository<Product, int>().GetByIdAsync(new ProductWithBrandAndTypeSpecifications(id));
            //2. Map to ProductResultDto => IMapper
            var productResult = Mapper.Map<ProductResultDto>(product);
            //3. Return ProductResultDto
            //return productResult;
            return product is null ? throw new ProductNotFoundException(id) : productResult;
        }
    }
}