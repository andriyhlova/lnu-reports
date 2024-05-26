using CsvHelper.Configuration.Attributes;
using System.ComponentModel;

namespace SRS.Services.Models.CsvModels
{
    public class CathedraCsvModel : BaseCsvModel
    {
        [Name("Назва факультету")]
        [Index(1)]
        [DisplayName("Назва факультету")]
        public string FacultyName { get; set; }
    }
}
