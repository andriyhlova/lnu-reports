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
        [Display(Name = "Іспанська")]
        ES,
        [Display(Name = "Французька")]
        FR,
        [Display(Name = "Польська")]
        PL,
        [Display(Name = "Чеська")]
        CS,
        [Display(Name = "Італіська")]
        IT,
        [Display(Name = "Турецька")]
        TR,
        [Display(Name = "Болгарська")]
        BG,
        [Display(Name = "Японська")]
        JA,
        [Display(Name = "Арабська")]
        AR,
        [Display(Name = "Сербська")]
        SR,
        [Display(Name = "Китайська")]
        ZH,
        [Display(Name = "Білоруська")]
        BE,
        [Display(Name = "Російська")]
        RU
    }
}
