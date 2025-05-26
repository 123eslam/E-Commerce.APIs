namespace Shared.OrderDtos
{
    public record OrderRequest
    {
        public string BasketId { get; init; }
        public AddressDto shipToAddress { get; init; }
        public int DeliveryMethodId { get; init; }
    }
}
