using SRS.Services.Models.UserModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SRS.Web.Models.ThemeOfScientificWorks
{
    public class ExternalPartTimeEmployeeViewModel
    {
        [Required]
        public List<I18nUserInitialsModel> I18nUserInitials { get; set; }

        [Required(ErrorMessage = "Введіть електронну пошту")]
        [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Некоректна електронна пошта. Повинна бути: ...@...")]
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

        [Required(ErrorMessage = "Виберіть факультет зі списку")]
        [Display(Name = "Факультет")]
        public int FacultyId { get; set; }

        [Required(ErrorMessage = "Виберіть кафедру зі списку")]
        [Display(Name = "Кафедра")]
        public int CathedraId { get; set; }

        [Required(ErrorMessage = "Виберіть посаду зі списку")]
        [Display(Name = "Посада")]
        public int PositionId { get; set; }
    }
}