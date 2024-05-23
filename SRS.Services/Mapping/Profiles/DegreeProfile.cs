using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class DegreeProfile : Profile
    {
        public DegreeProfile()
        {
            CreateMap<Degree, DegreeModel>().ReverseMap();
            CreateMap<DegreeModel, DegreeCsvModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Value));
        }
    }
}
