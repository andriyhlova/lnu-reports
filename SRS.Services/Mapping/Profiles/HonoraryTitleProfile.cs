using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class HonoraryTitleProfile : Profile
    {
        public HonoraryTitleProfile()
        {
            CreateMap<HonoraryTitle, HonoraryTitleModel>().ReverseMap();
        }
    }
}
