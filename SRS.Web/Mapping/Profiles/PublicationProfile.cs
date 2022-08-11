using System;
using AutoMapper;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.PublicationModels;
using SRS.Web.Models.Publications;
using SRS.Web.Models.Shared;

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
