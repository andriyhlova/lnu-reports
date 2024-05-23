using SRS.Web.Models.Shared;

namespace SRS.Web.Models.UsersManagement
{
    public class UserFilterViewModel : DepartmentFilterViewModel
    {
        public bool? IsActive { get; set; }

        public string RoleId { get; set; }
    }
}
