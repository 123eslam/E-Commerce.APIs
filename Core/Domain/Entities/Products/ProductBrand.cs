namespace Domain.Entities.Products
{
    public class ProductBrand : BaseEntity<int>
    {
        //Id
        public string Name { get; set; } = default!;
        //Navigation property many [Products]
    }
}