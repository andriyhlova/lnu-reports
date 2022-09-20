using AutoMapper;
using SRS.Services.Models.FilterModels;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.Shared;
using SRS.Web.Models.UsersManagement;

namespace SRS.Services.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserViewModel, UserInfoModel>();
            CreateMap<UserFilterViewModel, UserFilterModel>()
                .IncludeBase<BaseFilterViewModel, BaseFilterModel>();
        }
    }
}
