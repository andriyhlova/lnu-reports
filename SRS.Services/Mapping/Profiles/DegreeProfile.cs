using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class DegreeProfile : Profile
    {
        public DegreeProfile()
        {
            CreateMap<Degree, DegreeModel>().ReverseMap();
        }
    }
}
