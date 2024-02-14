using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class CathedraProfile : Profile
    {
        public CathedraProfile()
        {
            CreateMap<Cathedra, CathedraModel>().ReverseMap();
            CreateMap<CathedraModel, CathedraCsvModel>()
                .ForMember(dest => dest.FacultyName, opts => opts.MapFrom(src => src.Faculty.Name));
        }
    }
}
