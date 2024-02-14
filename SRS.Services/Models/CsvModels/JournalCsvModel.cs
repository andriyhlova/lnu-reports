using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class JournalCsvModel : BaseCsvModel
    {
        [Name("Скорочена назва")]
        [Index(1)]
        public string ShortName { get; set; }

        [Name("Тип журналу")]
        [Index(2)]
        public string JournalType { get; set; }

        [Name("Друкований ISSN")]
        [Index(3)]
        public string PrintIssn { get; set; }

        [Name("Електронний ISSN")]
        [Index(4)]
        public string ElectronicIssn { get; set; }

        [Name("Квартиль")]
        [Index(5)]
        public string BestQuartile { get; set; }
    }
}
