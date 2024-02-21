using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class PublicationCsvModel : BaseCsvModel
    {
        [Name("Рік")]
        [Index(1)]
        [DisplayName("Рік")]
        public string Date { get; set; }

        [Name("Тип")]
        [Index(2)]
        [DisplayName("Тип")]
        public string PublicationType { get; set; }

        [Name("Журнал / Видання / Конференція")]
        [Index(3)]
        [DisplayName("Журнал / Видання / Конференція")]
        public string JournalOrChapterMonographyOrConference { get; set; }

        [Name("Автори")]
        [Index(4)]
        [DisplayName("Автори")]
        public string Authors { get; set; }
    }
}
