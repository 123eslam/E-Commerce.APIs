using Domain.Contracts;
using Domain.Entities.Products;
using Shared.Parameters;

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
        public ProductWithBrandAndTypeSpecifications(ProductSpecificationsParameters parameters)
            : base(product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId.Value)&&
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId.Value)&&
            (string.IsNullOrWhiteSpace(parameters.Search) || product.Name.ToLower().Contains(parameters.Search.ToLower().Trim()))
            )
        {
            AddInclude(product => product.ProductBrand);
            AddInclude(product => product.ProductType);

            if (parameters.Sort is not null)
            {
                switch (parameters.Sort)
                {
                    case ProductSortOptions.PriceDesc:
                        AddOrderByDescending(product => product.Price);
                        break;
                    case ProductSortOptions.PriceAsc:
                        AddOrderBy(product => product.Price);
                        break;
                    case ProductSortOptions.NameDesc:
                        AddOrderByDescending(product => product.Name);
                        break;
                    default:
                        AddOrderBy(product => product.Name);
                        break;
                }
            }
            ApplyPagination(parameters.PageIndex, parameters.PageSize);
        }
    }
}
