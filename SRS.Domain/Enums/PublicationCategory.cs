using System.ComponentModel.DataAnnotations;

namespace SRS.Domain.Enums
{
    public enum PublicationCategory
    {
        [Display(Name = "Гуманітарно-мистецькі")]
        Гуманітарно_мистецькі,
        [Display(Name = "Природничо-математичні")]
        Природничо_математичні,
        [Display(Name = "Інженерно-технічні")]
        Інженерно_технічні,
        [Display(Name = "Соціально-поведінкові")]
        Соціально_поведінкові,
    }
}
