using System.Web.Mvc;
using AutoMapper;
using SRS.Domain.Enums;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<ThemeOfScientificWorkModel, SelectListItem>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Text, opts => opts.MapFrom(src => GetThemeOfScientificWorkText(src)));
        }

        private string GetThemeOfScientificWorkText(ThemeOfScientificWorkModel src)
        {
            if (src.Financial == Financial.БЮДЖЕТ)
            {
                return $"{src.Code} {src.Value}";
            }

            if (src.Financial == Financial.В_МЕЖАХ_РОБОЧОГО_ЧАСУ)
            {
                return $"{src.ScientificHead} {src.Value}";
            }

            return src.Value;
        }
    }
}
