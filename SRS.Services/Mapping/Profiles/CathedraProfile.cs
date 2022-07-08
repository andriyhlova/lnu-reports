using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class CathedraProfile : Profile
    {
        public CathedraProfile()
        {
            CreateMap<Cathedra, CathedraModel>().ReverseMap();
        }
    }
}
