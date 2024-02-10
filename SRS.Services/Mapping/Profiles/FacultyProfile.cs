using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;
using SRS.Services.Models.CsvModels;

namespace SRS.Services.Mapping.Profiles
{
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<Faculty, FacultyModel>().ReverseMap();
            CreateMap<FacultyModel, FacultyCsvModel>();
        }
    }
}
