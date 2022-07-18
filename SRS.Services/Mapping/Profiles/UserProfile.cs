using System.Linq;
using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using SRS.Domain.Entities;
using SRS.Domain.Enums;
using SRS.Services.Models;

namespace SRS.Services.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserAccountModel>()
                .ForMember(dest => dest.FacultyId, opts => opts.MapFrom(src => src.Cathedra.FacultyId))
                .ForMember(dest => dest.RoleIds, opts => opts.MapFrom(src => src.Roles.Select(x => x.RoleId)));

            CreateMap<ApplicationUser, ProfileInfoModel>().ReverseMap();

            CreateMap<ApplicationUser, BaseUserInfoModel>()
                .ForMember(dest => dest.RoleIds, opts => opts.MapFrom(src => src.Roles.Select(x => x.RoleId)));

            CreateMap<ApplicationUser, UserInitialsModel>()
                .ForMember(dest => dest.FacultyId, opts => opts.MapFrom(src => src.Cathedra.FacultyId))
                .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA).FirstName))
                .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA).LastName))
                .ForMember(dest => dest.FathersName, opts => opts.MapFrom(src => src.I18nUserInitials.FirstOrDefault(x => x.Language == Language.UA).FathersName));

            CreateMap<ApplicationUser, UserInfoModel>()
                .IncludeBase<ApplicationUser, BaseUserInfoModel>()
                .ForMember(dest => dest.FacultyId, opts => opts.MapFrom(src => src.Cathedra.FacultyId))
                .ForMember(dest => dest.FacultyName, opts => opts.MapFrom(src => src.Cathedra != null && src.Cathedra.Faculty != null ? src.Cathedra.Faculty.Name : null))
                .ForMember(dest => dest.AcademicStatusName, opts => opts.MapFrom(src => src.AcademicStatus != null ? src.AcademicStatus.Value : null))
                .ForMember(dest => dest.ScienceDegreeName, opts => opts.MapFrom(src => src.ScienceDegree != null ? src.ScienceDegree.Value : null))
                .ForMember(dest => dest.PositionName, opts => opts.MapFrom(src => src.Position != null ? src.Position.Value : null));

            CreateMap<UserInfoModel, ApplicationUser>()
                .ForMember(dest => dest.Roles, opts => opts.MapFrom(src => src.RoleIds.Select(x => new IdentityUserRole { RoleId = x, UserId = src.Id })));
        }
    }
}
