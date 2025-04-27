using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.BasketDtos;
using System.Net;

namespace Presentation.Controllers
{
    public class PaymentsController(IServiceManager serviceManager) : ApiController
    {
        [HttpPost("{basketId}")]
        [ProducesResponseType(typeof(BasketDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var result = await serviceManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            return Ok(result);
        }
    }
}
