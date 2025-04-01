namespace Domain.Entities.Products
{
    public class ProductType : BaseEntity<int>
    {
        //Id
        public string Name { get; set; } = default!;
        //Navigation property many [Products]
    }
}