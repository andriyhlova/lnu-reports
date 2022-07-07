using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Електронна пошта")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам’ятати?")]
        public bool RememberMe { get; set; }
    }
}
