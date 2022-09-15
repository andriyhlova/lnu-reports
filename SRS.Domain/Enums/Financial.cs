using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum Financial
    {
        [Display(Name = "Наукова, науково-технічна робота чи проєкт МОН України")]
        Budget,
        [Display(Name = "Державне замовлення на науково-технічні розробки")]
        GovernmentOrder,
        [Display(Name = "Національне надбання")]
        NationalProperty,
        [Display(Name = "Базове фінансування")]
        Base,
        [Display(Name = "Міжнародний науковий проєкт МОН України")]
        InternationalScienceProject,
        [Display(Name = "Національний фонд досліджень")]
        NationalResearchFoundation,
        [Display(Name = "Господарчий договір")]
        BusinessContract,
        [Display(Name = "Міжнародний грант")]
        InternationalGrant,
        [Display(Name = "Тема в межах робочого часу викладачів")]
        InWorkTime,
        [Display(Name = "Інше")]
        Other
    }
}