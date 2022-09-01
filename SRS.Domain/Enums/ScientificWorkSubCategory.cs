using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum ScientificWorkSubCategory
    {
        [Display(Name = "Фундаментальні")]
        Fundamental,
        [Display(Name = "Прикладні")]
        Applied,
        [Display(Name = "Розробки")]
        Developments,
        [Display(Name = "Наукові роботи молодих вчених")]
        ScientificWorkOfYoungScientists
    }
}