using AutoMapper;
using SRS.Services.Models.UserModels;
using SRS.Web.Models.UsersManagement;

namespace SRS.Services.Mapping.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UpdateUserViewModel, UserInfoModel>();
        }
    }
}
