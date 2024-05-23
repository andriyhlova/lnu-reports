using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class JournalCsvModel : BaseCsvModel
    {
        [Name("Скорочена назва")]
        [Index(1)]
        [DisplayName("Скорочена назва")]
        public string ShortName { get; set; }

        [Name("Тип журналу")]
        [Index(2)]
        [DisplayName("Тип журналу")]
        public string JournalType { get; set; }

        [Name("Друкований ISSN")]
        [Index(3)]
        [DisplayName("Друкований ISSN")]
        public string PrintIssn { get; set; }

        [Name("Електронний ISSN")]
        [Index(4)]
        [DisplayName("Електронний ISSN")]
        public string ElectronicIssn { get; set; }

        [Name("Квартиль")]
        [Index(5)]
        [DisplayName("Квартиль")]
        public string BestQuartile { get; set; }
    }
}
