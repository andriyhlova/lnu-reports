using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum AcademicStatusType
    {
        [Display(Name = "За замовчуванням")]
        Default,
        [Display(Name = "Академія наук")]
        AcademyOfScience
    }
}