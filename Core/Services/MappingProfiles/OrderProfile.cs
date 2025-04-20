using AutoMapper;
using Domain.Entities.Order_Entitie;
using Shared.OrderDtos;

namespace Services.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodResult>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, opt => opt.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, opt => opt.MapFrom(s => s.Product.PictureUrl));
            CreateMap<Order, OrderResult>()
                .ForMember(d => d.PaymentStatus, opt => opt.MapFrom(s => s.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, opt => opt.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, opt => opt.MapFrom(s => s.SubTotal + s.DeliveryMethod.Price));
        }
    }
}
