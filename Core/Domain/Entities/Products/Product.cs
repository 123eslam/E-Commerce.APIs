namespace Domain.Entities.Products
{
    public class Product : BaseEntity<int>
    {
        //Id
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; } = default!;
        //Navigation property one [ProductBrand]
        public ProductBrand ProductBrand { get; set; } = default!;
        //FK
        public int BrandId { get; set; }
        //Navigation property one [ProductType]
        public ProductType ProductType { get; set; } = default!;
        //FK
        public int TypeId { get; set; }
    }
}