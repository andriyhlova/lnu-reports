using CsvHelper.Configuration.Attributes;
using System.ComponentModel;

namespace SRS.Services.Models.CsvModels
{
    public class SpecializationCsvModel : BaseCsvModel
    {
        [Name("Код")]
        [Index(1)]
        [DisplayName("Код")]
        public string Code { get; set; }
    }
}
