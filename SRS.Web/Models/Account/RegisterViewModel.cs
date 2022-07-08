using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@(?:(?:[a-zA-Z0-9-]+\\.)?[a-zA-Z]+\\.)?lnu\\.edu\\.ua", ErrorMessage = "Некоректна електронна пошта. Повинен бути: ...@lnu.edu.ua")]
        [EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введіть пароль")]
        [StringLength(100, ErrorMessage = "{0} повинен мати мінімум {2} символи", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        [Compare(nameof(Password), ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Виберіть кафедру зі списку")]
        [Display(Name = "Кафедра")]
        public int CathedraId { get; set; }

        [Required(ErrorMessage = "Виберіть факультет зі списку")]
        [Display(Name = "Факультет")]
        public int FacultyId { get; set; }
    }
}
