using CsvHelper.Configuration.Attributes;
using System.ComponentModel;

namespace SRS.Services.Models.CsvModels
{
    public class DisertationDefenseCsvModel : BaseCsvModel
    {
        [Name("Дата захисту")]
        [Index(1)]
        [DisplayName("Дата захисту")]
        public string DefenseDate { get; set; }

        [Name("Дата подання до спеціалізованої вченої ради")]
        [Index(2)]
        [DisplayName("Дата подання до спеціалізованої вченої ради")]
        public string SubmissionDate { get; set; }

        [Name("Рік закінчення навчання в аспірантурі")]
        [Index(3)]
        [DisplayName("Рік закінчення навчання в аспірантурі")]
        public int YearOfGraduating { get; set; }

        [Name("Керівник")]
        [Index(4)]
        [DisplayName("Керівник")]
        public string SupervisorDescription { get; set; }

        [Name("Виконавець")]
        [Index(5)]
        [DisplayName("Виконавець")]
        public string UserDescription { get; set; }

        [Name("Спеціальність")]
        [Index(6)]
        [DisplayName("Спеціальність")]
        public string Specialization { get; set; }

        [Name("Тип дисертації")]
        [Index(7)]
        [DisplayName("Тип дисертації")]
        public string DissertationType { get; set; }
    }
}
