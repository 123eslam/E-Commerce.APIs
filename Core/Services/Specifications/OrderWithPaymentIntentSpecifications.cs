using Domain.Contracts;
using Domain.Entities.Order_Entitie;

namespace Services.Specifications
{
    internal class OrderWithPaymentIntentSpecifications : Specifications<Order>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId) : base(o => o.PaymentIntentId == paymentIntentId)
        {
            
        }
    }
}
