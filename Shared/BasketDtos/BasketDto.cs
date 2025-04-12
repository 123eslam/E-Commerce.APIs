namespace Shared.BasketDtos
{
    public record BasketDto
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDto> Items { get; init; }
    }
}
