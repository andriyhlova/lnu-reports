using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SRS.Services.Models.UserModels;

namespace SRS.Web.Models.UsersManagement
{
    public class UpdateUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public List<I18nUserInitialsModel> I18nUserInitials { get; set; }

        public bool IsActive { get; set; }

        [Display(Name = "Ролі")]
        public List<string> RoleIds { get; set; }

        public string CathedraName { get; set; }

        public string FacultyName { get; set; }
    }
}
