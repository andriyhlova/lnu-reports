using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum Financial
    {
        [Display(Name = "Бюджет")]
        Budget,
        [Display(Name = "Господарчий договір")]
        BusinessContract,
        [Display(Name = "Тема в межах робочого часу викладачів")]
        InWorkTime,
        [Display(Name = "Міжнародний науковий проєкт МОН України")]
        InternationalScienceProject,
        [Display(Name = "Базове фінансування")]
        Base,
        [Display(Name = "Національне надбання")]
        NationalProperty,
        [Display(Name = "Національний фонд досліджень")]
        NationalResearchFoundation,
        [Display(Name = "Міжнародний грант")]
        InternationalGrant
    }
}