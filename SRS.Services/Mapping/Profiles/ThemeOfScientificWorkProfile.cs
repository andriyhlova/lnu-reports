using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class ThemeOfScientificWorkProfile : Profile
    {
        public ThemeOfScientificWorkProfile()
        {
            CreateMap<ThemeOfScientificWork, ThemeOfScientificWorkModel>()
                .ForMember(dest => dest.FacultyId, opts => opts.MapFrom(src => src.Cathedra.FacultyId));

            CreateMap<ThemeOfScientificWorkModel, ThemeOfScientificWork>();
        }
    }
}
