using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class AcademicStatusProfile : Profile
    {
        public AcademicStatusProfile()
        {
            CreateMap<AcademicStatus, AcademicStatusModel>().ReverseMap();
            CreateMap<AcademicStatusModel, AcademicStatusCsvModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Value));
        }
    }
}
