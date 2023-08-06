using AutoMapper;
using Library.DTO;
using Library.Models;

namespace Library
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Event, EventInfo>().ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.GroupImage.Image.Image1));
            CreateMap<EventInfo, Event>().ForSourceMember(src => src.EventId, opt => opt.DoNotValidate());

            CreateMap<Banner, BannerInfo>();
            CreateMap<BannerInfo, Banner>().ForSourceMember(src => src.BannerId, opt => opt.DoNotValidate());

            CreateMap<User, UserInfo>();
            CreateMap<UserInfo, User>().ForSourceMember(src => src.UserId, opt => opt.DoNotValidate());

            CreateMap<Customer, CustomerInfo>();
            CreateMap<CustomerInfo, Customer>().ForSourceMember(src => src.CustomerId, opt => opt.DoNotValidate());

            CreateMap<Waiting, WaitingInfo>();
            CreateMap<WaitingInfo, Waiting>().ForSourceMember(src => src.WaitingId, opt => opt.DoNotValidate());
            CreateMap<CustomerInfo, WaitingInfo>()
                .ForSourceMember(src => src.CustomerId, opt => opt.DoNotValidate())
                .ForMember(dest => dest.CoffeeShopName, opt => opt.MapFrom(src => src.Name));
            CreateMap<WaitingInfo, UserInfo>().ForSourceMember(src => src.WaitingId, opt => opt.DoNotValidate());
            CreateMap<Customer, Waiting>().ForMember(dest => dest.CoffeeShopName, opt => opt.MapFrom(src => src.Name));

            CreateMap<Following, FollowInfo>();
            CreateMap<FollowInfo, Following>().ForSourceMember(src => src.FollowingId, opt => opt.DoNotValidate());

            CreateMap<Location, LocationInfo>();
            CreateMap<LocationInfo, Location>().ForSourceMember(src => src.LocationId, opt => opt.DoNotValidate());

            CreateMap<News, NewsInfo>().ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.GroupImage.Image.Image1));
            CreateMap<NewsInfo, News>().ForSourceMember(src => src.NewsId, opt => opt.DoNotValidate());

            CreateMap<Service, ServiceInfo>().ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.GroupImage.Image.Image1));
            CreateMap<ServiceInfo, Service>().ForSourceMember(src => src.ServiceId, opt => opt.DoNotValidate());

            CreateMap<Schedule, ScheduleInfo>();
            CreateMap<ScheduleInfo, Schedule>().ForSourceMember(src => src.ScheduleId, opt => opt.DoNotValidate());
        }
    }
}