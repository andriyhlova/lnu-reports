using AutoMapper;
using SRS.Domain.Entities;
using SRS.Web.Models.Account;
using System;

namespace SRS.Services.Models
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.BirthDate, opts => opts.MapFrom(src => new DateTime(1950, 1, 1)));
        }
    }
}
