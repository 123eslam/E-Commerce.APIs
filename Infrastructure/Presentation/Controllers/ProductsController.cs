using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.ErrorModels;
using Shared.Parameters;
using Shared.ProductDtos;
using Shared.Results;
using System.Net;

namespace Presentation.Controllers
{
    [Authorize]
    public class ProductsController(IServiceManager ServiceManager) : ApiController
    {
        [ProducesResponseType(typeof(PaginatedResult<ProductResultDto>), (int)HttpStatusCode.OK)]
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters)
        {
            var products = await ServiceManager.ProductService.GetAllProductAsync(parameters);
            return Ok(products);
        }
        [ProducesResponseType(typeof(IEnumerable<BrandResultDto>), (int)HttpStatusCode.OK)]
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await ServiceManager.ProductService.GetAllBrandAsync();
            return Ok(brands);
        }
        [ProducesResponseType(typeof(IEnumerable<TypeResultDto>), (int)HttpStatusCode.OK)]
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var types = await ServiceManager.ProductService.GetAllTypeAsync();
            return Ok(types);
        }
        [ProducesResponseType(typeof(ProductResultDto), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var product = await ServiceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
    }
}
