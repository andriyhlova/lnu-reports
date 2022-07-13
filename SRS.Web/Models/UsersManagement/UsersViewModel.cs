using PagedList;
using SRS.Services.Models;

namespace SRS.Web.Models.UsersManagement
{
    public class UsersViewModel
    {
        public UserFilterViewModel FilterModel { get; set; }

        public StaticPagedList<BaseUserInfoModel> Items { get; set; }
    }
}
