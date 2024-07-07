using AutoMapper;
using EduWork.Common.DTO;
using EduWork.Data.Entities;

namespace EduWork.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {

            CreateMap<User, UserAppRoleDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id));

            CreateMap<WorkDayTime, WorkTimePartDTO>()
                .ForMember(dest => dest.WorkDate, opt => opt.MapFrom(src => src.WorkDay.WorkDate));

            CreateMap<SetWorkDayTimeDTO, WorkDay>();
            CreateMap<SetWorkDayTimeDTO, WorkDayTime>()
                .ForMember(dest => dest.WorkDay, opt => opt.MapFrom(src => src.WorkDate));
        }
    }
}
