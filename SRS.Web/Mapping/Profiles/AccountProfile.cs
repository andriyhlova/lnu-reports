using System;
using AutoMapper;
using SRS.Domain.Entities;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.Account;
using SRS.Web.Models.ThemeOfScientificWorks;

namespace SRS.Web.Mapping.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(_ => new DateTime(1950, 1, 1)));

            CreateMap<ExternalPartTimeEmployeeViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(_ => new DateTime(1950, 1, 1)));

            CreateMap<ProfileInfoModel, UpdateProfileViewModel>()
                .ForMember(dest => dest.GraduationDate, opts => opts.MapFrom(src => src.GraduationDate.HasValue ? src.GraduationDate.Value.Year : (int?)null))
                .ForMember(dest => dest.AspirantStartYear, opts => opts.MapFrom(src => src.AspirantStartYear.HasValue ? src.AspirantStartYear.Value.Year : (int?)null))
                .ForMember(dest => dest.AspirantFinishYear, opts => opts.MapFrom(src => src.AspirantFinishYear.HasValue ? src.AspirantFinishYear.Value.Year : (int?)null))
                .ForMember(dest => dest.DegreeDefenseYear, opts => opts.MapFrom(src => src.DegreeDefenseYear.HasValue ? src.DegreeDefenseYear.Value.Year : (int?)null))
                .ForMember(dest => dest.DoctorStartYear, opts => opts.MapFrom(src => src.DoctorStartYear.HasValue ? src.DoctorStartYear.Value.Year : (int?)null))
                .ForMember(dest => dest.DoctorFinishYear, opts => opts.MapFrom(src => src.DoctorFinishYear.HasValue ? src.DoctorFinishYear.Value.Year : (int?)null))
                .ForMember(dest => dest.AcademicStatusDefenseYear, opts => opts.MapFrom(src => src.AcademicStatusDefenseYear.HasValue ? src.AcademicStatusDefenseYear.Value.Year : (int?)null));

            CreateMap<UpdateProfileViewModel, ProfileInfoModel>()
                .ForMember(dest => dest.GraduationDate, opts => opts.MapFrom(src => src.GraduationDate.HasValue ? new DateTime(src.GraduationDate.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.AspirantStartYear, opts => opts.MapFrom(src => src.AspirantStartYear.HasValue ? new DateTime(src.AspirantStartYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.AspirantFinishYear, opts => opts.MapFrom(src => src.AspirantFinishYear.HasValue ? new DateTime(src.AspirantFinishYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.DegreeDefenseYear, opts => opts.MapFrom(src => src.DegreeDefenseYear.HasValue ? new DateTime(src.DegreeDefenseYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.DoctorStartYear, opts => opts.MapFrom(src => src.DoctorStartYear.HasValue ? new DateTime(src.DoctorStartYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.DoctorFinishYear, opts => opts.MapFrom(src => src.DoctorFinishYear.HasValue ? new DateTime(src.DoctorFinishYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.AcademicStatusDefenseYear, opts => opts.MapFrom(src => src.AcademicStatusDefenseYear.HasValue ? new DateTime(src.AcademicStatusDefenseYear.Value, 1, 1) : (DateTime?)null));

            CreateMap<UserDegreeModel, UserDegreeViewModel>()
                .ForMember(dest => dest.AwardYear, opts => opts.MapFrom(src => src.AwardDate.Year));

            CreateMap<UserDegreeViewModel, UserDegreeModel>()
                .ForMember(dest => dest.AwardDate, opts => opts.MapFrom(src => new DateTime(src.AwardYear, 1, 1)));

            CreateMap<UserAcademicStatusModel, UserAcademicStatusViewModel>()
                .ForMember(dest => dest.AwardYear, opts => opts.MapFrom(src => src.AwardDate.Year));

            CreateMap<UserAcademicStatusViewModel, UserAcademicStatusModel>()
                .ForMember(dest => dest.AwardDate, opts => opts.MapFrom(src => new DateTime(src.AwardYear, 1, 1)));

            CreateMap<UserHonoraryTitleModel, UserHonoraryTitleViewModel>()
                .ForMember(dest => dest.AwardYear, opts => opts.MapFrom(src => src.AwardDate.Year));

            CreateMap<UserHonoraryTitleViewModel, UserHonoraryTitleModel>()
                .ForMember(dest => dest.AwardDate, opts => opts.MapFrom(src => new DateTime(src.AwardYear, 1, 1)));
        }
    }
}
