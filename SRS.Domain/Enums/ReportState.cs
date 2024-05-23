using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum ReportState
    {
        [Display(Name = "Новий")]
        Draft,
        [Display(Name = "Підписаний")]
        Signed,
        [Display(Name = "Підтверджений")]
        Confirmed
    }
}