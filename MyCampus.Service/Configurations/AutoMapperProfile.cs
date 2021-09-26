using AutoMapper;
using MyCampus.Domain.Academics;
using MyCampus.Domain.PersonRoles;
using MyCampus.Service.Dtos.Academics;
using MyCampus.Service.Dtos.PersonRoles;

namespace MyCampus.Service.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Course, CourseOutputDto>();
            CreateMap<AppUser, RegisterOutputDto>()
                .ForMember(dst => dst.Username, act => act.MapFrom(src => src.UserName));
        }
    }
}
