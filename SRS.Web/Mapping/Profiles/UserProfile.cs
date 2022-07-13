using AutoMapper;
using SRS.Services.Models;
using SRS.Services.Models.Constants;
using SRS.Services.Models.FilterModels;
using SRS.Web.Models.UsersManagement;

namespace SRS.Services.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserViewModel, UserInfoModel>();
            CreateMap<UserFilterViewModel, UserFilterModel>()
                .ForMember(dest => dest.Skip, opts => opts.MapFrom(src => (src.Page.Value - 1) * PaginationValues.PageSize))
                .ForMember(dest => dest.Take, opts => opts.MapFrom(src => PaginationValues.PageSize));
        }
    }
}
