using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.Parameters;
using Shared.ProductDtos;

namespace Presentation.Controllers
{
    [ApiController]              //To define a controller is an API controller
    [Route("api/[controller]")] //baseUrl/api/WeatherForecast
    public class ProductsController(IServiceManager ServiceManager) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters parameters)
        {
            var products = await ServiceManager.ProductService.GetAllProductAsync(parameters);
            return Ok(products);
        }
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<BrandResultDto>>> GetAllBrands()
        {
            var brands = await ServiceManager.ProductService.GetAllBrandAsync();
            return Ok(brands);
        }
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<TypeResultDto>>> GetAllTypes()
        {
            var types = await ServiceManager.ProductService.GetAllTypeAsync();
            return Ok(types);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id)
        {
            var product = await ServiceManager.ProductService.GetProductByIdAsync(id);
            return Ok(product);
        }
    }
}
