using AutoMapper;
using SRS.Services.Models;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Publications;
using SRS.Web.Models.Shared;
using System;

namespace SRS.Services.Mapping.Profiles
{
    public class PublicationProfile : Profile
    {
        public PublicationProfile()
        {
            CreateMap<PublicationFilterViewModel, PublicationFilterModel>()
                .IncludeBase<DepartmentFilterViewModel, DepartmentFilterModel>();

            CreateMap<PublicationModel, PublicationEditViewModel>()
                .ForMember(dest => dest.Year, opts => opts.MapFrom(src => src.Date.Year));

            CreateMap<PublicationEditViewModel, PublicationModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => new DateTime(src.Year, 1, 1)));
        }
    }
}
