using AutoMapper;
using SRS.Web.Models.Account;
using System;

namespace SRS.Services.Models
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserInfoModel, UpdateViewModel>()
                .ForMember(dest => dest.GraduationDate, opts => opts.MapFrom(src => src.GraduationDate.HasValue ? src.GraduationDate.Value.Year : (int?)null))
                .ForMember(dest => dest.AwardingDate, opts => opts.MapFrom(src => src.AwardingDate.HasValue ? src.AwardingDate.Value.Year : (int?)null))
                .ForMember(dest => dest.DefenseYear, opts => opts.MapFrom(src => src.DefenseYear.HasValue ? src.DefenseYear.Value.Year : (int?)null))
                .ForMember(dest => dest.AspirantStartYear, opts => opts.MapFrom(src => src.AspirantStartYear.HasValue ? src.AspirantStartYear.Value.Year : (int?)null))
                .ForMember(dest => dest.AspirantFinishYear, opts => opts.MapFrom(src => src.AspirantFinishYear.HasValue ? src.AspirantFinishYear.Value.Year : (int?)null))
                .ForMember(dest => dest.DoctorStartYear, opts => opts.MapFrom(src => src.DoctorStartYear.HasValue ? src.DoctorStartYear.Value.Year : (int?)null))
                .ForMember(dest => dest.DoctorFinishYear, opts => opts.MapFrom(src => src.DoctorFinishYear.HasValue ? src.DoctorFinishYear.Value.Year : (int?)null));

            CreateMap<UpdateViewModel, UserInfoModel>()
                .ForMember(dest => dest.GraduationDate, opts => opts.MapFrom(src => src.GraduationDate.HasValue ? new DateTime(src.GraduationDate.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.AwardingDate, opts => opts.MapFrom(src => src.AwardingDate.HasValue ? new DateTime(src.AwardingDate.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.DefenseYear, opts => opts.MapFrom(src => src.DefenseYear.HasValue ? new DateTime(src.DefenseYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.AspirantStartYear, opts => opts.MapFrom(src => src.AspirantStartYear.HasValue ? new DateTime(src.AspirantStartYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.AspirantFinishYear, opts => opts.MapFrom(src => src.AspirantFinishYear.HasValue ? new DateTime(src.AspirantFinishYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.DoctorStartYear, opts => opts.MapFrom(src => src.DoctorStartYear.HasValue ? new DateTime(src.DoctorStartYear.Value, 1, 1) : (DateTime?)null))
                .ForMember(dest => dest.DoctorFinishYear, opts => opts.MapFrom(src => src.DoctorFinishYear.HasValue ? new DateTime(src.DoctorFinishYear.Value, 1, 1) : (DateTime?)null));
        }
    }
}
