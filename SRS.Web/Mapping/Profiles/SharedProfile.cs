using AutoMapper;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.Shared;

namespace SRS.Web.Mapping.Profiles
{
    public class SharedProfile : Profile
    {
        public SharedProfile()
        {
            CreateMap<BaseFilterViewModel, BaseFilterModel>()
                .ForMember(dest => dest.Skip, opts => opts.MapFrom(src => (src.Page.Value - 1) * PaginationValues.PageSize))
                .ForMember(dest => dest.Take, opts => opts.MapFrom(src => PaginationValues.PageSize));

            CreateMap<DepartmentFilterViewModel, DepartmentFilterModel>()
                .IncludeBase<BaseFilterViewModel, BaseFilterModel>();
        }
    }
}
