using Domain.Contracts;
using Domain.Entities.Order_Entitie;

namespace Services.Specifications
{
    public class OrderWithIncludesSpecifications : Specifications<Order>
    {
        //Get all orders for user by email
        public OrderWithIncludesSpecifications(string userEmail) : base(o => o.UserEmail == userEmail)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
            AddOrderBy(o => o.OrderDate);
        }
        //Get order by id
        public OrderWithIncludesSpecifications(Guid id) : base(o => o.Id == id)
        {
            AddInclude(o => o.OrderItems);
            AddInclude(o => o.DeliveryMethod);
        }
    }
}
