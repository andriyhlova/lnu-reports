using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRS.Services.Models.CsvModels
{
    public class CathedraReportCsvModel
    {
        [Name("Протокол")]
        [Index(0)]
        public string Protocol { get; set; }

        [Name("Дата")]
        [Index(1)]
        public string Date { get; set; }

        [Name("Статус")]
        [Index(2)]
        public string State { get; set; }

        [Name("Кафедра")]
        [Index(3)]
        public string CathedraName { get; set; }

        [Name("Керівник кафедри")]
        [Index(4)]
        public string Head { get; set; }
    }
}
