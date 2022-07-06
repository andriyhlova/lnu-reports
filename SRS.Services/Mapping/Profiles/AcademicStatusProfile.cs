using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class AcademicStatusProfile : Profile
    {
        public AcademicStatusProfile()
        {
            CreateMap<AcademicStatus, AcademicStatusModel>().ReverseMap();
        }
    }
}
