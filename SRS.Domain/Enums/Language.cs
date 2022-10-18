using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum Language
    {
        [Display(Name = "Українська")]
        UA,
        [Display(Name = "Англійська")]
        EN,
        [Display(Name = "Німецька")]
        DE,
        [Display(Name = "Французька")]
        FR,
        [Display(Name = "Польська")]
        PL,
        [Display(Name = "Італійська")]
        IT,
        [Display(Name = "Інша")]
        Other
    }
}
