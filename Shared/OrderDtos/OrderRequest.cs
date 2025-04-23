namespace Shared.OrderDtos
{
    public record OrderRequest
    {
        public string BasketId { get; init; }
        public AddressDto AddressDto { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
