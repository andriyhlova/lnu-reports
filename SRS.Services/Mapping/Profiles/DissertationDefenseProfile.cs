using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class DissertationDefenseProfile : Profile
    {
        public DissertationDefenseProfile()
        {
            CreateMap<DissertationDefense, DissertationDefenseModel>()
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisor()))
                .ForMember(dest => dest.UserDescription, opts => opts.MapFrom(src => src.GetUser()));

            CreateMap<DissertationDefenseModel, DissertationDefense>();
        }
    }
}
