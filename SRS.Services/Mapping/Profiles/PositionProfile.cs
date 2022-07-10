using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionModel>().ReverseMap();
        }
    }
}
