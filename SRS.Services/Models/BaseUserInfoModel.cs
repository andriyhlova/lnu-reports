using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Services.Models
{
    public class BaseUserInfoModel : BaseUserModel
    {
        public string Email { get; set; }

        public List<I18nUserInitialsModel> I18nUserInitials { get; set; }

        [Display(Name = "Ролі")]
        public List<string> RoleIds { get; set; }
    }
}