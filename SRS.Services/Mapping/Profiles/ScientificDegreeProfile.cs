using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class ScientificDegreeProfile : Profile
    {
        public ScientificDegreeProfile()
        {
            CreateMap<ScienceDegree, ScienceDegreeModel>().ReverseMap();
        }
    }
}
