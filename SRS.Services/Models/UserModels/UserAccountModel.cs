using System.Collections.Generic;
using System.Linq;
using SRS.Services.Providers;

namespace SRS.Services.Models.UserModels
{
    public class UserAccountModel : BaseUserModel
    {
        public string UserName { get; set; }

        public int? CathedraId { get; set; }

        public int? FacultyId { get; set; }

        public List<string> RoleIds { get; set; }

        public bool IsInRole(string rolename)
        {
            return RoleIds.Any(x => RolesProvider.AllRoles[x] == rolename);
        }
    }
}
