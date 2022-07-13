using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using SRS.Domain.Entities;
using SRS.Services.Models;

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
