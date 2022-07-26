using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using SRS.Services.Models.UserModels;

namespace SRS.Services.Mapping.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<IdentityRole, RoleModel>();
        }
    }
}
