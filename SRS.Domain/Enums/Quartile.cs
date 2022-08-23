using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum Quartile
    {
        [Display(Name = "Немає")]
        None,
        Q1,
        Q2,
        Q3,
        Q4
    }
}