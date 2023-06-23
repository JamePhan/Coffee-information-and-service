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
            CreateMap<BannerInfo, Banner>().ForSourceMember(src => src.BannerId, opt => opt.DoNotValidate());
            CreateMap<User, UserInfo>();
            CreateMap<Customer, CustomerInfo>();
            CreateMap<Following, FollowInfo>();
            CreateMap<FollowInfo, Following>().ForSourceMember(src => src.FollowingId, opt => opt.DoNotValidate());
            CreateMap<Location, LocationInfo>();
            CreateMap<LocationInfo, Location>().ForSourceMember(src => src.LocationId, opt => opt.DoNotValidate());
            CreateMap<Service, ServiceInfo>();
            CreateMap<ServiceInfo, Service>().ForSourceMember(src => src.ServiceId, opt => opt.DoNotValidate());
            CreateMap<Schedule, ScheduleInfo>();
            CreateMap<ScheduleInfo, Schedule>().ForSourceMember(src => src.ScheduleId, opt => opt.DoNotValidate());
        }
    }
}