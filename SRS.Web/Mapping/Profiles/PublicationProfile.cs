using AutoMapper;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.PublicationModels;
using SRS.Web.Models.Publications;
using SRS.Web.Models.Shared;
using System;

namespace SRS.Web.Mapping.Profiles
{
    public class PublicationProfile : Profile
    {
        public PublicationProfile()
        {
            CreateMap<PublicationFilterViewModel, PublicationFilterModel>()
                .IncludeBase<DepartmentFilterViewModel, DepartmentFilterModel>();

            CreateMap<PublicationModel, PublicationEditViewModel>()
                .ForMember(dest => dest.Year, opts => opts.MapFrom(src => src.Date.Year))
                .ForMember(dest => dest.PublicationDate, opts => opts.MapFrom(src => src.Date));

            CreateMap<PublicationEditViewModel, PublicationModel>()
                .ForMember(dest => dest.Date, opts => opts.MapFrom(src => src.PublicationDate ?? src.ApplicationDate ?? (src.Year > 0 ? new DateTime(src.Year, 1, 1) : DateTime.Now)));
        }
    }
}
