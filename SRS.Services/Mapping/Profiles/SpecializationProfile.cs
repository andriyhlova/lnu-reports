using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class SpecializationProfile : Profile
    {
        public SpecializationProfile()
        {
            CreateMap<Specialization, SpecializationModel>()
                .ForMember(dest => dest.Value, opts => opts.MapFrom(src => src.Name)).ReverseMap();

            CreateMap<SpecializationModel, SpecializationCsvModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Value));
        }
    }
}
