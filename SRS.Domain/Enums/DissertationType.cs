using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum DissertationType
    {
        [Display(Name = "Доктор філософії")]
        PhD,
        [Display(Name = "Доктор наук")]
        Doctor
    }
}
