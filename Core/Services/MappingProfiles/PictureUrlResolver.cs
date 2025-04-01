using AutoMapper;
using Domain.Entities.Products;
using Microsoft.Extensions.Configuration;
using Shared.ProductDtos;

namespace Services.MappingProfiles
{
    internal class PictureUrlResolver(IConfiguration Configuration) : IValueResolver<Product, ProductResultDto, string>
    {
        public string Resolve(Product source, ProductResultDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            return $"{Configuration["BaseUrl"]}{source.PictureUrl}";
        }
    }
}
