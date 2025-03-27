using AutoMapper;
using BarberSuite.Application.Features.Shops.Queries.GetShopsList;
using BarberSuite.Domain.Models.Shops;

namespace BarberSuite.Application.MapProfiles
{
    public class ShopProfile : Profile
    {
        public ShopProfile()
        {
            CreateMap<Shop, GetShopListVM>()
                .ForMember(dest => dest.Address,
                    opt => opt.MapFrom(src => $"{src.Address.Street}, {src.Address.PostalCode} {src.Address.City}"))
                .ForMember(dest => dest.ServiceCount,
                    opt => opt.MapFrom(src => src.Services.Count));
        }
    }
}
