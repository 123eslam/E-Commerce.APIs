using Domain.Contracts;
using Domain.Entities.Products;

namespace Services.Specifications
{
    internal class ProductWithBrandAndTypeSpecifications : Specifications<Product>
    {
        //Use to retrieve a product by id
        public ProductWithBrandAndTypeSpecifications(int id)
            : base(product => product.Id == id)
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);
        }

        //Use to retrieve all products
        public ProductWithBrandAndTypeSpecifications(string? sort, int? brandId, int? typeId)
            : base(product =>
            (!brandId.HasValue || product.BrandId == brandId.Value)&&
            (!typeId.HasValue || product.TypeId == typeId.Value))
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

            if (!string.IsNullOrWhiteSpace(sort))
            {
                switch (sort.ToLower().Trim())
                {
                    case "pricedesc":
                        AddOrderByDescending(product => product.Price);
                        break;
                    case "priceasc":
                        AddOrderBy(product => product.Price);
                        break;
                    case "namedesc":
                        AddOrderByDescending(product => product.Name);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
        }
    }
}
