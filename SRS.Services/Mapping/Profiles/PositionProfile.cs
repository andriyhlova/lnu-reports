using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionModel>().ReverseMap();
            CreateMap<PositionModel, PositionCsvModel>()
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Value));
        }
    }
}
