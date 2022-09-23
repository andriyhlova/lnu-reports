using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum EditionCategory
    {
        [Display(Name = "Українське")]
        Ukrainian,
        [Display(Name = "Закордонне")]
        Foreign,
    }
}