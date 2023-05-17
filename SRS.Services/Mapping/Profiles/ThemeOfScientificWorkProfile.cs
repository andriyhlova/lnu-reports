using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.ThemeOfScientificWorkModels;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<ThemeOfScientificWork, ThemeOfScientificWorkModel>();
            CreateMap<ThemeOfScientificWorkModel, ThemeOfScientificWork>()
                .ForMember(dest => dest.IsActive, opts => opts.Ignore());
            CreateMap<ThemeOfScientificWorkFinancial, ThemeOfScientificWorkFinancialModel>().ReverseMap();
            CreateMap<ThemeOfScientificWorkCathedra, ThemeOfScientificWorkCathedraModel>().ReverseMap();

            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkModel>().ReverseMap();
            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkWithFinancialsModel>().ReverseMap();
        }
    }
}
