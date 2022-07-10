using SRS.Services.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.Account
{
    public class ChangePasswordViewModel
    {
        [RequiredField]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль")]
        public string OldPassword { get; set; }

        [RequiredField]
        [StringLength(100, ErrorMessage = "{0} має містити щонайменше {2} символів.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string NewPassword { get; set; }

        [RequiredField]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження нового паролю")]
        [Compare("NewPassword", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }
    }
}