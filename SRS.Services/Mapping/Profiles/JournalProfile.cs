using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.BaseModels;
using SRS.Services.Models.JournalModels;

namespace SRS.Services.Mapping.Profiles
{
    public class JournalProfile : Profile
    {
        public JournalProfile()
        {
            CreateMap<JournalType, BaseValueModel>().ReverseMap();
            CreateMap<Journal, JournalModel>().ReverseMap();
        }
    }
}
