namespace Shared.BasketDtos
{
    public record BasketDto
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDto> Items { get; init; }
        public string? PaymentIntentId { get; init; }
        public string? ClintSecret { get; init; }
        public decimal? ShippngPrice { get; init; }
        public int? DeliveryMethodId { get; init; }
    }
}
