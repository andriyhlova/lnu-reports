using System.Linq;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserModel>()
                .ForMember(dest => dest.FacultyId, opts => opts.MapFrom(src => src.Cathedra.FacultyId))
                .ForMember(dest => dest.RoleIds, opts => opts.MapFrom(src => src.Roles.Select(x => x.RoleId)));
        }
    }
}
