using System.ComponentModel.DataAnnotations;

namespace Shared.BasketDtos
{
    public record BasketItemDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string PictureUrl { get; init; }
        [Range(1, 99)]
        public int Quantity { get; init; }
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }
    }
}
