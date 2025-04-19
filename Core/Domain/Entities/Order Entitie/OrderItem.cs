namespace Domain.Entities.Order_Entitie
{
    public class OrderItem : BaseEntity<Guid>
    {
        public ProductInOrderItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
