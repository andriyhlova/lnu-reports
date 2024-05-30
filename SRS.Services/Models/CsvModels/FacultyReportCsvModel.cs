using CsvHelper.Configuration.Attributes;
using System.ComponentModel;

namespace SRS.Services.Models.CsvModels
{
    public class FacultyReportCsvModel
    {
        [Name("Протокол")]
        [Index(0)]
        [DisplayName("Протокол")]
        public string Protocol { get; set; }

        [Name("Дата")]
        [Index(1)]
        [DisplayName("Дата")]
        public string Date { get; set; }

        [Name("Статус")]
        [Index(2)]
        [DisplayName("Статус")]
        public string State { get; set; }

        [Name("Факультет")]
        [Index(3)]
        [DisplayName("Факультет")]
        public string FacultyName { get; set; }

        [Name("Керівник кафедри")]
        [Index(4)]
        [DisplayName("Керівник кафедри")]
        public string Head { get; set; }
    }
}
