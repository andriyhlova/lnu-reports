using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
