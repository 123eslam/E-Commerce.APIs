namespace Shared.Parameters
{
    public class ProductSpecificationsParameters
    {
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public ProductSortOptions? Sort { get; set; }
    }
    public enum ProductSortOptions
    {
        PriceAsc,
        PriceDesc,
        NameAsc,
        NameDesc
    }
}
