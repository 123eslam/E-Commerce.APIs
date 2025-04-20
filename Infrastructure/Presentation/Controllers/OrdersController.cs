using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared.OrderDtos;
using System.Net;
using System.Security.Claims;

namespace Presentation.Controllers
{
    public class OrdersController(IServiceManager _serviceManager) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(OrderResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResult>> Create(OrderRequest orderRequest)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _serviceManager.OrderService.CreateOrderAsync(orderRequest, email);
            return Ok(order);
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderResult>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<OrderResult>>> GetAllOreders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var orders = await _serviceManager.OrderService.GetAllOrdersByEmailAsync(email);
            return Ok(orders);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<OrderResult>> GetOrderById(Guid id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
        [HttpGet("DeliveryMethods")]
        [ProducesResponseType(typeof(IEnumerable<DeliveryMethodResult>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<DeliveryMethodResult>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManager.OrderService.GetAllDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }
    }
}
