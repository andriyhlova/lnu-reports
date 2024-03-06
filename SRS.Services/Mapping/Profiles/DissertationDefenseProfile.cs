using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Extensions;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class DissertationDefenseProfile : Profile
    {
        public DissertationDefenseProfile()
        {
            CreateMap<DissertationDefense, DissertationDefenseModel>()
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisor()))
                .ForMember(dest => dest.UserDescription, opts => opts.MapFrom(src => src.GetUser()));

            CreateMap<DissertationDefenseModel, DissertationDefenseCsvModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Theme))
                .ForMember(dest => dest.DissertationType, opts => opts.MapFrom(src => src.DissertationType != null ? src.DissertationType.GetDisplayName() : string.Empty));

            CreateMap<DissertationDefenseModel, DissertationDefense>();
        }
    }
}
