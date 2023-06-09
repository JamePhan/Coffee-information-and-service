using AutoMapper;
using Library.DTO;
using Library.Models;

namespace Library
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Event, EventInfo>();
            CreateMap<Banner, BannerInfo>();
            CreateMap<User, UserInfo>();
            CreateMap<Customer, CustomerInfo>();
        }
    }
}