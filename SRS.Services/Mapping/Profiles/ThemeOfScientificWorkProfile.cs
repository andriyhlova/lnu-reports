using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<ThemeOfScientificWork, ThemeOfScientificWorkModel>().ReverseMap();
            CreateMap<ThemeOfScientificWorkFinancial, ThemeOfScientificWorkFinancialModel>().ReverseMap();

            CreateMap<ThemeOfScientificWork, BaseThemeOfScientificWorkModel>().ReverseMap();
        }
    }
}
