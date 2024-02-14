using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Extensions;
using SRS.Services.Models;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.CsvModels;
using SRS.Services.Models.JournalModels;
using System.Linq;

namespace SRS.Services.Mapping.Profiles
{
    public class JournalProfile : Profile
    {
        public JournalProfile()
        {
            CreateMap<JournalType, BaseValueModel>().ReverseMap();
            CreateMap<Journal, JournalModel>().ReverseMap();

            CreateMap<JournalModel, JournalCsvModel>()
                .ForMember(dest => dest.JournalType, opts => opts.MapFrom(src => string
                .Join(", ", src.JournalTypes.OrderBy(x => x.Value).Select(x => x.Value))))
                .ForMember(dest => dest.BestQuartile, opts => opts.MapFrom(src => src.BestQuartile.GetDisplayName()));
        }
    }
}
