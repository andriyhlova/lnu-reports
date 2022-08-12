using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum ScientificWorkSubCategory
    {
        [Display(Name = "фундаментальні")]
        Fundamental,
        [Display(Name = "прикладні")]
        Applied,
        [Display(Name = "розробки")]
        Developments,
        [Display(Name = "наукові роботи молодих вчених")]
        ScientificWorkOfYoungScientists
    }
}