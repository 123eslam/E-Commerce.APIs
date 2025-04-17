using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.BasketDtos;
using Shared.ErrorModels;
using System.Net;

namespace Presentation.Controllers
{
    public class BasketController(IServiceManager _serviceManager) : ApiController
    {
        [ProducesResponseType(typeof(BasketDto), (int)HttpStatusCode.OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> Get(string id)
        {
            var basket = await _serviceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }
        [ProducesResponseType(typeof(BasketDto), (int)HttpStatusCode.OK)]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> Update(BasketDto basketDto)
        {
            var basket = await _serviceManager.BasketService.UpdateBasketAsync(basketDto);
            return Ok(basket);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceManager.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
    }
}
