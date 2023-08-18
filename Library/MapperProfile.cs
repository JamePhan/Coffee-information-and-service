using AutoMapper;
using Library.DTO;
using Library.Models;

namespace Library
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Event, EventInfo>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.GroupImage.Image.Image1))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Location.Address))
                .ForMember(dest => dest.CoffeeShopName, opt => opt.MapFrom(src => src.User.CoffeeShopName));
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

            CreateMap<Following, FollowInfo>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
            CreateMap<FollowInfo, Following>()
                .ForSourceMember(src => src.FollowingId, opt => opt.DoNotValidate())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.CustomerId))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<Location, LocationInfo>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
            CreateMap<LocationInfo, Location>()
                .ForSourceMember(src => src.LocationId, opt => opt.DoNotValidate())
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.UserId))
                .ForMember(dest => dest.User, opt => opt.Ignore());

            CreateMap<News, NewsInfo>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.GroupImage.Image.Image1))
                .ForMember(dest => dest.CoffeeShopName, opt => opt.MapFrom(src => src.User.CoffeeShopName));
            CreateMap<NewsInfo, News>().ForSourceMember(src => src.NewsId, opt => opt.DoNotValidate());

            CreateMap<Service, ServiceInfo>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.GroupImage.Image.Image1))
                .ForMember(dest => dest.CoffeeShopName, opt => opt.MapFrom(src => src.User.CoffeeShopName));
            CreateMap<ServiceInfo, Service>().ForSourceMember(src => src.ServiceId, opt => opt.DoNotValidate());

            CreateMap<Schedule, ScheduleInfo>()
                .ForMember(dest => dest.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
            CreateMap<ScheduleInfo, Schedule>()
                .ForSourceMember(src => src.ScheduleId, opt => opt.DoNotValidate())
                .ForMember(dest => dest.EventId, opt => opt.MapFrom(src => src.Event.EventId))
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer.CustomerId))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Event, opt => opt.Ignore());
        }
    }
}