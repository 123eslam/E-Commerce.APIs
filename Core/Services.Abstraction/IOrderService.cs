using Shared.OrderDtos;

namespace Services.Abstraction
{
    public interface IOrderService
    {
        //Get order by id ==> OrderResult (Guid id)
        Task<OrderResult> GetOrderByIdAsync(Guid id);
        //Get all orders for user by email ==> IEnumerable<OrderResult> (string userEmail)
        Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string userEmail);
        //Create order ==> OrderResult (OrderRequest request, string userEmail)
        Task<OrderResult> CreateOrderAsync(OrderRequest request, string userEmail);
        //Get all delvivery methods ==> IEnumerable<DeliveryMethodResult> ()
        Task<IEnumerable<DeliveryMethodResult>> GetAllDeliveryMethodsAsync();
    }
}
