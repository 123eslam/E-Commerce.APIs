using AutoMapper;
using Domain.Entities.Identity;
using Shared.OrderDtos;

namespace Services.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Adress, AddressDto>().ReverseMap();
        }
    }
}
