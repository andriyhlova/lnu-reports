using CsvHelper.Configuration.Attributes;
using System.ComponentModel;

namespace SRS.Services.Models.CsvModels
{
    public class BaseCsvModel
    {
        [Name("Назва")]
        [Index(0)]
        [DisplayName("Назва")]
        public string Name { get; set; }
    }
}
