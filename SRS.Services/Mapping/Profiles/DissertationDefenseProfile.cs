using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Extensions;
using SRS.Services.Models;
using SRS.Services.Models.CathedraReportModels;
using SRS.Services.Models.ReportGenerationModels.CathedraReport;

namespace SRS.Services.Mapping.Profiles
{
    public class DissertationDefenseProfile : Profile
    {
        public DissertationDefenseProfile()
        {
            CreateMap<DissertationDefense, DissertationDefenseModel>()
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisor()))
                .ForMember(dest => dest.UserDescription, opts => opts.MapFrom(src => src.GetUser()))
                .ForMember(dest => dest.SpecializationName, opts => opts.MapFrom(src => src.Specialization.Name))
                .ForMember(dest => dest.SpecializationCode, opts => opts.MapFrom(src => src.Specialization.Code));

            CreateMap<DissertationDefenseModel, DissertationDefense>();

            CreateMap<DissertationDefense, CathedraReportDissertarionDefenseModel>()
                .ForMember(dest => dest.DefenseDate, opts => opts.MapFrom(src => src.DefenseDate.Date.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.SubmissionDate, opts => opts.MapFrom(src => src.SubmissionDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisor()))
                .ForMember(dest => dest.UserDescription, opts => opts.MapFrom(src => src.GetUser()))
                .ForMember(dest => dest.DissertationType, opts => opts.MapFrom(src => src.DissertationType.GetDisplayName()))
                .ForMember(dest => dest.PositionAndCathedra, opts => opts.MapFrom(src =>
                src.User.Position.Value + ", " + src.User.Cathedra.Name));
        }
    }
}
