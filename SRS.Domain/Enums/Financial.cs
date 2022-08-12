using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum Financial
    {
        [Display(Name = "Бюджет")]
        Budget,
        [Display(Name = "Госпдоговір")]
        BusinessContract,
        [Display(Name = "В межах робочого часу")]
        InWorkTime,
        [Display(Name = "МОН України")]
        MinistryOfEducationAndScienceOfUkraine,
        [Display(Name = "Базове фінансування")]
        Base,
        [Display(Name = "Нацнадбання")]
        NationalProperty,
        [Display(Name = "НФДУ")]
        NationalResearchFoundationOfUkraine,
        [Display(Name = "Міжнародні гранти")]
        InternationalGrants
    }
}