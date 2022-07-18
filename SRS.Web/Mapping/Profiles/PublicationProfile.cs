using AutoMapper;
using SRS.Services.Models.FilterModels;
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
        }
    }
}
