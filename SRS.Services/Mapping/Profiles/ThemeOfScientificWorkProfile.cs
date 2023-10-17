using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.ThemeOfScientificWorkModels;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<ThemeOfScientificWork, ThemeOfScientificWorkModel>()
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisor()));

            CreateMap<ThemeOfScientificWorkModel, ThemeOfScientificWork>()
                .ForMember(dest => dest.IsActive, opts => opts.Ignore());
            CreateMap<ThemeOfScientificWorkFinancial, ThemeOfScientificWorkFinancialModel>().ReverseMap();

            CreateMap<ThemeOfScientificWorkCathedra, ThemeOfScientificWorkCathedraModel>()
                .ForMember(dest => dest.FacultyName, opts => opts.MapFrom(src => src.Cathedra.Faculty.Name));

            CreateMap<ThemeOfScientificWorkCathedraModel, ThemeOfScientificWorkCathedra>();

            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkModel>()
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisor()));

            CreateMap<BaseThemeOfScientificWorkModel, ThemeOfScientificWork>();

            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkWithFinancialsModel>()
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisorWithTitles()));

            CreateMap<BaseThemeOfScientificWorkWithFinancialsModel, ThemeOfScientificWork>();

            CreateMap<ThemeOfScientificWork, CathedraReportThemeOfScientificWorkModel>()
                .ForMember(dest => dest.SupervisorDescription, opts => opts.MapFrom(src => src.GetSupervisorWithTitles()));
        }
    }
}
