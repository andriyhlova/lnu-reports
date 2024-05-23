using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class HonoraryTitleProfile : Profile
    {
        public HonoraryTitleProfile()
        {
            CreateMap<HonoraryTitle, HonoraryTitleModel>().ReverseMap();
            CreateMap<HonoraryTitleModel, HonoraryTitleCsvModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Value));
        }
    }
}
