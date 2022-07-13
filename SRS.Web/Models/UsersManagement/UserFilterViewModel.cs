using SRS.Web.Models.Shared;

namespace SRS.Web.Models.UsersManagement
{
    public class UserFilterViewModel: BaseFilterViewModel
    {
        public int? CathedraId { get; set; }

        public int? FacultyId { get; set; }
    }
}
