namespace Domain.Entities.Order_Entitie
{
    public class Order : BaseEntity<Guid>
    {
        public string UserEmail { get; set; }
        public Address Address { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DeliveryMethod DeliveryMethod { get; set; }
        public int? DeliveryMethodId { get; set; }
        public decimal SubTotal { get; set; } //OrderItem.Price * OrderItem.Quantity  //Total ==> SubTotal + DeliveryMethod.Price 
        public DateTimeOffset OrderTime { get; set; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
