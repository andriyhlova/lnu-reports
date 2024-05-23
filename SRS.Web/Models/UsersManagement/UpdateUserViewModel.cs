using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Services.Attributes;

namespace SRS.Web.Models.UsersManagement
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Ролі")]
        public List<string> RoleIds { get; set; }

        [RequiredField]
        public int CathedraId { get; set; }

        [RequiredField]
        public int FacultyId { get; set; }

        [RequiredField]
        public int PositionId { get; set; }
    }
}
