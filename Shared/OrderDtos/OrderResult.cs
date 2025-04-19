namespace Shared.OrderDtos
{
    public record OrderResult
    {
        public Guid Id { get; init; }
        public string UserEmail { get; init; }
        public AddressDto Address { get; init; }
        public IEnumerable<OrderItemDto> OrderItems { get; init; } = new List<OrderItemDto>();
        public string PaymentStatus { get; init; } 
        public string DeliveryMethod { get; init; }
        public decimal SubTotal { get; init; } //OrderItem.Price * OrderItem.Quantity  //Total ==> SubTotal + DeliveryMethod.Price 
        public DateTimeOffset OrderDate { get; init; } = DateTimeOffset.UtcNow;
        public string PaymentIntentId { get; init; } = string.Empty;
    }
}
